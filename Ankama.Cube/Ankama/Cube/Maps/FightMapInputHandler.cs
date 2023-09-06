// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapInputHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.UI;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.Maps
{
  public class FightMapInputHandler
  {
    private readonly Camera m_camera;
    private readonly CameraHandler m_cameraHandler;
    private readonly Collider m_collider;
    private bool m_forceUpdate;
    private bool m_mouseButtonIsDown;
    private Vector3 m_previousMousePosition;

    public Vector2Int? targetCell { get; private set; }

    public bool pressedMouseButton { get; private set; }

    public bool releasedMouseButton { get; private set; }

    public bool clickedMouseButton { get; private set; }

    public Vector2Int? mouseButtonPressLocation { get; private set; }

    public Vector2Int? mouseButtonReleaseLocation { get; private set; }

    public FightMapInputHandler([NotNull] Collider collider, [CanBeNull] CameraHandler cameraHandler)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) collider)
        throw new ArgumentNullException(nameof (collider));
      this.m_camera = (UnityEngine.Object) null == (UnityEngine.Object) cameraHandler ? Camera.main : cameraHandler.camera;
      this.m_cameraHandler = cameraHandler;
      this.m_collider = collider;
      this.m_previousMousePosition = InputUtility.pointerPosition;
      if (!((UnityEngine.Object) cameraHandler != (UnityEngine.Object) null))
        return;
      cameraHandler.onMoved += new Action<CameraHandler>(this.ForceUpdate);
      cameraHandler.onZoomChanged += new Action<CameraHandler>(this.ForceUpdate);
    }

    public void SetDirty() => this.m_forceUpdate = true;

    private void ForceUpdate(CameraHandler cameraHandler) => this.m_forceUpdate = true;

    public bool Update(IMapDefinition mapDefinition)
    {
      EventSystem current = EventSystem.current;
      if ((UnityEngine.Object) null != (UnityEngine.Object) current && current.IsPointerOverGameObject())
      {
        Vector2Int? nullable;
        if (InputUtility.GetPointerUp())
        {
          int num;
          if (this.m_mouseButtonIsDown)
          {
            Vector2Int? buttonPressLocation = this.mouseButtonPressLocation;
            nullable = this.targetCell;
            num = buttonPressLocation.HasValue == nullable.HasValue ? (buttonPressLocation.HasValue ? (buttonPressLocation.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0;
          }
          else
            num = 0;
          this.clickedMouseButton = num != 0;
          this.pressedMouseButton = false;
          this.releasedMouseButton = true;
          nullable = new Vector2Int?();
          this.mouseButtonPressLocation = nullable;
          this.mouseButtonReleaseLocation = this.targetCell;
          this.m_mouseButtonIsDown = false;
        }
        else
        {
          this.clickedMouseButton = false;
          this.pressedMouseButton = false;
          this.releasedMouseButton = false;
        }
        nullable = this.targetCell;
        if (!nullable.HasValue)
          return false;
        nullable = new Vector2Int?();
        this.targetCell = nullable;
        return true;
      }
      bool flag1 = false;
      Vector3 pointerPosition = InputUtility.pointerPosition;
      bool flag2 = pointerPosition != this.m_previousMousePosition;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_cameraHandler && this.m_cameraHandler.userHasControl && this.m_camera.pixelRect.Contains(pointerPosition))
      {
        if (InputUtility.GetTertiaryDown())
        {
          FightStatus local = FightStatus.local;
          if (local != null)
            this.m_cameraHandler.StartFocusOnMapRegion(mapDefinition, local.fightId);
          else
            this.m_cameraHandler.StartFocusOnMapRegion(mapDefinition, 0);
        }
        else if (InputUtility.IsSecondaryDown())
        {
          if (flag2)
            this.m_cameraHandler.Pan((Vector2) pointerPosition, (Vector2) this.m_previousMousePosition);
        }
        else
        {
          float y = Input.mouseScrollDelta.y;
          if ((double) Math.Abs(y) > 1.4012984643248171E-45)
            this.m_cameraHandler.TweenZoom(y);
        }
      }
      Vector2Int? nullable1;
      if (flag2 || this.m_forceUpdate)
      {
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_camera)
        {
          RaycastHit hitInfo;
          if (this.m_collider.Raycast(this.m_camera.ScreenPointToRay(pointerPosition), out hitInfo, this.m_camera.farClipPlane))
          {
            Vector3 vector3 = hitInfo.point - this.m_collider.transform.position;
            Vector2Int vector2Int = new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.z));
            nullable1 = this.targetCell;
            if (nullable1.HasValue)
            {
              nullable1 = this.targetCell;
              if (!(nullable1.Value != vector2Int))
                goto label_28;
            }
            this.targetCell = new Vector2Int?(vector2Int);
            flag1 = true;
          }
          else if (this.targetCell.HasValue)
          {
            this.targetCell = new Vector2Int?();
            flag1 = true;
          }
        }
label_28:
        this.m_forceUpdate = false;
        this.m_previousMousePosition = pointerPosition;
      }
      if (InputUtility.GetPointerDown())
      {
        this.pressedMouseButton = true;
        this.releasedMouseButton = false;
        this.clickedMouseButton = false;
        this.mouseButtonPressLocation = this.targetCell;
        nullable1 = new Vector2Int?();
        this.mouseButtonReleaseLocation = nullable1;
        this.m_mouseButtonIsDown = true;
      }
      else if (InputUtility.GetPointerUp())
      {
        Vector2Int? nullable2;
        int num;
        if (this.m_mouseButtonIsDown)
        {
          nullable1 = this.mouseButtonPressLocation;
          nullable2 = this.targetCell;
          num = nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0;
        }
        else
          num = 0;
        this.clickedMouseButton = num != 0;
        this.pressedMouseButton = false;
        this.releasedMouseButton = true;
        nullable2 = new Vector2Int?();
        this.mouseButtonPressLocation = nullable2;
        this.mouseButtonReleaseLocation = this.targetCell;
        this.m_mouseButtonIsDown = false;
      }
      else
      {
        this.clickedMouseButton = false;
        this.pressedMouseButton = false;
        this.releasedMouseButton = false;
      }
      return flag1;
    }
  }
}
