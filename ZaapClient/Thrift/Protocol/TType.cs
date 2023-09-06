// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TType
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public enum TType : byte
  {
    Stop = 0,
    Void = 1,
    Bool = 2,
    Byte = 3,
    Double = 4,
    I16 = 6,
    I32 = 8,
    I64 = 10, // 0x0A
    String = 11, // 0x0B
    Struct = 12, // 0x0C
    Map = 13, // 0x0D
    Set = 14, // 0x0E
    List = 15, // 0x0F
  }
}
