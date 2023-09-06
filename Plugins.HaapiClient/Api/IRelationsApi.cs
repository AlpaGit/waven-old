// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IRelationsApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IRelationsApi
  {
    void Accept(long? idFrom, long? idTo);

    RRelationsApi<RelationGroup> AddGroup(long? accountId, string groupName);

    void Alias(long? idFrom, long? idTo, string alias);

    void Block(long? idFrom, long? idTo);

    void DeleteContact(long? idFrom, long? idTo);

    void DeleteGroup(long? accountId, string groupName);

    void InviteId(long? idFrom, long? idTo);

    void InviteNickname(long? idFrom, string nicknameTo);

    RRelationsApi<List<RelationRelation>> ListBlocked(long? accountId);

    RRelationsApi<List<RelationRelation>> ListFriends(long? accountId);

    RRelationsApi<List<RelationGroup>> ListGroup(long? accountId);

    RRelationsApi<List<RelationRelation>> ListRequestsByMe(long? accountId);

    RRelationsApi<List<RelationRelation>> ListRequestsForMe(long? accountId);

    void Refuse(long? idFrom, long? idTo);

    RRelationsApi<RelationGroup> SetContactGroup(long? idFrom, long? idTo, string groupName);

    void Unblock(long? idFrom, long? idTo);
  }
}
