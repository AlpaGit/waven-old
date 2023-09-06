// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleTargetSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISingleTargetSelectorUtils
  {
    public static ISingleTargetSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISingleTargetSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISingleTargetSelector");
        return (ISingleTargetSelector) null;
      }
      string str = jtoken.Value<string>();
      ISingleTargetSelector singleTargetSelector;
      switch (str)
      {
        case "ActionedEntitySelector":
          singleTargetSelector = (ISingleTargetSelector) new ActionedEntitySelector();
          break;
        case "ApplicationHolderSelector":
          singleTargetSelector = (ISingleTargetSelector) new ApplicationHolderSelector();
          break;
        case "CasterHeroSelector":
          singleTargetSelector = (ISingleTargetSelector) new CasterHeroSelector();
          break;
        case "CasterSelector":
          singleTargetSelector = (ISingleTargetSelector) new CasterSelector();
          break;
        case "CasterSpecificCompanionSelector":
          singleTargetSelector = (ISingleTargetSelector) new CasterSpecificCompanionSelector();
          break;
        case "CentralSymmetryCoordSelector":
          singleTargetSelector = (ISingleTargetSelector) new CentralSymmetryCoordSelector();
          break;
        case "EffectHolderSelector":
          singleTargetSelector = (ISingleTargetSelector) new EffectHolderSelector();
          break;
        case "EntityPositionSelector":
          singleTargetSelector = (ISingleTargetSelector) new EntityPositionSelector();
          break;
        case "FirstCastTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new IndexedCastTargetSelector();
          break;
        case "InvokedEntityTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new InvokedEntityTargetSelector();
          break;
        case "MechanismActivatorSelector":
          singleTargetSelector = (ISingleTargetSelector) new MechanismActivatorSelector();
          break;
        case "OwnerOfEffectHolderSelector":
          singleTargetSelector = (ISingleTargetSelector) new OwnerOfEffectHolderSelector();
          break;
        case "SecondCastTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new SecondCastTargetSelector();
          break;
        case "SingleCoordWithConditionSelector":
          singleTargetSelector = (ISingleTargetSelector) new SingleCoordWithConditionSelector();
          break;
        case "SingleEntityWithConditionSelector":
          singleTargetSelector = (ISingleTargetSelector) new SingleEntityWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new TriggeringEventFirstCastTargetSelector();
          break;
        case "TriggeringEventTargetSelector":
          singleTargetSelector = (ISingleTargetSelector) new TriggeringEventTargetSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISingleTargetSelector) null;
      }
      singleTargetSelector.PopulateFromJson(jsonObject);
      return singleTargetSelector;
    }

    public static ISingleTargetSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISingleTargetSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISingleTargetSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
