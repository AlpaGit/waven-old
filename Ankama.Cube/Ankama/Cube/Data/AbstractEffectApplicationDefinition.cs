// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AbstractEffectApplicationDefinition
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
  public abstract class AbstractEffectApplicationDefinition : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static AbstractEffectApplicationDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (AbstractEffectApplicationDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class AbstractEffectApplicationDefinition");
        return (AbstractEffectApplicationDefinition) null;
      }
      string str = jtoken.Value<string>();
      AbstractEffectApplicationDefinition applicationDefinition;
      switch (str)
      {
        case "EffectApplicationDefinition":
          applicationDefinition = (AbstractEffectApplicationDefinition) new EffectApplicationDefinition();
          break;
        case "AppearanceApplicationDefinition":
          applicationDefinition = (AbstractEffectApplicationDefinition) new AppearanceApplicationDefinition();
          break;
        case "InitializeEntityApplicationDefinition":
          applicationDefinition = (AbstractEffectApplicationDefinition) new InitializeEntityApplicationDefinition();
          break;
        case "DeathBlowApplicationDefinition":
          applicationDefinition = (AbstractEffectApplicationDefinition) new DeathBlowApplicationDefinition();
          break;
        case "DeathBlowForSpellApplicationDefinition":
          applicationDefinition = (AbstractEffectApplicationDefinition) new DeathBlowForSpellApplicationDefinition();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (AbstractEffectApplicationDefinition) null;
      }
      applicationDefinition.PopulateFromJson(jsonObject);
      return applicationDefinition;
    }

    public static AbstractEffectApplicationDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      AbstractEffectApplicationDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : AbstractEffectApplicationDefinition.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }
  }
}
