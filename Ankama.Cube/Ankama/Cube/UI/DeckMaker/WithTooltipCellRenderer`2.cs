// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.WithTooltipCellRenderer`2
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.DeckMaker
{
  public abstract class WithTooltipCellRenderer<T, U> : 
    CellRenderer<T, U>,
    ITooltipDataProvider,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler
    where U : IWithTooltipCellRendererConfigurator
  {
    protected IFightValueProvider m_valueProvider;
    private FightTooltip m_tooltip;
    private TooltipPosition m_tooltipPosition;

    public void SetTooltip(FightTooltip tooltip, TooltipPosition tooltipPosition)
    {
      this.m_tooltip = tooltip;
      this.m_tooltipPosition = tooltipPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if ((Object) this.m_tooltip == (Object) null || this.value == null || ItemDragNDropListener.instance.dragging)
        return;
      this.m_tooltip.Initialize((ITooltipDataProvider) this);
      this.m_tooltip.ShowAt(this.m_tooltipPosition, this.rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if ((Object) this.m_tooltip == (Object) null)
        return;
      this.m_tooltip.Hide();
    }

    public abstract TooltipDataType tooltipDataType { get; }

    public abstract int GetTitleKey();

    public abstract int GetDescriptionKey();

    public abstract IFightValueProvider GetValueProvider();

    public abstract KeywordReference[] keywordReferences { get; }

    public override void OnConfiguratorUpdate(bool instant)
    {
      ref U local1 = ref this.m_configurator;
      FightTooltip fightTooltip;
      if ((object) default (U) == null)
      {
        U u = local1;
        ref U local2 = ref u;
        if ((object) u == null)
        {
          fightTooltip = (FightTooltip) null;
          goto label_4;
        }
        else
          local1 = ref local2;
      }
      fightTooltip = local1.tooltip;
label_4:
      this.m_tooltip = fightTooltip;
      if ((object) this.m_configurator == null)
        return;
      this.m_tooltipPosition = this.m_configurator.tooltipPosition;
    }
  }
}
