// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.TabButton
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class TabButton : Toggle
  {
    [SerializeField]
    protected Image m_background;
    [SerializeField]
    protected Image m_border;
    [SerializeField]
    protected TabStyle m_style;
    private Sequence m_tweenSequence;

    protected override void Awake()
    {
      base.Awake();
      this.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
      this.OnValueChanged(this.isOn);
    }

    private void OnValueChanged(bool on) => this.DoStateTransition(this.currentSelectionState, false);

    protected override void OnEnable()
    {
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("TabButton " + this.name + " doesn't have a style defined !", 38, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\TabButton.cs");
        base.OnEnable();
      }
      else
        base.OnEnable();
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
      if (!this.gameObject.activeInHierarchy)
        return;
      if ((Object) this.m_style == (Object) null)
      {
        Log.Error("TabButton " + this.name + " doesn't have a style defined !", 70, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\TabButton.cs");
      }
      else
      {
        TabStyle.TabState tabState = this.m_style.disable;
        switch (state)
        {
          case Selectable.SelectionState.Normal:
            tabState = this.isOn ? this.m_style.selected : this.m_style.normal;
            break;
          case Selectable.SelectionState.Highlighted:
            tabState = this.isOn ? this.m_style.selected : this.m_style.highlight;
            break;
          case Selectable.SelectionState.Pressed:
            tabState = this.isOn ? this.m_style.selected : this.m_style.pressed;
            break;
          case Selectable.SelectionState.Disabled:
            tabState = this.m_style.disable;
            break;
        }
        Sequence tweenSequence = this.m_tweenSequence;
        if (tweenSequence != null)
          tweenSequence.Kill();
        if (instant)
        {
          if ((bool) (Object) this.m_background)
            this.m_background.color = tabState.backgroundColor;
          if (!(bool) (Object) this.m_border)
            return;
          this.m_border.color = tabState.borderColor;
        }
        else
        {
          this.m_tweenSequence = DOTween.Sequence();
          if ((bool) (Object) this.m_background)
            this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_background, tabState.backgroundColor, this.m_style.transitionDuration));
          if (!(bool) (Object) this.m_border)
            return;
          this.m_tweenSequence.Insert(0.0f, (Tween) DOTweenModuleUI.DOBlendableColor(this.m_border, tabState.borderColor, this.m_style.transitionDuration));
        }
      }
    }
  }
}
