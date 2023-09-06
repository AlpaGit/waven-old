// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.RandomTargetsFilter
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
  public sealed class RandomTargetsFilter : 
    IEditableContent,
    ICoordOrEntityFilter,
    ICoordFilter,
    ITargetFilter,
    IEntityFilter
  {
    private DynamicValue m_count;

    public DynamicValue count => this.m_count;

    public override string ToString() => this.GetType().Name;

    public static RandomTargetsFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (RandomTargetsFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      RandomTargetsFilter randomTargetsFilter = new RandomTargetsFilter();
      randomTargetsFilter.PopulateFromJson(jsonObject);
      return randomTargetsFilter;
    }

    public static RandomTargetsFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      RandomTargetsFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : RandomTargetsFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_count = DynamicValue.FromJsonProperty(jsonObject, "count");

    public IEnumerable<Coord> Filter(IEnumerable<Coord> coords, DynamicValueContext context) => coords;

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context) => entities;
  }
}
