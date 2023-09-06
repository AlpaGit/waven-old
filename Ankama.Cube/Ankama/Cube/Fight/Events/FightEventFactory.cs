// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightEventFactory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.ComponentModel;

namespace Ankama.Cube.Fight.Events
{
  public static class FightEventFactory
  {
    public static FightEvent FromProto(FightEventData proto)
    {
      switch (proto.EventType)
      {
        case FightEventData.Types.EventType.EffectStopped:
          return (FightEvent) new EffectStoppedEvent(proto);
        case FightEventData.Types.EventType.TurnStarted:
          return (FightEvent) new TurnStartedEvent(proto);
        case FightEventData.Types.EventType.EntityAreaMoved:
          return (FightEvent) new EntityAreaMovedEvent(proto);
        case FightEventData.Types.EventType.PlayerAdded:
          return (FightEvent) new PlayerAddedEvent(proto);
        case FightEventData.Types.EventType.HeroAdded:
          return (FightEvent) new HeroAddedEvent(proto);
        case FightEventData.Types.EventType.CompanionAdded:
          return (FightEvent) new CompanionAddedEvent(proto);
        case FightEventData.Types.EventType.EntityActioned:
          return (FightEvent) new EntityActionedEvent(proto);
        case FightEventData.Types.EventType.SpellsMoved:
          return (FightEvent) new SpellsMovedEvent(proto);
        case FightEventData.Types.EventType.TeamTurnEnded:
          return (FightEvent) new TeamTurnEndedEvent(proto);
        case FightEventData.Types.EventType.PlaySpell:
          return (FightEvent) new PlaySpellEvent(proto);
        case FightEventData.Types.EventType.TurnEnded:
          return (FightEvent) new TurnEndedEvent(proto);
        case FightEventData.Types.EventType.ArmoredLifeChanged:
          return (FightEvent) new ArmoredLifeChangedEvent(proto);
        case FightEventData.Types.EventType.EntityRemoved:
          return (FightEvent) new EntityRemovedEvent(proto);
        case FightEventData.Types.EventType.ElementPointsChanged:
          return (FightEvent) new ElementPointsChangedEvent(proto);
        case FightEventData.Types.EventType.CompanionAddedInReserve:
          return (FightEvent) new CompanionAddedInReserveEvent(proto);
        case FightEventData.Types.EventType.ActionPointsChanged:
          return (FightEvent) new ActionPointsChangedEvent(proto);
        case FightEventData.Types.EventType.CompanionReserveStateChanged:
          return (FightEvent) new CompanionReserveStateChangedEvent(proto);
        case FightEventData.Types.EventType.EntityProtectionAdded:
          return (FightEvent) new EntityProtectionAddedEvent(proto);
        case FightEventData.Types.EventType.EntityProtectionRemoved:
          return (FightEvent) new EntityProtectionRemovedEvent(proto);
        case FightEventData.Types.EventType.MagicalDamageModifierChanged:
          return (FightEvent) new MagicalDamageModifierChangedEvent(proto);
        case FightEventData.Types.EventType.MagicalHealModifierChanged:
          return (FightEvent) new MagicalHealModifierChangedEvent(proto);
        case FightEventData.Types.EventType.MovementPointsChanged:
          return (FightEvent) new MovementPointsChangedEvent(proto);
        case FightEventData.Types.EventType.DiceThrown:
          return (FightEvent) new DiceThrownEvent(proto);
        case FightEventData.Types.EventType.SummoningAdded:
          return (FightEvent) new SummoningAddedEvent(proto);
        case FightEventData.Types.EventType.EntityActionReset:
          return (FightEvent) new EntityActionResetEvent(proto);
        case FightEventData.Types.EventType.ReservePointsChanged:
          return (FightEvent) new ReservePointsChangedEvent(proto);
        case FightEventData.Types.EventType.ReserveUsed:
          return (FightEvent) new ReserveUsedEvent(proto);
        case FightEventData.Types.EventType.PropertyChanged:
          return (FightEvent) new PropertyChangedEvent(proto);
        case FightEventData.Types.EventType.FloorMechanismAdded:
          return (FightEvent) new FloorMechanismAddedEvent(proto);
        case FightEventData.Types.EventType.FloorMechanismActivation:
          return (FightEvent) new FloorMechanismActivationEvent(proto);
        case FightEventData.Types.EventType.ObjectMechanismAdded:
          return (FightEvent) new ObjectMechanismAddedEvent(proto);
        case FightEventData.Types.EventType.SpellCostModifierAdded:
          return (FightEvent) new SpellCostModifierAddedEvent(proto);
        case FightEventData.Types.EventType.SpellCostModifierRemoved:
          return (FightEvent) new SpellCostModifierRemovedEvent(proto);
        case FightEventData.Types.EventType.TeamAdded:
          return (FightEvent) new TeamAddedEvent(proto);
        case FightEventData.Types.EventType.FightEnded:
          return (FightEvent) new FightEndedEvent(proto);
        case FightEventData.Types.EventType.Transformation:
          return (FightEvent) new TransformationEvent(proto);
        case FightEventData.Types.EventType.ElementaryChanged:
          return (FightEvent) new ElementaryChangedEvent(proto);
        case FightEventData.Types.EventType.DamageReduced:
          return (FightEvent) new DamageReducedEvent(proto);
        case FightEventData.Types.EventType.Attack:
          return (FightEvent) new AttackEvent(proto);
        case FightEventData.Types.EventType.Explosion:
          return (FightEvent) new ExplosionEvent(proto);
        case FightEventData.Types.EventType.EntityAnimation:
          return (FightEvent) new EntityAnimationEvent(proto);
        case FightEventData.Types.EventType.EntitySkinChanged:
          return (FightEvent) new EntitySkinChangedEvent(proto);
        case FightEventData.Types.EventType.FloatingCounterValueChanged:
          return (FightEvent) new FloatingCounterValueChangedEvent(proto);
        case FightEventData.Types.EventType.AssemblageChanged:
          return (FightEvent) new AssemblageChangedEvent(proto);
        case FightEventData.Types.EventType.PhysicalDamageModifierChanged:
          return (FightEvent) new PhysicalDamageModifierChangedEvent(proto);
        case FightEventData.Types.EventType.PhysicalHealModifierChanged:
          return (FightEvent) new PhysicalHealModifierChangedEvent(proto);
        case FightEventData.Types.EventType.BossSummoningsWarning:
          return (FightEvent) new BossSummoningsWarningEvent(proto);
        case FightEventData.Types.EventType.BossReserveModification:
          return (FightEvent) new BossReserveModificationEvent(proto);
        case FightEventData.Types.EventType.BossEvolutionStepModification:
          return (FightEvent) new BossEvolutionStepModificationEvent(proto);
        case FightEventData.Types.EventType.BossTurnStart:
          return (FightEvent) new BossTurnStartEvent(proto);
        case FightEventData.Types.EventType.BossLifeModification:
          return (FightEvent) new BossLifeModificationEvent(proto);
        case FightEventData.Types.EventType.BossCastSpell:
          return (FightEvent) new BossCastSpellEvent(proto);
        case FightEventData.Types.EventType.GameEnded:
          return (FightEvent) new GameEndedEvent(proto);
        case FightEventData.Types.EventType.CompanionGiven:
          return (FightEvent) new CompanionGivenEvent(proto);
        case FightEventData.Types.EventType.CompanionReceived:
          return (FightEvent) new CompanionReceivedEvent(proto);
        case FightEventData.Types.EventType.TeamTurnStarted:
          return (FightEvent) new TeamTurnStartedEvent(proto);
        case FightEventData.Types.EventType.BossTurnEnd:
          return (FightEvent) new BossTurnEndEvent(proto);
        case FightEventData.Types.EventType.TurnSynchronization:
          return (FightEvent) new TurnSynchronizationEvent(proto);
        case FightEventData.Types.EventType.EventForParenting:
          return (FightEvent) new EventForParentingEvent(proto);
        case FightEventData.Types.EventType.TeamsScoreModification:
          return (FightEvent) new TeamsScoreModificationEvent(proto);
        case FightEventData.Types.EventType.MaxLifeChanged:
          return (FightEvent) new MaxLifeChangedEvent(proto);
        case FightEventData.Types.EventType.FightInitialized:
          return (FightEvent) new FightInitializedEvent(proto);
        case FightEventData.Types.EventType.EntityPassiveFx:
          return (FightEvent) new EntityPassiveFxEvent(proto);
        default:
          throw new InvalidEnumArgumentException(proto.EventType.ToString());
      }
    }
  }
}
