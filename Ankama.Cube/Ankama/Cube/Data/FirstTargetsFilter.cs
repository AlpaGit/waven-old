// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FirstTargetsFilter
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
  public sealed class FirstTargetsFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter
  {
    private DynamicValue m_count;

    public DynamicValue count => this.m_count;

    public override string ToString() => this.GetType().Name;

    public static FirstTargetsFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FirstTargetsFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FirstTargetsFilter firstTargetsFilter = new FirstTargetsFilter();
      firstTargetsFilter.PopulateFromJson(jsonObject);
      return firstTargetsFilter;
    }

    public static FirstTargetsFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FirstTargetsFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FirstTargetsFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_count = DynamicValue.FromJsonProperty(jsonObject, "count");

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context)
    {
      int left;
      this.m_count.GetValue(context, out left);
      if (left != 0)
      {
        foreach (Coord coord in coords)
        {
          yield return coord;
          --left;
          if (left == 0)
            break;
        }
      }
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      int left;
      this.m_count.GetValue(context, out left);
      if (left != 0)
      {
        foreach (IEntity entity in entities)
        {
          yield return entity;
          --left;
          if (left == 0)
            break;
        }
      }
    }
  }
}
