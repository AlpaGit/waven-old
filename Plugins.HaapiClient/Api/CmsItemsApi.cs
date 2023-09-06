// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.CmsItemsApi
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
  public class CmsItemsApi : ICmsItemsApi
  {
    public CmsItemsApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public CmsItemsApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RCmsItemsApi<List<CmsArticle>> Get(
      string templateKey,
      string site,
      string lang,
      long? page,
      long? count)
    {
      if (templateKey == null)
        throw new ApiException(400, "Missing required parameter 'templateKey' when calling Get");
      if (site == null)
        throw new ApiException(400, "Missing required parameter 'site' when calling Get");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling Get");
      string path = "/Cms/Items/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (templateKey != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "template_key", (object) templateKey));
      if (site != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (site), (object) site));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (count.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (count), (object) count));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<CmsArticle> data = (List<CmsArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<CmsArticle>), restResponse.Headers);
      return new RCmsItemsApi<List<CmsArticle>>(restResponse, data);
    }

    public RCmsItemsApi<CmsArticleExtended> GetById(
      string templateKey,
      string site,
      string lang,
      long? id)
    {
      if (templateKey == null)
        throw new ApiException(400, "Missing required parameter 'templateKey' when calling GetById");
      if (site == null)
        throw new ApiException(400, "Missing required parameter 'site' when calling GetById");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling GetById");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetById");
      string path = "/Cms/Items/GetById".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (templateKey != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "template_key", (object) templateKey));
      if (site != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (site), (object) site));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetById: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetById: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      CmsArticleExtended data = (CmsArticleExtended) this.ApiClient.Deserialize(restResponse.Content, typeof (CmsArticleExtended), restResponse.Headers);
      return new RCmsItemsApi<CmsArticleExtended>(restResponse, data);
    }
  }
}
