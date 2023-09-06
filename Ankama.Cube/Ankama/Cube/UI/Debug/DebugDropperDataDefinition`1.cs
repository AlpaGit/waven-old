// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugDropperDataDefinition`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Utilities;
using DataEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.UI.Debug
{
  public abstract class DebugDropperDataDefinition<T> : MonoBehaviour where T : EditableData
  {
    private const int Limit = 25;
    protected string m_title;
    private string m_searchPrefKey;
    [SerializeField]
    private string m_search = "";
    [SerializeField]
    protected int m_width = 300;
    [SerializeField]
    protected int m_offsetX = 300;
    private string m_lastSearch = "";
    private int m_level;
    private KeyCode m_closeKeyCode;
    private Event m_lastEvent;
    private T[] m_allData;
    private T[] m_allDataNames;
    private T[] m_allDataDescriptions;

    public event Action<T, int, Event> OnSelected;

    protected void Initialize(string title, string searchPrefKey)
    {
      this.m_title = title;
      this.m_searchPrefKey = searchPrefKey;
      this.m_search = PlayerPrefs.GetString(this.m_searchPrefKey);
    }

    protected void Reset() => this.OnSelected = (Action<T, int, Event>) null;

    protected virtual void Awake() => this.SetActive(false);

    public void SetCloseKeyCode(KeyCode closeKey) => this.m_closeKeyCode = closeKey;

    public void SetActive(bool active) => this.gameObject.SetActive(active);

    public void SetLocalPlayerLevel()
    {
      FightStatus local = FightStatus.local;
      if (local == null)
        return;
      foreach (HeroStatus enumerateEntity in local.EnumerateEntities<HeroStatus>())
      {
        if (enumerateEntity.ownerId == local.localPlayerId)
        {
          this.m_level = enumerateEntity.level;
          break;
        }
      }
    }

    protected virtual void OnGUI()
    {
      Event current = Event.current;
      if (current.type == EventType.MouseDown)
        this.m_lastEvent = new Event(current);
      int width = 50;
      int x = this.m_width + this.m_offsetX + width + 10;
      if (current.type == EventType.MouseDown && ((double) current.mousePosition.x > (double) x || (double) current.mousePosition.x < (double) this.m_offsetX) || current.type == EventType.KeyDown && (current.keyCode == KeyCode.Escape || current.keyCode == this.m_closeKeyCode))
      {
        this.Close();
      }
      else
      {
        GUI.Label(new Rect((float) this.m_offsetX, 10f, (float) width, 20f), this.m_title + " :");
        int num1 = 20;
        GUI.SetNextControlName("SearchField");
        Rect position = new Rect((float) (this.m_offsetX + width + 10), 10f, 200f, (float) num1);
        this.m_search = GUI.TextField(position, this.m_search, 25);
        if (GUI.GetNameOfFocusedControl() == string.Empty)
          GUI.FocusControl("SearchField");
        GUI.Label(new Rect(position.xMax + 5f, 10f, 30f, (float) num1), "level");
        GUI.SetNextControlName("LevelField");
        int result;
        if (int.TryParse(GUI.TextField(new Rect(position.xMax + 35f, 10f, (float) ((double) this.m_width - (double) position.width - 35.0), (float) num1), this.m_level.ToString()), out result))
          this.m_level = result;
        if (GUI.Button(new Rect((float) x, 10f, 20f, (float) num1), "x"))
        {
          this.Close();
        }
        else
        {
          string lowerInvariant = this.m_search.ToLowerInvariant();
          if (lowerInvariant != this.m_lastSearch)
          {
            this.m_lastSearch = lowerInvariant;
            this.m_allDataNames = (T[]) null;
            this.m_allDataDescriptions = (T[]) null;
            PlayerPrefs.SetString(this.m_searchPrefKey, this.m_search);
          }
          int num2 = this.DisplayDataResults(this.allDataNames, this.m_offsetX + width + 10, 40, "by name", num1);
          this.DisplayDataResults(this.allDataDescriptions, this.m_offsetX + width + 10, 50 + num2, "by description", num1);
        }
      }
    }

    private int DisplayDataResults(T[] dataArray, int x, int y, string filter, int lineHeight)
    {
      List<string> list = ((IEnumerable<T>) dataArray).Select<T, string>((Func<T, string>) (c => c.idAndName)).ToList<string>();
      GUI.Label(new Rect((float) x, (float) y, (float) this.m_width, (float) lineHeight), string.Format("{0} {1}(s) found ", (object) list.Count, (object) this.m_title) + filter, (GUIStyle) "box");
      string[] array = list.Take<string>(25).ToArray<string>();
      int height = array.Length * lineHeight;
      Rect position = new Rect((float) x, (float) (y + lineHeight), (float) this.m_width, (float) height);
      GUI.Box(position, "", (GUIStyle) "box");
      int index = GUI.SelectionGrid(position, -1, array, 1);
      if (index >= 0)
        this.OnSelect(dataArray[index]);
      y += height;
      if (list.Count > 25)
      {
        y += lineHeight;
        GUI.Label(new Rect((float) x, (float) y, (float) this.m_width, (float) lineHeight), "...", (GUIStyle) "box");
      }
      return y;
    }

    protected void Close() => this.SetActive(false);

    private void OnSelect(T data)
    {
      if (this.m_level < 1)
        this.m_level = 1;
      if (this.m_level > 10)
        this.m_level = 10;
      Action<T, int, Event> onSelected = this.OnSelected;
      if (onSelected == null)
        return;
      onSelected(data, this.m_level, this.m_lastEvent);
    }

    protected abstract T[] dataValues { get; }

    private T[] allData
    {
      get
      {
        if (this.m_allData == null)
          this.m_allData = this.dataValues;
        return this.m_allData;
      }
    }

    private T[] allDataNames
    {
      get
      {
        if (this.m_allDataNames == null)
        {
          string search = this.m_lastSearch.ToLower();
          this.m_allDataNames = ((IEnumerable<T>) this.allData).Where<T>(!int.TryParse(this.m_lastSearch, out int _) ? (Func<T, bool>) (definition => definition.displayName.ContainsIgnoreDiacritics(search, StringComparison.OrdinalIgnoreCase)) : (Func<T, bool>) (definition => definition.id.ToString().Contains(search) || definition.displayName.ToLower().Contains(search))).ToArray<T>();
        }
        return this.m_allDataNames;
      }
    }

    private T[] allDataDescriptions
    {
      get
      {
        if (this.m_allDataDescriptions == null)
        {
          string search = this.m_lastSearch.ToLower();
          string str;
          this.m_allDataDescriptions = ((IEnumerable<T>) this.allData).Where<T>((Func<T, bool>) (c => c is IDefinitionWithTooltip definitionWithTooltip && RuntimeData.TryGetText(definitionWithTooltip.i18nDescriptionId, out str) && str.ToLower().ContainsIgnoreDiacritics(search, StringComparison.OrdinalIgnoreCase))).ToArray<T>();
        }
        return this.m_allDataDescriptions;
      }
    }
  }
}
