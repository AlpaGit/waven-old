// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.TransformLayer`2
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Threading.Tasks;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public abstract class TransformLayer<TIn, TOut> : INetworkLayer<TIn>, IDisposable
  {
    protected TransformLayer(INetworkLayer<TOut> child)
    {
      this.child = child;
      this.child.OnData = new Action<TOut>(this.OnDataReceived);
    }

    protected INetworkLayer<TOut> child { get; }

    public virtual async Task ConnectAsync(string host, int port) => await this.child.ConnectAsync(host, port);

    public virtual void Dispose() => this.child.Dispose();

    public abstract bool Write(TIn input);

    protected abstract void OnDataReceived(TOut data);

    public Action<TIn> OnData { protected get; set; }

    public Action OnConnectionClosed
    {
      set => this.child.OnConnectionClosed = value;
    }
  }
}
