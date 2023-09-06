// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.TextField
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Utilities;
using UnityEngine;

namespace Ankama.Cube.UI.Components
{
  [ExecuteInEditMode]
  public sealed class TextField : AbstractTextField
  {
    [SerializeField]
    [TextKey]
    private int m_textKeyId;
    [SerializeField]
    private bool m_requiresValueProvider;
    private IValueProvider m_valueProvider;

    protected override string GetFormattedText() => this.m_textKeyId == 0 || this.m_requiresValueProvider && this.m_valueProvider == null ? string.Empty : RuntimeData.FormattedText(this.m_textKeyId, this.m_valueProvider);

    public void SetText(int textKeyId, IValueProvider valueProvider = null)
    {
      this.m_textKeyId = textKeyId;
      this.m_valueProvider = valueProvider;
      this.RefreshText();
    }

    public void SetText(string textKeyName, IValueProvider valueProvider = null)
    {
      int id;
      if (!RuntimeData.TryGetTextKeyId(textKeyName, out id))
      {
        Log.Warning("Could not found a text key named '" + textKeyName + "'.", 60, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\TextField.cs");
        this.m_textKeyId = 0;
        this.m_valueProvider = (IValueProvider) null;
      }
      else
        this.SetText(id, valueProvider);
    }
  }
}
