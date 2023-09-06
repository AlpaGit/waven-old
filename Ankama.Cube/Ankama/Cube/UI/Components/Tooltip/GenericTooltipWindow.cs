// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.Tooltip.GenericTooltipWindow
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components.Tooltip
{
  public class GenericTooltipWindow : AbstractTooltipWindow
  {
    [SerializeField]
    private TextField m_titleText;
    [SerializeField]
    private TextField m_text;
    [SerializeField]
    private int m_multilineMaxWidth;
    private LayoutElement m_layoutElement;

    public override void Awake()
    {
      base.Awake();
      this.m_layoutElement = this.gameObject.AddComponent<LayoutElement>();
    }

    public void SetText(
      int textKeyId,
      IValueProvider valueProvider = null,
      int? titleTextKeyId = null,
      IValueProvider titleValueProvider = null,
      bool multiline = true)
    {
      this.m_layoutElement.preferredWidth = multiline ? (float) this.m_multilineMaxWidth : -1f;
      this.m_text.SetText(textKeyId, valueProvider);
      this.m_titleText.gameObject.SetActive(titleTextKeyId.HasValue);
      if (!titleTextKeyId.HasValue)
        return;
      this.m_titleText.SetText(titleTextKeyId.Value, titleValueProvider);
    }

    public void SetText(
      string textKeyName,
      IValueProvider valueProvider = null,
      string titleTextKeyName = null,
      IValueProvider titleValueProvider = null,
      bool multiline = true)
    {
      this.m_layoutElement.preferredWidth = multiline ? (float) this.m_multilineMaxWidth : -1f;
      this.m_text.SetText(textKeyName, valueProvider);
      this.m_titleText.gameObject.SetActive(titleTextKeyName != null);
      if (titleTextKeyName == null)
        return;
      this.m_titleText.SetText(titleTextKeyName, titleValueProvider);
    }
  }
}
