// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.CmsPollInGameApi
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
  public class CmsPollInGameApi : ICmsPollInGameApi
  {
    public CmsPollInGameApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public CmsPollInGameApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RCmsPollInGameApi<List<CmsPollInGame>> Get(
      string site,
      string lang,
      long? page,
      long? count)
    {
      if (site == null)
        throw new ApiException(400, "Missing required parameter 'site' when calling Get");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling Get");
      if (!page.HasValue)
        throw new ApiException(400, "Missing required parameter 'page' when calling Get");
      if (!count.HasValue)
        throw new ApiException(400, "Missing required parameter 'count' when calling Get");
      string path = "/Cms/PollInGame/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (site != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (site), (object) site));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (count.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (count), (object) count));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<CmsPollInGame> data = (List<CmsPollInGame>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<CmsPollInGame>), restResponse.Headers);
      return new RCmsPollInGameApi<List<CmsPollInGame>>(restResponse, data);
    }

    public RCmsPollInGameApi<bool?> MarkAsRead(long? item)
    {
      if (!item.HasValue)
        throw new ApiException(400, "Missing required parameter 'item' when calling MarkAsRead");
      string path = "/Cms/PollInGame/MarkAsRead".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (item.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (item), (object) item));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MarkAsRead: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MarkAsRead: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      bool? data = (bool?) this.ApiClient.Deserialize(restResponse.Content, typeof (bool?), restResponse.Headers);
      return new RCmsPollInGameApi<bool?>(restResponse, data);
    }
  }
}
