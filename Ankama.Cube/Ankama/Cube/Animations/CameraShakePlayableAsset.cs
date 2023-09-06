// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CameraShakePlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public sealed class CameraShakePlayableAsset : PlayableAsset, ITimelineClipAsset
  {
    [SerializeField]
    private AnimationCurve m_curve = new AnimationCurve();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      FightContext fightContext = TimelineContextUtility.GetFightContext(graph);
      if ((Object) null != (Object) fightContext)
      {
        FightStatus local = FightStatus.local;
        if (local != null && fightContext.fightId != local.fightId)
          return Playable.Null;
      }
      CameraShakePlayableBehaviour template = new CameraShakePlayableBehaviour(this.m_curve);
      return (Playable) ScriptPlayable<CameraShakePlayableBehaviour>.Create(graph, template);
    }

    public ClipCaps clipCaps { get; }
  }
}
