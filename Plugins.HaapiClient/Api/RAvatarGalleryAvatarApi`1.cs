﻿// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.RAvatarGalleryAvatarApi`1
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using RestSharp;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public class RAvatarGalleryAvatarApi<T>
  {
    public readonly IRestResponse RestResponse;
    public readonly T Data;

    public RAvatarGalleryAvatarApi(IRestResponse restResponse, T data)
    {
      this.RestResponse = restResponse;
      this.Data = data;
    }
  }
}
