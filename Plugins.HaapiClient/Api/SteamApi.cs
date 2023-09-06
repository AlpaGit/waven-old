// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.SteamApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using IO.Swagger.Client;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class SteamApi : ISteamApi
  {
    public SteamApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public SteamApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RSteamApi<SessionLogin> AccountGhostCreate(
      string sessionTicket,
      long? gameId,
      string lang)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling AccountGhostCreate");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountGhostCreate");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling AccountGhostCreate");
      string path = "/Steam/AccountGhostCreate".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountGhostCreate: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountGhostCreate: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RSteamApi<SessionLogin>(restResponse, data);
    }

    public RSteamApi<SessionLogin> AccountLink(
      string sessionTicket,
      long? gameId,
      string login,
      string password)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling AccountLink");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountLink");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling AccountLink");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling AccountLink");
      string path = "/Steam/AccountLink".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLink: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLink: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RSteamApi<SessionLogin>(restResponse, data);
    }

    public RSteamApi<SessionLogin> AccountLogin(string sessionTicket, long? gameId)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling AccountLogin");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountLogin");
      string path = "/Steam/AccountLogin".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLogin: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLogin: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RSteamApi<SessionLogin>(restResponse, data);
    }

    public RSteamApi<ApiKey> ApiKeyGhostCreate(string sessionTicket, long? gameId, string lang)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling ApiKeyGhostCreate");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyGhostCreate");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ApiKeyGhostCreate");
      string path = "/Steam/ApiKeyGhostCreate".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyGhostCreate: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyGhostCreate: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RSteamApi<ApiKey>(restResponse, data);
    }

    public RSteamApi<ApiKey> ApiKeyLink(
      string sessionTicket,
      long? gameId,
      string login,
      string password)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling ApiKeyLink");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyLink");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling ApiKeyLink");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling ApiKeyLink");
      string path = "/Steam/ApiKeyLink".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLink: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLink: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RSteamApi<ApiKey>(restResponse, data);
    }

    public RSteamApi<ApiKey> ApiKeyLogin(string sessionTicket, long? gameId)
    {
      if (sessionTicket == null)
        throw new ApiException(400, "Missing required parameter 'sessionTicket' when calling ApiKeyLogin");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyLogin");
      string path = "/Steam/ApiKeyLogin".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionTicket != null)
        formParams.Add("session_ticket", this.ApiClient.ParameterToString((object) sessionTicket));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLogin: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLogin: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RSteamApi<ApiKey>(restResponse, data);
    }

    public RSteamApi<List<SteamAccountMeta>> AppOwnership(string steamUid, long? gameId)
    {
      if (steamUid == null)
        throw new ApiException(400, "Missing required parameter 'steamUid' when calling AppOwnership");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AppOwnership");
      string path = "/Steam/AppOwnership".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (steamUid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "steam_uid", (object) steamUid));
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "game_id", (object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AppOwnership: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AppOwnership: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<SteamAccountMeta> data = (List<SteamAccountMeta>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<SteamAccountMeta>), restResponse.Headers);
      return new RSteamApi<List<SteamAccountMeta>>(restResponse, data);
    }

    public RSteamApi<bool?> SetAchievement(long? steamUid, long? gameId, string achievements)
    {
      if (!steamUid.HasValue)
        throw new ApiException(400, "Missing required parameter 'steamUid' when calling SetAchievement");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling SetAchievement");
      if (achievements == null)
        throw new ApiException(400, "Missing required parameter 'achievements' when calling SetAchievement");
      string path = "/Steam/SetAchievement".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (steamUid.HasValue)
        formParams.Add("steam_uid", this.ApiClient.ParameterToString((object) steamUid));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (achievements != null)
        formParams.Add(nameof (achievements), this.ApiClient.ParameterToString((object) achievements));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetAchievement: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetAchievement: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      bool? data = (bool?) this.ApiClient.Deserialize(restResponse.Content, typeof (bool?), restResponse.Headers);
      return new RSteamApi<bool?>(restResponse, data);
    }
  }
}
