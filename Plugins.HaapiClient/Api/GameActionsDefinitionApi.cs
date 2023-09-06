// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.GameActionsDefinitionApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using IO.Swagger.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class GameActionsDefinitionApi : IGameActionsDefinitionApi
  {
    public GameActionsDefinitionApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public GameActionsDefinitionApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void Delete(long? id, long? accountId)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Delete");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Delete");
      string path = "/Game/Actions/Definition/Delete".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
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

    public RGameActionsDefinitionApi<List<GameActionsDefinition>> Get(
      long? page,
      long? count,
      long? categoryId,
      long? restrictionId,
      string orderExpression,
      string orderDirection,
      string search,
      long? game)
    {
      string path = "/Game/Actions/Definition/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (count.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (count), (object) count));
      if (categoryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "category_id", (object) categoryId));
      if (restrictionId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "restriction_id", (object) restrictionId));
      if (orderExpression != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_expression", (object) orderExpression));
      if (orderDirection != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_direction", (object) orderDirection));
      if (search != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (search), (object) search));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsDefinition> data = (List<GameActionsDefinition>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsDefinition>), restResponse.Headers);
      return new RGameActionsDefinitionApi<List<GameActionsDefinition>>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> GetById(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetById");
      string path = "/Game/Actions/Definition/GetById".Replace("{format}", "json");
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
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> GetByIdFromClient(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetByIdFromClient");
      string path = "/Game/Actions/Definition/GetByIdFromClient".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByIdFromClient: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByIdFromClient: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> GetByName(string name)
    {
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling GetByName");
      string path = "/Game/Actions/Definition/GetByName".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (name != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (name), (object) name));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByName: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByName: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> GetByType(string type)
    {
      if (type == null)
        throw new ApiException(400, "Missing required parameter 'type' when calling GetByType");
      string path = "/Game/Actions/Definition/GetByType".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (type != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (type), (object) type));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByType: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetByType: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> Insert(
      long? accountId,
      long? categoryId,
      string name,
      string actions,
      long? restrictionId,
      DateTime? onlineDate,
      DateTime? offlineDate,
      List<string> definitionLang,
      List<string> definitionTitle,
      List<string> definitionDescription,
      DateTime? availableDate)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Insert");
      if (!categoryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'categoryId' when calling Insert");
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling Insert");
      string path = "/Game/Actions/Definition/Insert".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (categoryId.HasValue)
        formParams.Add("category_id", this.ApiClient.ParameterToString((object) categoryId));
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (actions != null)
        formParams.Add(nameof (actions), this.ApiClient.ParameterToString((object) actions));
      if (restrictionId.HasValue)
        formParams.Add("restriction_id", this.ApiClient.ParameterToString((object) restrictionId));
      if (onlineDate.HasValue)
        formParams.Add("online_date", this.ApiClient.ParameterToString((object) onlineDate));
      if (offlineDate.HasValue)
        formParams.Add("offline_date", this.ApiClient.ParameterToString((object) offlineDate));
      if (definitionLang != null)
        formParams.Add("definition_lang", this.ApiClient.ParameterToString((object) definitionLang));
      if (definitionTitle != null)
        formParams.Add("definition_title", this.ApiClient.ParameterToString((object) definitionTitle));
      if (definitionDescription != null)
        formParams.Add("definition_description", this.ApiClient.ParameterToString((object) definitionDescription));
      if (availableDate.HasValue)
        formParams.Add("available_date", this.ApiClient.ParameterToString((object) availableDate));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Insert: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }

    public RGameActionsDefinitionApi<GameActionsDefinition> Update(
      long? id,
      long? accountId,
      long? categoryId,
      string name,
      string actions,
      long? restrictionId,
      DateTime? onlineDate,
      DateTime? offlineDate,
      List<string> definitionLang,
      List<string> definitionTitle,
      List<string> definitionDescription,
      DateTime? availableDate)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Update");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Update");
      string path = "/Game/Actions/Definition/Update".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        formParams.Add(nameof (id), this.ApiClient.ParameterToString((object) id));
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (categoryId.HasValue)
        formParams.Add("category_id", this.ApiClient.ParameterToString((object) categoryId));
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (actions != null)
        formParams.Add(nameof (actions), this.ApiClient.ParameterToString((object) actions));
      if (restrictionId.HasValue)
        formParams.Add("restriction_id", this.ApiClient.ParameterToString((object) restrictionId));
      if (onlineDate.HasValue)
        formParams.Add("online_date", this.ApiClient.ParameterToString((object) onlineDate));
      if (offlineDate.HasValue)
        formParams.Add("offline_date", this.ApiClient.ParameterToString((object) offlineDate));
      if (definitionLang != null)
        formParams.Add("definition_lang", this.ApiClient.ParameterToString((object) definitionLang));
      if (definitionTitle != null)
        formParams.Add("definition_title", this.ApiClient.ParameterToString((object) definitionTitle));
      if (definitionDescription != null)
        formParams.Add("definition_description", this.ApiClient.ParameterToString((object) definitionDescription));
      if (availableDate.HasValue)
        formParams.Add("available_date", this.ApiClient.ParameterToString((object) availableDate));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Update: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsDefinition data = (GameActionsDefinition) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsDefinition), restResponse.Headers);
      return new RGameActionsDefinitionApi<GameActionsDefinition>(restResponse, data);
    }
  }
}
