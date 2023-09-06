// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.AnkamaNoHaapiCredentials
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json;

namespace Ankama.Cube.Network.Spin2
{
  public sealed class AnkamaNoHaapiCredentials : AnkamaSpinCredentials
  {
    private readonly string m_login;

    public AnkamaNoHaapiCredentials(string login) => this.m_login = login;

    protected override void WriteCredentials(JsonTextWriter jsonWriter)
    {
      jsonWriter.WritePropertyName("token");
      jsonWriter.WriteValue(this.m_login);
    }
  }
}
