// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.MatchmakingPlayerElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class MatchmakingPlayerElement : MonoBehaviour
  {
    [SerializeField]
    private GameObject m_standardStuffParent;
    [SerializeField]
    private GameObject m_leaveStuffParent;
    [SerializeField]
    private Button m_inviteButton;
    [SerializeField]
    private Button m_cancelInviteButton;
    [SerializeField]
    private RawTextField m_name;
    [SerializeField]
    private CanvasGroup m_canvasGroup;
    private FightPlayerInfo m_player;
    private MatchmakingPlayerElement.InviteState m_inviteState;
    public Action<MatchmakingPlayerElement, FightPlayerInfo> onInvite;
    public Action<MatchmakingPlayerElement, FightPlayerInfo> onCancelInvite;

    public MatchmakingPlayerElement.InviteState state => this.m_inviteState;

    public FightPlayerInfo player => this.m_player;

    public bool interactable
    {
      set => this.m_canvasGroup.interactable = value;
    }

    private void Start()
    {
      this.m_inviteButton.onClick.AddListener((UnityAction) (() =>
      {
        Action<MatchmakingPlayerElement, FightPlayerInfo> onInvite = this.onInvite;
        if (onInvite == null)
          return;
        onInvite(this, this.m_player);
      }));
      this.m_cancelInviteButton.onClick.AddListener((UnityAction) (() =>
      {
        Action<MatchmakingPlayerElement, FightPlayerInfo> onCancelInvite = this.onCancelInvite;
        if (onCancelInvite == null)
          return;
        onCancelInvite(this, this.m_player);
      }));
    }

    public void SetState(MatchmakingPlayerElement.InviteState state)
    {
      this.m_inviteState = state;
      switch (this.m_inviteState)
      {
        case MatchmakingPlayerElement.InviteState.Empty:
          this.m_standardStuffParent.SetActive(true);
          this.m_leaveStuffParent.SetActive(false);
          this.m_inviteButton.gameObject.SetActive(false);
          this.m_cancelInviteButton.gameObject.SetActive(false);
          this.m_name.gameObject.SetActive(false);
          break;
        case MatchmakingPlayerElement.InviteState.Normal:
          this.m_standardStuffParent.SetActive(true);
          this.m_leaveStuffParent.SetActive(false);
          this.m_inviteButton.interactable = true;
          this.m_inviteButton.gameObject.SetActive(true);
          this.m_cancelInviteButton.gameObject.SetActive(false);
          break;
        case MatchmakingPlayerElement.InviteState.Leaving:
          this.m_standardStuffParent.SetActive(false);
          this.m_leaveStuffParent.SetActive(true);
          break;
        case MatchmakingPlayerElement.InviteState.Invited_By_Me:
          this.m_standardStuffParent.SetActive(true);
          this.m_leaveStuffParent.SetActive(false);
          this.m_inviteButton.gameObject.SetActive(false);
          this.m_cancelInviteButton.gameObject.SetActive(true);
          break;
        case MatchmakingPlayerElement.InviteState.Invite_Me:
          this.m_standardStuffParent.SetActive(true);
          this.m_leaveStuffParent.SetActive(false);
          this.m_inviteButton.interactable = false;
          this.m_inviteButton.gameObject.SetActive(true);
          this.m_cancelInviteButton.gameObject.SetActive(false);
          break;
      }
    }

    public void SetPlayer([CanBeNull] FightPlayerInfo player)
    {
      this.m_player = player;
      if (player != null)
      {
        this.m_name.SetText(player.Info.Nickname);
        this.m_name.gameObject.SetActive(true);
        this.SetState(MatchmakingPlayerElement.InviteState.Normal);
      }
      else
        this.SetState(MatchmakingPlayerElement.InviteState.Empty);
    }

    public enum InviteState
    {
      Empty,
      Normal,
      Leaving,
      Invited_By_Me,
      Invite_Me,
    }
  }
}
