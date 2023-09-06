// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.States.BaseFightSelectionState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Demo.UI;
using Ankama.Cube.States;
using System;

namespace Ankama.Cube.Demo.States
{
  public abstract class BaseFightSelectionState : LoadSceneStateContext
  {
    public Action onUIOpeningFinished;

    public SlidingSide fromSide { get; set; }

    public SlidingSide toSide { get; set; }

    protected void GotoPreviousState() => (this.parent as MainStateDemo).GotoPreviousState((StateContext) this);
  }
}
