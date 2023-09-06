// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.SpriteAtlasRegisteringRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class SpriteAtlasRegisteringRequest : AssetLoadAsyncRequest
  {
    [PublicAPI]
    public readonly string assetName;
    [PublicAPI]
    public readonly string assetBundleFileName;

    [PublicAPI]
    public SpriteAtlas spriteAtlas { get; private set; }

    internal SpriteAtlasRegisteringRequest(string assetName, UnityEngine.AssetBundle bundle)
      : base()
    {
      this.assetName = assetName;
      this.assetBundleFileName = bundle.name;
      this.m_assetRequest = bundle.LoadAssetAsync<SpriteAtlas>(assetName);
      this.m_assetRequest.completed += new Action<AsyncOperation>(this.OnAssetRequestCompleted);
    }

    internal SpriteAtlasRegisteringRequest(
      string assetName,
      AssetBundleLoadRequest bundleLoadRequest)
      : base()
    {
      this.assetName = assetName;
      this.assetBundleFileName = bundleLoadRequest.assetBundleFileName;
      this.m_bundleLoadRequest = bundleLoadRequest;
    }

    internal SpriteAtlasRegisteringRequest(
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
        if ((UnityEngine.Object) null == (UnityEngine.Object) assetBundle)
        {
          this.error = (AssetManagerError) 30;
          this.isDone = true;
          return true;
        }
        if (assetBundle.isStreamedSceneAssetBundle)
        {
          this.error = new AssetManagerError(10, "Cannot register sprite atlas named '" + this.assetName + "' from bundle named '" + this.assetBundleFileName + "' because it is a streamed scene asset bundle.");
          this.isDone = true;
          return true;
        }
        this.m_assetRequest = assetBundle.LoadAssetAsync<SpriteAtlas>(this.assetName);
        this.m_assetRequest.completed += new Action<AsyncOperation>(this.OnAssetRequestCompleted);
      }
      if (!this.m_assetRequest.isDone)
        return false;
      this.m_assetRequest = (AssetBundleRequest) null;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.spriteAtlas)
        this.error = (AssetManagerError) 30;
      this.isDone = true;
      return true;
    }

    private void OnAssetRequestCompleted(AsyncOperation obj)
    {
      SpriteAtlas asset = this.m_assetRequest.asset as SpriteAtlas;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) asset))
        return;
      AssetManager.registeredSpriteAtlases[asset.tag] = asset;
      this.spriteAtlas = asset;
    }
  }
}
