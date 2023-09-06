// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IStoppableCaracIdSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IStoppableCaracIdSelectorUtils
  {
    public static IStoppableCaracIdSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IStoppableCaracIdSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IStoppableCaracIdSelector");
        return (IStoppableCaracIdSelector) null;
      }
      string str = jtoken.Value<string>();
      if (str == "StoppableCaracIdDirectSelector")
      {
        IStoppableCaracIdSelector stoppableCaracIdSelector = (IStoppableCaracIdSelector) new StoppableCaracIdDirectSelector();
        stoppableCaracIdSelector.PopulateFromJson(jsonObject);
        return stoppableCaracIdSelector;
      }
      Debug.LogWarning((object) ("Unknown type: " + str));
      return (IStoppableCaracIdSelector) null;
    }

    public static IStoppableCaracIdSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IStoppableCaracIdSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IStoppableCaracIdSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
