// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FloatingCounterValue
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
  public sealed class FloatingCounterValue : DynamicValue
  {
    private CaracId m_counterType;
    private ISingleEntitySelector m_entitySelector;

    public CaracId counterType => this.m_counterType;

    public ISingleEntitySelector entitySelector => this.m_entitySelector;

    public override string ToString() => this.GetType().Name;

    public static FloatingCounterValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FloatingCounterValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FloatingCounterValue floatingCounterValue = new FloatingCounterValue();
      floatingCounterValue.PopulateFromJson(jsonObject);
      return floatingCounterValue;
    }

    public static FloatingCounterValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FloatingCounterValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FloatingCounterValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_counterType = (CaracId) Serialization.JsonTokenValue<int>(jsonObject, "counterType", 20);
      this.m_entitySelector = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "entitySelector");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      IEntity entity = this.m_entitySelector.EnumerateEntities(context).FirstOrDefault<IEntity>();
      if (entity != null)
      {
        value = entity.GetCarac(this.m_counterType);
        return true;
      }
      value = 0;
      return false;
    }
  }
}
