// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.AbstractStatLine`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public abstract class AbstractStatLine<T> : MonoBehaviour where T : AbstractStat
  {
    [SerializeField]
    protected CanvasGroup m_canvasGroup;
    [SerializeField]
    protected LayoutGroup m_alliesGroup;
    [SerializeField]
    protected LayoutGroup m_opponentsGroup;
    [SerializeField]
    protected List<T> m_alliesStats;
    [SerializeField]
    protected List<T> m_opponentStats;

    public CanvasGroup canvasGroup => this.m_canvasGroup;
  }
}
