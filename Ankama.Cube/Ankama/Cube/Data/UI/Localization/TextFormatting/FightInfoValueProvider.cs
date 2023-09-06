// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.FightInfoValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Fight.Info;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class FightInfoValueProvider : IValueProvider
  {
    [NotNull]
    private readonly FightInfoMessageRibbon InfoRoot;

    public FightInfoValueProvider(FightInfoMessageRibbon parent) => this.InfoRoot = parent;

    public string GetValue(string name)
    {
      int result;
      if (int.TryParse(name, out result))
      {
        IReadOnlyList<string> parameter = this.InfoRoot.GetParameter();
        if (result >= 0 && result < ((IReadOnlyCollection<string>) parameter).Count)
          return parameter[result];
      }
      return (string) null;
    }
  }
}
