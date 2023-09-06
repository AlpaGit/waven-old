// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.LocalizedTextData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data.UI
{
  public class LocalizedTextData : ScriptableObject
  {
    [UsedImplicitly]
    [SerializeField]
    private LocalizedTextData.TextKeyDictionary m_textKeyDictionary = new LocalizedTextData.TextKeyDictionary();
    [UsedImplicitly]
    [SerializeField]
    private LocalizedTextData.TextCollectionDictionary m_textCollectionDictionary = new LocalizedTextData.TextCollectionDictionary();
    private readonly Dictionary<string, int> m_textKeyNameDictionary = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public void Initialize()
    {
      this.m_textKeyNameDictionary.Clear();
      foreach (KeyValuePair<int, string> textKey in (Dictionary<int, string>) this.m_textKeyDictionary)
      {
        int key = textKey.Key;
        this.m_textKeyNameDictionary[textKey.Value] = key;
      }
    }

    public bool TryGetTextCollectionReference(
      string textCollectionName,
      out AssetReference textCollectionReference)
    {
      return this.m_textCollectionDictionary.TryGetValue(textCollectionName, out textCollectionReference);
    }

    public bool TryGetKeyId(string keyName, out int id) => this.m_textKeyNameDictionary.TryGetValue(keyName, out id);

    public bool TryGetKeyName(int id, out string keyName) => this.m_textKeyDictionary.TryGetValue(id, out keyName);

    [Serializable]
    private class TextKeyDictionary : SerializableDictionary<int, string>
    {
    }

    [Serializable]
    private class TextCollectionDictionary : SerializableDictionaryLogic<string, AssetReference>
    {
      [SerializeField]
      private string[] m_keys = new string[0];
      [SerializeField]
      private AssetReference[] m_values = new AssetReference[0];

      protected override string[] m_keyArray
      {
        get => this.m_keys;
        set => this.m_keys = value;
      }

      protected override AssetReference[] m_valueArray
      {
        get => this.m_values;
        set => this.m_values = value;
      }
    }
  }
}
