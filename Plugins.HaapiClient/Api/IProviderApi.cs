// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IProviderApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IProviderApi
  {
    RProviderApi<SessionLogin> AccountGhostCreate(
      string provider,
      string uId,
      long? gameId,
      string lang,
      string data,
      string email,
      bool? isValidEmail,
      string validationEmailLink);

    RProviderApi<SessionLogin> AccountLink(
      string provider,
      string uId,
      long? gameId,
      string login,
      string password,
      bool? passwordEncoded,
      string data);

    RProviderApi<SessionLogin> AccountLogin(string provider, string uId, long? gameId);

    RProviderApi<ApiKey> ApiKeyGhostCreate(
      string provider,
      string accessToken,
      long? gameId,
      string lang,
      string data);

    RProviderApi<ApiKey> ApiKeyLink(
      string provider,
      string accessToken,
      long? gameId,
      string login,
      string password,
      string data);

    RProviderApi<ApiKey> ApiKeyLogin(string provider, string accessToken, long? gameId);
  }
}
