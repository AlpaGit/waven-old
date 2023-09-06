// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Castable.CastableWithLevelData`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DataEditor;

namespace Ankama.Cube.Data.Castable
{
  public class CastableWithLevelData<T> where T : EditableData, IDefinitionWithTooltip
  {
    public readonly T definition;

    protected CastableWithLevelData(T definition, int level)
    {
      this.definition = definition;
      this.level = level;
    }

    public int level { get; }
  }
}
