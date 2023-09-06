// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.CustomGraphicRaycaster
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  [AddComponentMenu("Event/Custom Graphic Raycaster")]
  [RequireComponent(typeof (Canvas))]
  public class CustomGraphicRaycaster : BaseRaycaster
  {
    private Canvas m_Canvas;
    [NonSerialized]
    private List<Graphic> m_RaycastResults = new List<Graphic>();
    [NonSerialized]
    private static readonly List<Graphic> s_SortedGraphics = new List<Graphic>();

    public override int sortOrderPriority => this.canvas.renderMode == RenderMode.ScreenSpaceOverlay ? this.canvas.sortingOrder : base.sortOrderPriority;

    public override int renderOrderPriority
    {
      get
      {
        if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
          return -Mathf.RoundToInt(this.canvas.rootCanvas.planeDistance);
        return this.canvas.renderMode == RenderMode.ScreenSpaceOverlay ? this.canvas.rootCanvas.renderOrder : base.renderOrderPriority;
      }
    }

    protected CustomGraphicRaycaster()
    {
    }

    private Canvas canvas
    {
      get
      {
        if ((UnityEngine.Object) this.m_Canvas != (UnityEngine.Object) null)
          return this.m_Canvas;
        this.m_Canvas = this.GetComponent<Canvas>();
        return this.m_Canvas;
      }
    }

    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
      if ((UnityEngine.Object) this.canvas == (UnityEngine.Object) null)
        return;
      IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(this.canvas);
      if (graphicsForCanvas == null || graphicsForCanvas.Count == 0)
        return;
      Camera eventCamera = this.eventCamera;
      int index1 = this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || (UnityEngine.Object) eventCamera == (UnityEngine.Object) null ? this.canvas.targetDisplay : eventCamera.targetDisplay;
      Vector3 vector3 = Display.RelativeMouseAt((Vector3) eventData.position);
      if (vector3 != Vector3.zero)
      {
        if ((int) vector3.z != index1)
          return;
      }
      else
        vector3 = (Vector3) eventData.position;
      Vector2 vector2;
      if ((UnityEngine.Object) eventCamera == (UnityEngine.Object) null)
      {
        float num1 = (float) Screen.width;
        float num2 = (float) Screen.height;
        if (index1 > 0 && index1 < Display.displays.Length)
        {
          num1 = (float) Display.displays[index1].systemWidth;
          num2 = (float) Display.displays[index1].systemHeight;
        }
        vector2 = new Vector2(vector3.x / num1, vector3.y / num2);
      }
      else
        vector2 = (Vector2) eventCamera.ScreenToViewportPoint(vector3);
      if ((double) vector2.x < 0.0 || (double) vector2.x > 1.0 || (double) vector2.y < 0.0 || (double) vector2.y > 1.0)
        return;
      float maxValue = float.MaxValue;
      Ray ray = new Ray();
      if ((UnityEngine.Object) eventCamera != (UnityEngine.Object) null)
        ray = eventCamera.ScreenPointToRay(vector3);
      this.m_RaycastResults.Clear();
      CustomGraphicRaycaster.Raycast(this.canvas, eventCamera, (Vector2) vector3, graphicsForCanvas, this.m_RaycastResults);
      int count = this.m_RaycastResults.Count;
      for (int index2 = 0; index2 < count; ++index2)
      {
        GameObject gameObject = this.m_RaycastResults[index2].gameObject;
        if (true)
        {
          float num;
          if ((UnityEngine.Object) eventCamera == (UnityEngine.Object) null || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
          {
            num = 0.0f;
          }
          else
          {
            Transform transform = gameObject.transform;
            Vector3 forward = transform.forward;
            num = Vector3.Dot(forward, transform.position - eventCamera.transform.position) / Vector3.Dot(forward, ray.direction);
            if ((double) num < 0.0)
              continue;
          }
          if ((double) num < (double) maxValue)
          {
            RaycastResult raycastResult = new RaycastResult()
            {
              gameObject = gameObject,
              module = (BaseRaycaster) this,
              distance = num,
              screenPosition = (Vector2) vector3,
              index = (float) resultAppendList.Count,
              depth = this.m_RaycastResults[index2].depth,
              sortingLayer = this.canvas.sortingLayerID,
              sortingOrder = this.canvas.sortingOrder
            };
            resultAppendList.Add(raycastResult);
          }
        }
      }
    }

    public override Camera eventCamera
    {
      get
      {
        if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay || this.canvas.renderMode == RenderMode.ScreenSpaceCamera && (UnityEngine.Object) this.canvas.worldCamera == (UnityEngine.Object) null)
          return (Camera) null;
        return !((UnityEngine.Object) this.canvas.worldCamera != (UnityEngine.Object) null) ? Camera.main : this.canvas.worldCamera;
      }
    }

    private static void Raycast(
      Canvas canvas,
      Camera eventCamera,
      Vector2 pointerPosition,
      IList<Graphic> foundGraphics,
      List<Graphic> results)
    {
      int count1 = foundGraphics.Count;
      for (int index = 0; index < count1; ++index)
      {
        Graphic foundGraphic = foundGraphics[index];
        if (foundGraphic.depth != -1 && foundGraphic.raycastTarget && !foundGraphic.canvasRenderer.cull && RectTransformUtility.RectangleContainsScreenPoint(foundGraphic.rectTransform, pointerPosition, eventCamera) && (!((UnityEngine.Object) eventCamera != (UnityEngine.Object) null) || (double) eventCamera.WorldToScreenPoint(foundGraphic.rectTransform.position).z <= (double) eventCamera.farClipPlane) && foundGraphic.Raycast(pointerPosition, eventCamera))
          CustomGraphicRaycaster.s_SortedGraphics.Add(foundGraphic);
      }
      CustomGraphicRaycaster.s_SortedGraphics.Sort((Comparison<Graphic>) ((g1, g2) => g2.depth.CompareTo(g1.depth)));
      int count2 = CustomGraphicRaycaster.s_SortedGraphics.Count;
      for (int index = 0; index < count2; ++index)
        results.Add(CustomGraphicRaycaster.s_SortedGraphics[index]);
      CustomGraphicRaycaster.s_SortedGraphics.Clear();
    }
  }
}
