// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DrainActionPointsCost
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
  [RelatedToEvents(new EventCategory[] {EventCategory.ActionPointsChanged})]
  [Serializable]
  public sealed class DrainActionPointsCost : Cost
  {
    public override string ToString() => "Consume all AP";

    public static DrainActionPointsCost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DrainActionPointsCost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DrainActionPointsCost actionPointsCost = new DrainActionPointsCost();
      actionPointsCost.PopulateFromJson(jsonObject);
      return actionPointsCost;
    }

    public static DrainActionPointsCost FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DrainActionPointsCost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DrainActionPointsCost.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject) => base.PopulateFromJson(jsonObject);

    protected override CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext castTargetContext)
    {
      return status.actionPoints > 0 ? CastValidity.SUCCESS : CastValidity.NOT_ENOUGH_ACTION_POINTS;
    }

    public override void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context)
    {
      modifications.Increment(CaracId.ActionPoints, -player.actionPoints);
    }
  }
}
