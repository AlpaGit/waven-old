// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders.DynamicFightValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders
{
  public class DynamicFightValueProvider : AbstractDynamicFightValueProvider
  {
    [NotNull]
    private readonly IDynamicValueSource m_source;

    public DynamicFightValueProvider([NotNull] IDynamicValueSource source, int level)
      : base(source.dynamicValues, level)
    {
      this.m_source = source;
    }

    public override int GetDamageModifierValue() => !(this.m_source is IEntityWithAction source) ? base.GetDamageModifierValue() : source.physicalDamageBoost;

    public override int GetHealModifierValue() => !(this.m_source is IEntityWithAction source) ? base.GetHealModifierValue() : source.physicalHealBoost;

    public override Tuple<int, int> GetRange() => !(this.m_source is IEntityWithAction source) || !source.hasRange ? (Tuple<int, int>) null : new Tuple<int, int>(source.rangeMin, source.rangeMax);
  }
}
