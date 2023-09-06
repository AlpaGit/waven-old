// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.Windows.FightTooltip
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.Windows
{
  public class FightTooltip : AbstractTooltipWindow
  {
    [SerializeField]
    private FightTooltipContent m_content;
    [SerializeField]
    private KeywordTooltipContainer m_keywordsContainer;

    public override void Awake()
    {
      base.Awake();
      this.m_content.Setup();
    }

    public void Initialize([NotNull] ITooltipDataProvider dataProvider)
    {
      this.m_content.Initialize(dataProvider);
      this.m_keywordsContainer.Initialize(dataProvider);
    }

    protected override void DisplayTooltip(Vector3 worldPosition)
    {
      base.DisplayTooltip(worldPosition);
      this.SetKeywordContainerAlignment();
      this.m_keywordsContainer.Show();
    }

    private void SetKeywordContainerAlignment()
    {
      RectTransform transform = (RectTransform) this.transform;
      Vector2 pivot = transform.pivot;
      float num = transform.lossyScale.x * (this.m_keywordsContainer.width + this.m_keywordsContainer.spacing);
      KeywordTooltipContainer.HorizontalAlignment h = (double) pivot.x > 0.5 ? KeywordTooltipContainer.HorizontalAlignment.Left : KeywordTooltipContainer.HorizontalAlignment.Right;
      KeywordTooltipContainer.VerticalAlignment v = (double) pivot.y > 0.5 ? KeywordTooltipContainer.VerticalAlignment.Up : KeywordTooltipContainer.VerticalAlignment.Down;
      if ((double) this.borderDistanceToScreen.right < (double) num)
        h = KeywordTooltipContainer.HorizontalAlignment.Left;
      if ((double) this.borderDistanceToScreen.left < (double) num)
        h = KeywordTooltipContainer.HorizontalAlignment.Right;
      this.m_keywordsContainer.SetAlignement(h, v);
    }

    public override void Hide()
    {
      base.Hide();
      this.m_keywordsContainer.Hide();
    }
  }
}
