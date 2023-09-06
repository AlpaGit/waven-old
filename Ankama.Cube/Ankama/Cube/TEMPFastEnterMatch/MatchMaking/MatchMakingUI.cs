// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.MatchMaking.MatchMakingUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.TEMPFastEnterMatch.MatchMaking
{
  public class MatchMakingUI : AbstractUI
  {
    [SerializeField]
    private MatchMakingButton m_defaultButton;
    [SerializeField]
    private CustomDropdown m_godChoiceDropdown;
    [SerializeField]
    private CustomDropdown m_weaponsDropdown;
    [SerializeField]
    private CustomDropdown m_decksDropDown;
    [SerializeField]
    private Button m_returnButton;
    [SerializeField]
    private CustomDropdown m_forceLevelDropdown;
    [SerializeField]
    private List<int> m_availableFightDefIds;
    private readonly List<God> m_playableGods = new List<God>();
    private MatchMakingButton m_waitingButton;
    public Action<int, int?> onPlayRequested;
    public Action onForceAiRequested;
    public Action onReturnClicked;
    public Action onCancelRequested;
    private List<DeckInfo> m_deckList;
    private List<WeaponDefinition> m_weaponList;

    public event Action<God> onGodSelectedChanged;

    public event Action<int> onSelectedWeaponChanged;

    public event Action<int> onSelectedDeckChanged;

    private int? ForcedLevel => this.m_forceLevelDropdown.value == 0 ? new int?() : new int?(this.m_forceLevelDropdown.value);

    protected override void Awake()
    {
      this.m_defaultButton.gameObject.SetActive(false);
      foreach (int availableFightDefId in this.m_availableFightDefIds)
      {
        MatchMakingButton btn = UnityEngine.Object.Instantiate<MatchMakingButton>(this.m_defaultButton, this.m_defaultButton.transform.parent);
        btn.fightDefId = availableFightDefId;
        btn.StopWait();
        btn.gameObject.SetActive(true);
        btn.button.onClick.AddListener((UnityAction) (() => this.OnPlayButtonClicked(btn)));
        btn.forceAiBUtton.onClick.AddListener(new UnityAction(this.OnForceVersusAiButtonClicked));
      }
      this.m_returnButton.onClick.AddListener(new UnityAction(this.OnReturnClicked));
      this.m_godChoiceDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnGodSelected));
      this.m_weaponsDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnSelectWeapon));
      this.m_decksDropDown.onValueChanged.AddListener(new UnityAction<int>(this.OnSelectDeck));
      this.InitGodsChoice();
    }

    private void OnForceVersusAiButtonClicked()
    {
      if (!((UnityEngine.Object) this.m_waitingButton != (UnityEngine.Object) null))
        return;
      Action forceAiRequested = this.onForceAiRequested;
      if (forceAiRequested == null)
        return;
      forceAiRequested();
    }

    private void OnPlayButtonClicked(MatchMakingButton btn)
    {
      if ((UnityEngine.Object) this.m_waitingButton != (UnityEngine.Object) null)
      {
        Action onCancelRequested = this.onCancelRequested;
        if (onCancelRequested != null)
          onCancelRequested();
        this.m_waitingButton.StopWait();
        this.m_waitingButton = (MatchMakingButton) null;
      }
      else
      {
        btn.StartWait();
        Action<int, int?> onPlayRequested = this.onPlayRequested;
        if (onPlayRequested != null)
          onPlayRequested(btn.fightDefId, this.ForcedLevel);
        this.m_waitingButton = btn;
      }
    }

    private void OnSelectWeapon(int weaponIndex)
    {
      WeaponDefinition weapon = this.m_weaponList[weaponIndex];
      Action<int> selectedWeaponChanged = this.onSelectedWeaponChanged;
      if (selectedWeaponChanged == null)
        return;
      selectedWeaponChanged(weapon.id);
    }

    private void OnSelectDeck(int deckIndex)
    {
      DeckInfo deck = this.m_deckList[deckIndex];
      Action<int> selectedDeckChanged = this.onSelectedDeckChanged;
      if (selectedDeckChanged == null)
        return;
      selectedDeckChanged(deck.Id ?? 0);
    }

    private void OnGodSelected(int god)
    {
      Action<God> godSelectedChanged = this.onGodSelectedChanged;
      if (godSelectedChanged == null)
        return;
      godSelectedChanged(this.m_playableGods[god]);
    }

    private void OnReturnClicked()
    {
      if ((UnityEngine.Object) this.m_waitingButton != (UnityEngine.Object) null)
      {
        Action onCancelRequested = this.onCancelRequested;
        if (onCancelRequested != null)
          onCancelRequested();
        this.m_waitingButton = (MatchMakingButton) null;
      }
      Action onReturnClicked = this.onReturnClicked;
      if (onReturnClicked == null)
        return;
      onReturnClicked();
    }

    public void OnGodChanged() => this.InitGodsChoice();

    public void OnWeaponChanged(int weaponId) => this.InitWeapons(weaponId);

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
      this.InitWeapons(PlayerData.instance.GetCurrentWeapon());
    }

    private void InitWeapons(int selectedWeaponId)
    {
      this.m_weaponList = new List<WeaponDefinition>();
      this.m_weaponsDropdown.ClearOptions();
      God god = PlayerData.instance.god;
      foreach (int key in (IEnumerable<int>) PlayerData.instance.weaponInventory)
      {
        WeaponDefinition weaponDefinition;
        if (RuntimeData.weaponDefinitions.TryGetValue(key, out weaponDefinition) && weaponDefinition.god == god)
          this.m_weaponList.Add(weaponDefinition);
      }
      int index = this.m_weaponList.FindIndex((Predicate<WeaponDefinition>) (definition => definition.id == selectedWeaponId));
      if ((UnityEngine.Object) this.m_weaponsDropdown != (UnityEngine.Object) null)
      {
        this.m_weaponsDropdown.AddOptions(this.m_weaponList.Select<WeaponDefinition, string>((Func<WeaponDefinition, string>) (sd => sd.displayName)).ToList<string>());
        this.m_weaponsDropdown.value = index;
      }
      this.InitDecks();
    }

    private void InitDecks()
    {
      this.m_deckList = new List<DeckInfo>();
      this.m_decksDropDown.ClearOptions();
      int god = (int) PlayerData.instance.god;
      int currentWeapon = PlayerData.instance.GetCurrentWeapon();
      WeaponDefinition weaponDefinition;
      if (!RuntimeData.weaponDefinitions.TryGetValue(currentWeapon, out weaponDefinition))
        return;
      SquadDefinition definition;
      if (RuntimeData.squadDefinitions.TryGetValue(weaponDefinition.defaultDeck.value, out definition))
      {
        DeckInfo deckInfo = definition.ToDeckInfo();
        deckInfo.Id = new int?(-definition.id);
        this.m_deckList.Add(deckInfo);
      }
      foreach (DeckInfo deck in PlayerData.instance.GetDecks())
      {
        if (deck.Weapon == currentWeapon)
          this.m_deckList.Add(deck);
      }
      int selectedDeckId = PlayerData.instance.GetSelectedDeckByWeapon(weaponDefinition.id);
      int index = this.m_deckList.FindIndex((Predicate<DeckInfo>) (deck =>
      {
        int? id = deck.Id;
        int num = selectedDeckId;
        return id.GetValueOrDefault() == num & id.HasValue;
      }));
      if (!((UnityEngine.Object) this.m_decksDropDown != (UnityEngine.Object) null))
        return;
      this.m_decksDropDown.AddOptions(this.m_deckList.Select<DeckInfo, string>((Func<DeckInfo, string>) (sd => sd.Name)).ToList<string>());
      this.m_decksDropDown.value = index;
    }

    public void SetCurrentWeapon(int weaponId)
    {
      if (!((UnityEngine.Object) this.m_weaponsDropdown != (UnityEngine.Object) null))
        return;
      this.m_weaponsDropdown.value = this.m_weaponList.FindIndex((Predicate<WeaponDefinition>) (weapon => weapon.id == weaponId));
    }

    public void SetCurrentDeck(DeckInfo deckInfo)
    {
      if (!((UnityEngine.Object) this.m_decksDropDown != (UnityEngine.Object) null))
        return;
      this.m_decksDropDown.value = this.m_deckList.IndexOf(deckInfo);
    }
  }
}
