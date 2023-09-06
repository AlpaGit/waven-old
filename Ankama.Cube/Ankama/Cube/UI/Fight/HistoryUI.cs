// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.HistoryUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Fight.History;
using Ankama.Utilities;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public class HistoryUI : MonoBehaviour
  {
    [SerializeField]
    private RectTransform m_window;
    [SerializeField]
    private RectTransform m_container;
    [SerializeField]
    private RectTransform m_enterDummyPos;
    [SerializeField]
    private HistoryAbstractElement[] m_elementPrefabs;
    [SerializeField]
    private HistoryData m_datas;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    private readonly List<HistoryAbstractElement> m_elements = new List<HistoryAbstractElement>();
    private readonly List<HistoryAbstractElement> m_displayedElements = new List<HistoryAbstractElement>();

    private void Start()
    {
      for (int index = 0; index < this.m_elementPrefabs.Length; ++index)
        this.m_elementPrefabs[index].gameObject.SetActive(false);
      this.m_window.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float) (-(double) this.m_container.sizeDelta.y + (double) this.m_datas.maxElements * (double) (this.m_elementPrefabs[0].transform as RectTransform).rect.width + (double) (this.m_datas.maxElements - 1) * (double) this.m_datas.spacing));
    }

    public void AddReserveEvent(PlayerStatus status, int valueBefore) => (this.EnQueueVisibleElement(HistoryElementType.Reserve) as HistoryReserveElement).Set(status, valueBefore);

    public void AddSpellEvent(SpellStatus status, DynamicValueContext context, int cost) => (this.EnQueueVisibleElement(HistoryElementType.Spell) as HistorySpellElement).Set(status, context, cost);

    public void AddCompanionEvent(ReserveCompanionStatus companion) => (this.EnQueueVisibleElement(HistoryElementType.Companion) as HistoryCompanionElement).Set(companion);

    private HistoryAbstractElement GetFreeElement(HistoryElementType type)
    {
      for (int index = 0; index < this.m_elements.Count; ++index)
      {
        if (this.m_elements[index].type == type && !this.m_elements[index].gameObject.activeSelf)
          return this.m_elements[index];
      }
      HistoryAbstractElement original = (HistoryAbstractElement) null;
      for (int index = 0; index < this.m_elementPrefabs.Length; ++index)
      {
        if (this.m_elementPrefabs[index].type == type)
        {
          original = this.m_elementPrefabs[index];
          break;
        }
      }
      if ((UnityEngine.Object) original == (UnityEngine.Object) null)
        Log.Error(string.Format("cannot find prefab for type {0}", (object) type), 77, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Fight\\History\\HistoryUI.cs");
      HistoryAbstractElement freeElement = UnityEngine.Object.Instantiate<HistoryAbstractElement>(original);
      freeElement.onPointerEnter += new Action<HistoryAbstractElement>(this.OnElementPointerEnter);
      freeElement.onPointerExit += new Action<HistoryAbstractElement>(this.OnElementPointerExit);
      freeElement.transform.SetParent((Transform) this.m_container, false);
      this.m_elements.Add(freeElement);
      return freeElement;
    }

    private void OnElementPointerEnter(HistoryAbstractElement e)
    {
      if (FightCastManager.currentCastType != FightCastManager.CurrentCastType.None)
        return;
      FightUIRework.ShowTooltip(e.tooltipProvider, this.m_tooltipPosition, e.GetComponent<RectTransform>());
    }

    private void OnElementPointerExit(HistoryAbstractElement e)
    {
      if (FightCastManager.currentCastType != FightCastManager.CurrentCastType.None)
        return;
      FightUIRework.HideTooltip();
    }

    private HistoryAbstractElement EnQueueVisibleElement(HistoryElementType type)
    {
      HistoryAbstractElement freeElement = this.GetFreeElement(type);
      freeElement.canvasGroup.alpha = 1f;
      freeElement.transform.SetAsLastSibling();
      freeElement.gameObject.SetActive(true);
      (freeElement.transform as RectTransform).anchoredPosition = this.m_enterDummyPos.anchoredPosition;
      this.m_displayedElements.Add(freeElement);
      this.UpdateElements();
      for (int index = 0; index < this.m_displayedElements.Count; ++index)
        this.m_displayedElements[index].depthFactor = (float) (index + 1) / (float) this.m_displayedElements.Count;
      return freeElement;
    }

    private void DeQueueVisibleElement()
    {
      HistoryAbstractElement displayedElement = this.m_displayedElements[0];
      displayedElement.gameObject.SetActive(false);
      displayedElement.transform.SetAsFirstSibling();
      this.m_displayedElements.RemoveAt(0);
    }

    private void UpdateElements()
    {
      int num1 = this.m_displayedElements.Count - this.m_datas.maxElements;
      if (num1 > this.m_datas.maxHiddableElements)
      {
        int num2 = num1 - this.m_datas.maxHiddableElements;
        for (int index = 0; index < num2; ++index)
          this.DeQueueVisibleElement();
      }
      Tweener t = (Tweener) null;
      Vector2 zero = Vector2.zero;
      for (int index = 0; index < this.m_displayedElements.Count; ++index)
      {
        HistoryAbstractElement displayedElement = this.m_displayedElements[this.m_displayedElements.Count - 1 - index];
        CanvasGroup canvasGroup = displayedElement.canvasGroup;
        RectTransform transform = displayedElement.transform as RectTransform;
        float height = transform.rect.height;
        canvasGroup.DOKill();
        transform.DOKill();
        t = transform.DOAnchorPos(zero, this.m_datas.positionTweenDuration).SetEase<Tweener>(this.m_datas.postitionTweenEase);
        if (index == 0)
          transform.DOPunchScale(this.m_datas.inScalePunchValue, this.m_datas.inScalePunchDuration, 0, 0.0f);
        else
          transform.localScale = Vector3.one;
        if (index < this.m_datas.maxElements - 1)
          zero.y -= height + this.m_datas.spacing;
        if (index >= this.m_datas.maxElements)
          canvasGroup.DOFade(0.0f, this.m_datas.outAlphaTweenDuration);
      }
      if (t == null)
        return;
      t.OnComplete<Tweener>(new TweenCallback(this.OnUpdateElementsComplete));
    }

    private void OnUpdateElementsComplete()
    {
      if (this.m_displayedElements.Count <= this.m_datas.maxElements)
        return;
      int num = this.m_displayedElements.Count - this.m_datas.maxElements;
      for (int index = 0; index < num; ++index)
        this.DeQueueVisibleElement();
    }
  }
}
