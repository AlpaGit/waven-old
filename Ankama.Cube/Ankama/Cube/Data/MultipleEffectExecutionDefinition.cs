// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MultipleEffectExecutionDefinition
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
  public sealed class MultipleEffectExecutionDefinition : AbstractEffectExecutionDefinition
  {
    private List<AbstractEffectExecutionDefinition> m_executions;

    public IReadOnlyList<AbstractEffectExecutionDefinition> executions => (IReadOnlyList<AbstractEffectExecutionDefinition>) this.m_executions;

    public override string ToString() => "Multiple Executions\n - " + string.Join<AbstractEffectExecutionDefinition>("\n - ", (IEnumerable<AbstractEffectExecutionDefinition>) this.executions);

    public static MultipleEffectExecutionDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (MultipleEffectExecutionDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      MultipleEffectExecutionDefinition executionDefinition = new MultipleEffectExecutionDefinition();
      executionDefinition.PopulateFromJson(jsonObject);
      return executionDefinition;
    }

    public static MultipleEffectExecutionDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      MultipleEffectExecutionDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : MultipleEffectExecutionDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "executions");
      this.m_executions = new List<AbstractEffectExecutionDefinition>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_executions.Add(AbstractEffectExecutionDefinition.FromJsonToken(token));
    }
  }
}
