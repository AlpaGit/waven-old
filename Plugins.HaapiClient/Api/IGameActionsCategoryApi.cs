// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IGameActionsCategoryApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IGameActionsCategoryApi
  {
    void Delete(long? id);

    RGameActionsCategoryApi<List<GameActionsCategory>> Get(long? parentId);

    RGameActionsCategoryApi<GameActionsCategory> GetById(long? id);

    RGameActionsCategoryApi<GameActionsCategory> Insert(string name, long? parentId, long? gameId);

    RGameActionsCategoryApi<GameActionsCategory> Update(
      long? id,
      string name,
      long? parentId,
      long? gameId);
  }
}
