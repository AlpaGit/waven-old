// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.DynamicListPageScroller
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  public class DynamicListPageScroller : MonoBehaviour
  {
    [SerializeField]
    private DynamicList m_list;
    [SerializeField]
    private Button m_previousButton;
    [SerializeField]
    private Button m_nextButton;
    [SerializeField]
    private RawTextField m_pageText;
    private bool m_scrolling;

    private void Awake()
    {
      this.m_previousButton.onClick.AddListener(new UnityAction(this.PreviousButton));
      this.m_nextButton.onClick.AddListener(new UnityAction(this.NextButton));
      this.m_list.OnSetValues += new Action(this.OnContentUpdate);
      this.m_list.OnInsertion += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved += new Action<object, int>(this.UpdatePageCount2);
      this.OnContentUpdate();
    }

    private void PreviousButton() => this.StartCoroutine(this.PreviousButtonCoroutine());

    private IEnumerator PreviousButtonCoroutine()
    {
      if (!this.m_scrolling)
      {
        this.m_scrolling = true;
        yield return (object) this.m_list.ScrollToPage(this.m_list.currentPageIndex - 1, false);
        this.OnContentUpdate();
        this.m_scrolling = false;
      }
    }

    private void NextButton() => this.StartCoroutine(this.NextButtonCoroutine());

    private IEnumerator NextButtonCoroutine()
    {
      if (!this.m_scrolling)
      {
        this.m_scrolling = true;
        yield return (object) this.m_list.ScrollToPage(this.m_list.currentPageIndex + 1, false);
        this.OnContentUpdate();
        this.m_scrolling = false;
      }
    }

    private void UpdatePageCount2(object arg1, int arg2) => this.OnContentUpdate();

    private void OnContentUpdate()
    {
      int num = this.m_list.currentPageIndex + 1;
      int pagesCount = this.m_list.pagesCount;
      this.m_previousButton.interactable = this.m_list.HasPreviousPage();
      this.m_nextButton.interactable = this.m_list.HastNextPage();
      this.m_pageText.SetText(string.Format("{0} / {1}", (object) num, (object) pagesCount));
    }
  }
}
