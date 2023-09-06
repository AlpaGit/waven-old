// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Playables.Animator2DPlayableBehaviour
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Animations.Playables
{
  [Serializable]
  internal sealed class Animator2DPlayableBehaviour : PlayableBehaviour
  {
    [UsedImplicitly]
    [SerializeField]
    internal string animationName;
    [UsedImplicitly]
    [SerializeField]
    internal bool loop;
    [NonSerialized]
    internal Animator2D animator2D;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.animator2D || string.IsNullOrEmpty(this.animationName))
        return;
      if (!this.animator2D.animationName.Equals(this.animationName))
        this.animator2D.SetAnimation(this.animationName, this.loop, false, true);
      this.animator2D.currentFrame = 0;
      this.animator2D.paused = false;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.animator2D || !this.animator2D.animationName.Equals(this.animationName))
        return;
      this.animator2D.paused = true;
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.animator2D || string.IsNullOrEmpty(this.animationName))
        return;
      if (info.evaluationType == FrameData.EvaluationType.Evaluate)
      {
        this.animator2D.paused = true;
        this.animator2D.currentFrame = (int) (playable.GetTime<Playable>() * (double) this.animator2D.frameRate);
      }
      else
        this.animator2D.paused = false;
    }
  }
}
