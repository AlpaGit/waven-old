// Decompiled with JetBrains decompiler
// Type: FMODUnity.FMODPlatform
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;
using System;

namespace FMODUnity
{
  [PublicAPI]
  [Serializable]
  public enum FMODPlatform
  {
    None,
    PlayInEditor,
    Default,
    Desktop,
    Mobile,
    MobileHigh,
    MobileLow,
    Console,
    Windows,
    Mac,
    Linux,
    iOS,
    Android,
    WindowsPhone,
    XboxOne,
    PS4,
    WiiU,
    PSVita,
    AppleTV,
    UWP,
    Switch,
    WebGL,
    Count,
  }
}
