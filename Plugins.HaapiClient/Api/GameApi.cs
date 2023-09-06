// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.GameApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using IO.Swagger.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class GameApi : IGameApi
  {
    public GameApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public GameApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RGameApi<GameAdminRightResponse> AdminRight(long? game, long? account, long? server)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling AdminRight");
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling AdminRight");
      string path = "/Game/AdminRight".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (account.HasValue)
        formParams.Add(nameof (account), this.ApiClient.ParameterToString((object) account));
      if (server.HasValue)
        formParams.Add(nameof (server), this.ApiClient.ParameterToString((object) server));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AdminRight: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AdminRight: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameAdminRightResponse data = (GameAdminRightResponse) this.ApiClient.Deserialize(restResponse.Content, typeof (GameAdminRightResponse), restResponse.Headers);
      return new RGameApi<GameAdminRightResponse>(restResponse, data);
    }

    public RGameApi<GameAdminRightWithApiKeyResponse> AdminRightWithApiKey(long? game, long? server)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling AdminRightWithApiKey");
      string path = "/Game/AdminRightWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (server.HasValue)
        formParams.Add(nameof (server), this.ApiClient.ParameterToString((object) server));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AdminRightWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AdminRightWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameAdminRightWithApiKeyResponse data = (GameAdminRightWithApiKeyResponse) this.ApiClient.Deserialize(restResponse.Content, typeof (GameAdminRightWithApiKeyResponse), restResponse.Headers);
      return new RGameApi<GameAdminRightWithApiKeyResponse>(restResponse, data);
    }

    public void EndSession(
      long? sessionId,
      bool? subscriber,
      bool? closeAccountSession,
      DateTime? date)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling EndSession");
      string path = "/Game/EndSession".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id", (object) sessionId));
      if (subscriber.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (subscriber), (object) subscriber));
      if (closeAccountSession.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "close_account_session", (object) closeAccountSession));
      if (date.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (date), (object) date));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling EndSession: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling EndSession: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void EndSessionWithApiKey(
      long? sessionId,
      bool? subscriber,
      bool? closeAccountSession,
      DateTime? date)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling EndSessionWithApiKey");
      string path = "/Game/EndSessionWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id", (object) sessionId));
      if (subscriber.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (subscriber), (object) subscriber));
      if (closeAccountSession.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "close_account_session", (object) closeAccountSession));
      if (date.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (date), (object) date));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling EndSessionWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling EndSessionWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RGameApi<List<GameFriendModel>> GameFriends(long? gameId)
    {
      string path = "/Game/GameFriends".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GameFriends: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GameFriends: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameFriendModel> data = (List<GameFriendModel>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameFriendModel>), restResponse.Headers);
      return new RGameApi<List<GameFriendModel>>(restResponse, data);
    }

    public void GameReward(long? account, string reward)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling GameReward");
      if (reward == null)
        throw new ApiException(400, "Missing required parameter 'reward' when calling GameReward");
      string path = "/Game/GameReward".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        formParams.Add(nameof (account), this.ApiClient.ParameterToString((object) account));
      if (reward != null)
        formParams.Add(nameof (reward), this.ApiClient.ParameterToString((object) reward));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GameReward: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GameReward: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void RequestBan(
      long? gameId,
      long? serverId,
      long? authorAccountId,
      long? targetAccountId,
      long? targetAccountCharacterId,
      long? sanctionId,
      long? authorAccountCharacterId,
      long? locationId,
      string requestComment)
    {
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling RequestBan");
      if (!serverId.HasValue)
        throw new ApiException(400, "Missing required parameter 'serverId' when calling RequestBan");
      if (!authorAccountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'authorAccountId' when calling RequestBan");
      if (!targetAccountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'targetAccountId' when calling RequestBan");
      if (!targetAccountCharacterId.HasValue)
        throw new ApiException(400, "Missing required parameter 'targetAccountCharacterId' when calling RequestBan");
      if (!sanctionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sanctionId' when calling RequestBan");
      string path = "/Game/RequestBan".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (authorAccountId.HasValue)
        formParams.Add("author_account_id", this.ApiClient.ParameterToString((object) authorAccountId));
      if (authorAccountCharacterId.HasValue)
        formParams.Add("author_account_character_id", this.ApiClient.ParameterToString((object) authorAccountCharacterId));
      if (targetAccountId.HasValue)
        formParams.Add("target_account_id", this.ApiClient.ParameterToString((object) targetAccountId));
      if (targetAccountCharacterId.HasValue)
        formParams.Add("target_account_character_id", this.ApiClient.ParameterToString((object) targetAccountCharacterId));
      if (locationId.HasValue)
        formParams.Add("location_id", this.ApiClient.ParameterToString((object) locationId));
      if (sanctionId.HasValue)
        formParams.Add("sanction_id", this.ApiClient.ParameterToString((object) sanctionId));
      if (requestComment != null)
        formParams.Add("request_comment", this.ApiClient.ParameterToString((object) requestComment));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RequestBan: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RequestBan: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RGameApi<long?> SendEvent(
      long? game,
      long? sessionId,
      long? eventId,
      string data,
      DateTime? date)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SendEvent");
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SendEvent");
      if (!eventId.HasValue)
        throw new ApiException(400, "Missing required parameter 'eventId' when calling SendEvent");
      if (data == null)
        throw new ApiException(400, "Missing required parameter 'data' when calling SendEvent");
      string path = "/Game/SendEvent".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (eventId.HasValue)
        formParams.Add("event_id", this.ApiClient.ParameterToString((object) eventId));
      if (data != null)
        formParams.Add(nameof (data), this.ApiClient.ParameterToString((object) data));
      if (date.HasValue)
        formParams.Add(nameof (date), this.ApiClient.ParameterToString((object) date));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendEvent: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendEvent: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data1 = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RGameApi<long?>(restResponse, data1);
    }

    public RGameApi<long?> SendEvents(long? game, long? sessionId, string events)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SendEvents");
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SendEvents");
      if (events == null)
        throw new ApiException(400, "Missing required parameter 'events' when calling SendEvents");
      string path = "/Game/SendEvents".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (events != null)
        formParams.Add(nameof (events), this.ApiClient.ParameterToString((object) events));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendEvents: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendEvents: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RGameApi<long?>(restResponse, data);
    }

    public RGameApi<long?> StartSession(
      long? sessionId,
      long? serverId,
      long? characterId,
      DateTime? date,
      string sessionIdString)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling StartSession");
      string path = "/Game/StartSession".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id", (object) sessionId));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (characterId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "character_id", (object) characterId));
      if (date.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (date), (object) date));
      if (sessionIdString != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id_string", (object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSession: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSession: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RGameApi<long?>(restResponse, data);
    }

    public RGameApi<long?> StartSessionWithApiKey(
      long? sessionId,
      long? serverId,
      long? characterId,
      DateTime? date,
      string sessionIdString)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling StartSessionWithApiKey");
      string path = "/Game/StartSessionWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id", (object) sessionId));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (characterId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "character_id", (object) characterId));
      if (date.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (date), (object) date));
      if (sessionIdString != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "session_id_string", (object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSessionWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSessionWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RGameApi<long?>(restResponse, data);
    }
  }
}
