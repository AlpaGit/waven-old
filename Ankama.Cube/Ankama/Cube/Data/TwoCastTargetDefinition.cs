// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TwoCastTargetDefinition
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
  public sealed class TwoCastTargetDefinition : IEditableContent, ICastTargetDefinition
  {
    private ISelectorForCast m_selector1;
    private ISelectorForCast m_selector2;

    public ISelectorForCast selector1 => this.m_selector1;

    public ISelectorForCast selector2 => this.m_selector2;

    public override string ToString() => string.Format("{0} THEN\n {1}", (object) this.selector1, (object) this.selector2);

    public static TwoCastTargetDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (TwoCastTargetDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      TwoCastTargetDefinition targetDefinition = new TwoCastTargetDefinition();
      targetDefinition.PopulateFromJson(jsonObject);
      return targetDefinition;
    }

    public static TwoCastTargetDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      TwoCastTargetDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : TwoCastTargetDefinition.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_selector1 = ISelectorForCastUtils.FromJsonProperty(jsonObject, "selector1");
      this.m_selector2 = ISelectorForCastUtils.FromJsonProperty(jsonObject, "selector2");
    }

    public CastTargetContext CreateCastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int definitionId,
      int level,
      int instanceId)
    {
      return (CastTargetContext) new TwoCastTargetContext(fightStatus, playerId, type, definitionId, level, instanceId);
    }

    public int count => 2;

    public IEnumerable<Target> EnumerateTargets(CastTargetContext castTargetContext) => (castTargetContext.selectedTargetCount == 0 ? this.m_selector1 : this.m_selector2).EnumerateTargets((DynamicValueContext) castTargetContext);
  }
}
