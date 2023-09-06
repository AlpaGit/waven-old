// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.ICompanionStatusCellRendererConfigurator
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight;

namespace Ankama.Cube.UI.DeckMaker
{
  public interface ICompanionStatusCellRendererConfigurator : 
    ICompanionCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator
  {
    IDragNDropValidator GetDragNDropValidator();

    bool IsParentInteractable();

    CastEventListener GetEventListener();

    CompanionStatusData? GetCompanionStatusData(ReserveCompanionStatus companion);
  }
}
