// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.FightCharacterObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.UI;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class FightCharacterObject : 
    BoardCharacterObject,
    IObjectWithMovement,
    ICharacterObject,
    IMovableIsoObject,
    IIsoObject,
    IObjectWithAction,
    IObjectWithElementaryState,
    ICharacterTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private CharacterArmoredLifeUI m_lifeUI;
    [SerializeField]
    protected CharacterActionUI m_actionUI;
    [SerializeField]
    private CharacterElementaryStateUI m_elementaryStateUI;
    protected AnimatedFightCharacterData m_characterData;
    private const float MovementSingleCellTraversalTime = 5f;
    private const float MovementMultipleCellTraversalTime = 4f;
    private FightCharacterObjectContext m_context;

    protected override AnimatedCharacterData GetAnimatedCharacterData() => (AnimatedCharacterData) this.m_characterData;

    protected override void SetAnimatedCharacterData(AnimatedCharacterData data)
    {
      base.SetAnimatedCharacterData(data);
      AnimatedFightCharacterData fightCharacterData = data as AnimatedFightCharacterData;
      if ((UnityEngine.Object) null == (UnityEngine.Object) fightCharacterData)
        Log.Error("Data type mismatch: an instance of " + this.GetType().Name + " cannot be created with a data asset of type '" + data.GetType().Name + "'.", 47, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FightCharacterObject.cs");
      else
        this.m_characterData = fightCharacterData;
    }

    protected override void ClearAnimatedCharacterData() => this.m_characterData = (AnimatedFightCharacterData) null;

    public void Initialize(
      FightStatus fightStatus,
      PlayerStatus ownerStatus,
      CharacterStatus characterStatus)
    {
      this.Initialize<CharacterStatus>(fightStatus, ownerStatus, characterStatus);
      this.life = characterStatus.life;
      this.actionValue = characterStatus.actionValue;
      this.armor = characterStatus.armor;
      this.baseLife = characterStatus.baseLife;
      this.actionType = characterStatus.actionType;
      this.hasRange = characterStatus.hasRange;
      this.movementPoints = characterStatus.movementPoints;
      this.baseMovementPoints = characterStatus.movementPoints;
      this.physicalDamageBoost = characterStatus.physicalDamageBoost;
      this.physicalHealBoost = characterStatus.physicalHealBoost;
      this.m_context.SetParameterValue("life", (float) this.life);
      TimelineContextUtility.SetFightContext(this.m_playableDirector, fightStatus.context);
      this.m_base.InitializeState(fightStatus, characterStatus, ownerStatus);
      this.m_lifeUI.enabled = false;
      this.m_lifeUI.Setup(characterStatus.life);
      this.m_lifeUI.SetValues(characterStatus.life, characterStatus.armor);
      this.m_actionUI.enabled = false;
      this.m_actionUI.Setup(characterStatus.actionType, characterStatus.hasRange);
      if (this.actionValue.HasValue)
        this.m_actionUI.SetValue(this.actionValue.Value);
      this.m_elementaryStateUI.Setup();
    }

    public override void SetColorModifier(Color value)
    {
      base.SetColorModifier(value);
      this.m_lifeUI.color = value;
      this.m_actionUI.color = value;
      this.m_elementaryStateUI.color = value;
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.m_context = new FightCharacterObjectContext(this);
      this.m_context.Initialize();
    }

    protected override void OnDisable()
    {
      base.OnDisable();
      this.m_context.Release();
    }

    public override Ankama.Cube.Data.Direction direction
    {
      get => this.m_direction;
      set
      {
        if (value == this.m_direction)
          return;
        if (this.m_context != null)
          this.m_context.UpdateDirection(this.m_direction, value);
        this.m_direction = value;
      }
    }

    protected override void ClearAttachedEffects()
    {
      base.ClearAttachedEffects();
      this.ClearFloatingCounterEffect();
    }

    protected override void DestroyAttachedEffects()
    {
      base.DestroyAttachedEffects();
      this.ClearFloatingCounterEffect();
    }

    public override void SetArmoredLife(int lifeValue, int armorValue)
    {
      base.SetArmoredLife(lifeValue, armorValue);
      this.m_lifeUI.ChangeValues(lifeValue, armorValue);
      this.m_context.SetParameterValue("life", (float) this.life);
    }

    public override void SetBaseLife(int lifeValue)
    {
      base.SetBaseLife(lifeValue);
      this.m_lifeUI.SetMaximumLife(lifeValue);
    }

    public int movementPoints { get; protected set; }

    public int baseMovementPoints { get; protected set; }

    public void SetMovementPoints(int value) => this.movementPoints = value;

    public IEnumerator Move(Vector2Int[] movementCells)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      FightCharacterObject fightCharacterObject = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        fightCharacterObject.PlayIdleAnimation();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) fightCharacterObject.MoveToRoutine(movementCells);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public IEnumerator MoveToAction(
      Vector2Int[] movementCells,
      Ankama.Cube.Data.Direction actionDirection,
      bool hasFollowUpAnimation = true)
    {
      FightCharacterObject fightCharacterObject = this;
      yield return fightCharacterObject.m_characterData.hasDashAnimations ? (object) fightCharacterObject.MoveToDoActionRoutine(movementCells, actionDirection) : (object) fightCharacterObject.MoveToRoutine(movementCells);
      if (!hasFollowUpAnimation)
        fightCharacterObject.PlayIdleAnimation();
    }

    protected IEnumerator MoveToRoutine(Vector2Int[] movementCells)
    {
      FightCharacterObject fightCharacterObject = this;
      int movementCellsCount = movementCells.Length;
      if (movementCellsCount != 0)
      {
        CellObject cellObj = fightCharacterObject.m_cellObject;
        IMap parentMap = cellObj.parentMap;
        Animator2D animator = fightCharacterObject.m_animator2D;
        AnimatedFightCharacterData.IdleToRunTransitionMode idleToRunTransitionMode = fightCharacterObject.m_characterData.idleToRunTransitionMode;
        Vector2Int startCell = movementCells[0];
        Vector2Int endCell = movementCells[movementCellsCount - 1];
        if (cellObj.coords != startCell)
        {
          Log.Warning(string.Format("Was not on the start cell of a new movement sequence: {0} instead of {1} ({2}).", (object) cellObj.coords, (object) startCell, (object) fightCharacterObject.gameObject.name), 232, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FightCharacterObject.cs");
          CellObject cellObject = parentMap.GetCellObject(startCell.x, startCell.y);
          fightCharacterObject.SetCellObject(cellObject);
        }
        CharacterAnimationInfo transitionAnimationInfo;
        if (idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.IdleToRun))
        {
          transitionAnimationInfo = new CharacterAnimationInfo((Vector2) startCell, "idle_run", "idle-to-run", false, movementCellsCount >= 2 ? startCell.GetDirectionTo(movementCells[1]) : fightCharacterObject.direction, fightCharacterObject.m_mapRotation);
          fightCharacterObject.StartFightAnimation(transitionAnimationInfo);
          while (!CharacterObjectUtility.HasAnimationEnded(animator, transitionAnimationInfo))
            yield return (object) null;
          transitionAnimationInfo = new CharacterAnimationInfo();
        }
        Vector2Int position = startCell;
        float cellTraversalDuration = (movementCellsCount <= 2 ? 5f : 4f) / (float) animator.frameRate;
        foreach (CharacterAnimationInfo animationInfo in CharacterFightMovementSequencer.ComputeMovement(movementCells, fightCharacterObject.m_mapRotation))
        {
          Vector2Int cellCoords = animationInfo.position.RoundToInt();
          CellObject movementCell = parentMap.GetCellObject(cellCoords.x, cellCoords.y);
          bool goingUp = (double) movementCell.transform.position.y >= (double) cellObj.transform.position.y;
          Vector2 innerPositionStart;
          Vector2 innerPositionEnd;
          if (goingUp)
          {
            fightCharacterObject.SetCellObject(movementCell);
            innerPositionStart = (Vector2) (position - cellCoords);
            innerPositionEnd = Vector2.zero;
          }
          else
          {
            innerPositionStart = Vector2.zero;
            innerPositionEnd = (Vector2) (cellCoords - position);
          }
          fightCharacterObject.StartFightAnimation(animationInfo, restart: false);
          float animationTime = 0.0f;
          do
          {
            Vector2 innerPosition = Vector2.Lerp(innerPositionStart, innerPositionEnd, animationTime / cellTraversalDuration);
            // ISSUE: explicit non-virtual call
            __nonvirtual (fightCharacterObject.SetCellObjectInnerPosition(innerPosition));
            yield return (object) null;
            animationTime += Time.deltaTime;
          }
          while ((double) animationTime < (double) cellTraversalDuration);
          // ISSUE: explicit non-virtual call
          __nonvirtual (fightCharacterObject.SetCellObjectInnerPosition(innerPositionEnd));
          if (!goingUp)
            fightCharacterObject.SetCellObject(movementCell);
          position = cellCoords;
          cellObj = movementCell;
          IObjectWithActivation isoObject;
          if (cellCoords != endCell && movementCell.TryGetIsoObject<IObjectWithActivation>(out isoObject))
            isoObject.PlayDetectionAnimation();
          cellCoords = new Vector2Int();
          movementCell = (CellObject) null;
          innerPositionStart = new Vector2();
          innerPositionEnd = new Vector2();
        }
        if (idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.RunToIdle))
        {
          transitionAnimationInfo = new CharacterAnimationInfo((Vector2) position, "run_idle", "run-to-idle", false, fightCharacterObject.direction, fightCharacterObject.m_mapRotation);
          fightCharacterObject.StartFightAnimation(transitionAnimationInfo);
          while (!CharacterObjectUtility.HasAnimationEnded(animator, transitionAnimationInfo))
            yield return (object) null;
          transitionAnimationInfo = new CharacterAnimationInfo();
        }
      }
    }

    protected IEnumerator MoveToDoActionRoutine(
      Vector2Int[] movementCells,
      Ankama.Cube.Data.Direction actionDirection)
    {
      FightCharacterObject fightCharacterObject = this;
      int movementCellsCount = movementCells.Length;
      if (movementCellsCount != 0)
      {
        CellObject cellObject1 = fightCharacterObject.m_cellObject;
        IMap parentMap = cellObject1.parentMap;
        Vector2Int movementCell1 = movementCells[0];
        if (cellObject1.coords != movementCell1)
        {
          Log.Warning(string.Format("Was not on the start cell of a new movement sequence: {0} instead of {1} ({2}).", (object) cellObject1.coords, (object) movementCell1, (object) fightCharacterObject.gameObject.name), 341, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FightCharacterObject.cs");
          CellObject cellObject2 = parentMap.GetCellObject(movementCell1.x, movementCell1.y);
          fightCharacterObject.SetCellObject(cellObject2);
        }
        Animator2D animator = fightCharacterObject.m_animator2D;
        foreach (CharacterAnimationInfo characterAnimationInfo in CharacterFightMovementSequencer.ComputeMovementToAction(movementCells, actionDirection, fightCharacterObject.m_mapRotation))
        {
          CharacterAnimationInfo sequenceItem = characterAnimationInfo;
          // ISSUE: explicit non-virtual call
          __nonvirtual (fightCharacterObject.SetPosition(parentMap, sequenceItem.position));
          fightCharacterObject.StartFightAnimation(sequenceItem);
          while (!CharacterObjectUtility.HasAnimationEnded(animator, sequenceItem))
            yield return (object) null;
          sequenceItem = new CharacterAnimationInfo();
        }
        Vector2Int movementCell2 = movementCells[movementCellsCount - 1];
        // ISSUE: explicit non-virtual call
        __nonvirtual (fightCharacterObject.SetPosition(parentMap, (Vector2) movementCell2));
        if (fightCharacterObject.m_cellObject.TryGetIsoObject<IObjectWithActivation>(out IObjectWithActivation _))
        {
          fightCharacterObject.direction = actionDirection;
          fightCharacterObject.PlayIdleAnimation();
        }
      }
    }

    public override void ShowSpellTargetFeedback(bool isSelected)
    {
      base.ShowSpellTargetFeedback(isSelected);
      this.m_lifeUI.enabled = true;
      this.m_actionUI.enabled = true;
    }

    public override void HideSpellTargetFeedback()
    {
      base.HideSpellTargetFeedback();
      bool isFocused = this.m_isFocused;
      this.m_lifeUI.enabled = isFocused;
      this.m_actionUI.enabled = isFocused;
    }

    public override void ShowActionTargetFeedback(ActionType sourceActionType, bool isSelected)
    {
      base.ShowActionTargetFeedback(sourceActionType, isSelected);
      this.m_lifeUI.enabled = true;
      this.m_actionUI.enabled = true;
    }

    public override void HideActionTargetFeedback()
    {
      base.HideActionTargetFeedback();
      bool isFocused = this.m_isFocused;
      this.m_lifeUI.enabled = isFocused;
      this.m_actionUI.enabled = isFocused;
    }

    public int? actionValue { get; protected set; }

    public ActionType actionType { get; protected set; }

    public int physicalDamageBoost { get; protected set; }

    public int physicalHealBoost { get; protected set; }

    public bool hasRange { get; protected set; }

    public void SetPhysicalDamageBoost(int value)
    {
      this.physicalDamageBoost = value;
      if (!this.actionValue.HasValue)
        return;
      this.m_actionUI.ChangeValue(this.actionValue.Value + value);
    }

    public void SetPhysicalHealBoost(int value)
    {
      this.physicalHealBoost = value;
      if (!this.actionValue.HasValue)
        return;
      this.m_actionUI.ChangeValue(this.actionValue.Value + value);
    }

    public void SetActionUsed(bool actionUsed, bool turnEnded)
    {
      if (turnEnded)
        this.m_base.SetState(CharacterBase.State.NotPlayable);
      else
        this.m_base.SetState(actionUsed ? CharacterBase.State.ActionUsed : CharacterBase.State.ActionAvailable);
    }

    public IEnumerator PlayActionAnimation(
      Ankama.Cube.Data.Direction directionToAttack,
      bool waitForAnimationEndOnMissingLabel)
    {
      FightCharacterObject fightCharacterObject = this;
      CharacterAnimationInfo attackAnimationInfo = new CharacterAnimationInfo((Vector2) fightCharacterObject.m_cellObject.coords, "attack", "attack", false, directionToAttack, fightCharacterObject.m_mapRotation);
      fightCharacterObject.StartFightAnimation(attackAnimationInfo, new Action(((CharacterObject) fightCharacterObject).PlayIdleAnimation));
      Animator2D animator = fightCharacterObject.m_animator2D;
      if (animator.CurrentAnimationHasLabel("shot", out int _))
      {
        while (!CharacterObjectUtility.HasAnimationReachedLabel(animator, attackAnimationInfo, "shot"))
          yield return (object) null;
      }
      else
      {
        Log.Warning(animator.GetDefinition().name + " is missing the 'shot' label in the animation named '" + attackAnimationInfo.animationName + "'.", 475, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FightCharacterObject.cs");
        if (waitForAnimationEndOnMissingLabel)
        {
          while (!CharacterObjectUtility.HasAnimationEnded(animator, attackAnimationInfo))
            yield return (object) null;
        }
      }
    }

    public IEnumerator PlayRangedActionAnimation(Ankama.Cube.Data.Direction directionToAttack)
    {
      FightCharacterObject fightCharacterObject = this;
      if (!fightCharacterObject.m_characterData.hasRangedAttackAnimations)
      {
        // ISSUE: explicit non-virtual call
        yield return (object) __nonvirtual (fightCharacterObject.PlayActionAnimation(directionToAttack, true));
      }
      else
      {
        CharacterAnimationInfo rangedAttackAnimationInfo = new CharacterAnimationInfo((Vector2) fightCharacterObject.m_cellObject.coords, "rangedattack", "rangedattack", false, directionToAttack, fightCharacterObject.m_mapRotation);
        fightCharacterObject.StartFightAnimation(rangedAttackAnimationInfo, new Action(((CharacterObject) fightCharacterObject).PlayIdleAnimation));
        Animator2D animator = fightCharacterObject.m_animator2D;
        if (animator.CurrentAnimationHasLabel("shot", out int _))
        {
          while (!CharacterObjectUtility.HasAnimationReachedLabel(animator, rangedAttackAnimationInfo, "shot"))
            yield return (object) null;
        }
        else
          Log.Warning(animator.GetDefinition().name + " is missing the 'shot' label in the animation named '" + rangedAttackAnimationInfo.animationName + "'.", 512, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FightCharacterObject.cs");
      }
    }

    public void TriggerActionEffect(Vector2Int target)
    {
      CharacterEffect actionEffect = this.m_characterData.actionEffect;
      if ((UnityEngine.Object) null == (UnityEngine.Object) actionEffect)
        return;
      CellObject cellObject = this.m_cellObject.parentMap.GetCellObject(target.x, target.y);
      Component instance = actionEffect.Instantiate(cellObject.transform, (ITimelineContextProvider) this);
      if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
        return;
      this.StartCoroutine(actionEffect.DestroyWhenFinished(instance));
    }

    protected override void FocusCharacter()
    {
      base.FocusCharacter();
      this.m_lifeUI.sortingOrder = 1;
      this.m_lifeUI.enabled = true;
      this.m_actionUI.sortingOrder = 1;
      this.m_actionUI.enabled = true;
      this.m_elementaryStateUI.sortingOrder = 1;
    }

    protected override void UnFocusCharacter()
    {
      base.UnFocusCharacter();
      bool forceDisplayUi = this.m_forceDisplayUI;
      this.m_lifeUI.sortingOrder = 0;
      this.m_lifeUI.enabled = forceDisplayUi;
      this.m_actionUI.sortingOrder = 0;
      this.m_actionUI.enabled = forceDisplayUi;
      this.m_elementaryStateUI.sortingOrder = 0;
    }

    public ElementaryStates elementaryState { get; protected set; }

    public void SetElementaryState(ElementaryStates value)
    {
      this.elementaryState = value;
      this.m_elementaryStateUI.ChangeValue(value);
    }

    public override ITimelineContext GetTimelineContext() => (ITimelineContext) this.m_context;

    public override TooltipDataType tooltipDataType { get; }

    public ActionType GetActionType() => this.actionType;

    public TooltipActionIcon GetActionIcon() => TooltipWindowUtility.GetActionIcon(this.actionType, this.hasRange);

    public bool TryGetActionValue(out int value)
    {
      ref int local = ref value;
      int? actionValue = this.actionValue;
      int num = actionValue ?? 0;
      local = num;
      actionValue = this.actionValue;
      return actionValue.HasValue;
    }

    public int GetLifeValue() => this.life;

    public int GetMovementValue() => this.movementPoints;

    [SpecialName]
    GameObject IIsoObject.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IIsoObject.get_transform() => this.transform;
  }
}
