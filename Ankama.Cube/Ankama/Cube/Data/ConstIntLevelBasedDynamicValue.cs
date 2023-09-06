// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ConstIntLevelBasedDynamicValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class ConstIntLevelBasedDynamicValue : 
    LevelBasedDynamicValue,
    ILevelOnlyDependant,
    IEditableContent,
    IReferenceableContent
  {
    private string m_referenceId;
    private List<int> m_values;

    public string referenceId => this.m_referenceId;

    public IReadOnlyList<int> values => (IReadOnlyList<int>) this.m_values;

    public override string ToString()
    {
      int count = this.m_values.Count;
      return count == 0 ? "No values...." : string.Format("{0} .. {1}", (object) this.m_values[0], (object) this.m_values[count - 1]);
    }

    public static ConstIntLevelBasedDynamicValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ConstIntLevelBasedDynamicValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ConstIntLevelBasedDynamicValue basedDynamicValue = new ConstIntLevelBasedDynamicValue();
      basedDynamicValue.PopulateFromJson(jsonObject);
      return basedDynamicValue;
    }

    public static ConstIntLevelBasedDynamicValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ConstIntLevelBasedDynamicValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ConstIntLevelBasedDynamicValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_referenceId = Serialization.JsonTokenValue<string>(jsonObject, "referenceId", "");
      this.m_values = Serialization.JsonArrayAsList<int>(jsonObject, "values");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      int level = context != null ? context.level : 1;
      value = this.GetValue(level);
      return true;
    }

    public int GetValueWithLevel(int level) => this.GetValue(level);

    private int GetValue(int level) => this.m_values[level - 1];
  }
}
