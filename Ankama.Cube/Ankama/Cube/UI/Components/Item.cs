// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.Item
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (RectTransform))]
  public class Item : 
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
    [SerializeField]
    protected RectTransform m_content;
    [Header("Drag and drop")]
    [SerializeField]
    private bool m_enableDragAndDrop = true;
    [SerializeField]
    private bool m_removeOnDrag = true;
    [SerializeField]
    private bool m_insertOnDrop = true;
    [SerializeField]
    private Ease m_insertTweenEase = Ease.InOutQuad;
    [SerializeField]
    private float m_insertTweenDuration = 0.2f;
    private System.Type m_itemType;
    private CellRenderer m_cellRenderer;
    private object m_value;
    private CellRenderer m_cellRendererInstance;
    [NonSerialized]
    private bool m_initialized;
    private RectTransform m_rectTransform;
    private ICellRendererConfigurator m_cellRendererConfigurator;
    private IDragNDropValidator m_dragNDropValidator;
    private bool m_isDragging;

    public event Action<object, object> OnValueChange;

    public bool enableDragAndDrop
    {
      get => this.m_enableDragAndDrop;
      set => this.m_enableDragAndDrop = value;
    }

    private void Awake()
    {
      this.CheckInit();
      ItemDragNDropListener.instance.Register((DragNDropClient) this, this.m_itemType);
    }

    protected virtual void CheckInit()
    {
      if (this.m_initialized)
        return;
      this.m_rectTransform = this.GetComponent<RectTransform>();
      this.m_cellRenderer = this.m_prefab.GetComponent<CellRenderer>();
      if ((UnityEngine.Object) this.m_cellRenderer == (UnityEngine.Object) null)
      {
        Debug.LogWarningFormat("No valid CellRenderer found in the prefab {0} for list {1}", (object) this.m_prefab.name, (object) this.name);
      }
      else
      {
        this.m_itemType = this.m_cellRenderer.GetValueType();
        for (int index = this.m_content.transform.childCount - 1; index >= 0; --index)
        {
          CellRenderer component = this.m_content.transform.GetChild(index).GetComponent<CellRenderer>();
          if ((bool) (UnityEngine.Object) component)
            UnityEngine.Object.Destroy((UnityEngine.Object) component.gameObject);
        }
        this.m_cellRendererInstance = this.InitializeRenderer();
        this.m_initialized = true;
      }
    }

    public void SetValue<T>(T value)
    {
      this.CheckInit();
      if (!this.m_itemType.IsAssignableFrom(typeof (T)))
        Debug.LogWarningFormat("Wrong value type set in list {0}. Expected : {1}. Got {2}", (object) this.name, (object) this.m_itemType.Name, (object) typeof (T).Name);
      else
        this.SetValue((object) value);
    }

    private void SetValue(object value)
    {
      this.CheckInit();
      object obj = this.m_value;
      this.m_value = value;
      this.m_cellRendererInstance.SetValue(value);
      Action<object, object> onValueChange = this.OnValueChange;
      if (onValueChange == null)
        return;
      onValueChange(obj, value);
    }

    public void UpdateConfigurator(bool instant = false)
    {
      if (!(bool) (UnityEngine.Object) this.m_cellRendererInstance)
        return;
      this.m_cellRendererInstance.OnConfiguratorUpdate(instant);
    }

    private CellRenderer InitializeRenderer()
    {
      CellRenderer cellRenderer = UnityEngine.Object.Instantiate<CellRenderer>(this.m_cellRenderer);
      this.ConfigureCellRenderer(cellRenderer, true, false);
      return cellRenderer;
    }

    private void ConfigureCellRenderer(CellRenderer cellRenderer, bool instant, bool andUpdate)
    {
      RectTransform component = cellRenderer.GetComponent<RectTransform>();
      component.SetParent((Transform) this.m_content, true);
      if (instant)
      {
        component.anchorMin = Vector2.zero;
        component.anchorMax = Vector2.one;
        component.pivot = new Vector2(0.5f, 0.5f);
        component.sizeDelta = Vector2.zero;
        component.anchoredPosition3D = Vector3.zero;
        component.localScale = Vector3.one;
      }
      else
      {
        Vector3 localPosition = component.localPosition;
        component.localPosition = new Vector3(localPosition.x, localPosition.y);
        component.DOAnchorMin(Vector2.zero, this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
        component.DOAnchorMax(Vector2.one, this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
        component.DOPivot(new Vector2(0.5f, 0.5f), this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
        component.DOSizeDelta(Vector2.zero, this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
        component.DOAnchorPos3D(Vector3.zero, this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
        component.DOScale(1f, this.m_insertTweenDuration).SetEase<Tweener>(this.m_insertTweenEase);
      }
      cellRenderer.dragNDropClient = (DragNDropClient) this;
      cellRenderer.SetConfigurator(this.m_cellRendererConfigurator, andUpdate);
    }

    public RectTransform rectTransform => this.m_rectTransform;

    public bool activeInHierarchy => this.gameObject.activeInHierarchy;

    public void SetCellRendererConfigurator(ICellRendererConfigurator configurator)
    {
      this.m_cellRendererConfigurator = configurator;
      CellRenderer rendererInstance = this.m_cellRendererInstance;
      if ((UnityEngine.Object) rendererInstance == (UnityEngine.Object) null)
        return;
      rendererInstance.SetConfigurator(configurator);
    }

    public void SetDragAndDropValidator(IDragNDropValidator validator) => this.m_dragNDropValidator = validator;

    public void OnBeginDrag(PointerEventData evt)
    {
      if (!this.m_enableDragAndDrop || !RectTransformUtility.RectangleContainsScreenPoint(this.m_rectTransform, evt.position, evt.pressEventCamera) || this.m_value == null || this.m_dragNDropValidator != null && !this.m_dragNDropValidator.IsValidDrag(this.m_value))
        return;
      ItemDragNDropListener.instance.OnBeginDrag(evt, this.m_cellRendererInstance);
      this.m_isDragging = true;
      if (!this.m_removeOnDrag)
        return;
      this.SetValue((object) null);
    }

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
      if (!this.m_enableDragAndDrop || this.m_dragNDropValidator != null && !this.m_dragNDropValidator.IsValidDrop(this.m_value) || !this.m_insertOnDrop)
        return true;
      if ((bool) (UnityEngine.Object) this.m_cellRendererInstance.gameObject)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_cellRendererInstance.gameObject);
      this.m_cellRendererInstance = cellRenderer;
      this.ConfigureCellRenderer(cellRenderer, false, true);
      this.SetValue(cellRenderer.value);
      return false;
    }

    private void OnDisable()
    {
      if (!this.m_isDragging)
        return;
      ItemDragNDropListener.instance.CancelAll();
    }

    private void OnDestroy()
    {
      if (!(this.m_itemType != (System.Type) null))
        return;
      if (this.m_isDragging)
        ItemDragNDropListener.instance.CancelAll();
      ItemDragNDropListener.instance.UnRegister((DragNDropClient) this, this.m_itemType);
    }
  }
}
