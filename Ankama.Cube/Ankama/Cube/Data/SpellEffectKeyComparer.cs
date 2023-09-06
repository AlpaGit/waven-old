// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.SpellEffectKeyComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.Data
{
  public class SpellEffectKeyComparer : IEqualityComparer<SpellEffectKey>
  {
    public static readonly SpellEffectKeyComparer instance = new SpellEffectKeyComparer();

    public bool Equals(SpellEffectKey x, SpellEffectKey y) => x == y;

    public int GetHashCode(SpellEffectKey obj) => (int) obj;
  }
}
