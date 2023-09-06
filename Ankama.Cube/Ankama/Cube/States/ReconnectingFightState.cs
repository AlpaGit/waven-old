// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.ReconnectingFightState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Fight;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Utility;
using System;

namespace Ankama.Cube.States
{
  public class ReconnectingFightState : StateContext
  {
    private ReconnectingFightFrame m_frame;

    protected override void Enable()
    {
      this.m_frame = new ReconnectingFightFrame()
      {
        OnFightDataReceived = new Action<FightInfo>(this.OnFightDataReceived)
      };
      this.m_frame.RequestFightInfo();
    }

    protected override void Disable() => this.m_frame.Dispose();

    private void OnFightDataReceived(FightInfo fightInfo) => StatesUtility.DoTransition((StateContext) new FightState(fightInfo, true), StateManager.GetDefaultLayer().GetChildState());
  }
}
