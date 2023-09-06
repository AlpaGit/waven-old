// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.EffectValueParserRule
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
  public class EffectValueParserRule : IParserRuleWithPrefix, IParserRule
  {
    protected const string BonusColorPrefix = "<color=#008c00ff>";
    protected const string MalusColorPrefix = "<color=#d90000ff>";
    protected const string ColorPostfix = "</color>";
    private readonly string text;
    private bool m_initialized;
    private SubString rawText;
    public Func<IFightValueProvider, int> getModificationValue;

    public string prefix => this.text;

    public EffectValueParserRule(string text)
    {
      this.text = text;
      RuntimeData.CultureCodeChanged += (RuntimeData.CultureCodeChangedEventHandler) ((code, language) => this.ReloadRawText());
    }

    private void ReloadRawText()
    {
      string upper = this.text.ToUpper();
      string str;
      if (!RuntimeData.TryGetText(upper, out str))
      {
        Log.Error("Text key with name " + upper + " does not exist.", 114, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\Localization\\TextFormatting\\ParserRules\\TooltipParserRules\\EffectValueParserRule.cs");
        this.rawText = (SubString) upper;
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
      if (!reader.Read(this.text) || !reader.Read(':'))
        return false;
      if (!this.m_initialized)
        this.ReloadRawText();
      this.FormatValue(reader.ReadWord().ToString(), output, formatterParams);
      return true;
    }

    private void FormatValue(string varName, StringBuilder output, FormatterParams formatterParams)
    {
      if (formatterParams.valueProvider is IFightValueProvider valueProvider && this.getModificationValue != null)
      {
        int num1 = this.getModificationValue(valueProvider);
        if (num1 != 0)
        {
          int num2 = valueProvider.GetValueInt(varName) + num1;
          output.Append(num1 > 0 ? "<color=#008c00ff>" : "<color=#d90000ff>");
          formatterParams.valueProvider = (IValueProvider) new IndexedValueProvider(new string[1]
          {
            num2.ToString()
          });
          formatterParams.formatter.AppendFormat(this.rawText, output, formatterParams);
          output.Append("</color>");
          return;
        }
      }
      string str = formatterParams.valueProvider.GetValue(varName);
      formatterParams.valueProvider = (IValueProvider) new IndexedValueProvider(new string[1]
      {
        str
      });
      formatterParams.formatter.AppendFormat(this.rawText, output, formatterParams);
    }
  }
}
