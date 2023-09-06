// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.AnkamaTokenCredentials
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json;

namespace Ankama.Cube.Network.Spin2
{
  public sealed class AnkamaTokenCredentials : AnkamaSpinCredentials
  {
    private readonly string m_token;

    public AnkamaTokenCredentials(string token) => this.m_token = token;

    protected override void WriteCredentials(JsonTextWriter jsonWriter)
    {
      jsonWriter.WritePropertyName("token");
      jsonWriter.WriteValue(this.m_token);
    }
  }
}
