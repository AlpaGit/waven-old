// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.BossObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.AssetManagement;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class BossObject : MonoBehaviour
  {
    [Header("Components")]
    [SerializeField]
    protected Animator2D m_animator2D;
    [SerializeField]
    protected PlayableDirector m_playableDirector;
    [Header("Anim/Timeline")]
    [SerializeField]
    protected TimelineAssetDictionary m_timelineAssetDictionary;
    private CharacterAnimationCallback m_animationCallback;
    private bool m_hasTimeline;
    private bool m_loaded;

    protected abstract string spawnAnimation { get; }

    protected abstract string deathAnimation { get; }

    public abstract void GoToIdle();

    protected void Awake()
    {
      this.m_animationCallback = new CharacterAnimationCallback((IAnimator2D) this.m_animator2D);
      this.m_animator2D.Initialised += new Animator2DInitialisedEventHandler(this.OnAnimatorInitialized);
      this.m_animator2D.AnimationLooped += new AnimationLoopedEventHandler(this.OnAnimationLooped);
      this.m_playableDirector.playableAsset = (PlayableAsset) null;
      CameraHandler.AddMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
      this.StartCoroutine(this.Load());
    }

    protected IEnumerator Load()
    {
      while (!AssetManager.isInitialized)
        yield return (object) null;
      yield return (object) this.LoadTimelines();
      this.m_loaded = true;
    }

    protected virtual void OnDestroy()
    {
      CameraHandler.RemoveMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_animator2D)
      {
        this.m_animator2D.AnimationLooped -= new AnimationLoopedEventHandler(this.OnAnimationLooped);
        this.m_animator2D.Initialised -= new Animator2DInitialisedEventHandler(this.OnAnimatorInitialized);
      }
      if (this.m_animationCallback != null)
      {
        this.m_animationCallback.Release();
        this.m_animationCallback = (CharacterAnimationCallback) null;
      }
      this.Clear();
      this.UnloadTimelines();
      this.m_loaded = false;
    }

    protected virtual IEnumerator LoadTimelines()
    {
      foreach (KeyValuePair<string, TimelineAsset> timelineAsset in (Dictionary<string, TimelineAsset>) this.m_timelineAssetDictionary)
        yield return (object) TimelineUtility.LoadTimelineResources(timelineAsset.Value);
    }

    protected virtual void UnloadTimelines()
    {
      foreach (KeyValuePair<string, TimelineAsset> timelineAsset in (Dictionary<string, TimelineAsset>) this.m_timelineAssetDictionary)
        TimelineUtility.UnloadTimelineResources(timelineAsset.Value);
    }

    protected void Clear()
    {
      this.m_playableDirector.Stop();
      this.m_playableDirector.playableAsset = (PlayableAsset) null;
    }

    public IEnumerator Spawn()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      BossObject bossObject = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      bossObject.PlayAnimation(bossObject.spawnAnimation, new Action(bossObject.GoToIdle));
      return false;
    }

    public IEnumerator Die()
    {
      this.PlayAnimation(this.deathAnimation);
      if (this.m_animator2D.CurrentAnimationHasLabel("die", out int _))
      {
        while (!BossObject.HasAnimationReachedLabel(this.m_animator2D, this.deathAnimation, "die"))
          yield return (object) null;
        this.m_animator2D.paused = true;
      }
      else
        Log.Warning(this.m_animator2D.GetDefinition().name + " is missing the 'die' label in the animation named '" + this.deathAnimation + "'.", 151, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BossObject.cs");
    }

    private void OnAnimatorInitialized(object sender, Animator2DInitialisedEventArgs e)
    {
      this.m_animator2D.paused = false;
      this.GoToIdle();
    }

    private void OnAnimationLooped(object sender, AnimationLoopedEventArgs e)
    {
      if (!this.m_hasTimeline)
        return;
      this.m_playableDirector.time = 0.0;
      this.m_playableDirector.Resume();
    }

    public void PlayAnimation(
      string animationName,
      Action onComplete = null,
      bool loop = false,
      bool restart = true,
      bool async = false)
    {
      string key = animationName;
      TimelineAsset asset;
      bool flag = this.m_timelineAssetDictionary.TryGetValue(key, out asset);
      if (flag && (UnityEngine.Object) null != (UnityEngine.Object) asset)
      {
        this.m_playableDirector.extrapolationMode = DirectorWrapMode.Hold;
        if ((UnityEngine.Object) asset != (UnityEngine.Object) this.m_playableDirector.playableAsset)
        {
          this.m_playableDirector.Play((PlayableAsset) asset);
        }
        else
        {
          if (restart || !this.m_animator2D.animationName.Equals(animationName))
            this.m_playableDirector.time = 0.0;
          this.m_playableDirector.Resume();
        }
        this.m_hasTimeline = true;
      }
      else
      {
        if (flag)
          Log.Warning("Boss named '" + this.gameObject.name + "' has a timeline setup for key '" + key + "' but the actual asset is null.", 202, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BossObject.cs");
        this.m_playableDirector.time = 0.0;
        this.m_playableDirector.Pause();
        this.m_hasTimeline = false;
      }
      this.m_animationCallback.Setup(animationName, restart, onComplete);
      this.m_animator2D.SetAnimation(animationName, loop, async, restart);
    }

    public static bool HasAnimationReachedLabel(
      [NotNull] Animator2D animator2D,
      string animationName,
      [NotNull] string label)
    {
      return animator2D.reachedEndOfAnimation || label.Equals(animator2D.currentLabel, StringComparison.OrdinalIgnoreCase) || !animationName.Equals(animator2D.animationName);
    }

    private void OnMapRotationChanged(
      DirectionAngle previousMapRotation,
      DirectionAngle newMapRotation)
    {
      this.transform.rotation *= previousMapRotation.GetRotation() * newMapRotation.GetInverseRotation();
    }

    protected IEnumerator PlayTimeline(TimelineAsset timelineAsset)
    {
      while (!this.m_loaded)
        yield return (object) null;
      this.m_playableDirector.playableAsset = (PlayableAsset) timelineAsset;
      this.m_playableDirector.time = 0.0;
      this.m_playableDirector.extrapolationMode = DirectorWrapMode.None;
      this.m_playableDirector.Play();
      while (!this.m_playableDirector.HasReachedEndOfAnimation())
        yield return (object) null;
    }
  }
}
