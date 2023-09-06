// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DrainReservePointsCost
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.ReserveChanged})]
  [Serializable]
  public sealed class DrainReservePointsCost : Cost
  {
    public override string ToString() => "Consume whole reserve";

    public static DrainReservePointsCost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DrainReservePointsCost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DrainReservePointsCost reservePointsCost = new DrainReservePointsCost();
      reservePointsCost.PopulateFromJson(jsonObject);
      return reservePointsCost;
    }

    public static DrainReservePointsCost FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DrainReservePointsCost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DrainReservePointsCost.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    protected override CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext castTargetContext)
    {
      return status.reservePoints > 0 ? CastValidity.SUCCESS : CastValidity.NOT_ENOUGH_RESERVE_POINTS;
    }

    public override void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context)
    {
      modifications.Increment(CaracId.ReservePoints, -player.reservePoints);
    }
  }
}
