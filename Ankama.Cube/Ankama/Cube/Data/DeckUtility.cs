// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DeckUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using DataEditor;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public static class DeckUtility
  {
    public static int GetRemainingSlotsForWeapon(int weapon)
    {
      int num = 0;
      foreach (DeckInfo deck in PlayerData.instance.GetDecks())
      {
        if (deck.Weapon == weapon)
          ++num;
      }
      return Math.Max(0, 3 - num);
    }

    public static DeckInfo FindValidDeckForGod(God god)
    {
      foreach (DeckInfo deck in PlayerData.instance.GetDecks())
      {
        if ((God) deck.God == god && deck.IsValid())
          return deck;
      }
      return (DeckInfo) null;
    }

    public static bool IsValid(this DeckInfo info)
    {
      if (info == null)
        return false;
      RepeatedField<int> spells = info.Spells;
      int count1 = spells.Count;
      if (count1 < 8)
        return false;
      for (int index = 0; index < count1; ++index)
      {
        if (spells[index] <= 0)
          return false;
      }
      RepeatedField<int> companions = info.Companions;
      int count2 = info.Companions.Count;
      if (count2 < 4)
        return false;
      for (int index = 0; index < count2; ++index)
      {
        if (companions[index] <= 0)
          return false;
      }
      int? nullable = new int?(info.Weapon);
      return nullable.HasValue && PlayerData.instance.weaponInventory.TryGetLevel(nullable.Value, out int _);
    }

    public static int GetLevel(this DeckInfo info, ILevelProvider weaponLevelProvider)
    {
      int level = 0;
      weaponLevelProvider.TryGetLevel(info.Weapon, out level);
      return Math.Max(1, level);
    }

    public static DeckInfo EnsureDataConsistency(this DeckInfo deck)
    {
      RepeatedField<int> spells = deck.Spells;
      for (int index = spells.Count - 1; index >= 0; --index)
      {
        int key = spells[index];
        if (!RuntimeData.spellDefinitions.TryGetValue(key, out SpellDefinition _))
          spells.RemoveAt(index);
      }
      RepeatedField<int> companions = deck.Companions;
      for (int index = companions.Count - 1; index >= 0; --index)
      {
        int key = companions[index];
        if (!RuntimeData.companionDefinitions.TryGetValue(key, out CompanionDefinition _))
          companions.RemoveAt(index);
      }
      if (!RuntimeData.weaponDefinitions.TryGetValue(deck.Weapon, out WeaponDefinition _))
        deck.Weapon = 0;
      return deck;
    }

    public static DeckInfo ToDeckInfo(this SquadDefinition definition)
    {
      WeaponDefinition weaponDefinition;
      if (!RuntimeData.weaponDefinitions.TryGetValue(definition.weapon.value, out weaponDefinition))
        return (DeckInfo) null;
      DeckInfo deckInfo = new DeckInfo()
      {
        Name = RuntimeData.FormattedText(63105, (IValueProvider) null),
        God = (int) weaponDefinition.god,
        Weapon = weaponDefinition.id
      };
      IReadOnlyList<Id<CompanionDefinition>> companions = definition.companions;
      int num1 = 0;
      for (int count = ((IReadOnlyCollection<Id<CompanionDefinition>>) companions).Count; num1 < count; ++num1)
      {
        if (RuntimeData.companionDefinitions.TryGetValue(companions[num1].value, out CompanionDefinition _))
          deckInfo.Companions.Add(companions[num1].value);
      }
      IReadOnlyList<Id<SpellDefinition>> spells = definition.spells;
      int num2 = 0;
      for (int count = ((IReadOnlyCollection<Id<SpellDefinition>>) spells).Count; num2 < count; ++num2)
      {
        if (RuntimeData.spellDefinitions.TryGetValue(spells[num2].value, out SpellDefinition _))
          deckInfo.Spells.Add(spells[num2].value);
      }
      return deckInfo;
    }

    public static DeckInfo FillEmptySlotsCopy(this DeckInfo clone, int defaultValue = -1)
    {
      int count1 = clone.Companions.Count;
      for (int index = 4; count1 < index; ++count1)
        clone.Companions.Add(defaultValue);
      int count2 = clone.Spells.Count;
      for (int index = 8; count2 < index; ++count2)
        clone.Spells.Add(defaultValue);
      return clone;
    }

    public static DeckInfo TrimCopy(this DeckInfo info, int defaultValue = -1)
    {
      DeckInfo deckInfo = new DeckInfo()
      {
        Id = info.Id,
        Name = info.Name,
        God = info.God,
        Weapon = info.Weapon
      };
      RepeatedField<int> companions = info.Companions;
      int index1 = 0;
      for (int count = companions.Count; index1 < count; ++index1)
      {
        if (companions[index1] != defaultValue)
          deckInfo.Companions.Add(companions[index1]);
      }
      RepeatedField<int> spells = info.Spells;
      int index2 = 0;
      for (int count = spells.Count; index2 < count; ++index2)
      {
        if (spells[index2] != defaultValue)
          deckInfo.Spells.Add(spells[index2]);
      }
      return deckInfo;
    }

    public static bool DecksAreEqual(DeckInfo deck, DeckInfo deck2) => deck == null ? deck2 == null : deck.Equals(deck2);
  }
}
