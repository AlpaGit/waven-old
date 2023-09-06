// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.MainUIDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class MainUIDemo : AbstractUI
  {
    [SerializeField]
    private Button m_returnButton;
    [SerializeField]
    private StepIndicator m_stateIndicator1;
    [SerializeField]
    private StepIndicator m_stateIndicator2;
    [SerializeField]
    private StepIndicator m_stateIndicator3;
    [SerializeField]
    private PlayerAvatarDemo m_playerAvatar;
    [SerializeField]
    private PlayableDirector m_open;
    [SerializeField]
    private PlayableDirector m_close;
    [SerializeField]
    private PlayableDirector m_showPlayerAvatar;
    [SerializeField]
    private PlayableDirector m_hidePlayerAvatar;
    [SerializeField]
    private PlayableDirector m_showStepMenu;
    [SerializeField]
    private PlayableDirector m_hideStepMenu;
    [SerializeField]
    private PlayableDirector m_gotoVersus;
    [SerializeField]
    private PlayableDirector m_gotoFight;
    private int m_stateIndex = -1;
    public Action onReturn;

    public Button returnButton => this.m_returnButton;

    public PlayerAvatarDemo playerAvatar => this.m_playerAvatar;

    private void Start()
    {
      this.m_playerAvatar.gameObject.SetActive(false);
      this.m_returnButton.onClick.AddListener((UnityAction) (() =>
      {
        Action onReturn = this.onReturn;
        if (onReturn == null)
          return;
        onReturn();
      }));
    }

    public void SimulateReturnClick() => InputUtility.SimulateClickOn((Selectable) this.m_returnButton);

    public void SetStateIndex(int idx, bool tween)
    {
      if (this.m_stateIndex == idx)
        return;
      switch (idx)
      {
        case 0:
          this.m_stateIndicator1.SetState(StepIndicator.State.Enable, tween);
          this.m_stateIndicator2.SetState(StepIndicator.State.Disable, tween);
          this.m_stateIndicator3.SetState(StepIndicator.State.Disable, tween);
          break;
        case 1:
          this.m_stateIndicator1.SetState(StepIndicator.State.Disable, tween);
          this.m_stateIndicator2.SetState(StepIndicator.State.Enable, tween);
          this.m_stateIndicator3.SetState(StepIndicator.State.Disable, tween);
          break;
        case 2:
          this.m_stateIndicator1.SetState(StepIndicator.State.Disable, tween);
          this.m_stateIndicator2.SetState(StepIndicator.State.Disable, tween);
          this.m_stateIndicator3.SetState(StepIndicator.State.Enable, tween);
          break;
      }
      this.m_stateIndex = idx;
    }

    public void ShowPlayerAvatarAnim(bool value)
    {
      this.m_playerAvatar.gameObject.SetActive(true);
      if (value)
      {
        this.m_showPlayerAvatar.time = 0.0;
        this.m_showPlayerAvatar.Play();
      }
      else
      {
        this.m_hidePlayerAvatar.time = 0.0;
        this.m_hidePlayerAvatar.Play();
      }
    }

    public void ShowStepMenuAnim(bool value)
    {
      if (value)
      {
        this.m_showStepMenu.time = 0.0;
        this.m_showStepMenu.Play();
      }
      else
      {
        this.m_hideStepMenu.time = 0.0;
        this.m_hideStepMenu.Play();
      }
    }

    public IEnumerator GotoFightAnim()
    {
      yield return (object) BaseOpenCloseUI.PlayDirector(this.m_gotoFight);
    }

    public void PlayVersusAnim()
    {
      this.m_gotoVersus.time = 0.0;
      this.m_gotoVersus.Play();
    }

    public void Open()
    {
      this.m_open.time = 0.0;
      this.m_open.Play();
    }

    public IEnumerator CloseCoroutine()
    {
      yield return (object) BaseOpenCloseUI.PlayDirector(this.m_close);
    }
  }
}
