// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.CellPointer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [RequireComponent(typeof (SpriteRenderer))]
  public sealed class CellPointer : MonoBehaviour
  {
    private const float AnimationAlpha = 0.25f;
    private const float AnimationPeriod = 0.5f;
    private static Tweener s_animationTween;
    private static float s_animatedAlpha = 1f;
    private static List<CellPointer> s_animatedPointers;
    [SerializeField]
    [HideInInspector]
    private SpriteRenderer m_spriteRenderer;
    private bool m_animated;

    public static void Initialize()
    {
      CellPointer.s_animatedAlpha = 1f;
      CellPointer.s_animationTween = (Tweener) DOTween.To(new DOGetter<float>(CellPointer.BlinkerTweenGetter), new DOSetter<float>(CellPointer.BlinkerTweenSetter), 0.25f, 0.5f).SetEase<TweenerCore<float, float, FloatOptions>>(Ease.InOutCubic).SetLoops<TweenerCore<float, float, FloatOptions>>(-1, LoopType.Yoyo);
      CellPointer.s_animatedPointers = new List<CellPointer>();
    }

    public static void Release()
    {
      if (CellPointer.s_animationTween != null)
      {
        CellPointer.s_animationTween.Kill();
        CellPointer.s_animationTween = (Tweener) null;
      }
      if (CellPointer.s_animatedPointers == null)
        return;
      CellPointer.s_animatedPointers.Clear();
      CellPointer.s_animatedPointers = (List<CellPointer>) null;
    }

    public void Initialize(Material material, uint renderLayerMask)
    {
      this.m_spriteRenderer = this.GetComponent<SpriteRenderer>();
      this.m_spriteRenderer.sharedMaterial = material;
      this.m_spriteRenderer.enabled = false;
      this.m_spriteRenderer.renderingLayerMask = renderLayerMask;
    }

    public void SetSprite(Sprite sprite) => this.m_spriteRenderer.sprite = sprite;

    public void SetColor(Color color) => this.m_spriteRenderer.color = color;

    public void Show() => this.m_spriteRenderer.enabled = true;

    public void Hide()
    {
      this.m_spriteRenderer.enabled = false;
      this.SetAnimated(false);
    }

    public void SetAnimated(bool value)
    {
      if (this.m_animated == value)
        return;
      if (value)
      {
        CellPointer.s_animatedPointers.Add(this);
        if (!CellPointer.s_animationTween.IsPlaying())
          CellPointer.s_animationTween.Restart();
      }
      else
      {
        CellPointer.s_animatedPointers.Remove(this);
        if (CellPointer.s_animatedPointers.Count == 0)
          CellPointer.s_animationTween.Pause<Tweener>();
        this.m_spriteRenderer.color = Color.white;
      }
      this.m_animated = value;
    }

    private static float BlinkerTweenGetter() => CellPointer.s_animatedAlpha;

    private static void BlinkerTweenSetter(float value)
    {
      Color color = new Color(1f, 1f, 1f, value);
      List<CellPointer> animatedPointers = CellPointer.s_animatedPointers;
      int count = animatedPointers.Count;
      for (int index = 0; index < count; ++index)
        animatedPointers[index].SetColor(color);
      CellPointer.s_animatedAlpha = value;
    }

    private void OnDisable()
    {
      if (CellPointer.s_animatedPointers == null)
        return;
      this.SetAnimated(false);
    }
  }
}
