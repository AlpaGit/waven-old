// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Levelable.InventoryWithLevel
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Data.Levelable
{
  public class InventoryWithLevel : 
    IInventoryWithLevel,
    IEnumerable<int>,
    IEnumerable,
    ILevelProvider
  {
    private readonly Dictionary<int, int> m_levels = new Dictionary<int, int>();
    private readonly int? m_defaultLevel;

    public InventoryWithLevel(int? defaultLevel = null) => this.m_defaultLevel = defaultLevel;

    public void UpdateAllLevels(IDictionary<int, int> levels)
    {
      this.m_levels.Clear();
      foreach (KeyValuePair<int, int> level in (IEnumerable<KeyValuePair<int, int>>) levels)
        this.m_levels.Add(level.Key, level.Value);
    }

    public bool TryGetLevel(int id, out int level)
    {
      if (this.m_levels.TryGetValue(id, out level))
        return true;
      if (!this.m_defaultLevel.HasValue)
        return false;
      level = this.m_defaultLevel.Value;
      return true;
    }

    public IEnumerator<int> GetEnumerator() => (IEnumerator<int>) this.m_levels.Keys.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
