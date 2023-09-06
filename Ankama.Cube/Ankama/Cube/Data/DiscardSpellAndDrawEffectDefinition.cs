// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DiscardSpellAndDrawEffectDefinition
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
  public sealed class DiscardSpellAndDrawEffectDefinition : EffectExecutionDefinition
  {
    private bool m_randomly;
    private DynamicValue m_count;
    private List<SpellFilter> m_discardSpellFilters;
    private List<SpellFilter> m_drawSpellFilters;

    public bool randomly => this.m_randomly;

    public DynamicValue count => this.m_count;

    public IReadOnlyList<SpellFilter> discardSpellFilters => (IReadOnlyList<SpellFilter>) this.m_discardSpellFilters;

    public IReadOnlyList<SpellFilter> drawSpellFilters => (IReadOnlyList<SpellFilter>) this.m_drawSpellFilters;

    public override string ToString() => string.Format("{0} discards {1} spells and draws the same amount{2}", (object) this.m_executionTargetSelector, (object) this.count, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static DiscardSpellAndDrawEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DiscardSpellAndDrawEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DiscardSpellAndDrawEffectDefinition effectDefinition = new DiscardSpellAndDrawEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static DiscardSpellAndDrawEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DiscardSpellAndDrawEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DiscardSpellAndDrawEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_randomly = Serialization.JsonTokenValue<bool>(jsonObject, "randomly", true);
      this.m_count = DynamicValue.FromJsonProperty(jsonObject, "count");
      JArray jarray1 = Serialization.JsonArray(jsonObject, "discardSpellFilters");
      this.m_discardSpellFilters = new List<SpellFilter>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_discardSpellFilters.Add(SpellFilter.FromJsonToken(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "drawSpellFilters");
      this.m_drawSpellFilters = new List<SpellFilter>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 == null)
        return;
      foreach (JToken token in jarray2)
        this.m_drawSpellFilters.Add(SpellFilter.FromJsonToken(token));
    }
  }
}
