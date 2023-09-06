// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.DlcApi
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
  public class DlcApi : IDlcApi
  {
    public DlcApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public DlcApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void Delete(long? dlcId)
    {
      if (!dlcId.HasValue)
        throw new ApiException(400, "Missing required parameter 'dlcId' when calling Delete");
      string path = "/Dlc/Delete".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (dlcId.HasValue)
        formParams.Add("dlc_id", this.ApiClient.ParameterToString((object) dlcId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Delete: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Delete: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RDlcApi<SteamDLC> Get(long? dlcId)
    {
      if (!dlcId.HasValue)
        throw new ApiException(400, "Missing required parameter 'dlcId' when calling Get");
      string path = "/Dlc/Get".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (dlcId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "dlc_id", (object) dlcId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Get: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SteamDLC data = (SteamDLC) this.ApiClient.Deserialize(restResponse.Content, typeof (SteamDLC), restResponse.Headers);
      return new RDlcApi<SteamDLC>(restResponse, data);
    }

    public RDlcApi<List<SteamDLC>> GetList(long? gameId)
    {
      string path = "/Dlc/GetList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "game_id", (object) gameId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<SteamDLC> data = (List<SteamDLC>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<SteamDLC>), restResponse.Headers);
      return new RDlcApi<List<SteamDLC>>(restResponse, data);
    }

    public RDlcApi<SteamDLC> Save(long? gameId, string appId, long? definitionId, long? dlcId)
    {
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling Save");
      if (appId == null)
        throw new ApiException(400, "Missing required parameter 'appId' when calling Save");
      if (!definitionId.HasValue)
        throw new ApiException(400, "Missing required parameter 'definitionId' when calling Save");
      string path = "/Dlc/Save".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        formParams.Add("game_id", this.ApiClient.ParameterToString((object) gameId));
      if (appId != null)
        formParams.Add("app_id", this.ApiClient.ParameterToString((object) appId));
      if (definitionId.HasValue)
        formParams.Add("definition_id", this.ApiClient.ParameterToString((object) definitionId));
      if (dlcId.HasValue)
        formParams.Add("dlc_id", this.ApiClient.ParameterToString((object) dlcId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Save: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Save: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      SteamDLC data = (SteamDLC) this.ApiClient.Deserialize(restResponse.Content, typeof (SteamDLC), restResponse.Headers);
      return new RDlcApi<SteamDLC>(restResponse, data);
    }
  }
}
