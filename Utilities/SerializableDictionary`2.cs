// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.SerializableDictionary`2
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  [Serializable]
  public abstract class SerializableDictionary<TKey, TValue> : 
    Dictionary<TKey, TValue>,
    ISerializationCallbackReceiver
  {
    [SerializeField]
    private TKey[] m_keys = new TKey[0];
    [SerializeField]
    private TValue[] m_values = new TValue[0];

    protected SerializableDictionary()
    {
    }

    protected SerializableDictionary(IEqualityComparer<TKey> comparer)
      : base(comparer)
    {
    }

    protected SerializableDictionary(int capacity)
      : base(capacity)
    {
    }

    protected SerializableDictionary(int capacity, IEqualityComparer<TKey> comparer)
      : base(capacity, comparer)
    {
    }

    protected SerializableDictionary(IDictionary<TKey, TValue> dictionary)
      : base(dictionary)
    {
    }

    protected SerializableDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
      : base(dictionary, comparer)
    {
    }

    public void OnBeforeSerialize()
    {
      int count = this.Count;
      if (count != this.m_keys.Length || count != this.m_values.Length)
      {
        this.m_keys = new TKey[count];
        this.m_values = new TValue[count];
      }
      int index1 = 0;
      foreach (TKey key in this.Keys)
      {
        this.m_keys[index1] = key;
        ++index1;
      }
      int index2 = 0;
      foreach (TValue obj in this.Values)
      {
        this.m_values[index2] = obj;
        ++index2;
      }
    }

    public void OnAfterDeserialize()
    {
      int length1 = this.m_keys.Length;
      int length2 = this.m_values.Length;
      int num = length1 == length2 ? length1 : Math.Min(length1, length2);
      this.Clear();
      for (int index = 0; index < num; ++index)
      {
        TKey key = this.m_keys[index];
        if (!this.ContainsKey(key))
          this.Add(key, this.m_values[index]);
      }
    }
  }
}
