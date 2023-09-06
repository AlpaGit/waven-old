// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.AbstractEndGameState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using JetBrains.Annotations;
using System;

namespace Ankama.Cube.States
{
  public class AbstractEndGameState : LoadSceneStateContext
  {
    public Action onContinue;
    public bool allowTransition;

    public override bool AllowsTransition([CanBeNull] StateContext nextState) => this.allowTransition;
  }
}
