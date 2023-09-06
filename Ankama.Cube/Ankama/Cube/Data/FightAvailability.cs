// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.FightAvailability
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class FightAvailability : IEditableContent
  {
    private Id<FightDefinition> m_fight;
    private DataAvailability m_availability;

    public Id<FightDefinition> fight => this.m_fight;

    public DataAvailability availability => this.m_availability;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_fight, (object) this.m_availability);

    public static FightAvailability FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (FightAvailability) null;
      }
      JObject jsonObject = token.Value<JObject>();
      FightAvailability fightAvailability = new FightAvailability();
      fightAvailability.PopulateFromJson(jsonObject);
      return fightAvailability;
    }

    public static FightAvailability FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      FightAvailability defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : FightAvailability.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_fight = Serialization.JsonTokenIdValue<FightDefinition>(jsonObject, "fight");
      this.m_availability = (DataAvailability) Serialization.JsonTokenValue<int>(jsonObject, "availability");
    }
  }
}
