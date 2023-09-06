// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AvatarAvatarApi
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
  public class AvatarAvatarApi : IAvatarAvatarApi
  {
    public AvatarAvatarApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AvatarAvatarApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void DeleteAvatar(long? avatarId)
    {
      if (!avatarId.HasValue)
        throw new ApiException(400, "Missing required parameter 'avatarId' when calling DeleteAvatar");
      string path = "/Avatar/Avatar/DeleteAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (avatarId.HasValue)
        formParams.Add(nameof (avatarId), this.ApiClient.ParameterToString((object) avatarId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAvatarAvatarApi<AvatarModelAvatar> GetAvatar(long? avatarId)
    {
      if (!avatarId.HasValue)
        throw new ApiException(400, "Missing required parameter 'avatarId' when calling GetAvatar");
      string path = "/Avatar/Avatar/GetAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (avatarId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (avatarId), (object) avatarId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelAvatar data = (AvatarModelAvatar) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelAvatar), restResponse.Headers);
      return new RAvatarAvatarApi<AvatarModelAvatar>(restResponse, data);
    }

    public RAvatarAvatarApi<List<AvatarModelAvatar>> GetAvatars(
      long? gameId,
      long? galleryId,
      long? offset,
      long? limit,
      string orderBy,
      string orderDirection)
    {
      string path = "/Avatar/Avatar/GetAvatars".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (gameId), (object) gameId));
      if (galleryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (galleryId), (object) galleryId));
      if (offset.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (offset), (object) offset));
      if (limit.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (limit), (object) limit));
      if (orderBy != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (orderBy), (object) orderBy));
      if (orderDirection != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (orderDirection), (object) orderDirection));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetAvatars: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetAvatars: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AvatarModelAvatar> data = (List<AvatarModelAvatar>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AvatarModelAvatar>), restResponse.Headers);
      return new RAvatarAvatarApi<List<AvatarModelAvatar>>(restResponse, data);
    }

    public RAvatarAvatarApi<AvatarModelAvatar> PostAvatar(
      string gameId,
      Dictionary<string, string> image)
    {
      if (gameId == null)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling PostAvatar");
      string path = "/Avatar/Avatar/PostAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId != null)
        formParams.Add(nameof (gameId), this.ApiClient.ParameterToString((object) gameId));
      if (image != null)
        formParams.Add(nameof (image), this.ApiClient.ParameterToString((object) image));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelAvatar data = (AvatarModelAvatar) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelAvatar), restResponse.Headers);
      return new RAvatarAvatarApi<AvatarModelAvatar>(restResponse, data);
    }

    public RAvatarAvatarApi<AvatarModelAvatar> PutAvatar(
      long? avatarId,
      string gameId,
      Dictionary<string, string> image)
    {
      if (!avatarId.HasValue)
        throw new ApiException(400, "Missing required parameter 'avatarId' when calling PutAvatar");
      if (gameId == null)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling PutAvatar");
      string path = "/Avatar/Avatar/PutAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (avatarId.HasValue)
        formParams.Add(nameof (avatarId), this.ApiClient.ParameterToString((object) avatarId));
      if (gameId != null)
        formParams.Add(nameof (gameId), this.ApiClient.ParameterToString((object) gameId));
      if (image != null)
        formParams.Add(nameof (image), this.ApiClient.ParameterToString((object) image));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelAvatar data = (AvatarModelAvatar) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelAvatar), restResponse.Headers);
      return new RAvatarAvatarApi<AvatarModelAvatar>(restResponse, data);
    }
  }
}
