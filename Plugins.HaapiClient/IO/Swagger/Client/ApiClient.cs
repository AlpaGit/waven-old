// Decompiled with JetBrains decompiler
// Type: IO.Swagger.Client.ApiClient
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Newtonsoft.Json;
using RestSharp;
using RestSharp.Contrib;
using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IO.Swagger.Client
{
  public class ApiClient
  {
    private readonly Dictionary<string, string> _defaultHeaderMap = new Dictionary<string, string>();

    public ApiClient(string basePath = "https://haapi.ankama.lan/json/Ankama/v2")
    {
      this.BasePath = basePath;
      this.RestClient = new RestClient(this.BasePath);
    }

    public string BasePath { get; set; }

    public RestClient RestClient { get; set; }

    public Dictionary<string, string> DefaultHeader => this._defaultHeaderMap;

    public object CallApi(
      string path,
      Method method,
      List<KeyValuePair<string, string>> queryParams,
      string postBody,
      Dictionary<string, string> headerParams,
      Dictionary<string, string> formParams,
      Dictionary<string, FileParameter> fileParams,
      string[] authSettings)
    {
      RestRequest request = new RestRequest(path, method);
      this.UpdateParamsForAuth(queryParams, headerParams, authSettings);
      foreach (KeyValuePair<string, string> defaultHeader in this._defaultHeaderMap)
        request.AddHeader(defaultHeader.Key, defaultHeader.Value);
      foreach (KeyValuePair<string, string> headerParam in headerParams)
        request.AddHeader(headerParam.Key, headerParam.Value);
      foreach (KeyValuePair<string, string> queryParam in queryParams)
        request.AddParameter(queryParam.Key, (object) queryParam.Value, ParameterType.GetOrPost);
      foreach (KeyValuePair<string, string> formParam in formParams)
        request.AddParameter(formParam.Key, (object) formParam.Value, ParameterType.GetOrPost);
      foreach (KeyValuePair<string, FileParameter> fileParam in fileParams)
        request.AddFile(fileParam.Value.Name, fileParam.Value.Writer, fileParam.Value.FileName, fileParam.Value.ContentType);
      if (postBody != null)
        request.AddParameter("application/json", (object) postBody, ParameterType.RequestBody);
      return (object) this.RestClient.Execute((IRestRequest) request);
    }

    public void AddDefaultHeader(string key, string value) => this._defaultHeaderMap.Add(key, value);

    public string EscapeString(string str) => HttpUtility.UrlEncode(str);

    public FileParameter ParameterToFile(string name, Stream stream) => stream is FileStream ? FileParameter.Create(name, stream.ReadAsBytes(), Path.GetFileName(((FileStream) stream).Name)) : FileParameter.Create(name, stream.ReadAsBytes(), "no_file_name_provided");

    public string ParameterToString(object obj)
    {
      switch (obj)
      {
        case DateTime dateTime:
          return dateTime.ToString(Configuration.DateTimeFormat);
        case List<string> _:
          return string.Join(",", (obj as List<string>).ToArray());
        default:
          return Convert.ToString(obj);
      }
    }

    public IReadOnlyList<KeyValuePair<string, string>> ParameterToKeyValuePairs(
      string collectionFormat,
      string name,
      object value)
    {
      List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
      if (ApiClient.IsCollection(value) && collectionFormat == "multi")
      {
        IEnumerable source = value as IEnumerable;
        keyValuePairs.AddRange(source.Cast<object>().Select<object, KeyValuePair<string, string>>((Func<object, KeyValuePair<string, string>>) (item => new KeyValuePair<string, string>(name, this.ParameterToString(item)))));
      }
      else
        keyValuePairs.Add(new KeyValuePair<string, string>(name, this.ParameterToString(value)));
      return (IReadOnlyList<KeyValuePair<string, string>>) keyValuePairs;
    }

    private static bool IsCollection(object value) => value is IList || value is ICollection;

    public object Deserialize(string content, Type type, IList<Parameter> headers = null)
    {
      if (type == typeof (object))
        return (object) content;
      if (type == typeof (Stream))
      {
        string str = string.IsNullOrEmpty(Configuration.TempFolderPath) ? Path.GetTempPath() : Configuration.TempFolderPath;
        string path = str + (object) Guid.NewGuid();
        if (headers != null)
        {
          Match match = new Regex("Content-Disposition:.*filename=['\"]?([^'\"\\s]+)['\"]?$").Match(headers.ToString());
          if (match.Success)
            path = str + match.Value.Replace("\"", "").Replace("'", "");
        }
        File.WriteAllText(path, content);
        return (object) new FileStream(path, FileMode.Open);
      }
      if (type.Name.StartsWith("System.Nullable`1[[System.DateTime"))
        return (object) DateTime.Parse(content, (IFormatProvider) null, DateTimeStyles.RoundtripKind);
      if (!(type == typeof (string)))
      {
        if (!type.Name.StartsWith("System.Nullable"))
        {
          try
          {
            return JsonConvert.DeserializeObject(content, type);
          }
          catch (IOException ex)
          {
            throw new ApiException(500, ex.Message);
          }
        }
      }
      return ApiClient.ConvertType((object) content, type);
    }

    public string Serialize(object obj)
    {
      try
      {
        return obj != null ? JsonConvert.SerializeObject(obj) : (string) null;
      }
      catch (Exception ex)
      {
        throw new ApiException(500, ex.Message);
      }
    }

    public string GetApiKeyWithPrefix(string apiKeyIdentifier)
    {
      string str1 = "";
      Configuration.ApiKey.TryGetValue(apiKeyIdentifier, out str1);
      string str2 = "";
      return Configuration.ApiKeyPrefix.TryGetValue(apiKeyIdentifier, out str2) ? str2 + " " + str1 : str1;
    }

    public void UpdateParamsForAuth(
      List<KeyValuePair<string, string>> queryParams,
      Dictionary<string, string> headerParams,
      string[] authSettings)
    {
      if (authSettings == null || authSettings.Length == 0)
        return;
      foreach (string authSetting in authSettings)
      {
        switch (authSetting)
        {
          case "AuthAnkamaApiKey":
            headerParams["APIKEY"] = this.GetApiKeyWithPrefix("APIKEY");
            break;
          case "AuthPassword":
            headerParams["Authorization"] = "Basic " + ApiClient.Base64Encode(Configuration.Username + ":" + Configuration.Password);
            break;
        }
      }
    }

    public static string Base64Encode(string text) => Convert.ToBase64String(Encoding.UTF8.GetBytes(text));

    public static object ConvertType(object fromObject, Type toObject) => Convert.ChangeType(fromObject, toObject);
  }
}
