// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.SpriteAtlasesRegisteringRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public class SpriteAtlasesRegisteringRequest : AssetLoadAsyncRequest
  {
    [PublicAPI]
    public readonly string assetBundleFileName;

    [PublicAPI]
    public SpriteAtlas[] spriteAtlases { get; private set; }

    internal SpriteAtlasesRegisteringRequest(UnityEngine.AssetBundle bundle)
      : base()
    {
      this.assetBundleFileName = bundle.name;
      this.m_assetRequest = bundle.LoadAllAssetsAsync<SpriteAtlas>();
      this.m_assetRequest.completed += new Action<AsyncOperation>(this.OnAssetRequestCompleted);
    }

    internal SpriteAtlasesRegisteringRequest(AssetBundleLoadRequest bundleLoadRequest)
      : base()
    {
      this.assetBundleFileName = bundleLoadRequest.assetBundleFileName;
      this.m_bundleLoadRequest = bundleLoadRequest;
    }

    internal SpriteAtlasesRegisteringRequest(
      string assetBundleFileName,
      int errorCode,
      string errorMessage)
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
          this.error = new AssetManagerError(10, "Cannot register sprite atlases from bundle named '" + this.assetBundleFileName + "' because it is a streamed scene asset bundle.");
          this.isDone = true;
          return true;
        }
        this.m_assetRequest = assetBundle.LoadAllAssetsAsync<SpriteAtlas>();
        this.m_assetRequest.completed += new Action<AsyncOperation>(this.OnAssetRequestCompleted);
      }
      if (!this.m_assetRequest.isDone)
        return false;
      this.m_assetRequest = (AssetBundleRequest) null;
      this.isDone = true;
      return true;
    }

    private void OnAssetRequestCompleted(AsyncOperation obj)
    {
      SpriteAtlas[] spriteAtlasArray = Array.ConvertAll<UnityEngine.Object, SpriteAtlas>(this.m_assetRequest.allAssets, new System.Converter<UnityEngine.Object, SpriteAtlas>(SpriteAtlasesRegisteringRequest.Converter));
      Dictionary<string, SpriteAtlas> registeredSpriteAtlases = AssetManager.registeredSpriteAtlases;
      int length = spriteAtlasArray.Length;
      for (int index = 0; index < length; ++index)
      {
        SpriteAtlas spriteAtlas = spriteAtlasArray[index];
        registeredSpriteAtlases[spriteAtlas.tag] = spriteAtlas;
      }
      this.spriteAtlases = spriteAtlasArray;
    }

    private static SpriteAtlas Converter(UnityEngine.Object o) => (SpriteAtlas) o;
  }
}
