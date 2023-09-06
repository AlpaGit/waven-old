// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.HaapiHelper
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2;
using Com.Ankama.Haapi.Swagger.Model;
using System;

namespace Ankama.Cube.Configuration
{
  public static class HaapiHelper
  {
    public static SpinConnectionError From(ErrorAccountLogin error)
    {
      ErrorAccountLoginCode result;
      if (!Enum.TryParse<ErrorAccountLoginCode>(error.Reason, out result))
        return (SpinConnectionError) null;
      switch (result)
      {
        case ErrorAccountLoginCode.BAN:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.AccountKnonwButBanned);
        case ErrorAccountLoginCode.BLACKLIST:
        case ErrorAccountLoginCode.BRUTEFORCE:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.IpAddressRefused);
        case ErrorAccountLoginCode.LOCKED:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.AccountKnonwButBlocked);
        case ErrorAccountLoginCode.DELETED:
        case ErrorAccountLoginCode.NOACCOUNT:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.NoneOrOtherOrUnknown);
        case ErrorAccountLoginCode.RESETANKAMA:
        case ErrorAccountLoginCode.PARTNER:
        case ErrorAccountLoginCode.MAILNOVALID:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.NoneOrOtherOrUnknown);
        case ErrorAccountLoginCode.OTPTIMEFAILED:
        case ErrorAccountLoginCode.SECURITYCARD:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.InvalidAuthenticationInfo);
        case ErrorAccountLoginCode.FAILED:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.BadCredentials);
        case ErrorAccountLoginCode.BETACLOSED:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.BetaAccessRequired);
        case ErrorAccountLoginCode.ACCOUNT_LINKED:
        case ErrorAccountLoginCode.ACCOUNT_INVALID:
        case ErrorAccountLoginCode.ACCOUNT_SHIELDED:
        case ErrorAccountLoginCode.ACCOUNT_NO_CERTIFY:
          return new SpinConnectionError(SpinProtocol.ConnectionErrors.NoneOrOtherOrUnknown);
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
