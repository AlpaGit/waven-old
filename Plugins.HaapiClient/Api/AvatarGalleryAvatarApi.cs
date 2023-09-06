// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AvatarGalleryAvatarApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using IO.Swagger.Client;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class AvatarGalleryAvatarApi : IAvatarGalleryAvatarApi
  {
    public AvatarGalleryAvatarApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AvatarGalleryAvatarApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void LinkAvatar(long? galleryId, long? avatarId)
    {
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling LinkAvatar");
      if (!avatarId.HasValue)
        throw new ApiException(400, "Missing required parameter 'avatarId' when calling LinkAvatar");
      string path = "/Avatar/GalleryAvatar/LinkAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      if (avatarId.HasValue)
        formParams.Add(nameof (avatarId), this.ApiClient.ParameterToString((object) avatarId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LinkAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LinkAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public void UnlinkAvatar(long? galleryId, long? avatarId)
    {
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling UnlinkAvatar");
      if (!avatarId.HasValue)
        throw new ApiException(400, "Missing required parameter 'avatarId' when calling UnlinkAvatar");
      string path = "/Avatar/GalleryAvatar/UnlinkAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      if (avatarId.HasValue)
        formParams.Add(nameof (avatarId), this.ApiClient.ParameterToString((object) avatarId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling UnlinkAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling UnlinkAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
