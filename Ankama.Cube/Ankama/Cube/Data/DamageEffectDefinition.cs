// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DamageEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class DamageEffectDefinition : EffectExecutionDefinition
  {
    protected DynamicValue m_value;

    public DynamicValue value => this.m_value;

    public override string ToString() => string.Format("Damage {0} on {1}{2}", (object) this.m_value, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static DamageEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DamageEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class DamageEffectDefinition");
        return (DamageEffectDefinition) null;
      }
      string str = jtoken.Value<string>();
      DamageEffectDefinition effectDefinition;
      switch (str)
      {
        case "PhysicalDamageEffectDefinition":
          effectDefinition = (DamageEffectDefinition) new PhysicalDamageEffectDefinition();
          break;
        case "MagicalDamageEffectDefinition":
          effectDefinition = (DamageEffectDefinition) new MagicalDamageEffectDefinition();
          break;
        case "LifeLeechEffectDefinition":
          effectDefinition = (DamageEffectDefinition) new LifeLeechEffectDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (DamageEffectDefinition) null;
      }
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static DamageEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DamageEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DamageEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
    }
  }
}
