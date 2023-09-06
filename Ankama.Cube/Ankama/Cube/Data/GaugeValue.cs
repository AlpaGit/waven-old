// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.GaugeValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class GaugeValue : IEditableContent
  {
    private CaracId m_element;
    private DynamicValue m_value;

    public CaracId element => this.m_element;

    public DynamicValue value => this.m_value;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_value, (object) this.m_element);

    public static GaugeValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (GaugeValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      GaugeValue gaugeValue = new GaugeValue();
      gaugeValue.PopulateFromJson(jsonObject);
      return gaugeValue;
    }

    public static GaugeValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      GaugeValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : GaugeValue.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_element = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "element");
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
    }

    public void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus playerStatus,
      DynamicValueContext context)
    {
      int modification;
      this.value.GetValue(context, out modification);
      modifications.Increment(this.element, modification);
    }
  }
}
