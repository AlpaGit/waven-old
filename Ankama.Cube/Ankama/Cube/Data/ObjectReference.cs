// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ObjectReference
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  public static class ObjectReference
  {
    public static IDefinitionWithTooltip GetObject(ObjectReference.Type type, int id)
    {
      switch (type)
      {
        case ObjectReference.Type.Spell:
          return (IDefinitionWithTooltip) ObjectReference.GetSpell(id);
        case ObjectReference.Type.Companion:
          return (IDefinitionWithTooltip) ObjectReference.GetCompanion(id);
        case ObjectReference.Type.Summoning:
          return (IDefinitionWithTooltip) ObjectReference.GetSummoning(id);
        case ObjectReference.Type.FloorMechanism:
          return (IDefinitionWithTooltip) ObjectReference.GetFloorMechanism(id);
        case ObjectReference.Type.ObjectMechanism:
          return (IDefinitionWithTooltip) ObjectReference.GetObjectMechanism(id);
        case ObjectReference.Type.Weapon:
          return (IDefinitionWithTooltip) ObjectReference.GetWeapon(id);
        default:
          throw new ArgumentOutOfRangeException(nameof (type), (object) type, (string) null);
      }
    }

    public static SpellDefinition GetSpell(int spellId) => RuntimeData.spellDefinitions[spellId];

    public static CompanionDefinition GetCompanion(int companionId) => RuntimeData.companionDefinitions[companionId];

    public static SummoningDefinition GetSummoning(int summoningId) => RuntimeData.summoningDefinitions[summoningId];

    public static ObjectMechanismDefinition GetObjectMechanism(int objectMechanismId) => RuntimeData.objectMechanismDefinitions[objectMechanismId];

    public static FloorMechanismDefinition GetFloorMechanism(int floorMechanismId) => RuntimeData.floorMechanismDefinitions[floorMechanismId];

    public static WeaponDefinition GetWeapon(int weaponId) => RuntimeData.weaponDefinitions[weaponId];

    public static GodDefinition GetGod(string godName)
    {
      God key = (God) Enum.Parse(typeof (God), godName);
      GodDefinition god;
      RuntimeData.godDefinitions.TryGetValue(key, out god);
      return god;
    }

    public enum Type
    {
      None,
      Spell,
      Companion,
      Summoning,
      FloorMechanism,
      ObjectMechanism,
      Weapon,
    }
  }
}
