// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellsInHandValue
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
  public sealed class SpellsInHandValue : DynamicValue
  {
    private OwnerFilter m_player;
    private bool m_countMissingSpells;

    public OwnerFilter player => this.m_player;

    public bool countMissingSpells => this.m_countMissingSpells;

    public override string ToString() => this.GetType().Name;

    public static SpellsInHandValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellsInHandValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellsInHandValue spellsInHandValue = new SpellsInHandValue();
      spellsInHandValue.PopulateFromJson(jsonObject);
      return spellsInHandValue;
    }

    public static SpellsInHandValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellsInHandValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellsInHandValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
      this.m_countMissingSpells = Serialization.JsonTokenValue<bool>(jsonObject, "countMissingSpells");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      Debug.LogWarning((object) "Unable to compute SpellsInHandValue client-side. Invalid data ?");
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
