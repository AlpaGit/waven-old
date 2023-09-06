// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityAnimationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class EntityAnimationEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int animationKey { get; private set; }

    public IReadOnlyList<CellCoord> additionalCoords { get; private set; }

    public EntityAnimationEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int animationKey,
      IReadOnlyList<CellCoord> additionalCoords)
      : base(FightEventData.Types.EventType.EntityAnimation, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.animationKey = animationKey;
      this.additionalCoords = additionalCoords;
    }

    public EntityAnimationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityAnimation, proto)
    {
      this.concernedEntity = proto.Int1;
      this.animationKey = proto.Int2;
      this.additionalCoords = (IReadOnlyList<CellCoord>) proto.CellCoordList1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      switch (this.animationKey)
      {
        case 1:
          return this.DoAction(fightStatus);
        case 2:
          return this.DoRangedAction(fightStatus);
        case 3:
          return this.DoActivation(fightStatus);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private IEnumerator DoAction(FightStatus fightStatus)
    {
      if (((IReadOnlyCollection<CellCoord>) this.additionalCoords).Count < 1)
      {
        Log.Error(string.Format("{0} with key {1} has not supplied an additional coordinate.", (object) nameof (EntityAnimationEvent), (object) EntityAnimationKey.Attack), 35, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
      }
      else
      {
        IEntityWithBoardPresence entityStatus;
        if (!fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
          Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 41, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
        else if (entityStatus.view is IObjectWithAction view)
        {
          Vector2Int coords = view.cellObject.coords;
          yield return (object) view.DoAction(coords, (Vector2Int) this.additionalCoords[0]);
        }
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entityStatus), 52, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
      }
    }

    private IEnumerator DoRangedAction(FightStatus fightStatus)
    {
      if (((IReadOnlyCollection<CellCoord>) this.additionalCoords).Count < 1)
      {
        Log.Error(string.Format("{0} with key {1} has not supplied an additional coordinate.", (object) nameof (EntityAnimationEvent), (object) EntityAnimationKey.Attack), 60, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
      }
      else
      {
        IEntityWithBoardPresence entityStatus;
        if (!fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
          Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 66, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
        else if (entityStatus.view is IObjectWithAction view)
        {
          Vector2Int coords = view.cellObject.coords;
          yield return (object) view.DoRangedAction(coords, (Vector2Int) this.additionalCoords[0]);
        }
        else
          Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entityStatus), 77, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
      }
    }

    private IEnumerator DoActivation(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (!fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 85, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
      else if (entityStatus.view is IObjectWithActivationAnimation view)
      {
        Vector2Int coords = view.cellObject.coords;
        yield return (object) view.AnimateActivation(coords);
      }
      else
        Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithActivationAnimation>(entityStatus), 96, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAnimationEvent.cs");
    }
  }
}
