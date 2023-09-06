// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.AuthenticatorApi
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
  public class AuthenticatorApi : IAuthenticatorApi
  {
    public AuthenticatorApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public AuthenticatorApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public void AuthenticateToAdd(string login, string password, string lang)
    {
      string path = "/Authenticator/AuthenticateToAdd".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AuthenticateToAdd: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AuthenticateToAdd: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAuthenticatorApi<AuthenticatorAccount> AuthenticateToRemove(
      string login,
      string password,
      long? otpId,
      string dataCrypted,
      string lang)
    {
      if (login == null)
        throw new ApiException(400, "Missing required parameter 'login' when calling AuthenticateToRemove");
      if (password == null)
        throw new ApiException(400, "Missing required parameter 'password' when calling AuthenticateToRemove");
      if (!otpId.HasValue)
        throw new ApiException(400, "Missing required parameter 'otpId' when calling AuthenticateToRemove");
      if (dataCrypted == null)
        throw new ApiException(400, "Missing required parameter 'dataCrypted' when calling AuthenticateToRemove");
      string path = "/Authenticator/AuthenticateToRemove".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (login != null)
        formParams.Add(nameof (login), this.ApiClient.ParameterToString((object) login));
      if (password != null)
        formParams.Add(nameof (password), this.ApiClient.ParameterToString((object) password));
      if (otpId.HasValue)
        formParams.Add("otp_id", this.ApiClient.ParameterToString((object) otpId));
      if (dataCrypted != null)
        formParams.Add("data_crypted", this.ApiClient.ParameterToString((object) dataCrypted));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AuthenticateToRemove: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling AuthenticateToRemove: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AuthenticatorAccount data = (AuthenticatorAccount) this.ApiClient.Deserialize(restResponse.Content, typeof (AuthenticatorAccount), restResponse.Headers);
      return new RAuthenticatorApi<AuthenticatorAccount>(restResponse, data);
    }

    public void CheckDevice(long? deviceId, string appVersion, string lang)
    {
      if (!deviceId.HasValue)
        throw new ApiException(400, "Missing required parameter 'deviceId' when calling CheckDevice");
      string path = "/Authenticator/CheckDevice".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (deviceId.HasValue)
        formParams.Add("device_id", this.ApiClient.ParameterToString((object) deviceId));
      if (appVersion != null)
        formParams.Add("app_version", this.ApiClient.ParameterToString((object) appVersion));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CheckDevice: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CheckDevice: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAuthenticatorApi<AuthenticatorDevice> CreateDevice(
      string name,
      string type,
      string version,
      string appVersion,
      string uid,
      string lang)
    {
      if (name == null)
        throw new ApiException(400, "Missing required parameter 'name' when calling CreateDevice");
      if (type == null)
        throw new ApiException(400, "Missing required parameter 'type' when calling CreateDevice");
      if (version == null)
        throw new ApiException(400, "Missing required parameter 'version' when calling CreateDevice");
      string path = "/Authenticator/CreateDevice".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (name != null)
        formParams.Add(nameof (name), this.ApiClient.ParameterToString((object) name));
      if (type != null)
        formParams.Add(nameof (type), this.ApiClient.ParameterToString((object) type));
      if (version != null)
        formParams.Add(nameof (version), this.ApiClient.ParameterToString((object) version));
      if (appVersion != null)
        formParams.Add("app_version", this.ApiClient.ParameterToString((object) appVersion));
      if (uid != null)
        formParams.Add(nameof (uid), this.ApiClient.ParameterToString((object) uid));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateDevice: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateDevice: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AuthenticatorDevice data = (AuthenticatorDevice) this.ApiClient.Deserialize(restResponse.Content, typeof (AuthenticatorDevice), restResponse.Headers);
      return new RAuthenticatorApi<AuthenticatorDevice>(restResponse, data);
    }

    public RAuthenticatorApi<List<AuthenticatorHelp>> GetHelpViewData(string lang)
    {
      string path = "/Authenticator/GetHelpViewData".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetHelpViewData: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GetHelpViewData: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<AuthenticatorHelp> data = (List<AuthenticatorHelp>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<AuthenticatorHelp>), restResponse.Headers);
      return new RAuthenticatorApi<List<AuthenticatorHelp>>(restResponse, data);
    }

    public RAuthenticatorApi<AuthenticatorAccount> InsertOtpAccount(
      string code,
      long? deviceId,
      string lang)
    {
      if (code == null)
        throw new ApiException(400, "Missing required parameter 'code' when calling InsertOtpAccount");
      if (!deviceId.HasValue)
        throw new ApiException(400, "Missing required parameter 'deviceId' when calling InsertOtpAccount");
      string path = "/Authenticator/InsertOtpAccount".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (code != null)
        formParams.Add(nameof (code), this.ApiClient.ParameterToString((object) code));
      if (deviceId.HasValue)
        formParams.Add("device_id", this.ApiClient.ParameterToString((object) deviceId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InsertOtpAccount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling InsertOtpAccount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AuthenticatorAccount data = (AuthenticatorAccount) this.ApiClient.Deserialize(restResponse.Content, typeof (AuthenticatorAccount), restResponse.Headers);
      return new RAuthenticatorApi<AuthenticatorAccount>(restResponse, data);
    }

    public RAuthenticatorApi<AuthenticatorAccount> LoginEnabled(
      long? id,
      string dataCrypted,
      string lang)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling LoginEnabled");
      if (dataCrypted == null)
        throw new ApiException(400, "Missing required parameter 'dataCrypted' when calling LoginEnabled");
      string path = "/Authenticator/LoginEnabled".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        formParams.Add(nameof (id), this.ApiClient.ParameterToString((object) id));
      if (dataCrypted != null)
        formParams.Add("data_crypted", this.ApiClient.ParameterToString((object) dataCrypted));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LoginEnabled: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling LoginEnabled: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AuthenticatorAccount data = (AuthenticatorAccount) this.ApiClient.Deserialize(restResponse.Content, typeof (AuthenticatorAccount), restResponse.Headers);
      return new RAuthenticatorApi<AuthenticatorAccount>(restResponse, data);
    }

    public void RemoveOtpAccount(long? id, string code, long? deviceId, string lang)
    {
      if (!id.HasValue)
        throw new ApiException(400, "Missing required parameter 'id' when calling RemoveOtpAccount");
      if (code == null)
        throw new ApiException(400, "Missing required parameter 'code' when calling RemoveOtpAccount");
      if (!deviceId.HasValue)
        throw new ApiException(400, "Missing required parameter 'deviceId' when calling RemoveOtpAccount");
      string path = "/Authenticator/RemoveOtpAccount".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (id.HasValue)
        formParams.Add(nameof (id), this.ApiClient.ParameterToString((object) id));
      if (code != null)
        formParams.Add(nameof (code), this.ApiClient.ParameterToString((object) code));
      if (deviceId.HasValue)
        formParams.Add("device_id", this.ApiClient.ParameterToString((object) deviceId));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RemoveOtpAccount: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RemoveOtpAccount: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }

    public RAuthenticatorApi<AuthenticatorRestoreDevice> RestoreDevice(
      long? deleteDeviceId,
      string code,
      string lang)
    {
      if (!deleteDeviceId.HasValue)
        throw new ApiException(400, "Missing required parameter 'deleteDeviceId' when calling RestoreDevice");
      if (code == null)
        throw new ApiException(400, "Missing required parameter 'code' when calling RestoreDevice");
      string path = "/Authenticator/RestoreDevice".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (deleteDeviceId.HasValue)
        formParams.Add("delete_device_id", this.ApiClient.ParameterToString((object) deleteDeviceId));
      if (code != null)
        formParams.Add(nameof (code), this.ApiClient.ParameterToString((object) code));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RestoreDevice: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling RestoreDevice: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      AuthenticatorRestoreDevice data = (AuthenticatorRestoreDevice) this.ApiClient.Deserialize(restResponse.Content, typeof (AuthenticatorRestoreDevice), restResponse.Headers);
      return new RAuthenticatorApi<AuthenticatorRestoreDevice>(restResponse, data);
    }

    public void ValidateSecret(string response, string lang)
    {
      if (response == null)
        throw new ApiException(400, "Missing required parameter 'response' when calling ValidateSecret");
      string path = "/Authenticator/ValidateSecret".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (response != null)
        formParams.Add(nameof (response), this.ApiClient.ParameterToString((object) response));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateSecret: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ValidateSecret: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
    }
  }
}
