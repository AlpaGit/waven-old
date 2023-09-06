// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleStreamingAssetsLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  internal sealed class AssetBundleStreamingAssetsLoader : IAssetBundleLoader, IDisposable
  {
    private AssetBundleCreateRequest m_bundleRequest;

    public AssetBundleStreamingAssetsLoader(string bundleName)
    {
      this.error = (AssetManagerError) 0;
      this.m_bundleRequest = UnityEngine.AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/AssetBundles/" + bundleName);
    }

    public AssetManagerError error { get; private set; }

    public bool isDone => this.m_bundleRequest.isDone;

    public float progress => this.m_bundleRequest != null ? this.m_bundleRequest.progress : 1f;

    public UnityEngine.AssetBundle assetBundle
    {
      get
      {
        if ((int) this.error != 0)
          return (UnityEngine.AssetBundle) null;
        if (!this.m_bundleRequest.isDone)
          return (UnityEngine.AssetBundle) null;
        UnityEngine.AssetBundle assetBundle = this.m_bundleRequest.assetBundle;
        if (!((UnityEngine.Object) null == (UnityEngine.Object) assetBundle))
          return assetBundle;
        this.error = (AssetManagerError) 30;
        return (UnityEngine.AssetBundle) null;
      }
    }

    public void Cancel()
    {
    }

    public void Dispose() => this.m_bundleRequest = (AssetBundleCreateRequest) null;
  }
}
