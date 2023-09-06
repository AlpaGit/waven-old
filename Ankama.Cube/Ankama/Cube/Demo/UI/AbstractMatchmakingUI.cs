// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.AbstractMatchmakingUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.UI;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Demo.UI
{
  public abstract class AbstractMatchmakingUI : BaseFightSelectionUI
  {
    public const int DefaultPlayerLevel = 6;
    [SerializeField]
    protected PlayerInvitationUI m_playerInvitationPanel;
    [SerializeField]
    protected PlayableDirector m_matchmakingToVersus;
    [SerializeField]
    protected PlayableDirector m_versusToFight;
    [SerializeField]
    protected SlidingAnimUI m_slidingAnim;
    [SerializeField]
    protected DemoData m_fakeData;
    public Action onLaunchMatchmakingClick;
    public Action onCancelMatchmakingClick;

    public PlayerInvitationUI playerInvitation => this.m_playerInvitationPanel;

    public abstract void Init();

    public virtual IEnumerator GotoVersusAnim()
    {
      yield return (object) BaseOpenCloseUI.PlayDirector(this.m_matchmakingToVersus);
    }

    public virtual IEnumerator GotoFightAnim()
    {
      yield return (object) BaseOpenCloseUI.PlayDirector(this.m_versusToFight);
    }

    public override IEnumerator OpenFrom(SlidingSide side)
    {
      AbstractMatchmakingUI abstractMatchmakingUi = this;
      for (int index = 0; index < abstractMatchmakingUi.m_slidingAnim.elements.Count; ++index)
        abstractMatchmakingUi.m_slidingAnim.elements[index].transform.localPosition = Vector3.zero;
      Sequence slidingSequence = abstractMatchmakingUi.m_slidingAnim.PlayAnim(true, side, side == SlidingSide.Left);
      abstractMatchmakingUi.m_openDirector.time = 0.0;
      abstractMatchmakingUi.m_openDirector.Play();
      while (slidingSequence.IsActive() || abstractMatchmakingUi.m_openDirector.playableGraph.IsValid() && !abstractMatchmakingUi.m_openDirector.playableGraph.IsDone())
        yield return (object) null;
    }

    public override IEnumerator CloseTo(SlidingSide side)
    {
      AbstractMatchmakingUI abstractMatchmakingUi = this;
      Sequence slidingSequence = abstractMatchmakingUi.m_slidingAnim.PlayAnim(false, side, side == SlidingSide.Right);
      abstractMatchmakingUi.m_closeDirector.time = 0.0;
      abstractMatchmakingUi.m_closeDirector.Play();
      while (slidingSequence.IsActive() || abstractMatchmakingUi.m_closeDirector.playableGraph.IsValid() && !abstractMatchmakingUi.m_closeDirector.playableGraph.IsDone())
        yield return (object) null;
    }

    protected Tuple<SquadDefinition, SquadFakeData> GetSquadDataByDeckId(int deckId)
    {
      for (int index = 0; index < this.m_fakeData.squads.Length; ++index)
      {
        SquadFakeData squad = this.m_fakeData.squads[index];
        if (squad.id == deckId)
          return new Tuple<SquadDefinition, SquadFakeData>(RuntimeData.squadDefinitions[deckId], squad);
      }
      SquadFakeData squad1 = this.m_fakeData.squads[0];
      return new Tuple<SquadDefinition, SquadFakeData>(RuntimeData.squadDefinitions[squad1.id], squad1);
    }

    protected Tuple<SquadDefinition, SquadFakeData> GetSquadDataByWeaponId(int weaponId)
    {
      for (int index = 0; index < this.m_fakeData.squads.Length; ++index)
      {
        SquadFakeData squad = this.m_fakeData.squads[index];
        SquadDefinition squadDefinition = RuntimeData.squadDefinitions[squad.id];
        if (squadDefinition.weapon.value == weaponId)
          return new Tuple<SquadDefinition, SquadFakeData>(squadDefinition, squad);
      }
      SquadFakeData squad1 = this.m_fakeData.squads[0];
      return new Tuple<SquadDefinition, SquadFakeData>(RuntimeData.squadDefinitions[squad1.id], squad1);
    }
  }
}
