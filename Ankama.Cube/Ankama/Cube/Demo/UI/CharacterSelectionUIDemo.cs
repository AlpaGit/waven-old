// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.CharacterSelectionUIDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Utilities;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class CharacterSelectionUIDemo : BaseFightSelectionUI
  {
    [SerializeField]
    private Button m_selectButton;
    [SerializeField]
    private CharacterPanelList m_characterList;
    [SerializeField]
    private CharacterPanel m_characterPrefab;
    [SerializeField]
    private SlidingAnimUI m_buttonSlidingAnim;
    [SerializeField]
    private DemoData m_fakeData;
    public Action<int> onSelect;
    private List<Tuple<SquadDefinition, SquadFakeData>> m_displayedSquad;

    private void Start()
    {
      this.m_characterList.elementWidth = (int) ((RectTransform) this.m_characterPrefab.transform).sizeDelta.x;
      this.m_characterPrefab.gameObject.SetActive(false);
      this.m_selectButton.onClick.AddListener(new UnityAction(this.OnSelectClick));
      CharacterPanelList characterList = this.m_characterList;
      characterList.onElementSelected = characterList.onElementSelected + (Action<int>) (s => this.UpdateInactivityTime());
    }

    public void Init(God god)
    {
      List<Tuple<SquadDefinition, SquadFakeData>> displayedSquads = this.GetDisplayedSquads(god);
      for (int index = 0; index < this.m_fakeData.squadNbElementLockedBefore; ++index)
      {
        CharacterPanel characterPanel = this.CreateCharacterPanel();
        characterPanel.Set((SquadDefinition) null, (SquadFakeData) null);
        this.m_characterList.Add(characterPanel);
      }
      int count = displayedSquads.Count;
      for (int index = 0; index < count; ++index)
      {
        Tuple<SquadDefinition, SquadFakeData> tuple = displayedSquads[index];
        CharacterPanel characterPanel = this.CreateCharacterPanel();
        characterPanel.Set(tuple.Item1, tuple.Item2);
        this.m_characterList.Add(characterPanel);
      }
      for (int index = 0; index < this.m_fakeData.squadNbElementLockedAfter; ++index)
      {
        CharacterPanel characterPanel = this.CreateCharacterPanel();
        characterPanel.Set((SquadDefinition) null, (SquadFakeData) null);
        this.m_characterList.Add(characterPanel);
      }
      this.m_characterList.lockedLeft = this.m_fakeData.squadNbElementLockedBefore;
      this.m_characterList.lockedright = this.m_fakeData.squadNbElementLockedAfter;
      this.m_characterList.SetSelectedIndex(this.GetSelectedIndex(displayedSquads), false, false);
      this.m_selectButton.interactable = displayedSquads.Count > 0;
      this.m_displayedSquad = displayedSquads;
    }

    private List<Tuple<SquadDefinition, SquadFakeData>> GetDisplayedSquads(God god)
    {
      List<Tuple<SquadDefinition, SquadFakeData>> displayedSquads = new List<Tuple<SquadDefinition, SquadFakeData>>();
      SquadFakeData[] squads = this.m_fakeData.squads;
      int length = squads.Length;
      for (int index = 0; index < length; ++index)
      {
        SquadFakeData squadFakeData = squads[index];
        SquadDefinition squadDefinition;
        if (!RuntimeData.squadDefinitions.TryGetValue(squadFakeData.id, out squadDefinition))
        {
          Log.Error(string.Format("Cannot find squad definition with id {0}", (object) squadFakeData.id), 91, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\UI\\CharacterSelection\\CharacterSelectionUIDemo.cs");
        }
        else
        {
          WeaponDefinition weaponDefinition;
          if (!RuntimeData.weaponDefinitions.TryGetValue(squadDefinition.weapon.value, out weaponDefinition))
            Log.Error(string.Format("Cannot find weapon definition with id {0}", (object) squadDefinition.weapon.value), 98, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\UI\\CharacterSelection\\CharacterSelectionUIDemo.cs");
          else if (weaponDefinition.god == god)
            displayedSquads.Add(new Tuple<SquadDefinition, SquadFakeData>(squadDefinition, squadFakeData));
        }
      }
      return displayedSquads;
    }

    private int GetSelectedIndex(
      List<Tuple<SquadDefinition, SquadFakeData>> displayedList)
    {
      int num = -1;
      if (!this.m_fakeData.resetSelection)
      {
        int currentDeckId = PlayerData.instance.currentDeckId;
        SquadDefinition squadDefinition;
        if (currentDeckId < 0 && RuntimeData.squadDefinitions.TryGetValue(-currentDeckId, out squadDefinition))
        {
          int count = displayedList.Count;
          for (int index = 0; index < count; ++index)
          {
            if (displayedList[index].Item1.id == squadDefinition.id)
            {
              num = index;
              break;
            }
          }
        }
      }
      if (num < 0)
        num = 0;
      return num + this.m_fakeData.squadNbElementLockedBefore;
    }

    private CharacterPanel CreateCharacterPanel()
    {
      CharacterPanel characterPanel = UnityEngine.Object.Instantiate<CharacterPanel>(this.m_characterPrefab);
      characterPanel.gameObject.SetActive(true);
      return characterPanel;
    }

    public void SimulateRightArrowClick() => InputUtility.SimulateClickOn((Selectable) this.m_characterList.rightButton);

    public void SimulateLeftArrowClick() => InputUtility.SimulateClickOn((Selectable) this.m_characterList.leftButton);

    public void SimulateSelectClick() => InputUtility.SimulateClickOn((Selectable) this.m_selectButton);

    private void OnSelectClick()
    {
      int id = this.m_displayedSquad[this.m_characterList.selectedIndex - this.m_fakeData.squadNbElementLockedBefore].Item2.id;
      Action<int> onSelect = this.onSelect;
      if (onSelect == null)
        return;
      onSelect(id);
    }

    public override IEnumerator OpenFrom(SlidingSide side)
    {
      CharacterSelectionUIDemo characterSelectionUiDemo = this;
      Sequence buttonSequence = characterSelectionUiDemo.m_buttonSlidingAnim.PlayAnim(true, side, side == SlidingSide.Left);
      Sequence elemSequence = characterSelectionUiDemo.m_characterList.TransitionAnim(true, side);
      characterSelectionUiDemo.m_openDirector.time = 0.0;
      characterSelectionUiDemo.m_openDirector.Play();
      while (elemSequence.IsActive() || buttonSequence.IsActive() || characterSelectionUiDemo.m_openDirector.playableGraph.IsValid() && !characterSelectionUiDemo.m_openDirector.playableGraph.IsDone())
        yield return (object) null;
    }

    public override IEnumerator CloseTo(SlidingSide side)
    {
      CharacterSelectionUIDemo characterSelectionUiDemo = this;
      Sequence buttonSequence = characterSelectionUiDemo.m_buttonSlidingAnim.PlayAnim(false, side, side == SlidingSide.Right);
      Sequence elemSequence = characterSelectionUiDemo.m_characterList.TransitionAnim(false, side);
      characterSelectionUiDemo.m_closeDirector.time = 0.0;
      characterSelectionUiDemo.m_closeDirector.Play();
      while (elemSequence.IsActive() || buttonSequence.IsActive() || characterSelectionUiDemo.m_closeDirector.playableGraph.IsValid() && !characterSelectionUiDemo.m_closeDirector.playableGraph.IsDone())
        yield return (object) null;
    }
  }
}
