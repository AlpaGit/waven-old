// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpecificSpellFilter
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
  public sealed class SpecificSpellFilter : SpellFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Id<SpellDefinition>> m_spellDefinition;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Id<SpellDefinition>> spellDefinition => (IReadOnlyList<Id<SpellDefinition>>) this.m_spellDefinition;

    public override string ToString()
    {
      switch (this.m_spellDefinition.Count)
      {
        case 0:
          return "spellDefinition is <unset>";
        case 1:
          return string.Format("spellDefinition {0} {1}", (object) this.condition, (object) this.m_spellDefinition[0]);
        default:
          return string.Format("spellDefinition {0} \n - ", (object) this.condition) + string.Join<Id<SpellDefinition>>("\n - ", (IEnumerable<Id<SpellDefinition>>) this.m_spellDefinition);
      }
    }

    public static SpecificSpellFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpecificSpellFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpecificSpellFilter specificSpellFilter = new SpecificSpellFilter();
      specificSpellFilter.PopulateFromJson(jsonObject);
      return specificSpellFilter;
    }

    public static SpecificSpellFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpecificSpellFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpecificSpellFilter.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      JArray jarray = Serialization.JsonArray(jsonObject, "spellDefinition");
      this.m_spellDefinition = new List<Id<SpellDefinition>>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_spellDefinition.Add(Serialization.JsonTokenIdValue<SpellDefinition>(token));
    }

    public override bool Accept(int spellInstanceId, SpellDefinition spellDef)
    {
      int id = spellDef.id;
      for (int index = 0; index < this.m_spellDefinition.Count; ++index)
      {
        if (this.m_spellDefinition[index].value == id)
          return this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      }
      return this.m_condition == ShouldBeInOrNot.ShouldNotBeIn;
    }
  }
}
