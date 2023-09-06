// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ProviderApi
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
  public class ProviderApi : IProviderApi
  {
    public ProviderApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public ProviderApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RProviderApi<SessionLogin> AccountGhostCreate(
      string provider,
      string uId,
      long? gameId,
      string lang,
      string data,
      string email,
      bool? isValidEmail,
      string validationEmailLink)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling AccountGhostCreate");
      if (uId == null)
        throw new ApiException(400, "Missing required parameter 'uId' when calling AccountGhostCreate");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountGhostCreate");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling AccountGhostCreate");
      string path = "/Provider/AccountGhostCreate".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (uId != null)
        formParams.Add(nameof (uId), this.ApiClient.ParameterToString((object) uId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (data != null)
        formParams.Add(nameof (data), this.ApiClient.ParameterToString((object) data));
      if (email != null)
        formParams.Add(nameof (email), this.ApiClient.ParameterToString((object) email));
      if (isValidEmail.HasValue)
        formParams.Add("is_valid_email", this.ApiClient.ParameterToString((object) isValidEmail));
      if (validationEmailLink != null)
        formParams.Add("validation_email_link", this.ApiClient.ParameterToString((object) validationEmailLink));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountGhostCreate: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountGhostCreate: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data1 = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RProviderApi<SessionLogin>(restResponse, data1);
    }

    public RProviderApi<SessionLogin> AccountLink(
      string provider,
      string uId,
      long? gameId,
      string login,
      string password,
      bool? passwordEncoded,
      string data)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling AccountLink");
      if (uId == null)
        throw new ApiException(400, "Missing required parameter 'uId' when calling AccountLink");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountLink");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling AccountLink");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling AccountLink");
      string path = "/Provider/AccountLink".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (uId != null)
        formParams.Add(nameof (uId), this.ApiClient.ParameterToString((object) uId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (passwordEncoded.HasValue)
        formParams.Add(nameof (passwordEncoded), this.ApiClient.ParameterToString((object) passwordEncoded));
      if (data != null)
        formParams.Add(nameof (data), this.ApiClient.ParameterToString((object) data));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLink: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AccountLink: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SessionLogin data1 = (SessionLogin) this.ApiClient.Deserialize(restResponse.Content, typeof (SessionLogin), restResponse.Headers);
      return new RProviderApi<SessionLogin>(restResponse, data1);
    }

    public RProviderApi<SessionLogin> AccountLogin(string provider, string uId, long? gameId)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling AccountLogin");
      if (uId == null)
        throw new ApiException(400, "Missing required parameter 'uId' when calling AccountLogin");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling AccountLogin");
      string path = "/Provider/AccountLogin".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (uId != null)
        formParams.Add(nameof (uId), this.ApiClient.ParameterToString((object) uId));
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
      return new RProviderApi<SessionLogin>(restResponse, data);
    }

    public RProviderApi<ApiKey> ApiKeyGhostCreate(
      string provider,
      string accessToken,
      long? gameId,
      string lang,
      string data)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling ApiKeyGhostCreate");
      if (accessToken == null)
        throw new ApiException(400, "Missing required parameter 'accessToken' when calling ApiKeyGhostCreate");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyGhostCreate");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ApiKeyGhostCreate");
      string path = "/Provider/ApiKeyGhostCreate".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (accessToken != null)
        formParams.Add("access_token", this.ApiClient.ParameterToString((object) accessToken));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (data != null)
        formParams.Add(nameof (data), this.ApiClient.ParameterToString((object) data));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyGhostCreate: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyGhostCreate: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data1 = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RProviderApi<ApiKey>(restResponse, data1);
    }

    public RProviderApi<ApiKey> ApiKeyLink(
      string provider,
      string accessToken,
      long? gameId,
      string login,
      string password,
      string data)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling ApiKeyLink");
      if (accessToken == null)
        throw new ApiException(400, "Missing required parameter 'accessToken' when calling ApiKeyLink");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyLink");
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling ApiKeyLink");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling ApiKeyLink");
      string path = "/Provider/ApiKeyLink".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (accessToken != null)
        formParams.Add("access_token", this.ApiClient.ParameterToString((object) accessToken));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (data != null)
        formParams.Add(nameof (data), this.ApiClient.ParameterToString((object) data));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLink: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLink: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data1 = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RProviderApi<ApiKey>(restResponse, data1);
    }

    public RProviderApi<ApiKey> ApiKeyLogin(string provider, string accessToken, long? gameId)
    {
      if (provider == null)
        throw new ApiException(400, "Missing required parameter 'provider' when calling ApiKeyLogin");
      if (accessToken == null)
        throw new ApiException(400, "Missing required parameter 'accessToken' when calling ApiKeyLogin");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ApiKeyLogin");
      string path = "/Provider/ApiKeyLogin".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (provider != null)
        formParams.Add(nameof (provider), this.ApiClient.ParameterToString((object) provider));
      if (accessToken != null)
        formParams.Add("access_token", this.ApiClient.ParameterToString((object) accessToken));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLogin: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ApiKeyLogin: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RProviderApi<ApiKey>(restResponse, data);
    }
  }
}
