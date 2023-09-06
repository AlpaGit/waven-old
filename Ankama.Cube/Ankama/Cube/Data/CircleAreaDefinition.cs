// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CircleAreaDefinition
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
  public sealed class CircleAreaDefinition : IEditableContent, IAreaDefinition
  {
    private int m_radius;

    public int radius => this.m_radius;

    public override string ToString() => this.GetType().Name;

    public static CircleAreaDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CircleAreaDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CircleAreaDefinition circleAreaDefinition = new CircleAreaDefinition();
      circleAreaDefinition.PopulateFromJson(jsonObject);
      return circleAreaDefinition;
    }

    public static CircleAreaDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CircleAreaDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CircleAreaDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_radius = Serialization.JsonTokenValue<int>(jsonObject, "radius", 1);

    public Area ToArea(Vector2Int position) => this.m_radius == 0 ? (Area) new PointArea(position) : (Area) new CircleArea(position, this.m_radius);
  }
}
