// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.FloorMechanismObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Maps.Objects
{
  public sealed class FloorMechanismObject : 
    CharacterObject,
    IObjectWithActivation,
    ICharacterObject,
    IMovableIsoObject,
    IIsoObject,
    IObjectWithAssemblage,
    ITextTooltipDataProvider,
    ITooltipDataProvider
  {
    [SerializeField]
    private FloorMechanismDefinition m_definition;
    [SerializeField]
    private FloorMechanismBase m_base;
    private FloorMechanismAnimator m_animator;
    private GameObject m_instance;
    private AnimatedFloorMechanismData m_characterData;
    private DynamicFightValueProvider m_tooltipValueProvider;
    private bool m_alliedWithLocalPlayer;
    private FloorMechanismObjectContext m_context;

    public override IsoObjectDefinition definition
    {
      get => (IsoObjectDefinition) this.m_definition;
      protected set => this.m_definition = (FloorMechanismDefinition) value;
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

    public void Initialize(
      FightStatus fightStatus,
      PlayerStatus ownerStatus,
      FloorMechanismStatus floorMechanismStatus)
    {
      this.m_tooltipValueProvider = new DynamicFightValueProvider((IDynamicValueSource) floorMechanismStatus, floorMechanismStatus.level);
      TimelineContextUtility.SetFightContext(this.m_playableDirector, fightStatus.context);
      this.m_base.Setup(ownerStatus.playerType);
    }

    public override void SetColorModifier(Color value)
    {
      base.SetColorModifier(value);
      this.m_base.color = value;
    }

    public override void Destroy() => FightObjectFactory.ReleaseFloorMechanismObject(this);

    protected override void OnEnable()
    {
      base.OnEnable();
      this.m_context = new FloorMechanismObjectContext(this);
      this.m_context.Initialize();
    }

    private void PlayTimeline(string timelineKey, string animationName, bool restart)
    {
      ITimelineAssetProvider characterData = (ITimelineAssetProvider) this.m_characterData;
      if (characterData == null)
        return;
      TimelineAsset timelineAsset1;
      bool timelineAsset2 = characterData.TryGetTimelineAsset(timelineKey, out timelineAsset1);
      if (timelineAsset2 && (UnityEngine.Object) null != (UnityEngine.Object) timelineAsset1)
      {
        if ((UnityEngine.Object) timelineAsset1 != (UnityEngine.Object) this.m_playableDirector.playableAsset)
        {
          this.m_playableDirector.Play((PlayableAsset) timelineAsset1);
        }
        else
        {
          if (restart || !this.m_animator.animationName.Equals(animationName))
            this.m_playableDirector.time = 0.0;
          this.m_playableDirector.Resume();
        }
        this.m_hasTimeline = true;
      }
      else
      {
        if (timelineAsset2)
          Log.Warning("Character named '" + this.GetAnimatedCharacterData().name + "' has a timeline setup for key '" + timelineKey + "' but the actual asset is null.", 121, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FloorMechanismObject.cs");
        this.m_playableDirector.time = 0.0;
        this.m_playableDirector.Pause();
        this.m_hasTimeline = false;
      }
    }

    protected override IAnimator2D GetAnimator() => (IAnimator2D) this.m_animator;

    protected override AnimatedCharacterData GetAnimatedCharacterData() => (AnimatedCharacterData) this.m_characterData;

    protected override void SetAnimatedCharacterData(AnimatedCharacterData data)
    {
      AnimatedFloorMechanismData floorMechanismData = data as AnimatedFloorMechanismData;
      if ((UnityEngine.Object) null == (UnityEngine.Object) floorMechanismData)
        Log.Error("Data type mismatch: an instance of " + this.GetType().Name + " cannot be created with a data asset of type '" + data.GetType().Name + "'.", 149, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FloorMechanismObject.cs");
      else
        this.m_characterData = floorMechanismData;
    }

    protected override void ClearAnimatedCharacterData() => this.m_characterData = (AnimatedFloorMechanismData) null;

    protected override IEnumerator SetAnimatorDefinition()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      FloorMechanismObject floorMechanismObject = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      GameObject prefab = floorMechanismObject.m_characterData.prefab;
      if ((UnityEngine.Object) null == (UnityEngine.Object) prefab)
      {
        Log.Error("AnimatedFloorMechanismData named '" + floorMechanismObject.m_characterData.name + " has no prefab set.", 169, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FloorMechanismObject.cs");
        return false;
      }
      floorMechanismObject.m_instance = UnityEngine.Object.Instantiate<GameObject>(prefab, floorMechanismObject.transform.position, floorMechanismObject.m_direction.GetRotation(), floorMechanismObject.transform);
      floorMechanismObject.m_animator = floorMechanismObject.m_instance.GetComponent<FloorMechanismAnimator>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) floorMechanismObject.m_animator)
        Log.Error("Floor mechanism prefab named '" + prefab.name + "' is missing a FloorMechanismAnimator component.", 178, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\FloorMechanismObject.cs");
      floorMechanismObject.InitializeAnimator();
      return false;
    }

    protected override void ClearAnimatorDefinition()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_instance))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_instance);
      this.m_instance = (GameObject) null;
    }

    protected override void PlayIdleAnimation()
    {
      string animName;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_animator || !this.m_animator.TryGetIdleAnimationName(out animName))
        return;
      this.PlayTimeline("idle", animName, false);
      this.m_animationCallback.Setup(animName, false);
      this.m_animator.SetAnimation(animName, true, true, false);
    }

    protected override IEnumerator PlaySpawnAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      FloorMechanismObject floorMechanismObject = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      if ((UnityEngine.Object) null == (UnityEngine.Object) floorMechanismObject.m_animator)
        return false;
      string animName;
      if (floorMechanismObject.m_animator.TryGetSpawnAnimationName(out animName))
      {
        floorMechanismObject.PlayTimeline("spawn", animName, false);
        floorMechanismObject.m_animationCallback.Setup(animName, false, new Action(((CharacterObject) floorMechanismObject).PlayIdleAnimation));
        floorMechanismObject.m_animator.SetAnimation(animName, false, true, true);
      }
      else if (floorMechanismObject.m_animator.TryGetIdleAnimationName(out animName))
      {
        floorMechanismObject.PlayTimeline("spawn", animName, true);
        floorMechanismObject.m_animationCallback.Setup(animName, false, new Action(((CharacterObject) floorMechanismObject).PlayIdleAnimation));
        floorMechanismObject.m_animator.SetAnimation(animName, true, true, true);
      }
      return false;
    }

    protected override IEnumerator PlayDeathAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      FloorMechanismObject floorMechanismObject = this;
      if (num != 0)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      string animName;
      if ((UnityEngine.Object) null == (UnityEngine.Object) floorMechanismObject.m_animator || !floorMechanismObject.m_animator.TryGetDestructionAnimationName(out animName))
        return false;
      floorMechanismObject.PlayTimeline("die", animName, false);
      floorMechanismObject.m_animationCallback.Setup(animName, false);
      floorMechanismObject.m_animator.SetAnimation(animName, false, true, true);
      return false;
    }

    public override void ShowSpellTargetFeedback(bool isSelected)
    {
      this.gameObject.SetLayerRecursively(LayerMaskNames.characterFocusLayer);
      this.m_base.SetTargetState(isSelected ? FloorMechanismBase.TargetState.Targeted : FloorMechanismBase.TargetState.Targetable);
    }

    public override void HideSpellTargetFeedback()
    {
      this.gameObject.SetLayerRecursively(LayerMaskNames.defaultLayer);
      this.m_base.SetTargetState(FloorMechanismBase.TargetState.None);
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

    public override void CheckParentCellIndicator()
    {
    }

    public IEnumerator ActivatedByAlly()
    {
      FloorMechanismObject floorMechanismObject = this;
      FloorMechanismAnimator animator = floorMechanismObject.m_animator;
      string activationAnimationName;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) animator) && animator.TryGetAllyActivationAnimationName(out activationAnimationName))
      {
        floorMechanismObject.PlayTimeline("ally_activation", activationAnimationName, false);
        floorMechanismObject.m_animationCallback.Setup(activationAnimationName, false);
        animator.SetAnimation(activationAnimationName, false, false, true);
        do
        {
          yield return (object) null;
        }
        while (!CharacterObjectUtility.HasAnimationReachedLabel((IAnimator2D) animator, activationAnimationName, "shot"));
      }
    }

    public IEnumerator ActivatedByOpponent()
    {
      FloorMechanismObject floorMechanismObject = this;
      FloorMechanismAnimator animator = floorMechanismObject.m_animator;
      string activationAnimationName;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) animator) && animator.TryGetOpponentActivationAnimationName(out activationAnimationName))
      {
        floorMechanismObject.PlayTimeline("opponent_activation", activationAnimationName, false);
        floorMechanismObject.m_animationCallback.Setup(activationAnimationName, false);
        animator.SetAnimation(activationAnimationName, false, false, true);
        do
        {
          yield return (object) null;
        }
        while (!CharacterObjectUtility.HasAnimationReachedLabel((IAnimator2D) animator, activationAnimationName, "shot"));
      }
    }

    public IEnumerator WaitForActivationEnd()
    {
      FloorMechanismAnimator animator = this.m_animator;
      if (!((UnityEngine.Object) null == (UnityEngine.Object) animator))
      {
        string activationAnimationName = animator.animationName;
        while (!CharacterObjectUtility.HasAnimationEnded((IAnimator2D) animator, activationAnimationName))
          yield return (object) null;
      }
    }

    public void PlayDetectionAnimation()
    {
      FloorMechanismAnimator animator = this.m_animator;
      string animName;
      if ((UnityEngine.Object) null == (UnityEngine.Object) animator || !animator.TryGetDetectionAnimationName(out animName))
        return;
      this.PlayTimeline("detection", animName, false);
      this.m_animationCallback.Setup(animName, false);
      animator.SetAnimation(animName, false, true, true);
    }

    public void RefreshAssemblage(IEnumerable<Vector2Int> otherObjectInAssemblage) => this.m_base.RefreshAssemblage(this.m_cellObject.coords, otherObjectInAssemblage);

    protected override void FocusCharacter()
    {
    }

    protected override void UnFocusCharacter()
    {
    }

    public override ITimelineContext GetTimelineContext() => (ITimelineContext) this.m_context;

    public override TooltipDataType tooltipDataType { get; } = TooltipDataType.FloorMechanism;

    public override int GetTitleKey() => this.m_definition.i18nNameId;

    public override int GetDescriptionKey() => this.m_definition.i18nDescriptionId;

    public override IFightValueProvider GetValueProvider() => (IFightValueProvider) this.m_tooltipValueProvider;

    public override KeywordReference[] keywordReferences => this.m_definition.precomputedData.keywordReferences;

    [SpecialName]
    GameObject IIsoObject.get_gameObject() => this.gameObject;

    [SpecialName]
    Transform IIsoObject.get_transform() => this.transform;
  }
}
