// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.TurnFeedbackUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio.UI;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class TurnFeedbackUI : MonoBehaviour
  {
    [SerializeField]
    private TextField m_text;
    [SerializeField]
    private TextField m_textEffect;
    [SerializeField]
    private Image m_picto;
    [SerializeField]
    private RawTextField m_playerName;
    [SerializeField]
    private PlayableDirector m_openCloseDirector;
    [SerializeField]
    private TurnFeedbackData m_data;
    [SerializeField]
    private AudioEventUITrigger m_playerTurnSoundTrigger;
    private Coroutine m_animCoroutine;
    private PlayableDirector m_playingDirector;
    private bool m_isAnimating;

    public bool isAnimating => this.m_isAnimating;

    private void OnDisable()
    {
      if (this.m_animCoroutine != null)
      {
        this.StopCoroutine(this.m_animCoroutine);
        this.m_animCoroutine = (Coroutine) null;
      }
      this.m_isAnimating = false;
    }

    public void Show(TurnFeedbackUI.Type type, string playerName, Action onComplete = null)
    {
      TurnFeedbackData.PlayerSideData data;
      if (!this.GetData(type, out data))
        return;
      this.m_playerName.SetText(playerName);
      this.m_playerName.color = data.nameColor;
      this.m_text.SetText(data.messageKey);
      this.m_textEffect.SetText(data.messageKey);
      this.m_text.color = data.titleColor;
      this.m_textEffect.color = data.titleColor;
      this.m_picto.sprite = data.icon;
      this.m_playerTurnSoundTrigger.enabled = type == TurnFeedbackUI.Type.Player;
      this.PlayAnim(this.m_openCloseDirector, onComplete);
    }

    private bool GetData(TurnFeedbackUI.Type type, out TurnFeedbackData.PlayerSideData data)
    {
      switch (type)
      {
        case TurnFeedbackUI.Type.Player:
          data = this.m_data.player;
          return true;
        case TurnFeedbackUI.Type.PlayerTeam:
          data = this.m_data.playerTeam;
          return true;
        case TurnFeedbackUI.Type.Opponent:
          data = this.m_data.opponent;
          return true;
        case TurnFeedbackUI.Type.OpponentTeam:
          data = this.m_data.opponentTeam;
          return true;
        case TurnFeedbackUI.Type.Boss:
          data = this.m_data.boss;
          return true;
        default:
          Log.Error(string.Format("type not handled {0}", (object) type), 95, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\TurnFeedback\\TurnFeedbackUI.cs");
          data = new TurnFeedbackData.PlayerSideData();
          return false;
      }
    }

    private void PlayAnim(PlayableDirector director, Action onComplete = null)
    {
      this.m_isAnimating = true;
      this.gameObject.SetActive(true);
      if (this.m_animCoroutine != null)
        this.StopCoroutine(this.m_animCoroutine);
      if ((UnityEngine.Object) this.m_playingDirector != (UnityEngine.Object) null && this.m_playingDirector.playableGraph.IsValid() && !this.m_playingDirector.playableGraph.IsDone())
        this.m_playingDirector.Stop();
      this.m_playingDirector = director;
      director.time = 0.0;
      director.Play();
      this.m_animCoroutine = this.StartCoroutine(this.AnimCoroutine(director, onComplete));
    }

    private IEnumerator AnimCoroutine(PlayableDirector director, Action onComplete = null)
    {
      TurnFeedbackUI turnFeedbackUi = this;
      PlayableGraph graph = director.playableGraph;
      while (graph.IsValid() && !graph.IsDone())
        yield return (object) null;
      turnFeedbackUi.gameObject.SetActive(false);
      turnFeedbackUi.m_isAnimating = false;
      Action action = onComplete;
      if (action != null)
        action();
      turnFeedbackUi.m_animCoroutine = (Coroutine) null;
    }

    public enum Type
    {
      Player,
      PlayerTeam,
      Opponent,
      OpponentTeam,
      Boss,
    }
  }
}
