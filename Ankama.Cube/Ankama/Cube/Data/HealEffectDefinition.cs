// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.HealEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class HealEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_value;
    private ISingleEntitySelector m_source;

    public DynamicValue value => this.m_value;

    public ISingleEntitySelector source => this.m_source;

    public override string ToString() => string.Format("Heal {0} on {1}{2}", (object) this.m_value, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static HealEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (HealEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      HealEffectDefinition effectDefinition = new HealEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static HealEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      HealEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : HealEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
    }
  }
}
