// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.ListPageDisplayer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class ListPageDisplayer : MonoBehaviour
  {
    [SerializeField]
    private DynamicList m_list;
    [SerializeField]
    private GameObject m_pageDotContainer;
    [SerializeField]
    private Image m_pageDotPrefab;
    [SerializeField]
    private ListPageScrollerConfig m_config;
    private readonly List<Image> m_pageDots = new List<Image>();
    private int m_pageCount;

    private void Awake()
    {
      this.m_list.OnSetValues += new Action(this.OnContentUpdate);
      this.m_list.OnInsertion += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnRemoved += new Action<object, int>(this.UpdatePageCount2);
      this.m_list.OnScrollPercentage += new Action<float>(this.OnScrollUpdate);
      this.OnContentUpdate();
    }

    private void UpdatePageCount2(object arg1, int arg2) => this.OnContentUpdate();

    private void OnScrollUpdate(float percentage) => this.OnContentUpdate();

    private void OnContentUpdate()
    {
      int pageCount = this.m_pageCount;
      int currentPageIndex = this.m_list.currentPageIndex;
      int num1 = this.m_pageCount = this.m_list.pagesCount;
      float num2 = this.m_list.scrollPercentage * (float) (num1 - 1);
      for (int index = pageCount; index < num1; ++index)
      {
        Image v = UnityEngine.Object.Instantiate<Image>(this.m_pageDotPrefab);
        v.transform.SetParent(this.m_pageDotContainer.transform, false);
        v.WithAlpha<Image>(currentPageIndex == index ? this.m_config.selectedPageAlpha : this.m_config.unselectedPageAlpha);
        this.m_pageDots.Add(v);
      }
      for (int index = pageCount - 1; index >= num1; --index)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_pageDots[index].gameObject);
        this.m_pageDots.RemoveAt(index);
      }
      int index1 = 0;
      for (int count = this.m_pageDots.Count; index1 < count; ++index1)
        this.m_pageDots[index1].color = new Color(1f, 1f, 1f, Mathf.Lerp(this.m_config.unselectedPageAlpha, this.m_config.selectedPageAlpha, Mathf.Clamp01(1f - Mathf.Abs((float) index1 - num2))));
    }
  }
}
