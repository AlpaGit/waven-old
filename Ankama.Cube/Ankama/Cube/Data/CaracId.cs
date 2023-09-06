// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CaracId
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [Serializable]
  public enum CaracId
  {
    [RelatedToEvents(new EventCategory[] {EventCategory.LifeArmorChanged})] Life = 1,
    [RelatedToEvents(new EventCategory[] {EventCategory.MovementPointsChanged})] MovementPoints = 2,
    [RelatedToEvents(new EventCategory[] {EventCategory.ActionPointsChanged})] ActionPoints = 3,
    Resistance = 5,
    [RelatedToEvents(new EventCategory[] {EventCategory.LifeArmorChanged})] Armor = 6,
    HitLimit = 7,
    RangeMin = 8,
    RangeMax = 9,
    DamageReflection = 10, // 0x0000000A
    [RelatedToEvents(new EventCategory[] {EventCategory.ElementPointsChanged})] FirePoints = 11, // 0x0000000B
    [RelatedToEvents(new EventCategory[] {EventCategory.ElementPointsChanged})] WaterPoints = 12, // 0x0000000C
    [RelatedToEvents(new EventCategory[] {EventCategory.ElementPointsChanged})] EarthPoints = 13, // 0x0000000D
    [RelatedToEvents(new EventCategory[] {EventCategory.ElementPointsChanged})] AirPoints = 14, // 0x0000000E
    [RelatedToEvents(new EventCategory[] {EventCategory.DamageModifierChanged})] PhysicalDamageModifier = 15, // 0x0000000F
    [RelatedToEvents(new EventCategory[] {EventCategory.HealModifierChanged})] PhysicalHealModifier = 16, // 0x00000010
    [RelatedToEvents(new EventCategory[] {EventCategory.DamageModifierChanged})] MagicalDamageModifier = 17, // 0x00000011
    [RelatedToEvents(new EventCategory[] {EventCategory.HealModifierChanged})] MagicalHealModifier = 18, // 0x00000012
    [RelatedToEvents(new EventCategory[] {EventCategory.ReserveChanged})] ReservePoints = 19, // 0x00000013
    FloatingCounterRipostingSword = 20, // 0x00000014
    FloatingCounterBleedingSword = 21, // 0x00000015
    FloatingCounterFlyingFist = 22, // 0x00000016
    FloatingCounterPoisoningSkull = 23, // 0x00000017
    FloatingCounterHealingSkull = 24, // 0x00000018
    FloatingCounterDamagingSkull = 25, // 0x00000019
    FloatingCounterPunishingDagger = 26, // 0x0000001A
    FloatingCounterCorbakSouls = 27, // 0x0000001B
    FloatingCounterWildBoarSouls = 28, // 0x0000001C
    FloatingCounterRegenerationMaster = 29, // 0x0000001D
    FloatingCounterChibiDial = 30, // 0x0000001E
    FloatingCounterAvengerHourglass = 31, // 0x0000001F
    LifeMax = 32, // 0x00000020
    FloatingCounterNocturiansMaster = 33, // 0x00000021
    FloatingCounterSight = 34, // 0x00000022
    FloatingCounterTattooedFists = 35, // 0x00000023
    FloatingCounterTattooedEyes = 36, // 0x00000024
  }
}
