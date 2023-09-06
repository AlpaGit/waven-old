// Decompiled with JetBrains decompiler
// Type: Thrift.Collections.TCollections
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System.Collections;

namespace Thrift.Collections
{
  public class TCollections
  {
    public static bool Equals(IEnumerable first, IEnumerable second)
    {
      if (first == null && second == null)
        return true;
      if (first == null || second == null)
        return false;
      IEnumerator enumerator1 = first.GetEnumerator();
      IEnumerator enumerator2 = second.GetEnumerator();
      bool flag1 = enumerator1.MoveNext();
      bool flag2;
      for (flag2 = enumerator2.MoveNext(); flag1 && flag2; flag2 = enumerator2.MoveNext())
      {
        IEnumerable current1 = enumerator1.Current as IEnumerable;
        IEnumerable current2 = enumerator2.Current as IEnumerable;
        if (current1 != null && current2 != null)
        {
          if (!TCollections.Equals(current1, current2))
            return false;
        }
        else if (current1 == null ^ current2 == null || !object.Equals(enumerator1.Current, enumerator2.Current))
          return false;
        flag1 = enumerator1.MoveNext();
      }
      return flag1 == flag2;
    }

    public static int GetHashCode(IEnumerable enumerable)
    {
      if (enumerable == null)
        return 0;
      int hashCode = 0;
      foreach (object obj in enumerable)
      {
        int num = obj is IEnumerable enumerable1 ? TCollections.GetHashCode(enumerable1) : obj.GetHashCode();
        hashCode = hashCode * 397 ^ num;
      }
      return hashCode;
    }
  }
}
