// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IGameActionsRestrictionApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IGameActionsRestrictionApi
  {
    void Delete(long? id);

    RGameActionsRestrictionApi<List<GameActionsRestriction>> Get();

    RGameActionsRestrictionApi<GameActionsRestriction> GetById(long? id);

    RGameActionsRestrictionApi<GameActionsRestriction> Insert(string name, string conditions);

    RGameActionsRestrictionApi<GameActionsRestriction> Update(
      long? id,
      string name,
      string conditions);
  }
}
