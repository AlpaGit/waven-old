// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.LightIntensityPlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public sealed class LightIntensityPlayableAsset : PlayableAsset, ITimelineClipAsset
  {
    [SerializeField]
    private AnimationCurve m_curve = new AnimationCurve();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
      LightIntensityPlayableBehaviour template = new LightIntensityPlayableBehaviour(this.m_curve);
      return (Playable) ScriptPlayable<LightIntensityPlayableBehaviour>.Create(graph, template);
    }

    public ClipCaps clipCaps { get; }
  }
}
