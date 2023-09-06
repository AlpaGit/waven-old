// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleVariantCollectionCreator
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Ankama.AssetManagement.AssetBundles
{
  internal class AssetBundleVariantCollectionCreator : List<HashSet<string>>
  {
    private readonly StringBuilder m_logger = new StringBuilder();

    public void AddSingleVariant([NotNull] string variant)
    {
      foreach (HashSet<string> stringSet in (List<HashSet<string>>) this)
      {
        if (stringSet.Contains(variant))
          return;
      }
      this.Add(new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal)
      {
        variant
      });
    }

    public void AddVariant([NotNull] string variant, List<AssetBundleFileInfo> files)
    {
      int count = files.Count;
      for (int index = 0; index < count; ++index)
      {
        string variant1 = files[index].variant;
        foreach (HashSet<string> stringSet in (List<HashSet<string>>) this)
        {
          if (stringSet.Contains(variant1))
          {
            stringSet.Add(variant);
            return;
          }
        }
      }
      foreach (HashSet<string> stringSet in (List<HashSet<string>>) this)
      {
        if (stringSet.Contains(variant))
          return;
      }
      this.Add(new HashSet<string>((IEqualityComparer<string>) StringComparer.Ordinal)
      {
        variant
      });
    }

    [Conditional("UNITY_EDITOR")]
    public void Check()
    {
      int count = this.Count;
      for (int index1 = 0; index1 < count - 1; ++index1)
      {
        HashSet<string> variantSet1 = this[index1];
        foreach (string str in variantSet1)
        {
          for (int index2 = index1 + 1; index2 < count; ++index2)
          {
            HashSet<string> variantSet2 = this[index2];
            if (variantSet2.Contains(str))
              UnityEngine.Debug.LogWarning((object) ("[AssetManager] Variant '" + str + "' in in multiple variant collections: " + this.VariantSetToString(variantSet1) + " and " + this.VariantSetToString(variantSet2) + "."));
          }
        }
      }
    }

    public AssetBundleVariantCollection Build()
    {
      int count = this.Count;
      string[][] variantSets = new string[count][];
      for (int index1 = 0; index1 < count; ++index1)
      {
        HashSet<string> stringSet = this[index1];
        string[] strArray = new string[stringSet.Count];
        int index2 = 0;
        foreach (string str in stringSet)
        {
          strArray[index2] = str;
          ++index2;
        }
        variantSets[index1] = strArray;
      }
      return new AssetBundleVariantCollection(variantSets);
    }

    private string VariantSetToString(HashSet<string> variantSet)
    {
      this.m_logger.Append('{');
      if (variantSet.Count > 0)
      {
        foreach (string variant in variantSet)
        {
          this.m_logger.Append(variant);
          this.m_logger.Append(", ");
        }
        this.m_logger.Remove(this.m_logger.Length - 2, 2);
      }
      this.m_logger.Append('}');
      string str = this.m_logger.ToString();
      this.m_logger.Clear();
      return str;
    }
  }
}
