// Decompiled with JetBrains decompiler
// Type: FMOD.Studio.USER_PROPERTY
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using JetBrains.Annotations;

namespace FMOD.Studio
{
  [PublicAPI]
  public struct USER_PROPERTY
  {
    public StringWrapper name;
    public USER_PROPERTY_TYPE type;
    private Union_IntBoolFloatString value;

    public int intValue() => this.type != USER_PROPERTY_TYPE.INTEGER ? -1 : this.value.intvalue;

    public bool boolValue() => this.type == USER_PROPERTY_TYPE.BOOLEAN && this.value.boolvalue;

    public float floatValue() => this.type != USER_PROPERTY_TYPE.FLOAT ? -1f : this.value.floatvalue;

    public string stringValue() => this.type != USER_PROPERTY_TYPE.STRING ? "" : (string) this.value.stringvalue;
  }
}
