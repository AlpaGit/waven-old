// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IAvatarGalleryApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;
using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IAvatarGalleryApi
  {
    void DeleteGallery(long? galleryId);

    RAvatarGalleryApi<List<AvatarModelGallery>> GetGalleries(
      long? gameId,
      long? offset,
      long? limit,
      string orderBy,
      string orderDirection,
      string locale);

    RAvatarGalleryApi<AvatarModelGallery> GetGallery(long? galleryId, string locale);

    RAvatarGalleryApi<AvatarModelGallery> PostGallery(
      long? gameId,
      string title,
      bool? _public,
      string locale);

    RAvatarGalleryApi<AvatarModelGallery> PutGallery(
      long? galleryId,
      long? gameId,
      string title,
      bool? _public,
      string locale);
  }
}
