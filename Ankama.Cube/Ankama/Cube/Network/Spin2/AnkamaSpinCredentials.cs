// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.AnkamaSpinCredentials
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace Ankama.Cube.Network.Spin2
{
  public abstract class AnkamaSpinCredentials : ISpinCredentials
  {
    public string CreateMessage()
    {
      int num1 = 2;
      int num2 = AnkamaSpinCredentials.OsType();
      int num3 = AnkamaSpinCredentials.DeviceType();
      StringWriter stringWriter = new StringWriter();
      using (JsonTextWriter jsonWriter = new JsonTextWriter((TextWriter) stringWriter))
      {
        jsonWriter.WriteStartObject();
        this.WriteCredentials(jsonWriter);
        jsonWriter.WritePropertyName("clientType");
        jsonWriter.WriteValue(num1);
        jsonWriter.WritePropertyName("osType");
        jsonWriter.WriteValue(num2);
        jsonWriter.WritePropertyName("deviceType");
        jsonWriter.WriteValue(num3);
        jsonWriter.WritePropertyName("deviceId");
        jsonWriter.WriteValue(SystemInfo.deviceUniqueIdentifier);
        jsonWriter.WriteEndObject();
      }
      return stringWriter.ToString();
    }

    protected abstract void WriteCredentials(JsonTextWriter jsonWriter);

    private static int DeviceType()
    {
      switch (Device.currentType)
      {
        case Device.Type.PC:
          return 3;
        case Device.Type.Mobile:
          return 1;
        case Device.Type.Tablet:
          return 2;
        default:
          return 3;
      }
    }

    private static int OsType()
    {
      switch (Application.platform)
      {
        case RuntimePlatform.OSXEditor:
        case RuntimePlatform.OSXPlayer:
          return 4;
        case RuntimePlatform.WindowsPlayer:
        case RuntimePlatform.WindowsEditor:
          return 3;
        case RuntimePlatform.IPhonePlayer:
          return 2;
        case RuntimePlatform.Android:
          return 1;
        case RuntimePlatform.LinuxPlayer:
          return 5;
        default:
          return 0;
      }
    }
  }
}
