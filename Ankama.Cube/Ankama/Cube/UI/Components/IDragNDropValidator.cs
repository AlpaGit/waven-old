﻿// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.IDragNDropValidator
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.UI.Components
{
  public interface IDragNDropValidator
  {
    bool IsValidDrag(object value);

    bool IsValidDrop(object value);
  }
}
