// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.RotativeList`3
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public abstract class RotativeList<T, U, W> : MonoBehaviour
    where T : CellRenderer<U, W>, ListHighlightable
    where W : ICellRendererConfigurator
  {
    [Header("List config")]
    [SerializeField]
    private T m_prefab;
    [SerializeField]
    private CanvasGroup m_buttonsCanvasGroup;
    [SerializeField]
    private Button m_leftArrowButton;
    [SerializeField]
    private Button m_rightArrowButton;
    [SerializeField]
    private RectTransform m_container;
    [SerializeField]
    private RectTransform m_leftLimit;
    [SerializeField]
    private RectTransform m_rightLimit;
    [SerializeField]
    protected RectTransform m_outPosition;
    [SerializeField]
    protected RotativeListConfig m_config;
    protected readonly List<T> m_elements = new List<T>();
    protected readonly List<U> m_values = new List<U>();
    protected int m_previousSelectedIndex = -1;
    protected int m_selectedIndex = -1;
    protected Sequence m_currentTweenSequence;
    protected RotativeList<T, U, W>.ElementState[] m_currentTweenElementStates;
    private ICellRendererConfigurator m_cellRendererConfigurator;
    private bool m_enableScrollButtons;

    public event Action<U> OnSelectionChanged;

    public U selectedValue
    {
      get
      {
        if (this.m_selectedIndex < 0 || this.m_selectedIndex >= this.m_values.Count)
          return default (U);
        return this.m_selectedIndex < this.m_elements.Count ? (U) this.m_elements[this.m_selectedIndex].value : this.m_values[this.m_selectedIndex];
      }
    }

    public int selectedIndex => this.m_selectedIndex;

    public int count => this.m_values.Count;

    public bool enableScrollButtons
    {
      get => this.m_enableScrollButtons;
      set
      {
        if (this.m_enableScrollButtons == value)
          return;
        this.m_enableScrollButtons = value;
        this.m_buttonsCanvasGroup.gameObject.SetActive(true);
        if (value)
          this.m_buttonsCanvasGroup.DOFade(1f, this.m_config.inTweenDuration).SetEase<Tweener>(this.m_config.inTweenEase);
        else
          this.m_buttonsCanvasGroup.DOFade(0.0f, this.m_config.outTweenDuration).SetEase<Tweener>(this.m_config.outTweenEase).OnKill<Tweener>((TweenCallback) (() => this.m_buttonsCanvasGroup.gameObject.SetActive(false)));
      }
    }

    protected virtual void Start()
    {
      this.m_prefab.gameObject.SetActive(false);
      this.m_buttonsCanvasGroup.alpha = 0.0f;
      this.m_leftArrowButton.onClick.AddListener((UnityAction) (() => this.OnArrowClicked(-1)));
      this.m_rightArrowButton.onClick.AddListener((UnityAction) (() => this.OnArrowClicked(1)));
    }

    public void SetCellRendererConfigurator(ICellRendererConfigurator configurator)
    {
      this.m_cellRendererConfigurator = configurator;
      foreach (T element in this.m_elements)
        element.SetConfigurator(configurator, true);
    }

    public void UpdateAllConfigurators(bool instant = false)
    {
      foreach (T element in this.m_elements)
        element.OnConfiguratorUpdate(instant);
    }

    public void UpdateConfiguratorWithValue(object value, bool instant = false)
    {
      foreach (T element in this.m_elements)
      {
        element.OnConfiguratorUpdate(instant);
        if (element.value == value)
          element.OnConfiguratorUpdate(instant);
      }
    }

    public virtual void SetValues(IList<U> values, bool scroll = true)
    {
      int index1 = 0;
      for (int count = values.Count; index1 < count; ++index1)
        this.m_values.Add(values[index1]);
      int index2 = 0;
      for (int count = this.m_values.Count; index2 < count; ++index2)
      {
        T element = this.CreateElement();
        this.SetElementValue(this.m_values[index2], element);
        this.m_elements.Add(element);
      }
      this.SetSelectedIndex(0, false, scroll: scroll);
    }

    public virtual void SetValueAt(int index, U value)
    {
      if (index < 0 || index > this.m_values.Count)
        return;
      this.m_values[index] = value;
      T element = this.m_elements[index];
      this.SetElementValue(value, element);
    }

    public virtual void Insert(int index, U value, bool selectNew = false)
    {
      if (index < 0 || index > this.m_values.Count)
        return;
      this.m_values.Insert(index, value);
      T element = this.CreateElement();
      this.SetElementValue(this.m_values[index], element);
      this.m_elements.Insert(index, element);
      this.SetSelectedIndex(selectNew ? index : this.m_selectedIndex, force: true);
    }

    public virtual void RemoveAt(int index)
    {
      if (index < 0 || index >= this.m_values.Count)
        return;
      this.DestroyElement(this.m_elements[index]);
      this.m_values.RemoveAt(index);
      this.m_elements.RemoveAt(index);
      index = Mathf.Clamp(index, 0, this.m_values.Count - 1);
      this.SetSelectedIndex(index, force: true);
    }

    protected abstract void SetElementValue(U value, T element);

    public void SetSelectedValue(U value, bool tween = true, bool scroll = true) => this.SetSelectedIndex(this.IndexByValue(value), tween, scroll: scroll);

    public void SetSelectedIndex(int index, bool tween = true, bool force = false, bool scroll = true)
    {
      if (index < 0 || index >= this.m_values.Count || !force && index == this.m_selectedIndex)
        return;
      this.m_previousSelectedIndex = this.m_selectedIndex;
      this.m_selectedIndex = index;
      Action<U> selectionChanged = this.OnSelectionChanged;
      if (selectionChanged != null)
        selectionChanged(this.m_values[this.m_selectedIndex]);
      if (!scroll)
        return;
      this.ScrollToIndex(tween);
    }

    private int IndexByValue(U value)
    {
      int index = 0;
      for (int count = this.m_values.Count; index < count; ++index)
      {
        if (this.m_values[index].Equals((object) value))
          return index;
      }
      return -1;
    }

    private T CreateElement()
    {
      T element = UnityEngine.Object.Instantiate<T>(this.m_prefab);
      element.transform.SetParent((Transform) this.m_container, false);
      element.transform.localPosition = this.m_outPosition.localPosition;
      element.gameObject.SetActive(true);
      element.SetConfigurator(this.m_cellRendererConfigurator, false);
      return element;
    }

    private void DestroyElement(T element) => UnityEngine.Object.Destroy((UnityEngine.Object) element.gameObject);

    private void OnArrowClicked(int direction) => this.SetSelectedIndex(this.m_selectedIndex + direction);

    private void UpdateArrowState()
    {
      if (this.m_values.Count == 0)
      {
        this.m_leftArrowButton.gameObject.SetActive(false);
        this.m_rightArrowButton.gameObject.SetActive(false);
      }
      else
      {
        bool flag1 = this.m_selectedIndex > 0 && (this.m_config.emptyCellsAreSelectable || (object) this.m_values[this.m_selectedIndex - 1] != null);
        bool flag2 = this.m_selectedIndex < this.m_values.Count - 1 && (this.m_config.emptyCellsAreSelectable || (object) this.m_values[this.m_selectedIndex + 1] != null);
        this.m_leftArrowButton.gameObject.SetActive(flag1);
        this.m_rightArrowButton.gameObject.SetActive(flag2);
      }
    }

    private void ScrollToIndex(bool useTween)
    {
      RotativeList<T, U, W>.ElementState[] elementStates = this.ComputeElementStates();
      if (useTween)
      {
        Sequence currentTweenSequence = this.m_currentTweenSequence;
        if (currentTweenSequence != null)
          currentTweenSequence.Kill();
        Sequence s = this.m_currentTweenSequence = DOTween.Sequence();
        this.m_currentTweenElementStates = elementStates;
        for (int index = 0; index < this.m_elements.Count; ++index)
        {
          T element = this.m_elements[index];
          RotativeList<T, U, W>.ElementState elementState = elementStates[index];
          element.SetHighlightFactor(index == this.m_selectedIndex ? 1f : 0.0f);
          s.Insert(0.0f, (Tween) element.transform.DOLocalMove(elementState.pos, this.m_config.moveTweenDuration).SetEase<Tweener>(this.m_config.moveTweenEase));
          if (index == this.m_selectedIndex)
            element.SetVisibilityFactor(this.m_config.cellVisibilityCurve.Evaluate(elementState.depth));
          else
            s.Insert(0.0f, (Tween) DOTween.To(new DOGetter<float>(((ListHighlightable) element).GetVisibilityFactor), (DOSetter<float>) (a => element.SetVisibilityFactor(this.m_config.cellVisibilityCurve.Evaluate(a))), elementState.depth, this.m_config.moveTweenDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_config.moveTweenEase));
        }
        this.UpdateSiblingIndexes();
      }
      else
      {
        for (int index = 0; index < this.m_elements.Count; ++index)
        {
          T element = this.m_elements[index];
          RotativeList<T, U, W>.ElementState elementState = elementStates[index];
          element.transform.localPosition = elementState.pos;
          element.transform.SetSiblingIndex(elementState.siblingIndex);
          element.SetVisibilityFactor(this.m_config.cellVisibilityCurve.Evaluate(elementState.depth));
          element.SetHighlightFactor(this.m_config.cellHighlightCurve.Evaluate(index == this.m_selectedIndex ? 1f : 0.0f));
        }
      }
      this.UpdateArrowState();
    }

    protected void OnTweenComplete()
    {
      this.m_currentTweenSequence = (Sequence) null;
      this.UpdateSiblingIndexes();
    }

    private void UpdateSiblingIndexes()
    {
      if (this.m_currentTweenElementStates == null)
        return;
      RotativeList<T, U, W>.ElementState[] tweenElementStates = this.m_currentTweenElementStates;
      for (int index = 0; index < this.m_elements.Count; ++index)
        this.m_elements[index].transform.SetSiblingIndex(tweenElementStates[index].siblingIndex);
      this.m_currentTweenElementStates = (RotativeList<T, U, W>.ElementState[]) null;
    }

    public IEnumerator TweenOut()
    {
      Sequence currentTweenSequence = this.m_currentTweenSequence;
      if (currentTweenSequence != null)
        currentTweenSequence.Kill();
      yield return (object) this.TweenOutSequence().WaitForKill();
    }

    protected virtual Sequence TweenOutSequence()
    {
      Sequence sequence = this.m_currentTweenSequence = DOTween.Sequence();
      this.enableScrollButtons = false;
      float atPosition = (double) this.m_config.outTweenDelayByElement >= 0.0 ? this.m_config.outTweenDelayByElement : -this.m_config.outTweenDelayByElement * (float) (this.m_elements.Count - 1);
      for (int index = 0; index < this.m_elements.Count; ++index)
      {
        T element = this.m_elements[index];
        sequence.Insert(atPosition, (Tween) element.transform.DOLocalMove(this.m_outPosition.localPosition, this.m_config.outTweenDuration).SetEase<Tweener>(this.m_config.outTweenEase));
        sequence.Insert(atPosition, (Tween) element.transform.DOScaleX(this.m_config.outScale, this.m_config.outTweenDuration).SetEase<Tweener>(this.m_config.outTweenEase).OnComplete<Tweener>((TweenCallback) (() => element.gameObject.SetActive(false))));
        sequence.Insert(atPosition, (Tween) DOTween.To(new DOGetter<float>(((ListHighlightable) element).GetHighlightFactor), (DOSetter<float>) (a => element.SetHighlightFactor(this.m_config.cellHighlightCurve.Evaluate(a))), 0.0f, this.m_config.outTweenDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_config.outTweenEase));
        atPosition += this.m_config.outTweenDelayByElement;
      }
      sequence.OnKill<Sequence>(new TweenCallback(this.OnTweenComplete));
      return sequence;
    }

    protected virtual Sequence TweenInSequence()
    {
      Sequence s = this.m_currentTweenSequence = DOTween.Sequence();
      this.enableScrollButtons = true;
      RotativeList<T, U, W>.ElementState[] elementStates = this.ComputeElementStates();
      this.m_currentTweenElementStates = elementStates;
      this.UpdateSiblingIndexes();
      float atPosition = (double) this.m_config.inTweenDelayByElement >= 0.0 ? this.m_config.inTweenDelayByElement : -this.m_config.inTweenDelayByElement * (float) (this.m_elements.Count - 1);
      for (int index = 0; index < this.m_elements.Count; ++index)
      {
        T element = this.m_elements[index];
        RotativeList<T, U, W>.ElementState elementState = elementStates[index];
        element.gameObject.SetActive(true);
        s.Insert(atPosition, (Tween) element.transform.DOLocalMove(elementState.pos, this.m_config.inTweenDuration).SetEase<Tweener>(this.m_config.inTweenEase));
        s.Insert(atPosition, (Tween) element.transform.DOScaleX(1f, this.m_config.inTweenDuration).SetEase<Tweener>(this.m_config.inTweenEase));
        s.Insert(atPosition, (Tween) DOTween.To(new DOGetter<float>(((ListHighlightable) element).GetHighlightFactor), (DOSetter<float>) (a => element.SetHighlightFactor(this.m_config.cellHighlightCurve.Evaluate(a))), index == this.m_selectedIndex ? 1f : 0.0f, this.m_config.inTweenDuration).SetEase<TweenerCore<float, float, FloatOptions>>(this.m_config.inTweenEase));
        atPosition += this.m_config.inTweenDelayByElement;
      }
      return s;
    }

    public IEnumerator TweenIn()
    {
      Sequence currentTweenSequence = this.m_currentTweenSequence;
      if (currentTweenSequence != null)
        currentTweenSequence.Kill();
      yield return (object) this.TweenInSequence().WaitForKill();
    }

    private RotativeList<T, U, W>.ElementState[] ComputeElementStates()
    {
      int count = this.m_elements.Count;
      RotativeList<T, U, W>.ElementState[] elementStates = new RotativeList<T, U, W>.ElementState[count];
      int selectedIndex = this.selectedIndex;
      int num = this.selectedIndex == count - 1 ? 0 : count - 1 - this.selectedIndex;
      int siblingIndex = 0;
      for (int index = 0; index < selectedIndex; ++index)
      {
        elementStates[index] = RotativeList<T, U, W>.ElementState.Create(Vector3.Lerp(this.m_leftLimit.localPosition, Vector3.zero, this.m_config.cellPositionCurve.Evaluate((float) (index + 1) / (float) (selectedIndex + 1))), siblingIndex, this.m_config.cellVisibilityCurve.Evaluate((float) (1.0 - (double) (selectedIndex - index) / (double) count)));
        ++siblingIndex;
      }
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = count - 1 - index1;
        elementStates[index2] = RotativeList<T, U, W>.ElementState.Create(Vector3.Lerp(this.m_rightLimit.localPosition, Vector3.zero, this.m_config.cellPositionCurve.Evaluate((float) (index1 + 1) / (float) (num + 1))), siblingIndex, this.m_config.cellVisibilityCurve.Evaluate((float) (1.0 - (double) (num - index1) / (double) count)));
        ++siblingIndex;
      }
      elementStates[this.selectedIndex] = RotativeList<T, U, W>.ElementState.Create(Vector3.zero, siblingIndex, 1f);
      return elementStates;
    }

    protected struct ElementState
    {
      public Vector3 pos;
      public int siblingIndex;
      public float depth;

      public static RotativeList<T, U, W>.ElementState Create(
        Vector3 pos,
        int siblingIndex,
        float depth)
      {
        return new RotativeList<T, U, W>.ElementState()
        {
          pos = pos,
          siblingIndex = siblingIndex,
          depth = depth
        };
      }
    }
  }
}
