// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MetaDataExtractorUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ankama.Cube.Data
{
  public static class MetaDataExtractorUtility
  {
    public static IEnumerable<T> GetCustomAttributes<T>(this object obj) where T : class => obj is Enum e ? e.GetAttributes<T>() : obj.GetType().GetCustomAttributesIncludingBaseInterfaces<T>();

    public static IEnumerable<T> GetAttributes<T>(this Enum e) where T : class => e.GetType().GetMember(e.ToString())[0].GetAttributes<T>();

    public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : class => Attribute.GetCustomAttributes(member, typeof (T)).Cast<T>();

    private static IEnumerable<T> GetCustomAttributesIncludingBaseInterfaces<T>(this System.Type type)
    {
      System.Type attributeType = typeof (T);
      return ((IEnumerable<object>) type.GetCustomAttributes(attributeType, true)).Union<object>(((IEnumerable<System.Type>) type.GetInterfaces()).SelectMany<System.Type, object>((Func<System.Type, IEnumerable<object>>) (interfaceType => (IEnumerable<object>) interfaceType.GetCustomAttributes(attributeType, true)))).Distinct<object>().Cast<T>();
    }
  }
}
