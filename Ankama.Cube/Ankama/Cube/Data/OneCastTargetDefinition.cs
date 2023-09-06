// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.OneCastTargetDefinition
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
  public sealed class OneCastTargetDefinition : IEditableContent, ICastTargetDefinition
  {
    private ISelectorForCast m_selector;

    public ISelectorForCast selector => this.m_selector;

    public override string ToString() => this.selector.ToString();

    public static OneCastTargetDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (OneCastTargetDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      OneCastTargetDefinition targetDefinition = new OneCastTargetDefinition();
      targetDefinition.PopulateFromJson(jsonObject);
      return targetDefinition;
    }

    public static OneCastTargetDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      OneCastTargetDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : OneCastTargetDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject) => this.m_selector = ISelectorForCastUtils.FromJsonProperty(jsonObject, "selector");

    public CastTargetContext CreateCastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int definitionId,
      int level,
      int instanceId)
    {
      return (CastTargetContext) new OneCastTargetContext(fightStatus, playerId, type, definitionId, level, instanceId);
    }

    public int count => 1;

    public IEnumerable<Target> EnumerateTargets(CastTargetContext castTargetContext) => this.m_selector.EnumerateTargets((DynamicValueContext) castTargetContext);
  }
}
