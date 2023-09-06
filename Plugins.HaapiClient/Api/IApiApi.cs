// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IApiApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IApiApi
  {
    RApiApi<List<ApiTransactionReturn>> CommitTransaction(long? transactionId);

    RApiApi<ApiKey> CreateApiKey(
      string login,
      string password,
      bool? longLifeToken,
      List<string> meta,
      string shopKey,
      string lang,
      long? characterId,
      long? serverId,
      string country,
      string currency,
      string paymentMode,
      string partnerId);

    RApiApi<ApiKey> CreateApiKeyFromServer(long? accountId, string ip, List<string> meta);

    void DeleteApiKey();

    RApiApi<bool?> DeleteApiKeyByAccountId(long? accountId, string apikey);

    RApiApi<List<ApiKey>> GetApiKeysByAccountId(long? accountId);

    RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthAccount> OAuthAccount(
      string clientId,
      string accountId,
      string ip);

    RApiApi<Account> OAuthMe(string accessToken);

    RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthProvider> OAuthProvider(
      string clientId,
      string secret);

    RApiApi<OAuthKey> OAuthToken(
      string clientId,
      string clientSecret,
      string grantType,
      string redirectUri,
      string code,
      string refreshToken,
      string accessType);

    RApiApi<ApiKey> RefreshApiKey(string refreshToken, bool? longLifeToken);

    void StartTransaction();
  }
}
