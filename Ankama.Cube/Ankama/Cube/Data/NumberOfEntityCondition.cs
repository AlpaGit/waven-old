// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.NumberOfEntityCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class NumberOfEntityCondition : EffectCondition
  {
    private FilteredEntitySelector m_selector;
    private ValueFilter m_value;

    public FilteredEntitySelector selector => this.m_selector;

    public ValueFilter value => this.m_value;

    public override string ToString() => string.Format("number of entities {0}", (object) this.m_value);

    public static NumberOfEntityCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (NumberOfEntityCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      NumberOfEntityCondition ofEntityCondition = new NumberOfEntityCondition();
      ofEntityCondition.PopulateFromJson(jsonObject);
      return ofEntityCondition;
    }

    public static NumberOfEntityCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      NumberOfEntityCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : NumberOfEntityCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selector = FilteredEntitySelector.FromJsonProperty(jsonObject, "selector", (FilteredEntitySelector) null);
      this.m_value = ValueFilter.FromJsonProperty(jsonObject, "value");
    }

    public override bool IsValid(DynamicValueContext context)
    {
      int num = 0;
      foreach (IEntity enumerateEntity in this.selector.EnumerateEntities(context))
        ++num;
      return this.value.Matches(num, context);
    }
  }
}
