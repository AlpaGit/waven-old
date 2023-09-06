// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.RegisterDamageProtectorEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class RegisterDamageProtectorEffectDefinition : EffectExecutionWithDurationDefinition
  {
    private ISingleEntitySelector m_protector;
    private DynamicValue m_fixedProtectionValue;
    private DynamicValue m_damagePercentProtectionValue;

    public ISingleEntitySelector protector => this.m_protector;

    public DynamicValue fixedProtectionValue => this.m_fixedProtectionValue;

    public DynamicValue damagePercentProtectionValue => this.m_damagePercentProtectionValue;

    public override string ToString() => this.GetType().Name;

    public static RegisterDamageProtectorEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (RegisterDamageProtectorEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      RegisterDamageProtectorEffectDefinition effectDefinition = new RegisterDamageProtectorEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static RegisterDamageProtectorEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      RegisterDamageProtectorEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : RegisterDamageProtectorEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_protector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "protector");
      this.m_fixedProtectionValue = DynamicValue.FromJsonProperty(jsonObject, "fixedProtectionValue");
      this.m_damagePercentProtectionValue = DynamicValue.FromJsonProperty(jsonObject, "damagePercentProtectionValue");
    }
  }
}
