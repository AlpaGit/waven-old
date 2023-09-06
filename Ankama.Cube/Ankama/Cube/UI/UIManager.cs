// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.UIManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.SRP;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public class UIManager : MonoBehaviour
  {
    [SerializeField]
    private Camera m_cameraPrefab;
    [SerializeField]
    private int m_defaultUiDepth = 10;
    [SerializeField]
    private int m_defaultUiStep = 2;
    private readonly UIManager.RootNodeComparer m_rootNodeComparer = new UIManager.RootNodeComparer();
    private readonly List<UIManager.Node> m_rootNodes = new List<UIManager.Node>();
    private readonly Dictionary<StateContext, UIManager.Node> m_stateContextToNode = new Dictionary<StateContext, UIManager.Node>();
    private readonly Dictionary<AbstractUI, UIManager.Node> m_uiToNode = new Dictionary<AbstractUI, UIManager.Node>();
    private bool m_updateOrder;
    private UICameraPool m_cameraPool;
    private bool m_isBlurActive;
    private bool m_userInteractionLocked;
    private float m_userInteractionLockTimer;
    private Coroutine m_userInteractionLockCheckRoutine;
    private UIManager.UserInteractionLockSettings m_userInteractionLockSettings;

    public static UIManager instance { get; private set; }

    public float lastDepth { get; private set; }

    public int lastSortingOrder { get; private set; }

    [PublicAPI]
    public bool userInteractionLocked => this.m_userInteractionLocked;

    private void Awake()
    {
      UIManager.instance = this;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      this.m_cameraPool = new UICameraPool(this.m_cameraPrefab);
      this.m_cameraPrefab.gameObject.SetActive(false);
      this.UpdateBlurActiveState();
      QualityManager.onChanged += new Action<QualityAsset>(this.OnQualityChanged);
    }

    private void OnDisable()
    {
      this.m_cameraPool.ReleaseAll();
      this.m_userInteractionLockCheckRoutine = (Coroutine) null;
    }

    private void OnDestroy()
    {
      QualityManager.onChanged -= new Action<QualityAsset>(this.OnQualityChanged);
      if (!((UnityEngine.Object) UIManager.instance == (UnityEngine.Object) this))
        return;
      UIManager.instance = (UIManager) null;
    }

    public void NotifyLayerIndexChange()
    {
      int count = this.m_rootNodes.Count;
      for (int index = 0; index < count; ++index)
      {
        UIManager.Node rootNode = this.m_rootNodes[index];
        StateLayer layer = rootNode.context.GetLayer();
        if (layer != null)
          rootNode.rootIndex = layer.index;
      }
      this.m_rootNodes.Sort((IComparer<UIManager.Node>) this.m_rootNodeComparer);
      this.m_updateOrder = true;
    }

    public void NotifyUIDepthChanged(AbstractUI ui, StateContext state, int index = -1)
    {
      UIManager.Node node1;
      if (this.m_uiToNode.TryGetValue(ui, out node1))
      {
        node1.uis.Remove(ui);
        this.m_uiToNode.Remove(ui);
      }
      else
        ui.canvas.worldCamera = this.m_cameraPrefab;
      UIManager.Node node2 = this.GetOrCreateNode(state);
      if (index == -1)
        node2.uis.Add(ui);
      else
        node2.uis.Insert(index, ui);
      this.m_uiToNode.Add(ui, node2);
      if (!ui.gameObject.activeInHierarchy)
        return;
      this.m_updateOrder = true;
    }

    [PublicAPI]
    public UICamera GetCamera() => this.m_cameraPool.busyCameras[0];

    public void EnableStateChanged(AbstractUI ui, bool enable)
    {
      if (!this.m_uiToNode.ContainsKey(ui))
        return;
      this.m_updateOrder = true;
    }

    public void UseBlurChanged(AbstractUI ui, bool enable)
    {
      if (!this.m_isBlurActive || !ui.gameObject.activeInHierarchy || !this.m_uiToNode.ContainsKey(ui))
        return;
      this.m_updateOrder = true;
    }

    public void NotifyUIDestroyed(AbstractUI ui)
    {
      UIManager.Node node;
      if (!this.m_uiToNode.TryGetValue(ui, out node))
        return;
      node.uis.Remove(ui);
      this.m_uiToNode.Remove(ui);
      if (UIManager.CanBeRemoved(node))
      {
        for (UIManager.Node parent = node.parent; parent != null && parent.children.Count == 1 && parent.uis.Count == 0; parent = node.parent)
          node = parent;
        this.RemoveNode(node);
      }
      this.m_updateOrder = true;
    }

    private UIManager.Node GetOrCreateNode(StateContext state)
    {
      UIManager.Node node1;
      if (!this.m_stateContextToNode.TryGetValue(state, out node1))
      {
        node1 = new UIManager.Node()
        {
          context = state,
          renderBeforeParent = state is IStateUIChildPriority stateUiChildPriority && stateUiChildPriority.uiChildPriority == UIPriority.Back
        };
        this.m_stateContextToNode.Add(state, node1);
        StateContext parent = state.parent;
        if (parent != null)
        {
          UIManager.Node node2 = this.GetOrCreateNode(parent);
          if (state is IStateUITransitionPriority transitionPriority && transitionPriority.uiTransitionPriority == UIPriority.Back)
            node2.children.Insert(0, node1);
          else
            node2.children.Add(node1);
          node1.parent = node2;
        }
        else
        {
          if (state is StateLayer stateLayer)
            node1.rootIndex = stateLayer.index;
          this.m_rootNodes.Add(node1);
          this.m_rootNodes.Sort((IComparer<UIManager.Node>) this.m_rootNodeComparer);
        }
      }
      return node1;
    }

    private static bool CanBeRemoved(UIManager.Node node)
    {
      bool flag = true;
      List<UIManager.Node> children = node.children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
      {
        UIManager.Node node1 = children[index];
        flag &= UIManager.CanBeRemoved(node1);
      }
      return flag && node.uis.Count == 0;
    }

    private void RemoveNode(UIManager.Node node)
    {
      List<UIManager.Node> children = node.children;
      int count = children.Count;
      for (int index = 0; index < count; ++index)
        this.RemoveNode(children[index]);
      this.m_stateContextToNode.Remove(node.context);
      UIManager.Node parent = node.parent;
      if (parent == null)
        this.m_rootNodes.Remove(node);
      else
        parent.children.Remove(node);
    }

    private void LateUpdate()
    {
      if (!this.m_updateOrder)
        return;
      this.m_updateOrder = false;
      this.UpdateOrder();
    }

    private void UpdateOrder()
    {
      this.m_cameraPool.ReleaseAll();
      if (this.m_uiToNode.Count == 0)
        return;
      int sortingOrder = 0;
      UICamera uiCamera = (UICamera) null;
      int count = this.m_rootNodes.Count;
      for (int index = 0; index < count; ++index)
        this.SetUINodeOrder(this.m_rootNodes[index], ref uiCamera, ref sortingOrder);
      bool flag = false;
      float num1 = 0.0f;
      for (int index1 = this.m_cameraPool.busyCameras.Count - 1; index1 >= 0; --index1)
      {
        UICamera busyCamera = this.m_cameraPool.busyCameras[index1];
        busyCamera.camera.nearClipPlane = num1;
        float num2 = num1 + (float) this.m_defaultUiStep;
        for (int index2 = busyCamera.uis.Count - 1; index2 >= 0; --index2)
        {
          AbstractUI ui = busyCamera.uis[index2];
          num2 += (float) this.m_defaultUiDepth;
          ui.canvas.planeDistance = num2;
        }
        num1 = num2 + (float) this.m_defaultUiStep;
        busyCamera.camera.farClipPlane = num1;
        if (busyCamera.hasBlur)
        {
          if (flag)
          {
            busyCamera.NeedToDisplayBlur(false);
          }
          else
          {
            busyCamera.NeedToDisplayBlur(true);
            flag = busyCamera.isFullBlur;
          }
        }
      }
      this.lastSortingOrder = sortingOrder;
      this.lastDepth = num1;
    }

    private void SetUINodeOrder(UIManager.Node node, ref UICamera uiCamera, ref int sortingOrder)
    {
      List<UIManager.Node> children = node.children;
      int count1 = children.Count;
      for (int index = 0; index < count1; ++index)
      {
        UIManager.Node node1 = children[index];
        if (node1.renderBeforeParent)
          this.SetUINodeOrder(node1, ref uiCamera, ref sortingOrder);
      }
      List<AbstractUI> uis = node.uis;
      int count2 = uis.Count;
      for (int index = 0; index < count2; ++index)
      {
        AbstractUI linkedUI = uis[index];
        if (linkedUI.gameObject.activeInHierarchy)
        {
          if (linkedUI.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
          {
            linkedUI.canvas.sortingOrder = sortingOrder;
            ++sortingOrder;
          }
          else
          {
            if (this.m_isBlurActive && linkedUI.useBlur)
            {
              UICamera uiCamera1 = uiCamera;
              uiCamera = this.m_cameraPool.Get();
              uiCamera.ActiveBlurFor(linkedUI);
              uiCamera.child = uiCamera1;
            }
            if (uiCamera == null)
              uiCamera = this.m_cameraPool.Get();
            uiCamera.uis.Add(linkedUI);
            linkedUI.canvas.worldCamera = uiCamera.camera;
          }
        }
      }
      for (int index = 0; index < count1; ++index)
      {
        UIManager.Node node2 = children[index];
        if (!node2.renderBeforeParent)
          this.SetUINodeOrder(node2, ref uiCamera, ref sortingOrder);
      }
    }

    private void OnQualityChanged(QualityAsset current) => this.UpdateBlurActiveState();

    private void UpdateBlurActiveState()
    {
      bool activeBlurState = this.GetActiveBlurState();
      if (this.m_isBlurActive == activeBlurState)
        return;
      this.m_isBlurActive = activeBlurState;
      if (this.m_uiToNode.Count <= 0)
        return;
      this.m_updateOrder = true;
    }

    private bool GetActiveBlurState() => SystemInfo.supportsImageEffects && QualityManager.current.uiBlurQuality != UIBlurQuality.None;

    public void LockUserInteraction(
      float warningTimeout = 1f,
      float errorTimeout = 8f,
      Action warningTimeoutCallback = null,
      Action errorTimeoutCallback = null)
    {
      if (this.m_userInteractionLocked)
        Log.Warning("User Interaction is being locked but it was already acquired.", 489, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\UIManager.cs");
      this.m_userInteractionLockSettings = new UIManager.UserInteractionLockSettings(warningTimeout, errorTimeout, warningTimeoutCallback, errorTimeoutCallback);
      this.m_userInteractionLockTimer = 0.0f;
      this.m_userInteractionLocked = true;
      if (this.m_userInteractionLockCheckRoutine != null)
      {
        this.StopCoroutine(this.m_userInteractionLockCheckRoutine);
        this.m_userInteractionLockCheckRoutine = (Coroutine) null;
      }
      if (!this.m_userInteractionLockSettings.hasAnyCallback)
        return;
      this.m_userInteractionLockCheckRoutine = this.StartCoroutine(this.UserInteractionTimeoutCheckRoutine());
    }

    public void ReleaseUserInteractionLock()
    {
      if (!this.m_userInteractionLocked)
        Log.Warning("User Interaction Lock is being released but it was not acquired.", 512, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\UIManager.cs");
      this.m_userInteractionLockSettings = new UIManager.UserInteractionLockSettings();
      this.m_userInteractionLocked = false;
    }

    private IEnumerator UserInteractionTimeoutCheckRoutine()
    {
      UIManager.UserInteractionLockSettings settings = this.m_userInteractionLockSettings;
      do
      {
        yield return (object) null;
        float interactionLockTimer = this.m_userInteractionLockTimer;
        this.m_userInteractionLockTimer += Time.unscaledDeltaTime;
        Action warningTimeoutCallback = settings.warningTimeoutCallback;
        if (warningTimeoutCallback != null)
        {
          float warningTimeout = settings.warningTimeout;
          if ((double) this.m_userInteractionLockTimer >= (double) warningTimeout && (double) interactionLockTimer < (double) warningTimeout)
            warningTimeoutCallback();
        }
        Action errorTimeoutCallback = settings.errorTimeoutCallback;
        if (errorTimeoutCallback != null)
        {
          float errorTimeout = settings.errorTimeout;
          if ((double) this.m_userInteractionLockTimer >= (double) errorTimeout && (double) interactionLockTimer < (double) errorTimeout)
            errorTimeoutCallback();
        }
      }
      while ((double) this.m_userInteractionLockTimer < (double) settings.maxTimeout);
      this.m_userInteractionLocked = false;
      this.m_userInteractionLockCheckRoutine = (Coroutine) null;
    }

    public class Node
    {
      public List<UIManager.Node> children = new List<UIManager.Node>();
      public StateContext context;
      public UIManager.Node parent;
      public bool renderBeforeParent;
      public int rootIndex = -1;
      public List<AbstractUI> uis = new List<AbstractUI>();
    }

    private class RootNodeComparer : IComparer<UIManager.Node>
    {
      public int Compare(UIManager.Node x, UIManager.Node y) => x.rootIndex - y.rootIndex;
    }

    private struct UserInteractionLockSettings
    {
      public readonly float warningTimeout;
      public readonly float errorTimeout;
      public readonly float maxTimeout;
      public readonly Action warningTimeoutCallback;
      public readonly Action errorTimeoutCallback;

      public UserInteractionLockSettings(
        float warningTimeout,
        float errorTimeout,
        Action warningTimeoutCallback,
        Action errorTimeoutCallback)
      {
        this.warningTimeout = warningTimeout;
        this.errorTimeout = errorTimeout;
        this.maxTimeout = Mathf.Max(warningTimeout, errorTimeout);
        this.warningTimeoutCallback = warningTimeoutCallback;
        this.errorTimeoutCallback = errorTimeoutCallback;
      }

      public bool hasAnyCallback => this.warningTimeoutCallback != null && this.errorTimeoutCallback != null;
    }
  }
}
