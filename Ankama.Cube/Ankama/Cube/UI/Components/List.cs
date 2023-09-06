// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.List
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (RectMask2D))]
  public abstract class List : 
    MonoBehaviour,
    DragNDropClient,
    IBeginDragHandler,
    IEventSystemHandler,
    IDragHandler,
    IEndDragHandler
  {
    [Header("Cell")]
    [SerializeField]
    protected GameObject m_prefab;
    [Header("List configuration")]
    [SerializeField]
    protected bool m_horizontal;
    [SerializeField]
    protected Vector2Int m_cellSize;
    private RectTransform m_rectTransform;
    private System.Type m_itemType;
    private CellRenderer m_cellRenderer;
    protected int m_totalWidth;
    protected int m_totalHeight;
    private readonly List<CellRenderer> m_elementPool = new List<CellRenderer>();
    protected readonly List<object> m_items = new List<object>();
    protected readonly Dictionary<int, CellRenderer> m_elementsByItem = new Dictionary<int, CellRenderer>();
    protected bool m_needFullReLayout;
    protected int m_extraLineCount = 1;
    [NonSerialized]
    private bool m_initialized;

    public void SetValues<T>(IEnumerable<T> values) where T : class
    {
      this.CheckInit();
      if (!this.m_itemType.IsAssignableFrom(typeof (T)))
      {
        Debug.LogWarningFormat("Wrong value type set in list {0}. Expected : {1}. Got {2}", (object) this.name, (object) this.m_itemType.Name, (object) typeof (T).Name);
      }
      else
      {
        this.m_items.Clear();
        this.m_items.AddRange((IEnumerable<object>) values);
        this.ComputeDimensions();
      }
    }

    public float scrollPercentage { protected get; set; }

    protected virtual void Awake() => this.CheckInit();

    protected virtual void CheckInit()
    {
      if (this.m_initialized)
        return;
      this.m_rectTransform = this.GetComponent<RectTransform>();
      if (this.m_cellSize.x <= 0 || this.m_cellSize.y <= 0)
        this.m_cellSize = new Vector2Int(10, 10);
      this.m_cellRenderer = this.m_prefab.GetComponent<CellRenderer>();
      if ((UnityEngine.Object) this.m_cellRenderer == (UnityEngine.Object) null)
      {
        Debug.LogWarningFormat("No valid ItemElement found in the prefab {0} for list {1}", (object) this.m_prefab.name, (object) this.name);
      }
      else
      {
        this.m_itemType = this.m_cellRenderer.GetValueType();
        ItemDragNDropListener.instance.Register((DragNDropClient) this, this.m_itemType);
        this.m_initialized = true;
      }
    }

    private void OnRectTransformDimensionsChange() => this.ComputeDimensions();

    protected virtual void ComputeDimensions()
    {
      this.CheckInit();
      Rect rect = this.m_rectTransform.rect;
      this.m_totalWidth = Math.Max(1, (int) rect.width);
      this.m_totalHeight = Math.Max(1, (int) rect.height);
    }

    protected void ReturnToPool(CellRenderer cellRenderer)
    {
      if ((UnityEngine.Object) cellRenderer == (UnityEngine.Object) null)
        return;
      this.m_elementPool.Add(cellRenderer);
      cellRenderer.gameObject.SetActive(false);
    }

    protected CellRenderer GetFromPool()
    {
      if (this.m_elementPool.Count == 0)
      {
        CellRenderer fromPool = UnityEngine.Object.Instantiate<CellRenderer>(this.m_cellRenderer);
        RectTransform component = fromPool.GetComponent<RectTransform>();
        component.SetParent(this.transform, false);
        component.sizeDelta = (Vector2) this.m_cellSize;
        this.SetCellRectTransformAnchors(component);
        fromPool.dragNDropClient = (DragNDropClient) this;
        return fromPool;
      }
      CellRenderer fromPool1 = this.m_elementPool[this.m_elementPool.Count - 1];
      this.m_elementPool.RemoveAt(this.m_elementPool.Count - 1);
      fromPool1.gameObject.SetActive(true);
      return fromPool1;
    }

    protected abstract void SetCellRectTransformAnchors(RectTransform rectTransform);

    public RectTransform rectTransform => this.m_rectTransform;

    public bool activeInHierarchy => this.gameObject.activeInHierarchy;

    public void OnBeginDrag(PointerEventData evt)
    {
      if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_rectTransform, evt.position, evt.pressEventCamera))
        return;
      foreach (KeyValuePair<int, CellRenderer> keyValuePair in this.m_elementsByItem)
      {
        CellRenderer cellRenderer = keyValuePair.Value;
        if (RectTransformUtility.RectangleContainsScreenPoint(cellRenderer.rectTransform, evt.position, evt.pressEventCamera))
        {
          if (this.m_items.IndexOf(cellRenderer.value) == -1)
            break;
          ItemDragNDropListener.instance.OnBeginDrag(evt, cellRenderer);
          break;
        }
      }
    }

    public void OnDrag(PointerEventData eventData) => ItemDragNDropListener.instance.OnDrag(eventData);

    public void OnEndDrag(PointerEventData eventData) => ItemDragNDropListener.instance.OnEndDrag(eventData);

    public void OnDragOver(CellRenderer cellRenderer, PointerEventData evt)
    {
    }

    public bool OnDropOut(CellRenderer cellRenderer, PointerEventData evt) => true;

    public bool OnDrop(CellRenderer cellRenderer, PointerEventData evt) => true;

    private void OnDestroy() => ItemDragNDropListener.instance.UnRegister((DragNDropClient) this, this.m_itemType);
  }
}
