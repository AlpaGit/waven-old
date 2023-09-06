// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DragNDropElement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.DeckMaker
{
  public abstract class DragNDropElement : 
    MonoBehaviour,
    IBeginDragHandler,
    IEventSystemHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
  {
    [SerializeField]
    protected RectTransform m_content;
    [SerializeField]
    protected RectTransform m_subContent;
    [SerializeField]
    protected CanvasGroup m_canvasGroup;
    private DndElementState m_state;
    private bool m_buttonPressed;
    private bool m_onTarget;
    private bool m_skipEndDragEvent;
    protected Tween m_animationTween;
    protected Transform m_contentParent;
    private Vector3 m_contentPosition;
    private Vector2 m_contentAnchorMin;
    private Vector2 m_contentAnchorMax;
    private Vector2 m_contentPivot;
    private Vector2 m_contentSizeDelta;
    private bool m_enableDnd;
    private DndCastBehaviour m_castBehaviour;
    private bool m_contentParametersInitialized;
    private bool m_draggingInternal;
    private bool m_wasOnTarget;

    public bool enableDnd
    {
      set => this.m_enableDnd = value;
    }

    public DndCastBehaviour castBehaviour
    {
      set => this.m_castBehaviour = value;
    }

    private Camera m_camera => UIManager.instance.GetCamera().camera;

    public event Func<bool> OnDragBeginRequest;

    public event Action<bool> OnDragBegin;

    public event Action<bool> OnDragEnd;

    public bool SkipEndDragEvent
    {
      set => this.m_skipEndDragEvent = value;
    }

    public void OnEnterTarget()
    {
      if (this.m_onTarget || this.m_state != DndElementState.Drag && this.m_state != DndElementState.SimulatedDrag)
        return;
      if (this.m_state == DndElementState.Drag)
        this.m_state = DndElementState.DragTargeting;
      if (this.m_state == DndElementState.SimulatedDrag)
        this.m_state = DndElementState.SimulatedDragTargeting;
      this.m_onTarget = true;
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_animationTween = this.OnEnterTargetTween();
    }

    public void OnExitTarget()
    {
      if (!this.m_onTarget || this.m_state != DndElementState.DragTargeting && this.m_state != DndElementState.SimulatedDragTargeting)
        return;
      if (this.m_state == DndElementState.DragTargeting)
        this.m_state = DndElementState.Drag;
      if (this.m_state == DndElementState.SimulatedDragTargeting)
        this.m_state = DndElementState.SimulatedDrag;
      this.m_onTarget = false;
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_animationTween = this.OnExitTargetTween();
    }

    public void StartCast() => this.m_state = DndElementState.Casting;

    public void CancelCast() => this.EndDrag(true, DndCastBehaviour.MoveBack);

    public void DoneCasting() => this.EndDrag(true, this.m_castBehaviour);

    private void Update()
    {
      bool flag = this.m_state == DndElementState.SimulatedDrag || this.m_state == DndElementState.SimulatedDragTargeting;
      if (flag)
        DragNDropListener.instance.OnDrag((Vector2) InputUtility.pointerPosition, this.m_camera);
      if (this.m_skipEndDragEvent)
        return;
      if (this.m_buttonPressed && InputUtility.GetPointerUp())
      {
        this.m_buttonPressed = false;
        this.EndDrag(false, DndCastBehaviour.MoveBack);
      }
      else
      {
        if (!flag || !InputUtility.GetPointerDown())
          return;
        this.m_buttonPressed = true;
      }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      this.m_draggingInternal = true;
      if (DragNDropListener.instance.dragging || !this.m_enableDnd || this.m_state != DndElementState.Idle || this.OnDragBeginRequest != null && !this.OnDragBeginRequest())
        return;
      this.m_state = DndElementState.Drag;
      this.BeginDrag();
      DragNDropListener.instance.OnBeginDrag(eventData.position, this.m_camera, this.m_content);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (this.m_state != DndElementState.Drag && this.m_state != DndElementState.DragTargeting)
        return;
      DragNDropListener.instance.OnDrag(eventData.position, this.m_camera);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      this.m_draggingInternal = false;
      if (this.m_skipEndDragEvent || this.m_state != DndElementState.Drag && this.m_state != DndElementState.DragTargeting)
        return;
      this.EndDrag(false, DndCastBehaviour.MoveBack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (this.m_draggingInternal || DragNDropListener.instance.dragging || !this.m_enableDnd || this.m_state != DndElementState.Idle || this.OnDragBeginRequest != null && !this.OnDragBeginRequest())
        return;
      this.m_state = DndElementState.SimulatedDrag;
      this.BeginDrag();
      DragNDropListener.instance.OnBeginDrag(eventData.position, this.m_camera, this.m_content);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (!this.m_enableDnd || DragNDropListener.instance.dragging)
        return;
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_animationTween = this.OnPointerEnterTween();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      if (!this.m_enableDnd || DragNDropListener.instance.dragging)
        return;
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.m_animationTween = this.OnPointerExitTween();
    }

    protected void InitMove()
    {
      this.m_contentParent = this.m_content.parent;
      this.m_contentPosition = this.m_content.localPosition;
      this.m_contentAnchorMin = this.m_content.anchorMin;
      this.m_contentAnchorMax = this.m_content.anchorMax;
      this.m_contentPivot = this.m_content.pivot;
      this.m_contentSizeDelta = this.m_content.sizeDelta;
      this.m_contentParametersInitialized = true;
      Vector2 vector2_1 = new Vector2(0.5f, 0.0f);
      Rect rect1 = this.m_content.rect;
      Vector2 vector2_2 = vector2_1 - this.m_content.pivot;
      Vector2 vector2_3 = new Vector2(rect1.width * vector2_2.x, rect1.height * vector2_2.y);
      this.m_content.anchorMin = new Vector2(0.5f, 0.5f);
      this.m_content.anchorMax = new Vector2(0.5f, 0.5f);
      this.m_content.pivot = vector2_1;
      this.m_content.sizeDelta = rect1.size;
      this.m_content.anchoredPosition = new Vector2(this.m_contentPosition.x + vector2_3.x, this.m_contentPosition.y + vector2_3.y);
      Rect rect2 = this.m_subContent.rect;
      this.m_subContent.anchorMin = new Vector2(0.5f, 0.5f);
      this.m_subContent.anchorMax = new Vector2(0.5f, 0.5f);
      this.m_subContent.pivot = new Vector2(0.5f, 0.5f);
      this.m_subContent.anchoredPosition = new Vector2(0.0f, 0.0f);
      this.m_subContent.sizeDelta = rect2.size;
    }

    private void BeginDrag()
    {
      Tween animationTween = this.m_animationTween;
      if (animationTween != null)
        animationTween.Kill();
      this.InitMove();
      Action<bool> onDragBegin = this.OnDragBegin;
      if (onDragBegin == null)
        return;
      onDragBegin(this.m_state == DndElementState.SimulatedDrag);
    }

    private void EndDrag(bool force, DndCastBehaviour behaviour)
    {
      if ((this.m_state == DndElementState.Drag ? 1 : (this.m_state == DndElementState.SimulatedDrag ? 1 : 0)) == 0 && !force)
        return;
      DragNDropListener.instance.OnEndDrag();
      this.m_state = DndElementState.Idle;
      this.m_content.SetParent(this.m_contentParent);
      if (behaviour == DndCastBehaviour.MoveBack)
      {
        Rect rect = this.m_content.rect;
        Vector2 anchoredPosition = this.m_content.anchoredPosition;
        Vector2 vector2_1 = this.m_contentPivot - this.m_content.pivot;
        Vector2 vector2_2 = new Vector2(rect.width * vector2_1.x, rect.height * vector2_1.y);
        this.m_content.anchorMin = this.m_contentAnchorMin;
        this.m_content.anchorMax = this.m_contentAnchorMax;
        this.m_content.sizeDelta = this.m_contentSizeDelta;
        this.m_content.pivot = this.m_contentPivot;
        this.m_content.anchoredPosition = new Vector2(anchoredPosition.x + vector2_2.x, anchoredPosition.y + vector2_2.y);
        this.m_subContent.anchorMin = Vector2.zero;
        this.m_subContent.anchorMax = Vector2.one;
        this.m_subContent.sizeDelta = Vector2.zero;
        Tween animationTween = this.m_animationTween;
        if (animationTween != null)
          animationTween.Kill();
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0.0f, (Tween) this.m_content.DOLocalMove(this.m_contentPosition, 0.3f, true).SetEase<Tweener>(Ease.OutExpo));
        sequence.Insert(0.0f, (Tween) this.m_content.DOScale(Vector3.one, 0.3f).SetEase<Tweener>(Ease.OutBack));
        sequence.Insert(0.0f, (Tween) this.m_subContent.DOAnchorPos((Vector2) Vector3.zero, 0.3f));
        sequence.Insert(0.0f, (Tween) this.m_subContent.DOLocalRotate(Vector3.zero, 0.3f));
        this.m_animationTween = (Tween) sequence.OnKill<Sequence>(new TweenCallback(this.OnEndDragEnd));
      }
      else
        this.m_animationTween = (Tween) this.m_canvasGroup.DOFade(0.0f, 0.3f).OnKill<Tweener>(new TweenCallback(this.OnEndDragEnd));
      this.m_wasOnTarget = this.m_onTarget;
      this.m_buttonPressed = false;
      this.m_onTarget = false;
    }

    private void OnEndDragEnd()
    {
      Action<bool> onDragEnd = this.OnDragEnd;
      if (onDragEnd != null)
        onDragEnd(this.m_wasOnTarget);
      this.m_content.localPosition = this.m_contentPosition;
      this.m_content.sizeDelta = this.m_contentSizeDelta;
      this.m_content.localScale = Vector3.one;
      this.m_subContent.localRotation = Quaternion.Euler(Vector3.zero);
      this.m_subContent.anchoredPosition = Vector2.zero;
    }

    public void ResetContentPosition()
    {
      this.m_content.gameObject.SetActive(true);
      this.m_canvasGroup.alpha = 1f;
      if (!this.m_contentParametersInitialized)
        return;
      this.m_contentParametersInitialized = false;
      this.m_content.anchorMin = this.m_contentAnchorMin;
      this.m_content.anchorMax = this.m_contentAnchorMax;
      this.m_content.pivot = this.m_contentPivot;
      this.m_content.localPosition = this.m_contentPosition;
      this.m_content.sizeDelta = this.m_contentSizeDelta;
      this.m_content.localScale = Vector3.one;
      this.m_subContent.localRotation = Quaternion.Euler(Vector3.zero);
      this.m_subContent.anchorMin = Vector2.zero;
      this.m_subContent.anchorMax = Vector2.one;
      this.m_subContent.anchoredPosition = Vector2.zero;
      this.m_subContent.sizeDelta = Vector2.zero;
    }

    protected abstract Tween OnPointerEnterTween();

    protected abstract Tween OnPointerExitTween();

    protected abstract Tween OnEnterTargetTween();

    protected abstract Tween OnExitTargetTween();

    private void OnDestroy()
    {
      if (this.m_state == DndElementState.Idle)
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_content);
      DragNDropListener.instance.CancelAll();
    }
  }
}
