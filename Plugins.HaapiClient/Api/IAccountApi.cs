// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAccountApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAccountApi
  {
    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> Account();

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountByNicknameWithPassword(
      string nickname);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountFromToken(
      string token,
      string ip,
      long? gameId);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountWithPassword(long? accountId);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Avatar> Avatar();

    RAccountApi<string> CheckPasswordStrength(string password);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGhost(
      long? game,
      string lang,
      string uid,
      string ip);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGhostFromClient(
      long? game,
      string lang,
      string uid,
      string ip);

    RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGuest(
      long? game,
      string lang,
      string webParams,
      string captchaToken);

    RAccountApi<Token> CreateToken(long? game);

    RAccountApi<Token> CreateTokenFromServer(long? accountId, long? game, string ip, string meta);

    RAccountApi<Token> CreateTokenWithPassword(string login, string password, long? game);

    void DeleteGhost(long? game, string uid, string newUid);

    RAccountApi<SessionAccountsPaged> GetSessionAccounts(
      long? accountId,
      long? gameId,
      long? page,
      long? pageSize,
      string order,
      string groupBy,
      string ip,
      long? serverId,
      string minDate,
      string maxDate,
      string minIp,
      string maxIp);

    void SendDeviceInfos(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      string sessionIdString);

    void SendDeviceInfosWithPassword(
      long? accountId,
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      string sessionIdString);

    void SetNickname(long? accountId, string nickname, string lang);

    void SetNicknameWithApiKey(string nickname, string lang);

    RAccountApi<bool?> SetStatus(long? accountId, string statusKey, long? statusValue);

    void SignOff(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      DateTime? date,
      string sessionIdString);

    void SignOffWithApiKey(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      DateTime? date,
      string sessionIdString);

    RAccountApi<SessionLogin> SignOnWithApiKey(long? game);

    RAccountApi<SessionLogin> SignOnWithGhostUid(
      string uid,
      long? game,
      string ip,
      DateTime? date,
      string os,
      string device,
      string connectionType,
      string clientType,
      string partner,
      string deviceUid);

    RAccountApi<SessionLogin> SignOnWithPassword(
      string login,
      string password,
      string ip,
      long? game);

    RAccountApi<SessionLogin> SignOnWithToken(string token, string ip, long? game);

    RAccountApi<SessionAccount> StartSession(
      long? accountId,
      long? sessionId,
      long? game,
      string ip,
      string sessionIdString);

    RAccountApi<List<AccountPublicStatus>> Status();

    RAccountApi<List<AccountStatus>> StatusByAccountId(long? accountId);

    void ValidateGuest(
      string lang,
      long? accountId,
      long? gameId,
      string login,
      string nickname,
      string email,
      string password);

    void ValidateSteam(
      string lang,
      long? accountId,
      long? gameId,
      string login,
      string nickname,
      string email,
      string password);
  }
}
