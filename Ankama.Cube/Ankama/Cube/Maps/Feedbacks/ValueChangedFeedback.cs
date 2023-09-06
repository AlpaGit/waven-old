// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Feedbacks.ValueChangedFeedback
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.Feedbacks
{
  public sealed class ValueChangedFeedback : MonoBehaviour
  {
    private const float TweenDuration = 1f;
    private const Ease TweenEasing = Ease.OutCubic;
    private const float StartHeight = 1.25f;
    private const float MovementHeight = 0.75f;
    private const float AlphaDelay = 0.75f;
    private const float ItemDelay = 0.25f;
    [SerializeField]
    private SpriteTextRenderer m_spriteTextRenderer;
    private bool m_isNegative;
    private Vector3 m_startPosition;
    private Vector3 m_endPosition;
    private Tweener m_tween;
    private float m_tweenValue;

    public static void Launch(int value, ValueChangedFeedback.Type type, Transform parent)
    {
      int instanceCountInTransform;
      ValueChangedFeedback valueChangedFeedback = FightObjectFactory.CreateValueChangedFeedback(parent, out instanceCountInTransform);
      valueChangedFeedback.SetValue(value, type);
      valueChangedFeedback.StartTween(instanceCountInTransform);
    }

    private void SetValue(int value, ValueChangedFeedback.Type type)
    {
      switch (type)
      {
        case ValueChangedFeedback.Type.Damage:
          this.m_spriteTextRenderer.color = new Color(1f, 0.431372553f, 0.431372553f);
          break;
        case ValueChangedFeedback.Type.Heal:
          this.m_spriteTextRenderer.color = new Color(0.549019635f, 0.549019635f, 1f);
          break;
        case ValueChangedFeedback.Type.Action:
          this.m_spriteTextRenderer.color = new Color(1f, 0.980392158f, 0.0f);
          break;
        case ValueChangedFeedback.Type.Movement:
          this.m_spriteTextRenderer.color = new Color(0.549019635f, 0.8901961f, 0.211764708f);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (type), (object) type, (string) null);
      }
      this.m_isNegative = value < 0;
      this.m_spriteTextRenderer.text = value.ToStringSigned();
    }

    private void StartTween(int indexInParentTransform)
    {
      this.gameObject.SetActive(true);
      Vector3 vector3_1 = this.transform.parent.position + new Vector3(0.0f, 1.25f, 0.0f);
      Vector3 vector3_2 = vector3_1 + new Vector3(0.0f, 0.75f, 0.0f);
      float delay = (float) indexInParentTransform * 0.25f;
      if (this.m_isNegative)
      {
        this.transform.position = vector3_2;
        this.m_startPosition = vector3_2;
        this.m_endPosition = vector3_1;
      }
      else
      {
        this.transform.position = vector3_1;
        this.m_startPosition = vector3_1;
        this.m_endPosition = vector3_2;
      }
      this.m_tweenValue = 0.0f;
      this.m_tween = (Tweener) DOTween.To(new DOGetter<float>(this.TweenGetter), new DOSetter<float>(this.TweenSetter), 1f, 1f).SetDelay<TweenerCore<float, float, FloatOptions>>(delay).SetEase<TweenerCore<float, float, FloatOptions>>(Ease.OutCubic).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.TweenCompleteCallback));
    }

    private float TweenGetter() => this.m_tweenValue;

    private void TweenSetter(float value)
    {
      Vector3 vector3 = Vector3.Lerp(this.m_startPosition, this.m_endPosition, value);
      Color color = this.m_spriteTextRenderer.color with
      {
        a = Mathf.Lerp(1f, 0.0f, (float) (((double) value - 0.75) / 0.25))
      };
      this.transform.position = vector3;
      this.m_spriteTextRenderer.color = color;
      this.m_tweenValue = value;
    }

    private void TweenCompleteCallback()
    {
      FightObjectFactory.ReleaseValueChangedFeedback(this.gameObject);
      this.m_tween = (Tweener) null;
    }

    private void OnDestroy()
    {
      if (this.m_tween == null)
        return;
      this.m_tween.Kill();
      this.m_tween = (Tweener) null;
    }

    public enum Type
    {
      Damage,
      Heal,
      Action,
      Movement,
    }
  }
}
