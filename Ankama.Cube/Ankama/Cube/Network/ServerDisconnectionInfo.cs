// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.ServerDisconnectionInfo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.ServerProtocol;

namespace Ankama.Cube.Network
{
  public class ServerDisconnectionInfo : IDisconnectionInfo
  {
    public DisconnectedByServerEvent.Types.Reason reason { get; }

    public ServerDisconnectionInfo(DisconnectedByServerEvent.Types.Reason reason) => this.reason = reason;
  }
}
