﻿// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StateManagement.StateTransitionMode
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;

namespace Ankama.AssetManagement.StateManagement
{
  [PublicAPI]
  public enum StateTransitionMode
  {
    [PublicAPI] Inherit,
    [PublicAPI] Disabled,
    [PublicAPI] Enabled,
  }
}
