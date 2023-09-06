// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.PlaySpellEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class PlaySpellEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int spellInstanceId { get; private set; }

    public int spellDefId { get; private set; }

    public int spellLevel { get; private set; }

    public int actionPointsCost { get; private set; }

    public IReadOnlyList<CastTarget> targets { get; private set; }

    public PlaySpellEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int spellInstanceId,
      int spellDefId,
      int spellLevel,
      int actionPointsCost,
      IReadOnlyList<CastTarget> targets)
      : base(FightEventData.Types.EventType.PlaySpell, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.spellInstanceId = spellInstanceId;
      this.spellDefId = spellDefId;
      this.spellLevel = spellLevel;
      this.actionPointsCost = actionPointsCost;
      this.targets = targets;
    }

    public PlaySpellEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.PlaySpell, proto)
    {
      this.concernedEntity = proto.Int1;
      this.spellInstanceId = proto.Int2;
      this.spellDefId = proto.Int3;
      this.spellLevel = proto.Int4;
      this.actionPointsCost = proto.Int5;
      this.targets = (IReadOnlyList<CastTarget>) proto.CastTargetList1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      PlayerStatus entityStatus;
      if (fightStatus.TryGetEntity<PlayerStatus>(this.concernedEntity, out entityStatus))
      {
        SpellStatus spellStatus;
        if (entityStatus.TryGetSpell(this.spellInstanceId, out spellStatus))
        {
          if (!((Object) null == (Object) spellStatus.definition))
            return;
          SpellDefinition spellDefinition;
          if (RuntimeData.spellDefinitions.TryGetValue(this.spellDefId, out spellDefinition))
            spellStatus.Upgrade(spellDefinition, this.spellLevel);
          else
            Log.Error(FightEventErrors.DefinitionNotFound<SpellDefinition>(this.spellDefId), 31, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PlaySpellEvent.cs");
        }
        else
          Log.Error(string.Format("Could not find spell with instance id {0} for player with id {1}.", (object) this.spellInstanceId, (object) this.concernedEntity), 37, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PlaySpellEvent.cs");
      }
      else
        Log.Error(FightEventErrors.PlayerNotFound(this.concernedEntity), 42, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PlaySpellEvent.cs");
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      PlaySpellEvent playSpellEvent = this;
      PlayerStatus playerStatus;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<PlayerStatus>(__nonvirtual (playSpellEvent.concernedEntity), out playerStatus))
      {
        SpellStatus spellStatus;
        if (playerStatus.TryGetSpell(playSpellEvent.spellInstanceId, out spellStatus))
        {
          SpellDefinition definition = spellStatus.definition;
          if ((Object) null != (Object) definition)
          {
            yield return (object) definition.LoadResources();
            // ISSUE: explicit non-virtual call
            CastTargetContext castTargetContext = definition.castTarget.CreateCastTargetContext(fightStatus, __nonvirtual (playSpellEvent.concernedEntity), DynamicValueHolderType.Spell, playSpellEvent.spellDefId, playSpellEvent.spellLevel, 0);
            int count = ((IReadOnlyCollection<CastTarget>) playSpellEvent.targets).Count;
            for (int index = 0; index < count; ++index)
              castTargetContext.SelectTarget(playSpellEvent.targets[index].ToTarget(fightStatus));
            if (count > 0 && !playerStatus.isLocalPlayer)
              yield return (object) FightUIRework.ShowPlayingSpell(spellStatus, PlaySpellEvent.GetTargetedCell(fightStatus, playSpellEvent.targets[0]));
            List<SpellEffectInstantiationData> spellEffectData = (List<SpellEffectInstantiationData>) definition.spellEffectData;
            int spellEffectCount = spellEffectData.Count;
            if (spellEffectCount > 0)
            {
              List<IEnumerator> routineList = ListPool<IEnumerator>.Get();
              for (int i = 0; i < spellEffectCount; ++i)
              {
                SpellEffect spellEffect = definition.GetSpellEffect(i);
                if (!((Object) null == (Object) spellEffect))
                {
                  SpellEffectInstantiationData instantiationData = spellEffectData[i];
                  instantiationData.PreComputeDelayOverDistance((DynamicValueContext) castTargetContext);
                  foreach (Vector2Int instantiationPosition in instantiationData.EnumerateInstantiationPositions((DynamicValueContext) castTargetContext))
                    routineList.Add(FightSpellEffectFactory.PlaySpellEffect(spellEffect, instantiationPosition, instantiationData, castTargetContext));
                  foreach (IsoObject instantiationObjectTarget in instantiationData.EnumerateInstantiationObjectTargets((DynamicValueContext) castTargetContext))
                    routineList.Add(FightSpellEffectFactory.PlaySpellEffect(spellEffect, instantiationObjectTarget, instantiationData, castTargetContext));
                  yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(routineList.ToArray());
                  routineList.Clear();
                }
              }
              ListPool<IEnumerator>.Release(routineList);
              routineList = (List<IEnumerator>) null;
            }
            FightSpellEffectFactory.SetupSpellEffectOverrides((ISpellEffectOverrideProvider) definition, fightStatus.fightId, playSpellEvent.eventId);
            castTargetContext = (CastTargetContext) null;
            spellEffectData = (List<SpellEffectInstantiationData>) null;
          }
          definition = (SpellDefinition) null;
        }
        else
        {
          // ISSUE: explicit non-virtual call
          Log.Error(string.Format("Could not find spell with instance id {0} for player with id {1}.", (object) playSpellEvent.spellInstanceId, (object) __nonvirtual (playSpellEvent.concernedEntity)), 128, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PlaySpellEvent.cs");
        }
        spellStatus = (SpellStatus) null;
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.PlayerNotFound(__nonvirtual (playSpellEvent.concernedEntity)), 133, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\PlaySpellEvent.cs");
      }
    }

    private static CellObject GetTargetedCell(FightStatus fightStatus, CastTarget castTarget)
    {
      FightMap current = FightMap.current;
      if ((Object) null == (Object) current)
        return (CellObject) null;
      switch (castTarget.ValueCase)
      {
        case CastTarget.ValueOneofCase.Cell:
          CellCoord cell = castTarget.Cell;
          return current.GetCellObject(cell.X, cell.Y);
        case CastTarget.ValueOneofCase.EntityId:
          int entityId = castTarget.EntityId;
          IEntityWithBoardPresence entityStatus;
          if (!fightStatus.TryGetEntity<IEntityWithBoardPresence>(entityId, out entityStatus))
            return (CellObject) null;
          Vector2Int refCoord = entityStatus.area.refCoord;
          return current.GetCellObject(refCoord.x, refCoord.y);
        default:
          return (CellObject) null;
      }
    }
  }
}
