// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReturnCompanionToHandEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ReturnCompanionToHandEffectDefinition : EffectExecutionDefinition
  {
    public override string ToString() => this.GetType().Name;

    public static ReturnCompanionToHandEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ReturnCompanionToHandEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ReturnCompanionToHandEffectDefinition effectDefinition = new ReturnCompanionToHandEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ReturnCompanionToHandEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ReturnCompanionToHandEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ReturnCompanionToHandEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
