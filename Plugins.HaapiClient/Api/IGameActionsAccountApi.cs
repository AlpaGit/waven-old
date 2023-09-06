// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IGameActionsAccountApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IGameActionsAccountApi
  {
    RGameActionsAccountApi<List<GameActionsAccountAvailable>> Available(
      long? accountId,
      long? game,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailableByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<GameActionsAccountAvailable> AvailableById(long? id, string uid);

    RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailablePendingByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsActionsMeta>> AvailableSimple(
      long? accountId,
      long? game,
      long? serverId,
      long? userId,
      string type);

    RGameActionsAccountApi<List<GameActionsAccountAvailable>> AvailableWithApiKey(
      long? game,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>> CancelAll(
      long? accountId,
      long? game,
      long? id,
      long? serverId,
      long? userId);

    void CancelOrder(long? orderId);

    RGameActionsAccountApi<GameActionsActionsDeliveredMeta> Consume(
      long? accountId,
      long? game,
      long? id,
      string uid,
      long? quantity,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsActionsDeliveredMeta>> ConsumeAll(
      long? accountId,
      long? game,
      long? id,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsAccountConsumed>> ConsumedByAccountId(
      long? accountId,
      long? game,
      long? serverId,
      long? userId);

    RGameActionsAccountApi<List<GameActionsAccountConsumed>> ConsumedById(long? id, string uid);

    RGameActionsAccountApi<GameActionsAccountAvailable> Credit(
      long? definitionId,
      long? accountId,
      string externalType,
      long? externalId,
      long? game,
      long? serverId,
      long? userId,
      string content);

    void Delete(long? id);
  }
}
