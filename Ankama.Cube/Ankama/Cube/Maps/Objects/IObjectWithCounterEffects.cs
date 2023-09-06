// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IObjectWithCounterEffects
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps.Feedbacks;
using System.Collections;

namespace Ankama.Cube.Maps.Objects
{
  public interface IObjectWithCounterEffects
  {
    IEnumerator InitializeFloatingCounterEffect(
      FloatingCounterEffect floatingCounterEffect,
      int value);

    IEnumerator ChangeFloatingCounterEffect(FloatingCounterEffect floatingCounterEffect);

    IEnumerator RemoveFloatingCounterEffect();

    void ClearFloatingCounterEffect();

    FloatingCounterFeedback GetCurrentFloatingCounterFeedback();
  }
}
