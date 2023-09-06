// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterActionUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using DG.Tweening;
using DG.Tweening.Core;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  [ExecuteInEditMode]
  public sealed class CharacterActionUI : CharacterUILayoutElement
  {
    private const float IconOverlap = -0.04f;
    private const float TweenDurationFactor = 0.05f;
    [Header("Resources")]
    [UsedImplicitly]
    [SerializeField]
    private CharacterUIResources m_resources;
    [Header("Renderers")]
    [UsedImplicitly]
    [SerializeField]
    private SpriteRenderer m_actionIconRenderer;
    [UsedImplicitly]
    [SerializeField]
    private SpriteTextRenderer m_actionValueRenderer;
    private int m_value;
    private int m_currentValue;
    private Tweener m_tweener;

    public override Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        this.m_actionIconRenderer.color = value;
        this.m_actionValueRenderer.color = value;
      }
    }

    public override int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_actionIconRenderer.sortingOrder = this.sortingOrder;
        this.m_actionValueRenderer.sortingOrder = this.sortingOrder;
      }
    }

    public override void SetLayoutPosition(int value)
    {
      if (this.m_layoutPosition == value)
        return;
      float delta = 0.01f * (float) (value - this.m_layoutPosition);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_actionIconRenderer.transform, delta);
      CharacterUILayoutElement.LayoutMoveTransform(this.m_actionValueRenderer.transform, delta);
      this.m_layoutPosition = value;
    }

    public void Setup(ActionType actionType, bool ranged)
    {
      switch (actionType)
      {
        case ActionType.None:
          this.m_actionIconRenderer.sprite = (Sprite) null;
          this.m_actionIconRenderer.enabled = false;
          this.m_actionValueRenderer.enabled = false;
          break;
        case ActionType.Attack:
          bool enabled1 = this.enabled;
          this.m_actionIconRenderer.sprite = ranged ? this.m_resources.actionRangedAttackIcon : this.m_resources.actionAttackIcon;
          this.m_actionIconRenderer.enabled = enabled1;
          this.m_actionValueRenderer.enabled = enabled1;
          break;
        case ActionType.Heal:
          bool enabled2 = this.enabled;
          this.m_actionIconRenderer.sprite = ranged ? this.m_resources.actionRangedHealIcon : this.m_resources.actionHealIcon;
          this.m_actionIconRenderer.enabled = enabled2;
          this.m_actionValueRenderer.enabled = enabled2;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (actionType), (object) actionType, (string) null);
      }
    }

    public void SetValue(int value)
    {
      if (this.m_value == value)
        return;
      this.m_value = value;
      this.m_currentValue = value;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      this.Render();
    }

    public void ChangeValue(int value)
    {
      if (value == this.m_value)
        return;
      if (this.m_tweener != null)
      {
        this.m_tweener.Kill();
        this.m_tweener = (Tweener) null;
      }
      int num = Math.Abs(value - this.m_currentValue);
      if (num != 0)
      {
        float duration = (float) num * 0.05f;
        this.m_tweener = DOTween.To(new DOGetter<int>(this.ValueTweenGetter), new DOSetter<int>(this.ValueTweenSetter), value, duration).OnComplete<Tweener>(new TweenCallback(this.OnTweenComplete));
      }
      this.m_value = value;
    }

    private void Render()
    {
      this.m_actionValueRenderer.text = this.m_currentValue.ToString();
      this.Layout();
    }

    protected override void Layout()
    {
      float position1 = 0.01f * (float) this.m_layoutPosition;
      float position2 = position1 + (CharacterUILayoutElement.LayoutSetTransform(this.m_actionIconRenderer, position1) - 0.04f);
      this.layoutWidth = Mathf.CeilToInt(100f * (position2 + CharacterUILayoutElement.LayoutSetTransform(this.m_actionValueRenderer, position2))) - this.m_layoutPosition;
      base.Layout();
    }

    private int ValueTweenGetter() => this.m_currentValue;

    private void ValueTweenSetter(int value)
    {
      this.m_currentValue = value;
      this.Render();
    }

    private void OnTweenComplete() => this.m_tweener = (Tweener) null;

    private void OnEnable()
    {
      bool flag = (UnityEngine.Object) this.m_actionIconRenderer.sprite != (UnityEngine.Object) null;
      this.m_actionIconRenderer.enabled = flag;
      this.m_actionValueRenderer.enabled = flag;
      if (this.m_currentValue != this.m_value)
      {
        this.m_currentValue = this.m_value;
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
      this.m_actionIconRenderer.enabled = false;
      this.m_actionValueRenderer.enabled = false;
      base.Layout();
    }
  }
}
