// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SumValue
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
  public sealed class SumValue : DynamicValue
  {
    private List<DynamicValue> m_valuesToSum;

    public IReadOnlyList<DynamicValue> valuesToSum => (IReadOnlyList<DynamicValue>) this.m_valuesToSum;

    public override string ToString() => string.Join<DynamicValue>(" + ", (IEnumerable<DynamicValue>) this.valuesToSum) ?? "";

    public static SumValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SumValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SumValue sumValue = new SumValue();
      sumValue.PopulateFromJson(jsonObject);
      return sumValue;
    }

    public static SumValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SumValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SumValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "valuesToSum");
      this.m_valuesToSum = new List<DynamicValue>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_valuesToSum.Add(DynamicValue.FromJsonToken(token));
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      int num1 = 0;
      bool flag = true;
      for (int index = 0; index < this.m_valuesToSum.Count; ++index)
      {
        int num2;
        flag &= this.m_valuesToSum[index].GetValue(context, out num2);
        num1 += num2;
      }
      value = num1;
      return flag;
    }

    public override bool ToString(DynamicValueContext context, out string value)
    {
      int num1;
      int num2 = this.GetValue(context, out num1) ? 1 : 0;
      value = num1.ToString();
      return num2 != 0;
    }
  }
}
