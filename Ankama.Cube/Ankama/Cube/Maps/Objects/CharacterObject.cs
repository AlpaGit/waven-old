// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CharacterObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.AssetManagement;
using Ankama.AssetManagement.AssetBundles;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Extensions;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class CharacterObject : 
    MovableIsoObject,
    IMovableObject,
    ICharacterObject,
    IMovableIsoObject,
    IIsoObject,
    IObjectWithFocus,
    ITimelineContextProvider,
    ITooltipDataProvider,
    IObjectWithCounterEffects
  {
    [SerializeField]
    protected PlayableDirector m_playableDirector;
    [SerializeField]
    protected Transform m_attachableEffectsContainer;
    protected Ankama.Cube.Data.Direction m_direction = Ankama.Cube.Data.Direction.SouthEast;
    protected Component m_deathEffectInstance;
    protected Component m_spawnEffectInstance;
    protected readonly Dictionary<PropertyId, VisualEffect> m_propertyEffects = new Dictionary<PropertyId, VisualEffect>((IEqualityComparer<PropertyId>) PropertyIdComparer.instance);
    protected bool m_hasTimeline;
    protected DirectionAngle m_mapRotation;
    protected CharacterAnimationCallback m_animationCallback;
    protected bool m_isFocused;
    private Color m_colorModifier = Color.white;
    private BundleCategory m_activeCharacterDataBundleCategory;
    private BundleCategory m_animatedCharacterDataBundleCategory;
    private const float SlideCellTraversalTime = 2f;
    private FloatingCounterFeedback m_currentFloatingCounterFeedback;

    public DirectionAngle mapRotation => this.m_mapRotation;

    protected abstract IAnimator2D GetAnimator();

    protected abstract AnimatedCharacterData GetAnimatedCharacterData();

    protected abstract void SetAnimatedCharacterData([NotNull] AnimatedCharacterData data);

    protected abstract void ClearAnimatedCharacterData();

    protected void InitializeAnimator()
    {
      IAnimator2D animator = this.GetAnimator();
      if (animator == null)
        return;
      animator.AnimationLooped += new AnimationLoopedEventHandler(this.OnAnimationLooped);
      this.m_animationCallback = new CharacterAnimationCallback(animator);
    }

    protected void ReleaseAnimator()
    {
      IAnimator2D animator = this.GetAnimator();
      if (animator == null)
        return;
      animator.AnimationLooped -= new AnimationLoopedEventHandler(this.OnAnimationLooped);
      this.ClearAnimatorDefinition();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.InitializeAnimator();
      this.m_playableDirector.playableAsset = (PlayableAsset) null;
      this.m_playableDirector.extrapolationMode = DirectorWrapMode.Hold;
      TimelineContextUtility.SetContextProvider(this.m_playableDirector, (ITimelineContextProvider) this);
      CameraHandler.AddMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
    }

    protected virtual void OnDisable()
    {
      CameraHandler.RemoveMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
      this.DestroyAttachedEffects();
      if ((Object) null != (Object) this.m_playableDirector)
      {
        TimelineContextUtility.ClearFightContext(this.m_playableDirector);
        this.m_playableDirector.Stop();
        this.m_playableDirector.playableAsset = (PlayableAsset) null;
      }
      if (this.m_animationCallback != null)
      {
        this.m_animationCallback.Release();
        this.m_animationCallback = (CharacterAnimationCallback) null;
      }
      this.ReleaseAnimator();
      AnimatedCharacterData animatedCharacterData = this.GetAnimatedCharacterData();
      if ((Object) null != (Object) animatedCharacterData)
      {
        animatedCharacterData.UnloadTimelineResources();
        this.ClearAnimatedCharacterData();
        AssetManager.UnloadAssetBundle(AssetBundlesUtility.GetAnimatedCharacterDataBundle(this.m_animatedCharacterDataBundleCategory));
        this.m_animatedCharacterDataBundleCategory = BundleCategory.None;
      }
      if (this.m_activeCharacterDataBundleCategory == BundleCategory.None)
        return;
      AssetManager.UnloadAssetBundle(AssetBundlesUtility.GetAnimatedCharacterDataBundle(this.m_activeCharacterDataBundleCategory));
      this.m_activeCharacterDataBundleCategory = BundleCategory.None;
    }

    public IEnumerator LoadAnimationDefinitions(int skinId, Gender gender = Gender.Male)
    {
      CharacterSkinDefinition characterSkinDefinition;
      if (!RuntimeData.characterSkinDefinitions.TryGetValue(skinId, out characterSkinDefinition))
      {
        Log.Error(string.Format("Could not find character skin definition with id {0}.", (object) skinId), 150, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
      }
      else
      {
        BundleCategory bundleCategory = characterSkinDefinition.bundleCategory;
        string bundleName = AssetBundlesUtility.GetAnimatedCharacterDataBundle(bundleCategory);
        AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
        this.m_activeCharacterDataBundleCategory = bundleCategory;
        while (!bundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) bundleLoadRequest.error != 0)
        {
          Log.Error(string.Format("Failed to load asset bundle named '{0}' for character skin {1} ({2}): {3}", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) bundleLoadRequest.error), 169, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
        }
        else
        {
          AssetLoadRequest<AnimatedCharacterData> animatedCharacterDataLoadRequest = characterSkinDefinition.GetAnimatedCharacterDataReference(gender).LoadFromAssetBundleAsync<AnimatedCharacterData>(bundleName);
          while (!animatedCharacterDataLoadRequest.isDone)
            yield return (object) null;
          if ((int) animatedCharacterDataLoadRequest.error != 0)
          {
            AssetManager.UnloadAssetBundle(bundleName);
            this.m_activeCharacterDataBundleCategory = BundleCategory.None;
            Log.Error(string.Format("Failed to load {0} asset from bundle '{1}' for character skin {2} ({3}): {4}", (object) "AnimatedCharacterData", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) animatedCharacterDataLoadRequest.error), 186, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
          }
          else
          {
            AnimatedCharacterData asset = animatedCharacterDataLoadRequest.asset;
            this.SetAnimatedCharacterData(asset);
            this.m_animatedCharacterDataBundleCategory = this.m_activeCharacterDataBundleCategory;
            this.m_activeCharacterDataBundleCategory = BundleCategory.None;
            yield return (object) asset.LoadTimelineResources();
            yield return (object) this.SetAnimatorDefinition();
          }
        }
      }
    }

    public IEnumerator ChangeAnimatedCharacterData(int skinId, Gender gender)
    {
      CharacterSkinDefinition characterSkinDefinition;
      if (!RuntimeData.characterSkinDefinitions.TryGetValue(skinId, out characterSkinDefinition))
      {
        Log.Error(string.Format("Could not find character skin definition with id {0}.", (object) skinId), 211, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
      }
      else
      {
        BundleCategory bundleCategory = characterSkinDefinition.bundleCategory;
        string bundleName = AssetBundlesUtility.GetAnimatedCharacterDataBundle(bundleCategory);
        AssetBundleLoadRequest bundleLoadRequest = AssetManager.LoadAssetBundle(bundleName);
        this.m_activeCharacterDataBundleCategory = bundleCategory;
        while (!bundleLoadRequest.isDone)
          yield return (object) null;
        if ((int) bundleLoadRequest.error != 0)
        {
          Log.Error(string.Format("Failed to load asset bundle named '{0}' for character skin {1} ({2}): {3}", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) bundleLoadRequest.error), 230, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
        }
        else
        {
          AssetLoadRequest<AnimatedCharacterData> animatedCharacterDataLoadRequest = characterSkinDefinition.GetAnimatedCharacterDataReference(gender).LoadFromAssetBundleAsync<AnimatedCharacterData>(bundleName);
          while (!animatedCharacterDataLoadRequest.isDone)
            yield return (object) null;
          if ((int) animatedCharacterDataLoadRequest.error != 0)
          {
            AssetManager.UnloadAssetBundle(bundleName);
            this.m_activeCharacterDataBundleCategory = BundleCategory.None;
            Log.Error(string.Format("Failed to load requested {0} asset from bundle '{1}' for character skin {2} ({3}): {4}", (object) "AnimatedCharacterData", (object) bundleName, (object) characterSkinDefinition.displayName, (object) characterSkinDefinition.id, (object) animatedCharacterDataLoadRequest.error), 247, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
          }
          else
          {
            AnimatedCharacterData newAnimatedCharacterData = animatedCharacterDataLoadRequest.asset;
            yield return (object) newAnimatedCharacterData.LoadTimelineResources();
            AnimatedCharacterData animatedCharacterData = this.GetAnimatedCharacterData();
            if ((Object) null != (Object) animatedCharacterData)
            {
              string characterDataBundle = AssetBundlesUtility.GetAnimatedCharacterDataBundle(this.m_animatedCharacterDataBundleCategory);
              animatedCharacterData.UnloadTimelineResources();
              AssetManager.UnloadAssetBundle(characterDataBundle);
              this.m_animatedCharacterDataBundleCategory = BundleCategory.None;
            }
            this.SetAnimatedCharacterData(newAnimatedCharacterData);
            this.m_animatedCharacterDataBundleCategory = this.m_activeCharacterDataBundleCategory;
            this.m_activeCharacterDataBundleCategory = BundleCategory.None;
            IAnimator2D animator = this.GetAnimator();
            int animationFrame = animator.currentFrame;
            yield return (object) this.SetAnimatorDefinition();
            animator.currentFrame = animationFrame;
          }
        }
      }
    }

    public Color GetColorModifier() => this.m_colorModifier;

    public virtual void SetColorModifier(Color value)
    {
      this.m_colorModifier = value;
      foreach (VisualEffect visualEffect in this.m_propertyEffects.Values)
        visualEffect.SetColorModifier(value);
      if (!((Object) null != (Object) this.m_currentFloatingCounterFeedback))
        return;
      this.m_currentFloatingCounterFeedback.SetColorModifier(value);
    }

    public virtual Ankama.Cube.Data.Direction direction
    {
      get => this.m_direction;
      set => this.m_direction = value;
    }

    public IEnumerator AddPropertyEffect(AttachableEffect attachableEffect, PropertyId propertyId)
    {
      CharacterObject contextProvider = this;
      VisualEffect visualEffect = attachableEffect.InstantiateMainEffect(contextProvider.m_attachableEffectsContainer, (ITimelineContextProvider) contextProvider);
      if ((Object) null != (Object) visualEffect)
      {
        contextProvider.m_propertyEffects.Add(propertyId, visualEffect);
        visualEffect.Play();
      }
      yield return (object) new WaitForTime(attachableEffect.mainEffectDelay);
    }

    public IEnumerator RemovePropertyEffect(
      AttachableEffect attachableEffect,
      PropertyId propertyId)
    {
      CharacterObject contextProvider = this;
      VisualEffect visualEffect1;
      if (contextProvider.m_propertyEffects.TryGetValue(propertyId, out visualEffect1))
      {
        if ((Object) null != (Object) visualEffect1)
          visualEffect1.Stop();
        contextProvider.m_propertyEffects.Remove(propertyId);
      }
      VisualEffect visualEffect2 = attachableEffect.InstantiateStopEffect(contextProvider.m_attachableEffectsContainer, (ITimelineContextProvider) contextProvider);
      if ((Object) null != (Object) visualEffect2)
        visualEffect2.Play();
      yield return (object) new WaitForTime(attachableEffect.stopEffectDelay);
    }

    protected virtual void ClearAttachedEffects()
    {
      foreach (VisualEffect visualEffect in this.m_propertyEffects.Values)
        visualEffect.Stop();
      this.m_propertyEffects.Clear();
    }

    protected virtual void DestroyAttachedEffects()
    {
      foreach (Object @object in this.m_propertyEffects.Values)
        Object.Destroy(@object);
      this.m_propertyEffects.Clear();
    }

    public IEnumerator Spawn()
    {
      yield return (object) this.PlaySpawnAnimation();
      yield return (object) this.PlaySpawnEffect();
      this.CheckParentCellIndicator();
    }

    public IEnumerator Die()
    {
      yield return (object) this.PlayDeathAnimation();
      this.ClearAttachedEffects();
      yield return (object) this.PlayDeathEffect();
    }

    public IEnumerator PlaySpawnEffect()
    {
      CharacterObject contextProvider = this;
      CharacterEffect spawnEffect = contextProvider.GetAnimatedCharacterData().spawnEffect;
      if (!((Object) null == (Object) spawnEffect))
      {
        Component instance = spawnEffect.Instantiate(contextProvider.m_cellObject.transform, (ITimelineContextProvider) contextProvider);
        if ((Object) null != (Object) instance)
          contextProvider.StartCoroutine(spawnEffect.DestroyWhenFinished(instance));
        contextProvider.m_spawnEffectInstance = instance;
        do
        {
          yield return (object) null;
        }
        while ((Object) null != (Object) contextProvider.m_spawnEffectInstance && contextProvider.m_spawnEffectInstance.gameObject.activeSelf);
      }
    }

    public IEnumerator PlayDeathEffect()
    {
      CharacterObject contextProvider = this;
      CharacterEffect deathEffect = contextProvider.GetAnimatedCharacterData().deathEffect;
      if (!((Object) null == (Object) deathEffect))
      {
        Component instance = deathEffect.Instantiate(contextProvider.m_cellObject.transform, (ITimelineContextProvider) contextProvider);
        if ((Object) null != (Object) instance)
          contextProvider.StartCoroutine(deathEffect.DestroyWhenFinished(instance));
        contextProvider.m_deathEffectInstance = instance;
        do
        {
          yield return (object) null;
        }
        while ((Object) null != (Object) contextProvider.m_deathEffectInstance && contextProvider.m_deathEffectInstance.gameObject.activeSelf);
      }
    }

    public abstract void CheckParentCellIndicator();

    public abstract void ShowSpellTargetFeedback(bool isSelected);

    public abstract void HideSpellTargetFeedback();

    public void SetPosition(IMap map, Vector2 position)
    {
      Vector2Int vector2Int = position.RoundToInt();
      this.SetCellObject(map.GetCellObject(vector2Int.x, vector2Int.y));
      this.SetCellObjectInnerPosition(position - (Vector2) vector2Int);
    }

    public abstract void ChangeDirection(Ankama.Cube.Data.Direction newDirection);

    protected abstract void PlayIdleAnimation();

    protected abstract IEnumerator PlaySpawnAnimation();

    protected abstract IEnumerator PlayDeathAnimation();

    public IEnumerator Pull(Vector2Int[] movementCells) => this.SlideRoutine(movementCells, true);

    public IEnumerator Push(Vector2Int[] movementCells) => this.SlideRoutine(movementCells, false);

    public virtual void Teleport(Vector2Int target)
    {
      if (!((Object) null != (Object) this.m_cellObject))
        return;
      IMap parentMap = this.m_cellObject.parentMap;
      if (parentMap == null)
        return;
      this.SetCellObject(parentMap.GetCellObject(target.x, target.y));
      this.SetCellObjectInnerPosition(Vector2.zero);
      this.PlayIdleAnimation();
    }

    protected IEnumerator SlideRoutine(Vector2Int[] movementCells, bool followDirection)
    {
      CharacterObject characterObject = this;
      int movementCellsCount = movementCells.Length;
      if (movementCellsCount != 0)
      {
        // ISSUE: explicit non-virtual call
        CellObject cellObject1 = __nonvirtual (characterObject.cellObject);
        IMap parentMap = cellObject1.parentMap;
        Vector2Int movementCell1 = movementCells[0];
        if (cellObject1.coords != movementCell1)
        {
          Log.Warning(string.Format("Was not on the start cell of a new movement sequence: {0} instead of {1} ({2}).", (object) cellObject1.coords, (object) movementCell1, (object) characterObject.gameObject.name), 528, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
          CellObject cellObject2 = parentMap.GetCellObject(movementCell1.x, movementCell1.y);
          characterObject.SetCellObject(cellObject2);
        }
        if (movementCellsCount > 1)
        {
          Ankama.Cube.Data.Direction newDirection = followDirection ? movementCell1.GetDirectionTo(movementCells[1]) : movementCells[1].GetDirectionTo(movementCell1);
          if (!newDirection.IsAxisAligned())
            newDirection = newDirection.GetAxisAligned(characterObject.direction);
          characterObject.ChangeDirection(newDirection);
        }
        Vector2Int vector2Int = movementCell1;
        float cellTraversalDuration = 2f / (float) characterObject.GetAnimator().frameRate;
        for (int i = 1; i < movementCellsCount; ++i)
        {
          Vector2Int cellCoords = movementCells[i];
          CellObject movementCell = parentMap.GetCellObject(cellCoords.x, cellCoords.y);
          bool goingUp = (double) movementCell.transform.position.y >= (double) cellObject1.transform.position.y;
          Vector2 innerPositionStart;
          Vector2 innerPositionEnd;
          if (goingUp)
          {
            characterObject.SetCellObject(movementCell);
            innerPositionStart = (Vector2) (vector2Int - cellCoords);
            innerPositionEnd = Vector2.zero;
          }
          else
          {
            innerPositionStart = Vector2.zero;
            innerPositionEnd = (Vector2) (cellCoords - vector2Int);
          }
          float animationTime = 0.0f;
          do
          {
            Vector2 innerPosition = Vector2.Lerp(innerPositionStart, innerPositionEnd, animationTime / cellTraversalDuration);
            // ISSUE: explicit non-virtual call
            __nonvirtual (characterObject.SetCellObjectInnerPosition(innerPosition));
            yield return (object) null;
            animationTime += Time.deltaTime;
          }
          while ((double) animationTime < (double) cellTraversalDuration);
          // ISSUE: explicit non-virtual call
          __nonvirtual (characterObject.SetCellObjectInnerPosition(innerPositionEnd));
          if (!goingUp)
            characterObject.SetCellObject(movementCell);
          vector2Int = cellCoords;
          cellObject1 = movementCell;
          IObjectWithActivation isoObject;
          if (i < movementCellsCount - 1 && movementCell.TryGetIsoObject<IObjectWithActivation>(out isoObject))
            isoObject.PlayDetectionAnimation();
          cellCoords = new Vector2Int();
          movementCell = (CellObject) null;
          innerPositionStart = new Vector2();
          innerPositionEnd = new Vector2();
        }
      }
    }

    public virtual void SetFocus(bool value)
    {
      if ((Object) null == (Object) this || value == this.m_isFocused)
        return;
      if (value)
      {
        if (FightUIRework.tooltipsEnabled)
          this.FocusCharacter();
        TooltipWindowUtility.ShowFightCharacterTooltip((ITooltipDataProvider) this, this.transform.position);
      }
      else
      {
        this.UnFocusCharacter();
        FightUIRework.HideTooltip();
      }
      this.m_isFocused = value;
    }

    protected abstract void FocusCharacter();

    protected abstract void UnFocusCharacter();

    public IEnumerator InitializeFloatingCounterEffect(
      FloatingCounterEffect floatingCounterEffect,
      int value)
    {
      CharacterObject parent = this;
      if ((Object) null != (Object) parent.m_currentFloatingCounterFeedback)
        Log.Warning("InitializeFloatingCounterEffect called on " + parent.GetType().Name + " named " + parent.name + " but a floating counter feedback is already attached.", 662, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CharacterObject.cs");
      FloatingCounterFeedback floatingCounterFeedback = FightSpellEffectFactory.InstantiateFloatingCounterFeedback(parent.m_attachableEffectsContainer);
      if (!((Object) null == (Object) floatingCounterFeedback))
      {
        parent.m_currentFloatingCounterFeedback = floatingCounterFeedback;
        yield return (object) floatingCounterFeedback.Launch((IObjectWithCounterEffects) parent, floatingCounterEffect, value);
      }
    }

    public IEnumerator ChangeFloatingCounterEffect(FloatingCounterEffect floatingCounterEffect)
    {
      if ((Object) this.m_currentFloatingCounterFeedback != (Object) null && (Object) this.m_currentFloatingCounterFeedback.effect != (Object) floatingCounterEffect)
      {
        int count = this.m_currentFloatingCounterFeedback.objectsCount;
        yield return (object) this.RemoveFloatingCounterEffect();
        yield return (object) this.InitializeFloatingCounterEffect(floatingCounterEffect, count);
      }
    }

    public IEnumerator RemoveFloatingCounterEffect()
    {
      if ((Object) null != (Object) this.m_currentFloatingCounterFeedback)
      {
        yield return (object) this.m_currentFloatingCounterFeedback.FadeOut();
        this.ClearFloatingCounterEffect();
      }
    }

    public void ClearFloatingCounterEffect()
    {
      if (!((Object) null != (Object) this.m_currentFloatingCounterFeedback))
        return;
      FightSpellEffectFactory.DestroyFloatingCounterFeedback(this.m_currentFloatingCounterFeedback);
      this.m_currentFloatingCounterFeedback = (FloatingCounterFeedback) null;
    }

    [CanBeNull]
    public FloatingCounterFeedback GetCurrentFloatingCounterFeedback() => this.m_currentFloatingCounterFeedback;

    public Object GetTimelineBinding() => (Object) this;

    public abstract ITimelineContext GetTimelineContext();

    public abstract TooltipDataType tooltipDataType { get; }

    public abstract int GetTitleKey();

    public abstract int GetDescriptionKey();

    public abstract IFightValueProvider GetValueProvider();

    public abstract KeywordReference[] keywordReferences { get; }

    protected abstract IEnumerator SetAnimatorDefinition();

    protected abstract void ClearAnimatorDefinition();

    private void OnAnimationLooped(object sender, AnimationLoopedEventArgs e)
    {
      if (!this.m_hasTimeline)
        return;
      this.m_playableDirector.time = 0.0;
      this.m_playableDirector.Resume();
    }

    protected virtual void OnMapRotationChanged(
      DirectionAngle previousMapRotation,
      DirectionAngle newMapRotation)
    {
      this.transform.rotation *= previousMapRotation.GetRotation() * newMapRotation.GetInverseRotation();
      this.m_mapRotation = newMapRotation;
    }

    [SpecialName]
    GameObject IIsoObject.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IIsoObject.get_transform() => this.transform;
  }
}
