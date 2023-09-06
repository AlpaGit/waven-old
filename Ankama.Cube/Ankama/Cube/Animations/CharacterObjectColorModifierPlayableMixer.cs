// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CharacterObjectColorModifierPlayableMixer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps.Objects;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public sealed class CharacterObjectColorModifierPlayableMixer : PlayableBehaviour
  {
    private readonly CharacterObjectContext m_characterObjectContext;
    private readonly GameObject m_owner;
    private readonly IEnumerable<TimelineClip> m_clips;
    private readonly CharacterObjectColorModifierPlayableAsset m_defaultsSource;
    private CharacterObject m_characterObject;
    private CharacterObjectColorModifierPlayableAsset m_currentClip;

    [UsedImplicitly]
    public CharacterObjectColorModifierPlayableMixer()
    {
    }

    public CharacterObjectColorModifierPlayableMixer(
      [NotNull] CharacterObjectContext characterObjectContext,
      GameObject owner,
      IEnumerable<TimelineClip> clips,
      CharacterObjectColorModifierPlayableAsset defaultsSource)
    {
      this.m_characterObjectContext = characterObjectContext;
      this.m_owner = owner;
      this.m_clips = clips;
      this.m_defaultsSource = defaultsSource;
    }

    public override void OnGraphStart(Playable playable)
    {
      CharacterObject characterObject = this.m_characterObjectContext.characterObject;
      if ((Object) null == (Object) characterObject)
      {
        if ((Object) null == (Object) this.m_owner)
          return;
        characterObject = this.m_owner.GetComponent<CharacterObject>();
        if ((Object) null == (Object) characterObject)
          return;
      }
      this.m_characterObject = characterObject;
    }

    public override void OnGraphStop(Playable playable)
    {
      if ((Object) null == (Object) this.m_characterObject)
      {
        if (this.m_characterObjectContext == null)
          return;
        this.m_characterObject = this.m_characterObjectContext.characterObject;
        if ((Object) null == (Object) this.m_characterObject)
          return;
      }
      if (!((Object) null != (Object) this.m_defaultsSource))
        return;
      this.m_characterObject.SetColorModifier(this.m_defaultsSource.endColor);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
      if ((Object) null == (Object) this.m_characterObject)
        return;
      double time = playable.GetGraph<Playable>().GetRootPlayable(0).GetTime<Playable>();
      foreach (TimelineClip clip in this.m_clips)
      {
        double start = clip.start;
        if (time < start)
        {
          if (clip.IsPreExtrapolatedTime(time))
          {
            CharacterObjectColorModifierPlayableAsset asset = (CharacterObjectColorModifierPlayableAsset) clip.asset;
            this.m_characterObject.SetColorModifier(asset.startColor);
            this.m_currentClip = asset;
            return;
          }
        }
        else
        {
          double end = clip.end;
          if (time <= end)
          {
            CharacterObjectColorModifierPlayableAsset asset = (CharacterObjectColorModifierPlayableAsset) clip.asset;
            float t = Mathf.InverseLerp((float) start, (float) end, (float) time);
            this.m_characterObject.SetColorModifier(Color.Lerp(asset.startColor, asset.endColor, t));
            this.m_currentClip = asset;
            return;
          }
          if (clip.IsPostExtrapolatedTime(time))
          {
            CharacterObjectColorModifierPlayableAsset asset = (CharacterObjectColorModifierPlayableAsset) clip.asset;
            this.m_characterObject.SetColorModifier(asset.endColor);
            this.m_currentClip = asset;
            return;
          }
        }
      }
      if (!((Object) null != (Object) this.m_currentClip))
        return;
      this.m_characterObject.SetColorModifier(this.m_currentClip.endColor);
      this.m_currentClip = (CharacterObjectColorModifierPlayableAsset) null;
    }
  }
}
