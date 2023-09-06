// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AllAssetsLoadRequest`1
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class AllAssetsLoadRequest<T> : AssetLoadAsyncRequest where T : UnityEngine.Object
  {
    [PublicAPI]
    public readonly string assetBundleFileName;

    [PublicAPI]
    public T[] assets { get; private set; }

    internal AllAssetsLoadRequest(UnityEngine.AssetBundle bundle)
      : base()
    {
      this.assetBundleFileName = bundle.name;
      this.m_assetRequest = bundle.LoadAllAssetsAsync<T>();
    }

    internal AllAssetsLoadRequest(AssetBundleLoadRequest bundleLoadRequest)
      : base()
    {
      this.assetBundleFileName = bundleLoadRequest.assetBundleFileName;
      this.m_bundleLoadRequest = bundleLoadRequest;
    }

    internal AllAssetsLoadRequest(string assetBundleFileName, int errorCode, string errorMessage)
      : base(true)
    {
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
        if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
        {
          this.error = (AssetManagerError) 30;
          this.isDone = true;
          return true;
        }
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          this.error = new AssetManagerError(10, "Cannot load all assets from bundle named '" + this.assetBundleFileName + "' because it is a streamed scene asset bundle.");
          this.isDone = true;
          return true;
        }
        this.m_assetRequest = assetBundle.LoadAllAssetsAsync<T>();
      }
      if (!this.m_assetRequest.isDone)
        return false;
      this.assets = !(typeof (T) == typeof (UnityEngine.Object)) ? Array.ConvertAll<UnityEngine.Object, T>(this.m_assetRequest.allAssets, new System.Converter<UnityEngine.Object, T>(AllAssetsLoadRequest<T>.Converter)) : (T[]) this.m_assetRequest.allAssets;
      this.m_assetRequest = (AssetBundleRequest) null;
      this.isDone = true;
      return true;
    }

    private static T Converter(UnityEngine.Object o) => (T) o;
  }
}
