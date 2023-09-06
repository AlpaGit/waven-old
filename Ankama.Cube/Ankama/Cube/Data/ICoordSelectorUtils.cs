// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICoordSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ICoordSelectorUtils
  {
    public static ICoordSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ICoordSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ICoordSelector");
        return (ICoordSelector) null;
      }
      string str = jtoken.Value<string>();
      ICoordSelector coordSelector;
      switch (str)
      {
        case "CentralSymmetryCoordSelector":
          coordSelector = (ICoordSelector) new CentralSymmetryCoordSelector();
          break;
        case "EntityPositionSelector":
          coordSelector = (ICoordSelector) new EntityPositionSelector();
          break;
        case "FilteredCoordSelector":
          coordSelector = (ICoordSelector) new FilteredCoordSelector();
          break;
        case "FirstCastTargetSelector":
          coordSelector = (ICoordSelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          coordSelector = (ICoordSelector) new IndexedCastTargetSelector();
          break;
        case "SecondCastTargetSelector":
          coordSelector = (ICoordSelector) new SecondCastTargetSelector();
          break;
        case "SingleCoordWithConditionSelector":
          coordSelector = (ICoordSelector) new SingleCoordWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          coordSelector = (ICoordSelector) new TriggeringEventFirstCastTargetSelector();
          break;
        case "UnionOfCoordsSelector":
          coordSelector = (ICoordSelector) new UnionOfCoordsSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ICoordSelector) null;
      }
      coordSelector.PopulateFromJson(jsonObject);
      return coordSelector;
    }

    public static ICoordSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ICoordSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ICoordSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
