// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BounceEntitiesSelector
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
  public sealed class BounceEntitiesSelector : IEditableContent, ITargetSelector, IEntitySelector
  {
    private ISingleEntitySelector m_start;
    private bool m_includeStart;
    private List<IEntityFilter> m_bounceFilters;

    public ISingleEntitySelector start => this.m_start;

    public bool includeStart => this.m_includeStart;

    public IReadOnlyList<IEntityFilter> bounceFilters => (IReadOnlyList<IEntityFilter>) this.m_bounceFilters;

    public override string ToString() => this.GetType().Name;

    public static BounceEntitiesSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (BounceEntitiesSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      BounceEntitiesSelector entitiesSelector = new BounceEntitiesSelector();
      entitiesSelector.PopulateFromJson(jsonObject);
      return entitiesSelector;
    }

    public static BounceEntitiesSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      BounceEntitiesSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : BounceEntitiesSelector.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_start = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "start");
      this.m_includeStart = Serialization.JsonTokenValue<bool>(jsonObject, "includeStart", true);
      JArray jarray = Serialization.JsonArray(jsonObject, "bounceFilters");
      this.m_bounceFilters = new List<IEntityFilter>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_bounceFilters.Add(IEntityFilterUtils.FromJsonToken(token));
    }

    public IEnumerable<IEntity> EnumerateEntities(DynamicValueContext context)
    {
      yield break;
    }
  }
}
