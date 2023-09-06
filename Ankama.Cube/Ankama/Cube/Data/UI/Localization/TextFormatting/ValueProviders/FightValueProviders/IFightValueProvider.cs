// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders.IFightValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders
{
  public interface IFightValueProvider : IValueProvider
  {
    int GetValueInt(string name);

    int GetDamageModifierValue();

    int GetHealModifierValue();

    Tuple<int, int> GetRange();

    int level { get; }
  }
}
