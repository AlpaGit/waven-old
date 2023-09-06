// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.KardApi
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
  public class KardApi : IKardApi
  {
    public KardApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public KardApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RKardApi<KardTicket> ConsumeByCode(string code, string lang)
    {
      if (code == null)
        throw new ApiException(400, "Missing required parameter 'code' when calling ConsumeByCode");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ConsumeByCode");
      string path = "/Kard/ConsumeByCode".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (code != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (code), (object) code));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeByCode: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeByCode: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      KardTicket data = (KardTicket) this.ApiClient.Deserialize(restResponse.Content, typeof (KardTicket), restResponse.Headers);
      return new RKardApi<KardTicket>(restResponse, data);
    }

    public RKardApi<List<KardKard>> ConsumeById(string lang, long? id, long? gameId)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ConsumeById");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling ConsumeById");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling ConsumeById");
      string path = "/Kard/ConsumeById".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "game_id", (object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeById: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeById: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<KardKard> data = (List<KardKard>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<KardKard>), restResponse.Headers);
      return new RKardApi<List<KardKard>>(restResponse, data);
    }

    public RKardApi<List<KardKard>> ConsumeByOrderId(string lang, long? orderId)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling ConsumeByOrderId");
      if (!orderId.HasValue)
        throw new ApiException(400, "Missing required parameter 'orderId' when calling ConsumeByOrderId");
      string path = "/Kard/ConsumeByOrderId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (orderId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_id", (object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeByOrderId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeByOrderId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<KardKard> data = (List<KardKard>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<KardKard>), restResponse.Headers);
      return new RKardApi<List<KardKard>>(restResponse, data);
    }

    public RKardApi<List<KardKardStock>> GetByAccountId(
      long? accountId,
      string lang,
      long? collectionId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling GetByAccountId");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling GetByAccountId");
      string path = "/Kard/GetByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (collectionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "collection_id", (object) collectionId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<KardKardStock> data = (List<KardKardStock>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<KardKardStock>), restResponse.Headers);
      return new RKardApi<List<KardKardStock>>(restResponse, data);
    }
  }
}
