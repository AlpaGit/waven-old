// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CaracChangedEffectDefinition
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
  public sealed class CaracChangedEffectDefinition : EffectExecutionDefinition
  {
    private ICaracIdSelector m_caracSelector;
    private ValueModifier m_modifier;
    private DynamicValue m_value;
    private ISingleEntitySelector m_source;

    public ICaracIdSelector caracSelector => this.m_caracSelector;

    public ValueModifier modifier => this.m_modifier;

    public DynamicValue value => this.m_value;

    public ISingleEntitySelector source => this.m_source;

    public override string ToString()
    {
      switch (this.modifier)
      {
        case ValueModifier.Set:
          return string.Format("Set {0} to {1} for {2}{3}", (object) this.m_caracSelector, (object) this.m_value, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));
        case ValueModifier.Add:
          return string.Format("Add {0} to {1} for {2}{3} ", (object) this.m_value, (object) this.m_caracSelector, (object) this.m_executionTargetSelector, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));
        default:
          return string.Format("operator unknow: {0} ", (object) this.m_modifier);
      }
    }

    public static CaracChangedEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CaracChangedEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CaracChangedEffectDefinition effectDefinition = new CaracChangedEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static CaracChangedEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CaracChangedEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CaracChangedEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_caracSelector = ICaracIdSelectorUtils.FromJsonProperty(jsonObject, "caracSelector");
      this.m_modifier = (ValueModifier) Serialization.JsonTokenValue<int>(jsonObject, "modifier", 1);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
    }
  }
}
