// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.CensoredWordApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using IO.Swagger.Client;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class CensoredWordApi : ICensoredWordApi
  {
    public CensoredWordApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public CensoredWordApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RCensoredWordApi<bool?> IsCensoredWord(string word, string lang, List<string> applies)
    {
      if (word == null)
        throw new ApiException(400, "Missing required parameter 'word' when calling IsCensoredWord");
      string path = "/CensoredWord/IsCensoredWord".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (word != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (word), (object) word));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (applies != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("multi", nameof (applies), (object) applies));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling IsCensoredWord: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling IsCensoredWord: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      bool? data = (bool?) this.ApiClient.Deserialize(restResponse.Content, typeof (bool?), restResponse.Headers);
      return new RCensoredWordApi<bool?>(restResponse, data);
    }

    public RCensoredWordApi<string> ReplaceCensoredWord(
      string sentence,
      string lang,
      List<string> applies)
    {
      if (sentence == null)
        throw new ApiException(400, "Missing required parameter 'sentence' when calling ReplaceCensoredWord");
      string path = "/CensoredWord/ReplaceCensoredWord".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (sentence != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (sentence), (object) sentence));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (applies != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("multi", nameof (applies), (object) applies));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ReplaceCensoredWord: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ReplaceCensoredWord: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      string data = (string) this.ApiClient.Deserialize(restResponse.Content, typeof (string), restResponse.Headers);
      return new RCensoredWordApi<string>(restResponse, data);
    }
  }
}
