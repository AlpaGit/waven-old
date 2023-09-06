// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.FightValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class FightValueProvider : IFightValueProvider, IValueProvider
  {
    private readonly int m_damageModifier;
    private readonly int m_healModifier;
    private readonly Tuple<int, int> m_range;
    private readonly IReadOnlyList<ILevelOnlyDependant> m_dynamicValues;

    public int level { get; }

    public FightValueProvider([NotNull] ICastableStatus castableStatus)
      : this((AbstractDynamicFightValueProvider) new CastableFightValueProvider(castableStatus))
    {
    }

    public FightValueProvider(AbstractDynamicFightValueProvider dynamicProvider)
    {
      this.level = dynamicProvider.level;
      this.m_dynamicValues = dynamicProvider.dynamicValues;
      this.m_damageModifier = dynamicProvider.GetDamageModifierValue();
      this.m_healModifier = dynamicProvider.GetHealModifierValue();
      this.m_range = dynamicProvider.GetRange();
    }

    public FightValueProvider([NotNull] IDefinitionWithPrecomputedData definition, int level)
    {
      this.level = level;
      this.m_damageModifier = 0;
      this.m_healModifier = 0;
      this.m_range = FightValueProvider.CreateRange(definition, level);
      this.m_dynamicValues = definition.precomputedData.dynamicValueReferences;
    }

    public int GetValueInt(string name) => this.m_dynamicValues.GetValueInt(name, this.level);

    public int GetDamageModifierValue() => this.m_damageModifier;

    public int GetHealModifierValue() => this.m_healModifier;

    public Tuple<int, int> GetRange() => this.m_range;

    public string GetValue(string name) => this.GetValueInt(name).ToString();

    private static Tuple<int, int> CreateRange(IDefinitionWithPrecomputedData definition, int level)
    {
      CharacterDefinition characterDefinition = definition as CharacterDefinition;
      return (UnityEngine.Object) characterDefinition == (UnityEngine.Object) null || characterDefinition.actionRange == null ? (Tuple<int, int>) null : new Tuple<int, int>(characterDefinition.actionRange.min.GetValueWithLevel(level), characterDefinition.actionRange.max.GetValueWithLevel(level));
    }
  }
}
