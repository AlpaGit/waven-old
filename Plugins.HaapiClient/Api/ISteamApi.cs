// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ISteamApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface ISteamApi
  {
    RSteamApi<SessionLogin> AccountGhostCreate(string sessionTicket, long? gameId, string lang);

    RSteamApi<SessionLogin> AccountLink(
      string sessionTicket,
      long? gameId,
      string login,
      string password);

    RSteamApi<SessionLogin> AccountLogin(string sessionTicket, long? gameId);

    RSteamApi<ApiKey> ApiKeyGhostCreate(string sessionTicket, long? gameId, string lang);

    RSteamApi<ApiKey> ApiKeyLink(
      string sessionTicket,
      long? gameId,
      string login,
      string password);

    RSteamApi<ApiKey> ApiKeyLogin(string sessionTicket, long? gameId);

    RSteamApi<List<SteamAccountMeta>> AppOwnership(string steamUid, long? gameId);

    RSteamApi<bool?> SetAchievement(long? steamUid, long? gameId, string achievements);
  }
}
