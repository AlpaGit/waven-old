// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StateManagement.StateContext
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.InputManagement;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ankama.AssetManagement.StateManagement
{
  [PublicAPI]
  public abstract class StateContext
  {
    private readonly Stack<StateContext> m_pendingChildStates = new Stack<StateContext>(1);
    private readonly StateTransitionInfo m_transitionInfo = new StateTransitionInfo();
    private StateContext m_currentChildState;
    private IEnumerator m_enumerator;
    private YieldInstruction m_yieldInstruction;
    private Coroutine m_yieldInstructionCoroutine;
    private Dictionary<string, StateContext.AssetBundleLoadParams> m_bundleLoadInfos;
    private List<string> m_bundleTrackedRequests;
    private Dictionary<string, SceneLoadRequest> m_sceneLoadInfos;
    private List<string> m_sceneTrackedRequests;
    private bool m_executing;
    private bool m_changedDuringExecution;

    [PublicAPI]
    [CanBeNull]
    public StateContext parent { get; private set; }

    [PublicAPI]
    public StateLoadState loadState { get; internal set; }

    [PublicAPI]
    public virtual bool AllowsTransition([CanBeNull] StateContext nextState) => false;

    [PublicAPI]
    [CanBeNull]
    protected virtual IEnumerator Load() => (IEnumerator) null;

    [PublicAPI]
    protected virtual void Enable()
    {
    }

    [PublicAPI]
    protected virtual bool UseInput(InputState inputState) => false;

    [PublicAPI]
    [CanBeNull]
    protected virtual IEnumerator Update() => (IEnumerator) null;

    [PublicAPI]
    [CanBeNull]
    protected virtual IEnumerator Transition([CanBeNull] StateTransitionInfo transitionInfo) => (IEnumerator) null;

    [PublicAPI]
    protected virtual void Disable()
    {
    }

    [PublicAPI]
    [CanBeNull]
    protected virtual IEnumerator Unload() => this.ReleaseTrackedRequests();

    [PublicAPI]
    public void SetChildState([NotNull] StateContext childState, StateTransitionMode transitionMode = StateTransitionMode.Inherit)
    {
      if (childState == null)
        throw new ArgumentNullException(nameof (childState));
      if (childState.loadState != StateLoadState.Uninitialized)
        throw new ArgumentException("StateContext instances cannot be reused.", nameof (childState));
      switch (this.loadState)
      {
        case StateLoadState.Stopping:
        case StateLoadState.Unloading:
        case StateLoadState.Unloaded:
          UnityEngine.Debug.LogWarning((object) string.Format("[StateManager] Tried to add a child StateContext to a StateContext in an invalid state: {0}.", (object) this.loadState));
          break;
        default:
          foreach (StateContext pendingChildState in this.m_pendingChildStates)
            pendingChildState.UnloadInternal();
          this.m_pendingChildStates.Push(childState);
          childState.LoadInternal(this);
          if (this.m_currentChildState != null)
            this.m_currentChildState.TransitionInternal(transitionMode, childState);
          if (!this.m_executing)
            break;
          this.m_changedDuringExecution = true;
          break;
      }
    }

    [PublicAPI]
    public void ClearChildState(StateTransitionMode transitionMode = StateTransitionMode.Inherit)
    {
      switch (this.loadState)
      {
        case StateLoadState.Transitioning:
        case StateLoadState.Stopping:
          break;
        case StateLoadState.Unloading:
        case StateLoadState.Unloaded:
          break;
        default:
          foreach (StateContext pendingChildState in this.m_pendingChildStates)
            pendingChildState.UnloadInternal();
          if (this.m_currentChildState != null)
            this.m_currentChildState.StopInternal(transitionMode);
          if (!this.m_executing)
            break;
          this.m_changedDuringExecution = true;
          break;
      }
    }

    [PublicAPI]
    public bool HasChildState() => this.m_currentChildState != null;

    [PublicAPI]
    [CanBeNull]
    public StateContext GetChildState() => this.m_currentChildState;

    [PublicAPI]
    public bool HasPendingStates() => this.m_pendingChildStates.Count > 0;

    [PublicAPI]
    [CanBeNull]
    public StateContext GetPendingChildState()
    {
      if (this.m_pendingChildStates.Count == 0)
        return (StateContext) null;
      StateContext pendingChildState = this.m_pendingChildStates.Peek();
      switch (pendingChildState.loadState)
      {
        case StateLoadState.Uninitialized:
        case StateLoadState.Loading:
        case StateLoadState.Loaded:
        case StateLoadState.Enabled:
          return pendingChildState;
        case StateLoadState.Updating:
        case StateLoadState.Stopping:
          throw new Exception(string.Format("StateContext is pending in an invalid load state: {0}", (object) pendingChildState.loadState));
        case StateLoadState.Disabled:
        case StateLoadState.Unloading:
        case StateLoadState.Unloaded:
          return (StateContext) null;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [PublicAPI]
    [CanBeNull]
    public StateLayer GetLayer()
    {
      StateContext layer = this;
      for (StateContext parent = this.parent; parent != null; parent = layer.parent)
        layer = parent;
      return layer as StateLayer;
    }

    [PublicAPI]
    [NotNull]
    public StateContext GetChainRoot()
    {
      StateContext stateContext = this;
      for (StateContext parent = this.parent; parent != null; parent = stateContext.parent)
        stateContext = parent;
      return !(stateContext is StateLayer) ? stateContext : stateContext.m_currentChildState;
    }

    [PublicAPI]
    [NotNull]
    public StateContext GetChainEnd()
    {
      StateContext chainEnd = this;
      for (StateContext currentChildState = this.m_currentChildState; currentChildState != null; currentChildState = chainEnd.m_currentChildState)
        chainEnd = currentChildState;
      return chainEnd;
    }

    [PublicAPI]
    [NotNull]
    protected AssetBundleLoadRequest LoadAssetBundle([NotNull] string bundleName)
    {
      StateContext.AssetBundleLoadParams bundleLoadParams;
      if (this.m_bundleLoadInfos == null)
      {
        this.m_bundleLoadInfos = new Dictionary<string, StateContext.AssetBundleLoadParams>((IEqualityComparer<string>) StringComparer.Ordinal);
        this.m_bundleTrackedRequests = new List<string>(1);
        bundleLoadParams = new StateContext.AssetBundleLoadParams(1);
      }
      else if (this.m_bundleLoadInfos.TryGetValue(bundleName, out bundleLoadParams))
        ++bundleLoadParams.referenceCount;
      else
        bundleLoadParams = new StateContext.AssetBundleLoadParams(1);
      AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      if (!bundleLoadRequest.isDone)
      {
        bundleLoadParams.loadRequest = bundleLoadRequest;
        this.m_bundleTrackedRequests.Add(bundleName);
      }
      else if ((int) bundleLoadRequest.error != 0)
        --bundleLoadParams.referenceCount;
      this.m_bundleLoadInfos[bundleName] = bundleLoadParams;
      return bundleLoadRequest;
    }

    [PublicAPI]
    [CanBeNull]
    protected AssetBundleUnloadRequest UnloadAssetBundle(
      string bundleName,
      bool unloadDependencies,
      bool unloadAssets)
    {
      if (this.m_bundleLoadInfos != null)
      {
        StateContext.AssetBundleLoadParams bundleLoadParams;
        if (this.m_bundleLoadInfos.TryGetValue(bundleName, out bundleLoadParams))
        {
          if (bundleLoadParams.referenceCount > 0)
          {
            --bundleLoadParams.referenceCount;
            this.m_bundleLoadInfos[bundleName] = bundleLoadParams;
            return AssetManager.UnloadAssetBundle(bundleName);
          }
          UnityEngine.Debug.LogWarning((object) ("[StateManager] Tried to unload asset bundle named '" + bundleName + "' more time than it was loaded from the state context."));
          return (AssetBundleUnloadRequest) null;
        }
        UnityEngine.Debug.LogWarning((object) ("[StateManager] Tried to unload asset bundle named '" + bundleName + "' but it was not loaded from the state context."));
        return (AssetBundleUnloadRequest) null;
      }
      UnityEngine.Debug.LogWarning((object) ("[StateManager] Tried to unload asset bundle named '" + bundleName + "' before any bundle was loaded from the state context."));
      return (AssetBundleUnloadRequest) null;
    }

    [PublicAPI]
    [NotNull]
    protected SceneLoadRequest LoadScene(
      [NotNull] string sceneName,
      [NotNull] string bundleName,
      LoadSceneParameters loadSceneParameters,
      bool allowSceneActivation = true,
      Action<SceneLoadRequest> completed = null)
    {
      SceneLoadRequest sceneLoadRequest;
      if (this.m_sceneLoadInfos == null)
      {
        this.m_sceneLoadInfos = new Dictionary<string, SceneLoadRequest>((IEqualityComparer<string>) StringComparer.Ordinal);
        this.m_sceneTrackedRequests = new List<string>(1);
      }
      else if (this.m_sceneLoadInfos.TryGetValue(sceneName, out sceneLoadRequest))
      {
        sceneLoadRequest.completed = completed;
        return sceneLoadRequest;
      }
      sceneLoadRequest = AssetManager.LoadScene(sceneName, bundleName, loadSceneParameters, allowSceneActivation, completed);
      if (!sceneLoadRequest.isDone)
        this.m_sceneTrackedRequests.Add(sceneName);
      this.m_sceneLoadInfos[sceneName] = sceneLoadRequest;
      return sceneLoadRequest;
    }

    [PublicAPI]
    [CanBeNull]
    protected AsyncOperation UnloadScene([NotNull] string sceneName)
    {
      SceneLoadRequest sceneLoadRequest;
      if (this.m_sceneLoadInfos == null || !this.m_sceneLoadInfos.TryGetValue(sceneName, out sceneLoadRequest))
        return (AsyncOperation) null;
      sceneLoadRequest.Abort();
      this.m_sceneLoadInfos.Remove(sceneName);
      this.m_sceneTrackedRequests.Remove(sceneName);
      return SceneManager.UnloadSceneAsync(sceneName);
    }

    [PublicAPI]
    protected IEnumerator ReleaseTrackedRequests()
    {
      if (this.m_sceneLoadInfos != null)
      {
        int count = this.m_sceneLoadInfos.Count;
        if (count > 0)
        {
          StateContext.SceneUnloadData[] data = new StateContext.SceneUnloadData[count];
          int index = 0;
          foreach (KeyValuePair<string, SceneLoadRequest> sceneLoadInfo in this.m_sceneLoadInfos)
          {
            string key = sceneLoadInfo.Key;
            SceneLoadRequest loadRequest = sceneLoadInfo.Value;
            data[index] = new StateContext.SceneUnloadData(key, loadRequest);
            ++index;
          }
          yield return (object) this.UnloadTrackedSceneInParallel(data);
        }
        this.m_sceneTrackedRequests.Clear();
        this.m_sceneTrackedRequests = (List<string>) null;
        this.m_sceneLoadInfos.Clear();
        this.m_sceneLoadInfos = (Dictionary<string, SceneLoadRequest>) null;
      }
      if (this.m_bundleLoadInfos != null)
      {
        foreach (KeyValuePair<string, StateContext.AssetBundleLoadParams> bundleLoadInfo in this.m_bundleLoadInfos)
        {
          string key = bundleLoadInfo.Key;
          for (int referenceCount = bundleLoadInfo.Value.referenceCount; referenceCount > 0; --referenceCount)
            AssetManager.UnloadAssetBundle(key);
        }
        this.m_bundleTrackedRequests.Clear();
        this.m_bundleTrackedRequests = (List<string>) null;
        this.m_bundleLoadInfos.Clear();
        this.m_bundleLoadInfos = (Dictionary<string, StateContext.AssetBundleLoadParams>) null;
      }
    }

    internal bool ProcessInput(InputState inputState)
    {
      foreach (StateContext pendingChildState in this.m_pendingChildStates)
      {
        switch (pendingChildState.loadState)
        {
          case StateLoadState.Loading:
          case StateLoadState.Loaded:
            if (pendingChildState.ProcessInput(inputState))
              return true;
            continue;
          default:
            continue;
        }
      }
      if (this.m_currentChildState != null && this.m_currentChildState.ProcessInput(inputState))
        return true;
      try
      {
        return this.UseInput(inputState);
      }
      catch (Exception ex)
      {
        UnityEngine.Debug.LogException(ex);
        return false;
      }
    }

    internal bool Execute()
    {
      List<string> bundleTrackedRequests = this.m_bundleTrackedRequests;
      if (bundleTrackedRequests != null)
      {
        Dictionary<string, StateContext.AssetBundleLoadParams> bundleLoadInfos = this.m_bundleLoadInfos;
        int count = bundleTrackedRequests.Count;
        for (int index = 0; index < count; ++index)
        {
          string key = bundleTrackedRequests[index];
          StateContext.AssetBundleLoadParams bundleLoadParams = bundleLoadInfos[key];
          AssetBundleLoadRequest loadRequest = bundleLoadParams.loadRequest;
          if (loadRequest.isDone)
          {
            if ((int) loadRequest.error != 0)
              --bundleLoadParams.referenceCount;
            bundleLoadParams.loadRequest = (AssetBundleLoadRequest) null;
            bundleLoadInfos[key] = bundleLoadParams;
            bundleTrackedRequests.RemoveAt(index);
            --count;
            --index;
          }
        }
      }
      List<string> sceneTrackedRequests = this.m_sceneTrackedRequests;
      if (sceneTrackedRequests != null)
      {
        Dictionary<string, SceneLoadRequest> sceneLoadInfos = this.m_sceneLoadInfos;
        int count = sceneTrackedRequests.Count;
        for (int index = 0; index < count; ++index)
        {
          string key = sceneTrackedRequests[index];
          if (sceneLoadInfos[key].isReady)
          {
            sceneTrackedRequests.RemoveAt(index);
            --count;
            --index;
            sceneLoadInfos[key] = (SceneLoadRequest) null;
          }
        }
      }
      this.m_executing = true;
      bool flag;
      switch (this.loadState)
      {
        case StateLoadState.Uninitialized:
          flag = false;
          break;
        case StateLoadState.Loading:
          flag = this.ExecuteLoading();
          break;
        case StateLoadState.Loaded:
          flag = this.ExecuteLoaded();
          break;
        case StateLoadState.Enabled:
          flag = true;
          break;
        case StateLoadState.Updating:
          this.ExecuteUpdating();
          flag = true;
          break;
        case StateLoadState.Transitioning:
          flag = this.ExecuteTransitioning();
          break;
        case StateLoadState.Stopping:
          flag = this.ExecuteStopping();
          break;
        case StateLoadState.Disabled:
          flag = true;
          break;
        case StateLoadState.Unloading:
          flag = this.ExecuteUnloading();
          break;
        case StateLoadState.Unloaded:
          flag = false;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.m_executing = false;
      this.m_changedDuringExecution = false;
      return flag;
    }

    private bool ExecuteLoading()
    {
      if (this.m_pendingChildStates.Count > 0)
      {
        StateContext stateContext = this.m_pendingChildStates.Peek();
        if (stateContext.loadState == StateLoadState.Loading && (stateContext.Execute() || this.m_changedDuringExecution))
          return true;
      }
      if (this.m_enumerator == null)
      {
        this.m_enumerator = this.Load();
        if (this.m_enumerator == null)
        {
          this.loadState = StateLoadState.Loaded;
          return false;
        }
      }
      if (this.ExecuteEnumerator(this.m_enumerator) || this.m_changedDuringExecution)
        return true;
      this.loadState = StateLoadState.Loaded;
      return false;
    }

    private bool ExecuteLoaded()
    {
      while (this.m_pendingChildStates.Count > 0)
      {
        StateContext stateContext = this.m_pendingChildStates.Peek();
        if (stateContext.Execute() || this.m_changedDuringExecution)
          return true;
        if (stateContext.loadState == StateLoadState.Loaded)
        {
          StateContext currentChildState = this.m_currentChildState;
          if (currentChildState == null)
          {
            this.m_pendingChildStates.Pop();
            stateContext.EnableInternal();
            this.m_currentChildState = stateContext;
            stateContext.UpdateInternal();
          }
          else if (currentChildState.m_transitionInfo.isDone)
          {
            this.m_pendingChildStates.Pop();
            stateContext.EnableInternal();
            this.m_currentChildState = stateContext;
            stateContext.UpdateInternal();
            currentChildState.DisableInternal();
            this.m_pendingChildStates.Push(currentChildState);
            currentChildState.UnloadInternal();
          }
          else
            break;
        }
        else
          this.m_pendingChildStates.Pop();
      }
      return false;
    }

    private void ExecuteUpdating()
    {
      while (this.m_pendingChildStates.Count > 0)
      {
        StateContext stateContext = this.m_pendingChildStates.Peek();
        if (!stateContext.Execute() && !this.m_changedDuringExecution)
        {
          if (stateContext.loadState == StateLoadState.Loaded)
          {
            StateContext currentChildState = this.m_currentChildState;
            if (currentChildState == null)
            {
              this.m_pendingChildStates.Pop();
              stateContext.EnableInternal();
              this.m_currentChildState = stateContext;
              stateContext.UpdateInternal();
            }
            else if (currentChildState.m_transitionInfo.isDone)
            {
              this.m_pendingChildStates.Pop();
              stateContext.EnableInternal();
              this.m_currentChildState = stateContext;
              stateContext.UpdateInternal();
              currentChildState.DisableInternal();
              this.m_pendingChildStates.Push(currentChildState);
              currentChildState.UnloadInternal();
            }
            else
              break;
          }
          else
            this.m_pendingChildStates.Pop();
        }
        else
          break;
      }
      if (this.m_enumerator != null && !this.ExecuteEnumerator(this.m_enumerator))
        this.m_enumerator = (IEnumerator) null;
      StateContext currentChildState1 = this.m_currentChildState;
      if (currentChildState1 == null || currentChildState1.Execute() || currentChildState1.loadState != StateLoadState.Unloaded)
        return;
      this.m_currentChildState = (StateContext) null;
    }

    private bool ExecuteTransitioning()
    {
      while (this.m_pendingChildStates.Count > 0 && !this.m_pendingChildStates.Peek().Execute())
        this.m_pendingChildStates.Pop();
      StateContext currentChildState = this.m_currentChildState;
      if (currentChildState != null && !currentChildState.Execute() && currentChildState.loadState == StateLoadState.Unloaded)
        this.m_currentChildState = (StateContext) null;
      if (this.m_currentChildState != null || this.m_pendingChildStates.Count > 0)
        return true;
      if (this.m_transitionInfo.isDone)
        return false;
      if (this.m_enumerator == null)
      {
        this.m_enumerator = this.Transition(this.m_transitionInfo);
        if (this.m_enumerator == null)
        {
          this.m_transitionInfo.isDone = true;
          return false;
        }
      }
      if (this.ExecuteEnumerator(this.m_enumerator))
        return true;
      this.m_transitionInfo.isDone = true;
      this.m_enumerator = (IEnumerator) null;
      return false;
    }

    private bool ExecuteStopping()
    {
      while (this.m_pendingChildStates.Count > 0 && !this.m_pendingChildStates.Peek().Execute())
        this.m_pendingChildStates.Pop();
      StateContext currentChildState = this.m_currentChildState;
      if (currentChildState != null && !currentChildState.Execute() && currentChildState.loadState == StateLoadState.Unloaded)
        this.m_currentChildState = (StateContext) null;
      if (this.m_currentChildState != null || this.m_pendingChildStates.Count > 0)
        return true;
      if (this.m_enumerator == null)
      {
        this.m_enumerator = this.Transition((StateTransitionInfo) null);
        if (this.m_enumerator == null)
        {
          this.DisableInternal();
          this.UnloadInternal();
          return false;
        }
      }
      if (this.ExecuteEnumerator(this.m_enumerator))
        return true;
      this.DisableInternal();
      this.UnloadInternal();
      return false;
    }

    private bool ExecuteUnloading()
    {
      while (this.m_pendingChildStates.Count > 0 && !this.m_pendingChildStates.Peek().Execute())
        this.m_pendingChildStates.Pop();
      StateContext currentChildState = this.m_currentChildState;
      if (currentChildState != null && !currentChildState.Execute() && currentChildState.loadState == StateLoadState.Unloaded)
        this.m_currentChildState = (StateContext) null;
      if (this.m_currentChildState != null || this.m_pendingChildStates.Count > 0)
        return true;
      if (this.m_enumerator == null)
      {
        this.m_enumerator = this.Unload();
        if (this.m_enumerator == null)
        {
          this.loadState = StateLoadState.Unloaded;
          return false;
        }
      }
      if (this.ExecuteEnumerator(this.m_enumerator))
        return true;
      this.loadState = StateLoadState.Unloaded;
      return false;
    }

    private void LoadInternal(StateContext parentContext)
    {
      this.parent = parentContext;
      this.loadState = StateLoadState.Loading;
    }

    private void EnableInternal()
    {
      this.Enable();
      this.loadState = StateLoadState.Enabled;
    }

    private void UpdateInternal()
    {
      this.loadState = StateLoadState.Updating;
      this.m_enumerator = this.Update();
    }

    private void TransitionInternal(StateTransitionMode transitionMode, StateContext nextState)
    {
      if (this.loadState == StateLoadState.Updating)
      {
        foreach (StateContext pendingChildState in this.m_pendingChildStates)
          pendingChildState.UnloadInternal();
        if (this.m_currentChildState != null)
          this.m_currentChildState.StopInternal(transitionMode);
        this.ClearEnumerator();
        bool flag = transitionMode == StateTransitionMode.Inherit ? this.AllowsTransition(nextState) : transitionMode == StateTransitionMode.Enabled;
        this.m_transitionInfo.stateContext = nextState;
        this.m_transitionInfo.isDone = !flag;
        if (!flag)
          return;
        this.loadState = StateLoadState.Transitioning;
      }
      else
      {
        if (this.loadState != StateLoadState.Transitioning)
          return;
        bool flag = transitionMode == StateTransitionMode.Inherit ? this.AllowsTransition(nextState) : transitionMode == StateTransitionMode.Enabled;
        this.m_transitionInfo.stateContext = nextState;
        this.m_transitionInfo.isDone = !flag;
      }
    }

    private void StopInternal(StateTransitionMode transitionMode)
    {
      if (this.loadState == StateLoadState.Updating)
      {
        foreach (StateContext pendingChildState in this.m_pendingChildStates)
          pendingChildState.UnloadInternal();
        int num = transitionMode == StateTransitionMode.Inherit ? (this.AllowsTransition((StateContext) null) ? 1 : 0) : (transitionMode == StateTransitionMode.Enabled ? 1 : 0);
        if (this.m_currentChildState != null)
          this.m_currentChildState.StopInternal(transitionMode);
        this.ClearEnumerator();
        if (num != 0)
        {
          this.loadState = StateLoadState.Stopping;
        }
        else
        {
          this.DisableInternal();
          this.UnloadInternal();
        }
      }
      else
      {
        if (this.loadState != StateLoadState.Transitioning)
          return;
        bool flag = transitionMode == StateTransitionMode.Inherit ? this.AllowsTransition((StateContext) null) : transitionMode == StateTransitionMode.Enabled;
        this.m_transitionInfo.stateContext = (StateContext) null;
        this.m_transitionInfo.isDone = !flag;
        this.ClearEnumerator();
        if (flag)
        {
          this.loadState = StateLoadState.Stopping;
        }
        else
        {
          this.DisableInternal();
          this.UnloadInternal();
        }
      }
    }

    private void DisableInternal()
    {
      if (this.loadState != StateLoadState.Updating && this.loadState != StateLoadState.Transitioning && this.loadState != StateLoadState.Stopping && this.loadState != StateLoadState.Enabled)
        return;
      if (this.m_currentChildState != null)
        this.m_currentChildState.DisableInternal();
      this.ClearEnumerator();
      this.Disable();
      this.loadState = StateLoadState.Disabled;
    }

    private void UnloadInternal()
    {
      if (this.loadState == StateLoadState.Unloading || this.loadState == StateLoadState.Unloaded)
        return;
      foreach (StateContext pendingChildState in this.m_pendingChildStates)
        pendingChildState.UnloadInternal();
      if (this.m_currentChildState != null)
        this.m_currentChildState.UnloadInternal();
      this.ClearEnumerator();
      this.loadState = StateLoadState.Unloading;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ClearEnumerator()
    {
      if (this.m_yieldInstructionCoroutine != null)
        StateManager.callbackSource.StopCoroutine(this.m_yieldInstructionCoroutine);
      this.m_yieldInstructionCoroutine = (Coroutine) null;
      this.m_yieldInstruction = (YieldInstruction) null;
      this.m_enumerator = (IEnumerator) null;
    }

    private bool ExecuteEnumerator(IEnumerator enumerator)
    {
label_0:
      object current = enumerator.Current;
      if (current != null)
      {
        if (!(current is IEnumerator enumerator1))
        {
          if (current is YieldInstruction yieldInstruction)
          {
            if (this.m_yieldInstruction == yieldInstruction)
            {
              if (this.m_yieldInstructionCoroutine == null)
              {
                this.m_yieldInstruction = (YieldInstruction) null;
                goto label_10;
              }
            }
            else
            {
              this.m_yieldInstruction = yieldInstruction;
              this.m_yieldInstructionCoroutine = StateManager.callbackSource.StartCoroutine(this.ExecuteYieldInstruction(yieldInstruction));
            }
            return true;
          }
        }
        else if (this.ExecuteEnumerator(enumerator1))
          return true;
      }
label_10:
      try
      {
        if (!enumerator.MoveNext())
          return false;
        if (enumerator.Current == null)
          return true;
        goto label_0;
      }
      catch (Exception ex)
      {
        UnityEngine.Debug.LogException(ex);
        return true;
      }
    }

    private IEnumerator ExecuteYieldInstruction(YieldInstruction yieldInstruction)
    {
      yield return (object) yieldInstruction;
      this.m_yieldInstructionCoroutine = (Coroutine) null;
    }

    private IEnumerator UnloadTrackedSceneInParallel(StateContext.SceneUnloadData[] data)
    {
      int dataCount = data.Length;
      while (true)
      {
        bool flag = true;
        for (int index = 0; index < dataCount; ++index)
        {
          StateContext.SceneUnloadData sceneUnloadData = data[index];
          if (!sceneUnloadData.isDone)
          {
            SceneLoadRequest loadRequest = sceneUnloadData.loadRequest;
            if (loadRequest != null && !loadRequest.isDone)
            {
              flag = false;
            }
            else
            {
              AsyncOperation asyncOperation = sceneUnloadData.unloadOperation;
              if (asyncOperation == null)
              {
                Scene sceneByName = SceneManager.GetSceneByName(sceneUnloadData.sceneName);
                if (sceneByName.IsValid())
                {
                  asyncOperation = SceneManager.UnloadSceneAsync(sceneByName);
                  if (asyncOperation == null)
                  {
                    data[index].isDone = true;
                    continue;
                  }
                  data[index].unloadOperation = asyncOperation;
                }
                else
                {
                  data[index].isDone = true;
                  continue;
                }
              }
              if (asyncOperation.isDone)
                data[index].isDone = true;
              else
                flag = false;
            }
          }
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [Conditional("UNITY_EDITOR")]
    private void LogLoading()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogLoad()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogLoaded()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogEnabled()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogUpdating()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogFinishedUpdating()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogTransitioning()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogTransition()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogTransitioned()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogStopping()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogStop()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogStopped()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogDisabled()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogUnloading()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogUnload()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void LogUnloaded()
    {
    }

    [Conditional("UNITY_EDITOR")]
    private void NotifyStateChanged()
    {
    }

    private struct AssetBundleLoadParams
    {
      public int referenceCount;
      public AssetBundleLoadRequest loadRequest;

      public AssetBundleLoadParams(int initialReferenceCount)
      {
        this.referenceCount = initialReferenceCount;
        this.loadRequest = (AssetBundleLoadRequest) null;
      }
    }

    private struct SceneUnloadData
    {
      public readonly string sceneName;
      public readonly SceneLoadRequest loadRequest;
      public AsyncOperation unloadOperation;
      public bool isDone;

      public SceneUnloadData(string sceneName, SceneLoadRequest loadRequest)
      {
        this.sceneName = sceneName;
        this.loadRequest = loadRequest;
        this.unloadOperation = (AsyncOperation) null;
        this.isDone = false;
        loadRequest?.Abort();
      }
    }
  }
}
