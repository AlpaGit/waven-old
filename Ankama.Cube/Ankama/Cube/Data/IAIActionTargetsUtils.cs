// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IAIActionTargetsUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IAIActionTargetsUtils
  {
    public static IAIActionTargets FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IAIActionTargets) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IAIActionTargets");
        return (IAIActionTargets) null;
      }
      string str = jtoken.Value<string>();
      IAIActionTargets aiActionTargets;
      switch (str)
      {
        case "Nothing":
          aiActionTargets = (IAIActionTargets) new AITargets.Nothing();
          break;
        case "All":
          aiActionTargets = (IAIActionTargets) new AITargets.All();
          break;
        case "Allies":
          aiActionTargets = (IAIActionTargets) new AITargets.Allies();
          break;
        case "AlliesWounded":
          aiActionTargets = (IAIActionTargets) new AITargets.AlliesWounded();
          break;
        case "Opponents":
          aiActionTargets = (IAIActionTargets) new AITargets.Opponents();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (IAIActionTargets) null;
      }
      aiActionTargets.PopulateFromJson(jsonObject);
      return aiActionTargets;
    }

    public static IAIActionTargets FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IAIActionTargets defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IAIActionTargetsUtils.FromJsonToken(jproperty.Value);
    }
  }
}
