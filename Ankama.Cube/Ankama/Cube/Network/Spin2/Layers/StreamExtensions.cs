// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.Layers.StreamExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.IO;

namespace Ankama.Cube.Network.Spin2.Layers
{
  public static class StreamExtensions
  {
    public static void WriteInt(this Stream stream, int data, int bytesCount, bool bigIndian)
    {
      if (bytesCount <= 0 || bytesCount > 4)
        throw new ArgumentOutOfRangeException(nameof (bytesCount), "should be in [1, 4]");
      if ((long) data > 1L << 8 * bytesCount - 1)
        throw new ArgumentOutOfRangeException(nameof (data), "can't be encoded with " + (object) bytesCount + " Bytes");
      if (bigIndian)
      {
        for (int index = bytesCount - 1; index >= 0; --index)
        {
          int num = data >> index * 8 & (int) byte.MaxValue;
          stream.WriteByte((byte) num);
        }
      }
      else
      {
        for (int index = 0; index < bytesCount; ++index)
        {
          int num = data >> index * 8 & (int) byte.MaxValue;
          stream.WriteByte((byte) num);
        }
      }
    }

    public static int ReadInt(this Stream stream, int bytesCount, bool bigIndian)
    {
      int num = 0;
      if (bigIndian)
      {
        for (int index = bytesCount - 1; index >= 0; --index)
          num += stream.ReadByte() << index * 8;
      }
      else
      {
        for (int index = 0; index < bytesCount; ++index)
          num += (stream.ReadByte() & (int) byte.MaxValue) << index * 8;
      }
      return num;
    }
  }
}
