// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.CharacterPanel
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI.Components;
using UnityEngine;

namespace Ankama.Cube.Demo.UI
{
  public class CharacterPanel : Panel
  {
    [SerializeField]
    private RawTextField m_name;
    [SerializeField]
    private RawTextField m_ambience;
    [SerializeField]
    private RawTextField m_difficulty;

    public void Set(SquadDefinition definition, SquadFakeData fakeData)
    {
      if ((Object) definition == (Object) null || (Object) fakeData == (Object) null)
      {
        this.m_name.gameObject.SetActive(false);
        this.m_ambience.gameObject.SetActive(false);
        this.m_difficulty.gameObject.SetActive(false);
      }
      else
      {
        string title = fakeData.title;
        string description = fakeData.description;
        string difficulty = fakeData.difficulty;
        this.m_illu.sprite = fakeData.illu;
        this.m_name.SetText(title);
        this.m_name.gameObject.SetActive(!string.IsNullOrEmpty(title));
        this.m_ambience.SetText(description);
        this.m_ambience.gameObject.SetActive(!string.IsNullOrEmpty(description));
        this.m_difficulty.richText = true;
        this.m_difficulty.SetText(difficulty);
        this.m_difficulty.gameObject.SetActive(!string.IsNullOrEmpty(difficulty));
      }
    }
  }
}
