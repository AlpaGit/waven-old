// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.Spin2.SpinProtocol
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace Ankama.Cube.Network.Spin2
{
  public static class SpinProtocol
  {
    public static bool CheckAuthentication(
      byte[] jsonPayload,
      out SpinProtocol.ConnectionErrors optConnError)
    {
      JObject jobject = JObject.Parse(Encoding.UTF8.GetString(jsonPayload));
      if (jobject.Value<bool>((object) "success"))
      {
        optConnError = SpinProtocol.ConnectionErrors.NoneOrOtherOrUnknown;
        return true;
      }
      optConnError = (SpinProtocol.ConnectionErrors) jobject.Value<int>((object) "errCode");
      return false;
    }

    public enum ConnectionErrors
    {
      NoneOrOtherOrUnknown,
      BadCredentials,
      InvalidAuthenticationInfo,
      SubscriptionRequired,
      AdminRightsRequired,
      AccountKnonwButBanned,
      AccountKnonwButBlocked,
      IpAddressRefused,
      BetaAccessRequired,
      ServerTimeout,
      ServerError,
      AccountsBackendError,
      NickNameRequired,
    }

    public enum MessageType
    {
      Application,
      Ping,
      Pong,
      Heartbeat,
    }

    public interface Message
    {
      SpinProtocol.MessageType messageType { get; }

      byte[] Serialize();

      byte[] payload { get; }
    }

    public abstract class MessageWithPayload : SpinProtocol.Message
    {
      public SpinProtocol.MessageType messageType { get; }

      public byte[] payload { get; }

      protected MessageWithPayload(SpinProtocol.MessageType messageType, byte[] payload)
      {
        this.messageType = messageType;
        this.payload = payload;
      }

      public byte[] Serialize()
      {
        int length = this.payload.Length;
        byte[] dst = new byte[length + 1];
        dst[0] = (byte) this.messageType;
        Buffer.BlockCopy((Array) this.payload, 0, (Array) dst, 1, length);
        return dst;
      }
    }

    public class PingMessage : SpinProtocol.MessageWithPayload
    {
      public PingMessage(byte[] payload)
        : base(SpinProtocol.MessageType.Ping, payload)
      {
      }
    }

    public class PongMessage : SpinProtocol.MessageWithPayload
    {
      public PongMessage(byte[] payload)
        : base(SpinProtocol.MessageType.Pong, payload)
      {
      }
    }

    public class RawApplicationMessage : SpinProtocol.MessageWithPayload
    {
      public RawApplicationMessage(byte[] payload)
        : base(SpinProtocol.MessageType.Application, payload)
      {
      }
    }

    public class HeartbeatMessage : SpinProtocol.Message
    {
      public static readonly SpinProtocol.HeartbeatMessage instance = new SpinProtocol.HeartbeatMessage();
      private static readonly byte[] s_serialized = new byte[1]
      {
        (byte) 3
      };

      public byte[] payload => new byte[0];

      public SpinProtocol.MessageType messageType => SpinProtocol.MessageType.Heartbeat;

      public byte[] Serialize() => SpinProtocol.HeartbeatMessage.s_serialized;
    }
  }
}
