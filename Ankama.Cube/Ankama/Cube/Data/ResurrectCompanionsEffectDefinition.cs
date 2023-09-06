// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ResurrectCompanionsEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ResurrectCompanionsEffectDefinition : EffectExecutionDefinition
  {
    public override string ToString() => this.GetType().Name;

    public static ResurrectCompanionsEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ResurrectCompanionsEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ResurrectCompanionsEffectDefinition effectDefinition = new ResurrectCompanionsEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ResurrectCompanionsEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ResurrectCompanionsEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ResurrectCompanionsEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
