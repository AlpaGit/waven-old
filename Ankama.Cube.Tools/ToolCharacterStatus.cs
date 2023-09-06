// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.ToolCharacterStatus
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;

namespace Ankama.Cube.Tools
{
  public abstract class ToolCharacterStatus : 
    EntityStatus,
    IEntityWithLevel,
    IEntity,
    IEntityWithBoardPresence
  {
    protected ToolCharacterStatus(int id)
      : base(id)
    {
    }

    public int level { get; set; }

    public Area area { get; set; }

    public IsoObject view { get; set; }

    public abstract bool blocksMovement { get; }
  }
}
