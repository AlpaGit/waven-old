// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementaryStateFilter
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
  public sealed class ElementaryStateFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ElementaryStates m_elementaryState;

    public ElementaryStates elementaryState => this.m_elementaryState;

    public override string ToString() => this.GetType().Name;

    public static ElementaryStateFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementaryStateFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementaryStateFilter elementaryStateFilter = new ElementaryStateFilter();
      elementaryStateFilter.PopulateFromJson(jsonObject);
      return elementaryStateFilter;
    }

    public static ElementaryStateFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementaryStateFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementaryStateFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_elementaryState = (ElementaryStates) Serialization.JsonTokenValue<int>(jsonObject, "elementaryState");

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithElementaryState withElementaryState && withElementaryState.HasElementaryState(this.m_elementaryState))
          yield return entity;
      }
    }
  }
}
