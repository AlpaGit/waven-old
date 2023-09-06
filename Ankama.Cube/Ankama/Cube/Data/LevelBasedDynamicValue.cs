// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.LevelBasedDynamicValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class LevelBasedDynamicValue : DynamicValue
  {
    public override string ToString() => this.GetType().Name;

    public static LevelBasedDynamicValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (LevelBasedDynamicValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class LevelBasedDynamicValue");
        return (LevelBasedDynamicValue) null;
      }
      string str = jtoken.Value<string>();
      LevelBasedDynamicValue basedDynamicValue;
      switch (str)
      {
        case "LinearLevelBasedDynamicValue":
          basedDynamicValue = (LevelBasedDynamicValue) new LinearLevelBasedDynamicValue();
          break;
        case "ConstIntLevelBasedDynamicValue":
          basedDynamicValue = (LevelBasedDynamicValue) new ConstIntLevelBasedDynamicValue();
          break;
        case "DynamicValueLevelBasedDynamicValue":
          basedDynamicValue = (LevelBasedDynamicValue) new DynamicValueLevelBasedDynamicValue();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (LevelBasedDynamicValue) null;
      }
      basedDynamicValue.PopulateFromJson(jsonObject);
      return basedDynamicValue;
    }

    public static LevelBasedDynamicValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      LevelBasedDynamicValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : LevelBasedDynamicValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);
  }
}
