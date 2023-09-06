// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.ConnectionInterruptedError
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Network
{
  public class ConnectionInterruptedError : IConnectionError
  {
    public IDisconnectionInfo disconnectionInfo { get; }

    public ConnectionInterruptedError(IDisconnectionInfo disconnectionInfo) => this.disconnectionInfo = disconnectionInfo;
  }
}
