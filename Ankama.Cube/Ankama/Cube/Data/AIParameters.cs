// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AIParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class AIParameters : IEditableContent
  {
    private int m_playingOrder;
    private IAIActionTargets m_actionTargets;
    private AIMovementDefinition m_moveIfNoValidTarget;
    private AIMovementDefinition m_moveIfNoTarget;

    public int playingOrder => this.m_playingOrder;

    public IAIActionTargets actionTargets => this.m_actionTargets;

    public AIMovementDefinition moveIfNoValidTarget => this.m_moveIfNoValidTarget;

    public AIMovementDefinition moveIfNoTarget => this.m_moveIfNoTarget;

    public override string ToString() => this.GetType().Name;

    public static AIParameters FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AIParameters) null;
      }
      JObject jsonObject = token.Value<JObject>();
      AIParameters aiParameters = new AIParameters();
      aiParameters.PopulateFromJson(jsonObject);
      return aiParameters;
    }

    public static AIParameters FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AIParameters defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AIParameters.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_playingOrder = Serialization.JsonTokenValue<int>(jsonObject, "playingOrder");
      this.m_actionTargets = IAIActionTargetsUtils.FromJsonProperty(jsonObject, "actionTargets");
      this.m_moveIfNoValidTarget = AIMovementDefinition.FromJsonProperty(jsonObject, "moveIfNoValidTarget");
      this.m_moveIfNoTarget = AIMovementDefinition.FromJsonProperty(jsonObject, "moveIfNoTarget");
    }
  }
}
