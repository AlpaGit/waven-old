// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AvatarUserGalleryApi
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
  public class AvatarUserGalleryApi : IAvatarUserGalleryApi
  {
    public AvatarUserGalleryApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AvatarUserGalleryApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RAvatarUserGalleryApi<List<AvatarModelGallery>> GetUserGalleries(
      long? userId,
      long? gameId,
      string locale)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling GetUserGalleries");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling GetUserGalleries");
      string path = "/Avatar/UserGallery/GetUserGalleries".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (userId), (object) userId));
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (gameId), (object) gameId));
      if (locale != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "_locale", (object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserGalleries: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserGalleries: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AvatarModelGallery> data = (List<AvatarModelGallery>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AvatarModelGallery>), restResponse.Headers);
      return new RAvatarUserGalleryApi<List<AvatarModelGallery>>(restResponse, data);
    }

    public RAvatarUserGalleryApi<AvatarModelGallery> GetUserGallery(
      long? userId,
      string galleryUid,
      string locale)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling GetUserGallery");
      if (galleryUid == null)
        throw new ApiException(400, "Missing required parameter 'galleryUid' when calling GetUserGallery");
      string path = "/Avatar/UserGallery/GetUserGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (userId), (object) userId));
      if (galleryUid != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (galleryUid), (object) galleryUid));
      if (locale != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "_locale", (object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelGallery data = (AvatarModelGallery) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelGallery), restResponse.Headers);
      return new RAvatarUserGalleryApi<AvatarModelGallery>(restResponse, data);
    }

    public RAvatarUserGalleryApi<AvatarModelUserGallery> LinkUserGallery(
      long? userId,
      long? galleryId)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling LinkUserGallery");
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling LinkUserGallery");
      string path = "/Avatar/UserGallery/LinkUserGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        formParams.Add(nameof (userId), this.ApiClient.ParameterToString((object) userId));
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LinkUserGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LinkUserGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelUserGallery data = (AvatarModelUserGallery) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelUserGallery), restResponse.Headers);
      return new RAvatarUserGalleryApi<AvatarModelUserGallery>(restResponse, data);
    }

    public void UnlinkUserGallery(long? userId, long? galleryId)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling UnlinkUserGallery");
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling UnlinkUserGallery");
      string path = "/Avatar/UserGallery/UnlinkUserGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        formParams.Add(nameof (userId), this.ApiClient.ParameterToString((object) userId));
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling UnlinkUserGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling UnlinkUserGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
