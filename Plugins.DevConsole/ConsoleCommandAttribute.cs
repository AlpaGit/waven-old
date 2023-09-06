// Decompiled with JetBrains decompiler
// Type: DevConsole.ConsoleCommandAttribute
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using System;

namespace DevConsole
{
  [AttributeUsage(AttributeTargets.Class)]
  public class ConsoleCommandAttribute : Attribute
  {
    private string[] names;

    public ConsoleCommandAttribute(string[] _Names) => this.names = _Names;

    public string[] Names => this.names;
  }
}
