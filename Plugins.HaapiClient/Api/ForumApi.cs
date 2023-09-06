// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ForumApi
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
  public class ForumApi : IForumApi
  {
    public ForumApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public ForumApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RForumApi<List<ForumPost>> PostsList(
      string forum,
      string lang,
      long? topicId,
      long? page,
      long? size,
      long? accountId)
    {
      if (forum == null)
        throw new ApiException(400, "Missing required parameter 'forum' when calling PostsList");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling PostsList");
      if (!topicId.HasValue)
        throw new ApiException(400, "Missing required parameter 'topicId' when calling PostsList");
      string path = "/Forum/PostsList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (forum != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (forum), (object) forum));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (topicId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "topic_id", (object) topicId));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostsList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostsList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ForumPost> data = (List<ForumPost>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ForumPost>), restResponse.Headers);
      return new RForumApi<List<ForumPost>>(restResponse, data);
    }

    public RForumApi<List<ForumTopic>> TopicsList(
      string forum,
      string lang,
      long? threadId,
      long? page,
      long? size,
      long? accountId)
    {
      if (forum == null)
        throw new ApiException(400, "Missing required parameter 'forum' when calling TopicsList");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling TopicsList");
      if (!threadId.HasValue)
        throw new ApiException(400, "Missing required parameter 'threadId' when calling TopicsList");
      string path = "/Forum/TopicsList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (forum != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (forum), (object) forum));
      if (lang != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (lang), (object) lang));
      if (threadId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "thread_id", (object) threadId));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling TopicsList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling TopicsList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ForumTopic> data = (List<ForumTopic>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ForumTopic>), restResponse.Headers);
      return new RForumApi<List<ForumTopic>>(restResponse, data);
    }
  }
}
