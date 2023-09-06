// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.ItemDragNDropListener
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components
{
  public class ItemDragNDropListener : MonoBehaviour
  {
    private readonly Dictionary<System.Type, List<DragNDropClient>> m_clients = new Dictionary<System.Type, List<DragNDropClient>>();
    private CellRenderer m_currentRenderer;
    private DragNDropClient m_sourceClient;
    private CellRenderer m_copy;
    private RectTransform m_copyTransform;
    private PointerEventData m_lastEvent;
    private List<DragNDropClient> m_candidates;
    private Tween m_tweenViewPosition;
    private Tween m_tweenDestroy;

    public event Action OnDragBegin;

    public event Action OnDragEnd;

    public event Action OnDragEndSuccessful;

    private void Awake()
    {
      ItemDragNDropListener.instance = this;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    }

    public bool dragging { get; private set; }

    public static ItemDragNDropListener instance { get; private set; }

    public void Register(DragNDropClient client, System.Type type)
    {
      List<DragNDropClient> dragNdropClientList;
      if (!this.m_clients.TryGetValue(type, out dragNdropClientList))
      {
        dragNdropClientList = new List<DragNDropClient>();
        this.m_clients.Add(type, dragNdropClientList);
      }
      dragNdropClientList.Add(client);
    }

    public void UnRegister(DragNDropClient client, System.Type type)
    {
      List<DragNDropClient> dragNdropClientList;
      if (!this.m_clients.TryGetValue(type, out dragNdropClientList))
        return;
      if (client != null)
      {
        dragNdropClientList.Remove(client);
      }
      else
      {
        for (int index = dragNdropClientList.Count - 1; index >= 0; --index)
        {
          if (dragNdropClientList[index] == null)
            dragNdropClientList.RemoveAt(index);
        }
      }
    }

    public void CancelAll()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_copy.gameObject);
      this.dragging = false;
      this.m_currentRenderer = (CellRenderer) null;
      this.m_sourceClient = (DragNDropClient) null;
      this.m_copy = (CellRenderer) null;
      this.m_tweenDestroy = (Tween) null;
      DragNDropListener.instance.CancelAll();
    }

    public object DraggedValue => !((UnityEngine.Object) this.m_currentRenderer == (UnityEngine.Object) null) ? this.m_currentRenderer.value : (object) null;

    public void OnBeginDrag(PointerEventData eventData, CellRenderer cellRenderer)
    {
      Tween tweenDestroy = this.m_tweenDestroy;
      if (tweenDestroy != null)
        tweenDestroy.Kill();
      if ((UnityEngine.Object) this.m_currentRenderer != (UnityEngine.Object) null)
        return;
      this.dragging = true;
      this.m_currentRenderer = cellRenderer;
      this.m_sourceClient = cellRenderer.dragNDropClient;
      this.m_copy = cellRenderer.Clone();
      this.m_copy.dragNDropClient = (DragNDropClient) null;
      this.m_copyTransform = this.m_copy.rectTransform;
      Vector3 localPosition = this.m_copyTransform.localPosition;
      Rect rect = this.m_copyTransform.rect;
      Vector2 vector2 = new Vector2(0.5f, 0.5f);
      Vector2 pivot = this.m_copyTransform.pivot;
      this.m_copyTransform.anchorMin = new Vector2(0.5f, 0.5f);
      this.m_copyTransform.anchorMax = new Vector2(0.5f, 0.5f);
      this.m_copyTransform.pivot = vector2;
      this.m_copyTransform.sizeDelta = new Vector2(rect.width, rect.height);
      Vector3 vector3_1 = (Vector3) (vector2 - pivot);
      Vector3 vector3_2 = (Vector3) new Vector2(rect.width * vector3_1.x, rect.height * vector3_1.y);
      this.m_copyTransform.localPosition = localPosition + vector3_2;
      DragNDropListener.instance.OnBeginDrag(eventData.position, eventData.pressEventCamera, this.m_copyTransform);
      this.m_clients.TryGetValue(cellRenderer.GetValueType(), out this.m_candidates);
      Action onDragBegin = this.OnDragBegin;
      if (onDragBegin == null)
        return;
      onDragBegin();
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.dragging)
        return;
      DragNDropListener.instance.OnDrag(eventData.position, eventData.pressEventCamera);
      int index = 0;
      for (int count = this.m_candidates.Count; index < count; ++index)
      {
        DragNDropClient candidate = this.m_candidates[index];
        if (candidate.activeInHierarchy && RectTransformUtility.RectangleContainsScreenPoint(candidate.rectTransform, eventData.position, eventData.pressEventCamera))
        {
          candidate.OnDragOver(this.m_copy, eventData);
          break;
        }
      }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (!this.dragging)
        return;
      this.m_lastEvent = eventData;
      bool flag1 = false;
      bool flag2 = true;
      bool flag3 = true;
      bool flag4 = true;
      int index = 0;
      for (int count = this.m_candidates.Count; index < count; ++index)
      {
        DragNDropClient candidate = this.m_candidates[index];
        if (candidate.activeInHierarchy && RectTransformUtility.RectangleContainsScreenPoint(candidate.rectTransform, eventData.position, eventData.pressEventCamera))
        {
          flag2 = candidate.OnDrop(this.m_copy, eventData);
          flag1 = true;
          break;
        }
      }
      if (!flag1)
      {
        flag2 = this.m_sourceClient.OnDropOut(this.m_copy, eventData);
        flag3 = !flag2;
      }
      else
      {
        Action dragEndSuccessful = this.OnDragEndSuccessful;
        if (dragEndSuccessful != null)
          dragEndSuccessful();
      }
      if (flag2)
      {
        if (flag3)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.m_copy.gameObject);
        }
        else
        {
          this.m_tweenDestroy = (Tween) this.m_copy.DestroySequence();
          if (this.m_tweenDestroy == null)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) this.m_copy.gameObject);
          }
          else
          {
            this.m_tweenDestroy.OnKill<Tween>(new TweenCallback(this.OnDestroySequenceEnd));
            flag4 = false;
          }
        }
      }
      if (!flag4)
        return;
      this.EndDragAction();
    }

    private void OnDestroySequenceEnd()
    {
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_copy.gameObject);
      this.EndDragAction();
    }

    private void EndDragAction()
    {
      this.dragging = false;
      this.m_currentRenderer = (CellRenderer) null;
      this.m_sourceClient = (DragNDropClient) null;
      this.m_copy = (CellRenderer) null;
      this.m_tweenDestroy = (Tween) null;
      DragNDropListener.instance.OnEndDrag();
      Action onDragEnd = this.OnDragEnd;
      if (onDragEnd == null)
        return;
      onDragEnd();
    }
  }
}
