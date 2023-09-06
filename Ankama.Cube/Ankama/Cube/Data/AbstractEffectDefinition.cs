// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AbstractEffectDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public abstract class AbstractEffectDefinition : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static AbstractEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AbstractEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AbstractEffectDefinition");
        return (AbstractEffectDefinition) null;
      }
      string str = jtoken.Value<string>();
      AbstractEffectDefinition effectDefinition;
      switch (str)
      {
        case "EffectDefinition":
          effectDefinition = (AbstractEffectDefinition) new EffectDefinition();
          break;
        case "ReferenceEffectDefinition":
          effectDefinition = (AbstractEffectDefinition) new ReferenceEffectDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AbstractEffectDefinition) null;
      }
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static AbstractEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AbstractEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AbstractEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }
  }
}
