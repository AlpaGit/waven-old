// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterBase
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  public sealed class CharacterBase : MonoBehaviour, ICharacterUI
  {
    [SerializeField]
    private CharacterBaseFeedbackResources m_resources;
    [SerializeField]
    private SpriteRenderer m_renderer;
    private Color m_color = Color.white;
    private int m_sortingOrder;
    private CharacterBase.State m_state;
    private CharacterBase.TargetState m_targetState;
    private Color m_stateColor = Color.white;

    public Color color
    {
      get => this.m_color;
      set
      {
        this.m_color = value;
        this.m_renderer.color = value * this.m_stateColor;
      }
    }

    public int sortingOrder
    {
      get => this.m_sortingOrder;
      set
      {
        this.m_sortingOrder = value;
        this.m_renderer.sortingOrder = this.sortingOrder;
      }
    }

    public void Setup(PlayerType playerType)
    {
      this.m_stateColor = this.m_resources.feedbackColors.GetPlayerColor(playerType);
      this.Refresh();
    }

    public void InitializeState(
      FightStatus fightStatus,
      CharacterStatus characterStatus,
      PlayerStatus ownerStatus)
    {
      if (fightStatus.currentTurnPlayerId != ownerStatus.id)
        this.SetState(CharacterBase.State.NotPlayable);
      else
        this.SetState(characterStatus.actionUsed ? CharacterBase.State.ActionUsed : CharacterBase.State.ActionAvailable);
    }

    public void SetState(CharacterBase.State state)
    {
      if (state == this.m_state)
        return;
      this.m_state = state;
      this.Refresh();
    }

    public void SetTargetState(CharacterBase.TargetState state)
    {
      if (state == this.m_targetState)
        return;
      this.m_targetState = state;
      this.Refresh();
    }

    private void Refresh()
    {
      CharacterBaseFeedbackResources resources = this.m_resources;
      Sprite sprite;
      float num;
      switch (this.m_targetState)
      {
        case CharacterBase.TargetState.None:
          sprite = resources.baseSprite;
          switch (this.m_state)
          {
            case CharacterBase.State.NotPlayable:
              num = resources.notPlayableAlpha;
              break;
            case CharacterBase.State.ActionUsed:
              num = resources.actionUsedAlpha;
              break;
            case CharacterBase.State.ActionAvailable:
              num = resources.actionAvailableAlpha;
              break;
            default:
              throw new ArgumentOutOfRangeException("m_state", (object) this.m_state, (string) null);
          }
          break;
        case CharacterBase.TargetState.Targetable:
          sprite = resources.attackedSprite;
          num = 1f;
          break;
        case CharacterBase.TargetState.Targeted:
          sprite = resources.attackedSprite;
          num = 1f;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Color stateColor = this.m_stateColor with { a = num };
      this.m_renderer.sprite = sprite;
      this.m_renderer.color = this.m_color * stateColor;
      this.m_stateColor = stateColor;
    }

    public enum State
    {
      NotPlayable,
      ActionUsed,
      ActionAvailable,
    }

    public enum TargetState
    {
      None,
      Targetable,
      Targeted,
    }
  }
}
