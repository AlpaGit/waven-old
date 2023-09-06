// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CameraControlParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class CameraControlParameters : ScriptableObject
  {
    [Header("Pan")]
    [SerializeField]
    private float m_panTweenDuration = 0.25f;
    [SerializeField]
    private Ease m_panTweenEase = Ease.OutCubic;
    [Header("Zoom")]
    [SerializeField]
    private float m_zoomTweenMaxDuration = 0.5f;
    [SerializeField]
    private Ease m_zoomTweenEase = Ease.OutCubic;

    public float panTweenDuration => this.m_panTweenDuration;

    public Ease panTweenEase => this.m_panTweenEase;

    public float zoomTweenMaxDuration => this.m_zoomTweenMaxDuration;

    public Ease zoomTweenEase => this.m_zoomTweenEase;
  }
}
