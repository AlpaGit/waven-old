// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PanelListConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class PanelListConfig : ScriptableObject
  {
    [Header("Bounds Offset")]
    [SerializeField]
    public int leftOffset;
    [SerializeField]
    public int rightOffset;
    [Header("Selection Anim")]
    [SerializeField]
    public float selectionTweenDuration = 0.2f;
    [SerializeField]
    public Ease selectionTweenEase = Ease.Linear;
    [SerializeField]
    public AnimationCurve elementRepartition;
    [Header("Element visibility")]
    [SerializeField]
    public float imageDepthDarken = 0.35f;
    [SerializeField]
    public float imageDepthDesaturation = 0.5f;
    [SerializeField]
    public Color textDepthTint = Color.grey;
    [SerializeField]
    public AnimationCurve depthRepartition;
    [SerializeField]
    public float shadowDepthAlpha;
    [SerializeField]
    public AnimationCurve shadowDepthRepartition;
    [Header("Transition Anim")]
    [SerializeField]
    public SlidingAnimUIConfig openAnim;
    [SerializeField]
    public SlidingAnimUIConfig closeAnim;
  }
}
