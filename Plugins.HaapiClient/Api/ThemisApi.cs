// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ThemisApi
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
  public class ThemisApi : IThemisApi
  {
    public ThemisApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public ThemisApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void AddModel(bool? success, string _return, string description)
    {
      if (!success.HasValue)
        throw new ApiException(400, "Missing required parameter 'success' when calling AddModel");
      if (_return == null)
        throw new ApiException(400, "Missing required parameter '_return' when calling AddModel");
      if (description == null)
        throw new ApiException(400, "Missing required parameter 'description' when calling AddModel");
      string path = "/Themis/AddModel".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (success.HasValue)
        formParams.Add(nameof (success), this.ApiClient.ParameterToString((object) success));
      if (_return != null)
        formParams.Add("return", this.ApiClient.ParameterToString((object) _return));
      if (description != null)
        formParams.Add(nameof (description), this.ApiClient.ParameterToString((object) description));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AddModel: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AddModel: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RThemisApi<ThemisModel> GetLastModel()
    {
      string path = "/Themis/GetLastModel".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetLastModel: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetLastModel: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ThemisModel data = (ThemisModel) this.ApiClient.Deserialize(restResponse.Content, typeof (ThemisModel), restResponse.Headers);
      return new RThemisApi<ThemisModel>(restResponse, data);
    }

    public RThemisApi<ThemisModel> GetModelByID(long? id)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling GetModelByID");
      string path = "/Themis/GetModelByID".Replace("{format}", "json");
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
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetModelByID: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetModelByID: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ThemisModel data = (ThemisModel) this.ApiClient.Deserialize(restResponse.Content, typeof (ThemisModel), restResponse.Headers);
      return new RThemisApi<ThemisModel>(restResponse, data);
    }
  }
}
