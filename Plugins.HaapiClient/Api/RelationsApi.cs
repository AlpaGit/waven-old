// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.RelationsApi
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
  public class RelationsApi : IRelationsApi
  {
    public RelationsApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public RelationsApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void Accept(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling Accept");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling Accept");
      string path = "/Relations/Accept".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Accept: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Accept: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RRelationsApi<RelationGroup> AddGroup(long? accountId, string groupName)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling AddGroup");
      if (groupName == null)
        throw new ApiException(400, "Missing required parameter 'groupName' when calling AddGroup");
      string path = "/Relations/AddGroup".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (groupName != null)
        formParams.Add("group_name", this.ApiClient.ParameterToString((object) groupName));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AddGroup: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AddGroup: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      RelationGroup data = (RelationGroup) this.ApiClient.Deserialize(restResponse.Content, typeof (RelationGroup), restResponse.Headers);
      return new RRelationsApi<RelationGroup>(restResponse, data);
    }

    public void Alias(long? idFrom, long? idTo, string alias)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling Alias");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling Alias");
      string path = "/Relations/Alias".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      if (alias != null)
        formParams.Add(nameof (alias), this.ApiClient.ParameterToString((object) alias));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Alias: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Alias: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void Block(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling Block");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling Block");
      string path = "/Relations/Block".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Block: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Block: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void DeleteContact(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling DeleteContact");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling DeleteContact");
      string path = "/Relations/DeleteContact".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteContact: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteContact: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void DeleteGroup(long? accountId, string groupName)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling DeleteGroup");
      if (groupName == null)
        throw new ApiException(400, "Missing required parameter 'groupName' when calling DeleteGroup");
      string path = "/Relations/DeleteGroup".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (groupName != null)
        formParams.Add("group_name", this.ApiClient.ParameterToString((object) groupName));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGroup: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGroup: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void InviteId(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling InviteId");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling InviteId");
      string path = "/Relations/InviteId".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InviteId: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InviteId: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void InviteNickname(long? idFrom, string nicknameTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling InviteNickname");
      if (nicknameTo == null)
        throw new ApiException(400, "Missing required parameter 'nicknameTo' when calling InviteNickname");
      string path = "/Relations/InviteNickname".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (nicknameTo != null)
        formParams.Add("nickname_to", this.ApiClient.ParameterToString((object) nicknameTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InviteNickname: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InviteNickname: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RRelationsApi<List<RelationRelation>> ListBlocked(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ListBlocked");
      string path = "/Relations/ListBlocked".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListBlocked: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListBlocked: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<RelationRelation> data = (List<RelationRelation>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<RelationRelation>), restResponse.Headers);
      return new RRelationsApi<List<RelationRelation>>(restResponse, data);
    }

    public RRelationsApi<List<RelationRelation>> ListFriends(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ListFriends");
      string path = "/Relations/ListFriends".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListFriends: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListFriends: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<RelationRelation> data = (List<RelationRelation>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<RelationRelation>), restResponse.Headers);
      return new RRelationsApi<List<RelationRelation>>(restResponse, data);
    }

    public RRelationsApi<List<RelationGroup>> ListGroup(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ListGroup");
      string path = "/Relations/ListGroup".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListGroup: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListGroup: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<RelationGroup> data = (List<RelationGroup>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<RelationGroup>), restResponse.Headers);
      return new RRelationsApi<List<RelationGroup>>(restResponse, data);
    }

    public RRelationsApi<List<RelationRelation>> ListRequestsByMe(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ListRequestsByMe");
      string path = "/Relations/ListRequestsByMe".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListRequestsByMe: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListRequestsByMe: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<RelationRelation> data = (List<RelationRelation>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<RelationRelation>), restResponse.Headers);
      return new RRelationsApi<List<RelationRelation>>(restResponse, data);
    }

    public RRelationsApi<List<RelationRelation>> ListRequestsForMe(long? accountId)
    {
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling ListRequestsForMe");
      string path = "/Relations/ListRequestsForMe".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (accountId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "account_id", (object) accountId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListRequestsForMe: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ListRequestsForMe: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<RelationRelation> data = (List<RelationRelation>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<RelationRelation>), restResponse.Headers);
      return new RRelationsApi<List<RelationRelation>>(restResponse, data);
    }

    public void Refuse(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling Refuse");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling Refuse");
      string path = "/Relations/Refuse".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Refuse: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Refuse: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RRelationsApi<RelationGroup> SetContactGroup(long? idFrom, long? idTo, string groupName)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling SetContactGroup");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling SetContactGroup");
      string path = "/Relations/SetContactGroup".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      if (groupName != null)
        formParams.Add("group_name", this.ApiClient.ParameterToString((object) groupName));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetContactGroup: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SetContactGroup: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      RelationGroup data = (RelationGroup) this.ApiClient.Deserialize(restResponse.Content, typeof (RelationGroup), restResponse.Headers);
      return new RRelationsApi<RelationGroup>(restResponse, data);
    }

    public void Unblock(long? idFrom, long? idTo)
    {
      if (!idFrom.HasValue)
        throw new ApiException(400, "Missing required parameter 'idFrom' when calling Unblock");
      if (!idTo.HasValue)
        throw new ApiException(400, "Missing required parameter 'idTo' when calling Unblock");
      string path = "/Relations/Unblock".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (idFrom.HasValue)
        formParams.Add("id_from", this.ApiClient.ParameterToString((object) idFrom));
      if (idTo.HasValue)
        formParams.Add("id_to", this.ApiClient.ParameterToString((object) idTo));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Unblock: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Unblock: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
