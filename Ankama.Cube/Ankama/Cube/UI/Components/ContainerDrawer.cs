// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ContainerDrawer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class ContainerDrawer : MonoBehaviour
  {
    [SerializeField]
    private RectMask2D m_mask;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    [SerializeField]
    private RectTransform m_content;
    [SerializeField]
    private RectTransform m_hiddenPositionRect;
    [SerializeField]
    public bool open;
    private ContainerDrawerState m_state;
    private Tween m_tween;
    private Vector3 m_basePosition;
    private Vector3 m_hiddenPosition;

    private void Awake()
    {
      this.m_mask.enabled = false;
      this.m_canvasGroup.alpha = 1f;
      this.m_content.gameObject.SetActive(this.open);
      this.m_state = this.open ? ContainerDrawerState.Opened : ContainerDrawerState.Closed;
      this.m_basePosition = this.m_content.localPosition;
      this.m_hiddenPosition = this.m_hiddenPositionRect.localPosition;
    }

    public void Open(bool forceImmediate = false)
    {
      if (forceImmediate)
      {
        Tween tween = this.m_tween;
        if (tween != null)
          tween.onKill();
        this.OnClosed();
      }
      else
      {
        if (this.m_state == ContainerDrawerState.Opened || this.m_state == ContainerDrawerState.Opening)
          return;
        Tween tween = this.m_tween;
        if (tween != null)
          tween.Kill();
        this.m_state = ContainerDrawerState.Opening;
        this.m_mask.enabled = true;
        this.m_canvasGroup.alpha = 0.0f;
        this.m_content.gameObject.SetActive(true);
        this.m_content.transform.localPosition = this.m_hiddenPosition;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(1f, 0.3f).SetEase<Tweener>(Ease.InOutExpo));
        sequence.Insert(0.0f, (Tween) this.m_content.DOLocalMove(this.m_basePosition, 0.3f).SetEase<Tweener>(Ease.InOutExpo));
        sequence.OnKill<Sequence>(new TweenCallback(this.OnOpened));
        this.m_tween = (Tween) sequence;
      }
    }

    private void OnOpened()
    {
      this.m_state = ContainerDrawerState.Opened;
      this.m_mask.enabled = false;
      this.m_canvasGroup.alpha = 1f;
      this.m_content.localPosition = this.m_basePosition;
      this.m_tween = (Tween) null;
    }

    public void Close(bool forceImmediate = false)
    {
      if (forceImmediate)
      {
        Tween tween = this.m_tween;
        if (tween != null)
          tween.onKill();
        this.OnClosed();
      }
      else
      {
        if (this.m_state == ContainerDrawerState.Closed || this.m_state == ContainerDrawerState.Closing)
          return;
        Tween tween = this.m_tween;
        if (tween != null)
          tween.Kill();
        this.m_state = ContainerDrawerState.Closing;
        this.m_mask.enabled = true;
        this.m_canvasGroup.alpha = 1f;
        this.m_content.transform.localPosition = this.m_basePosition;
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(0.0f, 0.3f).SetEase<Tweener>(Ease.InOutExpo));
        sequence.Insert(0.0f, (Tween) this.m_content.DOLocalMove(this.m_hiddenPosition, 0.3f).SetEase<Tweener>(Ease.InOutExpo));
        sequence.OnKill<Sequence>(new TweenCallback(this.OnClosed));
        this.m_tween = (Tween) sequence;
      }
    }

    private void OnClosed()
    {
      this.m_state = ContainerDrawerState.Closed;
      this.m_mask.enabled = false;
      this.m_canvasGroup.alpha = 0.0f;
      this.m_content.localPosition = this.m_hiddenPosition;
      this.m_content.gameObject.SetActive(false);
      this.m_tween = (Tween) null;
    }

    public void Switch()
    {
      if (this.m_state == ContainerDrawerState.Opened || this.m_state == ContainerDrawerState.Opening)
        this.Close();
      else
        this.Open();
    }
  }
}
