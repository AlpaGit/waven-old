// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.ObjectMechanismObject
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
using Ankama.Utilities;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [SelectionBase]
  public sealed class ObjectMechanismObject : 
    BoardCharacterObject,
    IObjectWithActivationAnimation,
    ICharacterObject,
    IMovableIsoObject,
    IIsoObject,
    IObjectMechanismTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private ObjectMechanismDefinition m_definition;
    [SerializeField]
    private CharacterMechanismLifeUI m_lifeUI;
    private AnimatedObjectMechanismData m_characterData;
    private bool m_alliedWithLocalPlayer;
    private ObjectMechanismObjectContext m_context;

    public override IsoObjectDefinition definition
    {
      get => (IsoObjectDefinition) this.m_definition;
      protected set => this.m_definition = (ObjectMechanismDefinition) value;
    }

    public bool alliedWithLocalPlayer
    {
      get => this.m_alliedWithLocalPlayer;
      set
      {
        this.m_alliedWithLocalPlayer = value;
        this.m_direction = value ? Ankama.Cube.Data.Direction.SouthEast.Rotate(this.m_mapRotation) : Ankama.Cube.Data.Direction.SouthWest.Rotate(this.m_mapRotation);
      }
    }

    protected override AnimatedCharacterData GetAnimatedCharacterData() => (AnimatedCharacterData) this.m_characterData;

    protected override void SetAnimatedCharacterData(AnimatedCharacterData data)
    {
      base.SetAnimatedCharacterData(data);
      AnimatedObjectMechanismData objectMechanismData = data as AnimatedObjectMechanismData;
      if ((UnityEngine.Object) null == (UnityEngine.Object) objectMechanismData)
        Log.Error("Data type mismatch: an instance of " + this.GetType().Name + " cannot be created with a data asset of type '" + data.GetType().Name + "'.", 62, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\ObjectMechanismObject.cs");
      else
        this.m_characterData = objectMechanismData;
    }

    protected override void ClearAnimatedCharacterData() => this.m_characterData = (AnimatedObjectMechanismData) null;

    public void Initialize(
      FightStatus fightStatus,
      PlayerStatus ownerStatus,
      ObjectMechanismStatus objectMechanismStatus)
    {
      this.Initialize<ObjectMechanismStatus>(fightStatus, ownerStatus, objectMechanismStatus);
      this.life = objectMechanismStatus.life;
      this.baseLife = objectMechanismStatus.baseLife;
      TimelineContextUtility.SetFightContext(this.m_playableDirector, fightStatus.context);
      this.m_lifeUI.enabled = false;
      this.m_lifeUI.SetValue(objectMechanismStatus.life);
    }

    public override void SetColorModifier(Color value)
    {
      base.SetColorModifier(value);
      this.m_lifeUI.color = value;
    }

    public override void Destroy() => FightObjectFactory.ReleaseObjectMechanismObject(this);

    protected override void OnEnable()
    {
      base.OnEnable();
      this.m_context = new ObjectMechanismObjectContext(this);
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
      }
    }

    public override void ChangeDirection(Ankama.Cube.Data.Direction newDirection)
    {
    }

    public override void SetArmoredLife(int newLife, int newArmor)
    {
      base.SetArmoredLife(newLife, newArmor);
      this.m_lifeUI.ChangeValue(newLife);
    }

    public override void ShowSpellTargetFeedback(bool isSelected)
    {
      base.ShowSpellTargetFeedback(isSelected);
      this.m_lifeUI.enabled = true;
    }

    public override void HideSpellTargetFeedback()
    {
      base.HideSpellTargetFeedback();
      this.m_lifeUI.enabled = this.m_isFocused;
    }

    public override void ShowActionTargetFeedback(ActionType sourceActionType, bool isSelected)
    {
      base.ShowActionTargetFeedback(sourceActionType, isSelected);
      this.m_lifeUI.enabled = true;
    }

    public override void HideActionTargetFeedback()
    {
      base.HideActionTargetFeedback();
      this.m_lifeUI.enabled = this.m_isFocused;
    }

    protected override void FocusCharacter()
    {
      base.FocusCharacter();
      this.m_lifeUI.sortingOrder = 1;
      this.m_lifeUI.enabled = true;
    }

    protected override void UnFocusCharacter()
    {
      base.UnFocusCharacter();
      this.m_lifeUI.sortingOrder = 0;
      this.m_lifeUI.enabled = this.m_forceDisplayUI;
    }

    public IEnumerator PlayActivationAnimation()
    {
      ObjectMechanismObject objectMechanismObject = this;
      Animator2D animator = objectMechanismObject.m_animator2D;
      if (!objectMechanismObject.m_characterData.hasActivationAnimation)
      {
        Log.Warning(animator.GetDefinition().name + " does not have an activation animation.", 204, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\ObjectMechanismObject.cs");
      }
      else
      {
        CharacterAnimationInfo activationAnimationInfo = new CharacterAnimationInfo((Vector2) objectMechanismObject.m_cellObject.coords, "attack", "attack", false, objectMechanismObject.direction, objectMechanismObject.m_mapRotation);
        objectMechanismObject.StartFightAnimation(activationAnimationInfo, new Action(((CharacterObject) objectMechanismObject).PlayIdleAnimation));
        while (!CharacterObjectUtility.HasAnimationReachedLabel(animator, activationAnimationInfo, "shot"))
          yield return (object) null;
        objectMechanismObject.TriggerActivationEffect();
      }
    }

    public void TriggerActivationEffect()
    {
      CharacterEffect activationEffect = this.m_characterData.activationEffect;
      if ((UnityEngine.Object) null == (UnityEngine.Object) activationEffect)
        return;
      Component instance = activationEffect.Instantiate(this.m_cellObject.transform, (ITimelineContextProvider) this);
      if (!((UnityEngine.Object) null != (UnityEngine.Object) instance))
        return;
      this.StartCoroutine(activationEffect.DestroyWhenFinished(instance));
    }

    public override ITimelineContext GetTimelineContext() => (ITimelineContext) this.m_context;

    public override TooltipDataType tooltipDataType { get; } = TooltipDataType.ObjectMechanism;

    public override int GetTitleKey() => this.m_definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public override KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;

    public int GetArmorValue() => this.life;

    [SpecialName]
    GameObject IIsoObject.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IIsoObject.get_transform() => this.transform;
  }
}
