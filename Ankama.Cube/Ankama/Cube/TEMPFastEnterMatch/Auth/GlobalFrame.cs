// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Auth.GlobalFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.ServerProtocol;
using System;

namespace Ankama.Cube.TEMPFastEnterMatch.Auth
{
  public class GlobalFrame : CubeMessageFrame
  {
    public GlobalFrame() => this.WhenReceiveEnqueue<DisconnectedByServerEvent>(new Action<DisconnectedByServerEvent>(this.OnDisconnectedByServer));

    private void OnDisconnectedByServer(DisconnectedByServerEvent evt) => this.m_connection.DisconnectByServer(evt);
  }
}
