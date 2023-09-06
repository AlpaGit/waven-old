// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.IFightAdditionalDataDefinitionUtils
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class IFightAdditionalDataDefinitionUtils
  {
    public static IFightAdditionalDataDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (IFightAdditionalDataDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class IFightAdditionalDataDefinition");
        return (IFightAdditionalDataDefinition) null;
      }
      string str = jtoken.Value<string>();
      if (str == "BossFightAdditionalDataDefinition")
      {
        IFightAdditionalDataDefinition additionalDataDefinition = (IFightAdditionalDataDefinition) new BossFightAdditionalDataDefinition();
        additionalDataDefinition.PopulateFromJson(jsonObject);
        return additionalDataDefinition;
      }
      Debug.LogWarning((object) ("Unknown type: " + str));
      return (IFightAdditionalDataDefinition) null;
    }

    public static IFightAdditionalDataDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      IFightAdditionalDataDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : IFightAdditionalDataDefinitionUtils.FromJsonToken(jproperty.Value);
    }
  }
}
