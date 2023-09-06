// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OsamodasAnimalsEntityFilter
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
  public sealed class OsamodasAnimalsEntityFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    public override string ToString() => this.GetType().Name;

    public static OsamodasAnimalsEntityFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OsamodasAnimalsEntityFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OsamodasAnimalsEntityFilter animalsEntityFilter = new OsamodasAnimalsEntityFilter();
      animalsEntityFilter.PopulateFromJson(jsonObject);
      return animalsEntityFilter;
    }

    public static OsamodasAnimalsEntityFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OsamodasAnimalsEntityFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OsamodasAnimalsEntityFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      foreach (IEntity entity in entities)
      {
        if (entity is SummoningStatus)
          yield return entity;
      }
    }
  }
}
