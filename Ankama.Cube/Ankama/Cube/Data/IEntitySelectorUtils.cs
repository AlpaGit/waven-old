// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IEntitySelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IEntitySelectorUtils
  {
    public static IEntitySelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IEntitySelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IEntitySelector");
        return (IEntitySelector) null;
      }
      string str = jtoken.Value<string>();
      IEntitySelector entitySelector;
      switch (str)
      {
        case "ActionedEntitySelector":
          entitySelector = (IEntitySelector) new ActionedEntitySelector();
          break;
        case "AllEntitiesSelector":
          entitySelector = (IEntitySelector) new AllEntitiesSelector();
          break;
        case "ApplicationHolderSelector":
          entitySelector = (IEntitySelector) new ApplicationHolderSelector();
          break;
        case "BounceEntitiesSelector":
          entitySelector = (IEntitySelector) new BounceEntitiesSelector();
          break;
        case "CasterHeroSelector":
          entitySelector = (IEntitySelector) new CasterHeroSelector();
          break;
        case "CasterSelector":
          entitySelector = (IEntitySelector) new CasterSelector();
          break;
        case "CasterSpecificCompanionSelector":
          entitySelector = (IEntitySelector) new CasterSpecificCompanionSelector();
          break;
        case "EffectHolderSelector":
          entitySelector = (IEntitySelector) new EffectHolderSelector();
          break;
        case "FilteredEntitySelector":
          entitySelector = (IEntitySelector) new FilteredEntitySelector();
          break;
        case "FirstCastTargetSelector":
          entitySelector = (IEntitySelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          entitySelector = (IEntitySelector) new IndexedCastTargetSelector();
          break;
        case "InvokedEntityTargetSelector":
          entitySelector = (IEntitySelector) new InvokedEntityTargetSelector();
          break;
        case "MechanismActivatorSelector":
          entitySelector = (IEntitySelector) new MechanismActivatorSelector();
          break;
        case "OwnerOfEffectHolderSelector":
          entitySelector = (IEntitySelector) new OwnerOfEffectHolderSelector();
          break;
        case "OwnerOfSelector":
          entitySelector = (IEntitySelector) new OwnerOfSelector();
          break;
        case "SecondCastTargetSelector":
          entitySelector = (IEntitySelector) new SecondCastTargetSelector();
          break;
        case "SingleEntityWithConditionSelector":
          entitySelector = (IEntitySelector) new SingleEntityWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          entitySelector = (IEntitySelector) new TriggeringEventFirstCastTargetSelector();
          break;
        case "TriggeringEventTargetSelector":
          entitySelector = (IEntitySelector) new TriggeringEventTargetSelector();
          break;
        case "UnionOfEntitiesSelector":
          entitySelector = (IEntitySelector) new UnionOfEntitiesSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IEntitySelector) null;
      }
      entitySelector.PopulateFromJson(jsonObject);
      return entitySelector;
    }

    public static IEntitySelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IEntitySelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IEntitySelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
