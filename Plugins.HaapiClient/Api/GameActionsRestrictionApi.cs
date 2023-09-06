// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.GameActionsRestrictionApi
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
  public class GameActionsRestrictionApi : IGameActionsRestrictionApi
  {
    public GameActionsRestrictionApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public GameActionsRestrictionApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void Delete(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Delete");
      string path = "/Game/Actions/Restriction/Delete".Replace("{format}", "json");
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

    public RGameActionsRestrictionApi<List<GameActionsRestriction>> Get()
    {
      string path = "/Game/Actions/Restriction/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsRestriction> data = (List<GameActionsRestriction>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsRestriction>), restResponse.Headers);
      return new RGameActionsRestrictionApi<List<GameActionsRestriction>>(restResponse, data);
    }

    public RGameActionsRestrictionApi<GameActionsRestriction> GetById(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetById");
      string path = "/Game/Actions/Restriction/GetById".Replace("{format}", "json");
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
      GameActionsRestriction data = (GameActionsRestriction) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsRestriction), restResponse.Headers);
      return new RGameActionsRestrictionApi<GameActionsRestriction>(restResponse, data);
    }

    public RGameActionsRestrictionApi<GameActionsRestriction> Insert(string name, string conditions)
    {
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling Insert");
      if (conditions == null)
        throw new ApiException(400, "Missing required parameter 'conditions' when calling Insert");
      string path = "/Game/Actions/Restriction/Insert".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (conditions != null)
        formParams.Add(nameof (conditions), this.ApiClient.ParameterToString((object) conditions));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsRestriction data = (GameActionsRestriction) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsRestriction), restResponse.Headers);
      return new RGameActionsRestrictionApi<GameActionsRestriction>(restResponse, data);
    }

    public RGameActionsRestrictionApi<GameActionsRestriction> Update(
      long? id,
      string name,
      string conditions)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Update");
      string path = "/Game/Actions/Restriction/Update".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        formParams.Add(nameof (id), this.ApiClient.ParameterToString((object) id));
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (conditions != null)
        formParams.Add(nameof (conditions), this.ApiClient.ParameterToString((object) conditions));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsRestriction data = (GameActionsRestriction) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsRestriction), restResponse.Headers);
      return new RGameActionsRestrictionApi<GameActionsRestriction>(restResponse, data);
    }
  }
}
