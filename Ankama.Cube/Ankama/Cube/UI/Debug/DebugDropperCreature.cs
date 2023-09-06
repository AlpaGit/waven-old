// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugDropperCreature
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ankama.Cube.UI.Debug
{
  public class DebugDropperCreature : DebugDropperDataDefinition<CharacterDefinition>
  {
    private const string DisplayName = "Creature";
    private const string SearchPrefKey = "DebugCreatureDropperSearch";
    private CharacterDefinition m_selected;

    public CharacterDefinition selected => this.m_selected;

    public void SetSelected(CharacterDefinition current) => this.m_selected = current;

    protected override CharacterDefinition[] dataValues => ((IEnumerable<CharacterDefinition>) RuntimeData.summoningDefinitions.Values).Concat<CharacterDefinition>((IEnumerable<CharacterDefinition>) RuntimeData.companionDefinitions.Values).ToArray<CharacterDefinition>();

    protected override void Awake()
    {
      base.Awake();
      this.Initialize("Creature", "DebugCreatureDropperSearch");
    }

    protected override void OnGUI()
    {
      if ((Object) this.m_selected == (Object) null)
      {
        base.OnGUI();
      }
      else
      {
        Event current = Event.current;
        if (current.keyCode == KeyCode.Escape)
        {
          this.m_selected = (CharacterDefinition) null;
          this.Close();
        }
        else
        {
          GUI.Label(new Rect((float) this.m_offsetX, 10f, (float) this.m_width, 20f), this.m_title + " : " + this.m_selected.idAndName, new GUIStyle((GUIStyle) "box"));
          GUI.Label(new Rect(current.mousePosition.x + 10f, current.mousePosition.y + 20f, 150f, 20f), this.m_selected.displayName ?? "", new GUIStyle((GUIStyle) "box"));
        }
      }
    }
  }
}
