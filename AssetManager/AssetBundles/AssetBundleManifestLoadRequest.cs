// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleManifestLoadRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class AssetBundleManifestLoadRequest : AssetBundleAsyncRequest
  {
    private IAssetBundleLoader m_bundleLoader;
    private AssetLoadRequest<AssetBundleManifest> m_manifestRequest;
    private UnityWebRequest m_extendedManifestRequest;
    private UnityEngine.AssetBundle m_assetBundle;

    [PublicAPI]
    public AssetBundleSource assetBundleManifestSource { get; }

    [PublicAPI]
    public AssetBundleManifest assetBundleManifest { get; private set; }

    [PublicAPI]
    public AssetBundleExtendedManifest assetBundleExtendedManifest { get; private set; }

    [PublicAPI]
    public override float progress
    {
      get
      {
        if (this.m_bundleLoader != null)
          return this.m_extendedManifestRequest != null ? Mathf.Max(this.m_bundleLoader.progress, this.m_extendedManifestRequest.downloadProgress) : this.m_bundleLoader.progress;
        if (this.m_extendedManifestRequest != null)
          return this.m_extendedManifestRequest.downloadProgress;
        return !CacheManager.isInitialized ? 0.0f : 1f;
      }
    }

    internal AssetBundleManifestLoadRequest(AssetBundleSource source)
      : base()
    {
      this.assetBundleManifestSource = source;
      if (!CacheManager.isInitialized)
        return;
      this.Start();
    }

    internal AssetBundleManifestLoadRequest()
      : base(true)
    {
    }

    internal AssetBundleManifestLoadRequest(string errorMessage)
      : base(true)
    {
      this.error = new AssetManagerError(20, errorMessage);
    }

    private void Start()
    {
      string bundleName = Application.platform.ToSupportedPlatform().ToString();
      switch (this.assetBundleManifestSource)
      {
        case AssetBundleSource.Web:
          this.m_bundleLoader = (IAssetBundleLoader) new AssetBundleWebLoader(bundleName);
          this.m_extendedManifestRequest = UnityWebRequest.Get(AssetManager.assetBundleServerURL + bundleName + ".xmanifest");
          this.m_extendedManifestRequest.SendWebRequest();
          break;
        case AssetBundleSource.StreamingAssets:
          this.m_bundleLoader = (IAssetBundleLoader) new AssetBundleStreamingAssetsLoader(bundleName);
          break;
        default:
          throw new ArgumentException();
      }
    }

    internal bool Update()
    {
      if (!CacheManager.isInitialized)
      {
        if (!Caching.ready)
          return false;
        CacheManager.Initialize();
        this.Start();
      }
      if (this.m_extendedManifestRequest != null && this.m_extendedManifestRequest.isDone)
      {
        if (this.m_extendedManifestRequest.isHttpError || this.m_extendedManifestRequest.isNetworkError)
        {
          this.error = AssetManagerError.WebRequestError(this.m_extendedManifestRequest);
          this.Abort();
          return true;
        }
        string text = this.m_extendedManifestRequest.downloadHandler.text;
        this.m_extendedManifestRequest.Dispose();
        this.m_extendedManifestRequest = (UnityWebRequest) null;
        this.assetBundleExtendedManifest = JsonUtility.FromJson<AssetBundleExtendedManifest>(text);
        if (this.assetBundleExtendedManifest == null)
        {
          this.error = (AssetManagerError) 30;
          this.Abort();
          return true;
        }
      }
      if (this.m_bundleLoader != null)
      {
        if (this.m_bundleLoader.isDone)
        {
          UnityEngine.AssetBundle assetBundle = this.m_bundleLoader.assetBundle;
          this.error = this.m_bundleLoader.error;
          this.m_bundleLoader.Dispose();
          this.m_bundleLoader = (IAssetBundleLoader) null;
          if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
          {
            this.Abort();
            return true;
          }
          this.m_assetBundle = assetBundle;
          this.m_manifestRequest = new AssetLoadRequest<AssetBundleManifest>("AssetBundleManifest", assetBundle);
        }
      }
      else if (this.m_manifestRequest != null)
      {
        if (this.m_manifestRequest.isDone)
        {
          this.assetBundleManifest = this.m_manifestRequest.asset;
          this.error = this.m_manifestRequest.error;
          this.m_manifestRequest = (AssetLoadRequest<AssetBundleManifest>) null;
          if ((UnityEngine.Object) null == (UnityEngine.Object) this.assetBundleManifest)
          {
            this.Abort();
            return true;
          }
        }
        else
          this.m_manifestRequest.Update();
      }
      else if (this.m_extendedManifestRequest == null)
      {
        this.isDone = true;
        return true;
      }
      return false;
    }

    internal void Abort()
    {
      this.isDone = true;
      if (this.m_extendedManifestRequest != null)
      {
        this.m_extendedManifestRequest.Abort();
        this.m_extendedManifestRequest.Dispose();
        this.m_extendedManifestRequest = (UnityWebRequest) null;
        this.m_extendedManifestRequest = (UnityWebRequest) null;
      }
      if (this.m_bundleLoader != null)
      {
        this.m_bundleLoader.Cancel();
        this.m_bundleLoader.Dispose();
        this.m_bundleLoader = (IAssetBundleLoader) null;
      }
      this.m_manifestRequest = (AssetLoadRequest<AssetBundleManifest>) null;
      this.Unload();
    }

    [PublicAPI]
    public void Unload()
    {
      this.assetBundleManifest = (AssetBundleManifest) null;
      this.assetBundleExtendedManifest = (AssetBundleExtendedManifest) null;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_assetBundle))
        return;
      this.m_assetBundle.Unload(true);
      this.m_assetBundle = (UnityEngine.AssetBundle) null;
    }
  }
}
