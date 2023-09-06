// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SquadAvailability
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
  public sealed class SquadAvailability : IEditableContent
  {
    private Id<SquadDefinition> m_squad;
    private DataAvailability m_availability;

    public Id<SquadDefinition> squad => this.m_squad;

    public DataAvailability availability => this.m_availability;

    public override string ToString() => string.Format("{0} {1}", (object) this.m_squad, (object) this.m_availability);

    public static SquadAvailability FromJsonToken(JToken token)
    {
      if (token.Type != JTokenType.Object)
      {
        Debug.LogWarning((object) ("Malformed token : type Object expected, but " + (object) token.Type + " found"));
        return (SquadAvailability) null;
      }
      JObject jsonObject = token.Value<JObject>();
      SquadAvailability squadAvailability = new SquadAvailability();
      squadAvailability.PopulateFromJson(jsonObject);
      return squadAvailability;
    }

    public static SquadAvailability FromJsonProperty(
      JObject jsonObject,
      string propertyName,
      SquadAvailability defaultValue = null)
    {
      JProperty jproperty = jsonObject.Property(propertyName);
      return jproperty == null || jproperty.Value.Type == JTokenType.Null ? defaultValue : SquadAvailability.FromJsonToken(jproperty.Value);
    }

    public void PopulateFromJson(JObject jsonObject)
    {
      this.m_squad = Serialization.JsonTokenIdValue<SquadDefinition>(jsonObject, "squad");
      this.m_availability = (DataAvailability) Serialization.JsonTokenValue<int>(jsonObject, "availability");
    }
  }
}
