// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.TeleportCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Protocols.FightAdminProtocol;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class TeleportCommand : ContinuousFightAdminCommand
  {
    private Tuple<IEntityWithBoardPresence, Coord> m_movingEntity;
    private IEntityWithBoardPresence m_selectedEntity;

    public TeleportCommand(KeyCode keycode)
      : base("Teleport", keycode)
    {
    }

    protected override void Start() => this.SetTargetingPhase((IEntityWithBoardPresence) null);

    private void OnEntitySelected(Target? obj)
    {
      if (!obj.HasValue)
        return;
      Target target = obj.Value;
      if (target.type != Target.Type.Entity || !(target.entity is IEntityWithBoardPresence entity))
        return;
      this.SetTargetingPhase(entity);
    }

    private void OnDestinationSelected(Target? obj)
    {
      if (!obj.HasValue)
        return;
      Target target = obj.Value;
      if (target.type != Target.Type.Coord)
        return;
      if (this.m_selectedEntity == null)
      {
        this.SetTargetingPhase((IEntityWithBoardPresence) null);
      }
      else
      {
        Coord coord = target.coord;
        AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
        {
          Teleport = new AdminRequestCmd.Types.TeleportAdminCmd()
          {
            TargetEntityId = this.m_selectedEntity.id,
            Destination = coord.ToCellCoord()
          }
        });
        this.m_movingEntity = new Tuple<IEntityWithBoardPresence, Coord>(this.m_selectedEntity, coord);
        this.m_selectedEntity = (IEntityWithBoardPresence) null;
        FightMap fightMap = this.m_fightMap;
        fightMap.onTargetSelected = fightMap.onTargetSelected - new Action<Target?>(this.OnDestinationSelected);
        this.EndTargetingPhase();
      }
    }

    protected override void Update()
    {
      if (this.m_movingEntity == null || !this.m_movingEntity.Item2.Equals(this.m_movingEntity.Item1.area.refCoord))
        return;
      this.SetTargetingPhase((IEntityWithBoardPresence) null);
      this.m_movingEntity = (Tuple<IEntityWithBoardPresence, Coord>) null;
    }

    protected override void Stop()
    {
      FightMap fightMap1 = this.m_fightMap;
      fightMap1.onTargetSelected = fightMap1.onTargetSelected - new Action<Target?>(this.OnEntitySelected);
      FightMap fightMap2 = this.m_fightMap;
      fightMap2.onTargetSelected = fightMap2.onTargetSelected - new Action<Target?>(this.OnDestinationSelected);
      this.EndTargetingPhase();
      this.m_movingEntity = (Tuple<IEntityWithBoardPresence, Coord>) null;
    }

    private void SetTargetingPhase(IEntityWithBoardPresence entity)
    {
      if (entity == null)
      {
        this.m_fightMap.SetTargetingPhase(this.EnumerateEntitiesAsTargets<IEntityWithBoardPresence>());
        FightMap fightMap1 = this.m_fightMap;
        fightMap1.onTargetSelected = fightMap1.onTargetSelected - new Action<Target?>(this.OnDestinationSelected);
        FightMap fightMap2 = this.m_fightMap;
        fightMap2.onTargetSelected = fightMap2.onTargetSelected + new Action<Target?>(this.OnEntitySelected);
      }
      else
      {
        this.m_fightMap.SetTargetingPhase(this.EnumerateValidCellsFor(entity));
        FightMap fightMap3 = this.m_fightMap;
        fightMap3.onTargetSelected = fightMap3.onTargetSelected - new Action<Target?>(this.OnEntitySelected);
        FightMap fightMap4 = this.m_fightMap;
        fightMap4.onTargetSelected = fightMap4.onTargetSelected + new Action<Target?>(this.OnDestinationSelected);
      }
      this.m_selectedEntity = entity;
    }
  }
}
