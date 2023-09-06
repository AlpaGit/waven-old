// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.PointCounterStyleRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  [CreateAssetMenu(menuName = "Waven/UI/Fight/PointCounterStyle")]
  public sealed class PointCounterStyleRework : ScriptableObject
  {
    [Header("Tween Parameters")]
    [SerializeField]
    private Ease m_tweenEasing = Ease.Linear;
    [SerializeField]
    private float m_tweenDurationPerUnit = 0.05f;
    [SerializeField]
    private float m_maxTweenDuration = 1f;

    public Ease tweenEasing => this.m_tweenEasing;

    public float GetTweenDuration(int previousValue, int newValue) => Mathf.Min(this.m_maxTweenDuration, (float) Math.Abs(newValue - previousValue) * this.m_tweenDurationPerUnit);
  }
}
