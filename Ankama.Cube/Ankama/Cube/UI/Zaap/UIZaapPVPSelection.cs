// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Zaap.UIZaapPVPSelection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Zaap
{
  public class UIZaapPVPSelection : AbstractUI
  {
    public const int PVP_1V1_FIGHT_DEFINITION_ID = 1;
    public const int PVM_1V1_FIGHT_DEFINITION_ID = 2;
    public const int PVBOSS_FIGHT_DEFINITION_ID = 3;
    public const int PVP_2V2_FIGHT_DEFINITION_ID = 4;
    public Action<int, int?> onPlayRequested;
    [Header("Button")]
    [SerializeField]
    private AnimatedGraphicButton m_closeButton;
    [SerializeField]
    private Button m_oneVOneBtn;
    [SerializeField]
    private Button m_trainingBtn;
    public Action onCloseClick;

    protected override void Awake()
    {
      base.Awake();
      this.m_closeButton.onClick.AddListener(new UnityAction(this.OnCloseClick));
      this.m_oneVOneBtn.onClick.AddListener((UnityAction) (() => this.OnPlayButtonClicked(1)));
      this.m_trainingBtn.onClick.AddListener((UnityAction) (() => this.OnPlayButtonClicked(2)));
    }

    private void OnPlayButtonClicked(int fightDefinitionId)
    {
      int level;
      if (!PlayerData.instance.weaponInventory.TryGetLevel(PlayerData.instance.GetCurrentWeapon(), out level))
        return;
      Action<int, int?> onPlayRequested = this.onPlayRequested;
      if (onPlayRequested == null)
        return;
      onPlayRequested(fightDefinitionId, new int?(level));
    }

    private void OnCloseClick()
    {
      Action onCloseClick = this.onCloseClick;
      if (onCloseClick == null)
        return;
      onCloseClick();
    }

    public IEnumerator PlayEnterAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      UIZaapPVPSelection zaapPvpSelection = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) zaapPvpSelection.PlayAnimation(zaapPvpSelection.m_animationDirector.GetAnimation("Open"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public IEnumerator PlayTransitionToVersusAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      UIZaapPVPSelection zaapPvpSelection = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) zaapPvpSelection.PlayAnimation(zaapPvpSelection.m_animationDirector.GetAnimation("Transition"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public IEnumerator PlayCloseAnimation()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      UIZaapPVPSelection zaapPvpSelection = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) zaapPvpSelection.PlayAnimation(zaapPvpSelection.m_animationDirector.GetAnimation("Close"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
