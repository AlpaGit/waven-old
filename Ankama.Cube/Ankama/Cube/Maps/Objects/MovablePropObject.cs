// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.MovablePropObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [UsedImplicitly]
  public sealed class MovablePropObject : MovableIsoObject
  {
    [SerializeField]
    private PropDefinition m_definition;

    public override IsoObjectDefinition definition
    {
      get => (IsoObjectDefinition) this.m_definition;
      protected set => this.m_definition = (PropDefinition) value;
    }
  }
}
