// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OrCondition
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
  public sealed class OrCondition : EffectCondition
  {
    private List<EffectCondition> m_conditions;

    public IReadOnlyList<EffectCondition> conditions => (IReadOnlyList<EffectCondition>) this.m_conditions;

    public override string ToString() => "(" + string.Join<EffectCondition>(" or ", (IEnumerable<EffectCondition>) this.conditions) + ")";

    public static OrCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OrCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OrCondition orCondition = new OrCondition();
      orCondition.PopulateFromJson(jsonObject);
      return orCondition;
    }

    public static OrCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OrCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OrCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "conditions");
      this.m_conditions = new List<EffectCondition>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_conditions.Add(EffectCondition.FromJsonToken(token));
    }

    public override bool IsValid(DynamicValueContext context)
    {
      for (int index = 0; index < this.m_conditions.Count; ++index)
      {
        if (this.m_conditions[index].IsValid(context))
          return true;
      }
      return false;
    }
  }
}
