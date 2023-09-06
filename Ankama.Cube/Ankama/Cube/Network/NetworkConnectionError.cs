// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.NetworkConnectionError
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Network
{
  public class NetworkConnectionError : IConnectionError
  {
    public System.Exception exception { get; }

    public NetworkConnectionError(System.Exception exception) => this.exception = exception;
  }
}
