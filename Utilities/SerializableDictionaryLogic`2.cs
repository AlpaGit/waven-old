// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.SerializableDictionaryLogic`2
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
  public abstract class SerializableDictionaryLogic<TKey, TValue> : 
    Dictionary<TKey, TValue>,
    ISerializationCallbackReceiver
  {
    protected abstract TKey[] m_keyArray { get; set; }

    protected abstract TValue[] m_valueArray { get; set; }

    protected SerializableDictionaryLogic()
    {
    }

    protected SerializableDictionaryLogic(IEqualityComparer<TKey> comparer)
      : base(comparer)
    {
    }

    protected SerializableDictionaryLogic(int capacity)
      : base(capacity)
    {
    }

    protected SerializableDictionaryLogic(int capacity, IEqualityComparer<TKey> comparer)
      : base(capacity, comparer)
    {
    }

    protected SerializableDictionaryLogic(IDictionary<TKey, TValue> dictionary)
      : base(dictionary)
    {
    }

    protected SerializableDictionaryLogic(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
      : base(dictionary, comparer)
    {
    }

    public void OnBeforeSerialize()
    {
      int count = this.Count;
      TKey[] keyArray = this.m_keyArray;
      TValue[] objArray = this.m_valueArray;
      if (count != keyArray.Length || count != objArray.Length)
      {
        keyArray = new TKey[count];
        objArray = new TValue[count];
      }
      int index1 = 0;
      foreach (TKey key in this.Keys)
      {
        keyArray[index1] = key;
        ++index1;
      }
      int index2 = 0;
      foreach (TValue obj in this.Values)
      {
        objArray[index2] = obj;
        ++index2;
      }
      this.m_keyArray = keyArray;
      this.m_valueArray = objArray;
    }

    public void OnAfterDeserialize()
    {
      TKey[] mKeyArray = this.m_keyArray;
      TValue[] mValueArray = this.m_valueArray;
      int length1 = mKeyArray.Length;
      int length2 = mValueArray.Length;
      int num = length1 == length2 ? length1 : Math.Min(length1, length2);
      this.Clear();
      for (int index = 0; index < num; ++index)
      {
        TKey key = mKeyArray[index];
        if (!this.ContainsKey(key))
          this.Add(key, mValueArray[index]);
      }
    }
  }
}
