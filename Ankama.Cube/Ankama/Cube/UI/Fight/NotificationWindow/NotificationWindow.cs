// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.NotificationWindow
{
  public class NotificationWindow : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private RawTextField m_messageField;
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private NotificationWindowStyle m_style;
    private NotificationWindowState m_state;
    private Tween m_tween;
    private float m_endTime;

    public event Action<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow> OnClosed;

    private void Awake()
    {
      this.m_closeButton.onClick.AddListener(new UnityAction(this.Close));
      this.m_canvasGroup.alpha = 0.0f;
      this.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
      this.m_state = NotificationWindowState.OPENING;
    }

    public void Open(string message)
    {
      this.m_messageField.SetText(message);
      if (this.m_state != NotificationWindowState.OPENING)
        return;
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(1f, this.m_style.fadeInDuration).SetEase<Tweener>(this.m_style.fadeInEase));
      sequence.Insert(0.0f, (Tween) this.transform.DOScale(1f, this.m_style.fadeInDuration).SetEase<Tweener>(this.m_style.scaleFadeInEase));
      sequence.OnKill<Sequence>(new TweenCallback(this.OnOpenComplete));
      this.m_tween = (Tween) sequence;
    }

    private void OnOpenComplete()
    {
      this.m_state = NotificationWindowState.OPENED;
      this.m_tween = (Tween) null;
      this.m_endTime = Time.realtimeSinceStartup + this.m_style.displayDuration;
    }

    private void Update()
    {
      if (this.m_state != NotificationWindowState.OPENED || (double) this.m_endTime > (double) Time.realtimeSinceStartup)
        return;
      this.Close();
    }

    public void Close()
    {
      if (this.m_state == NotificationWindowState.CLOSING)
        return;
      this.m_state = NotificationWindowState.CLOSING;
      Tween tween = this.m_tween;
      if (tween != null)
        tween.Kill();
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(0.0f, this.m_style.fadeOutDuration).SetEase<Tweener>(this.m_style.fadeOutEase));
      sequence.OnKill<Sequence>(new TweenCallback(this.OnCloseComplete));
      this.m_tween = (Tween) sequence;
    }

    private void OnCloseComplete()
    {
      Action<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow> onClosed = this.OnClosed;
      if (onClosed == null)
        return;
      onClosed(this);
    }
  }
}
