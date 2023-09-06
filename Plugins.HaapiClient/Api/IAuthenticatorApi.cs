// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAuthenticatorApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAuthenticatorApi
  {
    void AuthenticateToAdd(string login, string password, string lang);

    RAuthenticatorApi<AuthenticatorAccount> AuthenticateToRemove(
      string login,
      string password,
      long? otpId,
      string dataCrypted,
      string lang);

    void CheckDevice(long? deviceId, string appVersion, string lang);

    RAuthenticatorApi<AuthenticatorDevice> CreateDevice(
      string name,
      string type,
      string version,
      string appVersion,
      string uid,
      string lang);

    RAuthenticatorApi<List<AuthenticatorHelp>> GetHelpViewData(string lang);

    RAuthenticatorApi<AuthenticatorAccount> InsertOtpAccount(
      string code,
      long? deviceId,
      string lang);

    RAuthenticatorApi<AuthenticatorAccount> LoginEnabled(long? id, string dataCrypted, string lang);

    void RemoveOtpAccount(long? id, string code, long? deviceId, string lang);

    RAuthenticatorApi<AuthenticatorRestoreDevice> RestoreDevice(
      long? deleteDeviceId,
      string code,
      string lang);

    void ValidateSecret(string response, string lang);
  }
}
