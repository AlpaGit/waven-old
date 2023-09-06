// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleExtendedManifest
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.AssetManagement.AssetBundles
{
  [PublicAPI]
  [Serializable]
  public sealed class AssetBundleExtendedManifest
  {
    [SerializeField]
    [UsedImplicitly]
    internal List<AssetBundleExtendedManifest.AssetBundle> assetBundles;
    [SerializeField]
    [UsedImplicitly]
    internal List<AssetBundleExtendedManifest.StreamingAsset> streamingAssets;

    [Serializable]
    internal struct AssetBundle
    {
      [UsedImplicitly]
      public string name;
      [UsedImplicitly]
      public uint crc;
      [UsedImplicitly]
      public AssetBundleSource source;
    }

    [Serializable]
    internal struct StreamingAsset
    {
      [UsedImplicitly]
      public string path;
      [UsedImplicitly]
      public string guid;
      [UsedImplicitly]
      public string md5;
      [UsedImplicitly]
      public bool patched;
    }
  }
}
