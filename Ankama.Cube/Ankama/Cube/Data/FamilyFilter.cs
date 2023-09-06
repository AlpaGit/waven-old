// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FamilyFilter
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
  public sealed class FamilyFilter : IEditableContent, IEntityFilter, ITargetFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Family> m_families;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Family> families => (IReadOnlyList<Family>) this.m_families;

    public override string ToString()
    {
      switch (this.m_families.Count)
      {
        case 0:
          return "family <unset>";
        case 1:
          return string.Format("family {0} {1}", (object) this.condition, (object) this.m_families[0]);
        default:
          return string.Format("family {0} \n - ", (object) this.condition) + string.Join<Family>("\n - ", (IEnumerable<Family>) this.m_families);
      }
    }

    public static FamilyFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FamilyFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FamilyFilter familyFilter = new FamilyFilter();
      familyFilter.PopulateFromJson(jsonObject);
      return familyFilter;
    }

    public static FamilyFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FamilyFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FamilyFilter.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      this.m_families = Serialization.JsonArrayAsList<Family>(jsonObject, "families");
    }

    public IEnumerable<IEntity> Filter(IEnumerable<IEntity> entities, DynamicValueContext context)
    {
      bool shouldContains = this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      foreach (IEntity entity in entities)
      {
        if (entity is IEntityWithFamilies entityWithFamilies && FamilyFilter.FamiliesIntersects(this.m_families, entityWithFamilies.families) == shouldContains)
          yield return entity;
      }
    }

    private static bool FamiliesIntersects(List<Family> families, IReadOnlyList<Family> others)
    {
      int count = families.Count;
      foreach (Family other in (IEnumerable<Family>) others)
      {
        for (int index = 0; index < count; ++index)
        {
          if (other == families[index])
            return true;
        }
      }
      return false;
    }
  }
}
