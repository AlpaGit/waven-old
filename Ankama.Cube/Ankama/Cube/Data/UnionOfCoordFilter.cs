// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UnionOfCoordFilter
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
  public sealed class UnionOfCoordFilter : 
    IEditableContent,
    ICoordFilter,
    ITargetFilter,
    IUnionTargetsFilter
  {
    private ICoordFilter m_firstFilter;
    private ICoordFilter m_secondFilter;

    public ICoordFilter firstFilter => this.m_firstFilter;

    public ICoordFilter secondFilter => this.m_secondFilter;

    public override string ToString() => string.Format("({0} OR {1})", (object) this.m_firstFilter, (object) this.m_secondFilter);

    public static UnionOfCoordFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UnionOfCoordFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UnionOfCoordFilter unionOfCoordFilter = new UnionOfCoordFilter();
      unionOfCoordFilter.PopulateFromJson(jsonObject);
      return unionOfCoordFilter;
    }

    public static UnionOfCoordFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UnionOfCoordFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UnionOfCoordFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_firstFilter = ICoordFilterUtils.FromJsonProperty(jsonObject, "firstFilter");
      this.m_secondFilter = ICoordFilterUtils.FromJsonProperty(jsonObject, "secondFilter");
    }

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      Coord[] allCoords = coords.ToArray<Coord>();
      Coord[] first = this.m_firstFilter.Filter((IEnumerable<Coord>) allCoords, context).ToArray<Coord>();
      for (int i = 0; i < first.Length; ++i)
        yield return first[i];
      foreach (Coord coord in this.m_secondFilter.Filter((IEnumerable<Coord>) allCoords, context))
      {
        if (!((IEnumerable<Coord>) first).Contains<Coord>(coord))
          yield return coord;
      }
    }
  }
}
