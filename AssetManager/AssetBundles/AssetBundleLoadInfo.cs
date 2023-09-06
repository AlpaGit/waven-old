// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleLoadInfo
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  internal class AssetBundleLoadInfo : IEquatable<AssetBundleLoadInfo>
  {
    public readonly string name;
    public readonly bool hasVariants;
    public readonly AssetBundleFileInfo[] files;
    public int referenceCount;
    public int dependencyReferenceCount;
    public UnityEngine.AssetBundle assetBundle;
    public AssetManagerError error = (AssetManagerError) 0;
    public AssetBundleLoadRequest loadRequest;
    public AssetBundleUnloadRequest unloadRequest;

    public AssetBundleLoadInfo([NotNull] string name, bool hasVariants, AssetBundleFileInfo[] files)
    {
      this.name = name;
      this.hasVariants = hasVariants;
      this.files = files;
    }

    public bool isAssetBundleLoaded => (UnityEngine.Object) null != (UnityEngine.Object) this.assetBundle;

    public bool isAssetBundleLoading => this.loadRequest != null && !this.loadRequest.isDone;

    public bool AllowCachedFileWithHash(Hash128 hash)
    {
      int length = this.files.Length;
      for (int index = 0; index < length; ++index)
      {
        AssetBundleFileInfo file = this.files[index];
        if (hash.Equals(file.hash))
          return file.source != AssetBundleSource.StreamingAssets;
      }
      return false;
    }

    public bool Equals(AssetBundleLoadInfo other)
    {
      if (other == null)
        return false;
      return this == other || string.Equals(this.name, other.name);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return obj is AssetBundleLoadInfo assetBundleLoadInfo && string.Equals(this.name, assetBundleLoadInfo.name);
    }

    public override int GetHashCode() => this.name.GetHashCode();
  }
}
