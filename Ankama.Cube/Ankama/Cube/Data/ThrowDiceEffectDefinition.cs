// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ThrowDiceEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ThrowDiceEffectDefinition : EffectExecutionDefinition
  {
    private DynamicValue m_dice;
    private ISingleEntitySelector m_thrower;

    public DynamicValue dice => this.m_dice;

    public ISingleEntitySelector thrower => this.m_thrower;

    public override string ToString() => string.Format("Throw dice {0}{1}", (object) this.dice, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static ThrowDiceEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ThrowDiceEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ThrowDiceEffectDefinition effectDefinition = new ThrowDiceEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ThrowDiceEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ThrowDiceEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ThrowDiceEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_dice = DynamicValue.FromJsonProperty(jsonObject, "dice");
      this.m_thrower = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "thrower");
    }
  }
}
