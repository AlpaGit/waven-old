// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.SimpleList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (RectMask2D))]
  public class SimpleList : List
  {
    [Header("ScrollBars")]
    [SerializeField]
    private Scrollbar m_horizontalScrollbar;
    [SerializeField]
    private Scrollbar m_verticalScrollbar;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_minScrollBarButtonSize;
    private int m_previousFirstCellIndex = -1;
    private int m_previousLastCellIndex = -1;
    private int m_rows;
    private int m_columns;
    private float m_horizontalLeeway;
    private float m_verticalLeeway;
    [NonSerialized]
    private bool m_initialized;

    protected override void Awake()
    {
      base.Awake();
      if (this.m_horizontal)
      {
        if ((bool) (UnityEngine.Object) this.m_horizontalScrollbar)
          this.m_horizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollValueChanged));
        if (!(bool) (UnityEngine.Object) this.m_verticalScrollbar)
          return;
        this.m_verticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollValueChanged));
      }
      else
      {
        if ((bool) (UnityEngine.Object) this.m_horizontalScrollbar)
          this.m_horizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollValueChanged));
        if (!(bool) (UnityEngine.Object) this.m_verticalScrollbar)
          return;
        this.m_verticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollValueChanged));
      }
    }

    private void OnScrollValueChanged(float value) => this.scrollPercentage = value;

    protected override void CheckInit()
    {
      base.CheckInit();
      if ((bool) (UnityEngine.Object) this.m_horizontalScrollbar)
        this.m_horizontalScrollbar.gameObject.SetActive(this.m_horizontal);
      else if (this.m_horizontal)
        Debug.LogWarningFormat("List {0} doesn't have a horizontal scrollBar", (object) this.name);
      if ((bool) (UnityEngine.Object) this.m_verticalScrollbar)
      {
        this.m_verticalScrollbar.gameObject.SetActive(!this.m_horizontal);
      }
      else
      {
        if (this.m_horizontal)
          return;
        Debug.LogWarningFormat("List {0} doesn't have a vertical scrollBar", (object) this.name);
      }
    }

    protected override void ComputeDimensions()
    {
      base.ComputeDimensions();
      if (this.m_horizontal)
      {
        this.m_rows = Math.Max(1, this.m_totalHeight / this.m_cellSize.y);
        this.m_columns = Math.Max(1, (int) Math.Ceiling((double) this.m_totalWidth / (double) this.m_cellSize.x)) + this.m_extraLineCount;
        this.m_horizontalLeeway = Math.Max(0.0f, Mathf.Ceil((float) this.m_items.Count / (float) this.m_rows) * (float) this.m_cellSize.x - (float) this.m_totalWidth);
        this.m_verticalLeeway = (float) Math.Max(0, this.m_columns * this.m_cellSize.y - this.m_totalHeight);
      }
      else
      {
        this.m_rows = Math.Max(1, (int) Math.Ceiling((double) this.m_totalHeight / (double) this.m_cellSize.y)) + this.m_extraLineCount;
        this.m_columns = Math.Max(1, this.m_totalWidth / this.m_cellSize.x);
        this.m_verticalLeeway = Math.Max(0.0f, Mathf.Ceil((float) this.m_items.Count / (float) this.m_columns) * (float) this.m_cellSize.y - (float) this.m_totalHeight);
        this.m_horizontalLeeway = (float) Math.Max(0, this.m_rows * this.m_cellSize.x - this.m_totalWidth);
      }
      this.SetupScrollBars();
      this.m_needFullReLayout = true;
    }

    private void SetupScrollBars()
    {
      if (this.m_horizontal && (bool) (UnityEngine.Object) this.m_horizontalScrollbar)
      {
        this.m_horizontalScrollbar.size = Mathf.Min(1f, Mathf.Max(this.m_minScrollBarButtonSize, (float) this.m_totalWidth / ((float) (this.m_items.Count * this.m_cellSize.x) / (float) this.m_rows)));
        this.m_horizontalScrollbar.numberOfSteps = 0;
        this.m_horizontalScrollbar.value = this.scrollPercentage;
      }
      else
      {
        if (this.m_horizontal || !(bool) (UnityEngine.Object) this.m_verticalScrollbar)
          return;
        this.m_verticalScrollbar.size = Mathf.Min(1f, Mathf.Max(this.m_minScrollBarButtonSize, (float) this.m_totalHeight / ((float) (this.m_items.Count * this.m_cellSize.y) / (float) this.m_columns)));
        this.m_verticalScrollbar.numberOfSteps = 0;
        this.m_verticalScrollbar.value = this.scrollPercentage;
      }
    }

    private void Update()
    {
      int index1 = this.ToIndex(this.scrollPercentage);
      int val1_1 = Mathf.Clamp(index1, 0, this.m_items.Count);
      int val1_2 = index1 + this.m_rows * this.m_columns - 1;
      if (this.m_needFullReLayout)
      {
        foreach (KeyValuePair<int, CellRenderer> keyValuePair in this.m_elementsByItem)
          this.ReturnToPool(keyValuePair.Value);
        this.m_elementsByItem.Clear();
        for (int index2 = val1_1; index2 <= val1_2; ++index2)
        {
          object obj = index2 < this.m_items.Count ? this.m_items[index2] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index2] = fromPool;
          fromPool.SetValue(obj);
        }
        this.m_needFullReLayout = false;
      }
      else
      {
        for (int previousFirstCellIndex = this.m_previousFirstCellIndex; previousFirstCellIndex < Math.Min(val1_1, this.m_previousLastCellIndex + 1); ++previousFirstCellIndex)
        {
          CellRenderer cellRenderer;
          if (this.m_elementsByItem.TryGetValue(previousFirstCellIndex, out cellRenderer))
          {
            this.ReturnToPool(cellRenderer);
            this.m_elementsByItem.Remove(previousFirstCellIndex);
          }
        }
        for (int previousLastCellIndex = this.m_previousLastCellIndex; previousLastCellIndex > Math.Max(val1_2, this.m_previousFirstCellIndex - 1); --previousLastCellIndex)
        {
          CellRenderer cellRenderer;
          if (this.m_elementsByItem.TryGetValue(previousLastCellIndex, out cellRenderer))
          {
            this.ReturnToPool(cellRenderer);
            this.m_elementsByItem.Remove(previousLastCellIndex);
          }
        }
        for (int index3 = val1_1; index3 < Math.Min(this.m_previousFirstCellIndex, val1_2 + 1); ++index3)
        {
          object obj = index3 < this.m_items.Count ? this.m_items[index3] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index3] = fromPool;
          fromPool.SetValue(obj);
        }
        for (int index4 = Math.Max(val1_1, this.m_previousLastCellIndex + 1); index4 <= val1_2; ++index4)
        {
          object obj = index4 < this.m_items.Count ? this.m_items[index4] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index4] = fromPool;
          fromPool.SetValue(obj);
        }
      }
      float rowIndex = this.ToRowIndex(this.scrollPercentage);
      if (this.m_horizontal)
      {
        float num1 = rowIndex * (float) this.m_cellSize.x % (float) this.m_cellSize.x;
        int num2 = val1_1;
        for (int index5 = 0; index5 < this.m_columns; ++index5)
        {
          float x = (float) (this.m_cellSize.x * index5) - num1;
          for (int index6 = 0; index6 < this.m_rows; ++index6)
          {
            int y = this.m_totalHeight - this.m_cellSize.y * (index6 + 1);
            CellRenderer cellRenderer;
            if (this.m_elementsByItem.TryGetValue(num2++, out cellRenderer))
              cellRenderer.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, (float) y);
          }
        }
      }
      else
      {
        float num3 = rowIndex * (float) this.m_cellSize.y % (float) this.m_cellSize.y;
        int num4 = val1_1;
        for (int index7 = 0; index7 < this.m_rows; ++index7)
        {
          float y = (float) this.m_totalHeight - ((float) (this.m_cellSize.y * (index7 + 1)) - num3);
          for (int index8 = 0; index8 < this.m_columns; ++index8)
          {
            int x = this.m_cellSize.x * index8;
            CellRenderer cellRenderer;
            if (this.m_elementsByItem.TryGetValue(num4++, out cellRenderer))
              cellRenderer.GetComponent<RectTransform>().anchoredPosition = new Vector2((float) x, y);
          }
        }
      }
      this.m_previousFirstCellIndex = val1_1;
      this.m_previousLastCellIndex = val1_2;
    }

    private int ToIndex(float perc)
    {
      float num = Mathf.Clamp01(perc);
      return !this.m_horizontal ? (int) Math.Floor((double) this.m_verticalLeeway * (double) num / (double) this.m_cellSize.y) * this.m_columns : (int) Math.Floor((double) this.m_horizontalLeeway * (double) num / (double) this.m_cellSize.x) * this.m_rows;
    }

    private float ToRowIndex(float perc)
    {
      float num = Mathf.Clamp01(perc);
      return !this.m_horizontal ? this.m_verticalLeeway * num / (float) this.m_cellSize.y : this.m_horizontalLeeway * num / (float) this.m_cellSize.x;
    }

    protected override void SetCellRectTransformAnchors(RectTransform rectTransform)
    {
      rectTransform.anchorMin = Vector2.zero;
      rectTransform.anchorMax = Vector2.zero;
      rectTransform.pivot = Vector2.zero;
    }
  }
}
