// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloatingCounterModificationEffectDefinition
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
  public sealed class FloatingCounterModificationEffectDefinition : 
    EffectExecutionWithDurationDefinition
  {
    private CaracId m_counterType;
    private ValueModifier m_modifier;
    private DynamicValue m_value;
    private ISingleEntitySelector m_source;
    private bool m_canAddWithoutPriorSet;

    public CaracId counterType => this.m_counterType;

    public ValueModifier modifier => this.m_modifier;

    public DynamicValue value => this.m_value;

    public ISingleEntitySelector source => this.m_source;

    public bool canAddWithoutPriorSet => this.m_canAddWithoutPriorSet;

    public override string ToString()
    {
      switch (this.modifier)
      {
        case ValueModifier.Set:
          return string.Format("{0}: Initialized to {1}{2}", (object) this.m_counterType, (object) this.m_value, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));
        case ValueModifier.Add:
          return string.Format("{0}: Add {1}{2}", (object) this.m_counterType, (object) this.m_value, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));
        default:
          return string.Format("operator unknow: {0} ", (object) this.modifier);
      }
    }

    public static FloatingCounterModificationEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FloatingCounterModificationEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FloatingCounterModificationEffectDefinition effectDefinition = new FloatingCounterModificationEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static FloatingCounterModificationEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FloatingCounterModificationEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FloatingCounterModificationEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_counterType = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "counterType", 20);
      this.m_modifier = (ValueModifier) Serialization.JsonTokenValue<int>(jsonObject, "modifier", 1);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
      this.m_canAddWithoutPriorSet = Serialization.JsonTokenValue<bool>(jsonObject, "canAddWithoutPriorSet");
    }
  }
}
