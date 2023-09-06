// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.MultipleCastTargetDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class MultipleCastTargetDefinition : IEditableContent, ICastTargetDefinition
  {
    private List<ISelectorForCast> m_selectors;

    public IReadOnlyList<ISelectorForCast> selectors => (IReadOnlyList<ISelectorForCast>) this.m_selectors;

    public override string ToString() => string.Format("{0} targets:\n - ", (object) this.m_selectors.Count) + string.Join<ISelectorForCast>("\n - ", (IEnumerable<ISelectorForCast>) this.selectors);

    public static MultipleCastTargetDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (MultipleCastTargetDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      MultipleCastTargetDefinition targetDefinition = new MultipleCastTargetDefinition();
      targetDefinition.PopulateFromJson(jsonObject);
      return targetDefinition;
    }

    public static MultipleCastTargetDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      MultipleCastTargetDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : MultipleCastTargetDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      JArray jarray = Serialization.JsonArray(jsonObject, "selectors");
      this.m_selectors = new List<ISelectorForCast>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_selectors.Add(ISelectorForCastUtils.FromJsonToken(token));
    }

    public CastTargetContext CreateCastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int definitionId,
      int level,
      int instanceId)
    {
      int count = this.m_selectors.Count;
      return (CastTargetContext) new MultipleCastTargetContext(fightStatus, playerId, type, definitionId, level, instanceId, count);
    }

    public int count => this.m_selectors.Count;

    public IEnumerable<Target> EnumerateTargets(CastTargetContext castTargetContext) => this.m_selectors[castTargetContext.selectedTargetCount].EnumerateTargets((DynamicValueContext) castTargetContext);
  }
}
