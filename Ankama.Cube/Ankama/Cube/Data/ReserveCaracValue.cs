// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ReserveCaracValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.ReserveChanged})]
  [Serializable]
  public sealed class ReserveCaracValue : DynamicValue
  {
    private OwnerFilter m_player;

    public OwnerFilter player => this.m_player;

    public override string ToString() => string.Format("(Reserve of {0}owner of {1})", this.m_player.isIdentical ? (object) "" : (object) "not ", (object) this.m_player.reference);

    public static ReserveCaracValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ReserveCaracValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ReserveCaracValue reserveCaracValue = new ReserveCaracValue();
      reserveCaracValue.PopulateFromJson(jsonObject);
      return reserveCaracValue;
    }

    public static ReserveCaracValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ReserveCaracValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ReserveCaracValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (context is DynamicValueFightContext valueFightContext)
      {
        IEnumerable<IEntity> entities = (IEnumerable<IEntity>) valueFightContext.fightStatus.EnumerateEntities<PlayerStatus>();
        int num = 0;
        foreach (IEntity entity in this.m_player.Filter(entities, context))
          num += entity.GetCarac(CaracId.ReservePoints);
        value = num;
        return true;
      }
      value = 0;
      return false;
    }
  }
}
