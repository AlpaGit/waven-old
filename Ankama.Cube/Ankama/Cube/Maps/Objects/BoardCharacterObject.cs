// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.BoardCharacterObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Extensions;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.UI;
using Ankama.Cube.SRP;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class BoardCharacterObject : 
    CharacterObject,
    IObjectWithArmoredLife,
    ICharacterObject,
    IMovableIsoObject,
    IIsoObject,
    IObjectTargetableByAction
  {
    [SerializeField]
    protected Animator2D m_animator2D;
    [SerializeField]
    protected CharacterBase m_base;
    [SerializeField]
    protected CharacterUIContainer m_uiContainer;
    [SerializeField]
    protected CharacterAttackableUI m_attackableUI;
    protected DynamicFightValueProvider m_tooltipValueProvider;
    protected CharacterAnimationParameters m_animationParameters;
    protected bool m_forceDisplayUI;

    protected override void OnEnable()
    {
      base.OnEnable();
      this.SetComponentsActive(false);
    }

    protected override void SetAnimatedCharacterData(AnimatedCharacterData data) => this.m_uiContainer.SetCharacterHeight(((AnimatedBoardCharacterData) data).height);

    protected void Initialize<T>(
      FightStatus fightStatus,
      PlayerStatus ownerStatus,
      T characterStatus)
      where T : class, IDynamicValueSource, IEntityWithLevel, IEntityWithTeam
    {
      this.m_isFocused = false;
      this.m_forceDisplayUI = false;
      this.m_tooltipValueProvider = new DynamicFightValueProvider((IDynamicValueSource) characterStatus, characterStatus.level);
      this.SetComponentsActive(true);
      this.m_uiContainer.Setup();
      this.m_base.Setup(ownerStatus.playerType);
      this.m_attackableUI.Setup();
    }

    public override void SetColorModifier(Color value)
    {
      base.SetColorModifier(value);
      this.m_animator2D.color = value;
      this.m_uiContainer.color = value;
      this.m_base.color = value;
      this.m_attackableUI.color = value;
    }

    private void SetComponentsActive(bool value)
    {
      this.m_base.gameObject.SetActive(value);
      this.m_uiContainer.gameObject.SetActive(value);
    }

    protected override IEnumerator SetAnimatorDefinition()
    {
      BoardCharacterObject boardCharacterObject = this;
      Animator2D animator = boardCharacterObject.m_animator2D;
      animator.Initialised += new Animator2DInitialisedEventHandler(boardCharacterObject.OnAnimatorInitialized);
      animator.SetDefinition(((AnimatedBoardCharacterData) boardCharacterObject.GetAnimatedCharacterData()).animatedObjectDefinition);
      while (true)
      {
        switch (animator.GetInitialisationState())
        {
          case Animator2DInitialisationState.Initialising:
            yield return (object) null;
            continue;
          case Animator2DInitialisationState.InitialisationPending:
            if (!animator.isActiveAndEnabled)
              goto label_3;
            else
              goto case Animator2DInitialisationState.Initialising;
          default:
            goto label_1;
        }
      }
label_1:
      yield break;
label_3:;
    }

    private void OnAnimatorInitialized(object sender, Animator2DInitialisedEventArgs e)
    {
      Animator2D animator2D = this.m_animator2D;
      animator2D.Initialised -= new Animator2DInitialisedEventHandler(this.OnAnimatorInitialized);
      animator2D.paused = false;
      this.PlayIdleAnimation();
    }

    protected override void ClearAnimatorDefinition()
    {
      this.m_animator2D.Initialised -= new Animator2DInitialisedEventHandler(this.OnAnimatorInitialized);
      this.m_animator2D.SetDefinition((AnimatedObjectDefinition) null);
    }

    protected void StartFightAnimation(
      CharacterAnimationInfo animationInfo,
      Action onComplete = null,
      Action onCancel = null,
      bool restart = true,
      bool async = false)
    {
      string animationName = animationInfo.animationName;
      string timelineKey = animationInfo.timelineKey;
      this.m_animator2D.transform.localRotation = animationInfo.flipX ? Quaternion.Euler(0.0f, -135f, 0.0f) : Quaternion.Euler(0.0f, 45f, 0.0f);
      this.direction = animationInfo.direction;
      ITimelineAssetProvider animatedCharacterData = (ITimelineAssetProvider) this.GetAnimatedCharacterData();
      if (animatedCharacterData != null)
      {
        TimelineAsset timelineAsset1;
        bool timelineAsset2 = animatedCharacterData.TryGetTimelineAsset(timelineKey, out timelineAsset1);
        if (timelineAsset2 && (UnityEngine.Object) null != (UnityEngine.Object) timelineAsset1)
        {
          if ((UnityEngine.Object) timelineAsset1 != (UnityEngine.Object) this.m_playableDirector.playableAsset)
          {
            this.m_playableDirector.Play((PlayableAsset) timelineAsset1);
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
          if (timelineAsset2)
            Log.Warning("Character named '" + this.GetAnimatedCharacterData().name + "' has a timeline setup for key '" + timelineKey + "' but the actual asset is null.", 171, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BoardCharacterObject.cs");
          this.m_playableDirector.time = 0.0;
          this.m_playableDirector.Pause();
          this.m_hasTimeline = false;
        }
      }
      this.m_animationCallback.Setup(animationName, restart, onComplete, onCancel);
      this.m_animator2D.SetAnimation(animationName, animationInfo.loops, async, restart);
      this.m_animationParameters = animationInfo.parameters;
    }

    protected override void OnDisable()
    {
      this.SetFocus(false);
      base.OnDisable();
    }

    protected override IAnimator2D GetAnimator() => (IAnimator2D) this.m_animator2D;

    protected override void PlayIdleAnimation() => this.StartFightAnimation(new CharacterAnimationInfo((Vector2) this.m_cellObject.coords, "idle", "idle", true, this.m_direction, this.m_mapRotation), restart: false);

    protected override IEnumerator PlaySpawnAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      BoardCharacterObject boardCharacterObject = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      CharacterAnimationInfo animationInfo = new CharacterAnimationInfo((Vector2) boardCharacterObject.m_cellObject.coords, "idle", "spawn", false, boardCharacterObject.direction, boardCharacterObject.m_mapRotation);
      boardCharacterObject.StartFightAnimation(animationInfo, new Action(((CharacterObject) boardCharacterObject).PlayIdleAnimation));
      return false;
    }

    protected override IEnumerator PlayDeathAnimation()
    {
      BoardCharacterObject boardCharacterObject = this;
      CharacterAnimationInfo deathAnimationInfo = new CharacterAnimationInfo((Vector2) boardCharacterObject.m_cellObject.coords, "hit", "death", false, boardCharacterObject.direction, boardCharacterObject.m_mapRotation);
      boardCharacterObject.StartFightAnimation(deathAnimationInfo, restart: false);
      Animator2D animator = boardCharacterObject.m_animator2D;
      if (animator.CurrentAnimationHasLabel("die", out int _))
      {
        while (!CharacterObjectUtility.HasAnimationReachedLabel(animator, deathAnimationInfo, "die"))
          yield return (object) null;
        animator.paused = true;
      }
      else
        Log.Warning(animator.GetDefinition().name + " is missing the 'die' label in the animation named '" + deathAnimationInfo.animationName + "'.", 244, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BoardCharacterObject.cs");
    }

    public override void CheckParentCellIndicator()
    {
      CellObject cellObject = this.m_cellObject;
      if ((UnityEngine.Object) null == (UnityEngine.Object) cellObject)
      {
        this.m_uiContainer.SetCellIndicator(MapCellIndicator.None);
      }
      else
      {
        IMap parentMap = cellObject.parentMap;
        if (parentMap == null)
        {
          this.m_uiContainer.SetCellIndicator(MapCellIndicator.None);
        }
        else
        {
          Vector2Int coords = cellObject.coords;
          this.m_uiContainer.SetCellIndicator(parentMap.GetCellIndicator(coords.x, coords.y));
        }
      }
    }

    public override void ShowSpellTargetFeedback(bool isSelected)
    {
      this.m_forceDisplayUI = true;
      this.gameObject.SetLayerRecursively(LayerMaskNames.characterFocusLayer);
      this.m_base.SetTargetState(isSelected ? CharacterBase.TargetState.Targeted : CharacterBase.TargetState.Targetable);
    }

    public override void HideSpellTargetFeedback()
    {
      this.m_forceDisplayUI = false;
      this.gameObject.SetLayerRecursively(LayerMaskNames.defaultLayer);
      this.m_base.SetTargetState(CharacterBase.TargetState.None);
    }

    public override void ChangeDirection(Ankama.Cube.Data.Direction newDirection)
    {
      if (newDirection == this.m_direction)
        return;
      Vector2 coords = (Vector2) this.m_cellObject.coords;
      Animator2D animator2D = this.m_animator2D;
      CharacterAnimationParameters animationParameters = this.m_animationParameters;
      CharacterAnimationInfo characterAnimationInfo;
      if (this.m_animationParameters.secondDirection == Ankama.Cube.Data.Direction.None)
      {
        characterAnimationInfo = new CharacterAnimationInfo(coords, animationParameters.animationName, animationParameters.timelineKey, animationParameters.loops, newDirection, this.m_mapRotation);
      }
      else
      {
        DirectionAngle angle = this.m_direction.DirectionAngleTo(newDirection);
        Ankama.Cube.Data.Direction previousDirection = animationParameters.firstDirection.Rotate(angle);
        characterAnimationInfo = new CharacterAnimationInfo(coords, animationParameters.animationName, animationParameters.timelineKey, animationParameters.loops, previousDirection, newDirection, this.m_mapRotation);
      }
      animator2D.transform.localRotation = characterAnimationInfo.flipX ? Quaternion.Euler(0.0f, -135f, 0.0f) : Quaternion.Euler(0.0f, 45f, 0.0f);
      this.direction = newDirection;
      string animationName = characterAnimationInfo.animationName;
      int currentFrame = animator2D.currentFrame;
      this.m_animationCallback.ChangeAnimationName(animationName);
      animator2D.SetAnimation(animationName, characterAnimationInfo.loops, false, true);
      animator2D.currentFrame = currentFrame;
      this.m_animationParameters = characterAnimationInfo.parameters;
    }

    public override void Teleport(Vector2Int target)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_cellObject))
        return;
      IMap parentMap = this.m_cellObject.parentMap;
      if (parentMap == null)
        return;
      Ankama.Cube.Data.Direction direction = this.m_cellObject.coords.GetDirectionTo(target);
      if (!direction.IsAxisAligned())
        direction = direction.GetAxisAligned(this.direction);
      this.direction = direction;
      this.SetCellObject(parentMap.GetCellObject(target.x, target.y));
      this.SetCellObjectInnerPosition(Vector2.zero);
      this.PlayIdleAnimation();
    }

    public virtual void ShowActionTargetFeedback(ActionType sourceActionType, bool isSelected)
    {
      this.m_forceDisplayUI = true;
      this.m_base.SetTargetState(isSelected ? CharacterBase.TargetState.Targeted : CharacterBase.TargetState.Targetable);
      this.m_attackableUI.SetValue(sourceActionType, isSelected);
    }

    public virtual void HideActionTargetFeedback()
    {
      this.m_forceDisplayUI = false;
      this.m_base.SetTargetState(CharacterBase.TargetState.None);
      this.m_attackableUI.SetValue(ActionType.None, false);
    }

    public int life { get; protected set; }

    public int armor { get; protected set; }

    public virtual void SetArmoredLife(int lifeValue, int armorValue)
    {
      this.life = lifeValue;
      this.armor = armorValue;
    }

    public int baseLife { get; protected set; }

    public virtual void SetBaseLife(int lifeValue) => this.baseLife = lifeValue;

    public IEnumerator PlayHitAnimation()
    {
      BoardCharacterObject boardCharacterObject = this;
      CharacterAnimationInfo hitAnimationInfo = new CharacterAnimationInfo((Vector2) boardCharacterObject.m_cellObject.coords, "hit", "hit", false, boardCharacterObject.direction, boardCharacterObject.m_mapRotation);
      boardCharacterObject.StartFightAnimation(hitAnimationInfo, new Action(((CharacterObject) boardCharacterObject).PlayIdleAnimation));
      Animator2D animator = boardCharacterObject.m_animator2D;
      while (!CharacterObjectUtility.HasAnimationReachedLabel(animator, hitAnimationInfo, "die"))
        yield return (object) null;
    }

    public IEnumerator PlayLethalHitAnimation()
    {
      BoardCharacterObject boardCharacterObject = this;
      Animator2D animator = boardCharacterObject.m_animator2D;
      string animationNameBackup = animator.animationName;
      yield return (object) null;
      if (string.Equals(animator.animationName, animationNameBackup))
      {
        CharacterAnimationInfo animationInfo = new CharacterAnimationInfo((Vector2) boardCharacterObject.m_cellObject.coords, "hit", "hit", false, boardCharacterObject.direction, boardCharacterObject.m_mapRotation);
        boardCharacterObject.StartFightAnimation(animationInfo, new Action(((CharacterObject) boardCharacterObject).PlayIdleAnimation));
      }
    }

    protected override void FocusCharacter() => this.m_attackableUI.sortingOrder = 1;

    protected override void UnFocusCharacter() => this.m_attackableUI.sortingOrder = 0;

    public override IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_tooltipValueProvider;

    protected override void OnMapRotationChanged(
      DirectionAngle previousMapRotation,
      DirectionAngle newMapRotation)
    {
      base.OnMapRotationChanged(previousMapRotation, newMapRotation);
      Animator2D animator2D = this.m_animator2D;
      if (animator2D.GetInitialisationState() != Animator2DInitialisationState.Initialised)
        return;
      Vector2 coords = (Vector2) this.m_cellObject.coords;
      CharacterAnimationParameters animationParameters = this.m_animationParameters;
      CharacterAnimationInfo characterAnimationInfo;
      if (this.m_animationParameters.secondDirection == Ankama.Cube.Data.Direction.None)
      {
        characterAnimationInfo = new CharacterAnimationInfo(coords, animationParameters.animationName, animationParameters.timelineKey, animationParameters.loops, this.direction, newMapRotation);
      }
      else
      {
        DirectionAngle angle = newMapRotation.Substract(previousMapRotation);
        Ankama.Cube.Data.Direction previousDirection = animationParameters.firstDirection.Rotate(angle);
        characterAnimationInfo = new CharacterAnimationInfo(coords, animationParameters.animationName, animationParameters.timelineKey, animationParameters.loops, previousDirection, this.direction, newMapRotation);
      }
      animator2D.transform.localRotation = characterAnimationInfo.flipX ? Quaternion.Euler(0.0f, -135f, 0.0f) : Quaternion.Euler(0.0f, 45f, 0.0f);
      string animationName = characterAnimationInfo.animationName;
      int currentFrame = animator2D.currentFrame;
      this.m_animationCallback.ChangeAnimationName(animationName);
      animator2D.SetAnimation(animationName, characterAnimationInfo.loops, false, true);
      animator2D.currentFrame = currentFrame;
      this.m_animationParameters = characterAnimationInfo.parameters;
    }

    [SpecialName]
    GameObject IIsoObject.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IIsoObject.get_transform() => this.transform;
  }
}
