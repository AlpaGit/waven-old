// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.SlidingAnimUIConfig
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  [CreateAssetMenu]
  public class SlidingAnimUIConfig : ScriptableObject
  {
    [SerializeField]
    public float delay;
    [SerializeField]
    public float duration;
    [SerializeField]
    public AnimationCurve positionCurve;
    [SerializeField]
    public float elementDelayOffset;
    [SerializeField]
    public float endAlpha;
    [SerializeField]
    public AnimationCurve alphaCurve;
    [SerializeField]
    public Vector2 anchorOffset;
  }
}
