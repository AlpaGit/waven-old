// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.EndOfTurnButtonRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public sealed class EndOfTurnButtonRework : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField]
    private TextField m_text;
    [SerializeField]
    private RawTextField m_textTurnTime;
    [SerializeField]
    private Image m_timeFilling;
    [Header("Colors")]
    [SerializeField]
    private FightMapFeedbackColors m_colors;
    [Header("Time Limit Animation")]
    [SerializeField]
    private Animator m_timerTextAnimator;
    [SerializeField]
    private Gradient m_warningColor;
    [SerializeField]
    private Button m_button;
    [Header("Audio Events")]
    [SerializeField]
    private UnityEvent m_onEndOfTurn;
    [SerializeField]
    private UnityEvent m_onEndOfTurnBeginAlert;
    [SerializeField]
    private UnityEvent m_onEndOfTurnEndAlert;
    private static readonly int s_warningHash = Animator.StringToHash("Warning");
    private static readonly int s_normalHash = Animator.StringToHash("Normal");
    private static readonly int s_alertHash = Animator.StringToHash("Alert");
    public Action onClick;
    private int m_turnDuration;
    private EndOfTurnButtonRework.State m_state;
    private bool m_running;
    private float m_turnStartTime;
    private EndOfTurnButtonRework.TimerState m_currentTimerState;
    private Tween m_timerColorTween;
    private int m_previousDisplayedTime = int.MinValue;

    public void SetState(EndOfTurnButtonRework.State value)
    {
      this.m_state = value;
      this.m_button.interactable = value == EndOfTurnButtonRework.State.LocalPlayer;
      this.RefreshStateView();
    }

    public void StartTurn(int turnIndex, int turnDuration)
    {
      this.m_turnDuration = turnDuration;
      this.UpdateTimerState(this.m_turnDuration);
      this.m_turnStartTime = Time.unscaledTime;
      this.m_running = true;
      this.ResetTimer();
    }

    public void EndTurn()
    {
      this.ResetTimer();
      this.m_running = false;
    }

    public void ShowEndOfTurn() => this.m_onEndOfTurn.Invoke();

    private void Awake()
    {
      this.m_button.onClick.AddListener(new UnityAction(this.DoClick));
      this.ResetTimer();
    }

    private void Update()
    {
      if (!this.m_running)
        return;
      float turnTime = Time.unscaledTime - this.m_turnStartTime;
      this.RefreshTime((int) ((double) this.m_turnDuration - (double) turnTime), turnTime);
    }

    private void RefreshStateView()
    {
      switch (this.m_state)
      {
        case EndOfTurnButtonRework.State.None:
          this.m_timeFilling.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
          this.m_text.SetText(0);
          break;
        case EndOfTurnButtonRework.State.LocalPlayer:
          this.m_timeFilling.color = this.m_colors.GetPlayerColor(PlayerType.Player);
          this.m_text.SetText(51179);
          break;
        case EndOfTurnButtonRework.State.LocalPlayerTeam:
          this.m_timeFilling.color = this.m_colors.GetPlayerColor(PlayerType.Ally);
          this.m_text.SetText(0);
          break;
        case EndOfTurnButtonRework.State.OpponentTeam:
          this.m_timeFilling.color = this.m_colors.GetPlayerColor(PlayerType.Opponent | PlayerType.Local);
          this.m_text.SetText(85537);
          break;
        default:
          throw new ArgumentOutOfRangeException("m_state", (object) this.m_state, (string) null);
      }
    }

    private void UpdateTimerState(int secondsRemaining)
    {
      if ((UnityEngine.Object) this.m_timerTextAnimator == (UnityEngine.Object) null)
        return;
      EndOfTurnButtonRework.TimerState timerState = EndOfTurnButtonRework.TimerState.Normal;
      if (secondsRemaining < 0 || secondsRemaining > 15)
        timerState = EndOfTurnButtonRework.TimerState.Normal;
      else if (secondsRemaining > 5 && secondsRemaining <= 15)
        timerState = EndOfTurnButtonRework.TimerState.Warning;
      else if (secondsRemaining <= 5 && secondsRemaining >= 0)
        timerState = EndOfTurnButtonRework.TimerState.Alert;
      if (timerState == this.m_currentTimerState)
        return;
      Tween timerColorTween = this.m_timerColorTween;
      if (timerColorTween != null)
        timerColorTween.Kill();
      this.m_timerColorTween = (Tween) null;
      this.m_currentTimerState = timerState;
      switch (timerState)
      {
        case EndOfTurnButtonRework.TimerState.Normal:
          this.m_timerTextAnimator.SetTrigger(EndOfTurnButtonRework.s_normalHash);
          break;
        case EndOfTurnButtonRework.TimerState.Warning:
          this.m_timerTextAnimator.SetTrigger(EndOfTurnButtonRework.s_warningHash);
          Sequence s = DOTween.Sequence();
          s.Insert(0.0f, (Tween) DOTweenModuleUI.DOColor(this.m_timeFilling, this.m_warningColor.Evaluate(1f), 0.5f));
          s.Insert(0.0f, (Tween) this.m_textTurnTime.DOColor(this.m_warningColor.Evaluate(1f), 0.5f));
          this.m_timerColorTween = (Tween) s;
          break;
        case EndOfTurnButtonRework.TimerState.Alert:
          this.m_timerTextAnimator.SetTrigger(EndOfTurnButtonRework.s_alertHash);
          this.m_textTurnTime.DOColor(this.m_warningColor.Evaluate(0.0f), 0.5f);
          this.m_timerColorTween = (Tween) DOTweenModuleUI.DOColor(this.m_timeFilling, this.m_warningColor.Evaluate(0.0f), 0.5f).SetLoops<Tweener>(-1, LoopType.Yoyo);
          this.m_onEndOfTurnBeginAlert.Invoke();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void RefreshTime(int remainingTimeInSeconds, float turnTime)
    {
      this.m_timeFilling.fillAmount = Mathf.Clamp01(turnTime / (float) Mathf.Max(1, this.m_turnDuration));
      if (this.m_previousDisplayedTime == remainingTimeInSeconds)
        return;
      this.m_previousDisplayedTime = remainingTimeInSeconds;
      this.UpdateTimerState(remainingTimeInSeconds);
      if (!(bool) (UnityEngine.Object) this.m_textTurnTime)
        return;
      if (remainingTimeInSeconds < 0)
      {
        this.m_textTurnTime.color = Color.white;
        this.m_textTurnTime.SetText("");
      }
      else
        this.m_textTurnTime.SetText(remainingTimeInSeconds.ToString());
    }

    private void ResetTimer()
    {
      this.RefreshTime(-1, 0.0f);
      this.m_onEndOfTurnEndAlert.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData) => this.DoClick();

    public void SimulateClick() => this.DoClick();

    private void DoClick()
    {
      if (this.m_state != EndOfTurnButtonRework.State.LocalPlayer || UIManager.instance.userInteractionLocked || DragNDropListener.instance.dragging)
        return;
      Action onClick = this.onClick;
      if (onClick == null)
        return;
      onClick();
    }

    public enum State
    {
      None,
      LocalPlayer,
      LocalPlayerTeam,
      OpponentTeam,
    }

    internal enum TimerState
    {
      Normal,
      Warning,
      Alert,
    }
  }
}
