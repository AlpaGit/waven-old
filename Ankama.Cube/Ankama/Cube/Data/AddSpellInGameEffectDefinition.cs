// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AddSpellInGameEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class AddSpellInGameEffectDefinition : EffectExecutionDefinition
  {
    private Id<SpellDefinition> m_spellDef;
    private SpellDestination m_destination;

    public Id<SpellDefinition> spellDef => this.m_spellDef;

    public SpellDestination destination => this.m_destination;

    public override string ToString() => string.Format("Add spell {0} to {1}{2}", (object) this.m_spellDef, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static AddSpellInGameEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AddSpellInGameEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AddSpellInGameEffectDefinition effectDefinition = new AddSpellInGameEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static AddSpellInGameEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AddSpellInGameEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AddSpellInGameEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_spellDef = Serialization.JsonTokenIdValue<SpellDefinition>(jsonObject, "spellDef");
      this.m_destination = (SpellDestination) Serialization.JsonTokenValue<int>(jsonObject, "destination");
    }
  }
}
