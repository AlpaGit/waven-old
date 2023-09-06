// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.BaseFightSelectionUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Demo.UI
{
  public abstract class BaseFightSelectionUI : AbstractUI
  {
    [SerializeField]
    protected PlayableDirector m_openDirector;
    [SerializeField]
    protected PlayableDirector m_closeDirector;

    protected void UpdateInactivityTime() => InactivityHandler.UpdateActivity();

    public abstract IEnumerator OpenFrom(SlidingSide side);

    public abstract IEnumerator CloseTo(SlidingSide side);
  }
}
