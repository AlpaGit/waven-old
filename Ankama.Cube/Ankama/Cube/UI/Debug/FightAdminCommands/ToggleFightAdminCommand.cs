// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.FightAdminCommands.ToggleFightAdminCommand
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using System;
using UnityEngine;

namespace Ankama.Cube.UI.Debug.FightAdminCommands
{
  public abstract class ToggleFightAdminCommand : AbstractFightAdminCommand
  {
    private bool m_isRunning;

    protected ToggleFightAdminCommand(string name, KeyCode key)
      : base(name, key)
    {
    }

    protected virtual void Start()
    {
    }

    protected virtual void Stop()
    {
    }

    public override sealed bool IsRunning() => this.m_isRunning;

    protected virtual bool Update() => this.IsRunning();

    public override bool Handle()
    {
      bool keyUp = Input.GetKeyUp(this.key);
      if (!this.m_isRunning)
      {
        if (!keyUp)
          return false;
        this.m_isRunning = true;
        switch (FightCastManager.currentCastType)
        {
          case FightCastManager.CurrentCastType.None:
            this.Start();
            break;
          case FightCastManager.CurrentCastType.Spell:
            FightCastManager.StopCastingSpell(true);
            goto case FightCastManager.CurrentCastType.None;
          case FightCastManager.CurrentCastType.Companion:
            FightCastManager.StopInvokingCompanion(true);
            goto case FightCastManager.CurrentCastType.None;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      if (!this.Update())
      {
        this.m_isRunning = false;
        this.Stop();
      }
      return this.m_isRunning;
    }
  }
}
