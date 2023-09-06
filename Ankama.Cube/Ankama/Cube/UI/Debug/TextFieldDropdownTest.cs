// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.TextFieldDropdownTest
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Debug
{
  public class TextFieldDropdownTest : MonoBehaviour
  {
    [SerializeField]
    private TextFieldDropdown m_dropdown;

    private void Awake()
    {
      List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
      int num = 0;
      for (int index = 5; num < index; ++num)
        options.Add(new Dropdown.OptionData(string.Format("Choix {0}", (object) num)));
      this.m_dropdown.AddOptions(options);
    }
  }
}
