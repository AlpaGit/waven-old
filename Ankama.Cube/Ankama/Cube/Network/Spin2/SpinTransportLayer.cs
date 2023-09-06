// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.SpinTransportLayer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2.Layers;
using System;
using UnityEngine;

namespace Ankama.Cube.Network.Spin2
{
  public class SpinTransportLayer : TransformLayer<SpinProtocol.Message, byte[]>
  {
    public SpinTransportLayer(INetworkLayer<byte[]> child)
      : base(child)
    {
    }

    public override bool Write(SpinProtocol.Message input) => this.child.Write(input.Serialize());

    protected override void OnDataReceived(byte[] data)
    {
      switch (data[0])
      {
        case 0:
          int count1 = data.Length - 1;
          byte[] numArray1 = new byte[count1];
          Buffer.BlockCopy((Array) data, 1, (Array) numArray1, 0, count1);
          this.OnData((SpinProtocol.Message) new SpinProtocol.RawApplicationMessage(numArray1));
          break;
        case 1:
          int count2 = data.Length - 1;
          byte[] numArray2 = new byte[count2];
          Buffer.BlockCopy((Array) data, 1, (Array) numArray2, 0, count2);
          this.OnData((SpinProtocol.Message) new SpinProtocol.PingMessage(numArray2));
          break;
        case 2:
          int count3 = data.Length - 1;
          byte[] numArray3 = new byte[count3];
          Buffer.BlockCopy((Array) data, 1, (Array) numArray3, 0, count3);
          this.OnData((SpinProtocol.Message) new SpinProtocol.PongMessage(numArray3));
          break;
        case 3:
          this.OnData((SpinProtocol.Message) SpinProtocol.HeartbeatMessage.instance);
          break;
        default:
          Debug.LogWarning((object) string.Format("Unknownspin message type: {0}", (object) data[0]));
          break;
      }
    }
  }
}
