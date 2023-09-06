// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FilteredCoordSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class FilteredCoordSelector : CoordSelectorForCast
  {
    private List<ICoordFilter> m_filters;

    public IReadOnlyList<ICoordFilter> filters => (IReadOnlyList<ICoordFilter>) this.m_filters;

    public override string ToString()
    {
      switch (this.m_filters.Count)
      {
        case 0:
          return "All coords";
        case 1:
          return string.Format("coord with ({0})", (object) this.m_filters[0]);
        default:
          return "Coords where:\n - " + string.Join<ICoordFilter>("\n - ", (IEnumerable<ICoordFilter>) this.filters);
      }
    }

    public static FilteredCoordSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FilteredCoordSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FilteredCoordSelector filteredCoordSelector = new FilteredCoordSelector();
      filteredCoordSelector.PopulateFromJson(jsonObject);
      return filteredCoordSelector;
    }

    public static FilteredCoordSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FilteredCoordSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FilteredCoordSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "filters");
      this.m_filters = new List<ICoordFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_filters.Add(ICoordFilterUtils.FromJsonToken(token));
    }

    public override IEnumerable<Coord> EnumerateCoords(DynamicValueContext context)
    {
      if (!(context is DynamicValueFightContext valueFightContext))
        return Enumerable.Empty<Coord>();
      IEnumerable<Coord> coords = valueFightContext.fightStatus.EnumerateCoords();
      int count = this.m_filters.Count;
      for (int index = 0; index < count; ++index)
        coords = this.m_filters[index].Filter(coords, context);
      return coords;
    }
  }
}
