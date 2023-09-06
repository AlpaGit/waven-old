// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ElementaryStates
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Data
{
  [RelatedToEvents(new EventCategory[] {EventCategory.ElementaryStateChanged})]
  [Serializable]
  public enum ElementaryStates
  {
    None = 1,
    Muddy = 2,
    Oiled = 3,
    Ventilated = 4,
    Wet = 5,
  }
}
