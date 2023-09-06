// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ResetActionEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ResetActionEffectDefinition : EffectExecutionDefinition
  {
    public override string ToString() => "Make action available again" + (this.m_condition == null ? "" : string.Format(" if {0}", (object) this.m_condition));

    public static ResetActionEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ResetActionEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ResetActionEffectDefinition effectDefinition = new ResetActionEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ResetActionEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ResetActionEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ResetActionEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
