// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.CharacterAttackableUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  public sealed class CharacterAttackableUI : MonoBehaviour, ICharacterUI
  {
    [Header("Resources")]
    [UsedImplicitly]
    [SerializeField]
    private CharacterUIResources m_resources;
    [Header("Renderers")]
    [UsedImplicitly]
    [SerializeField]
    private SpriteRenderer m_feedbackIconRenderer;

    public Color color
    {
      get => this.m_feedbackIconRenderer.color;
      set => this.m_feedbackIconRenderer.color = value;
    }

    public int sortingOrder
    {
      get => this.m_feedbackIconRenderer.sortingOrder;
      set => this.m_feedbackIconRenderer.sortingOrder = value;
    }

    public void Setup()
    {
      this.m_feedbackIconRenderer.enabled = false;
      this.m_feedbackIconRenderer.sprite = (Sprite) null;
    }

    public void SetValue(ActionType actionType, bool selected)
    {
      switch (actionType)
      {
        case ActionType.None:
          this.m_feedbackIconRenderer.enabled = false;
          this.m_feedbackIconRenderer.sprite = (Sprite) null;
          break;
        case ActionType.Attack:
          this.m_feedbackIconRenderer.sprite = selected ? this.m_resources.attackTargetSelectedFeedbackIcon : this.m_resources.attackTargetFeedbackIcon;
          this.m_feedbackIconRenderer.enabled = true;
          break;
        case ActionType.Heal:
          this.m_feedbackIconRenderer.sprite = selected ? this.m_resources.healTargetSelectedFeedbackIcon : this.m_resources.healTargetFeedbackIcon;
          this.m_feedbackIconRenderer.enabled = true;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (actionType), (object) actionType, (string) null);
      }
    }
  }
}
