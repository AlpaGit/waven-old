// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IntoAreaOfEntityFilter
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
  public sealed class IntoAreaOfEntityFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ISingleEntitySelector m_areaOfEntity;

    public ISingleEntitySelector areaOfEntity => this.m_areaOfEntity;

    public override string ToString() => string.Format("into {0}", (object) this.m_areaOfEntity);

    public static IntoAreaOfEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IntoAreaOfEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      IntoAreaOfEntityFilter areaOfEntityFilter = new IntoAreaOfEntityFilter();
      areaOfEntityFilter.PopulateFromJson(jsonObject);
      return areaOfEntityFilter;
    }

    public static IntoAreaOfEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IntoAreaOfEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IntoAreaOfEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_areaOfEntity = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "areaOfEntity");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      Area area;
      if (ZoneAreaFilterUtils.SingleTargetToCompareArea((ISingleTargetSelector) this.m_areaOfEntity, context, out area))
      {
        foreach (IEntity entity in entities)
        {
          if (entity is IEntityWithBoardPresence withBoardPresence && area.Intersects(withBoardPresence.area))
            yield return entity;
        }
      }
    }
  }
}
