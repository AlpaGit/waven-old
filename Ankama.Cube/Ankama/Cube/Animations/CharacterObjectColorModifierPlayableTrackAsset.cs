// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CharacterObjectColorModifierPlayableTrackAsset
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps.Objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  [TrackClipType(typeof (CharacterObjectColorModifierPlayableAsset))]
  [TrackColor(0.9254902f, 0.05882353f, 0.08235294f)]
  public sealed class CharacterObjectColorModifierPlayableTrackAsset : TrackAsset
  {
    [SerializeField]
    private bool m_writeDefaults = true;

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
      CharacterObjectContext context = TimelineContextUtility.GetContext<CharacterObjectContext>(graph);
      if (context == null)
      {
        DummyPlayableBehaviour template = new DummyPlayableBehaviour();
        return (Playable) ScriptPlayable<DummyPlayableBehaviour>.Create(graph, template);
      }
      CharacterObjectColorModifierPlayableAsset lastClip = this.m_writeDefaults ? this.GetLastClip() : (CharacterObjectColorModifierPlayableAsset) null;
      CharacterObjectColorModifierPlayableMixer template1 = new CharacterObjectColorModifierPlayableMixer(context, go, this.GetClips(), lastClip);
      return (Playable) ScriptPlayable<CharacterObjectColorModifierPlayableMixer>.Create(graph, template1, inputCount);
    }

    private CharacterObjectColorModifierPlayableAsset GetLastClip()
    {
      List<TimelineClip> clips = this.m_Clips;
      int count = clips.Count;
      return count <= 0 ? (CharacterObjectColorModifierPlayableAsset) null : (CharacterObjectColorModifierPlayableAsset) clips[count - 1].asset;
    }
  }
}
