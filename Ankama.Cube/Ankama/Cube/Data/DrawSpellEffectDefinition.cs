// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DrawSpellEffectDefinition
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
  public sealed class DrawSpellEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_count;
    private List<SpellFilter> m_spellFilters;

    public DynamicValue count => this.m_count;

    public IReadOnlyList<SpellFilter> spellFilters => (IReadOnlyList<SpellFilter>) this.m_spellFilters;

    public override string ToString() => string.Format("{0} draws {1} spells{2}", (object) this.m_executionTargetSelector, (object) this.count, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static DrawSpellEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DrawSpellEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DrawSpellEffectDefinition effectDefinition = new DrawSpellEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static DrawSpellEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DrawSpellEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DrawSpellEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_count = DynamicValue.FromJsonProperty(jsonObject, "count");
      JArray jarray = Serialization.JsonArray(jsonObject, "spellFilters");
      this.m_spellFilters = new List<SpellFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_spellFilters.Add(SpellFilter.FromJsonToken(token));
    }
  }
}
