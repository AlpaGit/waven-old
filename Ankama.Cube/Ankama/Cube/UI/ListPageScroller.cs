// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.ListPageScroller
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class ListPageScroller : MonoBehaviour
  {
    [SerializeField]
    private DynamicList m_list;
    [SerializeField]
    private Button m_previousButton;
    [SerializeField]
    private Button m_nextButton;
    private int m_pageCount;
    private bool m_scrolling;

    private void Awake()
    {
      this.m_previousButton.onClick.AddListener(new UnityAction(this.PreviousButton));
      this.m_nextButton.onClick.AddListener(new UnityAction(this.NextButton));
    }

    private void OnEnable()
    {
      this.m_list.OnSetValues += new Action(this.OnContentUpdate);
      this.m_list.OnInsertion += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnScrollPercentage += new Action<float>(this.UpdatePageCount);
      this.OnContentUpdate();
    }

    private void OnDisable()
    {
      this.m_list.OnSetValues -= new Action(this.OnContentUpdate);
      this.m_list.OnInsertion -= new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved -= new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnScrollPercentage -= new Action<float>(this.UpdatePageCount);
    }

    private void PreviousButton()
    {
      if (this.m_scrolling)
        return;
      this.StartCoroutine(this.PreviousButtonCoroutine());
    }

    private IEnumerator PreviousButtonCoroutine()
    {
      this.m_scrolling = true;
      yield return (object) this.m_list.ScrollToPage(this.m_list.currentPageIndex - 1, false);
      this.OnContentUpdate();
      this.m_scrolling = false;
    }

    private void NextButton()
    {
      if (this.m_scrolling)
        return;
      this.StartCoroutine(this.NextButtonCoroutine());
    }

    private IEnumerator NextButtonCoroutine()
    {
      this.m_scrolling = true;
      yield return (object) this.m_list.ScrollToPage(this.m_list.currentPageIndex + 1, false);
      this.OnContentUpdate();
      this.m_scrolling = false;
    }

    private void UpdatePageCount(float percentage) => this.OnContentUpdate();

    private void UpdatePageCount2(object arg1, int arg2) => this.OnContentUpdate();

    private void OnContentUpdate()
    {
      this.m_previousButton.interactable = this.m_list.HasPreviousPage();
      this.m_nextButton.interactable = this.m_list.HastNextPage();
    }
  }
}
