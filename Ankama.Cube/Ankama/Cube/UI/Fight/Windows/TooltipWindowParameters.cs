// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.TooltipWindowParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.Windows
{
  [CreateAssetMenu(menuName = "Waven/UI/Tooltip parameters")]
  public class TooltipWindowParameters : ScriptableObject
  {
    [SerializeField]
    private float m_moveDuration = 0.1f;
    [SerializeField]
    private Ease m_moveEase = Ease.OutExpo;
    [Header("Open")]
    [SerializeField]
    private float m_openDelay;
    [SerializeField]
    private float m_openDuration = 0.5f;
    [SerializeField]
    private Ease m_openEase = Ease.Flash;
    [Header("Close")]
    [SerializeField]
    private float m_closeDuration = 0.5f;
    [SerializeField]
    private Ease m_closeEase = Ease.Flash;

    public float moveDuration => this.m_moveDuration;

    public Ease moveEase => this.m_moveEase;

    public float openDelay => this.m_openDelay;

    public float openDuration => this.m_openDuration;

    public Ease openEase => this.m_openEase;

    public float closeDuration => this.m_closeDuration;

    public Ease closeEase => this.m_closeEase;
  }
}
