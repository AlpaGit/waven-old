// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarGalleryAvatarApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarGalleryAvatarApi
  {
    void LinkAvatar(long? galleryId, long? avatarId);

    void UnlinkAvatar(long? galleryId, long? avatarId);
  }
}
