// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders.AbstractDynamicFightValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders
{
  public class AbstractDynamicFightValueProvider : IFightValueProvider, IValueProvider
  {
    public int level { get; }

    public IReadOnlyList<ILevelOnlyDependant> dynamicValues { get; }

    protected AbstractDynamicFightValueProvider(
      IReadOnlyList<ILevelOnlyDependant> dynamicValues,
      int level)
    {
      this.dynamicValues = dynamicValues;
      this.level = level;
    }

    public string GetValue(string name) => this.GetValueInt(name).ToString();

    public int GetValueInt(string name) => this.dynamicValues.GetValueInt(name, this.level);

    public virtual int GetDamageModifierValue() => 0;

    public virtual int GetHealModifierValue() => 0;

    public virtual Tuple<int, int> GetRange() => (Tuple<int, int>) null;
  }
}
