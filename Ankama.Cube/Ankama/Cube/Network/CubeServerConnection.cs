// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.CubeServerConnection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2;
using Ankama.Cube.Network.Spin2.Layers;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Google.Protobuf;

namespace Ankama.Cube.Network
{
  public class CubeServerConnection : SpinConnection<IMessage>
  {
    public const int CubeMaximumMessageSize = 65536;
    private readonly TcpConnectionLayer m_tcpConnectionLayer;

    public CubeServerConnection()
      : this(new TcpConnectionLayer())
    {
    }

    private CubeServerConnection(TcpConnectionLayer tcpConnectionLayer)
      : base((ISpinCredentialsProvider) CredentialProvider.gameCredentialProvider, (INetworkLayer<byte[]>) tcpConnectionLayer, (ApplicationCodec<IMessage>) CubeApplicationCodec.instance, 65536)
    {
      this.m_tcpConnectionLayer = tcpConnectionLayer;
    }

    public void CloseAbruptly() => this.m_tcpConnectionLayer.CloseAbruptly();
  }
}
