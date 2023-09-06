// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.PickSpellCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightAdminProtocol;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class PickSpellCommand : ToggleFightAdminCommand
  {
    private readonly DebugDropperSpell m_spellDropper;

    public PickSpellCommand(KeyCode keycode, DebugDropperSpell spellDropper)
      : base("Pick Spell <i>(⇧: fill hand, Alt: opponent)</i>", keycode)
    {
      this.m_spellDropper = spellDropper;
    }

    protected override void Start()
    {
      this.m_spellDropper.OnSelected += new Action<SpellDefinition, int, Event>(this.OnSpellSelected);
      this.m_spellDropper.SetActive(true);
      this.m_spellDropper.SetLocalPlayerLevel();
      this.m_spellDropper.SetCloseKeyCode(this.key);
    }

    protected override void Stop()
    {
      this.m_spellDropper.OnSelected -= new Action<SpellDefinition, int, Event>(this.OnSpellSelected);
      this.m_spellDropper.SetActive(false);
    }

    protected override bool Update()
    {
      if (Input.GetKey(this.key))
        this.m_spellDropper.SetActive(false);
      return this.m_spellDropper.isActiveAndEnabled;
    }

    private void OnSpellSelected(SpellDefinition definition, int level, Event lastEvent)
    {
      PlayerStatus playerOrOpponent = this.GetPlayerOrOpponent(lastEvent);
      int num = lastEvent.shift ? GameStatus.fightDefinition.maxSpellInHand - playerOrOpponent.GetSpellInHandCount() : 1;
      if (num == 0)
        return;
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        PickSpell = new AdminRequestCmd.Types.PickSpellCmd()
        {
          PlayerEntityId = playerOrOpponent.id,
          Quantity = num,
          SpellDefinitionId = definition.id,
          SpellLevel = level
        }
      });
    }
  }
}
