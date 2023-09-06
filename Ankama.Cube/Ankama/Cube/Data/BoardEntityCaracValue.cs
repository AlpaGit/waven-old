// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.BoardEntityCaracValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.EntityAddedOrRemoved})]
  [Serializable]
  public sealed class BoardEntityCaracValue : DynamicValue
  {
    private IEntityFilter m_entity;
    private CaracId m_carac;

    public IEntityFilter entity => this.m_entity;

    public CaracId carac => this.m_carac;

    public override string ToString() => this.m_carac.ToString();

    public static BoardEntityCaracValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (BoardEntityCaracValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      BoardEntityCaracValue entityCaracValue = new BoardEntityCaracValue();
      entityCaracValue.PopulateFromJson(jsonObject);
      return entityCaracValue;
    }

    public static BoardEntityCaracValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      BoardEntityCaracValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : BoardEntityCaracValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_entity = IEntityFilterUtils.FromJsonProperty(jsonObject, "entity");
      this.m_carac = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "carac");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (context is DynamicValueFightContext valueFightContext)
      {
        IEnumerable<IEntityWithBoardPresence> withBoardPresences = valueFightContext.fightStatus.EnumerateEntities<IEntityWithBoardPresence>();
        int num = 0;
        foreach (IEntity entity in this.m_entity.Filter((IEnumerable<IEntity>) withBoardPresences, context))
          num += entity.GetCarac(this.m_carac);
        value = num;
        return true;
      }
      value = 0;
      return false;
    }
  }
}
