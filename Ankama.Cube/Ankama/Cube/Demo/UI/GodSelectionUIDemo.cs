// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.GodSelectionUIDemo
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
  public class GodSelectionUIDemo : BaseFightSelectionUI
  {
    [SerializeField]
    private Button m_selectButton;
    [SerializeField]
    private GodPanelList m_godList;
    [SerializeField]
    private GodPanel m_godPanelPrefab;
    [SerializeField]
    private SlidingAnimUI m_buttonSlidingAnim;
    [SerializeField]
    private DemoData m_fakeData;
    public Action<God> onSelect;

    private void Start()
    {
      this.m_godList.elementWidth = (int) ((RectTransform) this.m_godPanelPrefab.transform).sizeDelta.x;
      this.m_godPanelPrefab.gameObject.SetActive(false);
      this.m_selectButton.onClick.AddListener(new UnityAction(this.OnSelectClick));
      GodPanelList godList = this.m_godList;
      godList.onElementSelected = godList.onElementSelected + new Action<int>(this.OnGodSelected);
    }

    public void Init()
    {
      List<Tuple<GodDefinition, GodFakeData>> displayedGods = this.GetDisplayedGods();
      int count = displayedGods.Count;
      for (int index = 0; index < this.m_fakeData.godNbElementLockedBefore; ++index)
      {
        GodPanel godPanel = this.CreateGodPanel();
        godPanel.Set((GodDefinition) null, (GodFakeData) null);
        this.m_godList.Add(godPanel);
      }
      for (int index = 0; index < count; ++index)
      {
        Tuple<GodDefinition, GodFakeData> tuple = displayedGods[index];
        if (!tuple.Item2.locked)
        {
          GodPanel godPanel = this.CreateGodPanel();
          godPanel.Set(tuple.Item1, tuple.Item2);
          this.m_godList.Add(godPanel);
        }
      }
      int num = 0;
      for (int index = 0; index < count; ++index)
      {
        if (displayedGods[index].Item2.locked)
        {
          GodPanel godPanel = this.CreateGodPanel();
          godPanel.Set((GodDefinition) null, (GodFakeData) null);
          this.m_godList.Add(godPanel);
          ++num;
        }
      }
      for (int index = 0; index < this.m_fakeData.godNbElementLockedAfter; ++index)
      {
        GodPanel godPanel = this.CreateGodPanel();
        godPanel.Set((GodDefinition) null, (GodFakeData) null);
        this.m_godList.Add(godPanel);
        ++num;
      }
      this.m_godList.lockedLeft = this.m_fakeData.godNbElementLockedBefore;
      this.m_godList.lockedright = num;
      this.m_godList.SetSelectedIndex(this.GetSelectedIndex(displayedGods), false, false);
      this.m_selectButton.interactable = displayedGods.Count > 0;
    }

    private List<Tuple<GodDefinition, GodFakeData>> GetDisplayedGods()
    {
      List<Tuple<GodDefinition, GodFakeData>> displayedGods = new List<Tuple<GodDefinition, GodFakeData>>();
      GodFakeData[] gods = this.m_fakeData.gods;
      int length = gods.Length;
      for (int index = 0; index < length; ++index)
      {
        GodFakeData godFakeData = gods[index];
        GodDefinition godDefinition;
        if (!RuntimeData.godDefinitions.TryGetValue(godFakeData.god, out godDefinition))
          Log.Error(string.Format("Cannot find god definition with family {0}", (object) godFakeData.god), 105, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Demo\\Code\\UI\\GodSelection\\GodSelectionUIDemo.cs");
        else
          displayedGods.Add(new Tuple<GodDefinition, GodFakeData>(godDefinition, godFakeData));
      }
      return displayedGods;
    }

    private int GetSelectedIndex(
      List<Tuple<GodDefinition, GodFakeData>> displayedList)
    {
      int num = -1;
      if (!this.m_fakeData.resetSelection)
      {
        int currentDeckId = PlayerData.instance.currentDeckId;
        SquadDefinition squadDefinition;
        if (currentDeckId < 0 && RuntimeData.squadDefinitions.TryGetValue(currentDeckId, out squadDefinition))
        {
          WeaponDefinition weaponDefinition = RuntimeData.weaponDefinitions[squadDefinition.weapon.value];
          GodDefinition godDefinition = RuntimeData.godDefinitions[weaponDefinition.god];
          int count = displayedList.Count;
          for (int index = 0; index < count; ++index)
          {
            if (displayedList[index].Item1.god == godDefinition.god)
            {
              num = index;
              break;
            }
          }
        }
      }
      if (num < 0)
        num = 0;
      return num + this.m_fakeData.godNbElementLockedBefore;
    }

    private GodPanel CreateGodPanel()
    {
      GodPanel godPanel = UnityEngine.Object.Instantiate<GodPanel>(this.m_godPanelPrefab);
      godPanel.gameObject.SetActive(true);
      return godPanel;
    }

    public void SimulateRightArrowClick() => InputUtility.SimulateClickOn((Selectable) this.m_godList.rightButton);

    public void SimulateLeftArrowClick() => InputUtility.SimulateClickOn((Selectable) this.m_godList.leftButton);

    public void SimulateSelectClick() => InputUtility.SimulateClickOn((Selectable) this.m_selectButton);

    private void OnGodSelected(int s) => this.UpdateInactivityTime();

    private void OnSelectClick()
    {
      God god = this.m_fakeData.gods[this.m_godList.selectedIndex - this.m_fakeData.godNbElementLockedBefore].god;
      Action<God> onSelect = this.onSelect;
      if (onSelect == null)
        return;
      onSelect(god);
    }

    public override IEnumerator OpenFrom(SlidingSide side)
    {
      GodSelectionUIDemo godSelectionUiDemo = this;
      Sequence buttonSequence = godSelectionUiDemo.m_buttonSlidingAnim.PlayAnim(true, side, side == SlidingSide.Left);
      Sequence elemSequence = godSelectionUiDemo.m_godList.TransitionAnim(true, side);
      godSelectionUiDemo.m_openDirector.time = 0.0;
      godSelectionUiDemo.m_openDirector.Play();
      while (elemSequence.IsActive() || buttonSequence.IsActive() || godSelectionUiDemo.m_openDirector.playableGraph.IsValid() && !godSelectionUiDemo.m_openDirector.playableGraph.IsDone())
        yield return (object) null;
    }

    public override IEnumerator CloseTo(SlidingSide side)
    {
      GodSelectionUIDemo godSelectionUiDemo = this;
      Sequence buttonSequence = godSelectionUiDemo.m_buttonSlidingAnim.PlayAnim(false, side, side == SlidingSide.Right);
      Sequence elemSequence = godSelectionUiDemo.m_godList.TransitionAnim(false, side);
      godSelectionUiDemo.m_closeDirector.time = 0.0;
      godSelectionUiDemo.m_closeDirector.Play();
      while (elemSequence.IsActive() || buttonSequence.IsActive() || godSelectionUiDemo.m_closeDirector.playableGraph.IsValid() && !godSelectionUiDemo.m_closeDirector.playableGraph.IsDone())
        yield return (object) null;
    }
  }
}
