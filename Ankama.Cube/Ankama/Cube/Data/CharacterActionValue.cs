// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CharacterActionValue
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
  public sealed class CharacterActionValue : DynamicValue
  {
    private ISingleEntitySelector m_entitySelector;
    private bool m_addRelatedBoost;

    public ISingleEntitySelector entitySelector => this.m_entitySelector;

    public bool addRelatedBoost => this.m_addRelatedBoost;

    public override string ToString() => this.GetType().Name;

    public static CharacterActionValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CharacterActionValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CharacterActionValue characterActionValue = new CharacterActionValue();
      characterActionValue.PopulateFromJson(jsonObject);
      return characterActionValue;
    }

    public static CharacterActionValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CharacterActionValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CharacterActionValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_entitySelector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "entitySelector");
      this.m_addRelatedBoost = Serialization.JsonTokenValue<bool>(jsonObject, "addRelatedBoost");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (context is CharacterActionValueContext actionValueContext && actionValueContext.relatedCharacterActionValue.HasValue)
      {
        value = actionValueContext.relatedCharacterActionValue.Value;
        return true;
      }
      value = 0;
      return false;
    }
  }
}
