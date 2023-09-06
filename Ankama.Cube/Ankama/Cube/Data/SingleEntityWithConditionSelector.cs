// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SingleEntityWithConditionSelector
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
  [Serializable]
  public sealed class SingleEntityWithConditionSelector : 
    IEditableContent,
    ITargetSelector,
    ISingleEntitySelector,
    IEntitySelector,
    ISingleTargetSelector,
    ISingleTargetWithFiltersSelector
  {
    private ISingleEntitySelector m_from;
    private List<IEntityFilter> m_onlyIf;

    public ISingleEntitySelector from => this.m_from;

    public IReadOnlyList<IEntityFilter> onlyIf => (IReadOnlyList<IEntityFilter>) this.m_onlyIf;

    public override string ToString() => this.GetType().Name;

    public static SingleEntityWithConditionSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SingleEntityWithConditionSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SingleEntityWithConditionSelector conditionSelector = new SingleEntityWithConditionSelector();
      conditionSelector.PopulateFromJson(jsonObject);
      return conditionSelector;
    }

    public static SingleEntityWithConditionSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SingleEntityWithConditionSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SingleEntityWithConditionSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_from = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "from");
      JArray jarray = Serialization.JsonArray(jsonObject, "onlyIf");
      this.m_onlyIf = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_onlyIf.Add(IEntityFilterUtils.FromJsonToken(token));
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      if (!(context is DynamicValueFightContext valueFightContext))
        return Enumerable.Empty<IEntity>();
      IEnumerable<IEntity> entities = valueFightContext.fightStatus.EnumerateEntities();
      int count = this.m_onlyIf.Count;
      for (int index = 0; index < count; ++index)
        entities = this.m_onlyIf[index].Filter(entities, context);
      return entities;
    }

    public bool TryGetEntity<T>(DynamicValueContext context, out T entity) where T : class, IEntity => throw new NotImplementedException();
  }
}
