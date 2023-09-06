// Decompiled with JetBrains decompiler
// Type: DeckEditDynamicList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;

public class DeckEditDynamicList : DynamicList
{
  protected override void InitCellRenderer(CellRenderer cellRenderer, bool andUpdate)
  {
    base.InitCellRenderer(cellRenderer, andUpdate);
    cellRenderer.gameObject.AddComponent<DeckEditItemPointerListener>();
  }
}
