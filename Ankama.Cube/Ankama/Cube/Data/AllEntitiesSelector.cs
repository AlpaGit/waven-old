// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AllEntitiesSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class AllEntitiesSelector : EntitySelectorForCast
  {
    public override string ToString() => this.GetType().Name;

    public static AllEntitiesSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AllEntitiesSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AllEntitiesSelector entitiesSelector = new AllEntitiesSelector();
      entitiesSelector.PopulateFromJson(jsonObject);
      return entitiesSelector;
    }

    public static AllEntitiesSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AllEntitiesSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AllEntitiesSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context) => !(context is DynamicValueFightContext valueFightContext) ? Enumerable.Empty<IEntity>() : valueFightContext.fightStatus.EnumerateEntities();
  }
}
