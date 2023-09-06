// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ChangeEntitySkinEffectDefinition
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
  public sealed class ChangeEntitySkinEffectDefinition : EffectExecutionWithDurationDefinition
  {
    private Id<CharacterSkinDefinition> m_newSkin;
    private SkinPriority m_priority;

    public Id<CharacterSkinDefinition> newSkin => this.m_newSkin;

    public SkinPriority priority => this.m_priority;

    public override string ToString() => this.GetType().Name;

    public static ChangeEntitySkinEffectDefinition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ChangeEntitySkinEffectDefinition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ChangeEntitySkinEffectDefinition effectDefinition = new ChangeEntitySkinEffectDefinition();
      effectDefinition.PopulateFromJson(jsonObject);
      return effectDefinition;
    }

    public static ChangeEntitySkinEffectDefinition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ChangeEntitySkinEffectDefinition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ChangeEntitySkinEffectDefinition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_newSkin = Serialization.JsonTokenIdValue<CharacterSkinDefinition>(jsonObject, "newSkin");
      this.m_priority = (SkinPriority) Serialization.JsonTokenValue<int>(jsonObject, "priority", 2);
    }
  }
}
