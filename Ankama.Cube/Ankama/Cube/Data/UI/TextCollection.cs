// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.TextCollection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data.UI
{
  [CreateAssetMenu(fileName = "New Text Collection", menuName = "Waven/Data/UI/Text Collection")]
  public class TextCollection : ScriptableObject
  {
    [UsedImplicitly]
    [SerializeField]
    private CultureCode m_cultureCode;
    [UsedImplicitly]
    [SerializeField]
    private TextCollection.DataDictionary m_dataDictionary = new TextCollection.DataDictionary();

    public CultureCode cultureCode => this.m_cultureCode;

    public void FeedDictionary(Dictionary<int, string> dictionary)
    {
      foreach (KeyValuePair<int, string> data in (Dictionary<int, string>) this.m_dataDictionary)
        dictionary[data.Key] = data.Value;
    }

    public void StarveDictionary(Dictionary<int, string> dictionary)
    {
      foreach (KeyValuePair<int, string> data in (Dictionary<int, string>) this.m_dataDictionary)
        dictionary.Remove(data.Key);
    }

    public bool TryGetValue(int id, out string value) => this.m_dataDictionary.TryGetValue(id, out value);

    [Serializable]
    private class DataDictionary : SerializableDictionary<int, string>
    {
    }
  }
}
