// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.AssetReferences.AssetReferenceMap
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.AssetManagement.AssetReferences
{
  [PublicAPI]
  public sealed class AssetReferenceMap : ScriptableObject, ISerializationCallbackReceiver
  {
    [UsedImplicitly]
    [SerializeField]
    internal bool disable;
    [UsedImplicitly]
    [SerializeField]
    internal bool ignoreAssetsInAssetBundles;
    [UsedImplicitly]
    [SerializeField]
    internal string[] ignoreFolders = new string[0];
    [SerializeField]
    private string[] m_keys = new string[0];
    [SerializeField]
    private string[] m_values = new string[0];
    internal Dictionary<string, string> dictionary = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.Ordinal);

    public void OnBeforeSerialize()
    {
      int count = this.dictionary.Count;
      if (count != this.m_keys.Length || count != this.m_values.Length)
      {
        this.m_keys = new string[count];
        this.m_values = new string[count];
      }
      int index = 0;
      foreach (KeyValuePair<string, string> keyValuePair in this.dictionary)
      {
        this.m_keys[index] = keyValuePair.Key;
        this.m_values[index] = keyValuePair.Value;
        ++index;
      }
    }

    public void OnAfterDeserialize()
    {
      int length1 = this.m_keys.Length;
      int length2 = this.m_values.Length;
      int capacity;
      if (length1 != length2)
      {
        Debug.LogWarning((object) "[AssetManager] Asset Reference Map has a different key and value count, some values will be lost.\nAsset Reference Map needs to be rebuilt.");
        capacity = Math.Min(length1, length2);
      }
      else
        capacity = length1;
      this.dictionary = new Dictionary<string, string>(capacity, (IEqualityComparer<string>) StringComparer.Ordinal);
      for (int index = 0; index < capacity; ++index)
        this.dictionary.Add(this.m_keys[index], this.m_values[index]);
    }
  }
}
