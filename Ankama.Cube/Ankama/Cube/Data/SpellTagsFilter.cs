// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellTagsFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpellTagsFilter : SpellFilter
  {
    private ListComparison m_comparison;
    private List<SpellTag> m_spellTags;

    public ListComparison comparison => this.m_comparison;

    public IReadOnlyList<SpellTag> spellTags => (IReadOnlyList<SpellTag>) this.m_spellTags;

    public override string ToString()
    {
      string str;
      switch (this.m_spellTags.Count)
      {
        case 0:
          str = "<unset>";
          break;
        case 1:
          str = string.Format("{0}", (object) this.m_spellTags[0]);
          break;
        default:
          str = "\n - " + string.Join<SpellTag>("\n - ", (IEnumerable<SpellTag>) this.m_spellTags);
          break;
      }
      return string.Format("{0} {1}", (object) this.m_comparison, (object) str);
    }

    public static SpellTagsFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellTagsFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellTagsFilter spellTagsFilter = new SpellTagsFilter();
      spellTagsFilter.PopulateFromJson(jsonObject);
      return spellTagsFilter;
    }

    public static SpellTagsFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellTagsFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellTagsFilter.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_comparison = (ListComparison) Serialization.JsonTokenValue<int>(jsonObject, "comparison", 1);
      this.m_spellTags = Serialization.JsonArrayAsList<SpellTag>(jsonObject, "spellTags");
    }

    public override bool Accept(int spellInstanceId, SpellDefinition spellDef) => ListComparisonUtility.ValidateCondition<SpellTag>((IReadOnlyCollection<SpellTag>) spellDef.tags, this.m_comparison, (IReadOnlyList<SpellTag>) this.m_spellTags);
  }
}
