// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.History.HistoryCompanionElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.History
{
  public class HistoryCompanionElement : 
    HistoryAbstractElement,
    ICharacterTooltipDataProvider,
    ITooltipDataProvider
  {
    private CompanionDefinition m_definition;
    private FightValueProvider m_tooltipValueProvider;
    private int m_movementPoints;
    private int m_life;
    private int? m_actionValue;

    public void Set(ReserveCompanionStatus companion)
    {
      ReserveCompanionValueContext valueContext = companion.CreateValueContext();
      this.m_definition = companion.definition;
      this.m_tooltipValueProvider = new FightValueProvider((ICastableStatus) companion);
      this.m_life = this.m_definition.life.GetValueWithLevel(valueContext.level);
      this.m_movementPoints = this.m_definition.movementPoints.GetValueWithLevel(valueContext.level);
      this.m_actionValue = HistoryCompanionElement.ExtractActionValue(this.m_definition, (DynamicValueContext) valueContext);
      this.ApplyIllu(companion.currentPlayer.isLocalPlayer);
    }

    protected override bool HasIllu() => (UnityEngine.Object) this.m_definition != (UnityEngine.Object) null && this.m_definition.illustrationReference.hasValue;

    protected override IEnumerator LoadIllu(Action<Sprite, string> loadEndCallback)
    {
      CompanionDefinition definition = this.m_definition;
      yield return (object) definition.LoadIllustrationAsync<Sprite>(definition.illustrationBundleName, definition.illustrationReference, loadEndCallback);
    }

    public override HistoryElementType type => HistoryElementType.Companion;

    public override ITooltipDataProvider tooltipProvider => (ITooltipDataProvider) this;

    public TooltipDataType tooltipDataType => TooltipDataType.Character;

    public ActionType GetActionType() => this.m_definition.actionType;

    public TooltipActionIcon GetActionIcon() => TooltipWindowUtility.GetActionIcon((CharacterDefinition) this.m_definition);

    public bool TryGetActionValue(out int value)
    {
      if (this.m_actionValue.HasValue)
      {
        value = this.m_actionValue.Value;
        return true;
      }
      value = 0;
      return false;
    }

    public int GetLifeValue() => this.m_life;

    public int GetMovementValue() => this.m_movementPoints;

    public int GetTitleKey() => this.m_definition.i18nNameId;

    public int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_tooltipValueProvider;

    public KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;

    private static int? ExtractActionValue(
      CompanionDefinition definition,
      DynamicValueContext context)
    {
      return definition.actionValue?.GetValueWithLevel(context.level);
    }
  }
}
