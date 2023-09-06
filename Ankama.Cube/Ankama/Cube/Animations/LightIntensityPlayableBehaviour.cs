// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.LightIntensityPlayableBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.SRP;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Animations
{
  public sealed class LightIntensityPlayableBehaviour : PlayableBehaviour
  {
    private readonly AnimationCurve m_curve;
    private float m_originalIntensity;

    [UsedImplicitly]
    public LightIntensityPlayableBehaviour()
    {
    }

    public LightIntensityPlayableBehaviour(AnimationCurve curve) => this.m_curve = curve;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
      if (this.m_curve == null)
        return;
      CubeSRP.SetLightIntensityFactor((object) this, this.m_curve.Evaluate((float) playable.GetTime<Playable>() / (float) playable.GetDuration<Playable>()));
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) => CubeSRP.RemoveLightIntensityFactor((object) this);

    public override void OnPlayableDestroy(Playable playable) => CubeSRP.RemoveLightIntensityFactor((object) this);
  }
}
