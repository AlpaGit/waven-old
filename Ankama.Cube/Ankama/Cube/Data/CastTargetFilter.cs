// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CastTargetFilter
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
  public sealed class CastTargetFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private int m_castTargetIndex;

    public int castTargetIndex => this.m_castTargetIndex;

    public override string ToString() => this.GetType().Name;

    public static CastTargetFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CastTargetFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CastTargetFilter castTargetFilter = new CastTargetFilter();
      castTargetFilter.PopulateFromJson(jsonObject);
      return castTargetFilter;
    }

    public static CastTargetFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CastTargetFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CastTargetFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_castTargetIndex = Serialization.JsonTokenValue<int>(jsonObject, "castTargetIndex");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      if (context is CastTargetContext castTargetContext)
      {
        Target target = castTargetContext.GetTarget(this.m_castTargetIndex);
        if (target.type == Target.Type.Entity)
        {
          IEntity entity1 = target.entity;
          foreach (IEntity entity2 in entities)
          {
            if (entity1 == entity2)
            {
              yield return entity1;
              break;
            }
          }
        }
      }
    }
  }
}
