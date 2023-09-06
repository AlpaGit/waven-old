// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AvatarGalleryApi
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
  public class AvatarGalleryApi : IAvatarGalleryApi
  {
    public AvatarGalleryApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AvatarGalleryApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void DeleteGallery(long? galleryId)
    {
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling DeleteGallery");
      string path = "/Avatar/Gallery/DeleteGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling DeleteGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAvatarGalleryApi<List<AvatarModelGallery>> GetGalleries(
      long? gameId,
      long? offset,
      long? limit,
      string orderBy,
      string orderDirection,
      string locale)
    {
      string path = "/Avatar/Gallery/GetGalleries".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (gameId), (object) gameId));
      if (offset.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (offset), (object) offset));
      if (limit.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (limit), (object) limit));
      if (orderBy != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (orderBy), (object) orderBy));
      if (orderDirection != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (orderDirection), (object) orderDirection));
      if (locale != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "_locale", (object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetGalleries: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetGalleries: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AvatarModelGallery> data = (List<AvatarModelGallery>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AvatarModelGallery>), restResponse.Headers);
      return new RAvatarGalleryApi<List<AvatarModelGallery>>(restResponse, data);
    }

    public RAvatarGalleryApi<AvatarModelGallery> GetGallery(long? galleryId, string locale)
    {
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling GetGallery");
      string path = "/Avatar/Gallery/GetGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (galleryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (galleryId), (object) galleryId));
      if (locale != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "_locale", (object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelGallery data = (AvatarModelGallery) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelGallery), restResponse.Headers);
      return new RAvatarGalleryApi<AvatarModelGallery>(restResponse, data);
    }

    public RAvatarGalleryApi<AvatarModelGallery> PostGallery(
      long? gameId,
      string title,
      bool? _public,
      string locale)
    {
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling PostGallery");
      if (title == null)
        throw new ApiException(400, "Missing required parameter 'title' when calling PostGallery");
      if (!_public.HasValue)
        throw new ApiException(400, "Missing required parameter '_public' when calling PostGallery");
      string path = "/Avatar/Gallery/PostGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gameId.HasValue)
        formParams.Add(nameof (gameId), this.ApiClient.ParameterToString((object) gameId));
      if (title != null)
        formParams.Add(nameof (title), this.ApiClient.ParameterToString((object) title));
      if (_public.HasValue)
        formParams.Add("public", this.ApiClient.ParameterToString((object) _public));
      if (locale != null)
        formParams.Add("_locale", this.ApiClient.ParameterToString((object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PostGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelGallery data = (AvatarModelGallery) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelGallery), restResponse.Headers);
      return new RAvatarGalleryApi<AvatarModelGallery>(restResponse, data);
    }

    public RAvatarGalleryApi<AvatarModelGallery> PutGallery(
      long? galleryId,
      long? gameId,
      string title,
      bool? _public,
      string locale)
    {
      if (!galleryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'galleryId' when calling PutGallery");
      if (!gameId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gameId' when calling PutGallery");
      if (title == null)
        throw new ApiException(400, "Missing required parameter 'title' when calling PutGallery");
      if (!_public.HasValue)
        throw new ApiException(400, "Missing required parameter '_public' when calling PutGallery");
      string path = "/Avatar/Gallery/PutGallery".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (galleryId.HasValue)
        formParams.Add(nameof (galleryId), this.ApiClient.ParameterToString((object) galleryId));
      if (gameId.HasValue)
        formParams.Add(nameof (gameId), this.ApiClient.ParameterToString((object) gameId));
      if (title != null)
        formParams.Add(nameof (title), this.ApiClient.ParameterToString((object) title));
      if (_public.HasValue)
        formParams.Add("public", this.ApiClient.ParameterToString((object) _public));
      if (locale != null)
        formParams.Add("_locale", this.ApiClient.ParameterToString((object) locale));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutGallery: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutGallery: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelGallery data = (AvatarModelGallery) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelGallery), restResponse.Headers);
      return new RAvatarGalleryApi<AvatarModelGallery>(restResponse, data);
    }
  }
}
