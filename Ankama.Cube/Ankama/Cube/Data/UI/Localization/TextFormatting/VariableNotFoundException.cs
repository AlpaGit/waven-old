// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.VariableNotFoundException
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class VariableNotFoundException : TextFormatterException
  {
    public VariableNotFoundException(string text, int index, string variableName)
      : base(text, index, "Variable '" + variableName + "' could not be found.")
    {
    }
  }
}
