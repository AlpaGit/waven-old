// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OwnerFilter
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
  public sealed class OwnerFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private bool m_isIdentical;
    private ISingleEntitySelector m_reference;
    public static readonly OwnerFilter sameAsCaster = new OwnerFilter()
    {
      m_isIdentical = true,
      m_reference = (ISingleEntitySelector) new CasterSelector()
    };

    public bool isIdentical => this.m_isIdentical;

    public ISingleEntitySelector reference => this.m_reference;

    public override string ToString() => (this.m_isIdentical ? "" : "not ") + string.Format("same owner as {0}", (object) this.m_reference);

    public static OwnerFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OwnerFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OwnerFilter ownerFilter = new OwnerFilter();
      ownerFilter.PopulateFromJson(jsonObject);
      return ownerFilter;
    }

    public static OwnerFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OwnerFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OwnerFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_isIdentical = Serialization.JsonTokenValue<bool>(jsonObject, "isIdentical");
      this.m_reference = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "reference");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      IEntityWithOwner entity1;
      if (this.m_reference.TryGetEntity<IEntityWithOwner>(context, out entity1))
      {
        int referenceOwnerId = entity1.ownerId;
        foreach (IEntity entity2 in entities)
        {
          if (entity2 is IEntityWithOwner entityWithOwner && entityWithOwner.ownerId == referenceOwnerId == this.isIdentical)
            yield return entity2;
        }
      }
    }
  }
}
