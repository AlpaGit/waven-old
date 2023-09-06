// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityAreaMovedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
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
  public class EntityAreaMovedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int movementType { get; private set; }

    public int direction { get; private set; }

    public IReadOnlyList<CellCoord> cells { get; private set; }

    public EntityAreaMovedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int movementType,
      int direction,
      IReadOnlyList<CellCoord> cells)
      : base(FightEventData.Types.EventType.EntityAreaMoved, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.movementType = movementType;
      this.direction = direction;
      this.cells = cells;
    }

    public EntityAreaMovedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityAreaMoved, proto)
    {
      this.concernedEntity = proto.Int1;
      this.movementType = proto.Int2;
      this.direction = proto.Int3;
      this.cells = (IReadOnlyList<CellCoord>) proto.CellCoordList1;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      IEntityWithBoardPresence entityStatus;
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(this.concernedEntity, out entityStatus))
      {
        Area area = entityStatus.area;
        int count = ((IReadOnlyCollection<CellCoord>) this.cells).Count;
        Vector2Int cell1 = (Vector2Int) this.cells[0];
        Vector2Int cell2 = (Vector2Int) this.cells[count - 1];
        entityStatus.area.MoveTo(cell2);
        fightStatus.NotifyEntityAreaMoved();
        if (this.IsMovementAction() && entityStatus is CharacterStatus characterStatus)
        {
          characterStatus.actionUsed = true;
          fightStatus.NotifyEntityPlayableStateChanged();
        }
      }
      else
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(this.concernedEntity), 46, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
      FightLogicExecutor.FireUpdateStatus(fightStatus.fightId, EventCategory.EntityMoved);
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      EntityAreaMovedEvent entityAreaMovedEvent = this;
      int fightId = fightStatus.fightId;
      IEntityWithBoardPresence entity;
      // ISSUE: explicit non-virtual call
      if (fightStatus.TryGetEntity<IEntityWithBoardPresence>(__nonvirtual (entityAreaMovedEvent.concernedEntity), out entity))
      {
        IsoObject isoObject = entity.view;
        if ((UnityEngine.Object) null != (UnityEngine.Object) isoObject)
        {
          Vector2Int[] path;
          switch (entityAreaMovedEvent.movementType)
          {
            case 1:
              if (isoObject is IObjectWithMovement objectWithMovement1)
              {
                yield return (object) objectWithMovement1.Move(entityAreaMovedEvent.GetPath());
                break;
              }
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithMovement>(entity), 71, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              break;
            case 2:
            case 4:
              if (isoObject is IObjectWithMovement objectWithMovement2)
              {
                Ankama.Cube.Data.Direction direction = (Ankama.Cube.Data.Direction) entityAreaMovedEvent.direction;
                yield return (object) objectWithMovement2.MoveToAction(entityAreaMovedEvent.GetPath(), direction);
                break;
              }
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithMovement>(entity), 143, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              break;
            case 3:
              if (isoObject is IMovableObject objectWithMovement5)
              {
                FightContext fightContext = fightStatus.context;
                yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.TeleportationStart, fightId, entityAreaMovedEvent.parentEventId, isoObject, fightContext);
                objectWithMovement5.Teleport(entityAreaMovedEvent.GetDestination());
                yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.TeleportationEnd, fightId, entityAreaMovedEvent.parentEventId, isoObject, fightContext);
                fightContext = (FightContext) null;
              }
              else
                Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithMovement>(entity), 88, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              objectWithMovement5 = (IMovableObject) null;
              break;
            case 6:
              if (isoObject is IMovableObject objectWithMovement5)
              {
                path = entityAreaMovedEvent.GetPath();
                Quaternion pathRotation = entityAreaMovedEvent.GetPathRotation(path);
                Transform transform = isoObject.cellObject.transform;
                ITimelineContextProvider contextProvider = isoObject as ITimelineContextProvider;
                yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.Push, fightId, entityAreaMovedEvent.parentEventId, transform, pathRotation, Vector3.one, fightStatus.context, contextProvider);
                yield return (object) objectWithMovement5.Push(path);
                path = (Vector2Int[]) null;
              }
              else
                Log.Error(FightEventErrors.EntityHasIncompatibleView<IMovableObject>(entity), 108, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              objectWithMovement5 = (IMovableObject) null;
              break;
            case 7:
              if (isoObject is IMovableObject objectWithMovement5)
              {
                path = entityAreaMovedEvent.GetPath();
                Quaternion pathRotation = entityAreaMovedEvent.GetPathRotation(path);
                Transform transform = isoObject.cellObject.transform;
                ITimelineContextProvider contextProvider = isoObject as ITimelineContextProvider;
                yield return (object) FightSpellEffectFactory.PlayGenericEffect(SpellEffectKey.Pull, fightId, entityAreaMovedEvent.parentEventId, transform, pathRotation, Vector3.one, fightStatus.context, contextProvider);
                yield return (object) objectWithMovement5.Pull(path);
                path = (Vector2Int[]) null;
              }
              else
                Log.Error(FightEventErrors.EntityHasIncompatibleView<IMovableObject>(entity), 128, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              objectWithMovement5 = (IMovableObject) null;
              break;
            case 8:
              if (isoObject is IObjectWithMovement objectWithMovement6)
              {
                Ankama.Cube.Data.Direction direction = (Ankama.Cube.Data.Direction) entityAreaMovedEvent.direction;
                yield return (object) objectWithMovement6.MoveToAction(entityAreaMovedEvent.GetPath(), direction, false);
                break;
              }
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithMovement>(entity), 157, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
              break;
            default:
              throw new ArgumentOutOfRangeException();
          }
          if (entityAreaMovedEvent.IsMovementAction())
          {
            if (isoObject is IObjectWithAction objectWithAction)
              objectWithAction.SetActionUsed(true, false);
            else
              Log.Error(FightEventErrors.EntityHasIncompatibleView<IObjectWithAction>(entity), 174, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
          }
          if (isoObject is ICharacterObject characterObject)
            characterObject.CheckParentCellIndicator();
        }
        else
          Log.Error(FightEventErrors.EntityHasNoView(entity), 185, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
        isoObject = (IsoObject) null;
      }
      else
      {
        // ISSUE: explicit non-virtual call
        Log.Error(FightEventErrors.EntityNotFound<IEntityWithBoardPresence>(__nonvirtual (entityAreaMovedEvent.concernedEntity)), 190, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
      }
      FightLogicExecutor.FireUpdateView(fightId, EventCategory.EntityMoved);
    }

    private bool IsMovementAction()
    {
      switch (this.movementType)
      {
        case 1:
        case 2:
          return true;
        case 3:
        case 4:
        case 6:
        case 7:
        case 8:
          return false;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private Vector2Int[] GetPath()
    {
      int count = ((IReadOnlyCollection<CellCoord>) this.cells).Count;
      Vector2Int[] path = new Vector2Int[count];
      for (int index = 0; index < count; ++index)
        path[index] = (Vector2Int) this.cells[index];
      return path;
    }

    private Quaternion GetPathRotation(Vector2Int[] path)
    {
      if (path.Length > 1)
        return path[0].GetDirectionTo(path[1]).GetRotation();
      Log.Warning(string.Format("Movement of type {0} sent with an invalid path length ({1}).", (object) this.movementType, (object) path.Length), 237, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\Events\\EntityAreaMovedEvent.cs");
      return Quaternion.identity;
    }

    private Vector2Int GetDestination() => (Vector2Int) this.cells[((IReadOnlyCollection<CellCoord>) this.cells).Count - 1];
  }
}
