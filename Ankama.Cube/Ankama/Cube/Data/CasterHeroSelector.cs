// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CasterHeroSelector
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
  public sealed class CasterHeroSelector : 
    EntitySelectorForCast,
    ISingleEntitySelector,
    IEntitySelector,
    ITargetSelector,
    IEditableContent,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static CasterHeroSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CasterHeroSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CasterHeroSelector casterHeroSelector = new CasterHeroSelector();
      casterHeroSelector.PopulateFromJson(jsonObject);
      return casterHeroSelector;
    }

    public static CasterHeroSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CasterHeroSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CasterHeroSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (!(context is DynamicValueFightContext valueFightContext))
      {
        entity = default (T);
        return false;
      }
      PlayerStatus entityStatus;
      if (!valueFightContext.fightStatus.TryGetEntity<PlayerStatus>(valueFightContext.playerId, out entityStatus))
      {
        entity = default (T);
        return false;
      }
      if (!(entityStatus.heroStatus is T heroStatus))
      {
        entity = default (T);
        return false;
      }
      entity = heroStatus;
      return true;
    }

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }
  }
}
