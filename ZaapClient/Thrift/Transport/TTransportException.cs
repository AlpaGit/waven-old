// Decompiled with JetBrains decompiler
// Type: Thrift.Transport.TTransportException
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

namespace Thrift.Transport
{
  public class TTransportException : TException
  {
    protected TTransportException.ExceptionType type;

    public TTransportException()
    {
    }

    public TTransportException(TTransportException.ExceptionType type)
      : this()
    {
      this.type = type;
    }

    public TTransportException(TTransportException.ExceptionType type, string message)
      : base(message)
    {
      this.type = type;
    }

    public TTransportException(string message)
      : base(message)
    {
    }

    public TTransportException.ExceptionType Type => this.type;

    public enum ExceptionType
    {
      Unknown,
      NotOpen,
      AlreadyOpen,
      TimedOut,
      EndOfFile,
      Interrupted,
    }
  }
}
