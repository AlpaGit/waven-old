// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TeamFilter
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
  public sealed class TeamFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private bool m_isIdentical;
    private ISingleEntitySelector m_reference;

    public bool isIdentical => this.m_isIdentical;

    public ISingleEntitySelector reference => this.m_reference;

    public override string ToString() => (this.m_isIdentical ? "" : "not ") + string.Format("same team as {0}", (object) this.m_reference);

    public static TeamFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TeamFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TeamFilter teamFilter = new TeamFilter();
      teamFilter.PopulateFromJson(jsonObject);
      return teamFilter;
    }

    public static TeamFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TeamFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TeamFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_isIdentical = Serialization.JsonTokenValue<bool>(jsonObject, "isIdentical");
      this.m_reference = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "reference");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      IEntityWithTeam entity1;
      if (this.m_reference.TryGetEntity<IEntityWithTeam>(context, out entity1))
      {
        int referenceTeamIndex = entity1.teamIndex;
        foreach (IEntity entity2 in entities)
        {
          if (entity2 is IEntityWithTeam entityWithTeam && entityWithTeam.teamIndex == referenceTeamIndex == this.isIdentical)
            yield return entity2;
        }
      }
    }
  }
}
