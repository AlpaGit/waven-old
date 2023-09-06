// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.PanelList`1
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

namespace Ankama.Cube.Demo.UI
{
  [RequireComponent(typeof (RectTransform))]
  public abstract class PanelList<T> : PanelList where T : Panel
  {
    [SerializeField]
    private PanelListConfig m_config;
    [SerializeField]
    private Button m_leftArrow;
    [SerializeField]
    private Button m_rightArrow;
    private List<PanelList<T>.ElementState> m_elementsStates = new List<PanelList<T>.ElementState>();
    private List<T> m_elements = new List<T>();
    private List<Tween> m_tweens = new List<Tween>();
    private int m_selectedIndex = -1;
    private Sequence m_transitionTweenSequence;
    private SlidingAnimUI m_slidingAnim;
    public Action<int> onElementSelected;

    public int selectedIndex => this.m_selectedIndex;

    public Button rightButton => this.m_rightArrow;

    public Button leftButton => this.m_leftArrow;

    public int lockedLeft { set; get; }

    public int lockedright { set; get; }

    public int elementWidth { set; get; }

    protected void OnEnable() => this.StartCoroutine(this.DelayUpdateElements());

    private void Start()
    {
      this.m_leftArrow.onClick.AddListener((UnityAction) (() => this.OnArrowClicked(-1)));
      this.m_rightArrow.onClick.AddListener((UnityAction) (() => this.OnArrowClicked(1)));
    }

    private void OnRectTransformDimensionsChange() => this.UpdateElements(false);

    private void OnArrowClicked(int direction) => this.SetSelectedIndex(this.m_selectedIndex + direction, true, true);

    public void SetSelectedIndex(int index, bool tween, bool selectCallback)
    {
      if (this.m_elements.Count == 0)
      {
        this.UpdateArrowState();
      }
      else
      {
        this.m_selectedIndex = index;
        this.UpdateArrowState();
        this.UpdateElements(tween);
        if (!selectCallback)
          return;
        Action<int> onElementSelected = this.onElementSelected;
        if (onElementSelected == null)
          return;
        onElementSelected(this.m_selectedIndex);
      }
    }

    private void UpdateArrowState()
    {
      this.m_leftArrow.gameObject.SetActive(this.m_selectedIndex > 0 && this.m_selectedIndex > this.lockedLeft);
      this.m_rightArrow.gameObject.SetActive(this.m_selectedIndex < this.m_elements.Count - 1 && this.m_selectedIndex < this.m_elements.Count - 1 - this.lockedright);
    }

    private IEnumerator DelayUpdateElements()
    {
      yield return (object) null;
      this.UpdateElements(false);
    }

    private void UpdateElements(bool tween)
    {
      if (!this.gameObject.activeInHierarchy || this.m_elements.Count == 0 || this.m_selectedIndex == -1)
        return;
      this.UpdateElementsState(this.m_selectedIndex);
      for (int index = 0; index < this.m_tweens.Count; ++index)
      {
        Tween tween1 = this.m_tweens[index];
        if (tween1.IsActive())
          tween1.Kill();
      }
      this.m_tweens.Clear();
      for (int index = 0; index < this.m_elements.Count; ++index)
      {
        T element = this.m_elements[index];
        PanelList<T>.ElementState elementsState = this.m_elementsStates[index];
        if (tween)
        {
          float selectionTweenDuration = this.m_config.selectionTweenDuration;
          Ease selectionTweenEase = this.m_config.selectionTweenEase;
          element.transform.SetSiblingIndex(elementsState.slibingIndex);
          this.m_tweens.Add((Tween) element.transform.DOLocalMove(elementsState.pos, selectionTweenDuration).SetEase<Tweener>(selectionTweenEase));
          float currentFactor = element.GetVisibilityFactor();
          this.m_tweens.Add((Tween) DOTween.To((DOGetter<float>) (() => currentFactor), (DOSetter<float>) (a => element.SetVisibilityFactor(a, this.m_config)), elementsState.visibility, selectionTweenDuration).SetEase<TweenerCore<float, float, FloatOptions>>(selectionTweenEase));
        }
        else
        {
          element.transform.SetSiblingIndex(elementsState.slibingIndex);
          element.transform.localPosition = elementsState.pos;
          element.SetVisibilityFactor(elementsState.visibility, this.m_config);
        }
      }
    }

    private void UpdateElementsState(int selectedIndex)
    {
      RectTransform transform = this.transform as RectTransform;
      Vector3 a1 = new Vector3(transform.rect.xMin + (float) this.elementWidth / 2f - (float) this.m_config.leftOffset, 0.0f, 0.0f);
      Vector3 a2 = new Vector3(transform.rect.xMax - (float) this.elementWidth / 2f + (float) this.m_config.rightOffset, 0.0f, 0.0f);
      int count = this.m_elementsStates.Count;
      int num1 = selectedIndex;
      int num2 = selectedIndex == count - 1 ? 0 : count - 1 - selectedIndex;
      int slibingIndex = 0;
      for (int index = 0; index < num1; ++index)
      {
        this.m_elementsStates[index] = new PanelList<T>.ElementState(Vector3.Lerp(a1, Vector3.zero, this.m_config.elementRepartition.Evaluate((float) (index + 1) / (float) (num1 + 1))), slibingIndex, (float) (1.0 - (double) (num1 - index) / (double) count));
        ++slibingIndex;
      }
      for (int index = 0; index < num2; ++index)
      {
        this.m_elementsStates[count - 1 - index] = new PanelList<T>.ElementState(Vector3.Lerp(a2, Vector3.zero, this.m_config.elementRepartition.Evaluate((float) (index + 1) / (float) (num2 + 1))), slibingIndex, (float) (1.0 - (double) (num2 - index) / (double) count));
        ++slibingIndex;
      }
      this.m_elementsStates[selectedIndex] = new PanelList<T>.ElementState(Vector3.zero, slibingIndex, 1f);
    }

    public void Add(T element)
    {
      element.transform.SetParent(this.transform, false);
      this.m_elements.Add(element);
      this.m_elementsStates.Add(new PanelList<T>.ElementState());
    }

    public Sequence TransitionAnim(bool open, SlidingSide side)
    {
      if (this.m_slidingAnim.elements == null)
        this.m_slidingAnim.elements = new List<CanvasGroup>();
      this.m_slidingAnim.elements.Clear();
      this.m_slidingAnim.elements.Add(this.m_elements[this.m_selectedIndex].transitionCanvasGroup);
      for (int index1 = 1; index1 < this.m_elements.Count; ++index1)
      {
        int index2 = this.m_selectedIndex - index1;
        if (index2 >= 0 && index2 < this.m_elements.Count)
          this.m_slidingAnim.elements.Add(this.m_elements[index2].transitionCanvasGroup);
        int index3 = this.m_selectedIndex + index1;
        if (index3 >= 0 && index3 < this.m_elements.Count)
          this.m_slidingAnim.elements.Add(this.m_elements[index3].transitionCanvasGroup);
      }
      this.m_slidingAnim.closeConfig = this.m_config.closeAnim;
      this.m_slidingAnim.openConfig = this.m_config.openAnim;
      return this.m_slidingAnim.PlayAnim(open, side);
    }

    private struct ElementState
    {
      public Vector3 pos;
      public int slibingIndex;
      public float visibility;

      public ElementState(Vector3 pos, int slibingIndex, float visibility)
      {
        this.pos = pos;
        this.slibingIndex = slibingIndex;
        this.visibility = visibility;
      }
    }
  }
}
