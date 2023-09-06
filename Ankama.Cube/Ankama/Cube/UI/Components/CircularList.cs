// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.CircularList
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [RequireComponent(typeof (RectMask2D))]
  public class CircularList : List
  {
    private int m_previousFirstCellIndex = -1;
    private int m_previousLastCellIndex = -1;
    private int m_rows;
    [NonSerialized]
    private bool m_initialized;

    protected override void ComputeDimensions()
    {
      base.ComputeDimensions();
      this.m_rows = !this.m_horizontal ? Math.Max(1, (int) Math.Ceiling((double) this.m_totalHeight / (double) this.m_cellSize.y)) + this.m_extraLineCount : Math.Max(1, (int) Math.Ceiling((double) this.m_totalWidth / (double) this.m_cellSize.x)) + this.m_extraLineCount;
      this.m_needFullReLayout = true;
    }

    private void Update()
    {
      int val1_1 = this.ToIndex(this.scrollPercentage, false) - this.m_rows / 2;
      int val1_2 = val1_1 + this.m_rows;
      if (this.m_needFullReLayout)
      {
        foreach (KeyValuePair<int, CellRenderer> keyValuePair in this.m_elementsByItem)
          this.ReturnToPool(keyValuePair.Value);
        this.m_elementsByItem.Clear();
        for (int index1 = val1_1; index1 <= val1_2; ++index1)
        {
          int index2 = CircularList.NormalizeItemIndex(index1, this.m_items.Count);
          object obj = index2 >= 0 ? this.m_items[index2] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index1] = fromPool;
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
          int index4 = CircularList.NormalizeItemIndex(index3, this.m_items.Count);
          object obj = index4 >= 0 ? this.m_items[index4] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index3] = fromPool;
          fromPool.SetValue(obj);
        }
        for (int index5 = Math.Max(val1_1, this.m_previousLastCellIndex + 1); index5 <= val1_2; ++index5)
        {
          int index6 = CircularList.NormalizeItemIndex(index5, this.m_items.Count);
          object obj = index6 >= 0 ? this.m_items[index6] : (object) null;
          CellRenderer fromPool = this.GetFromPool();
          this.m_elementsByItem[index5] = fromPool;
          fromPool.SetValue(obj);
        }
      }
      float rowIndex = this.ToRowIndex(this.scrollPercentage, false);
      if (this.m_horizontal)
      {
        float num1 = rowIndex * (float) this.m_cellSize.x % (float) this.m_cellSize.x;
        int num2 = val1_1;
        for (int index = 0; index <= this.m_rows; ++index)
        {
          float x = (float) (-(this.m_rows / 2) * this.m_cellSize.x) - num1 + (float) (this.m_cellSize.x * index);
          int y = 0;
          CellRenderer cellRenderer;
          if (this.m_elementsByItem.TryGetValue(num2++, out cellRenderer))
            cellRenderer.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, (float) y);
        }
      }
      else
      {
        float num3 = rowIndex * (float) this.m_cellSize.y % (float) this.m_cellSize.y;
        int num4 = val1_1;
        for (int index = 0; index <= this.m_rows; ++index)
        {
          float y = (float) (this.m_rows / 2 * this.m_cellSize.y) + num3 - (float) (this.m_cellSize.y * index);
          int x = 0;
          CellRenderer cellRenderer;
          if (this.m_elementsByItem.TryGetValue(num4++, out cellRenderer))
            cellRenderer.GetComponent<RectTransform>().anchoredPosition = new Vector2((float) x, y);
        }
      }
      this.m_previousFirstCellIndex = val1_1;
      this.m_previousLastCellIndex = val1_2;
    }

    protected override void SetCellRectTransformAnchors(RectTransform rectTransform)
    {
      Vector2 vector2 = new Vector2(0.5f, 0.5f);
      rectTransform.anchorMin = vector2;
      rectTransform.anchorMax = vector2;
      rectTransform.pivot = vector2;
    }

    private static int NormalizeItemIndex(int index, int itemCount) => itemCount > 0 ? (index % itemCount + itemCount) % itemCount : -1;

    private static float NormalizeScrollPercentage(float percentage) => (float) (((double) percentage % 1.0 + 1.0) % 1.0);

    private int ToIndex(float perc, bool normalize) => !normalize ? Mathf.FloorToInt(perc * (float) this.m_items.Count) : Mathf.FloorToInt(CircularList.NormalizeScrollPercentage(perc) * (float) this.m_items.Count);

    private float ToRowIndex(float perc, bool normalize) => !normalize ? perc * (float) this.m_items.Count : CircularList.NormalizeScrollPercentage(perc) * (float) this.m_items.Count;
  }
}
