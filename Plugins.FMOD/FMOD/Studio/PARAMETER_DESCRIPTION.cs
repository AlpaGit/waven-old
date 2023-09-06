// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.PARAMETER_DESCRIPTION
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct PARAMETER_DESCRIPTION
  {
    public StringWrapper name;
    public int index;
    public float minimum;
    public float maximum;
    public float defaultvalue;
    public PARAMETER_TYPE type;
  }
}
