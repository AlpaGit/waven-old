// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckBuildingEventController
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckBuildingEventController
  {
    public event Action<EditModeSelection> OnEditRequest;

    public event Action OnSelectRequest;

    public event Action<int, int> OnCloneRequest;

    public event Action OnDeleteRequest;

    public event Action OnSaveRequest;

    public event Action OnCancelRequest;

    public event Action OnCloseRequest;

    public event Action<DeckSlot> OnDeckSlotSelectionChanged;

    public void OnClone(int title, int desc)
    {
      Action<int, int> onCloneRequest = this.OnCloneRequest;
      if (onCloneRequest == null)
        return;
      onCloneRequest(title, desc);
    }

    public void OnEdit(EditModeSelection selection)
    {
      Action<EditModeSelection> onEditRequest = this.OnEditRequest;
      if (onEditRequest == null)
        return;
      onEditRequest(selection);
    }

    public void OnDelete()
    {
      Action onDeleteRequest = this.OnDeleteRequest;
      if (onDeleteRequest == null)
        return;
      onDeleteRequest();
    }

    public void OnSave()
    {
      Action onSaveRequest = this.OnSaveRequest;
      if (onSaveRequest == null)
        return;
      onSaveRequest();
    }

    public void OnCancel()
    {
      Action onCancelRequest = this.OnCancelRequest;
      if (onCancelRequest == null)
        return;
      onCancelRequest();
    }

    public void OnClose()
    {
      Action onCloseRequest = this.OnCloseRequest;
      if (onCloseRequest == null)
        return;
      onCloseRequest();
    }

    public void OnSelect()
    {
      Action onSelectRequest = this.OnSelectRequest;
      if (onSelectRequest == null)
        return;
      onSelectRequest();
    }

    public void OnDeckSlotSelectionChange(DeckSlot slot)
    {
      Action<DeckSlot> selectionChanged = this.OnDeckSlotSelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged(slot);
    }
  }
}
