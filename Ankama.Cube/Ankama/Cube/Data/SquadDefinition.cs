// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SquadDefinition
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
  public sealed class SquadDefinition : EditableData
  {
    private Id<WeaponDefinition> m_weapon;
    private Gender m_gender;
    private int m_level;
    private List<Id<CompanionDefinition>> m_companions;
    private List<Id<SpellDefinition>> m_spells;

    public Id<WeaponDefinition> weapon => this.m_weapon;

    public Gender gender => this.m_gender;

    public int level => this.m_level;

    public IReadOnlyList<Id<CompanionDefinition>> companions => (IReadOnlyList<Id<CompanionDefinition>>) this.m_companions;

    public IReadOnlyList<Id<SpellDefinition>> spells => (IReadOnlyList<Id<SpellDefinition>>) this.m_spells;

    public override void PopulateFromJson(JObject jsonObject)
    {
      base.PopulateFromJson(jsonObject);
      this.m_weapon = Serialization.JsonTokenIdValue<WeaponDefinition>(jsonObject, "weapon");
      this.m_gender = (Gender) Serialization.JsonTokenValue<int>(jsonObject, "gender");
      this.m_level = Serialization.JsonTokenValue<int>(jsonObject, "level");
      JArray jarray1 = Serialization.JsonArray(jsonObject, "companions");
      this.m_companions = new List<Id<CompanionDefinition>>(jarray1 != null ? jarray1.Count : 0);
      if (jarray1 != null)
      {
        foreach (JToken token in jarray1)
          this.m_companions.Add(Serialization.JsonTokenIdValue<CompanionDefinition>(token));
      }
      JArray jarray2 = Serialization.JsonArray(jsonObject, "spells");
      this.m_spells = new List<Id<SpellDefinition>>(jarray2 != null ? jarray2.Count : 0);
      if (jarray2 == null)
        return;
      foreach (JToken token in jarray2)
        this.m_spells.Add(Serialization.JsonTokenIdValue<SpellDefinition>(token));
    }
  }
}
