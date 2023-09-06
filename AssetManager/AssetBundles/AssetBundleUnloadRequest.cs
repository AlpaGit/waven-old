// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleUnloadRequest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  public sealed class AssetBundleUnloadRequest : AssetBundleAsyncRequest
  {
    [PublicAPI]
    public readonly string assetBundleFileName;
    internal bool checkDependencies;
    private readonly AssetBundleFileInfo m_fileInfo;
    private readonly AssetBundleLoadInfo m_loadInfo;

    [PublicAPI]
    public override float progress => !this.isDone ? 0.0f : 1f;

    internal AssetBundleUnloadRequest(
      AssetBundleFileInfo fileInfo,
      AssetBundleLoadInfo loadInfo,
      bool checkDependencies)
      : base()
    {
      this.assetBundleFileName = fileInfo.fileName;
      this.checkDependencies = checkDependencies;
      this.m_fileInfo = fileInfo;
      this.m_loadInfo = loadInfo;
    }

    internal AssetBundleUnloadRequest(string bundleFileName, int errorCode, string errorMessage)
      : base(true)
    {
      this.assetBundleFileName = bundleFileName;
      this.error = new AssetManagerError(errorCode, errorMessage);
    }

    internal bool Update()
    {
      if (this.isDone)
        return true;
      AssetBundleLoadInfo loadInfo = this.m_loadInfo;
      UnityEngine.AssetBundle assetBundle = loadInfo.assetBundle;
      bool flag1 = loadInfo.referenceCount > 0;
      bool flag2 = flag1 || loadInfo.dependencyReferenceCount > 0;
      bool flag3 = (Object) null != (Object) assetBundle;
      bool flag4 = true;
      if (this.checkDependencies && !flag1)
      {
        AssetBundleLoadInfo[] dependencies = this.m_fileInfo.dependencies;
        int length = dependencies.Length;
        for (int index = 0; index < length; ++index)
        {
          AssetBundleLoadInfo assetBundleLoadInfo = dependencies[index];
          if (assetBundleLoadInfo.referenceCount == 0 && assetBundleLoadInfo.dependencyReferenceCount == 0 && (Object) null != (Object) assetBundleLoadInfo.assetBundle)
            flag4 = false;
        }
      }
      if (!flag2)
      {
        if (flag3)
        {
          if (AssetManager.HasActiveAssetLoadingRequest(loadInfo.name))
            return false;
          assetBundle.Unload(true);
          loadInfo.assetBundle = (UnityEngine.AssetBundle) null;
        }
        if (!flag4)
          return false;
        this.isDone = true;
        return true;
      }
      this.isDone = true;
      return true;
    }

    internal void Restart() => this.isDone = false;
  }
}
