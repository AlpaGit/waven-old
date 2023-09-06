// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.GaugePreviewResource
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public class GaugePreviewResource : ScriptableObject
  {
    [Header("Highlight")]
    [SerializeField]
    private bool m_highlightEnabled = true;
    [SerializeField]
    private int m_loopCount = 1;
    [SerializeField]
    private float m_highlightPunch = 0.1f;
    [SerializeField]
    private float m_highlightDuration = 0.3f;
    [SerializeField]
    private int m_highlightVibrato = 1;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_highlightElasticity = 0.1f;
    [Header("Text Value Modification")]
    [SerializeField]
    private bool m_textValueEnabled = true;
    [SerializeField]
    private float m_duration;
    [SerializeField]
    private Ease m_ease;

    public bool highlightEnabled => this.m_highlightEnabled;

    public int highlightLoopCount => this.m_loopCount;

    public float highlightPunch => this.m_highlightPunch;

    public float highlightDuration => this.m_highlightDuration;

    public int highlightVibrato => this.m_highlightVibrato;

    public float highlightElasticity => this.m_highlightElasticity;

    public bool displayText => this.m_textValueEnabled;

    public float duration => this.m_duration;

    public Ease ease => this.m_ease;
  }
}
