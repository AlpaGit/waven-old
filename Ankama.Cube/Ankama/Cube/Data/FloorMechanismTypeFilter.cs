// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloorMechanismTypeFilter
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
  public sealed class FloorMechanismTypeFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private FloorMechanismType m_floorType;

    public FloorMechanismType floorType => this.m_floorType;

    public override string ToString() => string.Format("{0}", (object) this.m_floorType);

    public static FloorMechanismTypeFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FloorMechanismTypeFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FloorMechanismTypeFilter mechanismTypeFilter = new FloorMechanismTypeFilter();
      mechanismTypeFilter.PopulateFromJson(jsonObject);
      return mechanismTypeFilter;
    }

    public static FloorMechanismTypeFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FloorMechanismTypeFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FloorMechanismTypeFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_floorType = (FloorMechanismType) Serialization.JsonTokenValue<int>(jsonObject, "floorType", 1);

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      FloorMechanismType searchType = this.m_floorType;
      foreach (IEntity entity in entities)
      {
        if (entity is FloorMechanismStatus floorMechanismStatus && ((FloorMechanismDefinition) floorMechanismStatus.definition).floorType == searchType)
          yield return entity;
      }
    }
  }
}
