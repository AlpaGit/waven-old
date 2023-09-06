// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Cost
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
  [Serializable]
  public abstract class Cost : IEditableContent
  {
    public override string ToString() => this.GetType().Name;

    public static Cost FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (Cost) null;
      }
      JObject jsonObject = token.Value<JObject>();
      JToken jtoken;
      if (!jsonObject.TryGetValue("type", out jtoken))
      {
        Debug.LogWarning((object) "Malformed json: no 'type' property in object of class Cost");
        return (Cost) null;
      }
      string str = jtoken.Value<string>();
      Cost cost;
      switch (str)
      {
        case "ActionPointsCost":
          cost = (Cost) new ActionPointsCost();
          break;
        case "DrainActionPointsCost":
          cost = (Cost) new DrainActionPointsCost();
          break;
        case "ReservePointsCost":
          cost = (Cost) new ReservePointsCost();
          break;
        case "DrainReservePointsCost":
          cost = (Cost) new DrainReservePointsCost();
          break;
        case "ElementPointsCost":
          cost = (Cost) new ElementPointsCost();
          break;
        default:
          Debug.LogWarning((object) ("Unknown type: " + str));
          return (Cost) null;
      }
      cost.PopulateFromJson(jsonObject);
      return cost;
    }

    public static Cost FromJsonProperty(JObject jsonObject, string propertyName, Cost defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : Cost.FromJsonToken(jproperty.Value);
    }

    public virtual void PopulateFromJson(JObject jsonObject)
    {
    }

    protected abstract CastValidity InternalCheckValidity(
      PlayerStatus status,
      DynamicValueContext castTargetContext);

    public CastValidity CheckValidity(PlayerStatus status, DynamicValueContext castTargetContext) => this.InternalCheckValidity(status, castTargetContext);

    public abstract void UpdateModifications(
      ref GaugesModification modifications,
      PlayerStatus player,
      DynamicValueContext context);
  }
}
