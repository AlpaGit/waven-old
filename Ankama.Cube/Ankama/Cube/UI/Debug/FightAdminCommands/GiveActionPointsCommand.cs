// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.GiveActionPointsCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Protocols.FightAdminProtocol;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public class GiveActionPointsCommand : InstantFightAdminCommand
  {
    public GiveActionPointsCommand(KeyCode keycode)
      : base("Give AP <i>(⇧: 10, Alt: opponent)</i>", keycode)
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
        GainActionPoints = new AdminRequestCmd.Types.GainActionPointsCmd()
        {
          PlayerEntityId = playerOrOpponent.id,
          Quantity = num
        }
      });
    }
  }
}
