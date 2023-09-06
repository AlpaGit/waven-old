// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NegativeValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class NegativeValue : DynamicValue
  {
    private DynamicValue m_valueToNeg;

    public DynamicValue valueToNeg => this.m_valueToNeg;

    public override string ToString() => string.Format("-{0}", (object) this.valueToNeg);

    public static NegativeValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NegativeValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NegativeValue negativeValue = new NegativeValue();
      negativeValue.PopulateFromJson(jsonObject);
      return negativeValue;
    }

    public static NegativeValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NegativeValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NegativeValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_valueToNeg = DynamicValue.FromJsonProperty(jsonObject, "valueToNeg");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      int num1;
      int num2 = this.m_valueToNeg.GetValue(context, out num1) ? 1 : 0;
      value = -num1;
      return num2 != 0;
    }
  }
}
