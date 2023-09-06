// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.EffectApplicationDefinition
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
  public sealed class EffectApplicationDefinition : AbstractEffectApplicationDefinition
  {
    private ITargetSelector m_applicationTargetSelector;
    private List<EffectTrigger> m_applicationEndTriggers;
    private List<EffectTrigger> m_executionStartTriggers;
    private EffectCondition m_condition;

    public ITargetSelector applicationTargetSelector => this.m_applicationTargetSelector;

    public IReadOnlyList<EffectTrigger> applicationEndTriggers => (IReadOnlyList<EffectTrigger>) this.m_applicationEndTriggers;

    public IReadOnlyList<EffectTrigger> executionStartTriggers => (IReadOnlyList<EffectTrigger>) this.m_executionStartTriggers;

    public EffectCondition condition => this.m_condition;

    public override string ToString() => string.Format("on {0} when {1}", (object) this.applicationTargetSelector, (object) string.Join<EffectTrigger>(" or ", (IEnumerable<EffectTrigger>) this.m_executionStartTriggers));

    public static EffectApplicationDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (EffectApplicationDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      EffectApplicationDefinition applicationDefinition = new EffectApplicationDefinition();
      applicationDefinition.PopulateFromJson(jsonObject);
      return applicationDefinition;
    }

    public static EffectApplicationDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      EffectApplicationDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : EffectApplicationDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_applicationTargetSelector = ITargetSelectorUtils.FromJsonProperty(jsonObject, "applicationTargetSelector");
      JArray jarray1 = Serialization.JsonArray(jsonObject, "applicationEndTriggers");
      this.m_applicationEndTriggers = new List<EffectTrigger>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_applicationEndTriggers.Add(EffectTrigger.FromJsonToken(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "executionStartTriggers");
      this.m_executionStartTriggers = new List<EffectTrigger>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 != null)
      {
        foreach (JToken token in jarray2)
          this.m_executionStartTriggers.Add(EffectTrigger.FromJsonToken(token));
      }
      this.m_condition = EffectCondition.FromJsonProperty(jsonObject, "condition");
    }
  }
}
