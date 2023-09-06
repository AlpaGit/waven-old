// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ICastTargetDefinition
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using DataEditor;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public interface ICastTargetDefinition : IEditableContent
  {
    CastTargetContext CreateCastTargetContext(
      FightStatus fightStatus,
      int playerId,
      DynamicValueHolderType type,
      int definitionId,
      int level,
      int instanceId);

    int count { get; }

    IEnumerable<Target> EnumerateTargets([NotNull] CastTargetContext castTargetContext);
  }
}
