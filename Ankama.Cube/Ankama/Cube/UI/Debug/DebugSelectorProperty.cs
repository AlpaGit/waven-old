﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugSelectorProperty
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;

namespace Ankama.Cube.UI.Debug
{
  public class DebugSelectorProperty : DebugSelector<PropertyId>
  {
    public DebugSelectorProperty()
      : base("PropertyId")
    {
    }

    protected override PropertyId[] dataValues => EnumUtility.GetValues<PropertyId>();
  }
}
