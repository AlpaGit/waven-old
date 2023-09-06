// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DynamicValueLevelBasedDynamicValue
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
  public sealed class DynamicValueLevelBasedDynamicValue : LevelBasedDynamicValue
  {
    private List<DynamicValue> m_values;

    public IReadOnlyList<DynamicValue> values => (IReadOnlyList<DynamicValue>) this.m_values;

    public override string ToString()
    {
      int count = this.m_values.Count;
      return count == 0 ? "No values...." : string.Format("{0} .. {1}", (object) this.m_values[0], (object) this.m_values[count - 1]);
    }

    public static DynamicValueLevelBasedDynamicValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DynamicValueLevelBasedDynamicValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DynamicValueLevelBasedDynamicValue basedDynamicValue = new DynamicValueLevelBasedDynamicValue();
      basedDynamicValue.PopulateFromJson(jsonObject);
      return basedDynamicValue;
    }

    public static DynamicValueLevelBasedDynamicValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DynamicValueLevelBasedDynamicValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DynamicValueLevelBasedDynamicValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray = Serialization.JsonArray(jsonObject, "values");
      this.m_values = new List<DynamicValue>(jarray != null ? jarray.Count : 0);
      if (jarray == null)
        return;
      foreach (JToken token in jarray)
        this.m_values.Add(DynamicValue.FromJsonToken(token));
    }

    public override bool GetValue(DynamicValueContext context, out int value) => this.GetDynamicValue(context != null ? context.level : 1).GetValue(context, out value);

    public DynamicValue GetDynamicValue(int level) => this.m_values[level - 1];
  }
}
