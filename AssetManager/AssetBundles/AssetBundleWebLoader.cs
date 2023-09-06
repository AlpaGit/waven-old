// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleWebLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Ankama.AssetManagement.AssetBundles
{
  internal sealed class AssetBundleWebLoader : IAssetBundleLoader, IDisposable
  {
    private const int Timeout = 30;
    private UnityWebRequest m_webRequest;
    private bool m_aborted;

    public AssetBundleWebLoader(string bundleName, Hash128 hash, uint crc)
    {
      this.error = (AssetManagerError) 0;
      this.m_webRequest = UnityWebRequestAssetBundle.GetAssetBundle(AssetManager.assetBundleServerURL + bundleName, new CachedAssetBundle(bundleName, hash), crc);
      this.m_webRequest.timeout = 30;
      this.m_webRequest.SendWebRequest();
    }

    public AssetBundleWebLoader(string bundleName)
    {
      this.error = (AssetManagerError) 0;
      string str = AssetManager.assetBundleServerURL + bundleName;
      DownloadHandlerAssetBundle handlerAssetBundle = new DownloadHandlerAssetBundle(str, 0U);
      this.m_webRequest = UnityWebRequest.Get(str);
      this.m_webRequest.downloadHandler = (DownloadHandler) handlerAssetBundle;
      this.m_webRequest.SendWebRequest();
    }

    public AssetManagerError error { get; private set; }

    public bool isDone => this.m_webRequest.isDone;

    public float progress => this.m_webRequest.downloadProgress;

    public UnityEngine.AssetBundle assetBundle
    {
      get
      {
        if ((int) this.error != 0)
          return (UnityEngine.AssetBundle) null;
        if (!this.m_webRequest.isDone)
          return (UnityEngine.AssetBundle) null;
        if (this.m_webRequest.isHttpError || this.m_webRequest.isNetworkError)
        {
          this.error = AssetManagerError.WebRequestError(this.m_webRequest);
          return (UnityEngine.AssetBundle) null;
        }
        UnityEngine.AssetBundle content = DownloadHandlerAssetBundle.GetContent(this.m_webRequest);
        if (!((UnityEngine.Object) null == (UnityEngine.Object) content))
          return content;
        this.error = (AssetManagerError) 30;
        return (UnityEngine.AssetBundle) null;
      }
    }

    public void Cancel()
    {
      if (this.m_webRequest.isDone || this.m_aborted)
        return;
      this.error = (AssetManagerError) 50;
      this.m_webRequest.Abort();
      this.m_aborted = true;
    }

    public void Dispose()
    {
      this.m_webRequest.Dispose();
      this.m_webRequest = (UnityWebRequest) null;
    }
  }
}
