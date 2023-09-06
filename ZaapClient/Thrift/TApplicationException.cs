// Decompiled with JetBrains decompiler
// Type: Thrift.TApplicationException
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using Thrift.Protocol;

namespace Thrift
{
  public class TApplicationException : TException
  {
    protected TApplicationException.ExceptionType type;

    public TApplicationException()
    {
    }

    public TApplicationException(TApplicationException.ExceptionType type) => this.type = type;

    public TApplicationException(TApplicationException.ExceptionType type, string message)
      : base(message)
    {
      this.type = type;
    }

    public static TApplicationException Read(TProtocol iprot)
    {
      string message = (string) null;
      TApplicationException.ExceptionType type = TApplicationException.ExceptionType.Unknown;
      iprot.ReadStructBegin();
      while (true)
      {
        TField tfield = iprot.ReadFieldBegin();
        if (tfield.Type != TType.Stop)
        {
          switch (tfield.ID)
          {
            case 1:
              if (tfield.Type == TType.String)
              {
                message = iprot.ReadString();
                break;
              }
              TProtocolUtil.Skip(iprot, tfield.Type);
              break;
            case 2:
              if (tfield.Type == TType.I32)
              {
                type = (TApplicationException.ExceptionType) iprot.ReadI32();
                break;
              }
              TProtocolUtil.Skip(iprot, tfield.Type);
              break;
            default:
              TProtocolUtil.Skip(iprot, tfield.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        else
          break;
      }
      iprot.ReadStructEnd();
      return new TApplicationException(type, message);
    }

    public void Write(TProtocol oprot)
    {
      TStruct struc = new TStruct(nameof (TApplicationException));
      TField field = new TField();
      oprot.WriteStructBegin(struc);
      if (!string.IsNullOrEmpty(this.Message))
      {
        field.Name = "message";
        field.Type = TType.String;
        field.ID = (short) 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(this.Message);
        oprot.WriteFieldEnd();
      }
      field.Name = "type";
      field.Type = TType.I32;
      field.ID = (short) 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteI32((int) this.type);
      oprot.WriteFieldEnd();
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public enum ExceptionType
    {
      Unknown,
      UnknownMethod,
      InvalidMessageType,
      WrongMethodName,
      BadSequenceID,
      MissingResult,
      InternalError,
      ProtocolError,
      InvalidTransform,
      InvalidProtocol,
      UnsupportedClientType,
    }
  }
}
