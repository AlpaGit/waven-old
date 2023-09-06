// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectExecutionWithDurationDefinition
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
  public abstract class EffectExecutionWithDurationDefinition : EffectExecutionDefinition
  {
    protected List<EffectTrigger> m_executionEndTriggers;

    public IReadOnlyList<EffectTrigger> executionEndTriggers => (IReadOnlyList<EffectTrigger>) this.m_executionEndTriggers;

    public override string ToString() => this.GetType().Name;

    public static EffectExecutionWithDurationDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectExecutionWithDurationDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class EffectExecutionWithDurationDefinition");
        return (EffectExecutionWithDurationDefinition) null;
      }
      string str = jtoken.Value<string>();
      EffectExecutionWithDurationDefinition durationDefinition;
      switch (str)
      {
        case "StoppableCaracChangedEffectDefinition":
          durationDefinition = (EffectExecutionWithDurationDefinition) new StoppableCaracChangedEffectDefinition();
          break;
        case "PropertyChangeEffectDefinition":
          durationDefinition = (EffectExecutionWithDurationDefinition) new PropertyChangeEffectDefinition();
          break;
        case "SpellCostModifierEffect":
          durationDefinition = (EffectExecutionWithDurationDefinition) new SpellCostModifierEffect();
          break;
        case "RegisterDamageProtectorEffectDefinition":
          durationDefinition = (EffectExecutionWithDurationDefinition) new RegisterDamageProtectorEffectDefinition();
          break;
        case "ChangeEntitySkinEffectDefinition":
          durationDefinition = (EffectExecutionWithDurationDefinition) new ChangeEntitySkinEffectDefinition();
          break;
        case "FloatingCounterModificationEffectDefinition":
          durationDefinition = (EffectExecutionWithDurationDefinition) new FloatingCounterModificationEffectDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (EffectExecutionWithDurationDefinition) null;
      }
      durationDefinition.PopulateFromJson(jsonObject);
      return durationDefinition;
    }

    public static EffectExecutionWithDurationDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectExecutionWithDurationDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectExecutionWithDurationDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "executionEndTriggers");
      this.m_executionEndTriggers = new List<EffectTrigger>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_executionEndTriggers.Add(EffectTrigger.FromJsonToken(token));
    }
  }
}
