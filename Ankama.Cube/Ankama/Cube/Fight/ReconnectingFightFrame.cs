// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.ReconnectingFightFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.Protocols.FightProtocol;
using Google.Protobuf;
using System;

namespace Ankama.Cube.Fight
{
  public sealed class ReconnectingFightFrame : CubeMessageFrame
  {
    public Action<FightInfo> OnFightDataReceived;

    public ReconnectingFightFrame() => this.WhenReceiveEnqueue<FightInfoEvent>(new Action<FightInfoEvent>(this.OnFightInfo));

    private void OnFightInfo(FightInfoEvent msg)
    {
      Action<FightInfo> fightDataReceived = this.OnFightDataReceived;
      if (fightDataReceived == null)
        return;
      fightDataReceived(msg.FightInfo);
    }

    public void RequestFightInfo() => this.m_connection.Write((IMessage) new GetFightInfoCmd());
  }
}
