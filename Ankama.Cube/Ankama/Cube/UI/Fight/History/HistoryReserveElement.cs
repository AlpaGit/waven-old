// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.History.HistoryReserveElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.History
{
  public class HistoryReserveElement : 
    HistoryAbstractElement,
    ITextTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private UISpriteTextRenderer m_valueText;
    private ReserveDefinition m_reserveDefinition;
    private FightValueProvider m_tooltipValueProvider;

    public void Set(PlayerStatus status, int valueBefore)
    {
      this.m_reserveDefinition = RuntimeDataHelper.GetReserveDefinitionFrom(status);
      this.m_tooltipValueProvider = new FightValueProvider((IDefinitionWithPrecomputedData) this.m_reserveDefinition, status.heroStatus.level);
      this.m_valueText.text = valueBefore.ToString();
      this.ApplyIllu(status.isLocalPlayer);
    }

    protected override bool HasIllu() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_reserveDefinition && this.m_reserveDefinition.illustrationReference.hasValue;

    protected override IEnumerator LoadIllu(Action<Sprite, string> loadEndCallback)
    {
      ReserveDefinition reserveDefinition = this.m_reserveDefinition;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) reserveDefinition))
        yield return (object) reserveDefinition.LoadIllustrationAsync<Sprite>(reserveDefinition.illustrationBundleName, reserveDefinition.illustrationReference, loadEndCallback);
    }

    public override HistoryElementType type => HistoryElementType.Reserve;

    public override ITooltipDataProvider tooltipProvider => (ITooltipDataProvider) this;

    public TooltipDataType tooltipDataType => TooltipDataType.Text;

    public int GetTitleKey() => !((UnityEngine.Object) null == (UnityEngine.Object) this.m_reserveDefinition) ? this.m_reserveDefinition.i18nNameId : 0;

    public int GetDescriptionKey() => !((UnityEngine.Object) null == (UnityEngine.Object) this.m_reserveDefinition) ? this.m_reserveDefinition.i18nDescriptionId : 0;

    public IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_tooltipValueProvider;

    public KeywordReference[] keywordReferences => !((UnityEngine.Object) null == (UnityEngine.Object) this.m_reserveDefinition) ? this.m_reserveDefinition.precomputedData.keywordReferences : (KeywordReference[]) null;
  }
}
