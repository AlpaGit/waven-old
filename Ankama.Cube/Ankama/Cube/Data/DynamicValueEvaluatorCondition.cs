// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DynamicValueEvaluatorCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class DynamicValueEvaluatorCondition : EffectCondition
  {
    private DynamicValue m_value;
    private ValueFilter m_evaluator;

    public DynamicValue value => this.m_value;

    public ValueFilter evaluator => this.m_evaluator;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_value, (object) this.m_evaluator);

    public static DynamicValueEvaluatorCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DynamicValueEvaluatorCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DynamicValueEvaluatorCondition evaluatorCondition = new DynamicValueEvaluatorCondition();
      evaluatorCondition.PopulateFromJson(jsonObject);
      return evaluatorCondition;
    }

    public static DynamicValueEvaluatorCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DynamicValueEvaluatorCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DynamicValueEvaluatorCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
      this.m_evaluator = ValueFilter.FromJsonProperty(jsonObject, "evaluator");
    }

    public override bool IsValid(DynamicValueContext context)
    {
      int num = 0;
      this.value.GetValue(context, out num);
      return this.evaluator.Matches(num, context);
    }
  }
}
