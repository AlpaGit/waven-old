// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReservePointsCost
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
  public sealed class ReservePointsCost : Cost
  {
    private DynamicValue m_value;

    public DynamicValue value => this.m_value;

    public override string ToString() => string.Format("{0} Reserve", (object) this.value);

    public static ReservePointsCost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ReservePointsCost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ReservePointsCost reservePointsCost = new ReservePointsCost();
      reservePointsCost.PopulateFromJson(jsonObject);
      return reservePointsCost;
    }

    public static ReservePointsCost FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ReservePointsCost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ReservePointsCost.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
    }

    protected override CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext castTargetContext)
    {
      int num;
      this.value.GetValue(castTargetContext, out num);
      return status.GetCarac(CaracId.ReservePoints, 0) >= num ? CastValidity.SUCCESS : CastValidity.NOT_ENOUGH_RESERVE_POINTS;
    }

    public override void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context)
    {
      int num;
      this.value.GetValue(context, out num);
      modifications.Increment(CaracId.ReservePoints, -num);
    }
  }
}
