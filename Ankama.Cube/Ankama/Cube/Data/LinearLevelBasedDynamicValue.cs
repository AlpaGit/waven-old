// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.LinearLevelBasedDynamicValue
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
  public sealed class LinearLevelBasedDynamicValue : 
    LevelBasedDynamicValue,
    ILevelOnlyDependant,
    IEditableContent,
    IReferenceableContent
  {
    private string m_referenceId;
    private int m_baseValue;
    private float m_factor;

    public string referenceId => this.m_referenceId;

    public int baseValue => this.m_baseValue;

    public float factor => this.m_factor;

    public override string ToString() => (double) this.m_factor == 1.0 ? (this.m_baseValue <= 0 ? string.Format("level {0}", (object) this.m_baseValue) : string.Format("level+{0}", (object) this.m_baseValue)) : (this.m_baseValue <= 0 ? string.Format("{0} * level {1}", (object) this.m_factor, (object) this.m_baseValue) : string.Format("{0} * level +{1}", (object) this.m_factor, (object) this.m_baseValue));

    public static LinearLevelBasedDynamicValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (LinearLevelBasedDynamicValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      LinearLevelBasedDynamicValue basedDynamicValue = new LinearLevelBasedDynamicValue();
      basedDynamicValue.PopulateFromJson(jsonObject);
      return basedDynamicValue;
    }

    public static LinearLevelBasedDynamicValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      LinearLevelBasedDynamicValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : LinearLevelBasedDynamicValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_referenceId = Serialization.JsonTokenValue<string>(jsonObject, "referenceId", "");
      this.m_baseValue = Serialization.JsonTokenValue<int>(jsonObject, "baseValue");
      this.m_factor = Serialization.JsonTokenValue<float>(jsonObject, "factor");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      int num = context != null ? context.level : 1;
      value = (int) Math.Round((double) this.baseValue + (double) num * (double) this.factor);
      return true;
    }

    public int GetValueWithLevel(int level) => (int) Math.Round((double) this.baseValue + (double) level * (double) this.factor);
  }
}
