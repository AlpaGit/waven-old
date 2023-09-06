// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.FightEndedUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Fight
{
  public class FightEndedUI : BaseOpenCloseUI
  {
    [SerializeField]
    private Button m_buttonOK;
    public Action onContinueClick;

    private void Start()
    {
      this.m_buttonOK.Select();
      this.m_buttonOK.onClick.AddListener(new UnityAction(this.OnButtonOKClicked));
    }

    public void DoContinueClick() => InputUtility.SimulateClickOn((Selectable) this.m_buttonOK);

    private void OnButtonOKClicked()
    {
      Action onContinueClick = this.onContinueClick;
      if (onContinueClick == null)
        return;
      onContinueClick();
    }
  }
}
