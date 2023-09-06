// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellElementsFilter
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
  public sealed class SpellElementsFilter : SpellFilter
  {
    private ShouldBeInOrNot m_condition;
    private List<Element> m_elements;

    public ShouldBeInOrNot condition => this.m_condition;

    public IReadOnlyList<Element> elements => (IReadOnlyList<Element>) this.m_elements;

    public override string ToString()
    {
      switch (this.m_elements.Count)
      {
        case 0:
          return "elements is <unset>";
        case 1:
          return string.Format("elements {0} {1}", (object) this.condition, (object) this.m_elements[0]);
        default:
          return string.Format("elements {0} \n - ", (object) this.condition) + string.Join<Element>("\n - ", (IEnumerable<Element>) this.m_elements);
      }
    }

    public static SpellElementsFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellElementsFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellElementsFilter spellElementsFilter = new SpellElementsFilter();
      spellElementsFilter.PopulateFromJson(jsonObject);
      return spellElementsFilter;
    }

    public static SpellElementsFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellElementsFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellElementsFilter.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_condition = (ShouldBeInOrNot) Serialization.JsonTokenValue<int>(jsonObject, "condition", 1);
      this.m_elements = Serialization.JsonArrayAsList<Element>(jsonObject, "elements");
    }

    public override bool Accept(int spellInstanceId, SpellDefinition spellDef)
    {
      Element element = spellDef.element;
      for (int index = 0; index < this.m_elements.Count; ++index)
      {
        if (this.m_elements[index] == element)
          return this.m_condition == ShouldBeInOrNot.ShouldBeIn;
      }
      return this.m_condition == ShouldBeInOrNot.ShouldNotBeIn;
    }
  }
}
