// Decompiled with JetBrains decompiler
// Type: SpellDraggerTest
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellDraggerTest : MonoBehaviour
{
  [SerializeField]
  private Camera cam;
  [SerializeField]
  private SpellHighlight spellHighlight;
  [SerializeField]
  private Vector3 draggingIconScale;
  [SerializeField]
  private float draggingIconRotation;
  [SerializeField]
  private Vector3 targetIconScale;
  [SerializeField]
  private Vector2 targetIconPivot;
  [SerializeField]
  private Vector3 mouseOverPosition;
  [SerializeField]
  private float targetIconRotation;
  [SerializeField]
  private GameObject selectFX;
  private bool dragging;
  private DraggableIcon draggableIcon;
  private FakeCell currentFakeCell;
  public SpellHighlight currentSpellHighlight;
  private List<RaycastResult> hitObjects = new List<RaycastResult>();

  private void Start() => this.selectFX.SetActive(false);

  private void Update()
  {
    DraggableIcon draggableIconUnderMouse = this.GetDraggableIconUnderMouse();
    if ((Object) draggableIconUnderMouse != (Object) null)
    {
      if (!Input.GetMouseButton(0))
      {
        if ((Object) this.draggableIcon != (Object) null)
        {
          if ((Object) this.draggableIcon != (Object) draggableIconUnderMouse)
          {
            this.draggableIcon.SetNewTarget(this.draggableIcon.startPosition);
            this.draggableIcon.SetNewRotation(0.0f);
          }
          else
          {
            this.draggableIcon.SetNewTarget(this.draggableIcon.startPosition + this.mouseOverPosition);
            this.draggableIcon.SetNewRotation(0.0f);
          }
        }
        this.draggableIcon = draggableIconUnderMouse;
      }
      if (Input.GetMouseButtonDown(0))
      {
        this.dragging = true;
        this.selectFX.transform.SetParent((Transform) this.draggableIcon.FXtrsf);
        this.selectFX.SetActive(true);
      }
    }
    else if ((Object) this.draggableIcon != (Object) null)
    {
      this.draggableIcon.SetNewTarget(this.draggableIcon.startPosition);
      this.draggableIcon.SetNewRotation(0.0f);
    }
    if (this.dragging)
    {
      this.selectFX.transform.position = this.draggableIcon.FXtrsf.position;
      RaycastHit hitInfo;
      if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f))
      {
        FakeCell component = hitInfo.collider.gameObject.GetComponent<FakeCell>();
        if ((Object) component != (Object) null)
        {
          if ((Object) this.currentFakeCell != (Object) null && (Object) component != (Object) this.currentFakeCell)
          {
            this.currentFakeCell.highlighted = false;
            if ((Object) this.currentSpellHighlight != (Object) null)
              this.currentSpellHighlight.Kill();
          }
          this.currentFakeCell = component;
          this.currentFakeCell.highlighted = true;
          if (this.currentFakeCell.active)
          {
            this.draggableIcon.SetNewTarget(this.ScreenToCanvas(this.cam.WorldToScreenPoint(this.currentFakeCell.transform.position + new Vector3(0.0f, 2.5f, 0.0f))));
            this.draggableIcon.SetNewRotation(this.targetIconRotation);
            this.draggableIcon.SetNewScale(this.targetIconScale);
            this.draggableIcon.SetNewPivot((Vector3) new Vector2(0.5f, 0.5f));
            this.draggableIcon.SetNewSelected(1f);
            this.draggableIcon.SetNewHighLight(0.0f);
            if ((Object) this.currentSpellHighlight == (Object) null)
            {
              this.currentSpellHighlight = Object.Instantiate<SpellHighlight>(this.spellHighlight, this.currentFakeCell.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 1f));
              this.currentSpellHighlight.SetDragger(this);
            }
          }
          else
          {
            this.draggableIcon.SetNewTarget(this.ScreenToCanvas(Input.mousePosition));
            this.draggableIcon.SetNewRotation(this.draggingIconRotation);
            this.draggableIcon.SetNewScale(this.draggingIconScale);
            this.draggableIcon.SetNewPivot((Vector3) this.targetIconPivot);
            this.draggableIcon.SetNewSelected(1f);
            this.draggableIcon.SetNewHighLight(0.0f);
          }
        }
        else
        {
          this.draggableIcon.SetNewTarget(this.ScreenToCanvas(Input.mousePosition));
          this.draggableIcon.SetNewRotation(this.draggingIconRotation);
          this.draggableIcon.SetNewScale(this.draggingIconScale);
          this.draggableIcon.SetNewPivot((Vector3) this.targetIconPivot);
          this.draggableIcon.SetNewSelected(1f);
          this.draggableIcon.SetNewHighLight(0.0f);
        }
      }
      else
      {
        this.draggableIcon.SetNewTarget(this.ScreenToCanvas(Input.mousePosition));
        this.draggableIcon.SetNewRotation(this.draggingIconRotation);
        this.draggableIcon.SetNewScale(this.draggingIconScale);
        this.draggableIcon.SetNewPivot((Vector3) this.targetIconPivot);
        this.draggableIcon.SetNewSelected(1f);
        this.draggableIcon.SetNewHighLight(0.0f);
        if ((Object) this.currentFakeCell != (Object) null)
        {
          this.currentFakeCell.highlighted = false;
          if ((Object) this.currentSpellHighlight != (Object) null)
            this.currentSpellHighlight.Kill();
        }
      }
    }
    if (!Input.GetMouseButtonUp(0))
      return;
    this.dragging = false;
    if ((Object) this.currentFakeCell != (Object) null)
    {
      this.currentFakeCell.highlighted = false;
      if (this.currentFakeCell.active)
      {
        this.draggableIcon.LaunchSpell();
        if ((Object) this.currentSpellHighlight != (Object) null)
          this.currentSpellHighlight.LaunchSpell();
      }
      else
      {
        if ((Object) this.draggableIcon != (Object) null)
          this.draggableIcon.SetNewTarget(this.draggableIcon.startPosition);
        if ((Object) this.currentSpellHighlight != (Object) null)
          this.currentSpellHighlight.Kill();
      }
    }
    else if ((Object) this.draggableIcon != (Object) null)
      this.draggableIcon.SetNewTarget(this.draggableIcon.startPosition);
    if ((Object) this.draggableIcon != (Object) null)
    {
      Debug.Log((object) "undrag");
      this.selectFX.transform.SetParent(this.transform);
      this.draggableIcon.SetNewRotation(0.0f);
      this.draggableIcon.SetNewScale(new Vector3(1f, 1f, 1f));
      this.draggableIcon.SetNewPivot((Vector3) new Vector2(0.5f, 0.5f));
      this.selectFX.transform.localScale = new Vector3(100f, 100f, 100f);
      this.selectFX.SetActive(false);
      this.draggableIcon.SetNewSelected(0.0f);
      this.draggableIcon.SetNewHighLight(0.0f);
      this.draggableIcon = (DraggableIcon) null;
    }
    this.currentFakeCell = (FakeCell) null;
  }

  private GameObject GetObjectUnderMouse()
  {
    EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current)
    {
      position = (Vector2) Input.mousePosition
    }, this.hitObjects);
    return this.hitObjects.Count <= 0 ? (GameObject) null : this.hitObjects.First<RaycastResult>().gameObject;
  }

  private DraggableIcon GetDraggableIconUnderMouse()
  {
    GameObject objectUnderMouse = this.GetObjectUnderMouse();
    return (Object) objectUnderMouse != (Object) null ? objectUnderMouse.GetComponent<DraggableIcon>() : (DraggableIcon) null;
  }

  private Vector3 ScreenToCanvas(Vector3 screenPos)
  {
    screenPos.x /= (float) Screen.width;
    screenPos.y /= (float) Screen.height;
    return new Vector3(Mathf.Lerp(-960f, 960f, screenPos.x), Mathf.Lerp(-540f, 540f, screenPos.y), 0.0f);
  }
}
