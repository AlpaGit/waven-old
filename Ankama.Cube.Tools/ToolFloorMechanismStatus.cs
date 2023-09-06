// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.ToolFloorMechanismStatus
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using Ankama.Cube.Data;

namespace Ankama.Cube.Tools
{
  public sealed class ToolFloorMechanismStatus : ToolCharacterStatus
  {
    public ToolFloorMechanismStatus(int id)
      : base(id)
    {
    }

    public override EntityType type { get; } = EntityType.FloorMechanism;

    public override bool blocksMovement { get; }
  }
}
