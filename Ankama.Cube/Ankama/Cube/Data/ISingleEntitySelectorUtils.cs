// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ISingleEntitySelectorUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class ISingleEntitySelectorUtils
  {
    public static ISingleEntitySelector FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ISingleEntitySelector) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class ISingleEntitySelector");
        return (ISingleEntitySelector) null;
      }
      string str = jtoken.Value<string>();
      ISingleEntitySelector singleEntitySelector;
      switch (str)
      {
        case "ActionedEntitySelector":
          singleEntitySelector = (ISingleEntitySelector) new ActionedEntitySelector();
          break;
        case "ApplicationHolderSelector":
          singleEntitySelector = (ISingleEntitySelector) new ApplicationHolderSelector();
          break;
        case "CasterHeroSelector":
          singleEntitySelector = (ISingleEntitySelector) new CasterHeroSelector();
          break;
        case "CasterSelector":
          singleEntitySelector = (ISingleEntitySelector) new CasterSelector();
          break;
        case "CasterSpecificCompanionSelector":
          singleEntitySelector = (ISingleEntitySelector) new CasterSpecificCompanionSelector();
          break;
        case "EffectHolderSelector":
          singleEntitySelector = (ISingleEntitySelector) new EffectHolderSelector();
          break;
        case "FirstCastTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new FirstCastTargetSelector();
          break;
        case "IndexedCastTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new IndexedCastTargetSelector();
          break;
        case "InvokedEntityTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new InvokedEntityTargetSelector();
          break;
        case "MechanismActivatorSelector":
          singleEntitySelector = (ISingleEntitySelector) new MechanismActivatorSelector();
          break;
        case "OwnerOfEffectHolderSelector":
          singleEntitySelector = (ISingleEntitySelector) new OwnerOfEffectHolderSelector();
          break;
        case "SecondCastTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new SecondCastTargetSelector();
          break;
        case "SingleEntityWithConditionSelector":
          singleEntitySelector = (ISingleEntitySelector) new SingleEntityWithConditionSelector();
          break;
        case "TriggeringEventFirstCastTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new TriggeringEventFirstCastTargetSelector();
          break;
        case "TriggeringEventTargetSelector":
          singleEntitySelector = (ISingleEntitySelector) new TriggeringEventTargetSelector();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (ISingleEntitySelector) null;
      }
      singleEntitySelector.PopulateFromJson(jsonObject);
      return singleEntitySelector;
    }

    public static ISingleEntitySelector FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ISingleEntitySelector defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ISingleEntitySelectorUtils.FromJsonToken(jproperty.Value);
    }
  }
}
