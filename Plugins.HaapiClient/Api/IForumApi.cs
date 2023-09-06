// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IForumApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IForumApi
  {
    RForumApi<List<ForumPost>> PostsList(
      string forum,
      string lang,
      long? topicId,
      long? page,
      long? size,
      long? accountId);

    RForumApi<List<ForumTopic>> TopicsList(
      string forum,
      string lang,
      long? threadId,
      long? page,
      long? size,
      long? accountId);
  }
}
