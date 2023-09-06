// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.GameActionsAccountApi
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
  public class GameActionsAccountApi : IGameActionsAccountApi
  {
    public GameActionsAccountApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public GameActionsAccountApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RGameActionsAccountApi<List<GameActionsAccountAvailable>> Available(
      long? accountId,
      long? game,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Available");
      string path = "/Game/Actions/Account/Available".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Available: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Available: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountAvailable> data = (List<GameActionsAccountAvailable>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountAvailable>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountAvailable>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailableByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling AvailableByAccountId");
      string path = "/Game/Actions/Account/AvailableByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountAvailable> data = (List<GameActionsAccountAvailable>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountAvailable>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountAvailable>>(restResponse, data);
    }

    public RGameActionsAccountApi<GameActionsAccountAvailable> AvailableById(long? id, string uid)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling AvailableById");
      string path = "/Game/Actions/Account/AvailableById".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableById: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableById: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsAccountAvailable data = (GameActionsAccountAvailable) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsAccountAvailable), restResponse.Headers);
      return new RGameActionsAccountApi<GameActionsAccountAvailable>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailablePendingByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling AvailablePendingByAccountId");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling AvailablePendingByAccountId");
      string path = "/Game/Actions/Account/AvailablePendingByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailablePendingByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailablePendingByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountAvailable> data = (List<GameActionsAccountAvailable>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountAvailable>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountAvailable>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsActionsMeta>> AvailableSimple(
      long? accountId,
      long? game,
      long? serverId,
      long? userId,
      string type)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling AvailableSimple");
      string path = "/Game/Actions/Account/AvailableSimple".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      if (type != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (type), (object) type));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableSimple: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableSimple: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsActionsMeta> data = (List<GameActionsActionsMeta>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsActionsMeta>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsActionsMeta>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailableWithApiKey(
      long? game,
      long? serverId,
      long? userId)
    {
      string path = "/Game/Actions/Account/AvailableWithApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (userId.HasValue)
        formParams.Add("user_id", this.ApiClient.ParameterToString((object) userId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableWithApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AvailableWithApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountAvailable> data = (List<GameActionsAccountAvailable>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountAvailable>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountAvailable>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>> CancelAll(
      long? accountId,
      long? game,
      long? id,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling CancelAll");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling CancelAll");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling CancelAll");
      string path = "/Game/Actions/Account/CancelAll".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CancelAll: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CancelAll: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsActionsDeliveredMeta> data = (List<GameActionsActionsDeliveredMeta>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsActionsDeliveredMeta>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>>(restResponse, data);
    }

    public void CancelOrder(long? orderId)
    {
      if (!orderId.HasValue)
        throw new ApiException(400, "Missing required parameter 'orderId' when calling CancelOrder");
      string path = "/Game/Actions/Account/CancelOrder".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (orderId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_id", (object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CancelOrder: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CancelOrder: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RGameActionsAccountApi<GameActionsActionsDeliveredMeta> Consume(
      long? accountId,
      long? game,
      long? id,
      string uid,
      long? quantity,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Consume");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling Consume");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Consume");
      if (uid == null)
        throw new ApiException(400, "Missing required parameter 'uid' when calling Consume");
      if (!quantity.HasValue)
        throw new ApiException(400, "Missing required parameter 'quantity' when calling Consume");
      string path = "/Game/Actions/Account/Consume".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      if (quantity.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (quantity), (object) quantity));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Consume: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Consume: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsActionsDeliveredMeta data = (GameActionsActionsDeliveredMeta) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsActionsDeliveredMeta), restResponse.Headers);
      return new RGameActionsAccountApi<GameActionsActionsDeliveredMeta>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>> ConsumeAll(
      long? accountId,
      long? game,
      long? id,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ConsumeAll");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling ConsumeAll");
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling ConsumeAll");
      string path = "/Game/Actions/Account/ConsumeAll".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeAll: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumeAll: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsActionsDeliveredMeta> data = (List<GameActionsActionsDeliveredMeta>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsActionsDeliveredMeta>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsAccountConsumed>> ConsumedByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ConsumedByAccountId");
      if (!game.HasValue)
        throw new ApiException(400, "Missing required parameter 'game' when calling ConsumedByAccountId");
      string path = "/Game/Actions/Account/ConsumedByAccountId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      if (game.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (game), (object) game));
      if (serverId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "server_id", (object) serverId));
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "user_id", (object) userId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumedByAccountId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumedByAccountId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountConsumed> data = (List<GameActionsAccountConsumed>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountConsumed>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountConsumed>>(restResponse, data);
    }

    public RGameActionsAccountApi<List<GameActionsAccountConsumed>> ConsumedById(
      long? id,
      string uid)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling ConsumedById");
      string path = "/Game/Actions/Account/ConsumedById".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (id), (object) id));
      if (uid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (uid), (object) uid));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumedById: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ConsumedById: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<GameActionsAccountConsumed> data = (List<GameActionsAccountConsumed>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<GameActionsAccountConsumed>), restResponse.Headers);
      return new RGameActionsAccountApi<List<GameActionsAccountConsumed>>(restResponse, data);
    }

    public RGameActionsAccountApi<GameActionsAccountAvailable> Credit(
      long? definitionId,
      long? accountId,
      string externalType,
      long? externalId,
      long? game,
      long? serverId,
      long? userId,
      string content)
    {
      if (!definitionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'definitionId' when calling Credit");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling Credit");
      if (externalType == null)
        throw new ApiException(400, "Missing required parameter 'externalType' when calling Credit");
      if (!externalId.HasValue)
        throw new ApiException(400, "Missing required parameter 'externalId' when calling Credit");
      string path = "/Game/Actions/Account/Credit".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (definitionId.HasValue)
        formParams.Add("definition_id", this.ApiClient.ParameterToString((object) definitionId));
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (externalType != null)
        formParams.Add("external_type", this.ApiClient.ParameterToString((object) externalType));
      if (externalId.HasValue)
        formParams.Add("external_id", this.ApiClient.ParameterToString((object) externalId));
      if (game.HasValue)
        formParams.Add(nameof (game), this.ApiClient.ParameterToString((object) game));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (userId.HasValue)
        formParams.Add("user_id", this.ApiClient.ParameterToString((object) userId));
      if (content != null)
        formParams.Add(nameof (content), this.ApiClient.ParameterToString((object) content));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Credit: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Credit: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      GameActionsAccountAvailable data = (GameActionsAccountAvailable) this.ApiClient.Deserialize(restResponse.Content, typeof (GameActionsAccountAvailable), restResponse.Headers);
      return new RGameActionsAccountApi<GameActionsAccountAvailable>(restResponse, data);
    }

    public void Delete(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling Delete");
      string path = "/Game/Actions/Account/Delete".Replace("{format}", "json");
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
  }
}
