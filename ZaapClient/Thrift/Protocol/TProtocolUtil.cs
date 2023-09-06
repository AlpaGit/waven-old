// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TProtocolUtil
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public static class TProtocolUtil
  {
    public static void Skip(TProtocol prot, TType type)
    {
      prot.IncrementRecursionDepth();
      try
      {
        switch (type)
        {
          case TType.Bool:
            prot.ReadBool();
            break;
          case TType.Byte:
            int num1 = (int) prot.ReadByte();
            break;
          case TType.Double:
            prot.ReadDouble();
            break;
          case TType.I16:
            int num2 = (int) prot.ReadI16();
            break;
          case TType.I32:
            prot.ReadI32();
            break;
          case TType.I64:
            prot.ReadI64();
            break;
          case TType.String:
            prot.ReadBinary();
            break;
          case TType.Struct:
            prot.ReadStructBegin();
            while (true)
            {
              TField tfield = prot.ReadFieldBegin();
              if (tfield.Type != TType.Stop)
              {
                TProtocolUtil.Skip(prot, tfield.Type);
                prot.ReadFieldEnd();
              }
              else
                break;
            }
            prot.ReadStructEnd();
            break;
          case TType.Map:
            TMap tmap = prot.ReadMapBegin();
            for (int index = 0; index < tmap.Count; ++index)
            {
              TProtocolUtil.Skip(prot, tmap.KeyType);
              TProtocolUtil.Skip(prot, tmap.ValueType);
            }
            prot.ReadMapEnd();
            break;
          case TType.Set:
            TSet tset = prot.ReadSetBegin();
            for (int index = 0; index < tset.Count; ++index)
              TProtocolUtil.Skip(prot, tset.ElementType);
            prot.ReadSetEnd();
            break;
          case TType.List:
            TList tlist = prot.ReadListBegin();
            for (int index = 0; index < tlist.Count; ++index)
              TProtocolUtil.Skip(prot, tlist.ElementType);
            prot.ReadListEnd();
            break;
        }
      }
      finally
      {
        prot.DecrementRecursionDepth();
      }
    }
  }
}
