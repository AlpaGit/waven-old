// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityHasBeenCrossedOverFilter
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
  public sealed class EntityHasBeenCrossedOverFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    public override string ToString() => this.GetType().Name;

    public static EntityHasBeenCrossedOverFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntityHasBeenCrossedOverFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntityHasBeenCrossedOverFilter crossedOverFilter = new EntityHasBeenCrossedOverFilter();
      crossedOverFilter.PopulateFromJson(jsonObject);
      return crossedOverFilter;
    }

    public static EntityHasBeenCrossedOverFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntityHasBeenCrossedOverFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntityHasBeenCrossedOverFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      Debug.LogError((object) "Cannot use Entity Has Been Crossed Over ");
      yield break;
    }
  }
}
