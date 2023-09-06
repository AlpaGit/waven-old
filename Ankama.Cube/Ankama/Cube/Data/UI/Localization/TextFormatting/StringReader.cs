// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.Localization.TextFormatting.StringReader
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Utility;

namespace Ankama.Cube.Data.UI.Localization.TextFormatting
{
  public struct StringReader
  {
    public readonly string text;
    public int position;

    public int limit { get; private set; }

    public int remaining => this.limit - this.position;

    public StringReader(string text)
    {
      this.text = text;
      this.position = 0;
      this.limit = text.Length;
    }

    public StringReader(SubString text)
    {
      this.text = text.originalText;
      this.position = text.startIndex;
      this.limit = text.endIndex;
    }

    public StringReader Copy() => new StringReader(this.text)
    {
      position = this.position,
      limit = this.limit
    };

    public void SetLimit(int length) => this.limit = length;

    public bool hasNext => this.position < this.limit;

    public char previous => this.text[this.position - 1];

    public char current => this.text[this.position];

    public char next => this.text[this.position + 1];

    public bool NextEquals(string value)
    {
      int length = value.Length;
      int position = this.position;
      for (int index = 0; index < length && position < this.limit; ++index)
      {
        if ((int) this.text[position] != (int) value[index])
          return false;
        ++position;
      }
      return true;
    }

    public void ReadUntil(char c)
    {
      int position = this.position;
      while (this.hasNext && (int) this.text[position] != (int) c)
        ++position;
      this.position = position;
    }

    public bool Read(string value)
    {
      int length = value.Length;
      int position = this.position;
      for (int index = 0; index < length && position < this.limit; ++index)
      {
        if ((int) this.text[position] != (int) value[index])
          return false;
        ++position;
      }
      this.position = position;
      return true;
    }

    public bool Read(char test)
    {
      int num1 = (int) this.text[this.position];
      ++this.position;
      int num2 = (int) test;
      return num1 == num2;
    }

    public void SkipSpaces()
    {
      int position = this.position;
      while (position < this.limit && char.IsWhiteSpace(this.text[position]))
        ++position;
      this.position = position;
    }

    public char ReadNext()
    {
      int num = (int) this.text[this.position];
      ++this.position;
      return (char) num;
    }

    public SubString ReadWord()
    {
      int position = this.position;
      SubString subString = new SubString(this.text, position, 0);
      while (position < this.limit && (char.IsLetterOrDigit(this.text[position]) || this.text[position] == '.'))
        ++position;
      subString.length = position - subString.startIndex;
      this.position = position;
      return subString;
    }

    public int ReadInt()
    {
      int position = this.position;
      SubString subString = new SubString(this.text, position, 0);
      while (position < this.limit && char.IsDigit(this.text[position]))
        ++position;
      if (position == this.position)
        throw new TextFormatterException(this.text, position, "Cannot read a int");
      subString.length = position - subString.startIndex;
      this.position = position;
      return int.Parse(subString.ToString());
    }

    public SubString ReadContent(char open, char close)
    {
      if ((int) this.current != (int) open)
        throw new TextFormatterException(this.text, this.position, string.Format("Cannot read a content between {0}{1}", (object) open, (object) close));
      int position = this.position;
      int num1 = position + 1;
      int num2 = 1;
      while (num2 != 0 && position < this.limit - 1)
      {
        ++position;
        char ch = this.text[position];
        if ((int) ch == (int) open)
          ++num2;
        else if ((int) ch == (int) close)
          --num2;
      }
      if (num2 != 0)
        throw new TextFormatterException(this.text, num1, string.Format("Not balanced {0} {1}", (object) open, (object) close));
      this.position = position + 1;
      return new SubString(this.text, num1, position - num1);
    }

    public bool ReadTag(string tagName, ref SubString tagAttributes, ref SubString content)
    {
      int position = this.position;
      if (this.Read('<') && this.Read(tagName))
      {
        tagAttributes.startIndex = this.position;
        bool flag1 = false;
        while (this.hasNext)
        {
          if (this.current == '>')
          {
            flag1 = true;
            break;
          }
          ++this.position;
        }
        if (flag1)
        {
          tagAttributes.length = this.position - tagAttributes.startIndex;
          content.startIndex = this.position + 1;
          int num1 = 1;
          int num2 = content.startIndex;
          try
          {
            while (num1 != 0)
            {
              if (this.position < this.limit - 1)
              {
                num2 = this.position;
                if (this.Read('<'))
                {
                  bool flag2 = false;
                  if (this.current == '/')
                  {
                    ++this.position;
                    flag2 = true;
                  }
                  if (this.Read(tagName))
                  {
                    if (flag2)
                    {
                      if (this.Read('>'))
                        --num1;
                    }
                    else
                    {
                      this.ReadUntil('>');
                      if (this.current == '>')
                        ++num1;
                    }
                  }
                }
              }
              else
                break;
            }
          }
          catch
          {
          }
          if (num1 == 0)
          {
            content.length = num2 - content.startIndex;
            return true;
          }
        }
      }
      this.position = position;
      return false;
    }
  }
}
