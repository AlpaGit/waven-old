// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CaracIdDirectSelector
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
  public sealed class CaracIdDirectSelector : 
    IEditableContent,
    ISingleCaracIdSelector,
    ICaracIdSelector
  {
    private CaracId m_caracId;

    public CaracId caracId => this.m_caracId;

    public override string ToString() => this.m_caracId.ToString();

    public static CaracIdDirectSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CaracIdDirectSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CaracIdDirectSelector idDirectSelector = new CaracIdDirectSelector();
      idDirectSelector.PopulateFromJson(jsonObject);
      return idDirectSelector;
    }

    public static CaracIdDirectSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CaracIdDirectSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CaracIdDirectSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_caracId = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "caracId", 1);

    public CaracId GetCaracId() => this.m_caracId;
  }
}
