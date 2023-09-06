// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.RotativeListConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [CreateAssetMenu(menuName = "Waven/UI/RotativeListConfig")]
  public class RotativeListConfig : ScriptableObject
  {
    [SerializeField]
    public int minCells;
    [SerializeField]
    [Range(0.0f, 1f)]
    public float extraCellsDistribution;
    [SerializeField]
    public bool emptyCellsAreSelectable;
    [SerializeField]
    public float moveTweenDuration;
    [SerializeField]
    public Ease moveTweenEase;
    [SerializeField]
    public float inTweenDuration;
    [SerializeField]
    public float inTweenDelayByElement;
    [SerializeField]
    public Ease inTweenEase;
    [SerializeField]
    public float outScale;
    [SerializeField]
    public float outTweenDuration;
    [SerializeField]
    public float outTweenDelayByElement;
    [SerializeField]
    public Ease outTweenEase;
    [SerializeField]
    public AnimationCurve cellPositionCurve;
    [SerializeField]
    public AnimationCurve cellVisibilityCurve;
    [SerializeField]
    public AnimationCurve cellHighlightCurve;
  }
}
