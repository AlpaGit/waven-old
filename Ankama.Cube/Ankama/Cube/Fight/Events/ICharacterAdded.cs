// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ICharacterAdded
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;

namespace Ankama.Cube.Fight.Events
{
  public interface ICharacterAdded
  {
    int entityDefId { get; }

    int ownerId { get; }

    CellCoord refCoord { get; }

    int direction { get; }

    int level { get; }
  }
}
