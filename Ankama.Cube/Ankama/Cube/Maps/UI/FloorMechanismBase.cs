// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.UI.FloorMechanismBase
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Feedbacks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.UI
{
  public class FloorMechanismBase : MonoBehaviour, ICharacterUI
  {
    [SerializeField]
    private FloorMechanismBaseFeedbackResources m_resources;
    [SerializeField]
    private SpriteRenderer m_renderer;
    private bool m_alliedWithLocalPlayer;
    private FloorMechanismBase.TargetState m_targetState;
    private Color m_color = Color.white;
    private int m_sortingOrder;
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
      this.m_alliedWithLocalPlayer = playerType.HasFlag((Enum) PlayerType.Ally);
      this.m_color = Color.white;
      this.m_targetState = FloorMechanismBase.TargetState.None;
      this.m_renderer.sprite = this.m_resources.sprites[0];
      this.m_renderer.color = this.m_stateColor;
      this.m_stateColor = this.m_resources.feedbackColors.GetPlayerColor(playerType);
      this.Refresh();
    }

    public void SetTargetState(FloorMechanismBase.TargetState state)
    {
      if (state == this.m_targetState)
        return;
      this.m_targetState = state;
      this.Refresh();
    }

    public void RefreshAssemblage(Vector2Int selfCoords, IEnumerable<Vector2Int> positions)
    {
      int actualValue = 0;
      foreach (Vector2Int position in positions)
      {
        if (position.y == selfCoords.y)
        {
          if (position.x == selfCoords.x - 1)
            actualValue |= 1;
          else if (position.x == selfCoords.x + 1)
            actualValue |= 2;
        }
        else if (position.x == selfCoords.x)
        {
          if (position.y == selfCoords.y - 1)
            actualValue |= 8;
          else if (position.y == selfCoords.y + 1)
            actualValue |= 4;
        }
      }
      int index;
      float y;
      switch (actualValue)
      {
        case 0:
          index = 0;
          y = 0.0f;
          break;
        case 1:
          index = 1;
          y = 90f;
          break;
        case 2:
          index = 1;
          y = -90f;
          break;
        case 3:
          index = 3;
          y = 0.0f;
          break;
        case 4:
          index = 1;
          y = 180f;
          break;
        case 5:
          index = 2;
          y = -90f;
          break;
        case 6:
          index = 2;
          y = 0.0f;
          break;
        case 7:
          index = 4;
          y = 180f;
          break;
        case 8:
          index = 1;
          y = 0.0f;
          break;
        case 9:
          index = 2;
          y = 180f;
          break;
        case 10:
          index = 2;
          y = 90f;
          break;
        case 11:
          index = 4;
          y = 0.0f;
          break;
        case 12:
          index = 3;
          y = -90f;
          break;
        case 13:
          index = 4;
          y = 90f;
          break;
        case 14:
          index = 4;
          y = -90f;
          break;
        case 15:
          index = 5;
          y = 0.0f;
          break;
        default:
          throw new ArgumentOutOfRangeException("bitSet", (object) actualValue, "Invalid bitSet.");
      }
      this.m_renderer.sprite = this.m_resources.sprites[index];
      this.m_renderer.transform.rotation = Quaternion.Euler(90f, y, 0.0f);
    }

    private void Refresh() => this.m_renderer.color = this.m_color * this.m_stateColor;

    public enum TargetState
    {
      None,
      Targetable,
      Targeted,
    }
  }
}
