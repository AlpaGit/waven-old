// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Animator2D
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using Ankama.Animations.Events;
using Ankama.Animations.Management;
using Ankama.Animations.Rendering;
using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Animations
{
  [PublicAPI]
  [ExecuteInEditMode]
  public sealed class Animator2D : MonoBehaviour, IAnimator2D
  {
    [UsedImplicitly]
    [SerializeField]
    internal AnimatedObjectDefinition definition;
    [UsedImplicitly]
    [SerializeField]
    internal CustomisationSlot[] customisationSlots;
    [UsedImplicitly]
    [SerializeField]
    internal Animator2DRenderingMethod renderingMethod;
    [UsedImplicitly]
    [SerializeField]
    internal Color colorModifier = Color.white;
    [UsedImplicitly]
    [SerializeField]
    internal int sortingLayerIdInternal;
    [UsedImplicitly]
    [SerializeField]
    internal int sortingOrderInternal;
    [UsedImplicitly]
    [SerializeField]
    internal int overriddenFrameRate;
    private IAnimator2DRenderer m_renderer;
    private BufferRequest m_bufferRequest;
    [NonSerialized]
    private Coroutine m_initialisationCoroutine;
    [NonSerialized]
    private string m_loadedAssetBundleName = string.Empty;
    [NonSerialized]
    private bool m_started;
    [NonSerialized]
    private bool m_allowRendering;
    [NonSerialized]
    private bool m_forceRendering;
    [NonSerialized]
    private Material m_overrideMaterial;
    [NonSerialized]
    private int m_frameRate;
    [NonSerialized]
    private Graphic[] m_overrideGraphics;
    [NonSerialized]
    private bool m_paused;
    [NonSerialized]
    private bool m_reachedEndOfAnimation;
    [NonSerialized]
    private string m_animationLabel = string.Empty;
    private int m_currentAnimationFrame;
    private string m_requestedAnimationName = string.Empty;
    [NonSerialized]
    private Animation m_currentAnimation;
    [NonSerialized]
    private float m_currentAnimationFrameTime;
    [NonSerialized]
    private Animation m_pendingAnimation;
    [NonSerialized]
    private int m_pendingAnimationFrame;
    [NonSerialized]
    private bool m_pendingAnimationLoops;
    [NonSerialized]
    private float m_pendingAnimationFrameTime;
    [NonSerialized]
    private Coroutine m_pendingAnimationLoading;
    [NonSerialized]
    private bool m_capturingEvents;
    [NonSerialized]
    private int m_capturedEventCount;
    [NonSerialized]
    private Queue<DelayedEvent> m_capturedEvents;
    [NonSerialized]
    private MeshRenderer m_meshRenderer;
    [NonSerialized]
    private MeshFilter m_meshFilter;
    private MaterialPropertyBlock m_materialPropertyBlock;
    private Dictionary<string, bool> m_pendingKeywordActions;

    [PublicAPI]
    public event Animator2DInitialisedEventHandler Initialised;

    [PublicAPI]
    public event AnimationLabelChangedEventHandler AnimationLabelChanged;

    [PublicAPI]
    public event AnimationStartedEventHandler AnimationStarted;

    [PublicAPI]
    public event AnimationLoopedEventHandler AnimationLooped;

    [PublicAPI]
    public event AnimationEndedEventHandler AnimationEnded;

    [PublicAPI]
    public bool paused
    {
      get => this.m_paused;
      set
      {
        if (value == this.m_paused)
          return;
        this.m_paused = value;
        if (value)
          return;
        float time = Time.time;
        this.m_currentAnimationFrameTime = time;
        this.m_pendingAnimationFrameTime = time;
      }
    }

    [PublicAPI]
    public int frameRate
    {
      get => this.m_frameRate;
      set
      {
        if (value < 1)
        {
          this.m_frameRate = (UnityEngine.Object) null == (UnityEngine.Object) this.definition ? 30 : this.definition.defaultFrameRate;
          this.overriddenFrameRate = 0;
        }
        else
        {
          this.m_frameRate = value;
          this.overriddenFrameRate = value;
        }
      }
    }

    [PublicAPI]
    public string animationName
    {
      [NotNull] get => this.m_currentAnimation != null ? this.m_currentAnimation.name : this.m_requestedAnimationName;
    }

    [PublicAPI]
    public bool animationLoops { get; set; }

    [PublicAPI]
    public int animationFrameCount
    {
      get
      {
        if (this.m_currentAnimation == null)
          return 0;
        AnimationInstance instance = this.m_currentAnimation.instance;
        return instance == null ? 0 : (int) instance.frameCount;
      }
    }

    [PublicAPI]
    public int currentFrame
    {
      get => this.m_currentAnimationFrame;
      set
      {
        if (this.m_currentAnimation == null)
          return;
        AnimationInstance instance = this.m_currentAnimation.instance;
        if (instance == null)
          return;
        int frameCount = (int) instance.frameCount;
        int num = this.m_currentAnimationFrame;
        if (this.animationLoops)
        {
          if (value >= frameCount)
          {
            num = -1;
            value %= frameCount;
          }
        }
        else if (value > frameCount - 1)
          value = frameCount - 1;
        float time = Time.time;
        if (value == num && (double) time == (double) this.m_currentAnimationFrameTime)
          return;
        this.m_currentAnimationFrame = value;
        this.m_currentAnimationFrameTime = time;
        this.m_reachedEndOfAnimation = false;
        this.m_forceRendering = true;
        this.StartEventCapture();
        int labelCount = (int) instance.labelCount;
        if (labelCount > 0)
        {
          bool flag = false;
          AnimationLabel[] labels = instance.labels;
          for (int index = 0; index < labelCount; ++index)
          {
            AnimationLabel animationLabel = labels[index];
            int frame = animationLabel.frame;
            if (frame > num && frame <= value)
            {
              this.currentLabel = animationLabel.label;
              flag = true;
              break;
            }
          }
          if (!flag && this.m_animationLabel.Length > 0)
            this.currentLabel = string.Empty;
        }
        this.StopEventCapture();
      }
    }

    [PublicAPI]
    public string currentLabel
    {
      [NotNull] get => this.m_animationLabel;
      [NotNull] private set
      {
        this.m_animationLabel = value;
        if (this.AnimationLabelChanged == null)
          return;
        AnimationLabelChangedEventArgs changedEventArgs = new AnimationLabelChangedEventArgs(this.m_currentAnimation.name, value);
        if (this.m_capturingEvents)
          this.CaptureEvent(DelayedEvent.EventType.LabelChanged, (EventArgs) changedEventArgs);
        else
          this.AnimationLabelChanged((object) this, changedEventArgs);
      }
    }

    [PublicAPI]
    public bool reachedEndOfAnimation
    {
      get => this.m_reachedEndOfAnimation;
      private set
      {
        this.m_reachedEndOfAnimation = value;
        if (!value || this.AnimationEnded == null)
          return;
        AnimationEndedEventArgs animationEndedEventArgs = new AnimationEndedEventArgs(this.m_currentAnimation.name);
        if (this.m_capturingEvents)
          this.CaptureEvent(DelayedEvent.EventType.AnimationEnded, (EventArgs) animationEndedEventArgs);
        else
          this.AnimationEnded((object) this, animationEndedEventArgs);
      }
    }

    [PublicAPI]
    public bool hasPendingAnimation => this.m_pendingAnimation != null;

    [PublicAPI]
    public string pendingAnimationName
    {
      [NotNull] get => this.m_pendingAnimation != null ? this.m_pendingAnimation.name : string.Empty;
    }

    [PublicAPI]
    public Color color
    {
      get => this.colorModifier;
      set
      {
        this.colorModifier = value;
        this.m_renderer?.SetColor(value);
      }
    }

    [PublicAPI]
    public int sortingLayerId
    {
      get => this.sortingLayerIdInternal;
      set
      {
        this.sortingLayerIdInternal = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_meshRenderer))
          return;
        this.m_meshRenderer.sortingLayerID = value;
      }
    }

    [PublicAPI]
    public int sortingOrder
    {
      get => this.sortingOrderInternal;
      set
      {
        this.sortingOrderInternal = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_meshRenderer))
          return;
        this.m_meshRenderer.sortingOrder = value;
      }
    }

    public Animator2DInitialisationState GetInitialisationState()
    {
      if (this.m_renderer != null)
        return Animator2DInitialisationState.Initialised;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.definition)
      {
        if (!this.m_started)
          return Animator2DInitialisationState.InitialisationPending;
        if (this.m_initialisationCoroutine != null)
          return !this.enabled ? Animator2DInitialisationState.InitialisationPending : Animator2DInitialisationState.Initialising;
      }
      return Animator2DInitialisationState.None;
    }

    public AnimatedObjectDefinition GetDefinition() => this.definition;

    [PublicAPI]
    public bool CurrentAnimationHasLabel(string labelName, out int frame)
    {
      if (this.m_currentAnimation == null)
      {
        frame = 0;
        return false;
      }
      AnimationInstance instance = this.m_currentAnimation.instance;
      if (instance == null)
      {
        frame = 0;
        return false;
      }
      int labelCount = (int) instance.labelCount;
      if (labelCount == 0)
      {
        frame = 0;
        return false;
      }
      AnimationLabel[] labels = instance.labels;
      for (int index = 0; index < labelCount; ++index)
      {
        AnimationLabel animationLabel = labels[index];
        if (animationLabel.label.Equals(labelName))
        {
          frame = animationLabel.frame;
          return true;
        }
      }
      frame = 0;
      return false;
    }

    [PublicAPI]
    public bool CurrentAnimationHasLabel(
      string labelName,
      StringComparison comparisonType,
      out int frame)
    {
      if (this.m_currentAnimation == null)
      {
        frame = 0;
        return false;
      }
      AnimationInstance instance = this.m_currentAnimation.instance;
      if (instance == null)
      {
        frame = 0;
        return false;
      }
      int labelCount = (int) instance.labelCount;
      if (labelCount == 0)
      {
        frame = 0;
        return false;
      }
      AnimationLabel[] labels = instance.labels;
      for (int index = 0; index < labelCount; ++index)
      {
        AnimationLabel animationLabel = labels[index];
        if (animationLabel.label.Equals(labelName, comparisonType))
        {
          frame = animationLabel.frame;
          return true;
        }
      }
      frame = 0;
      return false;
    }

    [PublicAPI]
    public void SetRenderingMethod(Animator2DRenderingMethod value)
    {
      if (value == this.renderingMethod)
        return;
      this.renderingMethod = value;
      if (this.m_renderer == null)
        return;
      this.m_renderer.Release();
      this.m_renderer = (IAnimator2DRenderer) null;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
      {
        Debug.LogError((object) ("[Animator2D] SetRenderingMethod called but the definition asset is no longer loaded on Animator2D named '" + this.name + "'."));
      }
      else
      {
        this.CreateRenderer(this.definition);
        if (this.m_currentAnimation == null || this.m_currentAnimation.isReady)
          return;
        AnimationInstance instance = this.m_currentAnimation.instance;
        this.m_renderer.Start(instance);
        this.m_renderer.Compute(this.definition.graphics, instance, this.m_currentAnimationFrame);
      }
    }

    [PublicAPI]
    public void SetDefinition(
      [CanBeNull] AnimatedObjectDefinition value,
      Material overrideMaterial = null,
      Graphic[] overrideGraphics = null)
    {
      if ((UnityEngine.Object) value == (UnityEngine.Object) this.definition)
        return;
      this.Release(true);
      this.definition = value;
      this.m_overrideMaterial = overrideMaterial;
      this.m_overrideGraphics = overrideGraphics;
      if ((UnityEngine.Object) null == (UnityEngine.Object) value)
      {
        this.ClearCustomisationSlots();
        this.m_requestedAnimationName = string.Empty;
      }
      else
      {
        switch (this.renderingMethod)
        {
          case Animator2DRenderingMethod.Fast:
            this.ClearCustomisationSlots();
            break;
          case Animator2DRenderingMethod.Customisable:
            this.FillCustomisationSlots(value);
            break;
          default:
            throw new ArgumentOutOfRangeException("renderingMethod", (object) this.renderingMethod, (string) null);
        }
        if (!this.m_started || !this.gameObject.activeInHierarchy)
          return;
        this.m_initialisationCoroutine = this.StartCoroutine(this.Initialise());
      }
    }

    [PublicAPI]
    public void SetOverrideMaterial([CanBeNull] Material overrideMaterial)
    {
      this.m_overrideMaterial = overrideMaterial;
      if (this.m_renderer == null)
        return;
      if ((UnityEngine.Object) null == (UnityEngine.Object) overrideMaterial)
        this.m_renderer.SetMaterial(this.definition.material, this.colorModifier);
      else
        this.m_renderer.SetMaterial(overrideMaterial, this.colorModifier);
    }

    [PublicAPI]
    public void SetOverrideGraphics([CanBeNull] Graphic[] overrideGraphics)
    {
      this.m_overrideGraphics = overrideGraphics;
      if (this.m_renderer == null)
        return;
      this.m_forceRendering = true;
    }

    [PublicAPI]
    public void SetCustomisation(int customisationSlotIndex, GraphicAsset graphicAsset)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
        Debug.LogError((object) ("[Animator2D] SetCustomisation cannot be called on Animator2D named '" + this.name + "' because no definition was set."));
      else if (this.renderingMethod != Animator2DRenderingMethod.Customisable)
      {
        Debug.LogError((object) "[Animator2D] Cannot change customisation on a non-customisable renderer.");
      }
      else
      {
        if (customisationSlotIndex < 0 || customisationSlotIndex >= this.customisationSlots.Length)
          throw new ArgumentOutOfRangeException(nameof (customisationSlotIndex));
        this.customisationSlots[customisationSlotIndex].graphicAsset = graphicAsset;
        if (!(this.m_renderer is Animator2DCustomisableRenderer renderer))
          return;
        renderer.SetCustomisation(customisationSlotIndex, graphicAsset);
        this.m_forceRendering = true;
      }
    }

    [PublicAPI]
    public bool SetCustomisation([NotNull] string customisationSlotName, GraphicAsset graphicAsset)
    {
      if (customisationSlotName == null)
        throw new ArgumentNullException(nameof (customisationSlotName));
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
      {
        Debug.LogError((object) ("[Animator2D] SetCustomisation cannot be called on Animator2D named '" + this.name + "' because no definition was set."));
        return false;
      }
      if (this.renderingMethod != Animator2DRenderingMethod.Customisable)
      {
        Debug.LogError((object) "[Animator2D] Cannot change customisation on a non-customisable renderer.");
        return false;
      }
      int length = this.customisationSlots.Length;
      for (int index = 0; index < length; ++index)
      {
        if (this.customisationSlots[index].nodeName.Equals(customisationSlotName, StringComparison.OrdinalIgnoreCase))
        {
          this.customisationSlots[index].graphicAsset = graphicAsset;
          if (!(this.m_renderer is Animator2DCustomisableRenderer renderer))
            return true;
          renderer.SetCustomisation(index, graphicAsset);
          this.m_forceRendering = true;
          return true;
        }
      }
      Debug.LogWarning((object) ("[Animator2D] Could not find a customisation slot with node name '" + customisationSlotName + "'."));
      return false;
    }

    [PublicAPI]
    public void SetAnimation(string animName, bool animLoops, bool async = true, bool restart = true)
    {
      this.m_requestedAnimationName = animName != null ? animName : throw new ArgumentNullException(nameof (animName));
      this.m_pendingAnimationLoops = animLoops;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
      {
        if (this.m_renderer == null)
          return;
        Debug.LogError((object) ("[Animator2D] SetAnimation called but the definition asset is no longer loaded on Animator2D named '" + this.name + "'."));
      }
      else
      {
        if (this.m_renderer == null)
          return;
        Animation animation = this.definition.GetAnimation(animName);
        if (animation == null)
          Debug.LogWarning((object) ("[Animator2D] Could not find animation named '" + animName + "' in definition named '" + this.definition.name + "'."));
        else if (this.m_pendingAnimation == null && animation == this.m_currentAnimation)
        {
          this.animationLoops = animLoops;
          if (!restart)
            return;
          this.RestartCurrentAnimation();
        }
        else
        {
          if (this.m_pendingAnimation != null && animation == this.m_pendingAnimation)
            return;
          if (this.m_pendingAnimationLoading != null)
          {
            this.StopCoroutine(this.m_pendingAnimationLoading);
            this.m_pendingAnimationLoading = (Coroutine) null;
            if (this.m_pendingAnimation.isReady)
            {
              this.m_pendingAnimation.Unload();
              this.m_pendingAnimation = (Animation) null;
            }
          }
          if (async)
          {
            Coroutine coroutine = this.StartCoroutine(animation.LoadAsync(this.definition.assetBundleName));
            if (animation.isReady && this.enabled)
            {
              this.m_currentAnimation.Unload();
              this.StartEventCapture();
              this.StartAnimation(animation, 0, Time.time, animLoops);
              this.StopEventCapture();
            }
            else
            {
              this.m_pendingAnimation = animation;
              this.m_pendingAnimationFrame = 0;
              this.m_pendingAnimationFrameTime = Time.time;
              this.m_pendingAnimationLoading = coroutine;
            }
          }
          else
          {
            animation.Load(this.definition.assetBundleName);
            if (!animation.isReady)
              Debug.LogWarning((object) ("[Animator2D] Could not start animation named '" + animName + "' from definition named '" + this.definition.name + "'."));
            else if (this.enabled)
            {
              this.m_currentAnimation.Unload();
              this.StartEventCapture();
              this.StartAnimation(animation, 0, Time.time, animLoops);
              this.StopEventCapture();
            }
            else
            {
              this.m_pendingAnimation = animation;
              this.m_pendingAnimationFrame = 0;
            }
          }
        }
      }
    }

    [PublicAPI]
    public int GetCustomisationSlotIndex([NotNull] string customisationSlotName)
    {
      if (customisationSlotName == null)
        throw new ArgumentNullException(nameof (customisationSlotName));
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
      {
        Debug.LogError((object) ("[Animator2D] GetCustomisationSlotIndex cannot be called on Animator2D named '" + this.name + "' because no definition was set."));
        return -1;
      }
      if (this.renderingMethod != Animator2DRenderingMethod.Customisable)
      {
        Debug.LogError((object) "[Animator2D] Cannot access customisation slots on a non-customisable renderer.");
        return -1;
      }
      int length = this.customisationSlots.Length;
      for (int customisationSlotIndex = 0; customisationSlotIndex < length; ++customisationSlotIndex)
      {
        if (this.customisationSlots[customisationSlotIndex].nodeName.Equals(customisationSlotName, StringComparison.OrdinalIgnoreCase))
          return customisationSlotIndex;
      }
      return -1;
    }

    [PublicAPI]
    [NotNull]
    public MaterialPropertyBlock GetPropertyBlock()
    {
      if (this.m_materialPropertyBlock == null)
        this.m_materialPropertyBlock = new MaterialPropertyBlock();
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_meshRenderer)
        this.m_meshRenderer.GetPropertyBlock(this.m_materialPropertyBlock);
      return this.m_materialPropertyBlock;
    }

    [PublicAPI]
    public void SetPropertyBlock([NotNull] MaterialPropertyBlock materialPropertyBlock)
    {
      if (materialPropertyBlock == null)
        throw new ArgumentNullException(nameof (materialPropertyBlock));
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_meshRenderer)
        this.m_meshRenderer.SetPropertyBlock(materialPropertyBlock);
      this.m_materialPropertyBlock = materialPropertyBlock;
    }

    [PublicAPI]
    public void EnableKeyword(string keyword)
    {
      if (this.m_renderer != null)
      {
        this.m_renderer.EnableKeyword(keyword);
      }
      else
      {
        if (this.m_pendingKeywordActions == null)
          this.m_pendingKeywordActions = new Dictionary<string, bool>(2, (IEqualityComparer<string>) StringComparer.Ordinal);
        this.m_pendingKeywordActions[keyword] = true;
      }
    }

    [PublicAPI]
    public void DisableKeyword(string keyword)
    {
      if (this.m_renderer != null)
      {
        this.m_renderer.DisableKeyword(keyword);
      }
      else
      {
        if (this.m_pendingKeywordActions == null)
          this.m_pendingKeywordActions = new Dictionary<string, bool>(2, (IEqualityComparer<string>) StringComparer.Ordinal);
        this.m_pendingKeywordActions[keyword] = false;
      }
    }

    public bool IsKeywordEnabled(string keyword) => this.m_renderer != null && this.m_renderer.IsKeywordEnabled(keyword);

    [UsedImplicitly]
    private void Awake() => Animator2DRendererUtility.CreateMeshComponents(this.gameObject, out this.m_meshRenderer, out this.m_meshFilter);

    [UsedImplicitly]
    private void OnEnable()
    {
      this.m_meshRenderer.enabled = true;
      if (!this.m_started || (UnityEngine.Object) null == (UnityEngine.Object) this.definition)
        return;
      if (this.m_renderer != null)
      {
        float time = Time.time;
        this.m_currentAnimationFrameTime = time;
        this.m_pendingAnimationFrameTime = time;
        this.m_allowRendering = true;
      }
      else
        this.m_initialisationCoroutine = this.StartCoroutine(this.Initialise());
    }

    [UsedImplicitly]
    private void Start()
    {
      this.m_started = true;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.definition)
        return;
      this.m_initialisationCoroutine = this.StartCoroutine(this.Initialise());
    }

    [UsedImplicitly]
    private void OnDisable()
    {
      this.m_meshRenderer.enabled = false;
      this.Release(!this.gameObject.activeInHierarchy);
    }

    [UsedImplicitly]
    private void OnDestroy()
    {
      this.Release(true);
      Animator2DRendererUtility.DestroyMeshComponents(this.m_meshRenderer, this.m_meshFilter);
      this.m_meshRenderer = (MeshRenderer) null;
      this.m_meshFilter = (MeshFilter) null;
    }

    private IEnumerator Initialise()
    {
      Animator2D target = this;
      string assetBundleName = target.definition.assetBundleName;
      if (!string.IsNullOrEmpty(assetBundleName) && !target.m_loadedAssetBundleName.Equals(assetBundleName))
      {
        while (!AssetManager.isReady)
          yield return (object) null;
        AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(assetBundleName);
        while (!bundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) bundleLoadRequest.error != 0)
        {
          Debug.LogError((object) string.Format("[Animator2D] Could not load asset bundle named '{0}': {1}.", (object) assetBundleName, (object) bundleLoadRequest.error));
          yield break;
        }
        else
        {
          target.m_loadedAssetBundleName = assetBundleName;
          bundleLoadRequest = (AssetBundleLoadRequest) null;
        }
      }
      if (target.enabled)
      {
        target.m_frameRate = target.overriddenFrameRate < 1 ? target.definition.defaultFrameRate : target.overriddenFrameRate;
        Animation anim;
        bool loops;
        if (!string.IsNullOrEmpty(target.m_requestedAnimationName))
        {
          anim = target.definition.GetAnimation(target.m_requestedAnimationName);
          loops = target.m_pendingAnimationLoops;
          if (anim == null)
          {
            anim = target.definition.GetDefaultAnimation();
            loops = target.definition.defaultAnimationLoops;
          }
        }
        else
        {
          anim = target.definition.GetDefaultAnimation();
          loops = target.definition.defaultAnimationLoops;
        }
        if (anim == null)
        {
          Debug.LogWarning((object) ("[Animator2D] Could not find any animation to play in definition named '" + target.definition.name + "'."));
        }
        else
        {
          anim.Load(assetBundleName);
          if (anim.isReady)
          {
            target.CreateRenderer(target.definition);
            Animator2DManager.Register(target);
            target.m_allowRendering = true;
            target.StartEventCapture();
            if (target.Initialised != null)
            {
              Animator2DInitialisedEventArgs args = new Animator2DInitialisedEventArgs();
              target.CaptureEvent(DelayedEvent.EventType.Initialised, (EventArgs) args);
            }
            target.StartAnimation(anim, 0, Time.time, loops);
            target.m_bufferRequest = new BufferRequest(target, anim.instance, target.m_bufferRequest.id);
            target.StopEventCapture();
          }
          else
            Debug.LogError((object) ("[Animator2D] Could not start animation named '" + anim.name + "'."));
        }
      }
    }

    private void Release(bool unloadResources)
    {
      if (unloadResources)
      {
        if (this.m_initialisationCoroutine != null)
        {
          this.StopCoroutine(this.m_initialisationCoroutine);
          this.m_initialisationCoroutine = (Coroutine) null;
        }
        if (!string.IsNullOrEmpty(this.m_loadedAssetBundleName))
        {
          AssetManager.UnloadAssetBundle(this.m_loadedAssetBundleName);
          this.m_loadedAssetBundleName = string.Empty;
        }
        if (this.m_currentAnimation != null)
        {
          this.m_currentAnimation.Unload();
          this.m_currentAnimation = (Animation) null;
        }
        if (this.m_pendingAnimationLoading != null)
        {
          this.StopCoroutine(this.m_pendingAnimationLoading);
          this.m_pendingAnimationLoading = (Coroutine) null;
        }
        if (this.m_pendingAnimation != null)
        {
          if (this.m_pendingAnimation.isReady)
            this.m_pendingAnimation.Unload();
          this.m_pendingAnimation = (Animation) null;
        }
        this.currentLabel = string.Empty;
        this.m_currentAnimationFrame = 0;
        this.m_pendingAnimationFrame = 0;
        if (this.m_renderer != null)
        {
          Animator2DManager.Unregister(this);
          this.m_renderer.Release();
          this.m_renderer = (IAnimator2DRenderer) null;
        }
      }
      ++this.m_bufferRequest.id;
      this.m_allowRendering = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CreateRenderer(AnimatedObjectDefinition animatedObjectDefinition)
    {
      switch (this.renderingMethod)
      {
        case Animator2DRenderingMethod.Fast:
          Material rendererMaterial;
          if (animatedObjectDefinition.GetUniqueMaterialInstance(this.m_overrideMaterial, out rendererMaterial))
          {
            this.m_renderer = (IAnimator2DRenderer) new Animator2DRenderer(this.m_meshRenderer, this.m_meshFilter, rendererMaterial, (int) animatedObjectDefinition.maxNodeCount);
            break;
          }
          Graphic[] graphics1 = this.m_overrideGraphics ?? animatedObjectDefinition.graphics;
          this.m_renderer = (IAnimator2DRenderer) new Animator2DMultiMaterialRenderer(this.m_meshRenderer, this.m_meshFilter, rendererMaterial, (int) animatedObjectDefinition.maxNodeCount, graphics1);
          break;
        case Animator2DRenderingMethod.Customisable:
          Material material;
          if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_overrideMaterial)
          {
            material = animatedObjectDefinition.material;
            if ((UnityEngine.Object) null == (UnityEngine.Object) material)
              Debug.LogWarning((object) ("[Animator2D] No suitable material is available for definition named '" + animatedObjectDefinition.name + "'."));
          }
          else
            material = this.m_overrideMaterial;
          Graphic[] graphics2 = this.m_overrideGraphics ?? animatedObjectDefinition.graphics;
          string[] exposedNodeNames = animatedObjectDefinition.exposedNodeNames;
          Animator2DCustomisableRenderer dcustomisableRenderer = new Animator2DCustomisableRenderer(this.m_meshRenderer, this.m_meshFilter, material, (int) animatedObjectDefinition.maxNodeCount, graphics2, exposedNodeNames);
          int length = this.customisationSlots.Length;
          for (int index = 0; index < length; ++index)
          {
            CustomisationSlot customisationSlot = this.customisationSlots[index];
            dcustomisableRenderer.SetCustomisation(index, customisationSlot?.graphicAsset);
          }
          this.m_renderer = (IAnimator2DRenderer) dcustomisableRenderer;
          break;
        default:
          throw new ArgumentOutOfRangeException("renderingMethod", (object) this.renderingMethod, (string) null);
      }
      this.m_renderer.SetColor(this.colorModifier);
      this.m_meshRenderer.sortingLayerID = this.sortingLayerIdInternal;
      this.m_meshRenderer.sortingOrder = this.sortingOrderInternal;
      if (this.m_materialPropertyBlock != null)
        this.m_meshRenderer.SetPropertyBlock(this.m_materialPropertyBlock);
      if (this.m_pendingKeywordActions == null)
        return;
      foreach (KeyValuePair<string, bool> pendingKeywordAction in this.m_pendingKeywordActions)
      {
        if (pendingKeywordAction.Value)
          this.m_renderer.EnableKeyword(pendingKeywordAction.Key);
        else
          this.m_renderer.DisableKeyword(pendingKeywordAction.Key);
      }
      this.m_pendingKeywordActions.Clear();
    }

    private void FillCustomisationSlots(AnimatedObjectDefinition animatedObjectDefinition)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) animatedObjectDefinition)
      {
        this.customisationSlots = new CustomisationSlot[0];
      }
      else
      {
        string[] exposedNodeNames = animatedObjectDefinition.exposedNodeNames;
        int length1 = exposedNodeNames.Length;
        int length2 = this.customisationSlots.Length;
        CustomisationSlot[] customisationSlotArray = new CustomisationSlot[length1];
        for (int index1 = 0; index1 < length1; ++index1)
        {
          string str = exposedNodeNames[index1];
          CustomisationSlot customisationSlot1 = (CustomisationSlot) null;
          for (int index2 = 0; index2 < length2; ++index2)
          {
            CustomisationSlot customisationSlot2 = this.customisationSlots[index2];
            if (customisationSlot2.nodeName.Equals(str))
            {
              customisationSlot1 = customisationSlot2;
              break;
            }
          }
          if (customisationSlot1 == null)
            customisationSlot1 = new CustomisationSlot()
            {
              nodeName = str
            };
          customisationSlotArray[index1] = customisationSlot1;
        }
        this.customisationSlots = customisationSlotArray;
      }
    }

    private void ClearCustomisationSlots() => this.customisationSlots = new CustomisationSlot[0];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void StartAnimation([NotNull] Animation anim, int frame, float frameTime, bool loops)
    {
      AnimationInstance instance = anim.instance;
      this.m_renderer.Start(instance);
      int frameCount = (int) instance.frameCount;
      if (loops)
      {
        if (frame >= frameCount)
          frame %= frameCount;
      }
      else if (frame > frameCount - 1)
        frame = frameCount - 1;
      this.animationLoops = loops;
      string previousAnimation = this.m_currentAnimation == null ? string.Empty : this.m_currentAnimation.name;
      this.m_currentAnimation = anim;
      this.m_currentAnimationFrame = frame;
      this.m_currentAnimationFrameTime = frameTime;
      this.m_reachedEndOfAnimation = false;
      if (this.AnimationStarted != null)
      {
        AnimationStartedEventArgs startedEventArgs = new AnimationStartedEventArgs(anim.name, previousAnimation, frame);
        if (this.m_capturingEvents)
          this.CaptureEvent(DelayedEvent.EventType.AnimationStarted, (EventArgs) startedEventArgs);
        else
          this.AnimationStarted((object) this, startedEventArgs);
      }
      int labelCount = (int) instance.labelCount;
      if (labelCount > 0)
      {
        bool flag = false;
        AnimationLabel[] labels = instance.labels;
        for (int index = 0; index < labelCount; ++index)
        {
          AnimationLabel animationLabel = labels[index];
          if (animationLabel.frame <= frame)
          {
            this.currentLabel = animationLabel.label;
            flag = true;
            break;
          }
        }
        if (!flag && this.m_animationLabel.Length > 0)
          this.currentLabel = string.Empty;
      }
      else
        this.currentLabel = string.Empty;
      this.m_forceRendering = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void RestartCurrentAnimation()
    {
      AnimationInstance instance = this.m_currentAnimation.instance;
      this.m_currentAnimationFrame = 0;
      this.m_currentAnimationFrameTime = Time.time;
      this.m_reachedEndOfAnimation = false;
      if (this.AnimationStarted != null)
      {
        string name = this.m_currentAnimation.name;
        AnimationStartedEventArgs startedEventArgs = new AnimationStartedEventArgs(name, name, 0);
        if (this.m_capturingEvents)
          this.CaptureEvent(DelayedEvent.EventType.AnimationStarted, (EventArgs) startedEventArgs);
        else
          this.AnimationStarted((object) this, startedEventArgs);
      }
      if (instance.labelCount > (ushort) 0)
      {
        AnimationLabel label = instance.labels[0];
        if (label.frame == 0)
          this.currentLabel = label.label;
        else if (this.m_animationLabel.Length > 0)
          this.currentLabel = string.Empty;
      }
      this.m_forceRendering = true;
    }

    internal void Run(float currentTime)
    {
      if (!this.m_allowRendering)
        return;
      this.StartEventCapture();
      float num1 = 1f / (float) this.m_frameRate;
      bool flag1 = !this.m_paused && (this.animationLoops || !this.m_reachedEndOfAnimation);
      if (this.m_pendingAnimation != null)
      {
        if (this.m_pendingAnimation.isReady)
        {
          this.m_currentAnimation.Unload();
          this.StartAnimation(this.m_pendingAnimation, this.m_pendingAnimationFrame, this.m_pendingAnimationFrameTime, this.m_pendingAnimationLoops);
          this.m_pendingAnimationLoading = (Coroutine) null;
          this.m_pendingAnimation = (Animation) null;
          flag1 = !this.m_paused && !this.m_reachedEndOfAnimation;
        }
        else if (flag1)
        {
          int num2 = (int) (((double) currentTime - (double) this.m_pendingAnimationFrameTime) / (double) num1);
          this.m_pendingAnimationFrame += num2;
          this.m_pendingAnimationFrameTime += num1 * (float) num2;
        }
      }
      if (flag1)
      {
        int num3 = (int) (((double) currentTime - (double) this.m_currentAnimationFrameTime) / (double) num1);
        if (num3 > 0)
        {
          AnimationInstance instance = this.m_currentAnimation.instance;
          int frameCount = (int) instance.frameCount;
          int num4 = this.m_currentAnimationFrame;
          bool flag2 = false;
          this.m_currentAnimationFrame += num3;
          this.m_currentAnimationFrameTime += num1 * (float) num3;
          if (this.animationLoops)
          {
            if (this.m_currentAnimationFrame >= frameCount)
            {
              num4 = -1;
              this.m_currentAnimationFrame %= frameCount;
              if (this.AnimationLooped != null)
              {
                AnimationLoopedEventArgs animationLoopedEventArgs = new AnimationLoopedEventArgs(this.m_currentAnimation.name);
                if (this.m_capturingEvents)
                  this.CaptureEvent(DelayedEvent.EventType.AnimationLooped, (EventArgs) animationLoopedEventArgs);
                else
                  this.AnimationLooped((object) this, animationLoopedEventArgs);
              }
            }
          }
          else
          {
            int num5 = frameCount - 1;
            if (this.m_currentAnimationFrame > num5)
            {
              this.m_currentAnimationFrame = num5;
              flag2 = true;
            }
          }
          int labelCount = (int) instance.labelCount;
          if (labelCount > 0)
          {
            bool flag3 = false;
            AnimationLabel[] labels = instance.labels;
            for (int index = 0; index < labelCount; ++index)
            {
              AnimationLabel animationLabel = labels[index];
              int frame = animationLabel.frame;
              if (frame > num4 && frame <= this.m_currentAnimationFrame)
              {
                this.currentLabel = animationLabel.label;
                flag3 = true;
                break;
              }
            }
            if (!flag3 && num4 < 0)
              this.currentLabel = string.Empty;
          }
          this.reachedEndOfAnimation = flag2;
          this.m_forceRendering = false;
          ++this.m_bufferRequest.id;
          this.m_renderer.Compute(this.m_overrideGraphics ?? this.definition.graphics, instance, this.m_currentAnimationFrame);
          Animator2DManager.AddBufferRequest(this.m_bufferRequest);
        }
      }
      if (this.m_forceRendering)
      {
        this.m_forceRendering = false;
        ++this.m_bufferRequest.id;
        this.m_renderer.Compute(this.m_overrideGraphics ?? this.definition.graphics, this.m_currentAnimation.instance, this.m_currentAnimationFrame);
        Animator2DManager.AddBufferRequest(this.m_bufferRequest);
      }
      this.StopEventCapture();
    }

    internal void Buffer(int requestId, AnimationInstance animationInstance)
    {
      if (requestId != this.m_bufferRequest.id)
        return;
      if (this.m_currentAnimation.instance != animationInstance)
        return;
      try
      {
        this.m_renderer.Buffer(this.m_overrideGraphics ?? this.definition.graphics, animationInstance);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) "[Animator2D] Encountered an exception while buffering object.");
        Debug.LogException(ex);
      }
    }

    private void StartEventCapture()
    {
      if (this.m_capturingEvents)
        Debug.LogWarning((object) "[Animation2D] StartEventCapture called while already capturing events.");
      else
        this.m_capturingEvents = true;
    }

    private void CaptureEvent(DelayedEvent.EventType eventType, EventArgs args)
    {
      if (this.m_capturedEvents == null)
        this.m_capturedEvents = new Queue<DelayedEvent>(4);
      this.m_capturedEvents.Enqueue(new DelayedEvent(eventType, args));
      ++this.m_capturedEventCount;
    }

    private void StopEventCapture()
    {
      if (!this.m_capturingEvents)
      {
        Debug.LogWarning((object) "[Animation2D] StopEventCapture called while not capturing events.");
      }
      else
      {
        int capturedEventCount = this.m_capturedEventCount;
        this.m_capturingEvents = false;
        this.m_capturedEventCount = 0;
        if (capturedEventCount == 0)
          return;
        for (int index = 0; index < capturedEventCount; ++index)
        {
          DelayedEvent delayedEvent = this.m_capturedEvents.Dequeue();
          switch (delayedEvent.type)
          {
            case DelayedEvent.EventType.Initialised:
              Animator2DInitialisedEventHandler initialised = this.Initialised;
              if (initialised != null)
              {
                initialised((object) this, (Animator2DInitialisedEventArgs) delayedEvent.args);
                break;
              }
              break;
            case DelayedEvent.EventType.AnimationStarted:
              AnimationStartedEventHandler animationStarted = this.AnimationStarted;
              if (animationStarted != null)
              {
                animationStarted((object) this, (AnimationStartedEventArgs) delayedEvent.args);
                break;
              }
              break;
            case DelayedEvent.EventType.AnimationLooped:
              AnimationLoopedEventHandler animationLooped = this.AnimationLooped;
              if (animationLooped != null)
              {
                animationLooped((object) this, (AnimationLoopedEventArgs) delayedEvent.args);
                break;
              }
              break;
            case DelayedEvent.EventType.LabelChanged:
              AnimationLabelChangedEventHandler animationLabelChanged = this.AnimationLabelChanged;
              if (animationLabelChanged != null)
              {
                animationLabelChanged((object) this, (AnimationLabelChangedEventArgs) delayedEvent.args);
                break;
              }
              break;
            case DelayedEvent.EventType.AnimationEnded:
              AnimationEndedEventHandler animationEnded = this.AnimationEnded;
              if (animationEnded != null)
              {
                animationEnded((object) this, (AnimationEndedEventArgs) delayedEvent.args);
                break;
              }
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
        }
      }
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
