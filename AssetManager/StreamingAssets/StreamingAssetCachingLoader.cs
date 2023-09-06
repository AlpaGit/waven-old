// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StreamingAssets.StreamingAssetCachingLoader
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using UnityEngine.Networking;

namespace Ankama.AssetManagement.StreamingAssets
{
  internal sealed class StreamingAssetCachingLoader : StreamingAssetLoader
  {
    private StreamingAssetDownloadHandler m_downloadHandler;
    private UnityWebRequest m_webRequest;

    public StreamingAssetCachingLoader(string assetPath, string filePath, int bufferSize)
    {
      string uri = AssetManager.patchedStreamingAssetsServerURL + assetPath;
      this.error = (AssetManagerError) 0;
      byte[] preallocatedBuffer = new byte[bufferSize];
      StreamingAssetDownloadHandler assetDownloadHandler = new StreamingAssetDownloadHandler(filePath, preallocatedBuffer);
      this.m_downloadHandler = assetDownloadHandler;
      this.m_webRequest = UnityWebRequest.Get(uri);
      this.m_webRequest.downloadHandler = (DownloadHandler) assetDownloadHandler;
      this.m_webRequest.SendWebRequest();
    }

    public override float progress => this.m_webRequest.downloadProgress;

    public override byte[] data
    {
      get
      {
        if (!this.m_webRequest.isDone || (int) this.error != 0)
          return (byte[]) null;
        byte[] data = this.m_webRequest.downloadHandler.data;
        if (data != null)
          return data;
        this.error = (AssetManagerError) 30;
        return (byte[]) null;
      }
    }

    public override bool Update()
    {
      if (!this.m_webRequest.isDone)
        return false;
      if (this.m_webRequest.isHttpError || this.m_webRequest.isNetworkError)
        this.error = AssetManagerError.WebRequestError(this.m_webRequest);
      return true;
    }

    public override void Cancel()
    {
      if (this.m_webRequest.isDone)
        return;
      this.m_downloadHandler.Abort();
      this.error = (AssetManagerError) 50;
      this.m_webRequest.Abort();
    }

    public override void Dispose()
    {
      this.m_webRequest.Dispose();
      this.m_downloadHandler = (StreamingAssetDownloadHandler) null;
      this.m_webRequest = (UnityWebRequest) null;
    }
  }
}
