// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.ParametersUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Audio.UI;
using Ankama.Cube.Configuration;
using Ankama.Cube.States;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class ParametersUI : AbstractUI
  {
    [SerializeField]
    private Button m_parametersButton;
    [SerializeField]
    private Button m_bugReportButton;
    [SerializeField]
    private Button m_optionButton;
    [SerializeField]
    private Button m_quitButton;
    [SerializeField]
    private Button m_outMenuButton;
    [SerializeField]
    private CanvasGroup m_menu;
    [SerializeField]
    private float m_fadeDuration = 0.15f;
    [Header("Audio")]
    [SerializeField]
    private AudioEventUITriggerOnEvent m_openAudio;
    [SerializeField]
    private AudioEventUITriggerOnEvent m_closeAudio;
    public Action<bool> onShowMenu;
    public Action onOptionClick;
    public Action onQuitClick;
    private bool m_menuOpen;
    private Tween m_fadeTween;

    private void Start()
    {
      this.m_parametersButton.onClick.AddListener(new UnityAction(this.OnParametersClick));
      this.m_bugReportButton.gameObject.SetActive(ApplicationConfig.enableBugReport);
      this.m_bugReportButton.onClick.AddListener(new UnityAction(this.OnBugReportClick));
      this.m_optionButton.onClick.AddListener(new UnityAction(this.OnOptionClick));
      this.m_quitButton.onClick.AddListener(new UnityAction(this.OnQuitClick));
      this.m_outMenuButton.onClick.AddListener(new UnityAction(this.OnOutMenuClick));
      this.m_menu.gameObject.SetActive(false);
      this.m_menu.alpha = 0.0f;
    }

    private void OnParametersClick() => this.ShowMenu(!this.m_menuOpen);

    private void OnBugReportClick()
    {
      if (!BugReportState.isReady)
        return;
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("OptionUI", out stateLayer))
        stateLayer = StateManager.GetDefaultLayer();
      StateManager.SetActiveInputLayer(stateLayer);
      BugReportState childState = new BugReportState();
      childState.Initialize();
      stateLayer.GetChainEnd().SetChildState((StateContext) childState);
    }

    private void OnOptionClick()
    {
      this.ShowMenu(false);
      Action onOptionClick = this.onOptionClick;
      if (onOptionClick == null)
        return;
      onOptionClick();
    }

    private void OnOutMenuClick() => this.ShowMenu(false);

    private void OnQuitClick()
    {
      this.ShowMenu(false);
      Action onQuitClick = this.onQuitClick;
      if (onQuitClick == null)
        return;
      onQuitClick();
    }

    private void ShowMenu(bool value)
    {
      this.m_menuOpen = value;
      Tween fadeTween = this.m_fadeTween;
      if (fadeTween != null)
        fadeTween.Kill();
      if (this.m_menuOpen)
      {
        this.m_menu.gameObject.SetActive(true);
        this.m_fadeTween = (Tween) this.m_menu.DOFade(1f, this.m_fadeDuration);
        this.m_openAudio.Trigger();
      }
      else
      {
        this.m_fadeTween = (Tween) this.m_menu.DOFade(0.0f, this.m_fadeDuration).OnComplete<Tweener>(new TweenCallback(this.DeactivateMenu));
        this.m_closeAudio.Trigger();
      }
      Action<bool> onShowMenu = this.onShowMenu;
      if (onShowMenu == null)
        return;
      onShowMenu(value);
    }

    private void DeactivateMenu() => this.m_menu.gameObject.SetActive(false);

    public void SimulateOptionClick() => InputUtility.SimulateClickOn((Selectable) this.m_parametersButton);
  }
}
