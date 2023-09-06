// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FamilyBundleNameExtension
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ankama.Cube.Data
{
  public static class FamilyBundleNameExtension
  {
    private static readonly Dictionary<BundleCategory, string> s_bundleSubCategoryToBundleName = new Dictionary<BundleCategory, string>();
    private static readonly Dictionary<God, string> s_godToBundleName = new Dictionary<God, string>();

    static FamilyBundleNameExtension()
    {
      FamilyBundleNameExtension.CacheBundleNames<BundleCategory>(FamilyBundleNameExtension.s_bundleSubCategoryToBundleName);
      FamilyBundleNameExtension.CacheBundleNames<God>(FamilyBundleNameExtension.s_godToBundleName);
    }

    private static void CacheBundleNames<T>(Dictionary<T, string> data)
    {
      System.Type type = typeof (T);
      Array values = Enum.GetValues(typeof (T));
      int length = values.Length;
      for (int index = 0; index < length; ++index)
      {
        T key = (T) values.GetValue(index);
        MemberInfo[] member = type.GetMember(key.ToString());
        BundleNameAttribute customAttribute = member.Length == 0 ? (BundleNameAttribute) null : (BundleNameAttribute) Attribute.GetCustomAttribute(member[0], typeof (BundleNameAttribute));
        string str = customAttribute != null ? customAttribute.bundleName : string.Empty;
        data.Add(key, str);
      }
    }

    [NotNull]
    public static string GetBundleName(this BundleCategory value) => FamilyBundleNameExtension.s_bundleSubCategoryToBundleName[value];

    [NotNull]
    public static string GetBundleName(this God value) => FamilyBundleNameExtension.s_godToBundleName[value];
  }
}
