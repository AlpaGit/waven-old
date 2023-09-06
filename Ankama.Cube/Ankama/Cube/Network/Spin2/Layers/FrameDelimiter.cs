// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.FrameDelimiter
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public class FrameDelimiter : TransformLayer<byte[], byte[]>
  {
    private readonly ByteBuffer m_receiveBuffer;
    private readonly ByteBuffer m_sendBuffer;
    private readonly int m_headerSize;
    private readonly int m_maximumMessageSize;
    private readonly bool m_includeHeaderSize;

    public FrameDelimiter(
      INetworkLayer<byte[]> child,
      int maximumMessageSize,
      int headerSize,
      bool includeHeaderSize,
      int receiveBufferSize = 8092,
      bool bigIndian = true)
      : base(child)
    {
      if (headerSize <= 0 || headerSize > 4)
        throw new ArgumentOutOfRangeException(nameof (headerSize), string.Format("HeaderSize should be between 1 and 4. Found {0}", (object) headerSize));
      int num = headerSize == 4 ? int.MaxValue : 1 << 8 * headerSize;
      this.m_maximumMessageSize = maximumMessageSize < num ? maximumMessageSize : throw new ArgumentOutOfRangeException(nameof (maximumMessageSize), string.Format("cannot encode {0} with a headerSize of {1}. {2} should be < {3}", (object) nameof (maximumMessageSize), (object) headerSize, (object) nameof (maximumMessageSize), (object) num));
      this.m_includeHeaderSize = includeHeaderSize;
      this.m_headerSize = headerSize;
      this.m_receiveBuffer = new ByteBuffer(receiveBufferSize, bigIndian);
      this.m_sendBuffer = new ByteBuffer(1024, bigIndian);
    }

    public override bool Write(byte[] input)
    {
      int length = input.Length;
      if (length == 0)
      {
        Debug.LogWarning((object) "FrameDelimiter: unable to write an empty byte array");
        return false;
      }
      ByteBuffer sendBuffer = this.m_sendBuffer;
      sendBuffer.Clear();
      sendBuffer.WriteInt(this.m_includeHeaderSize ? length + this.m_headerSize : length, this.m_headerSize);
      sendBuffer.Write(input);
      return this.child.Write(sendBuffer.ReadAll());
    }

    protected override void OnDataReceived(byte[] data)
    {
      ByteBuffer receiveBuffer = this.m_receiveBuffer;
      receiveBuffer.Write(data);
      while (receiveBuffer.remaining >= this.m_headerSize)
      {
        int num1 = receiveBuffer.PeekInt(this.m_headerSize);
        if (num1 < 0 || num1 > this.m_maximumMessageSize)
          throw new ArgumentException(string.Format("Frame too large received: frame size is {0} but maximumMessageSize is {1}", (object) num1, (object) this.m_maximumMessageSize));
        int num2 = num1 + (this.m_includeHeaderSize ? 0 : this.m_headerSize);
        if (receiveBuffer.remaining < num2)
          break;
        receiveBuffer.Skip(this.m_headerSize);
        int length = num2 - this.m_headerSize;
        this.OnData(receiveBuffer.ReadBytes(length));
        receiveBuffer.CompactIfNeeded();
      }
    }
  }
}
