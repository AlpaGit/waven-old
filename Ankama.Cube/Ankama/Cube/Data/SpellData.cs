// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.Castable;

namespace Ankama.Cube.Data
{
  public class SpellData : CastableWithLevelData<SpellDefinition>
  {
    public SpellData(SpellDefinition definition, int level)
      : base(definition, level)
    {
    }
  }
}
