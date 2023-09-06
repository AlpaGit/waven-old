// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Playables.Animator2DPlayableAsset
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Animations.Playables
{
  [Serializable]
  internal sealed class Animator2DPlayableAsset : PlayableAsset, ITimelineClipAsset
  {
    [UsedImplicitly]
    [SerializeField]
    internal string animationName = string.Empty;
    [UsedImplicitly]
    [SerializeField]
    internal bool loop;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      Animator2D genericBinding = owner.GetComponent<PlayableDirector>().GetGenericBinding((UnityEngine.Object) this) as Animator2D;
      ScriptPlayable<Animator2DPlayableBehaviour> playable = ScriptPlayable<Animator2DPlayableBehaviour>.Create(graph);
      Animator2DPlayableBehaviour behaviour = playable.GetBehaviour();
      behaviour.animator2D = genericBinding;
      behaviour.animationName = this.animationName;
      behaviour.loop = this.loop;
      return (Playable) playable;
    }

    public ClipCaps clipCaps => !this.loop ? ClipCaps.None : ClipCaps.Looping;
  }
}
