// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PrecomputedData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class PrecomputedData : IEditableContent
  {
    private List<ILevelOnlyDependant> m_dynamicValueReferences;
    private bool m_checkNumberOfSummonings;
    private bool m_checkNumberOfMechanisms;
    private static readonly KeywordReference[] NoKeywordReference = new KeywordReference[0];
    private KeywordReference[] m_keywordReferences;

    public IReadOnlyList<ILevelOnlyDependant> dynamicValueReferences => (IReadOnlyList<ILevelOnlyDependant>) this.m_dynamicValueReferences;

    public bool checkNumberOfSummonings => this.m_checkNumberOfSummonings;

    public bool checkNumberOfMechanisms => this.m_checkNumberOfMechanisms;

    public override string ToString() => this.GetType().Name;

    public static PrecomputedData FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PrecomputedData) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PrecomputedData precomputedData = new PrecomputedData();
      precomputedData.PopulateFromJson(jsonObject);
      return precomputedData;
    }

    public static PrecomputedData FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PrecomputedData defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PrecomputedData.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "dynamicValueReferences");
      this.m_dynamicValueReferences = new List<ILevelOnlyDependant>(jarray != null ? jarray.Count : 0);
      if (jarray != null)
      {
        foreach (JToken token in jarray)
          this.m_dynamicValueReferences.Add(ILevelOnlyDependantUtils.FromJsonToken(token));
      }
      this.m_checkNumberOfSummonings = Serialization.JsonTokenValue<bool>(jsonObject, "checkNumberOfSummonings");
      this.m_checkNumberOfMechanisms = Serialization.JsonTokenValue<bool>(jsonObject, "checkNumberOfMechanisms");
      this.AdditionalPopulateFromJson(jsonObject);
    }

    public KeywordReference[] keywordReferences => this.m_keywordReferences;

    private void AdditionalPopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "keywordReferences");
      int count = jarray == null ? 0 : jarray.Count;
      if (count > 0)
      {
        this.m_keywordReferences = new KeywordReference[count];
        for (int index = 0; index < count; ++index)
          this.m_keywordReferences[index] = KeywordReference.FromJson(jarray[index]);
      }
      else
        this.m_keywordReferences = PrecomputedData.NoKeywordReference;
    }
  }
}
