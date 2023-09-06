// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.WoundedFilter
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
  public sealed class WoundedFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private bool m_isWounded;

    public bool isWounded => this.m_isWounded;

    public override string ToString() => (this.m_isWounded ? "" : "not ") + "wounded";

    public static WoundedFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (WoundedFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      WoundedFilter woundedFilter = new WoundedFilter();
      woundedFilter.PopulateFromJson(jsonObject);
      return woundedFilter;
    }

    public static WoundedFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      WoundedFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : WoundedFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_isWounded = Serialization.JsonTokenValue<bool>(jsonObject, "isWounded", true);

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithLife entityWithLife && entityWithLife.wounded)
          yield return entity;
      }
    }
  }
}
