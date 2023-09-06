// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Network.MessageHandler`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.TEMPFastEnterMatch.Network
{
  public class MessageHandler<T> : IMessageHandler where T : class
  {
    private readonly System.Type m_messageType;
    private readonly Action<T> m_action;

    public MessageHandler(Action<T> action)
    {
      this.m_messageType = typeof (T);
      this.m_action = action;
    }

    public System.Type messageType => this.m_messageType;

    public void Execute(object data) => this.m_action(data as T);
  }
}
