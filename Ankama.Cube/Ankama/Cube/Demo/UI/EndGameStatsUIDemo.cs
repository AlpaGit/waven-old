// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.EndGameStatsUIDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio.UI;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class EndGameStatsUIDemo : BaseOpenCloseUI
  {
    [SerializeField]
    private Button m_quitButton;
    [SerializeField]
    private AbstractTextField m_victoryText;
    [SerializeField]
    private AbstractTextField m_defeatText;
    [SerializeField]
    private RawTextField m_gameTimeText;
    [SerializeField]
    private StatBoard m_statBoard;
    [SerializeField]
    private AudioEventUITriggerOnEnable m_winAudio;
    [SerializeField]
    private AudioEventUITriggerOnEnable m_looseAudio;
    public Action onContinueClick;

    private void Start() => this.m_quitButton.onClick.AddListener(new UnityAction(this.OnQuitClick));

    private void OnQuitClick()
    {
      Action onContinueClick = this.onContinueClick;
      if (onContinueClick == null)
        return;
      onContinueClick();
    }

    public void DoContinueClick() => InputUtility.SimulateClickOn((Selectable) this.m_quitButton);

    public IEnumerator Init(FightResult endResult, GameStatistics gameStatistics, int fightTime)
    {
      switch (endResult)
      {
        case FightResult.Draw:
        case FightResult.Defeat:
          this.m_victoryText.gameObject.SetActive(false);
          this.m_defeatText.gameObject.SetActive(true);
          this.m_looseAudio.gameObject.SetActive(true);
          break;
        case FightResult.Victory:
          this.m_victoryText.gameObject.SetActive(true);
          this.m_defeatText.gameObject.SetActive(false);
          this.m_winAudio.gameObject.SetActive(true);
          break;
      }
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) fightTime);
      this.m_gameTimeText.SetText("Temps de jeu : " + (timeSpan.Hours > 0 ? timeSpan.ToString("hh\\:mm\\:ss") : timeSpan.ToString("mm\\:ss")));
      yield return (object) this.m_statBoard.Init(gameStatistics);
    }

    public override IEnumerator OpenCoroutine()
    {
      Sequence boardSequence = this.m_statBoard.Open();
      yield return (object) base.OpenCoroutine();
      while (boardSequence.IsActive() || boardSequence.IsActive())
        yield return (object) null;
    }
  }
}
