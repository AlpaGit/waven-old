// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NotCoordFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class NotCoordFilter : IEditableContent, ICoordFilter, ITargetFilter
  {
    private ICoordFilter m_filter;

    public ICoordFilter filter => this.m_filter;

    public override string ToString() => string.Format("not ({0})", (object) this.filter);

    public static NotCoordFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NotCoordFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NotCoordFilter notCoordFilter = new NotCoordFilter();
      notCoordFilter.PopulateFromJson(jsonObject);
      return notCoordFilter;
    }

    public static NotCoordFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NotCoordFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NotCoordFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_filter = ICoordFilterUtils.FromJsonProperty(jsonObject, "filter");

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      List<Coord> coordList = ListPool<Coord>.Get();
      List<Coord> filtered = ListPool<Coord>.Get();
      coordList.AddRange(coords);
      filtered.AddRange(this.m_filter.Filter((IEnumerable<Coord>) coordList, context));
      int coordCount = coordList.Count;
      int filteredCount = filtered.Count;
label_7:
      for (int i = 0; i < coordCount; ++i)
      {
        Coord coord = coordList[i];
        for (int index = 0; index < filteredCount; ++index)
        {
          if (coord == filtered[index])
            goto label_7;
        }
        yield return coord;
      }
      ListPool<Coord>.Release(coordList);
      ListPool<Coord>.Release(filtered);
    }
  }
}
