﻿// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TBase64Utils
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  internal static class TBase64Utils
  {
    internal const string ENCODE_TABLE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    private static int[] DECODE_TABLE = new int[256]
    {
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      62,
      -1,
      -1,
      -1,
      63,
      52,
      53,
      54,
      55,
      56,
      57,
      58,
      59,
      60,
      61,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      12,
      13,
      14,
      15,
      16,
      17,
      18,
      19,
      20,
      21,
      22,
      23,
      24,
      25,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      26,
      27,
      28,
      29,
      30,
      31,
      32,
      33,
      34,
      35,
      36,
      37,
      38,
      39,
      40,
      41,
      42,
      43,
      44,
      45,
      46,
      47,
      48,
      49,
      50,
      51,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1,
      -1
    };

    internal static void encode(byte[] src, int srcOff, int len, byte[] dst, int dstOff)
    {
      dst[dstOff] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff] >> 2 & 63];
      if (len == 3)
      {
        dst[dstOff + 1] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff] << 4 & 48 | (int) src[srcOff + 1] >> 4 & 15];
        dst[dstOff + 2] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff + 1] << 2 & 60 | (int) src[srcOff + 2] >> 6 & 3];
        dst[dstOff + 3] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff + 2] & 63];
      }
      else if (len == 2)
      {
        dst[dstOff + 1] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff] << 4 & 48 | (int) src[srcOff + 1] >> 4 & 15];
        dst[dstOff + 2] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff + 1] << 2 & 60];
      }
      else
        dst[dstOff + 1] = (byte) "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int) src[srcOff] << 4 & 48];
    }

    internal static void decode(byte[] src, int srcOff, int len, byte[] dst, int dstOff)
    {
      dst[dstOff] = (byte) (TBase64Utils.DECODE_TABLE[(int) src[srcOff] & (int) byte.MaxValue] << 2 | TBase64Utils.DECODE_TABLE[(int) src[srcOff + 1] & (int) byte.MaxValue] >> 4);
      if (len <= 2)
        return;
      dst[dstOff + 1] = (byte) (TBase64Utils.DECODE_TABLE[(int) src[srcOff + 1] & (int) byte.MaxValue] << 4 & 240 | TBase64Utils.DECODE_TABLE[(int) src[srcOff + 2] & (int) byte.MaxValue] >> 2);
      if (len <= 3)
        return;
      dst[dstOff + 2] = (byte) (TBase64Utils.DECODE_TABLE[(int) src[srcOff + 2] & (int) byte.MaxValue] << 6 & 192 | TBase64Utils.DECODE_TABLE[(int) src[srcOff + 3] & (int) byte.MaxValue]);
    }
  }
}
