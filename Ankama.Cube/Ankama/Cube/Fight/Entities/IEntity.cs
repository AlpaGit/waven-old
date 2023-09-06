// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.IEntity
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Entities
{
  public interface IEntity
  {
    int id { get; }

    EntityType type { get; }

    bool isDirty { get; }

    IReadOnlyCollection<PropertyId> properties { get; }

    void SetCarac(CaracId caracId, int value);

    int GetCarac(CaracId caracId, int defaultValue = 0);

    bool HasProperty(PropertyId property);

    bool HasAnyProperty(params PropertyId[] properties);
  }
}
