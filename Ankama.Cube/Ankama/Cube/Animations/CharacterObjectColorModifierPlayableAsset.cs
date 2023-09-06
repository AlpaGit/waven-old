// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CharacterObjectColorModifierPlayableAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public sealed class CharacterObjectColorModifierPlayableAsset : PlayableAsset, ITimelineClipAsset
  {
    [SerializeField]
    private Color m_startColor = Color.white;
    [SerializeField]
    private Color m_endColor = Color.white;

    public Color startColor => this.m_startColor;

    public Color endColor => this.m_endColor;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) => Playable.Null;

    public ClipCaps clipCaps { get; } = ClipCaps.Extrapolation;
  }
}
