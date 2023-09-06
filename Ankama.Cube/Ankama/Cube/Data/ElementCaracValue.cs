// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementCaracValue
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
  [Serializable]
  public sealed class ElementCaracValue : DynamicValue
  {
    private OwnerFilter m_player;
    private CaracId m_elementType;

    public OwnerFilter player => this.m_player;

    public CaracId elementType => this.m_elementType;

    public override string ToString() => "<element>";

    public static ElementCaracValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (ElementCaracValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      ElementCaracValue elementCaracValue = new ElementCaracValue();
      elementCaracValue.PopulateFromJson(jsonObject);
      return elementCaracValue;
    }

    public static ElementCaracValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      ElementCaracValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : ElementCaracValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_player = OwnerFilter.FromJsonProperty(jsonObject, "player");
      this.m_elementType = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "elementType", 12);
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      if (context is DynamicValueFightContext valueFightContext)
      {
        IEnumerable<IEntity> entities = (IEnumerable<IEntity>) valueFightContext.fightStatus.EnumerateEntities<PlayerStatus>();
        int num = 0;
        foreach (IEntity entity in this.m_player.Filter(entities, context))
          num += entity.GetCarac(this.m_elementType);
        value = num;
        return true;
      }
      value = 0;
      return false;
    }
  }
}
