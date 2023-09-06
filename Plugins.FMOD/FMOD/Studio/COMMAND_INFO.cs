// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.COMMAND_INFO
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct COMMAND_INFO
  {
    private readonly StringWrapper commandname;
    public int parentcommandindex;
    public int framenumber;
    public float frametime;
    public INSTANCETYPE instancetype;
    public INSTANCETYPE outputtype;
    public uint instancehandle;
    public uint outputhandle;
  }
}
