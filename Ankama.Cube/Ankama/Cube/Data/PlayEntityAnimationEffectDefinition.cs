// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PlayEntityAnimationEffectDefinition
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
  public sealed class PlayEntityAnimationEffectDefinition : EffectExecutionDefinition
  {
    private EntityAnimationKey m_animation;
    private List<ISingleTargetSelector> m_additionalCoords;

    public EntityAnimationKey animation => this.m_animation;

    public IReadOnlyList<ISingleTargetSelector> additionalCoords => (IReadOnlyList<ISingleTargetSelector>) this.m_additionalCoords;

    public override string ToString() => this.GetType().Name;

    public static PlayEntityAnimationEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (PlayEntityAnimationEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      PlayEntityAnimationEffectDefinition effectDefinition = new PlayEntityAnimationEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static PlayEntityAnimationEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      PlayEntityAnimationEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : PlayEntityAnimationEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_animation = (EntityAnimationKey) Serialization.JsonTokenValue<int>(jsonObject, "animation", 1);
      JArray jarray = Serialization.JsonArray(jsonObject, "additionalCoords");
      this.m_additionalCoords = new List<ISingleTargetSelector>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_additionalCoords.Add(ISingleTargetSelectorUtils.FromJsonToken(token));
    }
  }
}
