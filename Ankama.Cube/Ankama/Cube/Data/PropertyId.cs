// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertyId
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.PropertyChanged})]
  [Serializable]
  public enum PropertyId
  {
    Stun = 1,
    [RelatedToEvents(new EventCategory[] {EventCategory.PlaySpellForbiddenChanged})] PlaySpellForbidden = 2,
    CharacterActionForbidden = 3,
    Rooted = 4,
    Petrify = 6,
    Initiative = 7,
    Shield = 8,
    PhysicalCounter = 9,
    ElementaryStateProof = 10, // 0x0000000A
    PhysicalDamageProof = 11, // 0x0000000B
    MagicalDamageProof = 12, // 0x0000000C
    HealProof = 13, // 0x0000000D
    DamageProof = 14, // 0x0000000E
    PhysicalHealProof = 15, // 0x0000000F
    MagicalHealProof = 16, // 0x00000010
    EntityVersion2 = 17, // 0x00000011
    CanPassThrough = 18, // 0x00000012
    Untargetable = 19, // 0x00000013
    Unmovable = 20, // 0x00000014
    DoubleDamageReceived = 21, // 0x00000015
    MotivatingSight = 22, // 0x00000016
    HealingSight = 23, // 0x00000017
    AnimalLink = 24, // 0x00000018
    Shadowed = 25, // 0x00000019
    Frozen = 26, // 0x0000001A
    InvokeCopyOnDeath = 27, // 0x0000001B
    Agony = 28, // 0x0000001C
    SacredSight = 29, // 0x0000001D
    RepulsiveSight = 30, // 0x0000001E
    ProtectedSight = 31, // 0x0000001F
    StoneSight = 32, // 0x00000020
    AerialSight = 33, // 0x00000021
    AutoResurrectCompanion = 34, // 0x00000022
  }
}
