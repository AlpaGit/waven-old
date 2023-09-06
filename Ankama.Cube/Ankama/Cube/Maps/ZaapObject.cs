// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.ZaapObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI;
using DG.Tweening;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class ZaapObject : MonoBehaviour
  {
    [SerializeField]
    private ZaapAnimData m_animData;
    [SerializeField]
    private Transform m_positionToReach;
    [SerializeField]
    private Portal m_portalFX;
    private bool m_interactable = true;
    private bool m_mouseOver;
    private ZaapObject.ZaapState m_state;
    public Action<ZaapObject> onClick;
    public Action<ZaapObject> onPortalBeginOpen;
    public Action<ZaapObject> onPortalEndOpen;

    public ZaapObject.ZaapState state => this.m_state;

    public Vector3 destination => this.m_positionToReach.position;

    public Vector3 outsideDestination => this.m_positionToReach.position - this.m_positionToReach.right;

    public Vector3 destinationLookAt => this.m_positionToReach.right;

    public bool waitingForCharacterToReach { get; set; }

    public bool interactable
    {
      get => this.m_interactable;
      set
      {
        this.m_interactable = value;
        if (value || this.m_state != ZaapObject.ZaapState.Highlight)
          return;
        this.m_state = ZaapObject.ZaapState.Normal;
        this.m_portalFX.SetState(this.m_state);
      }
    }

    private void Awake() => this.m_portalFX.SetState(this.m_state);

    public void UpdateCharacterPos(Vector3 worldPos)
    {
      if (!this.interactable || this.m_state != ZaapObject.ZaapState.Clicked || (double) Vector2.Distance(new Vector2(worldPos.x, worldPos.z), new Vector2(this.m_positionToReach.position.x, this.m_positionToReach.position.z)) >= 0.05000000074505806)
        return;
      this.OpenPortal();
    }

    public void ClosePortal()
    {
      this.m_state = ZaapObject.ZaapState.Normal;
      this.m_portalFX.SetState(this.m_state);
    }

    public void OnClickOutside()
    {
      this.m_state = ZaapObject.ZaapState.Normal;
      this.m_portalFX.SetState(this.m_state);
    }

    private void OnMouseUpAsButton()
    {
      if (!this.interactable || InputUtility.IsMouseOverUI)
        return;
      if (this.m_state == ZaapObject.ZaapState.Clicked)
      {
        this.OpenPortal();
      }
      else
      {
        this.m_state = ZaapObject.ZaapState.Clicked;
        this.m_portalFX.SetState(this.m_state);
        Action<ZaapObject> onClick = this.onClick;
        if (onClick == null)
          return;
        onClick(this);
      }
    }

    private void OpenPortal()
    {
      this.m_state = ZaapObject.ZaapState.Open;
      this.m_portalFX.SetState(this.m_state);
      Action<ZaapObject> onPortalBeginOpen = this.onPortalBeginOpen;
      if (onPortalBeginOpen != null)
        onPortalBeginOpen(this);
      DOVirtual.DelayedCall(this.m_animData.openCallbackDelay, new TweenCallback(this.OpenCallback));
    }

    private void OpenCallback()
    {
      Action<ZaapObject> onPortalEndOpen = this.onPortalEndOpen;
      if (onPortalEndOpen == null)
        return;
      onPortalEndOpen(this);
    }

    private void OnMouseOver()
    {
      if (this.m_mouseOver && InputUtility.IsMouseOverUI)
      {
        this.OnMouseExit();
      }
      else
      {
        if (this.m_mouseOver || InputUtility.IsMouseOverUI)
          return;
        this.OnMouseEnter();
      }
    }

    private void OnMouseEnter()
    {
      if (InputUtility.IsMouseOverUI)
        return;
      this.m_mouseOver = true;
      if (!this.interactable || this.m_state != ZaapObject.ZaapState.Normal)
        return;
      this.m_state = ZaapObject.ZaapState.Highlight;
      this.m_portalFX.SetState(this.m_state);
    }

    private void OnMouseExit()
    {
      this.m_mouseOver = false;
      if (!this.interactable || this.m_state != ZaapObject.ZaapState.Highlight)
        return;
      this.m_state = ZaapObject.ZaapState.Normal;
      this.m_portalFX.SetState(this.m_state);
    }

    public enum ZaapState
    {
      Normal,
      Highlight,
      Clicked,
      Open,
    }
  }
}
