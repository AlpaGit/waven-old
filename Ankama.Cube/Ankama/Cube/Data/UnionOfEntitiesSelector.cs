// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UnionOfEntitiesSelector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class UnionOfEntitiesSelector : EntitySelectorForCast
  {
    private IEntitySelector m_first;
    private IEntitySelector m_second;

    public IEntitySelector first => this.m_first;

    public IEntitySelector second => this.m_second;

    public override string ToString() => string.Format("({0} OR {1})", (object) this.m_first, (object) this.m_second);

    public static UnionOfEntitiesSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (UnionOfEntitiesSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      UnionOfEntitiesSelector entitiesSelector = new UnionOfEntitiesSelector();
      entitiesSelector.PopulateFromJson(jsonObject);
      return entitiesSelector;
    }

    public static UnionOfEntitiesSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      UnionOfEntitiesSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : UnionOfEntitiesSelector.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_first = IEntitySelectorUtils.FromJsonProperty(jsonObject, "first");
      this.m_second = IEntitySelectorUtils.FromJsonProperty(jsonObject, "second");
    }

    public override IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      HashSet<IEntity> entitiesSet = new HashSet<IEntity>();
      foreach (IEntity e in this.m_first.EnumerateEntities(context))
      {
        yield return e;
        entitiesSet.Add(e);
      }
      foreach (IEntity enumerateEntity in this.m_second.EnumerateEntities(context))
      {
        if (entitiesSet.Add(enumerateEntity))
          yield return enumerateEntity;
      }
    }
  }
}
