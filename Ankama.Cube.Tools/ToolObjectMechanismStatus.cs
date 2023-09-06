// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.ToolObjectMechanismStatus
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;

namespace Ankama.Cube.Tools
{
  public sealed class ToolObjectMechanismStatus : 
    ToolCharacterStatus,
    IEntityTargetableByAction,
    IEntityWithBoardPresence,
    IEntity
  {
    public ToolObjectMechanismStatus(int id)
      : base(id)
    {
    }

    public override EntityType type { get; } = EntityType.ObjectMechanism;

    public override bool blocksMovement { get; } = true;
  }
}
