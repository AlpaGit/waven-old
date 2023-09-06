// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IGameApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IGameApi
  {
    RGameApi<GameAdminRightResponse> AdminRight(long? game, long? account, long? server);

    RGameApi<GameAdminRightWithApiKeyResponse> AdminRightWithApiKey(long? game, long? server);

    void EndSession(long? sessionId, bool? subscriber, bool? closeAccountSession, DateTime? date);

    void EndSessionWithApiKey(
      long? sessionId,
      bool? subscriber,
      bool? closeAccountSession,
      DateTime? date);

    RGameApi<List<GameFriendModel>> GameFriends(long? gameId);

    void GameReward(long? account, string reward);

    void RequestBan(
      long? gameId,
      long? serverId,
      long? authorAccountId,
      long? targetAccountId,
      long? targetAccountCharacterId,
      long? sanctionId,
      long? authorAccountCharacterId,
      long? locationId,
      string requestComment);

    RGameApi<long?> SendEvent(
      long? game,
      long? sessionId,
      long? eventId,
      string data,
      DateTime? date);

    RGameApi<long?> SendEvents(long? game, long? sessionId, string events);

    RGameApi<long?> StartSession(
      long? sessionId,
      long? serverId,
      long? characterId,
      DateTime? date,
      string sessionIdString);

    RGameApi<long?> StartSessionWithApiKey(
      long? sessionId,
      long? serverId,
      long? characterId,
      DateTime? date,
      string sessionIdString);
  }
}
