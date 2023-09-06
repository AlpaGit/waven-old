// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CoordSelectorForCast
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class CoordSelectorForCast : 
    IEditableContent,
    ITargetSelector,
    ISelectorForCast,
    ICoordSelector
  {
    public override string ToString() => this.GetType().Name;

    public static CoordSelectorForCast FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CoordSelectorForCast) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class CoordSelectorForCast");
        return (CoordSelectorForCast) null;
      }
      string str = jtoken.Value<string>();
      CoordSelectorForCast coordSelectorForCast;
      switch (str)
      {
        case "UnionOfCoordsSelector":
          coordSelectorForCast = (CoordSelectorForCast) new UnionOfCoordsSelector();
          break;
        case "FilteredCoordSelector":
          coordSelectorForCast = (CoordSelectorForCast) new FilteredCoordSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (CoordSelectorForCast) null;
      }
      coordSelectorForCast.PopulateFromJson(jsonObject);
      return coordSelectorForCast;
    }

    public static CoordSelectorForCast FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CoordSelectorForCast defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CoordSelectorForCast.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    public abstract IEnumerable<Coord> EnumerateCoords(DynamicValueContext context);

    public IEnumerable<Target> EnumerateTargets(DynamicValueContext context)
    {
      foreach (Coord enumerateCoord in this.EnumerateCoords(context))
        yield return new Target(enumerateCoord);
    }
  }
}
