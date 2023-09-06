// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarUserCurrentAvatarApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarUserCurrentAvatarApi
  {
    RAvatarUserCurrentAvatarApi<AvatarModelAvatar> GetUserCurrentAvatar(long? userId);

    void PutUserCurrentAvatar(long? userId, string avatarUid, string characterInfos);
  }
}
