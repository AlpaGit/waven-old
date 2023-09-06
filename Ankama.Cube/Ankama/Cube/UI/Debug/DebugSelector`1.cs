// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugSelector`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.UI.Debug
{
  public abstract class DebugSelector<T> : MonoBehaviour
  {
    [SerializeField]
    protected int m_width = 300;
    [SerializeField]
    protected int m_offsetX = 300;
    private readonly string m_title;
    private bool m_showSelection;
    private T m_selected;

    public T selected => this.m_selected;

    protected DebugSelector(string title) => this.m_title = title;

    protected virtual void Awake()
    {
      this.m_selected = ((IEnumerable<T>) this.dataValues).FirstOrDefault<T>();
      this.SetActive(false);
    }

    public void SetActive(bool active) => this.gameObject.SetActive(active);

    private void OnGUI()
    {
      Event current = Event.current;
      if (this.m_showSelection)
      {
        int num1 = 50;
        int lineHeight = 20;
        int num2 = this.m_width + this.m_offsetX + num1 + 10;
        if (current.type == EventType.MouseDown && ((double) current.mousePosition.x > (double) num2 || (double) current.mousePosition.x < (double) this.m_offsetX) || current.type == EventType.KeyDown && current.keyCode == KeyCode.Escape)
        {
          this.m_showSelection = false;
          return;
        }
        if (GUI.Button(new Rect((float) this.m_offsetX, 10f, (float) this.m_width, 20f), string.Format("▼ {0} : {1}", (object) this.m_title, (object) this.selected), DebugSelector.Style))
          this.m_showSelection = false;
        double num3 = (double) this.DisplayDataResults(this.dataValues, (float) this.m_offsetX, 40f, "", (float) lineHeight);
      }
      else
      {
        if (current.type == EventType.KeyDown && current.keyCode == KeyCode.Escape)
        {
          this.Close();
          return;
        }
        if (GUI.Button(new Rect((float) this.m_offsetX, 10f, (float) this.m_width, 20f), string.Format("► {0} : {1}", (object) this.m_title, (object) this.selected), DebugSelector.Style))
          this.m_showSelection = true;
      }
      GUI.Label(new Rect(current.mousePosition.x + 10f, current.mousePosition.y + 20f, 150f, 20f), string.Format("{0}", (object) this.selected), DebugSelector.Style);
    }

    private float DisplayDataResults(
      T[] dataArray,
      float x,
      float y,
      string filter,
      float lineHeight)
    {
      string[] array = ((IEnumerable<T>) dataArray).Select<T, string>((Func<T, string>) (c => c.ToString())).ToArray<string>();
      int xCount = array.Length / 25 + 1;
      float height = (float) array.Length * lineHeight / (float) xCount;
      Rect position = new Rect(x, y, (float) this.m_width, height);
      GUI.Box(position, "");
      int index = GUI.SelectionGrid(position, -1, array, xCount);
      if (index >= 0)
      {
        this.m_showSelection = false;
        this.m_selected = dataArray[index];
      }
      y += height;
      return y;
    }

    private void Close() => this.SetActive(false);

    protected abstract T[] dataValues { get; }
  }
}
