// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.FloorMechanismAnimator
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public sealed class FloorMechanismAnimator : MonoBehaviour, IAnimator2D
  {
    public const string Spawn = "spawn";
    public const string Idle = "idle";
    public const string Detection = "detection";
    public const string AllyActivation = "ally_activation";
    public const string OpponentActivation = "opponent_activation";
    public const string Destruction = "die";
    [SerializeField]
    private AnimationClip m_spawnAnimation;
    [SerializeField]
    private AnimationClip m_idleAnimation;
    [SerializeField]
    private AnimationClip m_detectionAnimation;
    [SerializeField]
    private AnimationClip m_allyActivationAnimation;
    [SerializeField]
    private AnimationClip m_opponentActivationAnimation;
    [SerializeField]
    private AnimationClip m_destructionAnimation;
    [HideInInspector]
    [SerializeField]
    private Renderer[] m_renderers;
    private UnityEngine.Animation m_controller;
    private AnimationClip m_currentAnimation;
    private string m_currentLabel = string.Empty;
    private Color m_colorModifier;
    private int m_sortingLayerId;
    private int m_sortingOrder;

    public event Animator2DInitialisedEventHandler Initialised;

    public event AnimationStartedEventHandler AnimationStarted;

    public event AnimationLabelChangedEventHandler AnimationLabelChanged;

    public event AnimationLoopedEventHandler AnimationLooped;

    public event AnimationEndedEventHandler AnimationEnded;

    [PublicAPI]
    public bool HasSpawnAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_spawnAnimation;

    [PublicAPI]
    public bool TryGetSpawnAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spawnAnimation)
      {
        animName = this.m_spawnAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    [PublicAPI]
    public bool HasIdleAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_idleAnimation;

    [PublicAPI]
    public bool TryGetIdleAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleAnimation)
      {
        animName = this.m_idleAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    [PublicAPI]
    public bool HasDetectionAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_detectionAnimation;

    [PublicAPI]
    public bool TryGetDetectionAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_detectionAnimation)
      {
        animName = this.m_detectionAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    [PublicAPI]
    public bool HasAllyActivationAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_allyActivationAnimation;

    [PublicAPI]
    public bool TryGetAllyActivationAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_allyActivationAnimation)
      {
        animName = this.m_allyActivationAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    [PublicAPI]
    public bool HasOpponentActivationAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_opponentActivationAnimation;

    [PublicAPI]
    public bool TryGetOpponentActivationAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_opponentActivationAnimation)
      {
        animName = this.m_opponentActivationAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    [PublicAPI]
    public bool HasDestructionAnimation() => (UnityEngine.Object) null != (UnityEngine.Object) this.m_destructionAnimation;

    [PublicAPI]
    public bool TryGetDestructionAnimationName([NotNull] out string animName)
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_destructionAnimation)
      {
        animName = this.m_destructionAnimation.name;
        return true;
      }
      animName = string.Empty;
      return false;
    }

    public bool paused
    {
      get => this.m_controller.isPlaying;
      set
      {
        if (value)
          this.m_controller.Play(PlayMode.StopAll);
        else
          this.m_controller.Stop();
      }
    }

    public int frameRate
    {
      get
      {
        AnimationClip currentAnimation = this.m_currentAnimation;
        return !((UnityEngine.Object) null == (UnityEngine.Object) currentAnimation) ? (int) currentAnimation.frameRate : 60;
      }
      set => throw new NotImplementedException("Cannot set frame rate on an FloorMechanismAnimator component.");
    }

    public string animationName
    {
      get
      {
        AnimationClip currentAnimation = this.m_currentAnimation;
        return !((UnityEngine.Object) null == (UnityEngine.Object) currentAnimation) ? currentAnimation.name : string.Empty;
      }
    }

    public bool animationLoops
    {
      get => (UnityEngine.Object) null != (UnityEngine.Object) this.m_controller && this.m_controller.wrapMode == WrapMode.Loop;
      set
      {
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_controller))
          return;
        this.m_controller.wrapMode = value ? WrapMode.Loop : WrapMode.ClampForever;
      }
    }

    public int animationFrameCount
    {
      get
      {
        AnimationClip currentAnimation = this.m_currentAnimation;
        return !((UnityEngine.Object) null == (UnityEngine.Object) currentAnimation) ? Mathf.CeilToInt(currentAnimation.length * currentAnimation.frameRate) : 0;
      }
    }

    public int currentFrame
    {
      get
      {
        AnimationClip currentAnimation = this.m_currentAnimation;
        return (UnityEngine.Object) null == (UnityEngine.Object) currentAnimation ? 0 : Mathf.FloorToInt(this.m_controller[this.m_currentAnimation.name].time * currentAnimation.frameRate);
      }
      set
      {
        AnimationClip currentAnimation = this.m_currentAnimation;
        if ((UnityEngine.Object) null == (UnityEngine.Object) currentAnimation)
          return;
        string name = this.m_currentAnimation.name;
        float length = currentAnimation.length;
        AnimationState animationState = this.m_controller[name];
        animationState.time = Mathf.Clamp((float) value / currentAnimation.frameRate, 0.0f, length);
        if (this.m_controller.wrapMode == WrapMode.Loop)
          return;
        if (this.AnimationEnded != null)
        {
          int num = this.reachedEndOfAnimation ? 1 : 0;
          this.reachedEndOfAnimation = Mathf.Approximately(animationState.time, length);
          if (num != 0 || !this.reachedEndOfAnimation)
            return;
          this.AnimationEnded((object) this, new AnimationEndedEventArgs(name));
        }
        else
          this.reachedEndOfAnimation = Mathf.Approximately(animationState.time, length);
      }
    }

    public string currentLabel => this.m_currentLabel;

    public bool reachedEndOfAnimation { get; private set; }

    public Color color
    {
      get => this.m_colorModifier;
      set
      {
        if (this.m_colorModifier == value)
          return;
        Renderer[] renderers = this.m_renderers;
        int length = renderers.Length;
        for (int index = 0; index < length; ++index)
          renderers[index].material.color = value;
        this.m_colorModifier = value;
      }
    }

    public int sortingLayerId
    {
      get => this.m_sortingLayerId;
      set
      {
        if (this.m_sortingLayerId == value)
          return;
        Renderer[] renderers = this.m_renderers;
        int length = renderers.Length;
        for (int index = 0; index < length; ++index)
          renderers[index].sortingLayerID = value;
        this.m_sortingLayerId = value;
      }
    }

    public int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        if (this.m_sortingOrder == value)
          return;
        Renderer[] renderers = this.m_renderers;
        int length = renderers.Length;
        for (int index = 0; index < length; ++index)
          renderers[index].sortingOrder = value;
        this.m_sortingOrder = value;
      }
    }

    public Animator2DInitialisationState GetInitialisationState() => Animator2DInitialisationState.Initialised;

    public bool CurrentAnimationHasLabel(string labelName, out int frame)
    {
      AnimationEvent[] events = this.m_currentAnimation.events;
      int length = events.Length;
      for (int index = 0; index < length; ++index)
      {
        AnimationEvent animationEvent = events[index];
        if (animationEvent.functionName.Equals("SetLabel") && animationEvent.stringParameter.Equals(labelName))
        {
          frame = Mathf.FloorToInt(animationEvent.time * this.m_currentAnimation.frameRate);
          return true;
        }
      }
      frame = 0;
      return false;
    }

    public bool CurrentAnimationHasLabel(
      string labelName,
      StringComparison comparisonType,
      out int frame)
    {
      AnimationEvent[] events = this.m_currentAnimation.events;
      int length = events.Length;
      for (int index = 0; index < length; ++index)
      {
        AnimationEvent animationEvent = events[index];
        if (animationEvent.functionName.Equals("SetLabel") && animationEvent.stringParameter.Equals(labelName, comparisonType))
        {
          frame = Mathf.FloorToInt(animationEvent.time * this.m_currentAnimation.frameRate);
          return true;
        }
      }
      frame = 0;
      return false;
    }

    public void SetAnimation(string animName, bool animLoops, bool async = true, bool restart = true)
    {
      bool reachedEndOfAnimation = this.reachedEndOfAnimation;
      AnimationClip currentAnimation = this.m_currentAnimation;
      string previousAnimation;
      if ((UnityEngine.Object) null != (UnityEngine.Object) currentAnimation)
      {
        previousAnimation = currentAnimation.name;
        if (previousAnimation.Equals(animName))
        {
          if (!restart)
            return;
          this.m_currentLabel = string.Empty;
          AnimationState animationState = this.m_controller[animName];
          if (animationState.enabled)
            animationState.time = 0.0f;
          else
            this.m_controller.Play(animName, PlayMode.StopAll);
          if (this.m_controller.wrapMode != WrapMode.Loop)
          {
            this.reachedEndOfAnimation = Mathf.Approximately(0.0f, currentAnimation.length);
            if (this.AnimationEnded == null || !this.reachedEndOfAnimation)
              return;
            this.AnimationEnded((object) this, new AnimationEndedEventArgs(animName));
            return;
          }
          this.reachedEndOfAnimation = false;
          return;
        }
      }
      else
        previousAnimation = string.Empty;
      this.m_currentLabel = string.Empty;
      if (!this.m_controller.Play(animName, PlayMode.StopAll))
      {
        Log.Warning("Could not play animation named '" + animName + "' on GameObject named '" + this.name + "'.", 479, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\FloorMechanismAnimator.cs");
      }
      else
      {
        AnimationState animationState = this.m_controller[animName];
        if ((TrackedReference) null == (TrackedReference) animationState)
        {
          Log.Warning("Could not play animation named '" + animName + "' on GameObject named '" + this.name + "'.", 495, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\FloorMechanismAnimator.cs");
        }
        else
        {
          this.m_currentAnimation = animationState.clip;
          this.reachedEndOfAnimation = this.m_controller.wrapMode != WrapMode.Loop && (double) animationState.time >= (double) animationState.length;
          if (this.AnimationStarted != null)
            this.AnimationStarted((object) this, new AnimationStartedEventArgs(animName, previousAnimation, 0));
          if (this.AnimationEnded == null || !this.reachedEndOfAnimation || reachedEndOfAnimation)
            return;
          this.AnimationEnded((object) this, new AnimationEndedEventArgs(animName));
        }
      }
    }

    public void SetLabel([NotNull] string value)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_currentAnimation)
      {
        Log.Warning("SetLabel called while no animation is playing.", 527, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\FloorMechanismAnimator.cs");
      }
      else
      {
        if (this.m_currentLabel.Equals(value))
          return;
        this.m_currentLabel = value;
        if (this.AnimationLabelChanged == null)
          return;
        this.AnimationLabelChanged((object) this, new AnimationLabelChangedEventArgs(this.m_currentAnimation.name, string.Empty));
      }
    }

    public void ClearLabel()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_currentAnimation)
      {
        Log.Warning("SetLabel called while no animation is playing.", 559, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\FloorMechanismAnimator.cs");
      }
      else
      {
        if (this.m_currentLabel.Length == 0)
          return;
        this.m_currentLabel = string.Empty;
        if (this.AnimationLabelChanged == null)
          return;
        this.AnimationLabelChanged((object) this, new AnimationLabelChangedEventArgs(this.m_currentAnimation.name, string.Empty));
      }
    }

    private IEnumerable<AnimationClip> EnumerateAnimationProperties()
    {
      yield return this.m_spawnAnimation;
      yield return this.m_idleAnimation;
      yield return this.m_detectionAnimation;
      yield return this.m_allyActivationAnimation;
      yield return this.m_opponentActivationAnimation;
      yield return this.m_destructionAnimation;
    }

    private void Awake()
    {
      this.m_controller = this.GetComponent<UnityEngine.Animation>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_controller)
      {
        this.m_controller = this.gameObject.AddComponent<UnityEngine.Animation>();
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spawnAnimation)
          this.m_controller.AddClip(this.m_spawnAnimation, this.m_spawnAnimation.name);
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_idleAnimation)
          this.m_controller.AddClip(this.m_idleAnimation, this.m_idleAnimation.name);
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_detectionAnimation)
          this.m_controller.AddClip(this.m_detectionAnimation, this.m_detectionAnimation.name);
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_allyActivationAnimation)
          this.m_controller.AddClip(this.m_allyActivationAnimation, this.m_allyActivationAnimation.name);
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_opponentActivationAnimation)
          this.m_controller.AddClip(this.m_opponentActivationAnimation, this.m_opponentActivationAnimation.name);
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_destructionAnimation)
          this.m_controller.AddClip(this.m_destructionAnimation, this.m_destructionAnimation.name);
      }
      else
      {
        int clipCount = this.m_controller.GetClipCount();
        if (clipCount > 0)
        {
          int index1 = 0;
          string[] strArray = new string[clipCount];
          IEnumerator enumerator = this.m_controller.GetEnumerator();
label_26:
          while (enumerator.MoveNext())
          {
            AnimationState current = enumerator.Current as AnimationState;
            if ((TrackedReference) null != (TrackedReference) current)
            {
              string name = current.name;
              AnimationClip clip = current.clip;
              if ((UnityEngine.Object) null != (UnityEngine.Object) clip)
              {
                foreach (AnimationClip animationProperty in this.EnumerateAnimationProperties())
                {
                  if ((UnityEngine.Object) clip == (UnityEngine.Object) animationProperty)
                  {
                    if (animationProperty.name.Equals(name))
                      goto label_26;
                  }
                }
              }
              strArray[index1] = current.name;
              ++index1;
            }
          }
          for (int index2 = 0; index2 < index1; ++index2)
            this.m_controller.RemoveClip(strArray[index2]);
        }
        foreach (AnimationClip animationProperty in this.EnumerateAnimationProperties())
        {
          if ((UnityEngine.Object) null != (UnityEngine.Object) animationProperty && (TrackedReference) null == (TrackedReference) this.m_controller[animationProperty.name])
            this.m_controller.AddClip(animationProperty, animationProperty.name);
        }
      }
      this.m_controller.playAutomatically = false;
      if (this.Initialised == null)
        return;
      this.Initialised((object) this, new Animator2DInitialisedEventArgs());
    }

    private void LateUpdate()
    {
      AnimationClip currentAnimation = this.m_currentAnimation;
      if ((UnityEngine.Object) null == (UnityEngine.Object) currentAnimation)
        return;
      UnityEngine.Animation controller = this.m_controller;
      if (controller.isPlaying)
      {
        if (controller.wrapMode != WrapMode.Loop)
        {
          if (this.reachedEndOfAnimation)
            return;
          string name = currentAnimation.name;
          AnimationState animationState = this.m_controller[name];
          if ((double) animationState.time < (double) animationState.length)
            return;
          this.reachedEndOfAnimation = true;
          if (this.AnimationEnded == null)
            return;
          this.AnimationEnded((object) this, new AnimationEndedEventArgs(name));
        }
        else
        {
          if (this.AnimationLooped == null)
            return;
          string name = currentAnimation.name;
          AnimationState animationState = this.m_controller[name];
          float time = animationState.time;
          float length = animationState.length;
          int num = (int) ((double) Mathf.Max(0.0f, time - Time.deltaTime) / (double) length);
          if ((int) ((double) time / (double) length) <= num)
            return;
          this.AnimationLooped((object) this, new AnimationLoopedEventArgs(name));
        }
      }
      else
      {
        if (this.reachedEndOfAnimation)
          return;
        this.reachedEndOfAnimation = true;
        if (this.AnimationEnded == null)
          return;
        this.AnimationEnded((object) this, new AnimationEndedEventArgs(currentAnimation.name));
      }
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_controller))
        return;
      this.m_controller.Stop();
    }

    [SpecialName]
    GameObject IAnimator2D.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IAnimator2D.get_transform() => this.transform;

    [SpecialName]
    bool IAnimator2D.get_enabled() => this.enabled;

    [SpecialName]
    void IAnimator2D.set_enabled(bool value) => this.enabled = value;

    [SpecialName]
    bool IAnimator2D.get_isActiveAndEnabled() => this.isActiveAndEnabled;
  }
}
