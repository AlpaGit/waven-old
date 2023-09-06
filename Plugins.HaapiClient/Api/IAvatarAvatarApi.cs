// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarAvatarApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarAvatarApi
  {
    void DeleteAvatar(long? avatarId);

    RAvatarAvatarApi<AvatarModelAvatar> GetAvatar(long? avatarId);

    RAvatarAvatarApi<List<AvatarModelAvatar>> GetAvatars(
      long? gameId,
      long? galleryId,
      long? offset,
      long? limit,
      string orderBy,
      string orderDirection);

    RAvatarAvatarApi<AvatarModelAvatar> PostAvatar(string gameId, Dictionary<string, string> image);

    RAvatarAvatarApi<AvatarModelAvatar> PutAvatar(
      long? avatarId,
      string gameId,
      Dictionary<string, string> image);
  }
}
