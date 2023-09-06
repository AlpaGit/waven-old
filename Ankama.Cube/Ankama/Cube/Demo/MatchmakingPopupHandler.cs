// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.MatchmakingPopupHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.Code.UI;
using Ankama.Cube.Utility;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Demo
{
  public class MatchmakingPopupHandler
  {
    private readonly StateContext m_stateContext;
    private readonly List<MatchmakingPopupHandler.Message> m_messageStack = new List<MatchmakingPopupHandler.Message>();

    public static MatchmakingPopupHandler instance { get; private set; }

    public static void Init(StateContext state)
    {
      if (MatchmakingPopupHandler.instance != null)
        return;
      MatchmakingPopupHandler.instance = new MatchmakingPopupHandler(state);
      Singleton<PopupInfoManager>.instance.onPopupBeginClosing += new Action<int>(MatchmakingPopupHandler.instance.OnPopupClosing);
    }

    public static void Dispose()
    {
      if (MatchmakingPopupHandler.instance == null)
        return;
      Singleton<PopupInfoManager>.instance.onPopupBeginClosing -= new Action<int>(MatchmakingPopupHandler.instance.OnPopupClosing);
      MatchmakingPopupHandler.instance.RemoveAllStackedMessages();
      MatchmakingPopupHandler.instance = (MatchmakingPopupHandler) null;
    }

    public MatchmakingPopupHandler(StateContext state) => this.m_stateContext = state;

    private void OnPopupClosing(int id)
    {
      for (int index = this.m_messageStack.Count - 1; index >= 0; --index)
      {
        if (this.m_messageStack[index].popupInfoId == id)
        {
          this.m_messageStack.RemoveAt(index);
          break;
        }
      }
    }

    public void RemoveAllStackedMessages()
    {
      for (int index = this.m_messageStack.Count - 1; index >= 0; --index)
        PopupInfoManager.RemoveById(this.m_messageStack[index].popupInfoId);
      this.m_messageStack.Clear();
    }

    public void ShowMessage(
      MatchmakingPopupHandler.MessageType type,
      FightPlayerInfo player,
      Action noAction = null,
      Action yesAction = null)
    {
      string str = player != null ? player.Info.Nickname : "Player";
      int popupInfoId;
      switch (type)
      {
        case MatchmakingPopupHandler.MessageType.InvitationReceived:
          popupInfoId = PopupInfoManager.Show(this.m_stateContext, new PopupInfo()
          {
            message = new RawTextData(22824, new string[1]
            {
              str
            }),
            buttons = new ButtonData[2]
            {
              new ButtonData((TextData) 9912, yesAction),
              new ButtonData((TextData) 68421, noAction, style: ButtonStyle.Negative)
            },
            selectedButton = 1,
            style = PopupStyle.Normal,
            useBlur = true
          });
          break;
        case MatchmakingPopupHandler.MessageType.InvitationDeclined:
          popupInfoId = PopupInfoManager.Show(this.m_stateContext, new PopupInfo()
          {
            message = new RawTextData(32703, new string[1]
            {
              str
            }),
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            selectedButton = 1,
            style = PopupStyle.Normal,
            useBlur = true
          });
          break;
        case MatchmakingPopupHandler.MessageType.InvitationFail:
          popupInfoId = PopupInfoManager.Show(this.m_stateContext, new PopupInfo()
          {
            message = new RawTextData(18236, new string[1]
            {
              str
            }),
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            style = PopupStyle.Error,
            useBlur = true
          });
          break;
        case MatchmakingPopupHandler.MessageType.PlayerLeaveGroup:
          popupInfoId = PopupInfoManager.Show(this.m_stateContext, new PopupInfo()
          {
            message = new RawTextData(34361, new string[1]
            {
              str
            }),
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            style = PopupStyle.Normal,
            useBlur = true
          });
          break;
        case MatchmakingPopupHandler.MessageType.JoinGroupFail:
          popupInfoId = PopupInfoManager.Show(this.m_stateContext, new PopupInfo()
          {
            message = new RawTextData(80127, Array.Empty<string>()),
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            style = PopupStyle.Normal,
            useBlur = true
          });
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (type), (object) type, (string) null);
      }
      this.m_messageStack.Add(new MatchmakingPopupHandler.Message(type, player, popupInfoId));
    }

    public void RemoveAllMessageOfType(MatchmakingPopupHandler.MessageType type)
    {
      List<MatchmakingPopupHandler.Message> messageStack = this.m_messageStack;
      for (int index = messageStack.Count - 1; index >= 0; --index)
      {
        MatchmakingPopupHandler.Message message = messageStack[index];
        if (message.type == type)
        {
          messageStack.RemoveAt(index);
          PopupInfoManager.RemoveById(message.popupInfoId);
        }
      }
    }

    public void RemoveInvitationMessageFrom(long playerId)
    {
      List<MatchmakingPopupHandler.Message> messageStack = this.m_messageStack;
      for (int index = messageStack.Count - 1; index >= 0; --index)
      {
        MatchmakingPopupHandler.Message message = messageStack[index];
        if (message.type == MatchmakingPopupHandler.MessageType.InvitationReceived && message.player.Uid == playerId)
        {
          messageStack.RemoveAt(index);
          PopupInfoManager.RemoveById(message.popupInfoId);
          break;
        }
      }
    }

    public enum MessageType
    {
      InvitationReceived,
      InvitationDeclined,
      InvitationFail,
      PlayerLeaveGroup,
      JoinGroupFail,
    }

    private struct Message
    {
      public readonly MatchmakingPopupHandler.MessageType type;
      public readonly FightPlayerInfo player;
      public readonly int popupInfoId;

      public Message(
        MatchmakingPopupHandler.MessageType type,
        FightPlayerInfo player,
        int popupInfoId)
      {
        this.type = type;
        this.player = player;
        this.popupInfoId = popupInfoId;
      }
    }
  }
}
