// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.CubeApplicationCodec
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network.Spin2.Layers;
using Ankama.Cube.Protocols;
using Google.Protobuf;
using System.IO;
using UnityEngine;

namespace Ankama.Cube.Network
{
  public class CubeApplicationCodec : ApplicationCodec<IMessage>
  {
    public static CubeApplicationCodec instance = new CubeApplicationCodec();
    public const bool BigIndian = true;

    private CubeApplicationCodec()
    {
    }

    public bool TrySerialize(IMessage m, out byte[] result)
    {
      System.Type type = m.GetType();
      int data;
      if (ProtocolMap.identifiers.TryGetValue(type, out data))
      {
        MemoryStream stream = new MemoryStream();
        stream.WriteInt(data, 4, true);
        byte[] byteArray = m.ToByteArray();
        stream.Write(byteArray, 0, byteArray.Length);
        result = stream.ToArray();
        return true;
      }
      Debug.LogError((object) ("Unable to serialize message " + type.Name + ": no known id for it"));
      result = new byte[0];
      return false;
    }

    public bool TryDeserialize(byte[] data, out IMessage result)
    {
      MemoryStream memoryStream = new MemoryStream(data);
      int key = memoryStream.ReadInt(4, true);
      MessageParser messageParser;
      if (ProtocolMap.parsers.TryGetValue(key, out messageParser))
      {
        result = messageParser.ParseFrom((Stream) memoryStream);
        return true;
      }
      Debug.LogError((object) string.Format("Unable to serialize message with ID {0}: no known parser for it", (object) key));
      result = (IMessage) null;
      return false;
    }
  }
}
