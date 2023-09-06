// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders.IDynamicValueSourceExtension
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders
{
  public static class IDynamicValueSourceExtension
  {
    public static int GetValueInt(
      this IReadOnlyList<ILevelOnlyDependant> dynamicValues,
      string name,
      int level)
    {
      if (dynamicValues != null)
      {
        int count = ((IReadOnlyCollection<ILevelOnlyDependant>) dynamicValues).Count;
        for (int index = 0; index < count; ++index)
        {
          ILevelOnlyDependant dynamicValue = dynamicValues[index];
          if (dynamicValue.referenceId == name)
            return dynamicValue.GetValueWithLevel(level);
        }
      }
      if (name.Contains<char>('.'))
        return IDynamicValueSourceExtension.GetValueInOtherData(name, level);
      Log.Error("dynamic value " + name + " not found", 47, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ValueProviders\\FightValueProviders\\IFightValueProvider.cs");
      return 0;
    }

    private static int GetValueInOtherData(string name, int level)
    {
      string[] strArray = name.Split('.');
      if (strArray.Length != 0)
      {
        switch (strArray[0])
        {
          case "God":
            if (strArray.Length == 3)
            {
              GodDefinition god = ObjectReference.GetGod(strArray[1]);
              if ((Object) god != (Object) null)
              {
                string name1 = strArray[2];
                return god.precomputedData?.dynamicValueReferences.GetValueInt(name1, level);
              }
              Log.Error("No god for '" + strArray[1] + "': dynamic value " + name + " not found", 68, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ValueProviders\\FightValueProviders\\IFightValueProvider.cs");
              break;
            }
            break;
          case "Spell":
            if (strArray.Length == 3)
            {
              SpellDefinition spellDefinition = (SpellDefinition) null;
              int result;
              if (int.TryParse(strArray[1], out result))
                spellDefinition = ObjectReference.GetSpell(result);
              if ((Object) spellDefinition != (Object) null)
              {
                string name2 = strArray[2];
                return spellDefinition.precomputedData?.dynamicValueReferences.GetValueInt(name2, level);
              }
              Log.Error("No spell for id=" + strArray[1] + ": dynamic value " + name + " not found", 86, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ValueProviders\\FightValueProviders\\IFightValueProvider.cs");
              break;
            }
            break;
        }
      }
      Log.Error("dynamic value " + name + " not found", 92, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ValueProviders\\FightValueProviders\\IFightValueProvider.cs");
      return 0;
    }
  }
}
