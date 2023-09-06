// Decompiled with JetBrains decompiler
// Type: Thrift.Protocol.TProtocolException
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Protocol
{
  public class TProtocolException : TException
  {
    public const int UNKNOWN = 0;
    public const int INVALID_DATA = 1;
    public const int NEGATIVE_SIZE = 2;
    public const int SIZE_LIMIT = 3;
    public const int BAD_VERSION = 4;
    public const int NOT_IMPLEMENTED = 5;
    public const int DEPTH_LIMIT = 6;
    protected int type_;

    public TProtocolException()
    {
    }

    public TProtocolException(int type) => this.type_ = type;

    public TProtocolException(int type, string message)
      : base(message)
    {
      this.type_ = type;
    }

    public TProtocolException(string message)
      : base(message)
    {
    }

    public int getType() => this.type_;
  }
}
