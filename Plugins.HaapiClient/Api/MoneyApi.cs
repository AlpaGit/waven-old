// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.MoneyApi
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
  public class MoneyApi : IMoneyApi
  {
    public MoneyApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public MoneyApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RMoneyApi<MoneyBalance> FragmentCredit(long? account, long? amount, long? game)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling FragmentCredit");
      if (!amount.HasValue)
        throw new ApiException(400, "Missing required parameter 'amount' when calling FragmentCredit");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling FragmentCredit");
      string path = "/Money/FragmentCredit".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      if (amount.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (amount), (object) amount));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentCredit: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentCredit: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> FragmentsAmount()
    {
      string path = "/Money/FragmentsAmount".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentsAmount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentsAmount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> FragmentsAmountWithPassword(long? account)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling FragmentsAmountWithPassword");
      string path = "/Money/FragmentsAmountWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentsAmountWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling FragmentsAmountWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> KrozAmount()
    {
      string path = "/Money/KrozAmount".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozAmount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozAmount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> KrozAmountWithPassword(long? account)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling KrozAmountWithPassword");
      string path = "/Money/KrozAmountWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozAmountWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozAmountWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> KrozCredit(long? account, long? amount, long? game)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling KrozCredit");
      if (!amount.HasValue)
        throw new ApiException(400, "Missing required parameter 'amount' when calling KrozCredit");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling KrozCredit");
      string path = "/Money/KrozCredit".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      if (amount.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (amount), (object) amount));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozCredit: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling KrozCredit: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> OgrinsAmount()
    {
      string path = "/Money/OgrinsAmount".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsAmount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsAmount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> OgrinsAmountWithPassword(long? account)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling OgrinsAmountWithPassword");
      string path = "/Money/OgrinsAmountWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsAmountWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsAmountWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> OgrinsDofusTouchAmount()
    {
      string path = "/Money/OgrinsDofusTouchAmount".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsDofusTouchAmount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsDofusTouchAmount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }

    public RMoneyApi<MoneyBalance> OgrinsDofusTouchAmountWithPassword(long? account)
    {
      if (!account.HasValue)
        throw new ApiException(400, "Missing required parameter 'account' when calling OgrinsDofusTouchAmountWithPassword");
      string path = "/Money/OgrinsDofusTouchAmountWithPassword".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (account.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (account), (object) account));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsDofusTouchAmountWithPassword: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OgrinsDofusTouchAmountWithPassword: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      MoneyBalance data = (MoneyBalance) this.ApiClient.Deserialize(restResponse.Content, typeof (MoneyBalance), restResponse.Headers);
      return new RMoneyApi<MoneyBalance>(restResponse, data);
    }
  }
}
