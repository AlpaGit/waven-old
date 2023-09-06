// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TriggeringEventTargetSelector
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
  public sealed class TriggeringEventTargetSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static TriggeringEventTargetSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TriggeringEventTargetSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TriggeringEventTargetSelector eventTargetSelector = new TriggeringEventTargetSelector();
      eventTargetSelector.PopulateFromJson(jsonObject);
      return eventTargetSelector;
    }

    public static TriggeringEventTargetSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TriggeringEventTargetSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TriggeringEventTargetSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity
    {
      entity = default (T);
      return false;
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      IEntity entity;
      if (this.TryGetEntity<IEntity>(context, out entity))
        yield return entity;
    }
  }
}
