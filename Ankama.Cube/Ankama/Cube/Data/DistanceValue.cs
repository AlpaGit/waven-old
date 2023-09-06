// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DistanceValue
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Cube.Fight.Entities;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class DistanceValue : DynamicValue
  {
    private ISingleEntitySelector m_start;
    private ISingleEntitySelector m_end;

    public ISingleEntitySelector start => this.m_start;

    public ISingleEntitySelector end => this.m_end;

    public override string ToString() => this.GetType().Name;

    public static DistanceValue FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (DistanceValue) null;
      }
      JObject jsonObject = token.Value<JObject>();
      DistanceValue distanceValue = new DistanceValue();
      distanceValue.PopulateFromJson(jsonObject);
      return distanceValue;
    }

    public static DistanceValue FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      DistanceValue defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : DistanceValue.FromJsonToken(jproperty.Value);
    }

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_start = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "start");
      this.m_end = ISingleEntitySelectorUtils.FromJsonProperty(jsonObject, "end");
    }

    public override bool GetValue(DynamicValueContext context, out int value)
    {
      IEntityWithBoardPresence entity1;
      IEntityWithBoardPresence entity2;
      if (context is DynamicValueFightContext && this.m_start.TryGetEntity<IEntityWithBoardPresence>(context, out entity1) && this.m_end.TryGetEntity<IEntityWithBoardPresence>(context, out entity2))
      {
        Vector2Int refCoord1 = entity1.area.refCoord;
        Vector2Int refCoord2 = entity2.area.refCoord;
        value = refCoord1.DistanceTo(refCoord2);
        return true;
      }
      value = 0;
      return false;
    }
  }
}
