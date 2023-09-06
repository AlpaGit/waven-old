// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UnionOfCoordsSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class UnionOfCoordsSelector : CoordSelectorForCast
  {
    private ICoordSelector m_first;
    private ICoordSelector m_second;

    public ICoordSelector first => this.m_first;

    public ICoordSelector second => this.m_second;

    public override string ToString() => string.Format("({0} OR {1})", (object) this.m_first, (object) this.m_second);

    public static UnionOfCoordsSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UnionOfCoordsSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UnionOfCoordsSelector ofCoordsSelector = new UnionOfCoordsSelector();
      ofCoordsSelector.PopulateFromJson(jsonObject);
      return ofCoordsSelector;
    }

    public static UnionOfCoordsSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UnionOfCoordsSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UnionOfCoordsSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_first = ICoordSelectorUtils.FromJsonProperty(jsonObject, "first");
      this.m_second = ICoordSelectorUtils.FromJsonProperty(jsonObject, "second");
    }

    public override IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      HashSet<Coord> coordsSet = new HashSet<Coord>();
      foreach (Coord c in this.m_first.EnumerateCoords(context))
      {
        yield return c;
        coordsSet.Add(c);
      }
      foreach (Coord enumerateCoord in this.m_second.EnumerateCoords(context))
      {
        if (coordsSet.Add(enumerateCoord))
          yield return enumerateCoord;
      }
    }
  }
}
