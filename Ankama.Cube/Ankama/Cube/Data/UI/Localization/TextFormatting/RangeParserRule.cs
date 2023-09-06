// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.RangeParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting.ValueProviders.FightValueProviders;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class RangeParserRule : IParserRuleWithPrefix, IParserRule
  {
    private bool m_initialized;
    private SubString rawText;

    public string prefix => "range";

    private void ReloadRawText()
    {
      string name = "RANGE";
      string str;
      if (!RuntimeData.TryGetText(name, out str))
      {
        Log.Error("Text key with name " + name + " does not exist.", 59, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ParserRules\\TooltipParserRules\\RangeParserRule.cs");
        this.rawText = (SubString) name;
      }
      else
      {
        this.m_initialized = true;
        this.rawText = (SubString) str;
      }
    }

    public bool TryFormat(
      ref StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if (!reader.Read("range"))
        return false;
      if (!this.m_initialized)
        this.ReloadRawText();
      if (!(formatterParams.valueProvider is IFightValueProvider valueProvider))
      {
        Log.Error("Cannot format Range for a object without range", 80, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ParserRules\\TooltipParserRules\\RangeParserRule.cs");
        return false;
      }
      Tuple<int, int> range = valueProvider.GetRange();
      if (range == null)
      {
        Log.Error("Cannot format Range for a object without range", 87, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ParserRules\\TooltipParserRules\\RangeParserRule.cs");
        return false;
      }
      ref FormatterParams local = ref formatterParams;
      string[] strArray = new string[2];
      int num = range.Item1;
      strArray[0] = num.ToString();
      num = range.Item2;
      strArray[1] = num.ToString();
      IndexedValueProvider indexedValueProvider = new IndexedValueProvider(strArray);
      local.valueProvider = (IValueProvider) indexedValueProvider;
      formatterParams.formatter.AppendFormat(this.rawText, output, formatterParams);
      return true;
    }
  }
}
