// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CaracValueCondition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class CaracValueCondition : EffectCondition
  {
    private ISingleEntitySelector m_selector;
    private CaracId m_carac;
    private ValueFilter m_value;

    public ISingleEntitySelector selector => this.m_selector;

    public CaracId carac => this.m_carac;

    public ValueFilter value => this.m_value;

    public override string ToString() => string.Format("{0} has {1} {2}", (object) this.m_selector, (object) this.m_carac, (object) this.m_value);

    public static CaracValueCondition FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (CaracValueCondition) null;
      }
      JObject jsonObject = token.Value<JObject>();
      CaracValueCondition caracValueCondition = new CaracValueCondition();
      caracValueCondition.PopulateFromJson(jsonObject);
      return caracValueCondition;
    }

    public static CaracValueCondition FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      CaracValueCondition defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : CaracValueCondition.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_selector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "selector");
      this.m_carac = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "carac", 3);
      this.m_value = ValueFilter.FromJsonProperty(jsonObject, "value");
    }

    public override bool IsValid(DynamicValueContext context) => this.value.Matches(this.selector.EnumerateEntities(context).First<IEntity>().GetCarac(this.m_carac), context);
  }
}
