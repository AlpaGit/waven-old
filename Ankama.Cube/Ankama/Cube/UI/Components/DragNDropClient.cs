// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.DragNDropClient
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components
{
  public interface DragNDropClient
  {
    bool activeInHierarchy { get; }

    RectTransform rectTransform { get; }

    void OnDragOver(CellRenderer cellRenderer, PointerEventData evt);

    bool OnDropOut(CellRenderer cellRenderer, PointerEventData evt);

    bool OnDrop(CellRenderer cellRenderer, PointerEventData evt);
  }
}
