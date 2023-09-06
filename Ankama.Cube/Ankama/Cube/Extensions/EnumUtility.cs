// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.EnumUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ankama.Cube.Extensions
{
  public static class EnumUtility
  {
    [PublicAPI]
    public static T[] GetValues<T>()
    {
      Array values1 = Enum.GetValues(typeof (T));
      int length = values1.Length;
      T[] values2 = new T[length];
      for (int index = 0; index < length; ++index)
        values2[index] = (T) values1.GetValue(index);
      return values2;
    }

    [PublicAPI]
    public static IEnumerable<T> GetValuesNotObsolete<T>()
    {
      Array values = Enum.GetValues(typeof (T));
      int count = values.Length;
      System.Type enumType = typeof (T);
      for (int i = 0; i < count; ++i)
      {
        T obj = (T) values.GetValue(i);
        if (obj is Enum @enum)
        {
          MemberInfo[] member = enumType.GetMember(@enum.ToString());
          if (member.Length != 0 && member[0].GetCustomAttributes(typeof (ObsoleteAttribute), false).Length == 0)
            yield return obj;
        }
      }
    }
  }
}
