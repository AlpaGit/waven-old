// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IShopApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IShopApi
  {
    RShopApi<List<ShopArticle>> ArticlesListByCategory(long? categoryId, long? page, long? size);

    RShopApi<List<ShopArticle>> ArticlesListByCategoryKey(string key, long? page, long? size);

    RShopApi<List<ShopArticle>> ArticlesListByGondolahead(
      long? gondolaheadId,
      long? page,
      long? size);

    RShopApi<List<ShopArticle>> ArticlesListByIds(List<long?> ids);

    RShopApi<List<ShopArticle>> ArticlesListByKey(List<string> key);

    RShopApi<List<ShopArticle>> ArticlesListSearch(
      string text,
      List<long?> categoriesIds,
      long? page,
      long? size);

    RShopApi<ShopBuyResult> Buy(string data, string currency);

    RShopApi<List<ShopCategory>> CategoriesList();

    RShopApi<List<ShopCategory>> CategoriesListByKey(string key);

    RShopApi<ApiKey> CreateApiKey(
      string shopKey,
      string lang,
      long? accountId,
      string ip,
      long? characterId,
      long? serverId,
      string country,
      string currency,
      string paymentMode,
      string partnerId);

    RShopApi<List<ShopGondolaHead>> GondolaHeadsList(bool? home, long? categoryId);

    RShopApi<List<ShopHighlight>> HightLightsList(string type, long? categoryId);

    RShopApi<ShopHome> Home();

    RShopApi<ShopIAPsListResponse> IAPsList(string shopKey);

    RShopApi<ShopBuyResult> MobileCancelOrder(long? orderId);

    RShopApi<ShopBuyResult> MobileGetOrderByReceipt(string receipt);

    RShopApi<ShopBuyResult> MobileValidateOrder(string receipt, long? orderId);

    RShopApi<ShopBuyResult> OneClickBuy(string data, string currency, long? token);

    RShopApi<ShopPaymentHkCodeSendResult> OneClickSendCode(long? order);

    RShopApi<List<ShopOneClickToken>> OneClickTokens();

    RShopApi<ShopBuyResult> OneClickValidateOrder(long? orderId, string code);

    RShopApi<ShopBuyResult> OneClickValidateOrderByServer(long? orderId);

    RShopApi<ShopBuyResult> PartnerFinalizeTransaction(bool? finalize, long? orderId);

    RShopApi<List<ShopOrder>> PendingOrders();

    RShopApi<ShopBuyResult> SimpleBuy(
      long? articleId,
      long? quantity,
      float? amount,
      string currency);
  }
}
