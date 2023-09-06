// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ChargeEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ChargeEffectDefinition : EffectExecutionDefinition
  {
    private ISingleEntitySelector m_direction;
    private DynamicValue m_cellCount;
    private DynamicValue m_attackValue;
    private DynamicValue m_attackBoostByCell;

    public ISingleEntitySelector direction => this.m_direction;

    public DynamicValue cellCount => this.m_cellCount;

    public DynamicValue attackValue => this.m_attackValue;

    public DynamicValue attackBoostByCell => this.m_attackBoostByCell;

    public override string ToString() => string.Format("Charge up to {0} cells and attacks{1}", (object) this.cellCount, this.m_condition == null ? (object) "" : (object) string.Format(" if {0}", (object) this.m_condition));

    public static ChargeEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ChargeEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ChargeEffectDefinition effectDefinition = new ChargeEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ChargeEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ChargeEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ChargeEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_direction = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "direction");
      this.m_cellCount = DynamicValue.FromJsonProperty(jsonObject, "cellCount");
      this.m_attackValue = DynamicValue.FromJsonProperty(jsonObject, "attackValue");
      this.m_attackBoostByCell = DynamicValue.FromJsonProperty(jsonObject, "attackBoostByCell");
    }
  }
}
