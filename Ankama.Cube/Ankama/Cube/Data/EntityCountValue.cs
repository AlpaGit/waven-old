// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EntityCountValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.EntityAddedOrRemoved})]
  [Serializable]
  public sealed class EntityCountValue : DynamicValue
  {
    private List<IEntityFilter> m_entityFilters;

    public IReadOnlyList<IEntityFilter> entityFilters => (IReadOnlyList<IEntityFilter>) this.m_entityFilters;

    public override string ToString() => "<filtered entities count>";

    public static EntityCountValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EntityCountValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EntityCountValue entityCountValue = new EntityCountValue();
      entityCountValue.PopulateFromJson(jsonObject);
      return entityCountValue;
    }

    public static EntityCountValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EntityCountValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EntityCountValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "entityFilters");
      this.m_entityFilters = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_entityFilters.Add(IEntityFilterUtils.FromJsonToken(token));
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (context is DynamicValueFightContext valueFightContext)
      {
        IEnumerable<IEntity> entities = valueFightContext.fightStatus.EnumerateEntities();
        for (int index = 0; index < this.m_entityFilters.Count; ++index)
          entities = this.m_entityFilters[index].Filter(entities, context);
        value = entities.Count<IEntity>();
        return true;
      }
      value = 0;
      return false;
    }
  }
}
