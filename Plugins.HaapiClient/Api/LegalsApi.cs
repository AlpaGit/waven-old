// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.LegalsApi
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
  public class LegalsApi : ILegalsApi
  {
    public LegalsApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public LegalsApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RLegalsApi<LegalContent> Cgu(string lang, long? game, long? knowVersion, string format)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling Cgu");
      string path = "/Legals/Cgu".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (knowVersion.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (knowVersion), (object) knowVersion));
      if (format != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (format), (object) format));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Cgu: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Cgu: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      LegalContent data = (LegalContent) this.ApiClient.Deserialize(restResponse.Content, typeof (LegalContent), restResponse.Headers);
      return new RLegalsApi<LegalContent>(restResponse, data);
    }

    public RLegalsApi<LegalContent> Cgv(string lang, long? game, long? knowVersion, string format)
    {
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling Cgv");
      string path = "/Legals/Cgv".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (knowVersion.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (knowVersion), (object) knowVersion));
      if (format != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (format), (object) format));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Cgv: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Cgv: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      LegalContent data = (LegalContent) this.ApiClient.Deserialize(restResponse.Content, typeof (LegalContent), restResponse.Headers);
      return new RLegalsApi<LegalContent>(restResponse, data);
    }

    public RLegalsApi<long?> SetCguVersion(long? iVersion)
    {
      if (!iVersion.HasValue)
        throw new ApiException(400, "Missing required parameter 'iVersion' when calling SetCguVersion");
      string path = "/Legals/SetCguVersion".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (iVersion.HasValue)
        formParams.Add(nameof (iVersion), this.ApiClient.ParameterToString((object) iVersion));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetCguVersion: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetCguVersion: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RLegalsApi<long?>(restResponse, data);
    }

    public RLegalsApi<long?> SetCgvVersion(long? iVersion)
    {
      if (!iVersion.HasValue)
        throw new ApiException(400, "Missing required parameter 'iVersion' when calling SetCgvVersion");
      string path = "/Legals/SetCgvVersion".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (iVersion.HasValue)
        formParams.Add(nameof (iVersion), this.ApiClient.ParameterToString((object) iVersion));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetCgvVersion: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetCgvVersion: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      long? data = (long?) this.ApiClient.Deserialize(restResponse.Content, typeof (long?), restResponse.Headers);
      return new RLegalsApi<long?>(restResponse, data);
    }
  }
}
