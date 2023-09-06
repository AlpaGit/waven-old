// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Messages.ApiToken
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

namespace Ankama.Launcher.Messages
{
  public class ApiToken
  {
    private readonly string m_value;

    public ApiToken(string value) => this.m_value = value;

    public string Value => this.m_value;
  }
}
