// Decompiled with JetBrains decompiler
// Type: Thrift.TException
// Assembly: ZaapClient, Version=0.10.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 113F5C12-4234-4D75-AD55-E1FF9BE3B8DC
// Assembly location: E:\WAVEN\Waven_Data\Managed\ZaapClient.dll

using System;

namespace Thrift
{
  public class TException : Exception
  {
    public TException()
    {
    }

    public TException(string message)
      : base(message)
    {
    }
  }
}
