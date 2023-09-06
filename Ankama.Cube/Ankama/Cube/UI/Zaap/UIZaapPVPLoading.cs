// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Zaap.UIZaapPVPLoading
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI.Components;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Zaap
{
  public class UIZaapPVPLoading : AbstractUI
  {
    public Action onForceAiRequested;
    public Action onCancelRequested;
    public Action<int, int?> onEnterAnimationFinished;
    [Header("Canvas")]
    [SerializeField]
    private CanvasGroup m_searchOpponentGroup;
    [Header("Button")]
    [SerializeField]
    private AnimatedTextButton m_aiButton;
    [SerializeField]
    private AnimatedTextButton m_cancelButton;
    [Header("Preview")]
    [SerializeField]
    private UIZaapPlayerPreview m_playerPreview;
    [SerializeField]
    private UIZaapPlayerPreview m_opponentPreview;
    [SerializeField]
    private RectTransform m_playerRoot;
    [SerializeField]
    private RectTransform m_opponentRoot;
    private int m_fightDefinitionId;

    public void Init(int fightDefinitionId)
    {
      this.m_fightDefinitionId = fightDefinitionId;
      this.m_searchOpponentGroup.alpha = 0.0f;
      this.m_playerRoot.anchoredPosition = (Vector2) new Vector3(-2000f, 0.0f, 0.0f);
      this.m_opponentRoot.anchoredPosition = (Vector2) new Vector3(2000f, 0.0f, 0.0f);
      this.m_aiButton.onClick.AddListener(new UnityAction(this.OnAiClic));
      this.m_cancelButton.onClick.AddListener(new UnityAction(this.OnCancel));
    }

    public void OnEnterFinished() => this.onEnterAnimationFinished(this.m_fightDefinitionId, PlayerData.instance.GetCurrentWeaponLevel());

    private void OnCancel()
    {
      Action onCancelRequested = this.onCancelRequested;
      if (onCancelRequested == null)
        return;
      onCancelRequested();
    }

    private void OnAiClic()
    {
      Action forceAiRequested = this.onForceAiRequested;
      if (forceAiRequested == null)
        return;
      forceAiRequested();
    }

    public IEnumerator LoadUI()
    {
      UIZaapPVPLoading uiZaapPvpLoading = this;
      yield return (object) uiZaapPvpLoading.PlayAnimation(uiZaapPvpLoading.m_animationDirector.GetAnimation("Init"));
      yield return (object) uiZaapPvpLoading.m_playerPreview.SetPlayerData(PlayerData.instance);
      yield return (object) uiZaapPvpLoading.PlayAnimation(uiZaapPvpLoading.m_animationDirector.GetAnimation("Open"));
    }

    public IEnumerator CloseUI()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      UIZaapPVPLoading uiZaapPvpLoading = this;
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
      this.\u003C\u003E2__current = (object) uiZaapPvpLoading.PlayAnimation(uiZaapPvpLoading.m_animationDirector.GetAnimation("Close"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public IEnumerator SetOpponent(FightInfo.Types.Player opponent)
    {
      yield return (object) this.m_opponentPreview.SetPlayerData(opponent);
    }

    public IEnumerator GotoVersusAnim()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      UIZaapPVPLoading uiZaapPvpLoading = this;
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
      this.\u003C\u003E2__current = (object) uiZaapPvpLoading.PlayAnimation(uiZaapPvpLoading.m_animationDirector.GetAnimation("OpponentFound"));
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    public void OnGameStarted() => this.m_cancelButton.interactable = false;

    public void SimulateCancelClic() => InputUtility.SimulateClickOn((Selectable) this.m_cancelButton);
  }
}
