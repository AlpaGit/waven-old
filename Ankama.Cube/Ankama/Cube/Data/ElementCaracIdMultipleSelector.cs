// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementCaracIdMultipleSelector
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
  public sealed class ElementCaracIdMultipleSelector : IEditableContent, ICaracIdSelector
  {
    private List<CaracId> m_elements;

    public IReadOnlyList<CaracId> elements => (IReadOnlyList<CaracId>) this.m_elements;

    public override string ToString() => "(" + string.Join<CaracId>(", ", (IEnumerable<CaracId>) this.m_elements) + ")";

    public static ElementCaracIdMultipleSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementCaracIdMultipleSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementCaracIdMultipleSelector multipleSelector = new ElementCaracIdMultipleSelector();
      multipleSelector.PopulateFromJson(jsonObject);
      return multipleSelector;
    }

    public static ElementCaracIdMultipleSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementCaracIdMultipleSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementCaracIdMultipleSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_elements = Serialization.JsonArrayAsList<CaracId>(jsonObject, "elements");
  }
}
