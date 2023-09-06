// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PivotBasedSquareAreaDefinition
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
  public sealed class PivotBasedSquareAreaDefinition : 
    IEditableContent,
    IObjectAreaDefinition,
    IAreaDefinition
  {
    private int m_side;

    public int side => this.m_side;

    public override string ToString() => this.GetType().Name;

    public static PivotBasedSquareAreaDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PivotBasedSquareAreaDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PivotBasedSquareAreaDefinition squareAreaDefinition = new PivotBasedSquareAreaDefinition();
      squareAreaDefinition.PopulateFromJson(jsonObject);
      return squareAreaDefinition;
    }

    public static PivotBasedSquareAreaDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PivotBasedSquareAreaDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PivotBasedSquareAreaDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_side = Serialization.JsonTokenValue<int>(jsonObject, "side", 2);

    public Area ToArea(Vector2Int position) => this.m_side == 1 ? (Area) new PointArea(position) : (Area) new PivotBasedSquareArea(position, this.m_side);
  }
}
