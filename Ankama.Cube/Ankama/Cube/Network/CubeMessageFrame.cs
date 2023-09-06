// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.CubeMessageFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.TEMPFastEnterMatch.Network;
using Google.Protobuf;

namespace Ankama.Cube.Network
{
  public abstract class CubeMessageFrame : MessageFrame<IMessage>
  {
    protected CubeMessageFrame()
      : base(ConnectionHandler.instance.connection)
    {
    }
  }
}
