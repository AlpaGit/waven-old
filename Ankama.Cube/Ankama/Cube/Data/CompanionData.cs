// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.CompanionData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.Castable;

namespace Ankama.Cube.Data
{
  public class CompanionData : CastableWithLevelData<CompanionDefinition>
  {
    public CompanionData(CompanionDefinition definition, int level)
      : base(definition, level)
    {
    }
  }
}
