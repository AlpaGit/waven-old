﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.DynamicValueContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Data
{
  public abstract class DynamicValueContext
  {
    public readonly DynamicValueHolderType type;
    public readonly int level;

    protected DynamicValueContext(DynamicValueHolderType type, int level)
    {
      this.type = type;
      this.level = level;
    }
  }
}
