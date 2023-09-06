// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ConstIntegerValue
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
  public sealed class ConstIntegerValue : 
    DynamicValue,
    ILevelOnlyDependant,
    IEditableContent,
    IReferenceableContent
  {
    private string m_referenceId;
    private int m_value;

    public string referenceId => this.m_referenceId;

    public int value => this.m_value;

    public override string ToString() => string.Format("{0}", (object) this.value);

    public static ConstIntegerValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ConstIntegerValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ConstIntegerValue constIntegerValue = new ConstIntegerValue();
      constIntegerValue.PopulateFromJson(jsonObject);
      return constIntegerValue;
    }

    public static ConstIntegerValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ConstIntegerValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ConstIntegerValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_referenceId = Serialization.JsonTokenValue<string>(jsonObject, "referenceId", "");
      this.m_value = Serialization.JsonTokenValue<int>(jsonObject, "value");
    }

    public override bool GetValue(DynamicValueContext context, out int val)
    {
      val = this.m_value;
      return true;
    }

    public int GetValueWithLevel(int level) => this.m_value;
  }
}
