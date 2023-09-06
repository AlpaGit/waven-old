// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IObjectAreaDefinitionUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IObjectAreaDefinitionUtils
  {
    public static IObjectAreaDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IObjectAreaDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IObjectAreaDefinition");
        return (IObjectAreaDefinition) null;
      }
      string str = jtoken.Value<string>();
      IObjectAreaDefinition objectAreaDefinition;
      switch (str)
      {
        case "PointAreaDefinition":
          objectAreaDefinition = (IObjectAreaDefinition) new PointAreaDefinition();
          break;
        case "PivotBasedSquareAreaDefinition":
          objectAreaDefinition = (IObjectAreaDefinition) new PivotBasedSquareAreaDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IObjectAreaDefinition) null;
      }
      objectAreaDefinition.PopulateFromJson(jsonObject);
      return objectAreaDefinition;
    }

    public static IObjectAreaDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IObjectAreaDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IObjectAreaDefinitionUtils.FromJsonToken(jproperty.Value);
    }
  }
}
