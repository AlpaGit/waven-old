// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AvatarUserCurrentAvatarApi
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
  public class AvatarUserCurrentAvatarApi : IAvatarUserCurrentAvatarApi
  {
    public AvatarUserCurrentAvatarApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AvatarUserCurrentAvatarApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RAvatarUserCurrentAvatarApi<AvatarModelAvatar> GetUserCurrentAvatar(long? userId)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling GetUserCurrentAvatar");
      string path = "/Avatar/UserCurrentAvatar/GetUserCurrentAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (userId), (object) userId));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserCurrentAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetUserCurrentAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AvatarModelAvatar data = (AvatarModelAvatar) this.ApiClient.Deserialize(restResponse.Content, typeof (AvatarModelAvatar), restResponse.Headers);
      return new RAvatarUserCurrentAvatarApi<AvatarModelAvatar>(restResponse, data);
    }

    public void PutUserCurrentAvatar(long? userId, string avatarUid, string characterInfos)
    {
      if (!userId.HasValue)
        throw new ApiException(400, "Missing required parameter 'userId' when calling PutUserCurrentAvatar");
      if (avatarUid == null)
        throw new ApiException(400, "Missing required parameter 'avatarUid' when calling PutUserCurrentAvatar");
      string path = "/Avatar/UserCurrentAvatar/PutUserCurrentAvatar".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (userId.HasValue)
        formParams.Add(nameof (userId), this.ApiClient.ParameterToString((object) userId));
      if (avatarUid != null)
        formParams.Add(nameof (avatarUid), this.ApiClient.ParameterToString((object) avatarUid));
      if (characterInfos != null)
        formParams.Add(nameof (characterInfos), this.ApiClient.ParameterToString((object) characterInfos));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutUserCurrentAvatar: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PutUserCurrentAvatar: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
