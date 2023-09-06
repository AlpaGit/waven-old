// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.UIResourceLoader`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  public abstract class UIResourceLoader<T> : MonoBehaviour, IUIResourceProvider where T : UnityEngine.Object
  {
    private IUIResourceConsumer m_resourceConsumer;
    private Coroutine m_coroutine;
    private string m_pendingBundleName = string.Empty;
    private string m_loadedBundleName = string.Empty;

    [PublicAPI]
    public UIResourceLoadState loadState { get; protected set; }

    private void OnDestroy()
    {
      if (this.m_coroutine != null)
      {
        MonoBehaviour monoBehaviour = Main.monoBehaviour;
        if ((UnityEngine.Object) null != (UnityEngine.Object) monoBehaviour)
        {
          monoBehaviour.StopCoroutine(this.m_coroutine);
          this.m_coroutine = (Coroutine) null;
        }
      }
      switch (this.loadState)
      {
        case UIResourceLoadState.None:
        case UIResourceLoadState.Error:
          this.loadState = UIResourceLoadState.None;
          break;
        case UIResourceLoadState.Loading:
        case UIResourceLoadState.Loaded:
          if (this.m_pendingBundleName.Length != 0)
          {
            AssetManager.UnloadAssetBundle(this.m_pendingBundleName);
            this.m_pendingBundleName = string.Empty;
          }
          if (this.m_loadedBundleName.Length != 0)
          {
            AssetManager.UnloadAssetBundle(this.m_loadedBundleName);
            this.m_loadedBundleName = string.Empty;
          }
          if (this.m_resourceConsumer != null)
          {
            this.m_resourceConsumer.UnRegister((IUIResourceProvider) this);
            this.m_resourceConsumer = (IUIResourceConsumer) null;
            goto case UIResourceLoadState.None;
          }
          else
            goto case UIResourceLoadState.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    [PublicAPI]
    public void Setup(
      AssetReference assetReference,
      [NotNull] string bundleName,
      [CanBeNull] IUIResourceConsumer resourceConsumer = null)
    {
      MonoBehaviour monoBehaviour = Main.monoBehaviour;
      if ((UnityEngine.Object) null == (UnityEngine.Object) monoBehaviour)
        throw new Exception();
      if (this.m_coroutine != null)
      {
        monoBehaviour.StopCoroutine(this.m_coroutine);
        this.m_coroutine = (Coroutine) null;
      }
      if (this.loadState == UIResourceLoadState.Loading && this.m_pendingBundleName.Length != 0)
      {
        AssetManager.UnloadAssetBundle(this.m_pendingBundleName);
        this.m_pendingBundleName = string.Empty;
      }
      if (this.m_resourceConsumer != null)
        this.m_resourceConsumer.UnRegister((IUIResourceProvider) this);
      UIResourceDisplayMode displayMode = resourceConsumer == null ? UIResourceDisplayMode.None : resourceConsumer.Register((IUIResourceProvider) this);
      this.m_resourceConsumer = resourceConsumer;
      if (!assetReference.hasValue)
      {
        this.m_coroutine = monoBehaviour.StartCoroutineImmediate(this.Clear(displayMode));
      }
      else
      {
        this.m_pendingBundleName = !string.IsNullOrEmpty(bundleName) ? bundleName : throw new ArgumentException("The bundle name must be a non-null non-empty string.", nameof (bundleName));
        this.m_coroutine = monoBehaviour.StartCoroutineImmediate(this.Load(assetReference.value, bundleName, displayMode));
      }
    }

    [PublicAPI]
    public void Clear([CanBeNull] IUIResourceConsumer resourceConsumer = null)
    {
      MonoBehaviour monoBehaviour = Main.monoBehaviour;
      if ((UnityEngine.Object) null == (UnityEngine.Object) monoBehaviour)
        throw new Exception();
      if (this.m_coroutine != null)
      {
        monoBehaviour.StopCoroutine(this.m_coroutine);
        this.m_coroutine = (Coroutine) null;
      }
      if (this.loadState == UIResourceLoadState.Loading && this.m_pendingBundleName.Length != 0)
      {
        AssetManager.UnloadAssetBundle(this.m_pendingBundleName);
        this.m_pendingBundleName = string.Empty;
      }
      if (this.m_resourceConsumer != null)
        this.m_resourceConsumer.UnRegister((IUIResourceProvider) this);
      UIResourceDisplayMode displayMode = resourceConsumer == null ? UIResourceDisplayMode.None : resourceConsumer.Register((IUIResourceProvider) this);
      this.m_resourceConsumer = resourceConsumer;
      this.m_coroutine = monoBehaviour.StartCoroutineImmediate(this.Clear(displayMode));
    }

    private IEnumerator Load(string guid, string bundleName, UIResourceDisplayMode displayMode)
    {
      UIResourceLoader<T> provider = this;
      provider.loadState = UIResourceLoadState.Loading;
      AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
      while (!bundleLoadRequest.isDone)
        yield return (object) null;
      if ((int) bundleLoadRequest.error != 0)
      {
        provider.loadState = UIResourceLoadState.Error;
        provider.m_pendingBundleName = string.Empty;
        if (provider.m_resourceConsumer != null)
        {
          provider.m_resourceConsumer.UnRegister((IUIResourceProvider) provider);
          provider.m_resourceConsumer = (IUIResourceConsumer) null;
        }
        Log.Error(string.Format("Could not load bundle named '{0}': {1}", (object) bundleName, (object) bundleLoadRequest.error), 226, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\UIResourceLoader.cs");
        provider.m_coroutine = (Coroutine) null;
      }
      else
      {
        AssetLoadRequest<T> assetLoadRequest = AssetManager.LoadAssetAsync<T>(guid, bundleName);
        while (!assetLoadRequest.isDone)
          yield return (object) null;
        if ((int) assetLoadRequest.error != 0)
        {
          AssetManager.UnloadAssetBundle(bundleName);
          provider.loadState = UIResourceLoadState.Error;
          provider.m_pendingBundleName = string.Empty;
          if (provider.m_resourceConsumer != null)
          {
            provider.m_resourceConsumer.UnRegister((IUIResourceProvider) provider);
            provider.m_resourceConsumer = (IUIResourceConsumer) null;
          }
          Log.Error(string.Format("Could not load asset with guid {0} from bundle named '{1}': {2}", (object) guid, (object) bundleName, (object) assetLoadRequest.error), 253, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\ResourceLoader\\UIResourceLoader.cs");
          provider.m_coroutine = (Coroutine) null;
        }
        else
        {
          T asset = assetLoadRequest.asset;
          yield return (object) provider.Apply(asset, displayMode);
          if (provider.m_loadedBundleName.Length != 0)
            AssetManager.UnloadAssetBundle(provider.m_loadedBundleName);
          provider.loadState = UIResourceLoadState.Loaded;
          provider.m_pendingBundleName = string.Empty;
          provider.m_loadedBundleName = bundleName;
          if (provider.m_resourceConsumer != null)
          {
            provider.m_resourceConsumer.UnRegister((IUIResourceProvider) provider);
            provider.m_resourceConsumer = (IUIResourceConsumer) null;
          }
          provider.m_coroutine = (Coroutine) null;
        }
      }
    }

    private IEnumerator Clear(UIResourceDisplayMode displayMode)
    {
      yield return (object) this.Revert(displayMode);
      if (this.m_loadedBundleName.Length != 0)
      {
        AssetManager.UnloadAssetBundle(this.m_loadedBundleName);
        this.m_loadedBundleName = string.Empty;
      }
      this.loadState = UIResourceLoadState.None;
    }

    protected abstract IEnumerator Apply([NotNull] T asset, UIResourceDisplayMode displayMode);

    protected abstract IEnumerator Revert(UIResourceDisplayMode displayMode);
  }
}
