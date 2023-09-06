// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ClampValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ClampValue : DynamicValue
  {
    private DynamicValue m_valueToClamp;
    private DynamicValue m_min;
    private DynamicValue m_max;

    public DynamicValue valueToClamp => this.m_valueToClamp;

    public DynamicValue min => this.m_min;

    public DynamicValue max => this.m_max;

    public override string ToString() => this.GetType().Name;

    public static ClampValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ClampValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ClampValue clampValue = new ClampValue();
      clampValue.PopulateFromJson(jsonObject);
      return clampValue;
    }

    public static ClampValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ClampValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ClampValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_valueToClamp = DynamicValue.FromJsonProperty(jsonObject, "valueToClamp");
      this.m_min = DynamicValue.FromJsonProperty(jsonObject, "min");
      this.m_max = DynamicValue.FromJsonProperty(jsonObject, "max");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      int val1;
      this.m_valueToClamp.GetValue(context, out val1);
      if (this.m_min != null)
      {
        int val2;
        this.m_min.GetValue(context, out val2);
        val1 = Math.Max(val1, val2);
      }
      if (this.m_max != null)
      {
        int val2;
        this.m_max.GetValue(context, out val2);
        val1 = Math.Min(val1, val2);
      }
      value = val1;
      return true;
    }
  }
}
