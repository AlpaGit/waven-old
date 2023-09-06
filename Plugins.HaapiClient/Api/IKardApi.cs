// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IKardApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IKardApi
  {
    RKardApi<KardTicket> ConsumeByCode(string code, string lang);

    RKardApi<List<KardKard>> ConsumeById(string lang, long? id, long? gameId);

    RKardApi<List<KardKard>> ConsumeByOrderId(string lang, long? orderId);

    RKardApi<List<KardKardStock>> GetByAccountId(long? accountId, string lang, long? collectionId);
  }
}
