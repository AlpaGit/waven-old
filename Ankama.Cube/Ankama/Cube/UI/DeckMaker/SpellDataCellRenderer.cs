// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.SpellDataCellRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;

namespace Ankama.Cube.UI.DeckMaker
{
  public class SpellDataCellRenderer : 
    SpellCellRenderer<SpellData, ISpellDataCellRendererConfigurator>
  {
    protected override bool IsAvailable()
    {
      ISpellDataCellRendererConfigurator configurator = this.m_configurator;
      return configurator == null || configurator.IsSpellDataAvailable(this.m_value);
    }

    protected override int? GetAPCost() => this.m_value?.definition.GetCost((DynamicValueContext) null);

    protected override int? GetBaseAPCost() => this.m_value?.definition.GetCost((DynamicValueContext) null);

    protected override void SetValue(SpellData val) => this.SetValue(val?.definition, val != null ? val.level : 0);
  }
}
