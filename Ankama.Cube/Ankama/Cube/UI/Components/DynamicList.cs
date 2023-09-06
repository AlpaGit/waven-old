// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.DynamicList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (ScrollRect))]
  public class DynamicList : 
    MonoBehaviour,
    DragNDropClient,
    IBeginDragHandler,
    IEventSystemHandler,
    IDragHandler,
    IEndDragHandler
  {
    [Header("Elements")]
    [SerializeField]
    private RectTransform m_viewport;
    [SerializeField]
    protected RectTransform m_content;
    [Header("Cell renderer")]
    [SerializeField]
    protected GameObject m_prefab;
    [Header("Configuration")]
    [SerializeField]
    protected bool m_horizontal;
    [SerializeField]
    private bool m_displayEmptyCell;
    [SerializeField]
    protected Vector2Int m_cellSize;
    [SerializeField]
    protected Vector2Int m_margin;
    [Header("Drag and Drop")]
    [SerializeField]
    private bool m_enableDragAndDrop = true;
    [SerializeField]
    private DynamicList.OnDragAction m_onDragAction;
    [SerializeField]
    private DynamicList.OnDropAction m_onDropAction;
    [Header("Animation")]
    [SerializeField]
    protected Ease m_moveEase = Ease.InOutQuart;
    [SerializeField]
    protected float m_moveAnimationDuration = 0.2f;
    [SerializeField]
    protected Ease m_insertionEase = Ease.OutBounce;
    [SerializeField]
    protected float m_insertionAnimationDuration = 0.15f;
    [SerializeField]
    protected Ease m_scrollEase = Ease.InOutCubic;
    [SerializeField]
    protected float m_scrollDuration = 0.15f;
    private ICellRendererConfigurator m_cellRendererConfigurator;
    private CellRendererFilter m_cellRendererFilter;
    private IDragNDropValidator m_dragNDropValidator;
    private System.Type m_itemType;
    private CellRenderer m_cellRenderer;
    private ScrollRect m_scrollRect;
    private int m_viewportWidth;
    private int m_viewportHeight;
    private List<DynamicList.Item> m_allItems;
    private List<object> m_items;
    private List<CellRenderer> m_rendererPool;
    private List<DynamicList.CellParams> m_cellParams;
    private readonly Dictionary<uint, DynamicList.CellParams> m_cellParamsById = new Dictionary<uint, DynamicList.CellParams>();
    private uint m_lastId;
    private int m_previousFirstCellIndex = -1;
    private int m_previousLastCellIndex = -1;
    private int m_rows;
    private int m_columns;
    private float m_horizontalLeeway;
    private float m_verticalLeeway;
    private float m_contentWidth;
    private float m_contentHeight;
    private int m_cellSizeX;
    private int m_cellSizeY;
    private Vector2 m_scrollPercentageVector;
    private float m_scrollPercentage;
    private Rect m_viewportBoundingBox;
    private float m_animationStartTime = -1f;
    private int m_animationStep;
    private float m_removeAnimationDuration;
    private readonly List<int> m_insertedCells = new List<int>();
    private readonly List<int> m_removedCells = new List<int>();
    private bool m_initialized;
    private Vector2 m_lastLayoutDimensions;
    private bool m_viewportChanged;
    private bool m_itemCountChanged;
    private bool m_resetScrollPosition;
    private float m_previousContentWidth;
    private float m_previousContentHeight;
    private float m_targetContentWidth;
    private float m_targetContentHeight;
    private bool m_needReLayout;
    private bool m_isDragging;

    public event Action OnSetValues;

    public event Action<object, int> OnInsertion;

    public event Action<object, int> OnRemoved;

    public event Action<object, object, int> OnValueChanged;

    public event Action<float> OnScrollPercentage;

    public float scrollPercentage
    {
      get => this.m_scrollPercentage;
      private set
      {
        this.m_scrollPercentage = value;
        Action<float> scrollPercentage = this.OnScrollPercentage;
        if (scrollPercentage == null)
          return;
        scrollPercentage(this.m_scrollPercentage);
      }
    }

    public bool enableDragAndDrop
    {
      get => this.m_enableDragAndDrop;
      set => this.m_enableDragAndDrop = value;
    }

    protected void Awake()
    {
      this.CheckInit();
      ItemDragNDropListener.instance.Register((DragNDropClient) this, this.m_itemType);
    }

    private void CheckInit()
    {
      if (this.m_initialized)
        return;
      this.m_cellParams = Ankama.Utilities.ListPool<DynamicList.CellParams>.Get();
      this.m_rendererPool = Ankama.Utilities.ListPool<CellRenderer>.Get();
      this.m_items = Ankama.Utilities.ListPool<object>.Get();
      this.m_allItems = Ankama.Utilities.ListPool<DynamicList.Item>.Get();
      if (this.m_cellSize.x <= 0 || this.m_cellSize.y <= 0)
        this.m_cellSize = new Vector2Int(10, 10);
      this.m_cellSizeX = this.m_cellSize.x + this.m_margin.x;
      this.m_cellSizeY = this.m_cellSize.y + this.m_margin.y;
      this.m_scrollRect = this.GetComponent<ScrollRect>();
      this.m_scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollRectValueChanged));
      this.m_scrollRect.horizontal = false;
      this.m_scrollRect.vertical = false;
      this.OnScrollRectValueChanged(new Vector2(this.m_scrollRect.horizontalNormalizedPosition, this.m_scrollRect.verticalNormalizedPosition));
      this.m_viewportChanged = true;
      this.m_cellRenderer = this.m_prefab.GetComponent<CellRenderer>();
      if ((UnityEngine.Object) this.m_cellRenderer == (UnityEngine.Object) null)
      {
        Debug.LogWarningFormat("No valid ItemElement found in the prefab {0} for list {1}", (object) this.m_prefab.name, (object) this.name);
      }
      else
      {
        this.m_itemType = this.m_cellRenderer.GetValueType();
        foreach (Component componentsInChild in this.gameObject.GetComponentsInChildren<CellRenderer>())
          UnityEngine.Object.Destroy((UnityEngine.Object) componentsInChild.gameObject);
        this.m_initialized = true;
      }
    }

    public void SetCellRendererConfigurator(ICellRendererConfigurator configurator)
    {
      this.CheckInit();
      this.m_cellRendererConfigurator = configurator;
      foreach (CellRenderer cellRenderer in this.m_rendererPool)
        cellRenderer.SetConfigurator(configurator);
      foreach (DynamicList.CellParams cellParam in this.m_cellParams)
      {
        CellRenderer renderer = cellParam.renderer;
        if (!((UnityEngine.Object) renderer == (UnityEngine.Object) null))
          renderer.SetConfigurator(configurator);
      }
    }

    public void UpdateAllConfigurators(bool instant = false)
    {
      this.CheckInit();
      foreach (DynamicList.CellParams cellParam in this.m_cellParams)
      {
        CellRenderer renderer = cellParam.renderer;
        if (!((UnityEngine.Object) renderer == (UnityEngine.Object) null))
          renderer.OnConfiguratorUpdate(instant);
      }
    }

    public void UpdateConfiguratorWithValue(object value, bool instant = false)
    {
      this.CheckInit();
      foreach (DynamicList.CellParams cellParam in this.m_cellParams)
      {
        CellRenderer renderer = cellParam.renderer;
        if (!((UnityEngine.Object) renderer == (UnityEngine.Object) null) && renderer.value == value)
          renderer.OnConfiguratorUpdate(instant);
      }
    }

    public void SetValues<T>(IEnumerable<T> values) where T : class
    {
      this.CheckInit();
      if (!this.m_itemType.IsAssignableFrom(typeof (T)))
      {
        Debug.LogWarningFormat("Wrong value type set in list {0}. Expected : {1}. Got {2}", (object) this.name, (object) this.m_itemType.Name, (object) typeof (T).Name);
      }
      else
      {
        this.FinishAnimation();
        this.m_lastId = 0U;
        this.m_items.Clear();
        this.m_allItems.Clear();
        this.m_cellParams.Clear();
        foreach (KeyValuePair<uint, DynamicList.CellParams> keyValuePair in this.m_cellParamsById)
          this.ReturnToPool(keyValuePair.Value.renderer);
        this.m_cellParamsById.Clear();
        foreach (T obj in values)
        {
          bool filtered = this.m_cellRendererFilter != null && !this.m_cellRendererFilter((object) obj);
          this.m_allItems.Add(new DynamicList.Item((object) obj, filtered));
          if (!filtered)
          {
            this.m_items.Add((object) obj);
            this.m_cellParams.Add(new DynamicList.CellParams(this.m_lastId++, (object) obj, (Vector2) this.m_cellSize));
          }
        }
        this.m_itemCountChanged = true;
        this.m_resetScrollPosition = true;
        this.UpdateAll();
        Action onSetValues = this.OnSetValues;
        if (onSetValues == null)
          return;
        onSetValues();
      }
    }

    public void Insert<T>(int index, T value)
    {
      this.CheckInit();
      if (!this.m_itemType.IsAssignableFrom(typeof (T)))
      {
        Debug.LogWarningFormat("Wrong value type set in list {0}. Expected : {1}. Got {2}", (object) this.name, (object) this.m_itemType.Name, (object) typeof (T).Name);
      }
      else
      {
        if (index < 0 || index > this.m_allItems.Count)
          throw new ArgumentOutOfRangeException();
        this.FinishAnimation();
        bool filtered = this.m_cellRendererFilter != null && !this.m_cellRendererFilter((object) value);
        this.m_allItems.Insert(index, new DynamicList.Item((object) value, filtered));
        if (!filtered)
        {
          int index1 = 0;
          for (int index2 = 0; index2 < index; ++index2)
          {
            if (!this.m_allItems[index2].m_filtered)
              ++index1;
          }
          this.m_items.Insert(index1, (object) value);
          DynamicList.CellParams cellParams = new DynamicList.CellParams(this.m_lastId++, (object) value, (Vector2) this.m_cellSize, DynamicList.CellAnimation.InsertionFromScratch);
          this.m_cellParams.Insert(index1, cellParams);
          this.m_insertedCells.Add(index1);
          this.StartAnimation();
        }
        Action<object, int> onInsertion = this.OnInsertion;
        if (onInsertion == null)
          return;
        onInsertion((object) value, index);
      }
    }

    public void RemoveRange(int index, int count)
    {
      this.CheckInit();
      if (index < 0 || index + count > this.m_allItems.Count)
        throw new ArgumentOutOfRangeException();
      this.FinishAnimation();
      int index1 = 0;
      for (int index2 = 0; index2 < index; ++index2)
      {
        if (!this.m_allItems[index2].m_filtered)
          ++index1;
      }
      for (int index3 = 0; index3 < count; ++index3)
      {
        int index4 = index + index3;
        DynamicList.Item allItem = this.m_allItems[index4];
        if (!allItem.m_filtered)
        {
          this.m_cellParams[index1].removed = true;
          this.m_removedCells.Add(index1);
          ++index1;
        }
        Action<object, int> onRemoved = this.OnRemoved;
        if (onRemoved != null)
          onRemoved(allItem.m_value, index4);
      }
      this.StartAnimation();
    }

    public void RemoveAt(int index)
    {
      this.CheckInit();
      if (index < 0 || index > this.m_allItems.Count)
        throw new ArgumentOutOfRangeException();
      this.FinishAnimation();
      DynamicList.Item allItem1 = this.m_allItems[index];
      if (allItem1.m_filtered)
      {
        this.m_allItems.RemoveAt(index);
      }
      else
      {
        int index1 = 0;
        for (int index2 = 0; index2 < index; ++index2)
        {
          DynamicList.Item allItem2 = this.m_allItems[index2];
          if (!allItem2.m_filtered && allItem2.m_value != allItem1.m_value)
            ++index1;
        }
        this.m_cellParams[index1].removed = true;
        this.m_removedCells.Add(index1);
        this.StartAnimation();
      }
      Action<object, int> onRemoved = this.OnRemoved;
      if (onRemoved == null)
        return;
      onRemoved(allItem1.m_value, index);
    }

    public void SetFilter(CellRendererFilter filter)
    {
      this.m_cellRendererFilter = filter;
      this.UpdateFilter();
    }

    public void UpdateFilter()
    {
      this.FinishAnimation();
      bool flag1 = false;
      int index1 = 0;
      int index2 = 0;
      for (int count = this.m_allItems.Count; index2 < count; ++index2)
      {
        DynamicList.Item allItem = this.m_allItems[index2];
        int num = allItem.m_filtered ? 1 : 0;
        bool flag2 = this.m_cellRendererFilter != null && !this.m_cellRendererFilter(allItem.m_value);
        if (num != (flag2 ? 1 : 0))
        {
          allItem.m_filtered = flag2;
          this.m_allItems[index2] = allItem;
          if (flag2)
          {
            this.m_removedCells.Add(index1);
            this.m_cellParams[index1].filtered = true;
          }
          else
          {
            this.m_items.Insert(index1, allItem.m_value);
            DynamicList.CellParams cellParams = new DynamicList.CellParams(this.m_lastId++, allItem.m_value, (Vector2) this.m_cellSize, DynamicList.CellAnimation.InsertionFromScratch);
            this.m_cellParams.Insert(index1, cellParams);
            this.m_insertedCells.Add(index1 - this.m_removedCells.Count);
          }
          flag1 = true;
        }
        if (num == 0 || !flag2)
          ++index1;
      }
      if (!flag1)
        return;
      this.StartAnimation();
    }

    private void StartAnimation()
    {
      this.m_animationStep = 0;
      this.m_animationStartTime = Time.time;
      this.m_removeAnimationDuration = this.m_removedCells.Count == 0 ? 0.0f : this.m_insertionAnimationDuration;
      this.m_itemCountChanged = true;
      this.ComputeDimensions();
      this.ComputeCellPositions();
    }

    protected void Update() => this.UpdateAll();

    private void UpdateAll()
    {
      this.UpdateAnimation();
      int num = this.IsAnimating() ? 1 : 0;
      if (this.m_viewportChanged && this.m_viewport.rect.size == this.m_lastLayoutDimensions)
        this.m_viewportChanged = false;
      if (num == 0 && (this.m_itemCountChanged || this.m_viewportChanged))
      {
        this.ComputeDimensions();
        this.ComputeCellPositions();
      }
      if (this.m_resetScrollPosition)
      {
        this.m_scrollRect.horizontalNormalizedPosition = 0.0f;
        this.m_scrollRect.verticalNormalizedPosition = 1f;
        this.OnScrollRectValueChanged(new Vector2(0.0f, 1f));
        this.m_resetScrollPosition = false;
      }
      if (num != 0)
      {
        this.AccurateReLayout();
      }
      else
      {
        if (!this.m_needReLayout)
          return;
        this.FullReLayout();
      }
    }

    private bool IsAnimating() => (double) this.m_animationStartTime >= 0.0;

    private void UpdateAnimation(bool finish = false)
    {
      if ((double) this.m_animationStartTime < 0.0)
        return;
      float num = Time.time - this.m_animationStartTime;
      bool flag1 = false;
      if (this.m_animationStep == 0)
      {
        int index = 0;
        for (int count = this.m_removedCells.Count; index < count; ++index)
          this.m_cellParams[this.m_removedCells[index]].StartRemoval();
        ++this.m_animationStep;
      }
      if (this.m_animationStep == 1)
      {
        if (this.m_removedCells.Count == 0)
        {
          ++this.m_animationStep;
          flag1 = true;
        }
        else
        {
          float time = num;
          bool flag2 = false;
          if ((double) time >= (double) this.m_insertionAnimationDuration | finish)
          {
            flag2 = true;
            ++this.m_animationStep;
            flag1 = true;
          }
          if (flag2)
          {
            this.FinishRemoveAnimation();
          }
          else
          {
            float percentage = EaseManager.Evaluate(this.m_insertionEase, (EaseFunction) null, time, this.m_insertionAnimationDuration, 0.0f, 0.0f);
            int index = 0;
            for (int count = this.m_removedCells.Count; index < count; ++index)
              this.m_cellParams[this.m_removedCells[index]].EvaluateScale(percentage);
          }
        }
      }
      if (this.m_animationStep == 2)
      {
        if (flag1)
          this.ComputeDimensions();
        float time = num - this.m_removeAnimationDuration;
        bool flag3 = false;
        if ((double) time >= (double) this.m_moveAnimationDuration | finish)
        {
          ++this.m_animationStep;
          flag3 = true;
        }
        float percentage = EaseManager.Evaluate(this.m_moveEase, (EaseFunction) null, time, this.m_moveAnimationDuration, 0.0f, 0.0f);
        int index1 = 0;
        for (int count = this.m_cellParams.Count; index1 < count; ++index1)
        {
          if (flag3)
            this.m_cellParams[index1].EndMove();
          else
            this.m_cellParams[index1].EvaluateMove(percentage);
        }
        if (flag3)
        {
          int index2 = 0;
          for (int count = this.m_insertedCells.Count; index2 < count; ++index2)
            this.m_cellParams[this.m_insertedCells[index2]].StartInsertion();
          this.m_contentWidth = this.m_targetContentWidth;
          this.m_contentHeight = this.m_targetContentHeight;
        }
        else
        {
          this.m_contentWidth = this.m_previousContentWidth + (this.m_targetContentWidth - this.m_previousContentWidth) * percentage;
          this.m_contentHeight = this.m_previousContentHeight + (this.m_targetContentHeight - this.m_previousContentHeight) * percentage;
        }
        this.UpdateContentSize();
      }
      if (this.m_animationStep == 3)
      {
        if (this.m_insertedCells.Count == 0)
        {
          ++this.m_animationStep;
        }
        else
        {
          float time = num - this.m_moveAnimationDuration - this.m_removeAnimationDuration;
          bool flag4 = false;
          if ((double) time >= (double) this.m_insertionAnimationDuration | finish)
          {
            flag4 = true;
            ++this.m_animationStep;
          }
          if (flag4)
          {
            this.FinishInsertAnimation();
          }
          else
          {
            float percentage = EaseManager.Evaluate(this.m_insertionEase, (EaseFunction) null, time, this.m_insertionAnimationDuration, 0.0f, 0.0f);
            int index = 0;
            for (int count = this.m_insertedCells.Count; index < count; ++index)
              this.m_cellParams[this.m_insertedCells[index]].EvaluateScale(percentage);
          }
        }
      }
      if (this.m_animationStep != 4)
        return;
      this.m_animationStartTime = -1f;
      this.m_animationStep = 0;
    }

    private void FinishAnimation()
    {
      this.UpdateAnimation(true);
      this.AccurateReLayout();
    }

    private void FinishInsertAnimation()
    {
      int index = 0;
      for (int count = this.m_insertedCells.Count; index < count; ++index)
      {
        DynamicList.CellParams cellParam = this.m_cellParams[this.m_insertedCells[index]];
        cellParam.cellAnimation = DynamicList.CellAnimation.None;
        cellParam.EvaluateScale(1f);
      }
      this.m_insertedCells.Clear();
    }

    private void FinishRemoveAnimation()
    {
      for (int index = this.m_removedCells.Count - 1; index >= 0; --index)
      {
        int removedCell = this.m_removedCells[index];
        DynamicList.CellParams cellParam = this.m_cellParams[removedCell];
        int itemIndex = cellParam.itemIndex;
        this.m_cellParamsById.Remove(cellParam.id);
        this.ReturnToPool(cellParam.renderer);
        this.m_cellParams.RemoveAt(removedCell);
        this.m_items.RemoveAt(removedCell);
        if (cellParam.removed)
          this.m_allItems.RemoveAt(itemIndex);
      }
      this.m_removedCells.Clear();
    }

    private void OnRectTransformDimensionsChange() => this.m_viewportChanged = true;

    private void CheckEmptyCellParams()
    {
      if (!this.m_displayEmptyCell)
        return;
      int num = (!this.m_horizontal ? Mathf.CeilToInt((float) this.m_items.Count / (float) this.m_columns) * this.m_columns : Mathf.CeilToInt((float) this.m_items.Count / (float) this.m_rows) * this.m_rows) + this.m_removedCells.Count;
      for (int count = this.m_cellParams.Count; count < num; ++count)
        this.m_cellParams.Add(new DynamicList.CellParams(this.m_lastId++, (object) null, (Vector2) this.m_cellSize));
      for (int index = this.m_cellParams.Count - 1; index >= num; --index)
      {
        DynamicList.CellParams cellParam = this.m_cellParams[index];
        this.ReturnToPool(cellParam.renderer);
        cellParam.renderer = (CellRenderer) null;
        this.m_cellParams.RemoveAt(index);
        this.m_cellParamsById.Remove(cellParam.id);
      }
    }

    private void ReturnToPool(CellRenderer cellRenderer)
    {
      if ((UnityEngine.Object) cellRenderer == (UnityEngine.Object) null)
        return;
      cellRenderer.SetValue((object) null);
      this.m_rendererPool.Add(cellRenderer);
      cellRenderer.gameObject.SetActive(false);
    }

    private CellRenderer GetFromPool(DynamicList.CellParams cellParams)
    {
      bool flag = cellParams.cellAnimation != DynamicList.CellAnimation.InsertionFromScratch;
      if (this.m_rendererPool.Count == 0)
      {
        CellRenderer cellRenderer = UnityEngine.Object.Instantiate<CellRenderer>(this.m_cellRenderer);
        this.InitCellRenderer(cellRenderer, false);
        cellRenderer.gameObject.SetActive(flag);
        return cellRenderer;
      }
      CellRenderer fromPool = this.m_rendererPool[this.m_rendererPool.Count - 1];
      this.m_rendererPool.RemoveAt(this.m_rendererPool.Count - 1);
      fromPool.gameObject.SetActive(flag);
      return fromPool;
    }

    protected virtual void InitCellRenderer(CellRenderer cellRenderer, bool andUpdate)
    {
      RectTransform rectTransform = cellRenderer.rectTransform;
      rectTransform.SetParent((Transform) this.m_content, true);
      Vector3 localPosition = rectTransform.localPosition;
      rectTransform.sizeDelta = (Vector2) this.m_cellSize;
      rectTransform.anchorMin = Vector2.zero;
      rectTransform.anchorMax = Vector2.zero;
      rectTransform.pivot = new Vector2(0.5f, 0.5f);
      rectTransform.localPosition = localPosition;
      cellRenderer.dragNDropClient = (DragNDropClient) this;
      cellRenderer.SetConfigurator(this.m_cellRendererConfigurator, andUpdate);
    }

    private int m_scrollInPixels
    {
      get => this.m_horizontal ? Mathf.RoundToInt(Mathf.Clamp01(this.m_scrollRect.horizontalNormalizedPosition) * this.m_horizontalLeeway) : Mathf.RoundToInt((1f - Mathf.Clamp01(this.m_scrollRect.verticalNormalizedPosition)) * this.m_verticalLeeway);
      set
      {
        if (this.m_horizontal)
        {
          if (Mathf.Approximately(this.m_horizontalLeeway, 0.0f))
            this.m_scrollRect.horizontalNormalizedPosition = 0.0f;
          else
            this.m_scrollRect.horizontalNormalizedPosition = (float) value / this.m_horizontalLeeway;
        }
        else if (Mathf.Approximately(this.m_verticalLeeway, 0.0f))
          this.m_scrollRect.verticalNormalizedPosition = 1f;
        else
          this.m_scrollRect.verticalNormalizedPosition = (float) (1.0 - (double) value / (double) this.m_verticalLeeway);
      }
    }

    private float m_contentLength => !this.m_horizontal ? this.m_contentHeight : this.m_contentWidth;

    private float m_viewportLength => this.m_horizontal ? (float) this.m_viewportWidth : (float) this.m_viewportHeight;

    public IEnumerator ScrollToPage(int index, bool instant, TweenCallback onUpdate = null)
    {
      DynamicList dynamicList = this;
      dynamicList.CheckInit();
      int pagesCount = dynamicList.pagesCount;
      if (index >= 0 && index < pagesCount)
      {
        int num = Mathf.RoundToInt(dynamicList.m_viewportLength * (float) index);
        if (instant)
        {
          dynamicList.m_scrollInPixels = num;
          TweenCallback tweenCallback = onUpdate;
          if (tweenCallback != null)
            tweenCallback();
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          Tweener t = DOTween.To(new DOGetter<int>(dynamicList.\u003CScrollToPage\u003Eb__107_0), new DOSetter<int>(dynamicList.\u003CScrollToPage\u003Eb__107_1), Mathf.Clamp(num, 0, Mathf.RoundToInt(dynamicList.m_contentLength - dynamicList.m_viewportLength)), dynamicList.m_scrollDuration).SetEase<Tweener>(dynamicList.m_scrollEase);
          if (onUpdate != null)
            t = t.OnUpdate<Tweener>(onUpdate).OnKill<Tweener>(onUpdate);
          yield return (object) t.WaitForKill();
        }
      }
    }

    public bool HasPreviousPage()
    {
      this.CheckInit();
      return this.currentPageIndex > 0;
    }

    public bool HastNextPage()
    {
      this.CheckInit();
      return this.currentPageIndex < this.pagesCount - 1;
    }

    public int currentPageIndex
    {
      get
      {
        this.CheckInit();
        float f = (double) this.m_viewportLength <= 0.0 ? -1f : (float) this.m_scrollInPixels / this.m_viewportLength;
        return (double) f > 1E-06 ? Mathf.CeilToInt(f) : 0;
      }
    }

    public int pagesCount => Mathf.Max(0, Mathf.CeilToInt(this.m_contentLength / this.m_viewportLength));

    private void OnScrollRectValueChanged(Vector2 position)
    {
      this.m_scrollPercentageVector.Set(position.x, 1f - position.y);
      this.scrollPercentage = this.m_horizontal ? this.m_scrollPercentageVector.x : this.m_scrollPercentageVector.y;
      if (!this.m_initialized)
        return;
      this.ComputeViewportBoundingBox();
      if (this.IsAnimating() || this.m_needReLayout)
        return;
      this.FastReLayout();
    }

    private void ComputeDimensions()
    {
      this.CheckInit();
      if (this.m_viewportChanged)
      {
        this.m_viewportWidth = Math.Max(1, (int) this.m_viewport.rect.width);
        this.m_viewportHeight = Math.Max(1, (int) this.m_viewport.rect.height);
        if (this.m_horizontal)
        {
          this.m_rows = Math.Max(1, this.m_viewportHeight / this.m_cellSizeY);
          this.m_columns = Math.Max(1, (int) Math.Ceiling((double) this.m_viewportWidth / (double) this.m_cellSizeX)) + 1;
          this.m_contentHeight = (float) Math.Max(0, this.m_rows * this.m_cellSizeY);
        }
        else
        {
          this.m_columns = Math.Max(1, this.m_viewportWidth / this.m_cellSizeX);
          this.m_rows = Math.Max(1, (int) Math.Ceiling((double) this.m_viewportHeight / (double) this.m_cellSizeY)) + 1;
          this.m_contentWidth = (float) Math.Max(0, this.m_columns * this.m_cellSizeX);
        }
      }
      bool flag = this.IsAnimating();
      if (this.m_viewportChanged || this.m_itemCountChanged)
      {
        float num1 = (float) (this.m_items.Count - this.m_removedCells.Count);
        if (flag)
        {
          Rect rect = this.m_content.rect;
          this.m_previousContentWidth = rect.width;
          this.m_previousContentHeight = rect.height;
          this.m_targetContentWidth = this.m_contentWidth;
          this.m_targetContentHeight = this.m_contentHeight;
        }
        if (this.m_horizontal)
        {
          float num2 = Math.Max(0.0f, Mathf.Ceil(num1 / (float) this.m_rows) * (float) this.m_cellSizeX);
          if (flag)
            this.m_targetContentWidth = num2;
          else
            this.m_contentWidth = num2;
        }
        else
        {
          float num3 = Math.Max(0.0f, Mathf.Ceil(num1 / (float) this.m_columns) * (float) this.m_cellSizeY);
          if (flag)
            this.m_targetContentHeight = num3;
          else
            this.m_contentHeight = num3;
        }
        this.UpdateContentSize();
      }
      if (!flag)
        this.m_needReLayout = true;
      this.m_viewportChanged = false;
      this.m_itemCountChanged = false;
    }

    private void UpdateContentSize()
    {
      int mScrollInPixels = this.m_scrollInPixels;
      this.m_horizontalLeeway = Mathf.Max(0.0f, this.m_contentWidth - (float) this.m_viewportWidth);
      this.m_verticalLeeway = Mathf.Max(0.0f, this.m_contentHeight - (float) this.m_viewportHeight);
      this.m_content.sizeDelta = new Vector2(this.m_contentWidth, this.m_contentHeight);
      this.m_scrollInPixels = mScrollInPixels;
      this.ComputeViewportBoundingBox();
      this.CheckEmptyCellParams();
    }

    private void ComputeViewportBoundingBox()
    {
      if (this.m_horizontal)
        this.m_viewportBoundingBox = new Rect(this.m_horizontalLeeway * this.m_scrollPercentageVector.x, this.m_verticalLeeway / 2f, (float) (this.m_viewportWidth + this.m_cellSizeX), (float) this.m_viewportHeight);
      else
        this.m_viewportBoundingBox = new Rect(this.m_horizontalLeeway / 2f, this.m_verticalLeeway * (1f - this.m_scrollPercentageVector.y), (float) this.m_viewportWidth, (float) (this.m_viewportHeight + this.m_cellSizeY));
    }

    private void ComputeCellPositions()
    {
      bool flag = this.IsAnimating();
      int num1 = 0;
      int index1 = 0;
      for (int count = this.m_allItems.Count; index1 < count; ++index1)
      {
        if (!this.m_allItems[index1].m_filtered)
          this.m_cellParams[num1++].itemIndex = index1;
      }
      float num2 = flag ? this.m_targetContentHeight : this.m_contentHeight;
      int num3 = 0;
      int num4 = 0;
      int num5 = 0;
      int index2 = 0;
      for (int count = this.m_cellParams.Count; index2 < count; ++index2)
      {
        DynamicList.CellParams cellParam = this.m_cellParams[index2];
        if (!cellParam.removed && !cellParam.filtered)
        {
          cellParam.index = num5++;
          float x = (float) this.m_cellSizeX * ((float) num3 + 0.5f);
          float y = (float) (int) num2 - (float) this.m_cellSizeY * ((float) num4 + 0.5f);
          if (flag)
            cellParam.StartMove(new Vector2(x, y));
          else
            cellParam.position = new Vector2(x, y);
          if (this.m_horizontal)
          {
            ++num4;
            if (num4 >= this.m_rows)
            {
              num4 = 0;
              ++num3;
            }
          }
          else
          {
            ++num3;
            if (num3 >= this.m_columns)
            {
              num3 = 0;
              ++num4;
            }
          }
        }
      }
    }

    public void AccurateReLayout()
    {
      int index = 0;
      for (int count = this.m_cellParams.Count; index < count; ++index)
      {
        DynamicList.CellParams cellParam = this.m_cellParams[index];
        if (this.m_viewportBoundingBox.Overlaps(cellParam.actualRect))
        {
          if (!this.m_cellParamsById.TryGetValue(cellParam.id, out DynamicList.CellParams _))
          {
            cellParam.renderer = this.GetFromPool(cellParam);
            this.m_cellParamsById[cellParam.id] = cellParam;
          }
        }
        else
        {
          DynamicList.CellParams cellParams;
          if (this.m_cellParamsById.TryGetValue(cellParam.id, out cellParams))
          {
            this.ReturnToPool(cellParams.renderer);
            cellParams.renderer = (CellRenderer) null;
            this.m_cellParamsById.Remove(cellParams.id);
          }
        }
      }
    }

    private void FullReLayout()
    {
      int index1 = this.ToIndex(this.m_scrollPercentage);
      int num1 = Mathf.Clamp(index1, 0, this.m_cellParams.Count);
      int num2 = index1 + this.m_rows * this.m_columns - 1;
      foreach (KeyValuePair<uint, DynamicList.CellParams> keyValuePair in this.m_cellParamsById)
      {
        this.ReturnToPool(keyValuePair.Value.renderer);
        keyValuePair.Value.renderer = (CellRenderer) null;
      }
      this.m_cellParamsById.Clear();
      for (int index2 = num1; index2 <= num2 && index2 < this.m_cellParams.Count; ++index2)
      {
        DynamicList.CellParams cellParam = this.m_cellParams[index2];
        cellParam.renderer = this.GetFromPool(cellParam);
        this.m_cellParamsById[cellParam.id] = cellParam;
      }
      this.m_previousFirstCellIndex = num1;
      this.m_previousLastCellIndex = num2;
      this.m_lastLayoutDimensions = this.m_viewport.rect.size;
      this.m_needReLayout = false;
    }

    private void FastReLayout()
    {
      int index1 = this.ToIndex(this.m_scrollPercentage);
      int val1_1 = Mathf.Clamp(index1, 0, this.m_cellParams.Count);
      int val1_2 = index1 + this.m_rows * this.m_columns - 1;
      for (int previousFirstCellIndex = this.m_previousFirstCellIndex; previousFirstCellIndex < Math.Min(val1_1, this.m_previousLastCellIndex + 1); ++previousFirstCellIndex)
      {
        if (previousFirstCellIndex >= 0 && this.m_cellParams.Count > previousFirstCellIndex)
        {
          DynamicList.CellParams cellParam = this.m_cellParams[previousFirstCellIndex];
          this.ReturnToPool(cellParam.renderer);
          cellParam.renderer = (CellRenderer) null;
          this.m_cellParamsById.Remove(cellParam.id);
        }
      }
      for (int previousLastCellIndex = this.m_previousLastCellIndex; previousLastCellIndex > Math.Max(val1_2, this.m_previousFirstCellIndex - 1); --previousLastCellIndex)
      {
        if (previousLastCellIndex >= 0 && this.m_cellParams.Count > previousLastCellIndex)
        {
          DynamicList.CellParams cellParam = this.m_cellParams[previousLastCellIndex];
          this.ReturnToPool(cellParam.renderer);
          cellParam.renderer = (CellRenderer) null;
          this.m_cellParamsById.Remove(cellParam.id);
        }
      }
      for (int index2 = val1_1; index2 < Math.Min(this.m_previousFirstCellIndex, val1_2 + 1); ++index2)
      {
        if (index2 < this.m_cellParams.Count)
        {
          DynamicList.CellParams cellParam = this.m_cellParams[index2];
          if (!this.m_cellParamsById.ContainsKey(cellParam.id))
          {
            cellParam.renderer = this.GetFromPool(cellParam);
            this.m_cellParamsById[cellParam.id] = cellParam;
          }
        }
      }
      for (int index3 = Math.Max(val1_1, this.m_previousLastCellIndex + 1); index3 <= val1_2; ++index3)
      {
        if (index3 < this.m_cellParams.Count)
        {
          DynamicList.CellParams cellParam = this.m_cellParams[index3];
          if (!this.m_cellParamsById.ContainsKey(cellParam.id))
          {
            cellParam.renderer = this.GetFromPool(cellParam);
            this.m_cellParamsById[cellParam.id] = cellParam;
          }
        }
      }
      this.m_previousFirstCellIndex = val1_1;
      this.m_previousLastCellIndex = val1_2;
    }

    private int ToIndex(float percentage)
    {
      float num = Mathf.Clamp01(percentage);
      return !this.m_horizontal ? (int) Math.Floor((double) this.m_verticalLeeway * (double) num / (double) this.m_cellSizeY) * this.m_columns : (int) Math.Floor((double) this.m_horizontalLeeway * (double) num / (double) this.m_cellSizeX) * this.m_rows;
    }

    public RectTransform rectTransform => this.m_viewport;

    public bool activeInHierarchy => this.gameObject.activeInHierarchy;

    public void SetDragAndDropValidator(IDragNDropValidator validator) => this.m_dragNDropValidator = validator;

    public void OnDragOver(CellRenderer cellRenderer, PointerEventData evt)
    {
    }

    public bool OnDropOut(CellRenderer cellRenderer, PointerEventData evt)
    {
      int num = this.m_enableDragAndDrop ? 1 : 0;
      return true;
    }

    public bool OnDrop(CellRenderer cellRenderer, PointerEventData evt)
    {
      if (!this.m_enableDragAndDrop || this.m_onDropAction == DynamicList.OnDropAction.None || !RectTransformUtility.RectangleContainsScreenPoint(this.m_viewport, evt.position, evt.pressEventCamera))
        return true;
      int index1 = -1;
      int index2 = -1;
      foreach (KeyValuePair<uint, DynamicList.CellParams> keyValuePair in this.m_cellParamsById)
      {
        if (RectTransformUtility.RectangleContainsScreenPoint(keyValuePair.Value.renderer.rectTransform, evt.position, evt.pressEventCamera))
        {
          index1 = keyValuePair.Value.index;
          index2 = keyValuePair.Value.itemIndex;
          break;
        }
      }
      if (index1 == -1 || this.m_dragNDropValidator != null && !this.m_dragNDropValidator.IsValidDrop(cellRenderer.value))
        return true;
      this.FinishAnimation();
      this.InitCellRenderer(cellRenderer, true);
      cellRenderer.rectTransform.SetAsLastSibling();
      DynamicList.CellParams cellParams = new DynamicList.CellParams(this.m_lastId++, cellRenderer, (Vector2) this.m_cellSize, DynamicList.CellAnimation.InsertionFromExternal);
      bool filtered = this.m_cellRendererFilter != null && !this.m_cellRendererFilter(cellRenderer.value);
      switch (this.m_onDropAction)
      {
        case DynamicList.OnDropAction.None:
          return true;
        case DynamicList.OnDropAction.InsertAt:
          this.m_allItems.Insert(index2, new DynamicList.Item(cellRenderer.value, filtered));
          if (!filtered)
          {
            this.m_items.Insert(index1, cellRenderer.value);
            this.m_cellParams.Insert(index1, cellParams);
            this.m_cellParamsById.Add(cellParams.id, cellParams);
            this.m_insertedCells.Add(index1);
            this.StartAnimation();
          }
          Action<object, int> onInsertion = this.OnInsertion;
          if (onInsertion != null)
            onInsertion(cellRenderer.value, index1);
          return false;
        case DynamicList.OnDropAction.Replace:
          this.m_cellParams[index1].removed = true;
          this.m_removedCells.Add(index1);
          this.m_allItems.Insert(index2 + 1, new DynamicList.Item(cellRenderer.value, filtered));
          if (!filtered)
          {
            this.m_items.Insert(index1 + 1, cellRenderer.value);
            this.m_cellParams.Insert(index1 + 1, cellParams);
            this.m_cellParamsById.Add(cellParams.id, cellParams);
            this.m_insertedCells.Add(index1);
          }
          Action<object, object, int> onValueChanged = this.OnValueChanged;
          if (onValueChanged != null)
            onValueChanged(this.m_items[index1], cellRenderer.value, index1);
          this.StartAnimation();
          return false;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void OnBeginDrag(PointerEventData evt)
    {
      if (!this.m_enableDragAndDrop || !RectTransformUtility.RectangleContainsScreenPoint(this.m_viewport, evt.position, evt.pressEventCamera))
        return;
      int index = -1;
      foreach (KeyValuePair<uint, DynamicList.CellParams> keyValuePair in this.m_cellParamsById)
      {
        if (RectTransformUtility.RectangleContainsScreenPoint(keyValuePair.Value.renderer.rectTransform, evt.position, evt.pressEventCamera))
        {
          index = keyValuePair.Value.index;
          break;
        }
      }
      if (index == -1)
        return;
      DynamicList.CellParams cellParam = this.m_cellParams[index];
      if ((UnityEngine.Object) cellParam.renderer == (UnityEngine.Object) null || this.m_dragNDropValidator != null && !this.m_dragNDropValidator.IsValidDrag(cellParam.value))
        return;
      ItemDragNDropListener.instance.OnBeginDrag(evt, cellParam.renderer);
      this.m_isDragging = true;
      switch (this.m_onDragAction)
      {
        case DynamicList.OnDragAction.None:
          break;
        case DynamicList.OnDragAction.Remove:
          this.RemoveAt(index);
          break;
        case DynamicList.OnDragAction.SetToNull:
          object obj = this.m_items[index];
          bool filtered = this.m_cellRendererFilter != null && !this.m_cellRendererFilter((object) null);
          this.m_allItems[index] = new DynamicList.Item((object) null, filtered);
          this.m_items[index] = (object) null;
          this.m_cellParams[index].value = (object) null;
          this.m_cellParams[index].renderer.SetValue((object) null);
          Action<object, object, int> onValueChanged = this.OnValueChanged;
          if (onValueChanged == null)
            break;
          onValueChanged(obj, (object) null, index);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public List<DynamicList.CellParams> GetAllCells() => this.m_cellParams;

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.m_enableDragAndDrop)
        return;
      ItemDragNDropListener.instance.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (!this.m_enableDragAndDrop)
        return;
      this.m_isDragging = false;
      ItemDragNDropListener.instance.OnEndDrag(eventData);
    }

    private void OnDisable()
    {
      if (!this.m_isDragging)
        return;
      ItemDragNDropListener.instance.CancelAll();
    }

    private void OnDestroy()
    {
      if (this.m_isDragging)
        ItemDragNDropListener.instance.CancelAll();
      ItemDragNDropListener.instance.UnRegister((DragNDropClient) this, this.m_itemType);
      Ankama.Utilities.ListPool<object>.Release(this.m_items);
      Ankama.Utilities.ListPool<DynamicList.Item>.Release(this.m_allItems);
      Ankama.Utilities.ListPool<DynamicList.CellParams>.Release(this.m_cellParams);
      Ankama.Utilities.ListPool<CellRenderer>.Release(this.m_rendererPool);
    }

    public enum CellAnimation
    {
      None,
      InsertionFromScratch,
      InsertionFromExternal,
    }

    public class CellParams
    {
      public uint id;
      public object value;
      public int index = -1;
      public int itemIndex = -1;
      public DynamicList.CellAnimation cellAnimation;
      private readonly TweenableVector2 m_position = new TweenableVector2();
      private readonly TweenableVector2 m_size = new TweenableVector2();
      private readonly TweenableFloat m_scale = new TweenableFloat();
      private Vector2 m_pivot;
      private RectTransform m_rectTransform;
      private CellRenderer m_renderer;
      public bool removed;
      public bool filtered;
      public Rect actualRect;
      private bool m_moveAnimated;

      public CellParams(
        uint id,
        [NotNull] CellRenderer cellRenderer,
        Vector2 size,
        DynamicList.CellAnimation anim = DynamicList.CellAnimation.None)
      {
        this.id = id;
        this.cellAnimation = anim;
        this.m_renderer = cellRenderer;
        this.m_renderer.name = string.Format("cell {0}", (object) id);
        this.m_rectTransform = this.m_renderer.rectTransform;
        this.m_position.SetValue(this.m_rectTransform.anchoredPosition);
        this.m_size.SetValue(size);
        this.m_scale.SetValue(this.m_rectTransform.localScale.x);
        this.value = cellRenderer.value;
        this.ComputeActualRect();
      }

      public CellParams(uint id, object value, Vector2 size, DynamicList.CellAnimation anim = DynamicList.CellAnimation.None)
      {
        this.id = id;
        this.cellAnimation = anim;
        this.m_size.SetValue(size);
        this.m_scale.SetValue(1f);
        this.value = value;
      }

      private void ComputeActualRect()
      {
        Vector2 currentValue1 = this.m_position.currentValue;
        Vector2 currentValue2 = this.m_size.currentValue;
        this.actualRect.Set(currentValue1.x - currentValue2.x * 0.5f, currentValue1.y - currentValue2.y * 0.5f, currentValue2.x, currentValue2.y);
      }

      public Vector2 position
      {
        set
        {
          if (this.m_position.value == value)
            return;
          this.m_position.SetValue(value);
          this.ComputeActualRect();
          if (this.m_moveAnimated || !(bool) (UnityEngine.Object) this.m_rectTransform)
            return;
          this.m_rectTransform.anchoredPosition3D = (Vector3) value;
        }
      }

      public CellRenderer renderer
      {
        get => this.m_renderer;
        set
        {
          if ((UnityEngine.Object) this.m_renderer == (UnityEngine.Object) value)
            return;
          this.m_renderer = value;
          if ((UnityEngine.Object) this.m_renderer == (UnityEngine.Object) null)
          {
            this.m_rectTransform = (RectTransform) null;
          }
          else
          {
            this.m_renderer.name = string.Format("cell {0}", (object) this.id);
            this.m_renderer.SetValue(this.value);
            this.m_rectTransform = this.m_renderer.GetComponent<RectTransform>();
            this.m_rectTransform.anchoredPosition3D = (Vector3) this.m_position.currentValue;
            float currentValue = this.m_scale.currentValue;
            this.m_rectTransform.localScale = new Vector3(currentValue, currentValue, 1f);
          }
        }
      }

      public void StartInsertion()
      {
        if (this.cellAnimation == DynamicList.CellAnimation.InsertionFromExternal)
        {
          if ((bool) (UnityEngine.Object) this.m_rectTransform)
            this.m_scale.SetTweenValues(this.m_rectTransform.localScale.x, 1f);
          else
            this.m_scale.SetTweenValues(1f, 1f);
        }
        else
        {
          if ((bool) (UnityEngine.Object) this.m_renderer)
            this.m_renderer.gameObject.SetActive(true);
          this.m_scale.SetTweenValues(0.0f, 1f);
        }
        this.EvaluateScale(0.0f);
      }

      public void StartRemoval()
      {
        this.m_scale.SetTweenValues(1f, 0.0f);
        this.EvaluateScale(0.0f);
      }

      public void EvaluateScale(float percentage)
      {
        this.m_scale.Evaluate(percentage);
        if (!(bool) (UnityEngine.Object) this.m_rectTransform)
          return;
        this.m_rectTransform.localScale = new Vector3(this.m_scale.currentValue, this.m_scale.currentValue, 1f);
      }

      public void StartMove(Vector2 destination)
      {
        this.m_moveAnimated = true;
        this.m_position.SetTweenValues(this.m_position.init ? (!((UnityEngine.Object) this.m_rectTransform == (UnityEngine.Object) null) ? this.m_rectTransform.anchoredPosition : this.m_position.value) : destination, destination);
        this.EvaluateMove(0.0f);
      }

      public void EvaluateMove(float percentage)
      {
        this.m_position.Evaluate(percentage);
        this.ComputeActualRect();
        if (!(bool) (UnityEngine.Object) this.m_rectTransform)
          return;
        this.m_rectTransform.anchoredPosition3D = (Vector3) this.m_position.currentValue;
      }

      public void EndMove()
      {
        this.m_position.Evaluate(1f);
        this.ComputeActualRect();
        this.m_moveAnimated = false;
        if (!(bool) (UnityEngine.Object) this.m_rectTransform)
          return;
        this.m_rectTransform.anchoredPosition = this.m_position.currentValue;
      }
    }

    public enum OnDragAction
    {
      None,
      Remove,
      SetToNull,
    }

    public enum OnDropAction
    {
      None,
      InsertAt,
      Replace,
    }

    private struct Item
    {
      internal object m_value;
      internal bool m_filtered;

      public Item(object value, bool filtered)
      {
        this.m_value = value;
        this.m_filtered = filtered;
      }
    }
  }
}
