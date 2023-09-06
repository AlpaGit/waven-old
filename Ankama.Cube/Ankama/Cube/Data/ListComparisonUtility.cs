// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ListComparisonUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ankama.Cube.Data
{
  public static class ListComparisonUtility
  {
    public static bool ValidateCondition<T>(
      IReadOnlyCollection<T> currents,
      ListComparison comparison,
      IReadOnlyList<T> expected)
    {
      int count = ((IReadOnlyCollection<T>) expected).Count;
      switch (comparison)
      {
        case ListComparison.ContainsAll:
          for (int index = 0; index < count; ++index)
          {
            if (!((IEnumerable<T>) currents).Contains<T>(expected[index]))
              return false;
          }
          return true;
        case ListComparison.ContainsAtLeastOne:
          for (int index = 0; index < count; ++index)
          {
            if (((IEnumerable<T>) currents).Contains<T>(expected[index]))
              return true;
          }
          return false;
        case ListComparison.ContainsExactlyOne:
          bool flag = false;
          for (int index = 0; index < count; ++index)
          {
            if (((IEnumerable<T>) currents).Contains<T>(expected[index]))
            {
              if (flag)
                return false;
              flag = true;
            }
          }
          return flag;
        case ListComparison.ContainsNone:
          for (int index = 0; index < count; ++index)
          {
            if (((IEnumerable<T>) currents).Contains<T>(expected[index]))
              return false;
          }
          return true;
        default:
          throw new ArgumentOutOfRangeException(nameof (comparison), (object) comparison, (string) null);
      }
    }
  }
}
