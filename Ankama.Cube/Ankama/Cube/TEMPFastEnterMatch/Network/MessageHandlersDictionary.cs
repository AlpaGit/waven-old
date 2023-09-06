// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Network.MessageHandlersDictionary
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;

namespace Ankama.Cube.TEMPFastEnterMatch.Network
{
  internal class MessageHandlersDictionary
  {
    private readonly Dictionary<System.Type, List<IMessageHandler>> m_dict = new Dictionary<System.Type, List<IMessageHandler>>();
    private static readonly IReadOnlyList<IMessageHandler> NoHandlers = (IReadOnlyList<IMessageHandler>) new List<IMessageHandler>(0);

    public void Add(IMessageHandler handler)
    {
      List<IMessageHandler> messageHandlerList;
      if (this.m_dict.TryGetValue(handler.messageType, out messageHandlerList))
      {
        messageHandlerList.Add(handler);
        this.m_dict.Add(handler.messageType, messageHandlerList);
      }
      else
        this.m_dict.Add(handler.messageType, new List<IMessageHandler>()
        {
          handler
        });
    }

    public IReadOnlyList<IMessageHandler> HandlersFor(System.Type messageType)
    {
      List<IMessageHandler> messageHandlerList;
      return this.m_dict.TryGetValue(messageType, out messageHandlerList) ? (IReadOnlyList<IMessageHandler>) messageHandlerList : MessageHandlersDictionary.NoHandlers;
    }

    public void Clear() => this.m_dict.Clear();
  }
}
