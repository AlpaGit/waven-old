// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DataAvailabilityDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class DataAvailabilityDefinition : EditableData
  {
    private List<GodAvailability> m_gods;
    private List<FightAvailability> m_fights;
    private List<SquadAvailability> m_squads;
    private List<CompanionAvailability> m_companions;
    private static DataAvailabilityDefinition s_instance;

    public IReadOnlyList<GodAvailability> gods => (IReadOnlyList<GodAvailability>) this.m_gods;

    public IReadOnlyList<FightAvailability> fights => (IReadOnlyList<FightAvailability>) this.m_fights;

    public IReadOnlyList<SquadAvailability> squads => (IReadOnlyList<SquadAvailability>) this.m_squads;

    public IReadOnlyList<CompanionAvailability> companions => (IReadOnlyList<CompanionAvailability>) this.m_companions;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      JArray jarray1 = Serialization.JsonArray(jsonObject, "gods");
      this.m_gods = new List<GodAvailability>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_gods.Add(GodAvailability.FromJsonToken(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "fights");
      this.m_fights = new List<FightAvailability>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 != null)
      {
        foreach (JToken token in jarray2)
          this.m_fights.Add(FightAvailability.FromJsonToken(token));
      }
      JArray jarray3 = Serialization.JsonArray(jsonObject, "squads");
      this.m_squads = new List<SquadAvailability>(jarray3 != null ? jarray3.Count : 0);
      if (jarray3 != null)
      {
        foreach (JToken token in jarray3)
          this.m_squads.Add(SquadAvailability.FromJsonToken(token));
      }
      JArray jarray4 = Serialization.JsonArray(jsonObject, "companions");
      this.m_companions = new List<CompanionAvailability>(jarray4 != null ? jarray4.Count : 0);
      if (jarray4 == null)
        return;
      foreach (JToken token in jarray4)
        this.m_companions.Add(CompanionAvailability.FromJsonToken(token));
    }

    private void OnEnable() => DataAvailabilityDefinition.s_instance = this;

    public static DataAvailability GetAvailability(God god)
    {
      foreach (GodAvailability god1 in (IEnumerable<GodAvailability>) DataAvailabilityDefinition.s_instance.gods)
      {
        if (god1.god == god)
          return god1.availability;
      }
      return DataAvailability.NotUsed;
    }

    public static DataAvailability GetAvailability(FightDefinition fight)
    {
      foreach (FightAvailability fight1 in (IEnumerable<FightAvailability>) DataAvailabilityDefinition.s_instance.fights)
      {
        if (fight1.fight.value == fight.id)
          return fight1.availability;
      }
      return DataAvailability.NotUsed;
    }

    public static DataAvailability GetAvailability(SquadDefinition squad)
    {
      foreach (SquadAvailability squad1 in (IEnumerable<SquadAvailability>) DataAvailabilityDefinition.s_instance.squads)
      {
        if (squad1.squad.value == squad.id)
          return squad1.availability;
      }
      return DataAvailability.NotUsed;
    }

    public static DataAvailability GetAvailability(CompanionDefinition companion)
    {
      foreach (CompanionAvailability companion1 in (IEnumerable<CompanionAvailability>) DataAvailabilityDefinition.s_instance.companions)
      {
        if (companion1.companion.value == companion.id)
          return companion1.availability;
      }
      return DataAvailability.NotUsed;
    }
  }
}
