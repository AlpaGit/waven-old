// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarUserAvatarApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarUserAvatarApi
  {
    RAvatarUserAvatarApi<AvatarModelAvatar> GetUserAvatars(long? userId, long? gameId);

    RAvatarUserAvatarApi<AvatarModelUserAvatar> LinkUserAvatar(long? userId, long? avatarId);

    void UnlinkUserAvatar(long? userId, long? avatarId);
  }
}
