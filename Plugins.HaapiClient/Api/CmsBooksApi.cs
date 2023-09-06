// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.CmsBooksApi
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
  public class CmsBooksApi : ICmsBooksApi
  {
    public CmsBooksApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public CmsBooksApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RCmsBooksApi<CmsBookItem> GetBookItem(string site, string lang, long? id)
    {
      if (site == null)
        throw new ApiException(400, "Missing required parameter 'site' when calling GetBookItem");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling GetBookItem");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetBookItem");
      string path = "/Cms/Books/GetBookItem".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (site != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (site), (object) site));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetBookItem: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetBookItem: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      CmsBookItem data = (CmsBookItem) this.ApiClient.Deserialize(restResponse.Content, typeof (CmsBookItem), restResponse.Headers);
      return new RCmsBooksApi<CmsBookItem>(restResponse, data);
    }
  }
}
