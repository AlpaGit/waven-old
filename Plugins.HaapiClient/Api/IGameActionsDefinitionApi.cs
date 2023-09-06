// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IGameActionsDefinitionApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IGameActionsDefinitionApi
  {
    void Delete(long? id, long? accountId);

    RGameActionsDefinitionApi<List<GameActionsDefinition>> Get(
      long? page,
      long? count,
      long? categoryId,
      long? restrictionId,
      string orderExpression,
      string orderDirection,
      string search,
      long? game);

    RGameActionsDefinitionApi<GameActionsDefinition> GetById(long? id);

    RGameActionsDefinitionApi<GameActionsDefinition> GetByIdFromClient(long? id);

    RGameActionsDefinitionApi<GameActionsDefinition> GetByName(string name);

    RGameActionsDefinitionApi<GameActionsDefinition> GetByType(string type);

    RGameActionsDefinitionApi<GameActionsDefinition> Insert(
      long? accountId,
      long? categoryId,
      string name,
      string actions,
      long? restrictionId,
      DateTime? onlineDate,
      DateTime? offlineDate,
      List<string> definitionLang,
      List<string> definitionTitle,
      List<string> definitionDescription,
      DateTime? availableDate);

    RGameActionsDefinitionApi<GameActionsDefinition> Update(
      long? id,
      long? accountId,
      long? categoryId,
      string name,
      string actions,
      long? restrictionId,
      DateTime? onlineDate,
      DateTime? offlineDate,
      List<string> definitionLang,
      List<string> definitionTitle,
      List<string> definitionDescription,
      DateTime? availableDate);
  }
}
