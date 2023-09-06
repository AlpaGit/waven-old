// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.KeywordTooltip
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.Windows
{
  public class KeywordTooltip : MonoBehaviour
  {
    [SerializeField]
    private FightTooltipContent m_content;
    [SerializeField]
    private CanvasGroup m_canvasGroup;

    public float alpha
    {
      get => this.m_canvasGroup.alpha;
      set => this.m_canvasGroup.alpha = value;
    }

    public void Initialize(ITooltipDataProvider tooltipDataProvider) => this.m_content.Initialize(tooltipDataProvider);
  }
}
