// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Feedbacks.CellHighlight
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Feedbacks
{
  [RequireComponent(typeof (SpriteRenderer))]
  public class CellHighlight : MonoBehaviour
  {
    private SpriteRenderer m_spriteRenderer;
    private Quaternion m_originalRotation;
    private Tweener m_tweener;
    private bool m_shown;

    public void Initialize(Material material, uint renderLayerMask)
    {
      SpriteRenderer component = this.GetComponent<SpriteRenderer>();
      component.sharedMaterial = material;
      component.color = new Color(1f, 1f, 1f, 0.0f);
      component.renderingLayerMask = renderLayerMask;
      this.m_spriteRenderer = component;
      this.m_originalRotation = this.transform.localRotation;
      this.m_shown = false;
    }

    public void SetSprite([NotNull] Sprite sprite, Color color)
    {
      if (!this.m_shown)
      {
        this.Show();
        this.m_shown = true;
      }
      this.transform.localRotation = this.m_originalRotation;
      this.m_spriteRenderer.sprite = sprite;
      this.m_spriteRenderer.color = color;
    }

    public void SetSprite([NotNull] Sprite sprite, Color color, float angle)
    {
      if (!this.m_shown)
      {
        this.Show();
        this.m_shown = true;
      }
      this.transform.localRotation = this.m_originalRotation * Quaternion.AngleAxis(angle, Vector3.forward);
      this.m_spriteRenderer.sprite = sprite;
      this.m_spriteRenderer.color = color;
    }

    public void ClearSprite()
    {
      if (!this.m_shown)
        return;
      this.Hide();
      this.m_shown = false;
    }

    private void Show()
    {
      Tweener tweener = this.m_tweener;
      if (tweener != null)
        tweener.Kill();
      this.m_tweener = this.m_spriteRenderer.DOFade(1f, 0.116666667f).SetEase<Tweener>(Ease.InQuad).OnComplete<Tweener>(new TweenCallback(this.OnShowTweenerComplete));
      this.m_spriteRenderer.enabled = true;
    }

    private void Hide()
    {
      Tweener tweener = this.m_tweener;
      if (tweener != null)
        tweener.Kill();
      this.m_tweener = this.m_spriteRenderer.DOFade(0.0f, 0.166666672f).SetEase<Tweener>(Ease.OutQuad).OnComplete<Tweener>(new TweenCallback(this.OnHideTweenerComplete));
    }

    private void OnShowTweenerComplete() => this.m_tweener = (Tweener) null;

    private void OnHideTweenerComplete()
    {
      this.m_spriteRenderer.enabled = false;
      this.m_spriteRenderer.sprite = (Sprite) null;
      this.m_tweener = (Tweener) null;
    }
  }
}
