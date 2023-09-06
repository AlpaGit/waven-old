// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.PropertiesUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;

namespace Ankama.Cube.Data
{
  public static class PropertiesUtility
  {
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventVoluntaryMove = new PropertyId[4]
    {
      PropertyId.Rooted,
      PropertyId.Stun,
      PropertyId.Petrify,
      PropertyId.Shadowed
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventMoveEffects = new PropertyId[1]
    {
      PropertyId.Unmovable
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventAction = new PropertyId[4]
    {
      PropertyId.CharacterActionForbidden,
      PropertyId.Stun,
      PropertyId.Petrify,
      PropertyId.Shadowed
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventMagicalDamage = new PropertyId[3]
    {
      PropertyId.DamageProof,
      PropertyId.MagicalDamageProof,
      PropertyId.Petrify
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventPhysicalDamage = new PropertyId[3]
    {
      PropertyId.DamageProof,
      PropertyId.PhysicalDamageProof,
      PropertyId.Petrify
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventMagicalHeal = new PropertyId[3]
    {
      PropertyId.HealProof,
      PropertyId.MagicalHealProof,
      PropertyId.Petrify
    };
    [PublicAPI]
    public static readonly PropertyId[] propertiesWhichPreventPhysicalHeal = new PropertyId[3]
    {
      PropertyId.HealProof,
      PropertyId.PhysicalHealProof,
      PropertyId.Petrify
    };

    [PublicAPI]
    public static bool PreventsAction(PropertyId property)
    {
      PropertyId[] whichPreventAction = PropertiesUtility.propertiesWhichPreventAction;
      int length = whichPreventAction.Length;
      for (int index = 0; index < length; ++index)
      {
        if (whichPreventAction[index] == property)
          return true;
      }
      return false;
    }

    public static bool IsSightProperty(PropertyId property)
    {
      PropertyId[] values = SightPropertyId.values;
      int length = values.Length;
      for (int index = 0; index < length; ++index)
      {
        if (values[index] == property)
          return true;
      }
      return false;
    }
  }
}
