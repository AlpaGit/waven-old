// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.INetworkLayer`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Threading.Tasks;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public interface INetworkLayer<TIn> : IDisposable
  {
    Task ConnectAsync(string host, int port);

    bool Write(TIn input);

    Action<TIn> OnData { set; }

    Action OnConnectionClosed { set; }
  }
}
