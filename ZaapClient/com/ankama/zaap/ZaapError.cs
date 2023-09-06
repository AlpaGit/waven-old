// Decompiled with JetBrains decompiler
// Type: com.ankama.zaap.ZaapError
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;
using System.Text;
using Thrift;
using Thrift.Protocol;

namespace com.ankama.zaap
{
  [Serializable]
  public class ZaapError : TException, TBase, TAbstractBase
  {
    private string _details;
    public ZaapError.Isset __isset;

    public ZaapError()
    {
    }

    public ZaapError(ErrorCode code)
      : this()
    {
      this.Code = code;
    }

    public ErrorCode Code { get; set; }

    public string Details
    {
      get => this._details;
      set
      {
        this.__isset.details = true;
        this._details = value;
      }
    }

    public void Read(TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool flag = false;
        iprot.ReadStructBegin();
        while (true)
        {
          TField tfield = iprot.ReadFieldBegin();
          if (tfield.Type != TType.Stop)
          {
            switch (tfield.ID)
            {
              case 1:
                if (tfield.Type == TType.I32)
                {
                  this.Code = (ErrorCode) iprot.ReadI32();
                  flag = true;
                  break;
                }
                TProtocolUtil.Skip(iprot, tfield.Type);
                break;
              case 2:
                if (tfield.Type == TType.String)
                {
                  this.Details = iprot.ReadString();
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
        if (!flag)
          throw new TProtocolException(1);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct(nameof (ZaapError));
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        field.Name = "code";
        field.Type = TType.I32;
        field.ID = (short) 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int) this.Code);
        oprot.WriteFieldEnd();
        if (this.Details != null && this.__isset.details)
        {
          field.Name = "details";
          field.Type = TType.String;
          field.ID = (short) 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(this.Details);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder("ZaapError(");
      stringBuilder.Append(", Code: ");
      stringBuilder.Append((object) this.Code);
      if (this.Details != null && this.__isset.details)
      {
        stringBuilder.Append(", Details: ");
        stringBuilder.Append(this.Details);
      }
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    [Serializable]
    public struct Isset
    {
      public bool details;
    }
  }
}
