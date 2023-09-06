// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ApiApi
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
  public class ApiApi : IApiApi
  {
    public ApiApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public ApiApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RApiApi<List<ApiTransactionReturn>> CommitTransaction(long? transactionId)
    {
      if (!transactionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'transactionId' when calling CommitTransaction");
      string path = "/Api/CommitTransaction".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (transactionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "transaction_id", (object) transactionId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CommitTransaction: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CommitTransaction: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ApiTransactionReturn> data = (List<ApiTransactionReturn>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ApiTransactionReturn>), restResponse.Headers);
      return new RApiApi<List<ApiTransactionReturn>>(restResponse, data);
    }

    public RApiApi<ApiKey> CreateApiKey(
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
      string partnerId)
    {
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling CreateApiKey");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling CreateApiKey");
      string path = "/Api/CreateApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (longLifeToken.HasValue)
        formParams.Add("long_life_token", this.ApiClient.ParameterToString((object) longLifeToken));
      if (meta != null)
        formParams.Add(nameof (meta), this.ApiClient.ParameterToString((object) meta));
      if (shopKey != null)
        formParams.Add("shop_key", this.ApiClient.ParameterToString((object) shopKey));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (characterId.HasValue)
        formParams.Add("character_id", this.ApiClient.ParameterToString((object) characterId));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (country != null)
        formParams.Add(nameof (country), this.ApiClient.ParameterToString((object) country));
      if (currency != null)
        formParams.Add(nameof (currency), this.ApiClient.ParameterToString((object) currency));
      if (paymentMode != null)
        formParams.Add("payment_mode", this.ApiClient.ParameterToString((object) paymentMode));
      if (partnerId != null)
        formParams.Add("partner_id", this.ApiClient.ParameterToString((object) partnerId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RApiApi<ApiKey>(restResponse, data);
    }

    public RApiApi<ApiKey> CreateApiKeyFromServer(long? accountId, string ip, List<string> meta)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling CreateApiKeyFromServer");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling CreateApiKeyFromServer");
      string path = "/Api/CreateApiKeyFromServer".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (meta != null)
        formParams.Add(nameof (meta), this.ApiClient.ParameterToString((object) meta));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKeyFromServer: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKeyFromServer: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RApiApi<ApiKey>(restResponse, data);
    }

    public void DeleteApiKey()
    {
      string path = "/Api/DeleteApiKey".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RApiApi<bool?> DeleteApiKeyByAccountId(long? accountId, string apikey)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling DeleteApiKeyByAccountId");
      string path = "/Api/DeleteApiKeyByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (apikey != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (apikey), (object) apikey));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteApiKeyByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteApiKeyByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      bool? data = (bool?) this.ApiClient.Deserialize(restResponse.Content, typeof (bool?), restResponse.Headers);
      return new RApiApi<bool?>(restResponse, data);
    }

    public RApiApi<List<ApiKey>> GetApiKeysByAccountId(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling GetApiKeysByAccountId");
      string path = "/Api/GetApiKeysByAccountId".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetApiKeysByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetApiKeysByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ApiKey> data = (List<ApiKey>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ApiKey>), restResponse.Headers);
      return new RApiApi<List<ApiKey>>(restResponse, data);
    }

    public RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthAccount> OAuthAccount(
      string clientId,
      string accountId,
      string ip)
    {
      if (clientId == null)
        throw new ApiException(400, "Missing required parameter 'clientId' when calling OAuthAccount");
      if (accountId == null)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling OAuthAccount");
      string path = "/Api/OAuthAccount".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (clientId != null)
        formParams.Add("client_id", this.ApiClient.ParameterToString((object) clientId));
      if (accountId != null)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthAccount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthAccount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.OAuthAccount data = (Com.Ankama.Haapi.Swagger.Model.OAuthAccount) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.OAuthAccount), restResponse.Headers);
      return new RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthAccount>(restResponse, data);
    }

    public RApiApi<Account> OAuthMe(string accessToken)
    {
      if (accessToken == null)
        throw new ApiException(400, "Missing required parameter 'accessToken' when calling OAuthMe");
      string path = "/Api/OAuthMe".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accessToken != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "access_token", (object) accessToken));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthMe: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthMe: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Account data = (Account) this.ApiClient.Deserialize(restResponse.Content, typeof (Account), restResponse.Headers);
      return new RApiApi<Account>(restResponse, data);
    }

    public RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthProvider> OAuthProvider(
      string clientId,
      string secret)
    {
      if (clientId == null)
        throw new ApiException(400, "Missing required parameter 'clientId' when calling OAuthProvider");
      string path = "/Api/OAuthProvider".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (clientId != null)
        formParams.Add("client_id", this.ApiClient.ParameterToString((object) clientId));
      if (secret != null)
        formParams.Add(nameof (secret), this.ApiClient.ParameterToString((object) secret));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthProvider: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthProvider: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      Com.Ankama.Haapi.Swagger.Model.OAuthProvider data = (Com.Ankama.Haapi.Swagger.Model.OAuthProvider) this.ApiClient.Deserialize(restResponse.Content, typeof (Com.Ankama.Haapi.Swagger.Model.OAuthProvider), restResponse.Headers);
      return new RApiApi<Com.Ankama.Haapi.Swagger.Model.OAuthProvider>(restResponse, data);
    }

    public RApiApi<OAuthKey> OAuthToken(
      string clientId,
      string clientSecret,
      string grantType,
      string redirectUri,
      string code,
      string refreshToken,
      string accessType)
    {
      if (clientId == null)
        throw new ApiException(400, "Missing required parameter 'clientId' when calling OAuthToken");
      if (clientSecret == null)
        throw new ApiException(400, "Missing required parameter 'clientSecret' when calling OAuthToken");
      if (grantType == null)
        throw new ApiException(400, "Missing required parameter 'grantType' when calling OAuthToken");
      string path = "/Api/OAuthToken".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (clientId != null)
        formParams.Add("client_id", this.ApiClient.ParameterToString((object) clientId));
      if (clientSecret != null)
        formParams.Add("client_secret", this.ApiClient.ParameterToString((object) clientSecret));
      if (grantType != null)
        formParams.Add("grant_type", this.ApiClient.ParameterToString((object) grantType));
      if (redirectUri != null)
        formParams.Add("redirect_uri", this.ApiClient.ParameterToString((object) redirectUri));
      if (code != null)
        formParams.Add(nameof (code), this.ApiClient.ParameterToString((object) code));
      if (refreshToken != null)
        formParams.Add("refresh_token", this.ApiClient.ParameterToString((object) refreshToken));
      if (accessType != null)
        formParams.Add("access_type", this.ApiClient.ParameterToString((object) accessType));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthToken: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OAuthToken: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      OAuthKey data = (OAuthKey) this.ApiClient.Deserialize(restResponse.Content, typeof (OAuthKey), restResponse.Headers);
      return new RApiApi<OAuthKey>(restResponse, data);
    }

    public RApiApi<ApiKey> RefreshApiKey(string refreshToken, bool? longLifeToken)
    {
      if (refreshToken == null)
        throw new ApiException(400, "Missing required parameter 'refreshToken' when calling RefreshApiKey");
      string path = "/Api/RefreshApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (refreshToken != null)
        formParams.Add("refresh_token", this.ApiClient.ParameterToString((object) refreshToken));
      if (longLifeToken.HasValue)
        formParams.Add("long_life_token", this.ApiClient.ParameterToString((object) longLifeToken));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RefreshApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RefreshApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RApiApi<ApiKey>(restResponse, data);
    }

    public void StartTransaction()
    {
      string path = "/Api/StartTransaction".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartTransaction: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling StartTransaction: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
