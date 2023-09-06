// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IAreaDefinitionUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IAreaDefinitionUtils
  {
    public static IAreaDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IAreaDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IAreaDefinition");
        return (IAreaDefinition) null;
      }
      string str = jtoken.Value<string>();
      IAreaDefinition areaDefinition;
      switch (str)
      {
        case "CenteredSquareAreaDefinition":
          areaDefinition = (IAreaDefinition) new CenteredSquareAreaDefinition();
          break;
        case "CircleAreaDefinition":
          areaDefinition = (IAreaDefinition) new CircleAreaDefinition();
          break;
        case "PointAreaDefinition":
          areaDefinition = (IAreaDefinition) new PointAreaDefinition();
          break;
        case "PivotBasedSquareAreaDefinition":
          areaDefinition = (IAreaDefinition) new PivotBasedSquareAreaDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IAreaDefinition) null;
      }
      areaDefinition.PopulateFromJson(jsonObject);
      return areaDefinition;
    }

    public static IAreaDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IAreaDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IAreaDefinitionUtils.FromJsonToken(jproperty.Value);
    }
  }
}
