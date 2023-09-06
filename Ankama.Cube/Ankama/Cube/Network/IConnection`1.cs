// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.IConnection`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.ServerProtocol;
using System;

namespace Ankama.Cube.Network
{
  public interface IConnection<T> where T : class
  {
    void Connect(string host, int port);

    void Disconnect();

    void DisconnectByServer(DisconnectedByServerEvent evt);

    void Write(T message);

    event Action<T> OnApplicationMessage;

    event Action<IConnectionError> OnOpenConnectionFailed;

    event Action OnConnectionOpened;

    event Action<IDisconnectionInfo> OnConnectionClosed;
  }
}
