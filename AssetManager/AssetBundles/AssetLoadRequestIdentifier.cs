// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetLoadRequestIdentifier
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System;
using System.Collections.Generic;

namespace Ankama.AssetManagement.AssetBundles
{
  internal struct AssetLoadRequestIdentifier
  {
    public readonly Type assetType;
    public readonly string assetName;
    public readonly string bundleName;
    private readonly int m_hash;
    public static readonly AssetLoadRequestIdentifier.EqualityComparer equalityComparer = new AssetLoadRequestIdentifier.EqualityComparer();

    public AssetLoadRequestIdentifier(string bundleName, Type assetType, string assetName)
    {
      this.assetType = assetType;
      this.assetName = assetName;
      this.bundleName = bundleName;
      this.m_hash = ((-2128831035 * 16777619 ^ bundleName.GetHashCode()) * 16777619 ^ assetType.GetHashCode()) * 16777619 ^ assetName.GetHashCode();
    }

    public AssetLoadRequestIdentifier(string bundleName, Type assetType)
    {
      this.bundleName = bundleName;
      this.assetType = assetType;
      this.assetName = string.Empty;
      this.m_hash = (-2128831035 * 16777619 ^ bundleName.GetHashCode()) * 16777619 ^ assetType.GetHashCode();
    }

    public class EqualityComparer : IEqualityComparer<AssetLoadRequestIdentifier>
    {
      public bool Equals(AssetLoadRequestIdentifier x, AssetLoadRequestIdentifier y) => x.assetType == y.assetType && x.assetName.Equals(y.assetName) && x.bundleName.Equals(y.bundleName);

      public int GetHashCode(AssetLoadRequestIdentifier obj) => obj.m_hash;
    }
  }
}
