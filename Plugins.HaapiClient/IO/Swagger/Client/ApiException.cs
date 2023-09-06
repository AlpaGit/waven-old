// Decompiled with JetBrains decompiler
// Type: IO.Swagger.Client.ApiException
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using System;

namespace IO.Swagger.Client
{
  public class ApiException : Exception
  {
    public int ErrorCode { get; set; }

    public object ErrorContent { get; private set; }

    public ApiException()
    {
    }

    public ApiException(int errorCode, string message)
      : base(message)
    {
      this.ErrorCode = errorCode;
    }

    public ApiException(int errorCode, string message, object errorContent = null)
      : base(message)
    {
      this.ErrorCode = errorCode;
      this.ErrorContent = errorContent;
    }
  }
}
