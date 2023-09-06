// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.PopupInfoUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.Code.UI
{
  public class PopupInfoUI : BaseOpenCloseUI
  {
    [Header("Components")]
    [SerializeField]
    protected RawTextField m_titleText;
    [SerializeField]
    protected RawTextField m_descriptionText;
    [SerializeField]
    protected Image m_blackBackground;
    [SerializeField]
    protected Image m_windowBackground;
    [Header("Buttons")]
    [SerializeField]
    protected Button m_buttonBackground;
    [SerializeField]
    protected AnimatedTextButton m_buttonNormal;
    [SerializeField]
    protected AnimatedTextButton m_buttonNegative;
    [SerializeField]
    protected AnimatedTextButton m_buttonCancel;
    [Header("Styles")]
    [SerializeField]
    protected PopupInfoStyle[] m_styles;
    public Action closeAction;
    private readonly List<Button> m_buttons = new List<Button>();
    private int m_selectedIndex;

    public void Initialize(PopupInfo data)
    {
      this.m_useBlur = data.useBlur;
      this.m_buttonNormal.gameObject.SetActive(false);
      this.m_buttonNegative.gameObject.SetActive(false);
      this.m_buttonCancel.gameObject.SetActive(false);
      PopupInfoUI.ApplyRawText(this.m_titleText, data.title);
      PopupInfoUI.ApplyRawText(this.m_descriptionText, data.message);
      ButtonData[] buttons = data.buttons;
      if (buttons != null)
      {
        int length = buttons.Length;
        for (int index = 0; index < length; ++index)
          this.AddButton(buttons[index]);
      }
      if (data.closeOnBackgroundClick)
        this.m_buttonBackground.onClick.AddListener(new UnityAction(this.DoClose));
      PopupInfoStyle style = this.GetStyle(data.style);
      this.m_titleText.color = style.titleColor;
      this.m_descriptionText.color = style.textColor;
      this.m_selectedIndex = data.selectedButton - 1;
      if (this.m_selectedIndex < 0 || this.m_selectedIndex >= this.m_buttons.Count)
        this.m_selectedIndex = 0;
      if (this.m_buttons.Count <= 0)
        return;
      this.m_buttons[this.m_selectedIndex].Select();
    }

    private PopupInfoStyle GetStyle(PopupStyle style)
    {
      PopupInfoStyle[] styles = this.m_styles;
      int length = styles.Length;
      for (int index = 0; index < length; ++index)
      {
        PopupInfoStyle style1 = styles[index];
        if (style1.style == style)
          return style1;
      }
      Log.Error(string.Format("Cannot find style {0}", (object) style), 96, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\PopupInfo\\PopupInfoUI.cs");
      return this.m_styles[0];
    }

    public void RemoveListeners()
    {
      List<Button> buttons = this.m_buttons;
      int count = buttons.Count;
      for (int index = 0; index < count; ++index)
        buttons[index].onClick.RemoveAllListeners();
      this.m_buttonBackground.onClick.RemoveAllListeners();
    }

    private void AddButton(ButtonData data)
    {
      if (!data.isValid)
        return;
      AnimatedTextButton freeButton = this.GetFreeButton(data.style);
      freeButton.gameObject.SetActive(true);
      freeButton.transform.SetAsLastSibling();
      if (data.textOverride.isValid)
        PopupInfoUI.ApplyText(freeButton.textField, data.textOverride);
      if (data.onClick != null)
        freeButton.onClick.AddListener(new UnityAction(data.onClick.Invoke));
      if (data.closeOnClick)
        freeButton.onClick.AddListener(new UnityAction(this.DoClose));
      this.m_buttons.Add((Button) freeButton);
    }

    private AnimatedTextButton GetFreeButton(ButtonStyle style)
    {
      AnimatedTextButton original;
      switch (style)
      {
        case ButtonStyle.Normal:
          original = this.m_buttonNormal;
          break;
        case ButtonStyle.Negative:
          original = this.m_buttonNegative;
          break;
        case ButtonStyle.Cancel:
          original = this.m_buttonCancel;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (style), (object) style, (string) null);
      }
      if (!original.gameObject.activeSelf)
        return original;
      AnimatedTextButton freeButton = UnityEngine.Object.Instantiate<AnimatedTextButton>(original);
      freeButton.transform.SetParent(original.transform.parent, false);
      return freeButton;
    }

    private void DoClose()
    {
      Action closeAction = this.closeAction;
      if (closeAction == null)
        return;
      closeAction();
    }

    private static void ApplyRawText(RawTextField text, RawTextData data)
    {
      text.gameObject.SetActive(data.isValid);
      if (!data.isValid)
        return;
      text.SetText(data.formattedText);
    }

    private static void ApplyText(TextField text, TextData data)
    {
      text.gameObject.SetActive(data.isValid);
      if (!data.isValid)
        return;
      text.SetText(data.textId.Value, data.valueProvider);
    }

    public void DoClickSelected()
    {
      List<Button> buttons = this.m_buttons;
      if (buttons.Count == 0)
        return;
      Button button = buttons[this.m_selectedIndex];
      if (!((UnityEngine.Object) button.gameObject == (UnityEngine.Object) EventSystem.current.currentSelectedGameObject))
        return;
      InputUtility.SimulateClickOn((Selectable) button);
    }

    public void SelectNext()
    {
      List<Button> buttons = this.m_buttons;
      int count = buttons.Count;
      if (count == 0)
        return;
      this.m_selectedIndex = (this.m_selectedIndex + 1) % count;
      buttons[this.m_selectedIndex].Select();
    }
  }
}
