// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CameraShakePlayableBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Animations
{
  public sealed class CameraShakePlayableBehaviour : PlayableBehaviour
  {
    private readonly AnimationCurve m_curve;

    [UsedImplicitly]
    public CameraShakePlayableBehaviour()
    {
    }

    public CameraShakePlayableBehaviour(AnimationCurve curve) => this.m_curve = curve;

    public override void PrepareFrame(Playable playable, FrameData info)
    {
      if (this.m_curve == null)
        return;
      CameraHandler current = CameraHandler.current;
      if ((Object) null == (Object) current)
        return;
      float num = this.m_curve.Evaluate((float) playable.GetTime<Playable>() / (float) playable.GetDuration<Playable>());
      current.AddShake(num);
    }
  }
}
