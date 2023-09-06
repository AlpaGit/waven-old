// Decompiled with JetBrains decompiler
// Type: WorldUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube;
using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorldUI : AbstractUI
{
  private static WorldUI s_instance;
  [SerializeField]
  private Button m_pvpButton;
  [SerializeField]
  private Button m_gotoTofuButton;
  [SerializeField]
  private Button m_gotoGardenButton;
  [SerializeField]
  private Button m_gotoHomeButton;
  [SerializeField]
  private Button m_gotoCityButton;
  [SerializeField]
  private Button m_squadMakerButton;
  [SerializeField]
  private CustomDropdown m_godChoiceDropdown;
  [SerializeField]
  private Button m_disconnectButton;
  [SerializeField]
  private TextField m_currentDeckName;
  public Action onPvp;
  public Action onGotoDungeon;
  public Action onGotoGarden;
  public Action onGotoHome;
  public Action onGotoCity;
  public Action onDisconnect;
  public Action onDeckMaker;
  private readonly List<God> m_playableGods = new List<God>();

  public event Action<God> onGodSelectedChanged;

  [UsedImplicitly]
  protected override void Awake()
  {
    WorldUI.s_instance = this;
    this.m_pvpButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onPvp = this.onPvp;
      if (onPvp == null)
        return;
      onPvp();
    }));
    this.m_gotoTofuButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onGotoDungeon = this.onGotoDungeon;
      if (onGotoDungeon != null)
        onGotoDungeon();
      this.RoomSelected(this.m_gotoTofuButton);
    }));
    this.m_gotoGardenButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onGotoGarden = this.onGotoGarden;
      if (onGotoGarden != null)
        onGotoGarden();
      this.RoomSelected(this.m_gotoGardenButton);
    }));
    this.m_gotoHomeButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onGotoHome = this.onGotoHome;
      if (onGotoHome != null)
        onGotoHome();
      this.RoomSelected(this.m_gotoHomeButton);
    }));
    this.m_gotoCityButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onGotoCity = this.onGotoCity;
      if (onGotoCity != null)
        onGotoCity();
      this.RoomSelected(this.m_gotoCityButton);
    }));
    this.m_disconnectButton.onClick.AddListener(new UnityAction(this.OnDisconnect));
    this.m_squadMakerButton.onClick.AddListener((UnityAction) (() =>
    {
      Action onDeckMaker = this.onDeckMaker;
      if (onDeckMaker == null)
        return;
      onDeckMaker();
    }));
    this.m_gotoHomeButton.interactable = false;
    this.m_gotoTofuButton.interactable = true;
    this.m_gotoGardenButton.interactable = true;
    this.m_gotoCityButton.interactable = true;
    this.InitGodsChoice();
  }

  private void OnDisconnect()
  {
    Action onDisconnect = this.onDisconnect;
    if (onDisconnect == null)
      return;
    onDisconnect();
  }

  private void RoomSelected(Button button)
  {
    this.m_gotoTofuButton.interactable = (UnityEngine.Object) this.m_gotoTofuButton != (UnityEngine.Object) button;
    this.m_gotoGardenButton.interactable = (UnityEngine.Object) this.m_gotoGardenButton != (UnityEngine.Object) button;
    this.m_gotoHomeButton.interactable = (UnityEngine.Object) this.m_gotoHomeButton != (UnityEngine.Object) button;
    this.m_gotoCityButton.interactable = (UnityEngine.Object) this.m_gotoCityButton != (UnityEngine.Object) button;
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    WorldUI.s_instance = (WorldUI) null;
  }

  private void InitGodsChoice()
  {
    this.m_playableGods.Clear();
    List<string> options = new List<string>();
    God god = PlayerData.instance.god;
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
    this.m_godChoiceDropdown.ClearOptions();
    this.m_godChoiceDropdown.AddOptions(options);
    this.m_godChoiceDropdown.value = num1 >= 0 ? num1 : 0;
    this.m_godChoiceDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnGodSelected));
  }

  public void SetCurrentDeck(DeckInfo deckInfo)
  {
    this.m_pvpButton.interactable = deckInfo != null;
    if (deckInfo == null)
      this.m_currentDeckName.SetText(64793);
    else
      this.m_currentDeckName.SetText(66030, (IValueProvider) new IndexedValueProvider(new string[1]
      {
        deckInfo.Name
      }));
  }

  private void OnGodSelected(int god)
  {
    Action<God> godSelectedChanged = this.onGodSelectedChanged;
    if (godSelectedChanged == null)
      return;
    godSelectedChanged(this.m_playableGods[god]);
  }

  public void OnPlayerHeroInfoUpdated() => this.InitGodsChoice();
}
