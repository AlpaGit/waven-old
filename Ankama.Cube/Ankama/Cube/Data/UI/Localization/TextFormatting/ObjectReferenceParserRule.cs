// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.ObjectReferenceParserRule
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Text;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public class ObjectReferenceParserRule : IParserRuleWithPrefix, IParserRule
  {
    private readonly string m_text;
    private readonly ObjectReference.Type m_type;

    public string prefix => this.m_text;

    public ObjectReferenceParserRule(string text, ObjectReference.Type type)
    {
      this.m_text = text;
      this.m_type = type;
    }

    public bool TryFormat(
      ref StringReader reader,
      StringBuilder output,
      FormatterParams formatterParams)
    {
      if (!reader.Read(this.m_text) || !reader.Read(':'))
        return false;
      int id = reader.ReadInt();
      output.BeginKeyWord();
      output.Append(ObjectReferenceParserRule.GetText(this.m_type, id));
      output.EndKeyWord();
      return true;
    }

    private static string GetText(ObjectReference.Type type, int id)
    {
      IDefinitionWithTooltip definitionWithTooltip = ObjectReference.GetObject(type, id);
      return definitionWithTooltip == null ? string.Format("{0}:{1}", (object) type, (object) id) : RuntimeData.FormattedText(definitionWithTooltip.i18nNameId, (IValueProvider) null);
    }
  }
}
