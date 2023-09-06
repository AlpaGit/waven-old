// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.CostModifiers.SpellCostModification
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ankama.Cube.Fight.CostModifiers
{
  public class SpellCostModification
  {
    public readonly int id;
    public readonly int modificationValue;
    private readonly SpellFilter[] m_filters;

    public SpellCostModification(int id, int modificationValue, string spellFilterJson)
    {
      this.id = id;
      this.modificationValue = modificationValue;
      this.m_filters = SpellCostModification.DeserializeSpellFilters(spellFilterJson);
    }

    public bool Accept(int spellInstanceId, SpellDefinition spellDefinition)
    {
      if (this.m_filters == null || this.m_filters.Length == 0)
        return true;
      for (int index = 0; index < this.m_filters.Length; ++index)
      {
        if (!this.m_filters[index].Accept(spellInstanceId, spellDefinition))
          return false;
      }
      return true;
    }

    public override string ToString()
    {
      string str = this.m_filters == null ? string.Empty : string.Join("\n", ((IEnumerable<SpellFilter>) this.m_filters).Select<SpellFilter, string>((Func<SpellFilter, string>) (f => f.ToString())));
      return string.Format("{0}: {1}, {2}, {3}: {4}", (object) "id", (object) this.id, (object) this.modificationValue.ToStringSigned(), (object) "m_filters", (object) str);
    }

    private static SpellFilter[] DeserializeSpellFilters(string valueJson)
    {
      try
      {
        JArray jarray = JsonConvert.DeserializeObject(valueJson) as JArray;
        int count = jarray.Count;
        SpellFilter[] spellFilterArray = new SpellFilter[count];
        for (int index = 0; index < count; ++index)
          spellFilterArray[index] = SpellFilter.FromJsonToken(jarray[index]);
        return spellFilterArray;
      }
      catch (Exception ex)
      {
        Log.Error("Cannot deserialize " + valueJson, (object) ex, 77, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\CostModifiers\\SpellCostModification.cs");
        return (SpellFilter[]) null;
      }
    }

    public static int ApplyCostModification(
      List<SpellCostModification> modifications,
      int cost,
      SpellDefinition spellDefinition,
      CastTargetContext context)
    {
      int count = modifications.Count;
      for (int index = 0; index < count; ++index)
      {
        SpellCostModification modification = modifications[index];
        if (modification.Accept(context.instanceId, spellDefinition))
          cost += modification.modificationValue;
      }
      return Math.Max(0, cost);
    }
  }
}
