// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AccountApi
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
  public class AccountApi : IAccountApi
  {
    public AccountApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AccountApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> Account()
    {
      string path = "/Account/Account".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Account: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Account: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountByNicknameWithPassword(
      string nickname)
    {
      if (nickname == null)
        throw new ApiException(400, "Missing required parameter 'nickname' when calling AccountByNicknameWithPassword");
      string path = "/Account/AccountByNicknameWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (nickname != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (nickname), (object) nickname));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountByNicknameWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountByNicknameWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountFromToken(
      string token,
      string ip,
      long? gameId)
    {
      if (token == null)
        throw new ApiException(400, "Missing required parameter 'token' when calling AccountFromToken");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling AccountFromToken");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountFromToken");
      string path = "/Account/AccountFromToken".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (token != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (token), (object) token));
      if (ip != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (ip), (object) ip));
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "game_id", (object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountFromToken: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountFromToken: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> AccountWithPassword(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling AccountWithPassword");
      string path = "/Account/AccountWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Avatar> Avatar()
    {
      string path = "/Account/Avatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Avatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Avatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Avatar data = (Com.Ankama.Haapi.Swagger.Model.Avatar) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Avatar), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Avatar>(restResponse, data);
    }

    public RAccountApi<string> CheckPasswordStrength(string password)
    {
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling CheckPasswordStrength");
      string path = "/Account/CheckPasswordStrength".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (password != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (password), (object) password));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CheckPasswordStrength: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CheckPasswordStrength: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      string data = (string) this.ApiClient.Deserialize(restResponse.Content, typeof (string), restResponse.Headers);
      return new RAccountApi<string>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGhost(
      long? game,
      string lang,
      string uid,
      string ip)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateGhost");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling CreateGhost");
      if (uid == null)
        throw new ApiException(400, "Missing required parameter 'uid' when calling CreateGhost");
      string path = "/Account/CreateGhost".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      if (ip != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (ip), (object) ip));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGhost: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGhost: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGhostFromClient(
      long? game,
      string lang,
      string uid,
      string ip)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateGhostFromClient");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling CreateGhostFromClient");
      if (uid == null)
        throw new ApiException(400, "Missing required parameter 'uid' when calling CreateGhostFromClient");
      string path = "/Account/CreateGhostFromClient".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      if (ip != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (ip), (object) ip));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGhostFromClient: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGhostFromClient: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account> CreateGuest(
      long? game,
      string lang,
      string webParams,
      string captchaToken)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateGuest");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling CreateGuest");
      string path = "/Account/CreateGuest".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (webParams != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "web_params", (object) webParams));
      if (captchaToken != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "captcha_token", (object) captchaToken));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGuest: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateGuest: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.Account data = (Com.Ankama.Haapi.Swagger.Model.Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.Account), restResponse.Headers);
      return new RAccountApi<Com.Ankama.Haapi.Swagger.Model.Account>(restResponse, data);
    }

    public RAccountApi<Token> CreateToken(long? game)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateToken");
      string path = "/Account/CreateToken".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateToken: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateToken: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Token data = (Token) this.ApiClient.Deserialize(restResponse.Content, typeof (Token), restResponse.Headers);
      return new RAccountApi<Token>(restResponse, data);
    }

    public RAccountApi<Token> CreateTokenFromServer(
      long? accountId,
      long? game,
      string ip,
      string meta)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling CreateTokenFromServer");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateTokenFromServer");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling CreateTokenFromServer");
      string path = "/Account/CreateTokenFromServer".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (ip != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (ip), (object) ip));
      if (meta != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (meta), (object) meta));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateTokenFromServer: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateTokenFromServer: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Token data = (Token) this.ApiClient.Deserialize(restResponse.Content, typeof (Token), restResponse.Headers);
      return new RAccountApi<Token>(restResponse, data);
    }

    public RAccountApi<Token> CreateTokenWithPassword(string login, string password, long? game)
    {
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling CreateTokenWithPassword");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling CreateTokenWithPassword");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CreateTokenWithPassword");
      string path = "/Account/CreateTokenWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateTokenWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateTokenWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Token data = (Token) this.ApiClient.Deserialize(restResponse.Content, typeof (Token), restResponse.Headers);
      return new RAccountApi<Token>(restResponse, data);
    }

    public void DeleteGhost(long? game, string uid, string newUid)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling DeleteGhost");
      if (uid == null)
        throw new ApiException(400, "Missing required parameter 'uid' when calling DeleteGhost");
      if (newUid == null)
        throw new ApiException(400, "Missing required parameter 'newUid' when calling DeleteGhost");
      string path = "/Account/DeleteGhost".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      if (newUid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "new_uid", (object) newUid));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGhost: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGhost: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAccountApi<SessionAccountsPaged> GetSessionAccounts(
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
      string maxIp)
    {
      string path = "/Account/GetSessionAccounts".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (page.HasValue)
        formParams.Add(nameof (page), this.ApiClient.ParameterToString((object) page));
      if (pageSize.HasValue)
        formParams.Add("page_size", this.ApiClient.ParameterToString((object) pageSize));
      if (order != null)
        formParams.Add(nameof (order), this.ApiClient.ParameterToString((object) order));
      if (groupBy != null)
        formParams.Add("group_by", this.ApiClient.ParameterToString((object) groupBy));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (minDate != null)
        formParams.Add("min_date", this.ApiClient.ParameterToString((object) minDate));
      if (maxDate != null)
        formParams.Add("max_date", this.ApiClient.ParameterToString((object) maxDate));
      if (minIp != null)
        formParams.Add("min_ip", this.ApiClient.ParameterToString((object) minIp));
      if (maxIp != null)
        formParams.Add("max_ip", this.ApiClient.ParameterToString((object) maxIp));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetSessionAccounts: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetSessionAccounts: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionAccountsPaged data = (SessionAccountsPaged) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionAccountsPaged), restResponse.Headers);
      return new RAccountApi<SessionAccountsPaged>(restResponse, data);
    }

    public void SendDeviceInfos(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      string sessionIdString)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SendDeviceInfos");
      if (connectionType == null)
        throw new ApiException(400, "Missing required parameter 'connectionType' when calling SendDeviceInfos");
      if (clientType == null)
        throw new ApiException(400, "Missing required parameter 'clientType' when calling SendDeviceInfos");
      string path = "/Account/SendDeviceInfos".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (connectionType != null)
        formParams.Add("connection_type", this.ApiClient.ParameterToString((object) connectionType));
      if (clientType != null)
        formParams.Add("client_type", this.ApiClient.ParameterToString((object) clientType));
      if (os != null)
        formParams.Add(nameof (os), this.ApiClient.ParameterToString((object) os));
      if (device != null)
        formParams.Add(nameof (device), this.ApiClient.ParameterToString((object) device));
      if (partner != null)
        formParams.Add(nameof (partner), this.ApiClient.ParameterToString((object) partner));
      if (deviceUid != null)
        formParams.Add("device_uid", this.ApiClient.ParameterToString((object) deviceUid));
      if (sessionIdString != null)
        formParams.Add("session_id_string", this.ApiClient.ParameterToString((object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendDeviceInfos: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendDeviceInfos: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void SendDeviceInfosWithPassword(
      long? accountId,
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      string sessionIdString)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling SendDeviceInfosWithPassword");
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SendDeviceInfosWithPassword");
      if (connectionType == null)
        throw new ApiException(400, "Missing required parameter 'connectionType' when calling SendDeviceInfosWithPassword");
      if (clientType == null)
        throw new ApiException(400, "Missing required parameter 'clientType' when calling SendDeviceInfosWithPassword");
      string path = "/Account/SendDeviceInfosWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (connectionType != null)
        formParams.Add("connection_type", this.ApiClient.ParameterToString((object) connectionType));
      if (clientType != null)
        formParams.Add("client_type", this.ApiClient.ParameterToString((object) clientType));
      if (os != null)
        formParams.Add(nameof (os), this.ApiClient.ParameterToString((object) os));
      if (device != null)
        formParams.Add(nameof (device), this.ApiClient.ParameterToString((object) device));
      if (partner != null)
        formParams.Add(nameof (partner), this.ApiClient.ParameterToString((object) partner));
      if (deviceUid != null)
        formParams.Add("device_uid", this.ApiClient.ParameterToString((object) deviceUid));
      if (sessionIdString != null)
        formParams.Add("session_id_string", this.ApiClient.ParameterToString((object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendDeviceInfosWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SendDeviceInfosWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void SetNickname(long? accountId, string nickname, string lang)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling SetNickname");
      if (nickname == null)
        throw new ApiException(400, "Missing required parameter 'nickname' when calling SetNickname");
      string path = "/Account/SetNickname".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (nickname != null)
        formParams.Add(nameof (nickname), this.ApiClient.ParameterToString((object) nickname));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetNickname: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetNickname: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void SetNicknameWithApiKey(string nickname, string lang)
    {
      if (nickname == null)
        throw new ApiException(400, "Missing required parameter 'nickname' when calling SetNicknameWithApiKey");
      string path = "/Account/SetNicknameWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (nickname != null)
        formParams.Add(nameof (nickname), this.ApiClient.ParameterToString((object) nickname));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetNicknameWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetNicknameWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAccountApi<bool?> SetStatus(long? accountId, string statusKey, long? statusValue)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling SetStatus");
      if (statusKey == null)
        throw new ApiException(400, "Missing required parameter 'statusKey' when calling SetStatus");
      if (!statusValue.HasValue)
        throw new ApiException(400, "Missing required parameter 'statusValue' when calling SetStatus");
      string path = "/Account/SetStatus".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (statusKey != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "status_key", (object) statusKey));
      if (statusValue.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "status_value", (object) statusValue));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetStatus: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetStatus: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      bool? data = (bool?) this.ApiClient.Deserialize(restResponse.Content, typeof (bool?), restResponse.Headers);
      return new RAccountApi<bool?>(restResponse, data);
    }

    public void SignOff(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      DateTime? date,
      string sessionIdString)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SignOff");
      if (connectionType == null)
        throw new ApiException(400, "Missing required parameter 'connectionType' when calling SignOff");
      if (clientType == null)
        throw new ApiException(400, "Missing required parameter 'clientType' when calling SignOff");
      string path = "/Account/SignOff".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (connectionType != null)
        formParams.Add("connection_type", this.ApiClient.ParameterToString((object) connectionType));
      if (clientType != null)
        formParams.Add("client_type", this.ApiClient.ParameterToString((object) clientType));
      if (os != null)
        formParams.Add(nameof (os), this.ApiClient.ParameterToString((object) os));
      if (device != null)
        formParams.Add(nameof (device), this.ApiClient.ParameterToString((object) device));
      if (partner != null)
        formParams.Add(nameof (partner), this.ApiClient.ParameterToString((object) partner));
      if (deviceUid != null)
        formParams.Add("device_uid", this.ApiClient.ParameterToString((object) deviceUid));
      if (date.HasValue)
        formParams.Add(nameof (date), this.ApiClient.ParameterToString((object) date));
      if (sessionIdString != null)
        formParams.Add("session_id_string", this.ApiClient.ParameterToString((object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOff: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOff: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void SignOffWithApiKey(
      long? sessionId,
      string connectionType,
      string clientType,
      string os,
      string device,
      string partner,
      string deviceUid,
      DateTime? date,
      string sessionIdString)
    {
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling SignOffWithApiKey");
      if (connectionType == null)
        throw new ApiException(400, "Missing required parameter 'connectionType' when calling SignOffWithApiKey");
      if (clientType == null)
        throw new ApiException(400, "Missing required parameter 'clientType' when calling SignOffWithApiKey");
      string path = "/Account/SignOffWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (connectionType != null)
        formParams.Add("connection_type", this.ApiClient.ParameterToString((object) connectionType));
      if (clientType != null)
        formParams.Add("client_type", this.ApiClient.ParameterToString((object) clientType));
      if (os != null)
        formParams.Add(nameof (os), this.ApiClient.ParameterToString((object) os));
      if (device != null)
        formParams.Add(nameof (device), this.ApiClient.ParameterToString((object) device));
      if (partner != null)
        formParams.Add(nameof (partner), this.ApiClient.ParameterToString((object) partner));
      if (deviceUid != null)
        formParams.Add("device_uid", this.ApiClient.ParameterToString((object) deviceUid));
      if (date.HasValue)
        formParams.Add(nameof (date), this.ApiClient.ParameterToString((object) date));
      if (sessionIdString != null)
        formParams.Add("session_id_string", this.ApiClient.ParameterToString((object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOffWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOffWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAccountApi<SessionLogin> SignOnWithApiKey(long? game)
    {
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SignOnWithApiKey");
      string path = "/Account/SignOnWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RAccountApi<SessionLogin>(restResponse, data);
    }

    public RAccountApi<SessionLogin> SignOnWithGhostUid(
      string uid,
      long? game,
      string ip,
      DateTime? date,
      string os,
      string device,
      string connectionType,
      string clientType,
      string partner,
      string deviceUid)
    {
      if (uid == null)
        throw new ApiException(400, "Missing required parameter 'uid' when calling SignOnWithGhostUid");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SignOnWithGhostUid");
      string path = "/Account/SignOnWithGhostUid".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (uid != null)
        formParams.Add(nameof (uid), this.ApiClient.ParameterToString((object) uid));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (date.HasValue)
        formParams.Add(nameof (date), this.ApiClient.ParameterToString((object) date));
      if (os != null)
        formParams.Add(nameof (os), this.ApiClient.ParameterToString((object) os));
      if (device != null)
        formParams.Add(nameof (device), this.ApiClient.ParameterToString((object) device));
      if (connectionType != null)
        formParams.Add("connection_type", this.ApiClient.ParameterToString((object) connectionType));
      if (clientType != null)
        formParams.Add("client_type", this.ApiClient.ParameterToString((object) clientType));
      if (partner != null)
        formParams.Add(nameof (partner), this.ApiClient.ParameterToString((object) partner));
      if (deviceUid != null)
        formParams.Add("device_uid", this.ApiClient.ParameterToString((object) deviceUid));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithGhostUid: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithGhostUid: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RAccountApi<SessionLogin>(restResponse, data);
    }

    public RAccountApi<SessionLogin> SignOnWithPassword(
      string login,
      string password,
      string ip,
      long? game)
    {
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling SignOnWithPassword");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling SignOnWithPassword");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling SignOnWithPassword");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SignOnWithPassword");
      string path = "/Account/SignOnWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RAccountApi<SessionLogin>(restResponse, data);
    }

    public RAccountApi<SessionLogin> SignOnWithToken(string token, string ip, long? game)
    {
      if (token == null)
        throw new ApiException(400, "Missing required parameter 'token' when calling SignOnWithToken");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling SignOnWithToken");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling SignOnWithToken");
      string path = "/Account/SignOnWithToken".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (token != null)
        formParams.Add(nameof (token), this.ApiClient.ParameterToString((object) token));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithToken: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SignOnWithToken: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RAccountApi<SessionLogin>(restResponse, data);
    }

    public RAccountApi<SessionAccount> StartSession(
      long? accountId,
      long? sessionId,
      long? game,
      string ip,
      string sessionIdString)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling StartSession");
      if (!sessionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'sessionId' when calling StartSession");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling StartSession");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling StartSession");
      string path = "/Account/StartSession".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (sessionId.HasValue)
        formParams.Add("session_id", this.ApiClient.ParameterToString((object) sessionId));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (sessionIdString != null)
        formParams.Add("session_id_string", this.ApiClient.ParameterToString((object) sessionIdString));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSession: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartSession: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionAccount data = (SessionAccount) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionAccount), restResponse.Headers);
      return new RAccountApi<SessionAccount>(restResponse, data);
    }

    public RAccountApi<List<AccountPublicStatus>> Status()
    {
      string path = "/Account/Status".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Status: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Status: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AccountPublicStatus> data = (List<AccountPublicStatus>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AccountPublicStatus>), restResponse.Headers);
      return new RAccountApi<List<AccountPublicStatus>>(restResponse, data);
    }

    public RAccountApi<List<AccountStatus>> StatusByAccountId(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling StatusByAccountId");
      string path = "/Account/StatusByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StatusByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StatusByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AccountStatus> data = (List<AccountStatus>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AccountStatus>), restResponse.Headers);
      return new RAccountApi<List<AccountStatus>>(restResponse, data);
    }

    public void ValidateGuest(
      string lang,
      long? accountId,
      long? gameId,
      string login,
      string nickname,
      string email,
      string password)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ValidateGuest");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ValidateGuest");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ValidateGuest");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling ValidateGuest");
      if (nickname == null)
        throw new ApiException(400, "Missing required parameter 'nickname' when calling ValidateGuest");
      if (email == null)
        throw new ApiException(400, "Missing required parameter 'email' when calling ValidateGuest");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling ValidateGuest");
      string path = "/Account/ValidateGuest".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (nickname != null)
        formParams.Add(nameof (nickname), this.ApiClient.ParameterToString((object) nickname));
      if (email != null)
        formParams.Add(nameof (email), this.ApiClient.ParameterToString((object) email));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateGuest: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateGuest: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void ValidateSteam(
      string lang,
      long? accountId,
      long? gameId,
      string login,
      string nickname,
      string email,
      string password)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ValidateSteam");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ValidateSteam");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ValidateSteam");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling ValidateSteam");
      if (nickname == null)
        throw new ApiException(400, "Missing required parameter 'nickname' when calling ValidateSteam");
      if (email == null)
        throw new ApiException(400, "Missing required parameter 'email' when calling ValidateSteam");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling ValidateSteam");
      string path = "/Account/ValidateSteam".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (nickname != null)
        formParams.Add(nameof (nickname), this.ApiClient.ParameterToString((object) nickname));
      if (email != null)
        formParams.Add(nameof (email), this.ApiClient.ParameterToString((object) email));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateSteam: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateSteam: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
