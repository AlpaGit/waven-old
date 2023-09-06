// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarUserGalleryApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarUserGalleryApi
  {
    RAvatarUserGalleryApi<List<AvatarModelGallery>> GetUserGalleries(
      long? userId,
      long? gameId,
      string locale);

    RAvatarUserGalleryApi<AvatarModelGallery> GetUserGallery(
      long? userId,
      string galleryUid,
      string locale);

    RAvatarUserGalleryApi<AvatarModelUserGallery> LinkUserGallery(long? userId, long? galleryId);

    void UnlinkUserGallery(long? userId, long? galleryId);
  }
}
