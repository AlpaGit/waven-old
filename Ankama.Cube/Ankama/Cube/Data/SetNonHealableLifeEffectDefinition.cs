// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SetNonHealableLifeEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SetNonHealableLifeEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_newValue;
    private ISingleEntitySelector m_source;

    public DynamicValue newValue => this.m_newValue;

    public ISingleEntitySelector source => this.m_source;

    public override string ToString() => this.GetType().Name;

    public static SetNonHealableLifeEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SetNonHealableLifeEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SetNonHealableLifeEffectDefinition effectDefinition = new SetNonHealableLifeEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static SetNonHealableLifeEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SetNonHealableLifeEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SetNonHealableLifeEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_newValue = DynamicValue.FromJsonProperty(jsonObject, "newValue");
      this.m_source = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "source");
    }
  }
}
