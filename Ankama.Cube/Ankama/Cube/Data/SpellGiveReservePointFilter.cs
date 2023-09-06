// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellGiveReservePointFilter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class SpellGiveReservePointFilter : SpellFilter
  {
    public override string ToString() => this.GetType().Name;

    public static SpellGiveReservePointFilter FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SpellGiveReservePointFilter) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SpellGiveReservePointFilter reservePointFilter = new SpellGiveReservePointFilter();
      reservePointFilter.PopulateFromJson(jsonObject);
      return reservePointFilter;
    }

    public static SpellGiveReservePointFilter FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SpellGiveReservePointFilter defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SpellGiveReservePointFilter.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    public override bool Accept(int spellInstanceId, SpellDefinition spellDef)
    {
      IReadOnlyList<GaugeValue> modifyOnSpellPlay = spellDef.gaugeToModifyOnSpellPlay;
      for (int index = 0; index < ((IReadOnlyCollection<GaugeValue>) modifyOnSpellPlay).Count; ++index)
      {
        if (modifyOnSpellPlay[index].element == CaracId.ReservePoints)
          return true;
      }
      return false;
    }
  }
}
