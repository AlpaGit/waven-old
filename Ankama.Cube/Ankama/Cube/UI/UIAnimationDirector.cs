// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.UIAnimationDirector
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.UI
{
  [RequireComponent(typeof (PlayableDirector))]
  public class UIAnimationDirector : MonoBehaviour
  {
    [SerializeField]
    private List<UIAnimationDescription> m_animations;
    private PlayableDirector m_playableDirector;

    public IEnumerator Load()
    {
      foreach (UIAnimationDescription animation in this.m_animations)
        yield return (object) TimelineUtility.LoadTimelineResources(animation.animation);
    }

    public void Unload()
    {
      foreach (UIAnimationDescription animation in this.m_animations)
        TimelineUtility.UnloadTimelineResources(animation.animation);
    }

    public PlayableDirector GetDirector()
    {
      if ((UnityEngine.Object) this.m_playableDirector == (UnityEngine.Object) null)
        this.m_playableDirector = this.GetComponent<PlayableDirector>();
      return this.m_playableDirector;
    }

    public TimelineAsset GetAnimation(string name)
    {
      foreach (UIAnimationDescription animation in this.m_animations)
      {
        if (string.Equals(name, animation.Name, StringComparison.OrdinalIgnoreCase))
          return animation.animation;
      }
      return (TimelineAsset) null;
    }

    public List<UIAnimationDescription> GetAnimations() => this.m_animations;

    public void SetAnimation(int i, UIAnimationDescription animations) => this.m_animations[i] = animations;

    public void AddAnimation(string newAnimation, TimelineAsset o) => this.m_animations.Add(new UIAnimationDescription()
    {
      Name = newAnimation,
      animation = o
    });

    public void DeleteAnimation(int index) => this.m_animations.RemoveAt(index);

    public void EditAnimation(TimelineAsset timelineAsset)
    {
      if ((UnityEngine.Object) this.m_playableDirector == (UnityEngine.Object) null)
        this.m_playableDirector = this.GetComponent<PlayableDirector>();
      this.m_playableDirector.playableAsset = (PlayableAsset) timelineAsset;
    }
  }
}
