// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ITargetSelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ITargetSelectorUtils
  {
    public static ITargetSelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ITargetSelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ITargetSelector");
        return (ITargetSelector) null;
      }
      string str = jtoken.Value<string>();
      ITargetSelector targetSelector;
      switch (str)
      {
        case "ActionedEntitySelector":
          targetSelector = (ITargetSelector) new ActionedEntitySelector();
          break;
        case "AllEntitiesSelector":
          targetSelector = (ITargetSelector) new AllEntitiesSelector();
          break;
        case "ApplicationHolderSelector":
          targetSelector = (ITargetSelector) new ApplicationHolderSelector();
          break;
        case "BounceEntitiesSelector":
          targetSelector = (ITargetSelector) new BounceEntitiesSelector();
          break;
        case "CasterHeroSelector":
          targetSelector = (ITargetSelector) new CasterHeroSelector();
          break;
        case "CasterSelector":
          targetSelector = (ITargetSelector) new CasterSelector();
          break;
        case "CasterSpecificCompanionSelector":
          targetSelector = (ITargetSelector) new CasterSpecificCompanionSelector();
          break;
        case "CentralSymmetryCoordSelector":
          targetSelector = (ITargetSelector) new CentralSymmetryCoordSelector();
          break;
        case "ConditionalSelectorForCast":
          targetSelector = (ITargetSelector) new ConditionalSelectorForCast();
          break;
        case "EffectHolderSelector":
          targetSelector = (ITargetSelector) new EffectHolderSelector();
          break;
        case "EntityPositionSelector":
          targetSelector = (ITargetSelector) new EntityPositionSelector();
          break;
        case "FilteredCoordSelector":
          targetSelector = (ITargetSelector) new FilteredCoordSelector();
          break;
        case "FilteredEntitySelector":
          targetSelector = (ITargetSelector) new FilteredEntitySelector();
          break;
        case "FirstCastTargetSelector":
          targetSelector = (ITargetSelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          targetSelector = (ITargetSelector) new IndexedCastTargetSelector();
          break;
        case "InvokedEntityTargetSelector":
          targetSelector = (ITargetSelector) new InvokedEntityTargetSelector();
          break;
        case "MechanismActivatorSelector":
          targetSelector = (ITargetSelector) new MechanismActivatorSelector();
          break;
        case "OwnerOfEffectHolderSelector":
          targetSelector = (ITargetSelector) new OwnerOfEffectHolderSelector();
          break;
        case "OwnerOfSelector":
          targetSelector = (ITargetSelector) new OwnerOfSelector();
          break;
        case "SecondCastTargetSelector":
          targetSelector = (ITargetSelector) new SecondCastTargetSelector();
          break;
        case "SingleCoordWithConditionSelector":
          targetSelector = (ITargetSelector) new SingleCoordWithConditionSelector();
          break;
        case "SingleEntityWithConditionSelector":
          targetSelector = (ITargetSelector) new SingleEntityWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          targetSelector = (ITargetSelector) new TriggeringEventFirstCastTargetSelector();
          break;
        case "TriggeringEventTargetSelector":
          targetSelector = (ITargetSelector) new TriggeringEventTargetSelector();
          break;
        case "UnionOfCoordsSelector":
          targetSelector = (ITargetSelector) new UnionOfCoordsSelector();
          break;
        case "UnionOfEntitiesSelector":
          targetSelector = (ITargetSelector) new UnionOfEntitiesSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ITargetSelector) null;
      }
      targetSelector.PopulateFromJson(jsonObject);
      return targetSelector;
    }

    public static ITargetSelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ITargetSelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ITargetSelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
