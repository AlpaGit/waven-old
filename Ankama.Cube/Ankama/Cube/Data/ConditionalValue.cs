// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ConditionalValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ConditionalValue : DynamicValue
  {
    private EffectCondition m_condition;
    private DynamicValue m_ifTrue;
    private DynamicValue m_ifFalse;

    public EffectCondition condition => this.m_condition;

    public DynamicValue ifTrue => this.m_ifTrue;

    public DynamicValue ifFalse => this.m_ifFalse;

    public override string ToString() => this.GetType().Name;

    public static ConditionalValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ConditionalValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ConditionalValue conditionalValue = new ConditionalValue();
      conditionalValue.PopulateFromJson(jsonObject);
      return conditionalValue;
    }

    public static ConditionalValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ConditionalValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ConditionalValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
      this.m_ifTrue = DynamicValue.FromJsonProperty(jsonObject, "ifTrue");
      this.m_ifFalse = DynamicValue.FromJsonProperty(jsonObject, "ifFalse");
    }

    public override bool GetValue(DynamicValueContext context, out int value) => this.m_condition.IsValid(context) ? this.m_ifTrue.GetValue(context, out value) : this.m_ifFalse.GetValue(context, out value);
  }
}
