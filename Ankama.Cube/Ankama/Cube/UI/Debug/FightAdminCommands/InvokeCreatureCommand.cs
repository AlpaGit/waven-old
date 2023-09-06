// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.InvokeCreatureCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Protocols.CommonProtocol;
using Ankama.Cube.Protocols.FightAdminProtocol;
using DataEditor;
using System;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class InvokeCreatureCommand : ToggleFightAdminCommand
  {
    private readonly DebugDropperCreature m_creatureDropper;
    private bool m_targeting;
    private int m_level;

    public InvokeCreatureCommand(KeyCode keycode, DebugDropperCreature creatureDropper)
      : base("Invoke Creature <i>(Alt: opponent)</i>", keycode)
    {
      this.m_creatureDropper = creatureDropper;
    }

    protected override void Start()
    {
      this.m_targeting = false;
      this.m_creatureDropper.OnSelected += new Action<CharacterDefinition, int, Event>(this.OnDefinitionSelected);
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected + new Action<Target?>(this.InvokeCreatureAt);
      this.m_creatureDropper.SetCloseKeyCode(KeyCode.Escape);
      this.m_creatureDropper.SetSelected((CharacterDefinition) null);
      this.m_creatureDropper.SetActive(true);
      this.m_creatureDropper.SetLocalPlayerLevel();
    }

    private void OnDefinitionSelected(CharacterDefinition definition, int level, Event lastEvent)
    {
      this.m_creatureDropper.SetSelected(definition);
      this.m_level = level;
    }

    protected override bool Update()
    {
      if (this.m_fightStatus.localPlayerId != this.m_fightStatus.currentTurnPlayerId)
        return false;
      if (this.m_targeting)
      {
        CharacterDefinition selected = this.m_creatureDropper.selected;
        this.m_fightMap.SetTargetingPhase(CellValidForCharacterFilter.EnumerateCells(this.m_fightStatus).Select<Coord, Target>((Func<Coord, Target>) (r => new Target(r))));
      }
      if ((UnityEngine.Object) this.m_creatureDropper.selected != (UnityEngine.Object) null && !this.m_targeting)
        this.m_targeting = true;
      return this.m_creatureDropper.isActiveAndEnabled;
    }

    private void InvokeCreatureAt(Target? target)
    {
      CharacterDefinition selected = this.m_creatureDropper.selected;
      if (target.HasValue && (UnityEngine.Object) selected != (UnityEngine.Object) null)
      {
        AdminRequestCmd request = InvokeCreatureCommand.CreateRequest(this.GetPlayerOrOpponent(), (EditableData) selected, this.m_level, target.Value.coord.ToCellCoord());
        if (request != null)
          AbstractFightAdminCommand.SendAdminCommand(request);
      }
      this.m_targeting = false;
      this.m_creatureDropper.SetActive(false);
    }

    protected override void Stop()
    {
      this.m_creatureDropper.OnSelected -= new Action<CharacterDefinition, int, Event>(this.OnDefinitionSelected);
      this.m_creatureDropper.SetSelected((CharacterDefinition) null);
      this.m_creatureDropper.SetActive(false);
      this.m_targeting = false;
      this.EndTargetingPhase();
      FightMap fightMap = this.m_fightMap;
      fightMap.onTargetSelected = fightMap.onTargetSelected - new Action<Target?>(this.InvokeCreatureAt);
    }

    private static AdminRequestCmd CreateRequest(
      PlayerStatus owner,
      EditableData definition,
      int level,
      CellCoord coord)
    {
      switch (definition)
      {
        case CompanionDefinition _:
          return new AdminRequestCmd()
          {
            InvokeCompanion = new AdminRequestCmd.Types.InvokeCompanionAdminCmd()
            {
              OwnerEntityId = owner.id,
              DefinitionId = definition.id,
              CompanionLevel = level,
              Destination = coord
            }
          };
        case SummoningDefinition _:
          return new AdminRequestCmd()
          {
            InvokeSummoning = new AdminRequestCmd.Types.InvokeSummoningAdminCmd()
            {
              OwnerEntityId = owner.id,
              DefinitionId = definition.id,
              SummoningLevel = level,
              Destination = coord
            }
          };
        default:
          return (AdminRequestCmd) null;
      }
    }
  }
}
