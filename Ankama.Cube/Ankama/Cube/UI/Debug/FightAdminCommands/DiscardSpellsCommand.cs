// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.DiscardSpellsCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightAdminProtocol;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class DiscardSpellsCommand : InstantFightAdminCommand
  {
    public DiscardSpellsCommand(KeyCode keycode)
      : base("Discard Spell <i>(⇧: all, Alt: opponent)</i>", keycode)
    {
    }

    protected override void Execute()
    {
      PlayerStatus playerOrOpponent = this.GetPlayerOrOpponent();
      if (playerOrOpponent == null)
        return;
      int num = AbstractFightAdminCommand.IsShiftDown() ? playerOrOpponent.GetSpellInHandCount() : 1;
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        DiscardSpells = new AdminRequestCmd.Types.DiscardSpellsCmd()
        {
          PlayerEntityId = playerOrOpponent.id,
          Quantity = num
        }
      });
    }
  }
}
