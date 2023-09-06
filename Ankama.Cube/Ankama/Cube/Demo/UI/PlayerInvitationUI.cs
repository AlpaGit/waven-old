// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PlayerInvitationUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class PlayerInvitationUI : MonoBehaviour
  {
    [SerializeField]
    private CanvasGroup m_playersCanvasGroup;
    [SerializeField]
    private List<MatchmakingPlayerElement> m_playersElements;
    public Action<bool> onSearchingValueChanged;
    public Action<FightPlayerInfo> onInvitationSend;
    public Action<FightPlayerInfo> onInvitationCanceled;
    public Action<FightPlayerInfo> onInvitationDeclined;
    public Action<FightPlayerInfo> onInvitationAccepted;

    public void Init()
    {
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        playersElement.SetPlayer((FightPlayerInfo) null);
        playersElement.SetState(MatchmakingPlayerElement.InviteState.Empty);
      }
    }

    private void Start()
    {
      this.UpdateInviteResearch();
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        playersElement.onInvite += new Action<MatchmakingPlayerElement, FightPlayerInfo>(this.OnSendInvitationClick);
        playersElement.onCancelInvite += new Action<MatchmakingPlayerElement, FightPlayerInfo>(this.OnCancelInvitationClick);
      }
    }

    private void UpdateInviteResearch()
    {
      int num = 0;
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        if (playersElement.state == MatchmakingPlayerElement.InviteState.Invited_By_Me || playersElement.state == MatchmakingPlayerElement.InviteState.Invite_Me)
          ++num;
      }
      Action<bool> searchingValueChanged = this.onSearchingValueChanged;
      if (searchingValueChanged == null)
        return;
      searchingValueChanged(num > 0);
    }

    public void SetInvitationsLocked(bool value) => this.m_playersCanvasGroup.interactable = !value;

    private void OnSendInvitationClick(MatchmakingPlayerElement element, FightPlayerInfo player)
    {
      element.SetState(MatchmakingPlayerElement.InviteState.Invited_By_Me);
      this.UpdateInviteResearch();
      Action<FightPlayerInfo> onInvitationSend = this.onInvitationSend;
      if (onInvitationSend == null)
        return;
      onInvitationSend(player);
    }

    private void OnCancelInvitationClick(MatchmakingPlayerElement element, FightPlayerInfo player)
    {
      element.SetState(MatchmakingPlayerElement.InviteState.Normal);
      this.UpdateInviteResearch();
      Action<FightPlayerInfo> invitationCanceled = this.onInvitationCanceled;
      if (invitationCanceled == null)
        return;
      invitationCanceled(player);
    }

    public void ReceiveInvitationResult(long uid, bool result)
    {
      if (result)
        return;
      MatchmakingPlayerElement element = this.FindElement(uid);
      if (!((UnityEngine.Object) element != (UnityEngine.Object) null) || element.state != MatchmakingPlayerElement.InviteState.Invited_By_Me)
        return;
      MatchmakingPopupHandler.instance.ShowMessage(MatchmakingPopupHandler.MessageType.InvitationFail, element.player);
      element.SetState(MatchmakingPlayerElement.InviteState.Normal);
      this.UpdateInviteResearch();
    }

    private void OnInvitationAccepted(FightPlayerInfo player)
    {
      Action<FightPlayerInfo> invitationAccepted = this.onInvitationAccepted;
      if (invitationAccepted == null)
        return;
      invitationAccepted(player);
    }

    private void OnInvitationDeclined(FightPlayerInfo player)
    {
      MatchmakingPlayerElement element = this.FindElement(player.Uid);
      if ((UnityEngine.Object) element != (UnityEngine.Object) null)
      {
        element.SetState(MatchmakingPlayerElement.InviteState.Normal);
        this.UpdateInviteResearch();
      }
      Action<FightPlayerInfo> invitationDeclined = this.onInvitationDeclined;
      if (invitationDeclined == null)
        return;
      invitationDeclined(player);
    }

    public void AddPlayer(FightPlayerInfo player)
    {
      MatchmakingPlayerElement matchmakingPlayerElement = this.FirstFreeElement();
      if ((UnityEngine.Object) matchmakingPlayerElement != (UnityEngine.Object) null)
      {
        matchmakingPlayerElement.SetPlayer(player);
        matchmakingPlayerElement.SetState(MatchmakingPlayerElement.InviteState.Normal);
      }
      this.UpdateInviteResearch();
    }

    public void RemovePlayer(long uid)
    {
      MatchmakingPlayerElement element = this.FindElement(uid);
      if ((UnityEngine.Object) element != (UnityEngine.Object) null)
      {
        element.SetPlayer((FightPlayerInfo) null);
        element.SetState(MatchmakingPlayerElement.InviteState.Leaving);
      }
      this.UpdateInviteResearch();
    }

    public void RemoveAllPlayers()
    {
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        playersElement.SetPlayer((FightPlayerInfo) null);
        playersElement.SetState(MatchmakingPlayerElement.InviteState.Empty);
      }
      MatchmakingPopupHandler.instance.RemoveAllStackedMessages();
      this.UpdateInviteResearch();
    }

    public void InvitationReceiveFrom(FightPlayerInfo player)
    {
      MatchmakingPlayerElement element = this.FindElement(player.Uid);
      if ((UnityEngine.Object) element != (UnityEngine.Object) null)
      {
        element.SetState(MatchmakingPlayerElement.InviteState.Invite_Me);
        this.UpdateInviteResearch();
      }
      MatchmakingPopupHandler.instance.ShowMessage(MatchmakingPopupHandler.MessageType.InvitationReceived, player, (Action) (() => this.OnInvitationDeclined(player)), (Action) (() => this.OnInvitationAccepted(player)));
    }

    public void InvitationCanceledFrom(long uid)
    {
      MatchmakingPopupHandler.instance.RemoveInvitationMessageFrom(uid);
      MatchmakingPlayerElement element = this.FindElement(uid);
      if (!((UnityEngine.Object) element != (UnityEngine.Object) null))
        return;
      element.SetState(MatchmakingPlayerElement.InviteState.Normal);
      this.UpdateInviteResearch();
    }

    public void RemoveAllReceivedInvitations()
    {
      MatchmakingPopupHandler.instance.RemoveAllMessageOfType(MatchmakingPopupHandler.MessageType.InvitationReceived);
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        if (playersElement.state == MatchmakingPlayerElement.InviteState.Invite_Me)
          playersElement.SetState(MatchmakingPlayerElement.InviteState.Normal);
      }
    }

    public void InvitationAnswerFrom(long uid, bool value)
    {
      if (value)
        return;
      MatchmakingPlayerElement element = this.FindElement(uid);
      if (!((UnityEngine.Object) element != (UnityEngine.Object) null))
        return;
      MatchmakingPopupHandler.instance.ShowMessage(MatchmakingPopupHandler.MessageType.InvitationDeclined, element.player);
      element.SetState(MatchmakingPlayerElement.InviteState.Normal);
      this.UpdateInviteResearch();
    }

    private MatchmakingPlayerElement FirstFreeElement()
    {
      for (int index = 0; index < this.m_playersElements.Count; ++index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        if (playersElement.state == MatchmakingPlayerElement.InviteState.Empty || playersElement.state == MatchmakingPlayerElement.InviteState.Leaving)
          return playersElement;
      }
      MatchmakingPlayerElement matchmakingPlayerElement = UnityEngine.Object.Instantiate<MatchmakingPlayerElement>(this.m_playersElements[0]);
      matchmakingPlayerElement.gameObject.SetActive(true);
      matchmakingPlayerElement.transform.SetParent(this.m_playersElements[0].transform.parent, false);
      matchmakingPlayerElement.SetState(MatchmakingPlayerElement.InviteState.Empty);
      this.m_playersElements.Add(matchmakingPlayerElement);
      return matchmakingPlayerElement;
    }

    private MatchmakingPlayerElement FindElement(long uid)
    {
      for (int index = this.m_playersElements.Count - 1; index >= 0; --index)
      {
        MatchmakingPlayerElement playersElement = this.m_playersElements[index];
        if (playersElement.player != null && playersElement.player.Uid == uid)
          return playersElement;
      }
      return (MatchmakingPlayerElement) null;
    }
  }
}
