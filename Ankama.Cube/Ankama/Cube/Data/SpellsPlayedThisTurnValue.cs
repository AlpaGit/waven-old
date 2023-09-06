// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellsPlayedThisTurnValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.SpellsMoved})]
  [Serializable]
  public sealed class SpellsPlayedThisTurnValue : DynamicValue
  {
    private OwnerFilter m_player;
    private bool m_includingThisSpellCast;
    private bool m_countThisSpellOnly;

    public OwnerFilter player => this.m_player;

    public bool includingThisSpellCast => this.m_includingThisSpellCast;

    public bool countThisSpellOnly => this.m_countThisSpellOnly;

    public override string ToString() => this.GetType().Name;

    public static SpellsPlayedThisTurnValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellsPlayedThisTurnValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellsPlayedThisTurnValue playedThisTurnValue = new SpellsPlayedThisTurnValue();
      playedThisTurnValue.PopulateFromJson(jsonObject);
      return playedThisTurnValue;
    }

    public static SpellsPlayedThisTurnValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellsPlayedThisTurnValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellsPlayedThisTurnValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
      this.m_includingThisSpellCast = Serialization.JsonTokenValue<bool>(jsonObject, "includingThisSpellCast");
      this.m_countThisSpellOnly = Serialization.JsonTokenValue<bool>(jsonObject, "countThisSpellOnly");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Debug.LogWarning((object) "Unable to compute SpellsPlayedThisTurnValue client-side. Invalid data ?");
      value = 0;
      return false;
    }

    public override bool ToString(DynamicValueContext context, out string value)
    {
      value = "0";
      return false;
    }
  }
}
