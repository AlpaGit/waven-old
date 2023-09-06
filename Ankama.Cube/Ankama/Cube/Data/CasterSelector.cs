// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CasterSelector
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
  public sealed class CasterSelector : 
    EntitySelectorForCast,
    ISingleEntitySelector,
    IEntitySelector,
    ITargetSelector,
    IEditableContent,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static CasterSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CasterSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CasterSelector casterSelector = new CasterSelector();
      casterSelector.PopulateFromJson(jsonObject);
      return casterSelector;
    }

    public static CasterSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CasterSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CasterSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      if (context is DynamicValueFightContext valueFightContext)
        return valueFightContext.fightStatus.TryGetEntity<T>(valueFightContext.playerId, out entity);
      entity = default (T);
      return false;
    }

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }
  }
}
