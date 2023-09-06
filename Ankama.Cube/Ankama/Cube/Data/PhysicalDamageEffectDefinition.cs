// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PhysicalDamageEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class PhysicalDamageEffectDefinition : DamageEffectDefinition
  {
    private ISingleEntitySelector m_source;

    public ISingleEntitySelector source => this.m_source;

    public override string ToString() => string.Format("Physical damage {0} on {1}{2}", (object) this.m_value, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static PhysicalDamageEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PhysicalDamageEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PhysicalDamageEffectDefinition effectDefinition = new PhysicalDamageEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static PhysicalDamageEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PhysicalDamageEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PhysicalDamageEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
    }
  }
}
