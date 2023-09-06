// Decompiled with JetBrains decompiler
// Type: Com.Ankama.Haapi.Swagger.Api.IMoneyApi
// Assembly: Plugins.HaapiClient, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 483BDDA8-3FAE-40DE-BAD8-0DEF21DA9780
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.HaapiClient.dll

using Com.Ankama.Haapi.Swagger.Model;

namespace Com.Ankama.Haapi.Swagger.Api
{
  public interface IMoneyApi
  {
    RMoneyApi<MoneyBalance> FragmentCredit(long? account, long? amount, long? game);

    RMoneyApi<MoneyBalance> FragmentsAmount();

    RMoneyApi<MoneyBalance> FragmentsAmountWithPassword(long? account);

    RMoneyApi<MoneyBalance> KrozAmount();

    RMoneyApi<MoneyBalance> KrozAmountWithPassword(long? account);

    RMoneyApi<MoneyBalance> KrozCredit(long? account, long? amount, long? game);

    RMoneyApi<MoneyBalance> OgrinsAmount();

    RMoneyApi<MoneyBalance> OgrinsAmountWithPassword(long? account);

    RMoneyApi<MoneyBalance> OgrinsDofusTouchAmount();

    RMoneyApi<MoneyBalance> OgrinsDofusTouchAmountWithPassword(long? account);
  }
}
