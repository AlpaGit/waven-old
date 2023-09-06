// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Entities.IEntityWithAction
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;

namespace Ankama.Cube.Fight.Entities
{
  public interface IEntityWithAction : IEntity
  {
    int? actionValue { get; }

    ActionType actionType { get; }

    bool hasRange { get; }

    int rangeMin { get; }

    int rangeMax { get; }

    int physicalDamageBoost { get; }

    int physicalHealBoost { get; }

    bool actionUsed { get; set; }

    bool canDoActionOnTarget { get; }

    IEntitySelector customActionTarget { get; }
  }
}
