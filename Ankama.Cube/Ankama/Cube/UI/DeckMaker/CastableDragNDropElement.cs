// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.CastableDragNDropElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.DeckMaker
{
  public class CastableDragNDropElement : DragNDropElement
  {
    public Tween PlayCastImmediate(Vector3 worldPosition, float zoomFactor, Transform parent)
    {
      this.InitMove();
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_content.SetParent(parent, true);
      this.m_content.position = worldPosition;
      this.m_content.localScale = new Vector3(zoomFactor, zoomFactor, zoomFactor);
      this.m_canvasGroup.alpha = 0.0f;
      this.m_subContent.localRotation = Quaternion.Euler(0.0f, 0.0f, 45f);
      this.m_subContent.anchoredPosition = new Vector2(0.0f, 175f);
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_canvasGroup.DOFade(1f, 1f).SetEase<Tweener>(Ease.OutExpo));
      sequence.OnKill<Sequence>(new TweenCallback(this.OnEndPlayCastImmediate));
      this.m_animationTween = (Tween) sequence;
      return this.m_animationTween;
    }

    public void UpdateCastAnimationPosition(Vector3 worldPosition, float zoomFactor)
    {
      this.m_content.position = worldPosition;
      this.m_content.localScale = new Vector3(zoomFactor, zoomFactor, zoomFactor);
    }

    private void OnEndPlayCastImmediate() => this.m_canvasGroup.alpha = 1f;

    public Tween EndCastImmediate() => (Tween) this.m_canvasGroup.DOFade(0.0f, 0.25f).OnKill<Tweener>(new TweenCallback(this.OnEndCastImmediate));

    private void OnEndCastImmediate()
    {
      this.m_canvasGroup.alpha = 0.0f;
      this.m_content.SetParent(this.m_contentParent, false);
      this.m_content.localScale = Vector3.one;
    }

    protected override Tween OnEnterTargetTween()
    {
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_subContent.DOLocalRotate(new Vector3(0.0f, 0.0f, 45f), 0.5f).SetEase<Tweener>(Ease.OutExpo));
      sequence.Insert(0.0f, (Tween) this.m_subContent.DOAnchorPos(new Vector2(0.0f, 200f), 0.5f).SetEase<Tweener>(Ease.OutExpo));
      sequence.OnKill<Sequence>(new TweenCallback(this.EndEnterCastSequence));
      return (Tween) sequence;
    }

    private void EndEnterCastSequence()
    {
      this.m_subContent.localRotation = Quaternion.Euler(0.0f, 0.0f, 45f);
      this.m_subContent.anchoredPosition = new Vector2(0.0f, 200f);
    }

    protected override Tween OnExitTargetTween()
    {
      Sequence sequence = DOTween.Sequence();
      sequence.Insert(0.0f, (Tween) this.m_subContent.DOLocalRotate(Vector3.zero, 0.5f).SetEase<Tweener>(Ease.OutExpo));
      sequence.Insert(0.0f, (Tween) this.m_subContent.DOAnchorPos(Vector2.zero, 0.5f).SetEase<Tweener>(Ease.OutExpo));
      return (Tween) sequence.OnKill<Sequence>(new TweenCallback(this.EndExitCastTarget));
    }

    private void EndExitCastTarget()
    {
      this.m_subContent.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
      this.m_subContent.anchoredPosition = Vector2.zero;
    }

    protected override Tween OnPointerEnterTween() => (Tween) this.m_content.DOScale(1.1f, 0.3f).SetEase<Tweener>(Ease.OutBack);

    protected override Tween OnPointerExitTween() => (Tween) this.m_content.DOScale(1f, 0.3f).SetEase<Tweener>(Ease.OutBack);
  }
}
