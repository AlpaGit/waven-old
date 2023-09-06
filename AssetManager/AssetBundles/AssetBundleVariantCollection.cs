// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetBundles.AssetBundleVariantCollection
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using System.Collections.Generic;

namespace Ankama.AssetManagement.AssetBundles
{
  internal class AssetBundleVariantCollection
  {
    private readonly string[][] m_variantSets;

    public AssetBundleVariantCollection(string[][] variantSets) => this.m_variantSets = variantSets;

    public bool VariantExists(string variant)
    {
      int length1 = this.m_variantSets.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        string[] variantSet = this.m_variantSets[index1];
        int length2 = variantSet.Length;
        for (int index2 = 0; index2 < length2; ++index2)
        {
          if (variant.Equals(variantSet[index2]))
            return true;
        }
      }
      return false;
    }

    public bool VariantConflicts(string variantA, HashSet<string> activeVariants)
    {
      int length1 = this.m_variantSets.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        string[] variantSet = this.m_variantSets[index1];
        int length2 = variantSet.Length;
        for (int index2 = 0; index2 < length2; ++index2)
        {
          string str1 = variantSet[index2];
          if (variantA.Equals(str1))
          {
            for (int index3 = 0; index3 < length2; ++index3)
            {
              string str2 = variantSet[index3];
              if (activeVariants.Contains(str2))
                return true;
            }
            break;
          }
        }
      }
      return false;
    }
  }
}
