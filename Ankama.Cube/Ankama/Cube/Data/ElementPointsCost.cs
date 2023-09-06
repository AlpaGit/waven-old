// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementPointsCost
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Fight;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.ElementPointsChanged})]
  [Serializable]
  public sealed class ElementPointsCost : Cost
  {
    private CaracId m_element;
    private DynamicValue m_value;

    public CaracId element => this.m_element;

    public DynamicValue value => this.m_value;

    public override string ToString() => string.Format("{0} {1}", (object) this.value, (object) this.element);

    public static ElementPointsCost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementPointsCost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementPointsCost elementPointsCost = new ElementPointsCost();
      elementPointsCost.PopulateFromJson(jsonObject);
      return elementPointsCost;
    }

    public static ElementPointsCost FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementPointsCost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementPointsCost.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_element = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "element");
      this.m_value = DynamicValue.FromJsonProperty(jsonObject, "value");
    }

    protected override CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext castTargetContext)
    {
      int num;
      this.value.GetValue(castTargetContext, out num);
      return status.GetCarac(this.element, 0) >= num ? CastValidity.SUCCESS : CastValidity.NOT_ENOUGH_ELEMENT_POINTS;
    }

    public override void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context)
    {
      int num;
      this.value.GetValue(context, out num);
      modifications.Increment(this.element, -num);
    }
  }
}
