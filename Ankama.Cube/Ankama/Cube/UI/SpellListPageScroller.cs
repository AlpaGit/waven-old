// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.SpellListPageScroller
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class SpellListPageScroller : MonoBehaviour
  {
    [SerializeField]
    private DynamicList m_list;
    [SerializeField]
    private List<Button> m_buttons;
    [SerializeField]
    private Scrollbar m_scrollBar;
    private int m_pageCount;
    private bool m_scrolling;

    private void Awake()
    {
      this.m_scrollBar.interactable = false;
      int index1 = 0;
      for (int count = this.m_buttons.Count; index1 < count; ++index1)
      {
        Button button = this.m_buttons[index1];
        int index = index1;
        button.onClick.AddListener((UnityAction) (() => this.ScrollTo(index)));
      }
    }

    private void OnEnable()
    {
      this.m_list.OnSetValues += new Action(this.OnContentUpdate);
      this.m_list.OnInsertion += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnScrollPercentage += new Action<float>(this.OnScrollUpdate);
      this.OnContentUpdate();
    }

    private void OnDisable()
    {
      this.m_list.OnSetValues -= new Action(this.OnContentUpdate);
      this.m_list.OnInsertion -= new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved -= new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnScrollPercentage -= new Action<float>(this.OnScrollUpdate);
    }

    private void ScrollTo(int i)
    {
      if (this.m_scrolling)
        return;
      this.StartCoroutine(this.ScrollToCoroutine(i));
    }

    private IEnumerator ScrollToCoroutine(int i)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      SpellListPageScroller listPageScroller = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        listPageScroller.OnContentUpdate();
        listPageScroller.m_scrolling = false;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      listPageScroller.m_scrolling = true;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) listPageScroller.m_list.ScrollToPage(i, false, new TweenCallback(listPageScroller.OnContentUpdate));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private void UpdatePageCount2(object arg1, int arg2) => this.OnContentUpdate();

    private void OnScrollUpdate(float percentage) => this.OnContentUpdate();

    private void OnContentUpdate() => this.m_scrollBar.value = this.m_list.scrollPercentage;
  }
}
