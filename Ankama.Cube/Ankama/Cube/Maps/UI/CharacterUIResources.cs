// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterUIResources
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  public sealed class CharacterUIResources : ScriptableObject
  {
    [Header("Action Type Icons")]
    [SerializeField]
    private Sprite m_actionAttackIcon;
    [SerializeField]
    private Sprite m_actionRangedAttackIcon;
    [SerializeField]
    private Sprite m_actionHealIcon;
    [SerializeField]
    private Sprite m_actionRangedHealIcon;
    [Header("Elementary State Icons")]
    [SerializeField]
    private Sprite m_elementaryStateMuddyIcon;
    [SerializeField]
    private Sprite m_elementaryStateOiledIcon;
    [SerializeField]
    private Sprite m_elementaryStateVentilatedIcon;
    [SerializeField]
    private Sprite m_elementaryStateWetIcon;
    [Header("Attack Target Feedback Icons")]
    [SerializeField]
    private Sprite m_attackTargetFeedbackIcon;
    [SerializeField]
    private Sprite m_attackTargetSelectedFeedbackIcon;
    [SerializeField]
    private Sprite m_healTargetFeedbackIcon;
    [SerializeField]
    private Sprite m_healTargetSelectedFeedbackIcon;
    [Header("Map Cell Indicators")]
    [SerializeField]
    private Sprite m_mapCellIndicatorDeathIcon;

    public Sprite actionAttackIcon => this.m_actionAttackIcon;

    public Sprite actionRangedAttackIcon => this.m_actionRangedAttackIcon;

    public Sprite actionHealIcon => this.m_actionHealIcon;

    public Sprite actionRangedHealIcon => this.m_actionRangedHealIcon;

    public Sprite elementaryStateMuddyIcon => this.m_elementaryStateMuddyIcon;

    public Sprite elementaryStateOiledIcon => this.m_elementaryStateOiledIcon;

    public Sprite elementaryStateVentilatedIcon => this.m_elementaryStateVentilatedIcon;

    public Sprite elementaryStateWetIcon => this.m_elementaryStateWetIcon;

    public Sprite attackTargetFeedbackIcon => this.m_attackTargetFeedbackIcon;

    public Sprite attackTargetSelectedFeedbackIcon => this.m_attackTargetSelectedFeedbackIcon;

    public Sprite healTargetFeedbackIcon => this.m_healTargetFeedbackIcon;

    public Sprite healTargetSelectedFeedbackIcon => this.m_healTargetSelectedFeedbackIcon;

    public Sprite mapCellIndicatorDeathIcon => this.m_mapCellIndicatorDeathIcon;
  }
}
