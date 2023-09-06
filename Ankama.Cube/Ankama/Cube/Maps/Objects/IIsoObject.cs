// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IIsoObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public interface IIsoObject
  {
    [PublicAPI]
    GameObject gameObject { get; }

    [PublicAPI]
    Transform transform { get; }

    [PublicAPI]
    CellObject cellObject { get; }
  }
}
