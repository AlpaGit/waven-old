// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleCoordSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISingleCoordSelectorUtils
  {
    public static ISingleCoordSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISingleCoordSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISingleCoordSelector");
        return (ISingleCoordSelector) null;
      }
      string str = jtoken.Value<string>();
      ISingleCoordSelector singleCoordSelector;
      switch (str)
      {
        case "CentralSymmetryCoordSelector":
          singleCoordSelector = (ISingleCoordSelector) new CentralSymmetryCoordSelector();
          break;
        case "EntityPositionSelector":
          singleCoordSelector = (ISingleCoordSelector) new EntityPositionSelector();
          break;
        case "FirstCastTargetSelector":
          singleCoordSelector = (ISingleCoordSelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          singleCoordSelector = (ISingleCoordSelector) new IndexedCastTargetSelector();
          break;
        case "SecondCastTargetSelector":
          singleCoordSelector = (ISingleCoordSelector) new SecondCastTargetSelector();
          break;
        case "SingleCoordWithConditionSelector":
          singleCoordSelector = (ISingleCoordSelector) new SingleCoordWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          singleCoordSelector = (ISingleCoordSelector) new TriggeringEventFirstCastTargetSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISingleCoordSelector) null;
      }
      singleCoordSelector.PopulateFromJson(jsonObject);
      return singleCoordSelector;
    }

    public static ISingleCoordSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISingleCoordSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISingleCoordSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
