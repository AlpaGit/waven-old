// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PointAreaDefinition
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
  public sealed class PointAreaDefinition : IEditableContent, IObjectAreaDefinition, IAreaDefinition
  {
    public override string ToString() => this.GetType().Name;

    public static PointAreaDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PointAreaDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PointAreaDefinition pointAreaDefinition = new PointAreaDefinition();
      pointAreaDefinition.PopulateFromJson(jsonObject);
      return pointAreaDefinition;
    }

    public static PointAreaDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PointAreaDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PointAreaDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public Area ToArea(Vector2Int position) => (Area) new PointArea(position);
  }
}
