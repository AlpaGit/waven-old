// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.GiveElementPointsCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightAdminProtocol;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class GiveElementPointsCommand : InstantFightAdminCommand
  {
    public GiveElementPointsCommand(KeyCode keycode)
      : base("Give Elements <i>(⇧: 10, Alt: opponent)</i>", keycode)
    {
    }

    protected override void Execute()
    {
      PlayerStatus playerOrOpponent = this.GetPlayerOrOpponent();
      if (playerOrOpponent == null)
        return;
      int num = AbstractFightAdminCommand.IsShiftDown() ? 10 : 1;
      AbstractFightAdminCommand.SendAdminCommand(new AdminRequestCmd()
      {
        GainElementPoints = new AdminRequestCmd.Types.GainElementPointsCmd()
        {
          PlayerEntityId = playerOrOpponent.id,
          Quantity = num
        }
      });
    }
  }
}
