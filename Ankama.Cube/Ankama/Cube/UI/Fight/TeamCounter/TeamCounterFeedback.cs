// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.TeamCounter.TeamCounterFeedback
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight.TeamCounter
{
  public class TeamCounterFeedback : MonoBehaviour
  {
    [SerializeField]
    private MaskableGraphic m_target;
    [SerializeField]
    private float m_tweenDuration;
    private Sequence m_sequence;

    public void PlayFeedback()
    {
      if (this.m_sequence != null && this.m_sequence.IsActive())
        this.m_sequence.Kill();
      this.transform.localScale = Vector3.one;
      this.SetAlpha(1f);
      this.m_target.enabled = true;
      this.m_sequence = DOTween.Sequence();
      this.m_sequence.Insert(0.0f, (Tween) DOTween.To(new DOGetter<float>(this.TweenAlphaGetter), new DOSetter<float>(this.SetAlpha), 0.0f, this.m_tweenDuration));
      this.m_sequence.Insert(0.0f, (Tween) DOTween.To((DOGetter<Vector3>) (() => Vector3.one), (DOSetter<Vector3>) (x => this.transform.localScale = x), new Vector3(5f, 5f, 5f), this.m_tweenDuration).SetDelay<TweenerCore<Vector3, Vector3, VectorOptions>>(0.0f));
      this.m_sequence.OnComplete<Sequence>(new TweenCallback(this.OnTweenComplete));
    }

    private void SetAlpha(float value) => this.m_target.color = this.m_target.color with
    {
      a = value
    };

    private float TweenAlphaGetter() => this.m_target.color.a;

    private void OnTweenComplete() => this.SetOff();

    public void SetOff() => this.m_target.enabled = false;
  }
}
