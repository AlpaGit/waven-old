// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.TooltipDataProviderFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.UI.Fight.Windows
{
  public static class TooltipDataProviderFactory
  {
    [CanBeNull]
    public static ITooltipDataProvider Create(
      KeywordReference keywordReference,
      IFightValueProvider valueProvider)
    {
      if (keywordReference.type == ObjectReference.Type.None)
        return (ITooltipDataProvider) new TooltipDataProviderFactory.KeywordTooltipDataProvider(keywordReference.keyword, valueProvider);
      if (valueProvider == null)
      {
        Log.Error(string.Format("No value provider defined for {0}", (object) keywordReference), 24, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\Tooltip\\TooltipDataProviderFactory.cs");
        return (ITooltipDataProvider) null;
      }
      IDefinitionWithTooltip definition = ObjectReference.GetObject(keywordReference.type, keywordReference.id);
      return definition != null ? TooltipDataProviderFactory.Create<IDefinitionWithTooltip>(definition, valueProvider.level) : (ITooltipDataProvider) null;
    }

    [CanBeNull]
    public static ITooltipDataProvider Create<T>(T definition, int level) where T : IDefinitionWithTooltip
    {
      SpellDefinition spell = (object) definition as SpellDefinition;
      if ((UnityEngine.Object) spell != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(spell, level);
      CompanionDefinition companion = (object) definition as CompanionDefinition;
      if ((UnityEngine.Object) companion != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(companion, level);
      SummoningDefinition summoning = (object) definition as SummoningDefinition;
      if ((UnityEngine.Object) summoning != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(summoning, level);
      WeaponDefinition weapon = (object) definition as WeaponDefinition;
      if ((UnityEngine.Object) weapon != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(weapon, level);
      FloorMechanismDefinition mechanism1 = (object) definition as FloorMechanismDefinition;
      if ((UnityEngine.Object) mechanism1 != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(mechanism1, level);
      ObjectMechanismDefinition mechanism2 = (object) definition as ObjectMechanismDefinition;
      if ((UnityEngine.Object) mechanism2 != (UnityEngine.Object) null)
        return TooltipDataProviderFactory.Create(mechanism2, level);
      ReserveDefinition mechanism3 = (object) definition as ReserveDefinition;
      return (UnityEngine.Object) mechanism3 != (UnityEngine.Object) null ? TooltipDataProviderFactory.Create(mechanism3, level) : throw new ArgumentOutOfRangeException();
    }

    private static ITooltipDataProvider Create(SpellDefinition spell, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.SpellTooltipDataProvider(spell, level);

    private static ITooltipDataProvider Create(CompanionDefinition companion, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.CharacterTooltipDataProvider<CompanionDefinition>(companion, level);

    private static ITooltipDataProvider Create(SummoningDefinition summoning, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.CharacterTooltipDataProvider<SummoningDefinition>(summoning, level);

    private static ITooltipDataProvider Create(WeaponDefinition weapon, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.CharacterTooltipDataProvider<WeaponDefinition>(weapon, level);

    private static ITooltipDataProvider Create(ObjectMechanismDefinition mechanism, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.ObjectMechanismTooltipDataProvider(mechanism, level);

    private static ITooltipDataProvider Create(FloorMechanismDefinition mechanism, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.FloorMechanismTooltipDataProvider(mechanism, level);

    private static ITooltipDataProvider Create(ReserveDefinition mechanism, int level) => (ITooltipDataProvider) new TooltipDataProviderFactory.ReserveTooltipDataProvider(mechanism, level);

    private class CharacterTooltipDataProvider<T> : 
      TooltipDataProviderFactory.TooltipDataProvider<T>,
      ICharacterTooltipDataProvider,
      ITooltipDataProvider
      where T : CharacterDefinition, IDefinitionWithTooltip
    {
      public CharacterTooltipDataProvider(T definition, int level)
        : base(TooltipDataType.Character, definition, level)
      {
      }

      public ActionType GetActionType() => this.m_definition.actionType;

      public TooltipActionIcon GetActionIcon() => TooltipWindowUtility.GetActionIcon((CharacterDefinition) this.m_definition);

      public bool TryGetActionValue(out int value)
      {
        if (this.m_definition.actionValue == null)
        {
          value = 0;
          return false;
        }
        value = this.m_definition.actionValue.GetValueWithLevel(this.m_valueProvider.level);
        return true;
      }

      public int GetLifeValue() => this.m_definition.life.GetValueWithLevel(this.m_valueProvider.level);

      public int GetMovementValue() => this.m_definition.movementPoints.GetValueWithLevel(this.m_valueProvider.level);
    }

    private class ObjectMechanismTooltipDataProvider : 
      TooltipDataProviderFactory.TooltipDataProvider<ObjectMechanismDefinition>,
      IObjectMechanismTooltipDataProvider,
      ITooltipDataProvider
    {
      public ObjectMechanismTooltipDataProvider(ObjectMechanismDefinition definition, int level)
        : base(TooltipDataType.ObjectMechanism, definition, level)
      {
      }

      public int GetArmorValue() => this.m_definition.baseMecaLife.GetValueWithLevel(this.m_valueProvider.level);
    }

    private class FloorMechanismTooltipDataProvider : 
      TooltipDataProviderFactory.TooltipDataProvider<FloorMechanismDefinition>,
      IFloorMechanismTooltipDataProvider,
      ITooltipDataProvider
    {
      public FloorMechanismTooltipDataProvider(FloorMechanismDefinition definition, int level)
        : base(TooltipDataType.FloorMechanism, definition, level)
      {
      }
    }

    private class SpellTooltipDataProvider : 
      TooltipDataProviderFactory.TooltipDataProvider<SpellDefinition>,
      ISpellTooltipDataProvider,
      ITooltipDataProvider
    {
      public SpellTooltipDataProvider(SpellDefinition definition, int level)
        : base(TooltipDataType.Spell, definition, level)
      {
      }

      public TooltipElementValues GetGaugeModifications() => TooltipWindowUtility.GetTooltipElementValues(this.m_definition, (DynamicValueContext) null);
    }

    private class ReserveTooltipDataProvider : 
      TooltipDataProviderFactory.TooltipDataProvider<ReserveDefinition>,
      ITextTooltipDataProvider,
      ITooltipDataProvider
    {
      public ReserveTooltipDataProvider(ReserveDefinition definition, int level)
        : base(TooltipDataType.Text, definition, level)
      {
      }
    }

    private class KeywordTooltipDataProvider : ITextTooltipDataProvider, ITooltipDataProvider
    {
      private readonly int m_titleKey;
      private readonly int m_descriptionKey;
      private readonly IFightValueProvider m_valueProvider;

      public KeywordTooltipDataProvider(string keyword, IFightValueProvider valueProvider)
      {
        RuntimeData.TryGetTextKeyId("KEYWORD." + keyword + ".NAME", out this.m_titleKey);
        RuntimeData.TryGetTextKeyId("KEYWORD." + keyword + ".DESC", out this.m_descriptionKey);
        this.m_valueProvider = valueProvider;
      }

      public TooltipDataType tooltipDataType => TooltipDataType.Text;

      public int GetTitleKey() => this.m_titleKey;

      public int GetDescriptionKey() => this.m_descriptionKey;

      public IFightValueProvider GetValueProvider() => this.m_valueProvider;

      public KeywordReference[] keywordReferences => (KeywordReference[]) null;
    }

    private abstract class TooltipDataProvider<T> : ITooltipDataProvider where T : class, IDefinitionWithTooltip
    {
      protected readonly T m_definition;
      protected readonly IFightValueProvider m_valueProvider;

      protected TooltipDataProvider(TooltipDataType type, T definition, int level)
      {
        this.tooltipDataType = type;
        this.m_definition = definition;
        this.m_valueProvider = (IFightValueProvider) new FightValueProvider((IDefinitionWithPrecomputedData) definition, level);
      }

      public TooltipDataType tooltipDataType { get; }

      public int GetTitleKey() => this.m_definition.i18nNameId;

      public int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

      public IFightValueProvider GetValueProvider() => this.m_valueProvider;

      public KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;
    }
  }
}
