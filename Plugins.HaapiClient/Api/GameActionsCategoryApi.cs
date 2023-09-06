// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.GameActionsCategoryApi
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
  public class GameActionsCategoryApi : IGameActionsCategoryApi
  {
    public GameActionsCategoryApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public GameActionsCategoryApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void Delete(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Delete");
      string path = "/Game/Actions/Category/Delete".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Delete: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Delete: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RGameActionsCategoryApi<List<GameActionsCategory>> Get(long? parentId)
    {
      string path = "/Game/Actions/Category/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (parentId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "parent_id", (object) parentId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsCategory> data = (List<GameActionsCategory>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsCategory>), restResponse.Headers);
      return new RGameActionsCategoryApi<List<GameActionsCategory>>(restResponse, data);
    }

    public RGameActionsCategoryApi<GameActionsCategory> GetById(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetById");
      string path = "/Game/Actions/Category/GetById".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetById: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetById: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsCategory data = (GameActionsCategory) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsCategory), restResponse.Headers);
      return new RGameActionsCategoryApi<GameActionsCategory>(restResponse, data);
    }

    public RGameActionsCategoryApi<GameActionsCategory> Insert(
      string name,
      long? parentId,
      long? gameId)
    {
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling Insert");
      string path = "/Game/Actions/Category/Insert".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (parentId.HasValue)
        formParams.Add("parent_id", this.ApiClient.ParameterToString((object) parentId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsCategory data = (GameActionsCategory) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsCategory), restResponse.Headers);
      return new RGameActionsCategoryApi<GameActionsCategory>(restResponse, data);
    }

    public RGameActionsCategoryApi<GameActionsCategory> Update(
      long? id,
      string name,
      long? parentId,
      long? gameId)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Update");
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling Update");
      string path = "/Game/Actions/Category/Update".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        formParams.Add(nameof (id), this.ApiClient.ParameterToString((object) id));
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (parentId.HasValue)
        formParams.Add("parent_id", this.ApiClient.ParameterToString((object) parentId));
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsCategory data = (GameActionsCategory) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsCategory), restResponse.Headers);
      return new RGameActionsCategoryApi<GameActionsCategory>(restResponse, data);
    }
  }
}
