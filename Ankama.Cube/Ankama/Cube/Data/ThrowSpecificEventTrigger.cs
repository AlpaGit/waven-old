// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ThrowSpecificEventTrigger
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
  public sealed class ThrowSpecificEventTrigger : EffectExecutionDefinition
  {
    private SpecificEventTrigger m_trigger;

    public SpecificEventTrigger trigger => this.m_trigger;

    public override string ToString() => this.GetType().Name;

    public static ThrowSpecificEventTrigger FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ThrowSpecificEventTrigger) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ThrowSpecificEventTrigger specificEventTrigger = new ThrowSpecificEventTrigger();
      specificEventTrigger.PopulateFromJson(jsonObject);
      return specificEventTrigger;
    }

    public static ThrowSpecificEventTrigger FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ThrowSpecificEventTrigger defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ThrowSpecificEventTrigger.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_trigger = (SpecificEventTrigger) Serialization.JsonTokenValue<int>(jsonObject, "trigger");
    }
  }
}
