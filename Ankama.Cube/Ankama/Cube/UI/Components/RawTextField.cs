// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.RawTextField
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [ExecuteInEditMode]
  public sealed class RawTextField : AbstractTextField
  {
    [UsedImplicitly]
    [SerializeField]
    private string m_formattedText = string.Empty;

    protected override string GetFormattedText() => this.m_formattedText;

    public void SetText(string text)
    {
      this.m_formattedText = text;
      this.RefreshText();
    }
  }
}
