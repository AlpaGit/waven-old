// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.DragNDropListener
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components
{
  public class DragNDropListener : MonoBehaviour
  {
    [SerializeField]
    private Canvas m_canvas;
    [SerializeField]
    private RectTransform m_content;
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private float m_scaleFactor = 1.2f;
    [SerializeField]
    private Ease m_scaleEase = Ease.OutBack;
    [SerializeField]
    private float m_scaleTweenDuration = 0.2f;
    [SerializeField]
    private Ease m_moveEase = Ease.InOutQuad;
    [SerializeField]
    private float m_moveTweenDuration = 0.2f;
    private RectTransform m_dragObject;
    private PointerEventData m_lastEvent;
    private Tween m_tweenViewPosition;
    private Vector2? m_snapScreenPosition;
    private Vector2 m_previousPosition;

    public event Action OnDragBegin;

    public event Action OnDragEnd;

    private void Awake()
    {
      DragNDropListener.instance = this;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      this.m_canvas.gameObject.SetActive(false);
      this.m_camera.gameObject.SetActive(false);
    }

    public bool dragging { get; private set; }

    public static DragNDropListener instance { get; private set; }

    public void CancelAll() => this.Clear();

    private void UpdateCamera()
    {
      if (this.dragging)
      {
        float lastDepth = UIManager.instance.lastDepth;
        this.m_canvas.sortingOrder = UIManager.instance.lastSortingOrder;
        this.m_canvas.planeDistance = lastDepth;
        this.m_camera.nearClipPlane = lastDepth;
        this.m_camera.farClipPlane = lastDepth + 1f;
        this.m_canvas.gameObject.SetActive(true);
        this.m_camera.gameObject.SetActive(true);
      }
      else
      {
        this.m_canvas.gameObject.SetActive(false);
        this.m_camera.gameObject.SetActive(false);
      }
    }

    public void SnapDragToScreenPosition(Vector2 position) => this.m_snapScreenPosition = new Vector2?(position);

    public void SnapDragToWorldPosition(Camera cam, Vector3 position) => this.m_snapScreenPosition = new Vector2?(RectTransformUtility.WorldToScreenPoint(cam, position));

    public void CancelSnapDrag() => this.m_snapScreenPosition = new Vector2?();

    public void OnBeginDrag(Vector2 screenPosition, Camera cam, RectTransform dragObject)
    {
      if ((UnityEngine.Object) this.m_dragObject != (UnityEngine.Object) null)
        return;
      this.dragging = true;
      this.UpdateCamera();
      this.m_dragObject = dragObject;
      this.m_dragObject.SetParent((Transform) this.m_content, true);
      this.m_dragObject.anchoredPosition3D = (Vector3) this.m_dragObject.anchoredPosition;
      this.m_dragObject.DOScale(this.m_scaleFactor, this.m_scaleTweenDuration).SetEase<Tweener>(this.m_scaleEase);
      Vector2 localPoint;
      if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_content, this.m_snapScreenPosition ?? screenPosition, cam, out localPoint))
      {
        this.m_previousPosition = localPoint;
        Tween tweenViewPosition = this.m_tweenViewPosition;
        if (tweenViewPosition != null)
          tweenViewPosition.Kill();
        this.m_tweenViewPosition = (Tween) this.m_dragObject.DOAnchorPos3D((Vector3) localPoint, this.m_moveTweenDuration).SetEase<Tweener>(this.m_moveEase);
      }
      Action onDragBegin = this.OnDragBegin;
      if (onDragBegin == null)
        return;
      onDragBegin();
    }

    public void OnDrag(Vector2 screenPosition, Camera cam)
    {
      Vector2 localPoint;
      if (!this.dragging || !RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_content, this.m_snapScreenPosition ?? screenPosition, cam, out localPoint) || this.m_previousPosition == localPoint)
        return;
      this.m_previousPosition = localPoint;
      Tween tweenViewPosition = this.m_tweenViewPosition;
      if (tweenViewPosition != null)
        tweenViewPosition.Kill();
      this.m_tweenViewPosition = (Tween) this.m_dragObject.DOAnchorPos3D((Vector3) localPoint, this.m_moveTweenDuration).SetEase<Tweener>(this.m_moveEase);
      this.m_tweenViewPosition.OnKill<Tween>(new TweenCallback(this.TweenPositionCompleteCallback));
    }

    public void OnEndDrag()
    {
      if (!this.dragging)
        return;
      this.Clear();
      Action onDragEnd = this.OnDragEnd;
      if (onDragEnd == null)
        return;
      onDragEnd();
    }

    private void Clear()
    {
      Tween tweenViewPosition = this.m_tweenViewPosition;
      if (tweenViewPosition != null)
        tweenViewPosition.Kill();
      this.dragging = false;
      this.m_dragObject = (RectTransform) null;
      this.UpdateCamera();
    }

    private void TweenPositionCompleteCallback() => this.m_tweenViewPosition = (Tween) null;
  }
}
