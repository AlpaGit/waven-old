// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleLoadRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class AssetBundleLoadRequest : AssetBundleAsyncRequest
  {
    [PublicAPI]
    public readonly string assetBundleFileName;
    private readonly AssetBundleFileInfo m_fileInfo;
    private readonly AssetBundleLoadInfo m_loadInfo;
    private IAssetBundleLoader m_loader;

    [PublicAPI]
    public UnityEngine.AssetBundle assetBundle { get; private set; }

    [PublicAPI]
    public override float progress => this.m_loader == null ? 1f : this.m_loader.progress;

    internal AssetBundleLoadRequest(AssetBundleFileInfo fileInfo, AssetBundleLoadInfo loadInfo)
      : base()
    {
      this.assetBundleFileName = fileInfo.fileName;
      this.m_fileInfo = fileInfo;
      this.m_loadInfo = loadInfo;
      this.assetBundle = loadInfo.assetBundle;
    }

    internal AssetBundleLoadRequest(string bundleName, int errorCode, string errorMessage)
      : base(true)
    {
      this.assetBundleFileName = bundleName;
      this.error = new AssetManagerError(errorCode, errorMessage);
    }

    internal bool Update()
    {
      if (this.isDone)
        return true;
      AssetBundleLoadInfo loadInfo = this.m_loadInfo;
      IAssetBundleLoader assetBundleLoader = this.m_loader;
      UnityEngine.AssetBundle assetBundle1 = loadInfo.assetBundle;
      int num = loadInfo.referenceCount > 0 ? 1 : 0;
      bool flag1 = num != 0 || loadInfo.dependencyReferenceCount > 0;
      bool flag2 = (UnityEngine.Object) null != (UnityEngine.Object) assetBundle1;
      bool flag3 = true;
      if (num != 0)
      {
        AssetBundleLoadInfo[] dependencies = this.m_fileInfo.dependencies;
        int length = dependencies.Length;
        for (int index = 0; index < length; ++index)
        {
          AssetBundleLoadInfo assetBundleLoadInfo = dependencies[index];
          if ((int) assetBundleLoadInfo.error != 0)
          {
            this.AbortOnError(assetBundleLoadInfo.error);
            return true;
          }
          if (!assetBundleLoadInfo.isAssetBundleLoaded)
            flag3 = false;
        }
      }
      if (flag1)
      {
        if (!flag2)
        {
          if (assetBundleLoader == null)
          {
            AssetBundleFileInfo fileInfo = this.m_fileInfo;
            switch (fileInfo.source)
            {
              case AssetBundleSource.Web:
                assetBundleLoader = (IAssetBundleLoader) new AssetBundleWebLoader(fileInfo.fileName, fileInfo.hash, fileInfo.crc);
                break;
              case AssetBundleSource.StreamingAssets:
                assetBundleLoader = (IAssetBundleLoader) new AssetBundleStreamingAssetsLoader(fileInfo.fileName);
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            this.m_loader = assetBundleLoader;
          }
          if (!assetBundleLoader.isDone)
            return false;
          UnityEngine.AssetBundle assetBundle2 = assetBundleLoader.assetBundle;
          if ((int) assetBundleLoader.error != 0)
          {
            this.AbortOnError(assetBundleLoader.error);
            return true;
          }
          assetBundleLoader.Dispose();
          this.m_loader = (IAssetBundleLoader) null;
          this.assetBundle = assetBundle2;
          loadInfo.assetBundle = assetBundle2;
        }
        if (!flag3)
          return false;
        this.isDone = true;
        return true;
      }
      if (assetBundleLoader != null)
      {
        assetBundleLoader.Cancel();
        if (!assetBundleLoader.isDone)
          return false;
        UnityEngine.AssetBundle assetBundle3 = assetBundleLoader.assetBundle;
        assetBundleLoader.Dispose();
        this.m_loader = (IAssetBundleLoader) null;
        if ((UnityEngine.Object) null != (UnityEngine.Object) assetBundle3)
          assetBundle3.Unload(true);
        this.assetBundle = (UnityEngine.AssetBundle) null;
      }
      this.error = (AssetManagerError) 50;
      this.isDone = true;
      return true;
    }

    internal bool Cancel()
    {
      if (this.isDone)
      {
        if ((int) this.error == 0)
          this.error = (AssetManagerError) 50;
        return true;
      }
      if (this.m_loader != null)
        return false;
      this.error = (AssetManagerError) 50;
      this.isDone = true;
      return true;
    }

    internal void Restart()
    {
      this.error = (AssetManagerError) 0;
      this.isDone = false;
    }

    private void AbortOnError(AssetManagerError e)
    {
      if (this.m_loader != null)
      {
        this.m_loader.Cancel();
        this.m_loader.Dispose();
        this.m_loader = (IAssetBundleLoader) null;
      }
      if (this.m_loadInfo.referenceCount > 0)
        AssetManager.RevertAssetBundleRequest(this.m_fileInfo, this.m_loadInfo);
      else
        AssetManager.RevertDependencyAssetBundleRequest(this.m_loadInfo);
      this.error = e;
      this.isDone = true;
    }
  }
}
