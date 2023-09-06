// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.InstantFightAdminCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public abstract class InstantFightAdminCommand : AbstractFightAdminCommand
  {
    protected InstantFightAdminCommand(string name, KeyCode key)
      : base(name, key)
    {
    }

    public override sealed bool Handle()
    {
      if (Input.GetKeyDown(this.key))
        this.Execute();
      return false;
    }

    protected abstract void Execute();

    public override sealed bool IsRunning() => false;
  }
}
