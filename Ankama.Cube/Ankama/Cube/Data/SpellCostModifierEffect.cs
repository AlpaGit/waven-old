// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellCostModifierEffect
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
  public sealed class SpellCostModifierEffect : EffectExecutionWithDurationDefinition
  {
    private DynamicValue m_modification;
    private List<SpellFilter> m_spellFilters;

    public DynamicValue modification => this.m_modification;

    public IReadOnlyList<SpellFilter> spellFilters => (IReadOnlyList<SpellFilter>) this.m_spellFilters;

    public override string ToString() => string.Format("cost += {0}{1}", (object) this.m_modification, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static SpellCostModifierEffect FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellCostModifierEffect) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellCostModifierEffect costModifierEffect = new SpellCostModifierEffect();
      costModifierEffect.PopulateFromJson(jsonObject);
      return costModifierEffect;
    }

    public static SpellCostModifierEffect FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellCostModifierEffect defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellCostModifierEffect.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_modification = DynamicValue.FromJsonProperty(jsonObject, "modification");
      JArray jarray = Serialization.JsonArray(jsonObject, "spellFilters");
      this.m_spellFilters = new List<SpellFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_spellFilters.Add(SpellFilter.FromJsonToken(token));
    }
  }
}
