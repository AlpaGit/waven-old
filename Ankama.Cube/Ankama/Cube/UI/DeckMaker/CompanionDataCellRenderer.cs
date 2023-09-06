// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.CompanionDataCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Collections.Generic;

namespace Ankama.Cube.UI.DeckMaker
{
  public class CompanionDataCellRenderer : 
    CompanionCellRenderer<CompanionData, ICompanionDataCellRendererConfigurator>
  {
    protected override IReadOnlyList<Cost> GetCosts() => this.m_value?.definition.cost;

    protected override bool IsAvailable()
    {
      ICompanionDataCellRendererConfigurator configurator = this.m_configurator;
      return configurator == null || configurator.IsCompanionDataAvailable(this.m_value);
    }

    protected override void SetValue(CompanionData val) => this.SetValue(val?.definition, val != null ? val.level : 0);
  }
}
