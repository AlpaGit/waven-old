// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.Tooltip.GenericTooltipRequester
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components.Tooltip
{
  [RequireComponent(typeof (RectTransform))]
  public class GenericTooltipRequester : 
    MonoBehaviour,
    IPointerEnterHandler,
    IEventSystemHandler,
    IPointerExitHandler
  {
    [SerializeField]
    private GenericTooltipWindow m_tooltip;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    [TextKey]
    private int m_textKeyId;
    [SerializeField]
    private bool m_withTitle;
    [SerializeField]
    [TextKey]
    private int m_titleTextKeyId;
    [SerializeField]
    private bool m_multiline = true;
    private RectTransform m_rectTransform;
    private bool m_showingTooltip;

    private void Awake() => this.m_rectTransform = this.GetComponent<RectTransform>();

    public void OnPointerEnter(PointerEventData eventData)
    {
      if ((Object) this.m_tooltip == (Object) null)
        return;
      int? titleTextKeyId = new int?();
      if (this.m_withTitle)
        titleTextKeyId = new int?(this.m_titleTextKeyId);
      this.m_tooltip.SetText(this.m_textKeyId, titleTextKeyId: titleTextKeyId, multiline: this.m_multiline);
      this.m_tooltip.ShowAt(this.m_tooltipPosition, this.m_rectTransform);
      this.m_showingTooltip = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if ((Object) this.m_tooltip == (Object) null)
        return;
      this.m_tooltip.Hide();
      this.m_showingTooltip = false;
    }

    private void OnDisable()
    {
      if ((Object) this.m_tooltip == (Object) null || !this.m_showingTooltip)
        return;
      this.m_tooltip.Hide();
      this.m_showingTooltip = false;
    }
  }
}
