﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ActivateFloorMechanismEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ActivateFloorMechanismEffectDefinition : EffectExecutionDefinition
  {
    public override string ToString() => this.GetType().Name;

    public static ActivateFloorMechanismEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ActivateFloorMechanismEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ActivateFloorMechanismEffectDefinition effectDefinition = new ActivateFloorMechanismEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ActivateFloorMechanismEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ActivateFloorMechanismEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ActivateFloorMechanismEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
