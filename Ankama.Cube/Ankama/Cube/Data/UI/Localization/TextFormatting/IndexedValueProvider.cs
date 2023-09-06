// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.IndexedValueProvider
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class IndexedValueProvider : IValueProvider
  {
    [NotNull]
    private readonly string[] m_args;

    public IndexedValueProvider([NotNull] params string[] args) => this.m_args = args;

    public string GetValue(string name)
    {
      int result;
      return int.TryParse(name, out result) && result >= 0 && result < this.m_args.Length ? this.m_args[result] : (string) null;
    }
  }
}
