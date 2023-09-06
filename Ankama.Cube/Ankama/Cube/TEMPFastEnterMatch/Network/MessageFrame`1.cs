// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Network.MessageFrame`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.TEMPFastEnterMatch.Network
{
  public abstract class MessageFrame<T> : IDisposable where T : class
  {
    protected readonly IConnection<T> m_connection;
    private readonly MessageHandlersDictionary m_handlersDict = new MessageHandlersDictionary();

    protected MessageFrame(IConnection<T> connection)
    {
      this.m_connection = connection;
      this.m_connection.OnApplicationMessage += new Action<T>(this.ExecuteHandlers);
    }

    public void WhenReceiveEnqueue<U>(Action<U> action) where U : class, T => this.m_handlersDict.Add((IMessageHandler) new MessageHandler<U>(action));

    public virtual void Dispose()
    {
      this.m_connection.OnApplicationMessage -= new Action<T>(this.ExecuteHandlers);
      this.m_handlersDict.Clear();
    }

    private void ExecuteHandlers(T message)
    {
      IReadOnlyList<IMessageHandler> messageHandlerList = this.m_handlersDict.HandlersFor(message.GetType());
      int count = ((IReadOnlyCollection<IMessageHandler>) messageHandlerList).Count;
      for (int index = 0; index < count; ++index)
        messageHandlerList[index].Execute((object) message);
    }
  }
}
