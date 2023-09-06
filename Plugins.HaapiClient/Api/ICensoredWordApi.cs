// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.ICensoredWordApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using System.Collections.Generic;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface ICensoredWordApi
  {
    RCensoredWordApi<bool?> IsCensoredWord(string word, string lang, List<string> applies);

    RCensoredWordApi<string> ReplaceCensoredWord(
      string sentence,
      string lang,
      List<string> applies);
  }
}
