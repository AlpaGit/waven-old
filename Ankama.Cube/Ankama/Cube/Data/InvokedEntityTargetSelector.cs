// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.InvokedEntityTargetSelector
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
  public sealed class InvokedEntityTargetSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector
  {
    public override string ToString() => this.GetType().Name;

    public static InvokedEntityTargetSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (InvokedEntityTargetSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      InvokedEntityTargetSelector entityTargetSelector = new InvokedEntityTargetSelector();
      entityTargetSelector.PopulateFromJson(jsonObject);
      return entityTargetSelector;
    }

    public static InvokedEntityTargetSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      InvokedEntityTargetSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : InvokedEntityTargetSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context) => throw new NotImplementedException();

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity => throw new NotImplementedException();
  }
}
