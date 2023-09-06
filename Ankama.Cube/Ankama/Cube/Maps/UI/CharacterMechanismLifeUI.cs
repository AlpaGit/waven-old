// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterMechanismLifeUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  [ExecuteInEditMode]
  public sealed class CharacterMechanismLifeUI : CharacterUILayoutElement
  {
    private const float IconOverlap = -0.04f;
    private const float TweenDurationFactor = 0.05f;
    [Header("Renderers")]
    [SerializeField]
    private SpriteRenderer m_iconRenderer;
    [SerializeField]
    private SpriteTextRenderer m_valueRenderer;
    private int m_life;
    private int m_currentLife;
    private Tweener m_tweener;

    public override Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        this.m_iconRenderer.color = value;
        this.m_valueRenderer.color = value;
      }
    }

    public override int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_iconRenderer.sortingOrder = this.sortingOrder;
        this.m_valueRenderer.sortingOrder = this.sortingOrder;
      }
    }

    public override void SetLayoutPosition(int value)
    {
      if (this.m_layoutPosition == value)
        return;
      float delta = 0.01f * (float) (value - this.m_layoutPosition);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_iconRenderer.transform, delta);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_valueRenderer.transform, delta);
      this.m_layoutPosition = value;
    }

    public void SetValue(int life)
    {
      if (life == this.m_life)
        return;
      this.m_life = life;
      this.m_currentLife = life;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.Render();
    }

    public void ChangeValue(int life)
    {
      if (life == this.m_life)
        return;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      int num = Math.Abs(life - this.m_currentLife);
      if (num != 0)
      {
        float duration = (float) num * 0.05f;
        this.m_tweener = DOTween.To(new DOGetter<int>(this.LifeTweenGetter), new DOSetter<int>(this.LifeTweenSetter), life, duration).OnComplete<Tweener>(new TweenCallback(this.OnLifeTweenComplete));
      }
      this.m_life = life;
    }

    private void Render()
    {
      this.m_valueRenderer.text = this.m_currentLife.ToString();
      this.Layout();
    }

    protected override void Layout()
    {
      float position1 = 0.01f * (float) this.m_layoutPosition;
      float position2 = position1 + (CharacterUILayoutElement.LayoutSetTransform(this.m_iconRenderer, position1) - 0.04f);
      this.layoutWidth = Mathf.CeilToInt(100f * (position2 + CharacterUILayoutElement.LayoutSetTransform(this.m_valueRenderer, position2))) - this.m_layoutPosition;
      base.Layout();
    }

    private int LifeTweenGetter() => this.m_currentLife;

    private void LifeTweenSetter(int value)
    {
      this.m_currentLife = value;
      this.Render();
    }

    private void OnLifeTweenComplete() => this.m_tweener = (Tweener) null;

    private void OnEnable()
    {
      this.m_iconRenderer.enabled = true;
      this.m_valueRenderer.enabled = true;
      if (this.m_currentLife != this.m_life)
      {
        this.m_currentLife = this.m_life;
        this.Render();
      }
      else
        this.Layout();
    }

    private void OnDisable()
    {
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.m_iconRenderer.enabled = false;
      this.m_valueRenderer.enabled = false;
      base.Layout();
    }
  }
}
