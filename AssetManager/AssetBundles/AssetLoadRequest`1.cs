// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetLoadRequest`1
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class AssetLoadRequest<T> : AssetLoadAsyncRequest where T : Object
  {
    [PublicAPI]
    public static readonly AssetLoadRequest<T> empty = new AssetLoadRequest<T>();
    [PublicAPI]
    public readonly string assetName;
    [PublicAPI]
    public readonly string assetBundleFileName;

    [PublicAPI]
    public T asset { get; private set; }

    internal AssetLoadRequest()
      : base(true)
    {
      this.assetName = string.Empty;
      this.assetBundleFileName = string.Empty;
      this.error = (AssetManagerError) 10;
    }

    internal AssetLoadRequest(string assetName, UnityEngine.AssetBundle bundle)
      : base()
    {
      this.assetName = assetName;
      this.assetBundleFileName = bundle.name;
      this.m_assetRequest = bundle.LoadAssetAsync<T>(assetName);
    }

    internal AssetLoadRequest(string assetName, AssetBundleLoadRequest bundleLoadRequest)
      : base()
    {
      this.assetName = assetName;
      this.assetBundleFileName = bundleLoadRequest.assetBundleFileName;
      this.m_bundleLoadRequest = bundleLoadRequest;
    }

    internal AssetLoadRequest(
      string assetName,
      string assetBundleFileName,
      int errorCode,
      string errorMessage)
      : base(true)
    {
      this.assetName = assetName;
      this.assetBundleFileName = assetBundleFileName;
      this.error = new AssetManagerError(errorCode, errorMessage);
    }

    internal override bool Update()
    {
      if (this.m_bundleLoadRequest != null)
      {
        if (!this.m_bundleLoadRequest.isDone)
          return false;
        UnityEngine.AssetBundle assetBundle = this.m_bundleLoadRequest.assetBundle;
        AssetManagerError error = this.m_bundleLoadRequest.error;
        this.m_bundleLoadRequest = (AssetBundleLoadRequest) null;
        if ((int) error != 0)
        {
          this.error = error;
          this.isDone = true;
          return true;
        }
        if ((Object) null == (Object) assetBundle)
        {
          this.error = (AssetManagerError) 30;
          this.isDone = true;
          return true;
        }
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          this.error = new AssetManagerError(10, "Cannot load asset named '" + this.assetName + "' from bundle named '" + this.assetBundleFileName + "' because it is a streamed scene asset bundle.");
          this.isDone = true;
          return true;
        }
        this.m_assetRequest = assetBundle.LoadAssetAsync<T>(this.assetName);
      }
      if (!this.m_assetRequest.isDone)
        return false;
      this.asset = this.m_assetRequest.asset as T;
      this.m_assetRequest = (AssetBundleRequest) null;
      if ((Object) null == (Object) this.asset)
        this.error = (AssetManagerError) 30;
      this.isDone = true;
      return true;
    }
  }
}
