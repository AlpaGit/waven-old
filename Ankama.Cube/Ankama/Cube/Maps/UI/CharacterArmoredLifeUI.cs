// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterArmoredLifeUI
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
  public sealed class CharacterArmoredLifeUI : CharacterUILayoutElement
  {
    private const float IconOverlap = -0.04f;
    private const float TweenDurationFactor = 0.05f;
    [Header("Renderers")]
    [SerializeField]
    private SpriteRenderer m_lifeIconRenderer;
    [SerializeField]
    private SpriteTextRenderer m_lifeValueRenderer;
    [SerializeField]
    private SpriteRenderer m_armorIconRenderer;
    [SerializeField]
    private SpriteTextRenderer m_armorValueRenderer;
    [Header("Layout")]
    [SerializeField]
    private int m_layoutSpacing = 1;
    private int m_life;
    private int m_armor;
    private int m_currentLife;
    private int m_currentArmor;
    private int m_maximumLife;
    private Tweener m_tweener;

    public override Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        this.m_lifeIconRenderer.color = value;
        this.m_lifeValueRenderer.color = value;
        this.m_armorIconRenderer.color = value;
        this.m_armorValueRenderer.color = value;
      }
    }

    public override int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_lifeIconRenderer.sortingOrder = this.sortingOrder;
        this.m_lifeValueRenderer.sortingOrder = this.sortingOrder;
        this.m_armorIconRenderer.sortingOrder = this.sortingOrder;
        this.m_armorValueRenderer.sortingOrder = this.sortingOrder;
      }
    }

    public override void SetLayoutPosition(int value)
    {
      if (this.m_layoutPosition == value)
        return;
      float delta = 0.01f * (float) (value - this.m_layoutPosition);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_lifeIconRenderer.transform, delta);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_lifeValueRenderer.transform, delta);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_armorIconRenderer.transform, delta);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_armorValueRenderer.transform, delta);
      this.m_layoutPosition = value;
    }

    public void Setup(int maximumLife) => this.m_maximumLife = maximumLife;

    public void SetMaximumLife(int maximumLife)
    {
      if (maximumLife == this.m_maximumLife)
        return;
      this.m_maximumLife = maximumLife;
      this.Render();
    }

    public void SetValues(int life, int armor)
    {
      if (life == this.m_life && armor == this.m_armor)
        return;
      this.m_life = life;
      this.m_currentLife = life;
      this.m_armor = armor;
      this.m_currentArmor = armor;
      bool flag = armor > 0 && this.enabled;
      this.m_armorIconRenderer.enabled = flag;
      this.m_armorValueRenderer.enabled = flag;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.Render();
    }

    public void ChangeValues(int life, int armor)
    {
      if (life == this.m_life && armor == this.m_armor)
        return;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      int num1 = Math.Abs(armor - this.m_currentArmor);
      if (num1 != 0)
      {
        if (armor > 0)
        {
          bool enabled = this.enabled;
          this.m_armorIconRenderer.enabled = enabled;
          this.m_armorValueRenderer.enabled = enabled;
        }
        float duration = (float) num1 * 0.05f;
        this.m_tweener = DOTween.To(new DOGetter<int>(this.ArmorTweenGetter), new DOSetter<int>(this.ArmorTweenSetter), armor, duration).OnComplete<Tweener>(new TweenCallback(this.OnArmorTweenComplete));
      }
      else
      {
        int num2 = Math.Abs(life - this.m_currentLife);
        if (num2 != 0)
        {
          float duration = (float) num2 * 0.05f;
          this.m_tweener = DOTween.To(new DOGetter<int>(this.LifeTweenGetter), new DOSetter<int>(this.LifeTweenSetter), life, duration).OnComplete<Tweener>(new TweenCallback(this.OnLifeTweenComplete));
        }
      }
      this.m_life = life;
      this.m_armor = armor;
    }

    private void Render()
    {
      this.m_lifeValueRenderer.text = this.m_currentLife.ToString();
      this.m_armorValueRenderer.text = this.m_currentArmor.ToString();
      this.Layout();
    }

    protected override void Layout()
    {
      float position1 = 0.01f * (float) this.m_layoutPosition;
      float position2 = position1 + (CharacterUILayoutElement.LayoutSetTransform(this.m_lifeIconRenderer, position1) - 0.04f);
      float num = position2 + CharacterUILayoutElement.LayoutSetTransform(this.m_lifeValueRenderer, position2);
      if (this.m_currentArmor > 0)
      {
        float position3 = num + 0.01f * (float) this.m_layoutSpacing;
        float position4 = position3 + (CharacterUILayoutElement.LayoutSetTransform(this.m_armorIconRenderer, position3) - 0.04f);
        num = position4 + CharacterUILayoutElement.LayoutSetTransform(this.m_armorValueRenderer, position4);
      }
      this.layoutWidth = Mathf.CeilToInt(100f * num) - this.m_layoutPosition;
      base.Layout();
    }

    private int LifeTweenGetter() => this.m_currentLife;

    private void LifeTweenSetter(int value)
    {
      this.m_currentLife = value;
      this.Render();
    }

    private void OnLifeTweenComplete() => this.m_tweener = (Tweener) null;

    private int ArmorTweenGetter() => this.m_currentArmor;

    private void ArmorTweenSetter(int value)
    {
      this.m_currentArmor = value;
      this.Render();
    }

    private void OnArmorTweenComplete()
    {
      bool flag = this.m_currentArmor > 0 && this.enabled;
      this.m_armorIconRenderer.enabled = flag;
      this.m_armorValueRenderer.enabled = flag;
      if (this.m_currentLife != this.m_life)
        this.m_tweener = DOTween.To(new DOGetter<int>(this.LifeTweenGetter), new DOSetter<int>(this.LifeTweenSetter), this.m_life, (float) Math.Abs(this.m_life - this.m_life) * 0.05f).OnComplete<Tweener>(new TweenCallback(this.OnLifeTweenComplete));
      else
        this.m_tweener = (Tweener) null;
    }

    private void OnEnable()
    {
      bool flag = this.m_armor > 0;
      this.m_lifeIconRenderer.enabled = true;
      this.m_lifeValueRenderer.enabled = true;
      this.m_armorValueRenderer.enabled = flag;
      this.m_armorIconRenderer.enabled = flag;
      if (this.m_life != this.m_currentLife || this.m_armor != this.m_currentArmor)
      {
        this.m_currentLife = this.m_life;
        this.m_currentArmor = this.m_armor;
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
      this.m_lifeIconRenderer.enabled = false;
      this.m_lifeValueRenderer.enabled = false;
      this.m_armorValueRenderer.enabled = false;
      this.m_armorIconRenderer.enabled = false;
      base.Layout();
    }
  }
}
