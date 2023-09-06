// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.History.HistorySpellElement
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
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.History
{
  public class HistorySpellElement : 
    HistoryAbstractElement,
    ISpellTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private UISpriteTextRenderer m_costText;
    private SpellDefinition m_definition;
    private FightValueProvider m_tooltipValueProvider;
    private TooltipElementValues m_tooltipElementValues;

    public void Set(SpellStatus status, DynamicValueContext context, int cost)
    {
      this.m_definition = status.definition;
      this.m_tooltipValueProvider = new FightValueProvider((ICastableStatus) status);
      this.m_tooltipElementValues = TooltipWindowUtility.GetTooltipElementValues(this.m_definition, context);
      this.m_costText.text = cost.ToString();
      this.ApplyIllu(status.ownerPlayer.isLocalPlayer);
    }

    protected override bool HasIllu() => (UnityEngine.Object) this.m_definition != (UnityEngine.Object) null && this.m_definition.illustrationReference.hasValue;

    protected override IEnumerator LoadIllu(Action<Sprite, string> loadEndCallback)
    {
      SpellDefinition definition = this.m_definition;
      yield return (object) definition.LoadIllustrationAsync<Sprite>(definition.illustrationBundleName, definition.illustrationReference, loadEndCallback);
    }

    public override HistoryElementType type => HistoryElementType.Spell;

    public override ITooltipDataProvider tooltipProvider => (ITooltipDataProvider) this;

    public TooltipDataType tooltipDataType => TooltipDataType.Spell;

    public TooltipElementValues GetGaugeModifications() => this.m_tooltipElementValues;

    public int GetTitleKey() => this.m_definition.i18nNameId;

    public int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_tooltipValueProvider;

    public KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;
  }
}
