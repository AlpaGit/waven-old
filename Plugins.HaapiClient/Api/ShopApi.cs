// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ShopApi
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
  public class ShopApi : IShopApi
  {
    public ShopApi(ApiClient apiClient = null)
    {
      if (apiClient == null)
        this.ApiClient = Configuration.DefaultApiClient;
      else
        this.ApiClient = apiClient;
    }

    public ShopApi(string basePath) => this.ApiClient = new ApiClient(basePath);

    public void SetBasePath(string basePath) => this.ApiClient.BasePath = basePath;

    public string GetBasePath(string basePath) => this.ApiClient.BasePath;

    public ApiClient ApiClient { get; set; }

    public RShopApi<List<ShopArticle>> ArticlesListByCategory(
      long? categoryId,
      long? page,
      long? size)
    {
      if (!categoryId.HasValue)
        throw new ApiException(400, "Missing required parameter 'categoryId' when calling ArticlesListByCategory");
      string path = "/Shop/ArticlesListByCategory".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (categoryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "category_id", (object) categoryId));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByCategory: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByCategory: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<List<ShopArticle>> ArticlesListByCategoryKey(
      string key,
      long? page,
      long? size)
    {
      if (key == null)
        throw new ApiException(400, "Missing required parameter 'key' when calling ArticlesListByCategoryKey");
      string path = "/Shop/ArticlesListByCategoryKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (key != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (key), (object) key));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByCategoryKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByCategoryKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<List<ShopArticle>> ArticlesListByGondolahead(
      long? gondolaheadId,
      long? page,
      long? size)
    {
      if (!gondolaheadId.HasValue)
        throw new ApiException(400, "Missing required parameter 'gondolaheadId' when calling ArticlesListByGondolahead");
      string path = "/Shop/ArticlesListByGondolahead".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (gondolaheadId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "gondolahead_id", (object) gondolaheadId));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByGondolahead: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByGondolahead: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<List<ShopArticle>> ArticlesListByIds(List<long?> ids)
    {
      if (ids == null)
        throw new ApiException(400, "Missing required parameter 'ids' when calling ArticlesListByIds");
      string path = "/Shop/ArticlesListByIds".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (ids != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("multi", nameof (ids), (object) ids));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByIds: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByIds: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<List<ShopArticle>> ArticlesListByKey(List<string> key)
    {
      if (key == null)
        throw new ApiException(400, "Missing required parameter 'key' when calling ArticlesListByKey");
      string path = "/Shop/ArticlesListByKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (key != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("multi", nameof (key), (object) key));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListByKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<List<ShopArticle>> ArticlesListSearch(
      string text,
      List<long?> categoriesIds,
      long? page,
      long? size)
    {
      if (text == null)
        throw new ApiException(400, "Missing required parameter 'text' when calling ArticlesListSearch");
      string path = "/Shop/ArticlesListSearch".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (text != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (text), (object) text));
      if (categoriesIds != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("multi", "categories_ids", (object) categoriesIds));
      if (page.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (page), (object) page));
      if (size.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (size), (object) size));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListSearch: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling ArticlesListSearch: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopArticle> data = (List<ShopArticle>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopArticle>), restResponse.Headers);
      return new RShopApi<List<ShopArticle>>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> Buy(string data, string currency)
    {
      if (data == null)
        throw new ApiException(400, "Missing required parameter 'data' when calling Buy");
      string path = "/Shop/Buy".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (data != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (data), (object) data));
      if (currency != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (currency), (object) currency));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Buy: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Buy: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data1 = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data1);
    }

    public RShopApi<List<ShopCategory>> CategoriesList()
    {
      string path = "/Shop/CategoriesList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CategoriesList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CategoriesList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopCategory> data = (List<ShopCategory>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopCategory>), restResponse.Headers);
      return new RShopApi<List<ShopCategory>>(restResponse, data);
    }

    public RShopApi<List<ShopCategory>> CategoriesListByKey(string key)
    {
      if (key == null)
        throw new ApiException(400, "Missing required parameter 'key' when calling CategoriesListByKey");
      string path = "/Shop/CategoriesListByKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (key != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (key), (object) key));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CategoriesListByKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CategoriesListByKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopCategory> data = (List<ShopCategory>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopCategory>), restResponse.Headers);
      return new RShopApi<List<ShopCategory>>(restResponse, data);
    }

    public RShopApi<ApiKey> CreateApiKey(
      string shopKey,
      string lang,
      long? accountId,
      string ip,
      long? characterId,
      long? serverId,
      string country,
      string currency,
      string paymentMode,
      string partnerId)
    {
      if (shopKey == null)
        throw new ApiException(400, "Missing required parameter 'shopKey' when calling CreateApiKey");
      if (lang == null)
        throw new ApiException(400, "Missing required parameter 'lang' when calling CreateApiKey");
      if (!accountId.HasValue)
        throw new ApiException(400, "Missing required parameter 'accountId' when calling CreateApiKey");
      if (ip == null)
        throw new ApiException(400, "Missing required parameter 'ip' when calling CreateApiKey");
      string path = "/Shop/CreateApiKey".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (shopKey != null)
        formParams.Add("shop_key", this.ApiClient.ParameterToString((object) shopKey));
      if (lang != null)
        formParams.Add(nameof (lang), this.ApiClient.ParameterToString((object) lang));
      if (accountId.HasValue)
        formParams.Add("account_id", this.ApiClient.ParameterToString((object) accountId));
      if (ip != null)
        formParams.Add(nameof (ip), this.ApiClient.ParameterToString((object) ip));
      if (characterId.HasValue)
        formParams.Add("character_id", this.ApiClient.ParameterToString((object) characterId));
      if (serverId.HasValue)
        formParams.Add("server_id", this.ApiClient.ParameterToString((object) serverId));
      if (country != null)
        formParams.Add(nameof (country), this.ApiClient.ParameterToString((object) country));
      if (currency != null)
        formParams.Add(nameof (currency), this.ApiClient.ParameterToString((object) currency));
      if (paymentMode != null)
        formParams.Add("payment_mode", this.ApiClient.ParameterToString((object) paymentMode));
      if (partnerId != null)
        formParams.Add("partner_id", this.ApiClient.ParameterToString((object) partnerId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKey: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling CreateApiKey: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ApiKey data = (ApiKey) this.ApiClient.Deserialize(restResponse.Content, typeof (ApiKey), restResponse.Headers);
      return new RShopApi<ApiKey>(restResponse, data);
    }

    public RShopApi<List<ShopGondolaHead>> GondolaHeadsList(bool? home, long? categoryId)
    {
      string path = "/Shop/GondolaHeadsList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (home.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (home), (object) home));
      if (categoryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "category_id", (object) categoryId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GondolaHeadsList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling GondolaHeadsList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopGondolaHead> data = (List<ShopGondolaHead>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopGondolaHead>), restResponse.Headers);
      return new RShopApi<List<ShopGondolaHead>>(restResponse, data);
    }

    public RShopApi<List<ShopHighlight>> HightLightsList(string type, long? categoryId)
    {
      string path = "/Shop/HightLightsList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (type != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (type), (object) type));
      if (categoryId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "category_id", (object) categoryId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling HightLightsList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling HightLightsList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopHighlight> data = (List<ShopHighlight>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopHighlight>), restResponse.Headers);
      return new RShopApi<List<ShopHighlight>>(restResponse, data);
    }

    public RShopApi<ShopHome> Home()
    {
      string path = "/Shop/Home".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Home: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling Home: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopHome data = (ShopHome) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopHome), restResponse.Headers);
      return new RShopApi<ShopHome>(restResponse, data);
    }

    public RShopApi<ShopIAPsListResponse> IAPsList(string shopKey)
    {
      if (shopKey == null)
        throw new ApiException(400, "Missing required parameter 'shopKey' when calling IAPsList");
      string path = "/Shop/IAPsList".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (shopKey != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "shop_key", (object) shopKey));
      string[] authSettings = new string[0];
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling IAPsList: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling IAPsList: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopIAPsListResponse data = (ShopIAPsListResponse) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopIAPsListResponse), restResponse.Headers);
      return new RShopApi<ShopIAPsListResponse>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> MobileCancelOrder(long? orderId)
    {
      if (!orderId.HasValue)
        throw new ApiException(400, "Missing required parameter 'orderId' when calling MobileCancelOrder");
      string path = "/Shop/MobileCancelOrder".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (orderId.HasValue)
        formParams.Add("order_id", this.ApiClient.ParameterToString((object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileCancelOrder: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileCancelOrder: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> MobileGetOrderByReceipt(string receipt)
    {
      if (receipt == null)
        throw new ApiException(400, "Missing required parameter 'receipt' when calling MobileGetOrderByReceipt");
      string path = "/Shop/MobileGetOrderByReceipt".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (receipt != null)
        formParams.Add(nameof (receipt), this.ApiClient.ParameterToString((object) receipt));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileGetOrderByReceipt: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileGetOrderByReceipt: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> MobileValidateOrder(string receipt, long? orderId)
    {
      if (receipt == null)
        throw new ApiException(400, "Missing required parameter 'receipt' when calling MobileValidateOrder");
      string path = "/Shop/MobileValidateOrder".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (receipt != null)
        formParams.Add(nameof (receipt), this.ApiClient.ParameterToString((object) receipt));
      if (orderId.HasValue)
        formParams.Add("order_id", this.ApiClient.ParameterToString((object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileValidateOrder: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling MobileValidateOrder: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> OneClickBuy(string data, string currency, long? token)
    {
      if (data == null)
        throw new ApiException(400, "Missing required parameter 'data' when calling OneClickBuy");
      if (currency == null)
        throw new ApiException(400, "Missing required parameter 'currency' when calling OneClickBuy");
      if (!token.HasValue)
        throw new ApiException(400, "Missing required parameter 'token' when calling OneClickBuy");
      string path = "/Shop/OneClickBuy".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (data != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (data), (object) data));
      if (currency != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (currency), (object) currency));
      if (token.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (token), (object) token));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickBuy: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickBuy: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data1 = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data1);
    }

    public RShopApi<ShopPaymentHkCodeSendResult> OneClickSendCode(long? order)
    {
      if (!order.HasValue)
        throw new ApiException(400, "Missing required parameter 'order' when calling OneClickSendCode");
      string path = "/Shop/OneClickSendCode".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (order.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (order), (object) order));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickSendCode: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickSendCode: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopPaymentHkCodeSendResult data = (ShopPaymentHkCodeSendResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopPaymentHkCodeSendResult), restResponse.Headers);
      return new RShopApi<ShopPaymentHkCodeSendResult>(restResponse, data);
    }

    public RShopApi<List<ShopOneClickToken>> OneClickTokens()
    {
      string path = "/Shop/OneClickTokens".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickTokens: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickTokens: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopOneClickToken> data = (List<ShopOneClickToken>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopOneClickToken>), restResponse.Headers);
      return new RShopApi<List<ShopOneClickToken>>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> OneClickValidateOrder(long? orderId, string code)
    {
      if (!orderId.HasValue)
        throw new ApiException(400, "Missing required parameter 'orderId' when calling OneClickValidateOrder");
      if (code == null)
        throw new ApiException(400, "Missing required parameter 'code' when calling OneClickValidateOrder");
      string path = "/Shop/OneClickValidateOrder".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (orderId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_id", (object) orderId));
      if (code != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (code), (object) code));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickValidateOrder: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickValidateOrder: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> OneClickValidateOrderByServer(long? orderId)
    {
      if (!orderId.HasValue)
        throw new ApiException(400, "Missing required parameter 'orderId' when calling OneClickValidateOrderByServer");
      string path = "/Shop/OneClickValidateOrderByServer".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (orderId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_id", (object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthPassword"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickValidateOrderByServer: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling OneClickValidateOrderByServer: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> PartnerFinalizeTransaction(bool? finalize, long? orderId)
    {
      if (!finalize.HasValue)
        throw new ApiException(400, "Missing required parameter 'finalize' when calling PartnerFinalizeTransaction");
      string path = "/Shop/PartnerFinalizeTransaction".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (finalize.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (finalize), (object) finalize));
      if (orderId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "order_id", (object) orderId));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PartnerFinalizeTransaction: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PartnerFinalizeTransaction: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }

    public RShopApi<List<ShopOrder>> PendingOrders()
    {
      string path = "/Shop/PendingOrders".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PendingOrders: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling PendingOrders: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      List<ShopOrder> data = (List<ShopOrder>) this.ApiClient.Deserialize(restResponse.Content, typeof (List<ShopOrder>), restResponse.Headers);
      return new RShopApi<List<ShopOrder>>(restResponse, data);
    }

    public RShopApi<ShopBuyResult> SimpleBuy(
      long? articleId,
      long? quantity,
      float? amount,
      string currency)
    {
      if (!articleId.HasValue)
        throw new ApiException(400, "Missing required parameter 'articleId' when calling SimpleBuy");
      if (!quantity.HasValue)
        throw new ApiException(400, "Missing required parameter 'quantity' when calling SimpleBuy");
      string path = "/Shop/SimpleBuy".Replace("{format}", "json");
      List<KeyValuePair<string, string>> queryParams = new List<KeyValuePair<string, string>>();
      Dictionary<string, string> headerParams = new Dictionary<string, string>();
      Dictionary<string, string> formParams = new Dictionary<string, string>();
      Dictionary<string, FileParameter> fileParams = new Dictionary<string, FileParameter>();
      string postBody = (string) null;
      if (articleId.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", "article_id", (object) articleId));
      if (quantity.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (quantity), (object) quantity));
      if (amount.HasValue)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (amount), (object) amount));
      if (currency != null)
        queryParams.AddRange((IEnumerable<KeyValuePair<string, string>>) this.ApiClient.ParameterToKeyValuePairs("", nameof (currency), (object) currency));
      string[] authSettings = new string[1]
      {
        "AuthAnkamaApiKey"
      };
      IRestResponse restResponse = (IRestResponse) this.ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
      if (restResponse.StatusCode >= HttpStatusCode.BadRequest)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SimpleBuy: " + restResponse.Content, (object) restResponse.Content);
      if (restResponse.StatusCode == (HttpStatusCode) 0)
        throw new ApiException((int) restResponse.StatusCode, "Error calling SimpleBuy: " + restResponse.ErrorMessage, (object) restResponse.ErrorMessage);
      ShopBuyResult data = (ShopBuyResult) this.ApiClient.Deserialize(restResponse.Content, typeof (ShopBuyResult), restResponse.Headers);
      return new RShopApi<ShopBuyResult>(restResponse, data);
    }
  }
}
