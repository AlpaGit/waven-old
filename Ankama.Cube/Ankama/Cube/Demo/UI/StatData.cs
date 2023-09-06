// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.StatData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class StatData : ScriptableObject
  {
    [SerializeField]
    private Color m_allyColor;
    [SerializeField]
    private Color m_opponentColor;
    [SerializeField]
    private Color m_bestValueColor;
    [SerializeField]
    private Color m_worstValueColor;
    [SerializeField]
    private Color m_neutralValueColor;
    [SerializeField]
    private float m_illuMvpScale;
    [SerializeField]
    private float m_illuNeutralScale;
    [SerializeField]
    private float m_openBoardDelay;
    [SerializeField]
    private float m_openBoardDuration;
    [SerializeField]
    private float m_openBoardLineTweenDuration;
    [SerializeField]
    private Ease m_openBoardLineTweenEase;
    [SerializeField]
    private FightStatTypeIconDico m_fightStatTypeIcons;

    public Color allyColor => this.m_allyColor;

    public Color opponentColor => this.m_opponentColor;

    public Color bestValueColor => this.m_bestValueColor;

    public Color worstValueColor => this.m_worstValueColor;

    public Color neutralValueColor => this.m_neutralValueColor;

    public float illuMvpScale => this.m_illuMvpScale;

    public float illuNeutralScale => this.m_illuNeutralScale;

    public float openBoardDelay => this.m_openBoardDelay;

    public float openBoardDuration => this.m_openBoardDuration;

    public float openBoardLineTweenDuration => this.m_openBoardLineTweenDuration;

    public Ease openBoardLineTweenEase => this.m_openBoardLineTweenEase;

    public Sprite GetIcon(FightStatType type)
    {
      Sprite sprite;
      return this.m_fightStatTypeIcons.TryGetValue(type, out sprite) ? sprite : (Sprite) null;
    }
  }
}
