// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GodSelectionUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class GodSelectionUI : AbstractUI
  {
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private TextFieldDropdown m_godSelection;
    public Action<God> onGodSelected;
    public Action onCloseClick;
    private readonly List<God> m_playableGods = new List<God>();

    private void Start()
    {
      this.m_godSelection.options.Clear();
      List<string> options = new List<string>();
      this.m_playableGods.Clear();
      God god = PlayerData.instance != null ? PlayerData.instance.god : God.Iop;
      int num1 = -1;
      int num2 = 0;
      foreach (GodDefinition godDefinition in RuntimeData.godDefinitions.Values)
      {
        if (godDefinition.playable)
        {
          this.m_playableGods.Add(godDefinition.god);
          options.Add(RuntimeData.FormattedText(godDefinition.i18nNameId, (IValueProvider) null));
          if (godDefinition.god == god)
            num1 = num2;
          ++num2;
        }
      }
      this.m_godSelection.AddOptions(options);
      this.m_godSelection.value = num1 >= 0 ? num1 : 0;
      this.m_godSelection.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChange));
      this.m_closeButton.onClick.AddListener(new UnityAction(this.OnCloseClick));
    }

    private void OnCloseClick()
    {
      Action onCloseClick = this.onCloseClick;
      if (onCloseClick == null)
        return;
      onCloseClick();
    }

    private void OnValueChange(int value)
    {
      God playableGod = this.m_playableGods[value];
      Action<God> onGodSelected = this.onGodSelected;
      if (onGodSelected == null)
        return;
      onGodSelected(playableGod);
    }

    public void SimulateCloseClick() => InputUtility.SimulateClickOn((Selectable) this.m_closeButton);
  }
}
