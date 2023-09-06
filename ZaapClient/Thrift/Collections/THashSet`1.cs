// Decompiled with JetBrains decompiler
// Type: Thrift.Collections.THashSet`1
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Thrift.Collections
{
  [Serializable]
  public class THashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
  {
    private HashSet<T> set = new HashSet<T>();

    public int Count => this.set.Count;

    public bool IsReadOnly => false;

    public void Add(T item) => this.set.Add(item);

    public void Clear() => this.set.Clear();

    public bool Contains(T item) => this.set.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => this.set.CopyTo(array, arrayIndex);

    public IEnumerator GetEnumerator() => (IEnumerator) this.set.GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>) this.set).GetEnumerator();

    public bool Remove(T item) => this.set.Remove(item);
  }
}
