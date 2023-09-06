// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleFileInfo
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  internal class AssetBundleFileInfo
  {
    public readonly string fileName;
    public readonly string variant;
    public readonly Hash128 hash;
    public AssetBundleSource source;
    public uint crc;
    public AssetBundleLoadInfo[] dependencies;

    public AssetBundleFileInfo(string fileName, string variant, Hash128 hash)
    {
      this.fileName = fileName;
      this.variant = variant;
      this.hash = hash;
      this.source = AssetBundleSource.StreamingAssets;
    }
  }
}
