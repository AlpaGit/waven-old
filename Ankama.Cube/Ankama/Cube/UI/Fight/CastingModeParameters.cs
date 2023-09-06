// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.CastingModeParameters
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public class CastingModeParameters : ScriptableObject
  {
    [SerializeField]
    private Color m_disabledColor = new Color(0.6f, 0.6f, 0.6f, 1f);
    [SerializeField]
    private float m_disableFadeDuration = 0.3f;
    [SerializeField]
    private float m_castInvalidPopupDuration = 2f;
    [Header("Enter In Hand")]
    [SerializeField]
    private Vector3 m_enterInHandOffset = new Vector3(0.0f, 10f, 0.0f);
    [SerializeField]
    private float m_enterInHandDuration = 0.3f;
    [SerializeField]
    public Ease enterInHandEase = Ease.OutExpo;
    [Header("Pointer over")]
    [SerializeField]
    private Vector3 m_selectOffset = new Vector3(0.0f, 10f, 0.0f);
    [SerializeField]
    private float m_selectDuration = 0.1f;
    [SerializeField]
    public Ease selectEase = Ease.OutExpo;
    [Header("Start drag")]
    [SerializeField]
    private float m_dragThreshold = 50f;
    [SerializeField]
    private Vector2 m_startDragPivot = new Vector2(0.5f, 0.5f);
    [SerializeField]
    private float m_startDragRotation;
    [SerializeField]
    private float m_startDragScale = 0.8f;
    [SerializeField]
    private float m_startDragDuration = 1f;
    [SerializeField]
    public Ease startDragEase = Ease.OutExpo;
    [Header("Dragging")]
    [SerializeField]
    private Vector2 m_movePivot = new Vector2(0.5f, 0.0f);
    [SerializeField]
    private float m_moveRotation;
    [SerializeField]
    private float m_moveScale = 1f;
    [SerializeField]
    private float m_moveDuration = 0.07f;
    [SerializeField]
    public Ease moveEase = Ease.OutExpo;
    [Header("Targeting")]
    [SerializeField]
    private Vector3 m_cellTargetOffset = new Vector3(0.0f, 1.5f, 0.0f);
    [SerializeField]
    private Vector2 m_snapPivot = new Vector2(0.5f, 0.5f);
    [SerializeField]
    private float m_snapRotation;
    [SerializeField]
    private float m_snapScale = 0.7f;
    [SerializeField]
    private float m_uiSnapScale = 0.7f;
    [SerializeField]
    public Ease snapEase = Ease.OutExpo;
    [Space(10f)]
    [SerializeField]
    private float m_snapMoveDuration = 0.05f;
    [SerializeField]
    public Ease snapMoveEase = Ease.OutExpo;
    [Space(10f)]
    [SerializeField]
    private float m_snapRotationDuration = 0.05f;
    [SerializeField]
    public Ease snapRotationEase = Ease.OutExpo;
    [Space(15f)]
    [SerializeField]
    private float m_snapReleaseCellThreshold = 50f;
    [SerializeField]
    private float m_snapReleaseWaitingDuration = 0.5f;
    [SerializeField]
    private float m_snapReleaseDuration = 0.05f;
    [SerializeField]
    public Ease snapReleaseEase = Ease.InExpo;
    [Header("Release")]
    [SerializeField]
    private float m_returnDuration = 0.1f;
    [SerializeField]
    public Ease returnEase = Ease.OutExpo;
    [Header("Throw")]
    [SerializeField]
    public float noiseSpeed = 0.1f;
    [SerializeField]
    public float noiseAmount = 0.1f;
    [Header("Remove")]
    [SerializeField]
    private float m_fadeDuration = 0.1f;
    [SerializeField]
    public Ease fadeEase = Ease.OutExpo;
    [Header("Misc.")]
    [SerializeField]
    private float m_infoFadeDuration = 0.1f;
    [SerializeField]
    private float m_opponentPlayingDuration = 0.15f;
    [SerializeField]
    private float m_opponentCastPlayingDuration = 0.15f;

    public Color disabledColor => this.m_disabledColor;

    public float disableFadeDuration => this.m_disableFadeDuration;

    public float castInvalidPopupDuration => this.m_castInvalidPopupDuration;

    public Vector3 enterInHandOffset => this.m_enterInHandOffset;

    public float enterInHandDuration => this.m_enterInHandDuration;

    public Vector3 selectOffset => this.m_selectOffset;

    public float selectDuration => this.m_selectDuration;

    public float dragThreshold => this.m_dragThreshold;

    public Vector2 startDragPivot => this.m_startDragPivot;

    public float startDragRotation => this.m_startDragRotation;

    public float startDragScale => this.m_startDragScale;

    public float startDragDuration => this.m_startDragDuration;

    public Vector2 movePivot => this.m_movePivot;

    public float moveRotation => this.m_moveRotation;

    public float moveScale => this.m_moveScale;

    public float moveDuration => this.m_moveDuration;

    public Vector3 cellTargetOffset => this.m_cellTargetOffset;

    public Vector2 snapPivot => this.m_snapPivot;

    public float snapRotation => this.m_snapRotation;

    public float snapScale => this.m_snapScale;

    public float uiSnapScale => this.m_uiSnapScale;

    public float snapMoveDuration => this.m_snapMoveDuration;

    public float snapRotationDuration => this.m_snapRotationDuration;

    public float snapReleaseDuration => this.m_snapReleaseDuration;

    public float snapReleaseCellThreshold => this.m_snapReleaseCellThreshold;

    public float snapReleaseWaitingDuration => this.m_snapReleaseWaitingDuration;

    public float returnDuration => this.m_returnDuration;

    public float fadeDuration => this.m_fadeDuration;

    public float infoFadeDuration => this.m_infoFadeDuration;

    public float opponentPlayingDuration => this.m_opponentPlayingDuration;

    public float opponentCastPlayingDuration => this.m_opponentCastPlayingDuration;
  }
}
