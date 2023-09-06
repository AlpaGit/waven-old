// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OwnerOfSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class OwnerOfSelector : EntitySelectorForCast
  {
    private IEntitySelector m_selector;

    public IEntitySelector selector => this.m_selector;

    public override string ToString() => string.Format("owner of {0}", (object) this.selector);

    public static OwnerOfSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OwnerOfSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OwnerOfSelector ownerOfSelector = new OwnerOfSelector();
      ownerOfSelector.PopulateFromJson(jsonObject);
      return ownerOfSelector;
    }

    public static OwnerOfSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OwnerOfSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OwnerOfSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selector = IEntitySelectorUtils.FromJsonProperty(jsonObject, "selector");
    }

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      if (context is DynamicValueFightContext fightContext)
      {
        List<int> ownerIds = ListPool<int>.Get(2);
        foreach (IEntity enumerateEntity in this.m_selector.EnumerateEntities(context))
        {
          if (enumerateEntity is IEntityWithOwner entityWithOwner && !ownerIds.Contains(entityWithOwner.ownerId))
          {
            ownerIds.Add(entityWithOwner.ownerId);
            IEntity entityStatus;
            if (fightContext.fightStatus.TryGetEntity<IEntity>(entityWithOwner.ownerId, out entityStatus))
              yield return entityStatus;
          }
        }
        ListPool<int>.Release(ownerIds);
      }
    }
  }
}
