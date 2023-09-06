// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.ByteBuffer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.IO;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public class ByteBuffer
  {
    private readonly MemoryStream m_stream;
    private readonly bool m_bigIndian;
    private int m_readPosition;

    public ByteBuffer(int capacity = 8092, bool bigIndian = true)
    {
      this.m_stream = new MemoryStream(capacity);
      this.m_bigIndian = bigIndian;
    }

    public bool isEmpty => this.m_stream.Length <= (long) this.m_readPosition;

    public int remaining => (int) this.m_stream.Length - this.m_readPosition;

    private void PrepareForRead() => this.m_stream.Position = (long) this.m_readPosition;

    private void PrepareForWrite() => this.m_stream.Position = this.m_stream.Length;

    public void Write(byte[] buffer)
    {
      this.PrepareForWrite();
      this.Write(buffer, 0, buffer.Length);
    }

    public void Write(byte[] buffer, int offset, int dataLength)
    {
      this.PrepareForWrite();
      this.m_stream.Position = this.m_stream.Length;
      this.m_stream.Write(buffer, offset, dataLength);
    }

    public void WriteInt(int data, int bytesCount)
    {
      this.PrepareForWrite();
      this.m_stream.Position = this.m_stream.Length;
      this.m_stream.WriteInt(data, bytesCount, this.m_bigIndian);
    }

    public int ReadInt(int bytesCount)
    {
      int num = this.PeekInt(bytesCount);
      this.m_readPosition += bytesCount;
      return num;
    }

    public int PeekInt(int bytesCount)
    {
      this.PrepareForRead();
      if ((long) (this.m_readPosition + bytesCount) > this.m_stream.Length)
        throw new IndexOutOfRangeException(string.Format("Trying to read too many Bytes. end({0}+{1}) > Length({2})", (object) this.m_readPosition, (object) bytesCount, (object) this.m_stream.Length));
      return this.m_stream.ReadInt(bytesCount, this.m_bigIndian);
    }

    public byte[] ReadAll() => this.ReadBytes(this.remaining);

    public byte[] ReadBytes(int length)
    {
      byte[] numArray = this.PeekBytes(length);
      this.m_readPosition += length;
      return numArray;
    }

    public byte[] PeekBytes(int length)
    {
      this.PrepareForRead();
      if ((long) (this.m_readPosition + length) > this.m_stream.Length)
        throw new IndexOutOfRangeException(string.Format("Trying to read too many Bytes. end({0}+{1}) > Length({2})", (object) this.m_readPosition, (object) length, (object) this.m_stream.Length));
      byte[] buffer = new byte[length];
      this.m_stream.Position = (long) this.m_readPosition;
      this.m_stream.Read(buffer, 0, length);
      return buffer;
    }

    public void Skip(int bytesCount)
    {
      int num = this.m_readPosition + bytesCount;
      if ((long) num > this.m_stream.Length)
        throw new IndexOutOfRangeException(string.Format("Can't skip {0} bytes: readPosition is {1} and length is {2}", (object) bytesCount, (object) this.m_readPosition, (object) this.m_stream.Length));
      this.m_readPosition = num;
    }

    public void Clear()
    {
      this.m_readPosition = 0;
      this.m_stream.SetLength(0L);
    }

    public void CompactIfNeeded()
    {
      if (this.m_readPosition == 0)
        return;
      int remaining = this.remaining;
      if (remaining == 0)
        this.Clear();
      if (this.m_readPosition <= this.m_stream.Capacity / 4)
        return;
      byte[] buffer = this.m_stream.GetBuffer();
      Buffer.BlockCopy((Array) buffer, this.m_readPosition, (Array) buffer, 0, remaining);
      this.m_readPosition = 0;
      this.m_stream.SetLength((long) remaining);
    }

    public void Compact()
    {
      if (this.m_readPosition == 0)
        return;
      int remaining = this.remaining;
      if (remaining == 0)
        this.Clear();
      byte[] buffer = this.m_stream.GetBuffer();
      Buffer.BlockCopy((Array) buffer, this.m_readPosition, (Array) buffer, 0, remaining);
      this.m_readPosition = 0;
      this.m_stream.SetLength((long) remaining);
    }
  }
}
