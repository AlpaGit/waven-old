// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Player.PlayerData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Utilities;
using DataEditor;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.TEMPFastEnterMatch.Player
{
  public class PlayerData
  {
    private static bool s_initialized;
    private static PlayerData s_instance;
    private readonly Dictionary<int, DeckInfo> m_decks = new Dictionary<int, DeckInfo>();
    private readonly Dictionary<int, int> m_selectedDecks = new Dictionary<int, int>();
    private readonly Dictionary<int, int> m_selectedWeapons = new Dictionary<int, int>();
    private long m_hash;
    private string m_nickName;
    private string m_accountType;
    private bool m_admin;
    private God m_god;
    private Gender m_gender;
    private Id<CharacterSkinDefinition> m_skin;
    private readonly InventoryWithLevel m_weaponInventory;
    private readonly HashSet<int> m_companionInventory = new HashSet<int>();
    private int m_havreMapSceneIndex;
    private int m_currentDeckId;

    public static event PlayerData.PlayerDataInitialized OnPlayerDataInitialized;

    public event Action<string> OnNicknameUpdated;

    public event Action OnPlayerHeroInfoUpdated;

    public event Action OnPlayerHeroSkinChanged;

    public event Action OnPlayerGodChanged;

    public event Action OnDeckListUpdated;

    public event Action OnCompanionsUpdated;

    public event Action<int> OnRequestVisualWeaponUpdated;

    public event Action OnSelectedWeaponUpdated;

    public event Action OnWeaponsLevelsUpdated;

    public event Action OnSelectedWeaponsUpdated;

    public event Action OnSelectedDecksUpdated;

    public event Action OnSelectedDeckUpdated;

    public static void Init(
      long hash,
      string nickName,
      string accountType = "ANKAMA",
      bool admin = false,
      bool pendingFightFound = false)
    {
      if (!PlayerData.s_initialized)
      {
        PlayerData.s_instance = new PlayerData(hash, nickName, accountType, admin);
        PlayerData.s_instance.m_havreMapSceneIndex = UnityEngine.Random.Range(0, 5);
        Log.Info(string.Format("PlayerData Initialized for {0}. pending fight found: {1}", (object) nickName, (object) pendingFightFound), 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Player\\PlayerData.cs");
        PlayerData.PlayerDataInitialized playerDataInitialized = PlayerData.OnPlayerDataInitialized;
        if (playerDataInitialized != null)
          playerDataInitialized(pendingFightFound);
        PlayerData.s_initialized = true;
      }
      else
        Log.Error("PlayerData already Initialized for " + nickName, 49, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Player\\PlayerData.cs");
    }

    public static void Clean()
    {
      PlayerData.s_initialized = false;
      PlayerData.s_instance = (PlayerData) null;
    }

    public static PlayerData instance => PlayerData.s_instance;

    public long hash => this.m_hash;

    public string nickName => this.m_nickName;

    public string accountType => this.m_accountType;

    public bool admin => this.m_admin;

    public God god => this.m_god;

    public Gender gender => this.m_gender;

    public Id<CharacterSkinDefinition> Skin => this.m_skin;

    public IInventoryWithLevel weaponInventory => (IInventoryWithLevel) this.m_weaponInventory;

    public HashSet<int> companionInventory => this.m_companionInventory;

    public int havreMapSceneIndex => this.m_havreMapSceneIndex;

    private PlayerData(long hash, string nickName, string accountType = "ANKAMA", bool admin = false)
    {
      this.m_hash = hash;
      this.m_nickName = nickName;
      this.m_accountType = accountType;
      this.m_admin = admin;
      this.m_weaponInventory = new InventoryWithLevel(new int?(1));
    }

    public void UpdateNickname(string nickname)
    {
      this.m_nickName = nickname;
      Action<string> onNicknameUpdated = this.OnNicknameUpdated;
      if (onNicknameUpdated == null)
        return;
      onNicknameUpdated(this.m_nickName);
    }

    public void SetAllDecks(IEnumerable<DeckInfo> decksInfos)
    {
      this.m_decks.Clear();
      foreach (DeckInfo decksInfo in decksInfos)
      {
        int? id = decksInfo.Id;
        if (id.HasValue)
        {
          id = decksInfo.Id;
          this.m_decks.Add(id.Value, decksInfo.EnsureDataConsistency());
        }
      }
      Action onDeckListUpdated = this.OnDeckListUpdated;
      if (onDeckListUpdated == null)
        return;
      onDeckListUpdated();
    }

    public void AddOrUpdateDeck(DeckInfo info)
    {
      if (!info.Id.HasValue)
      {
        Log.Error("Unable to Add Or Update deck: no Id provided", (int) sbyte.MaxValue, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Player\\PlayerData.cs");
      }
      else
      {
        int key = info.Id.Value;
        DeckInfo deckInfo = info.EnsureDataConsistency();
        if (this.m_decks.ContainsKey(key))
          this.m_decks[key] = deckInfo;
        else
          this.m_decks.Add(key, deckInfo);
        Action onDeckListUpdated = this.OnDeckListUpdated;
        if (onDeckListUpdated == null)
          return;
        onDeckListUpdated();
      }
    }

    public void RemoveDeck(int id)
    {
      this.m_decks.Remove(id);
      Action onDeckListUpdated = this.OnDeckListUpdated;
      if (onDeckListUpdated == null)
        return;
      onDeckListUpdated();
    }

    public IEnumerable<DeckInfo> GetDecks() => (IEnumerable<DeckInfo>) this.m_decks.Values;

    public bool TryGetDeckById(int id, out DeckInfo deckInfo)
    {
      if (id >= 0)
        return this.m_decks.TryGetValue(id, out deckInfo);
      foreach (KeyValuePair<int, WeaponDefinition> weaponDefinition in RuntimeData.weaponDefinitions)
      {
        SquadDefinition definition;
        if (!(weaponDefinition.Value.defaultDeck == (Id<SquadDefinition>) null) && RuntimeData.squadDefinitions.TryGetValue(weaponDefinition.Value.defaultDeck.value, out definition) && definition.id == -id)
        {
          DeckInfo deckInfo1 = definition.ToDeckInfo();
          deckInfo1.Id = new int?(id);
          deckInfo = deckInfo1;
          return true;
        }
      }
      deckInfo = (DeckInfo) null;
      return false;
    }

    public void UpdatePlayerHeroInfo(HeroInfo info)
    {
      int num = this.m_skin != (Id<CharacterSkinDefinition>) null ? this.m_skin.value : -1;
      God god = this.m_god;
      this.m_god = (God) info.God;
      this.m_gender = (Gender) info.Gender;
      this.m_skin = new Id<CharacterSkinDefinition>(info.Skin);
      int skin = info.Skin;
      if (num != skin)
      {
        Action playerHeroSkinChanged = this.OnPlayerHeroSkinChanged;
        if (playerHeroSkinChanged != null)
          playerHeroSkinChanged();
      }
      if (god != this.m_god)
      {
        Action playerGodChanged = this.OnPlayerGodChanged;
        if (playerGodChanged != null)
          playerGodChanged();
      }
      Action playerHeroInfoUpdated = this.OnPlayerHeroInfoUpdated;
      if (playerHeroInfoUpdated != null)
        playerHeroInfoUpdated();
      this.RefreshCurrentDeck();
    }

    public int GetCurrentWeapon()
    {
      int num;
      return this.m_selectedWeapons.TryGetValue((int) this.god, out num) ? num : 0;
    }

    public void AddOrUpdateSelectedDeck(int weaponId, int deckId)
    {
      if (this.m_selectedDecks.ContainsKey(weaponId))
        this.m_selectedDecks[weaponId] = deckId;
      else
        this.m_selectedDecks.Add(weaponId, deckId);
      Action selectedDecksUpdated = this.OnSelectedDecksUpdated;
      if (selectedDecksUpdated != null)
        selectedDecksUpdated();
      this.RefreshCurrentDeck();
    }

    public void RemoveSelectedDeck(int weaponId)
    {
      this.m_selectedDecks.Remove(weaponId);
      Action selectedDecksUpdated = this.OnSelectedDecksUpdated;
      if (selectedDecksUpdated != null)
        selectedDecksUpdated();
      this.RefreshCurrentDeck();
    }

    public int GetSelectedDeckByWeapon(int weaponId)
    {
      int selectedDeckByWeapon;
      if (this.m_selectedDecks.TryGetValue(weaponId, out selectedDeckByWeapon))
        return selectedDeckByWeapon;
      WeaponDefinition weaponDefinition;
      if (!RuntimeData.weaponDefinitions.TryGetValue(weaponId, out weaponDefinition))
        return 0;
      Id<SquadDefinition> defaultDeck = weaponDefinition.defaultDeck;
      return (object) defaultDeck == null ? 0 : -defaultDeck.value;
    }

    private void RefreshCurrentDeck()
    {
      int weaponId;
      if (!this.m_selectedWeapons.TryGetValue((int) this.m_god, out weaponId))
        return;
      int selectedDeckByWeapon = this.GetSelectedDeckByWeapon(weaponId);
      int num = selectedDeckByWeapon != this.m_currentDeckId ? 1 : 0;
      this.m_currentDeckId = selectedDeckByWeapon;
      if (num == 0)
        return;
      Action selectedDeckUpdated = this.OnSelectedDeckUpdated;
      if (selectedDeckUpdated == null)
        return;
      selectedDeckUpdated();
    }

    public int currentDeckId => this.m_currentDeckId;

    public void UpdateWeaponsLevelsData(MapField<int, int> levels)
    {
      this.m_weaponInventory.UpdateAllLevels((IDictionary<int, int>) levels);
      Action weaponsLevelsUpdated = this.OnWeaponsLevelsUpdated;
      if (weaponsLevelsUpdated == null)
        return;
      weaponsLevelsUpdated();
    }

    public void UpdateSelectedWeaponsData(MapField<int, int> selectedWeapons)
    {
      this.m_selectedWeapons.Clear();
      foreach (KeyValuePair<int, int> selectedWeapon in selectedWeapons)
        this.m_selectedWeapons.Add(selectedWeapon.Key, selectedWeapon.Value);
      Action selectedWeaponUpdated = this.OnSelectedWeaponUpdated;
      if (selectedWeaponUpdated != null)
        selectedWeaponUpdated();
      this.RefreshCurrentDeck();
    }

    public void UpdateCompanionData(RepeatedField<int> companions)
    {
      this.m_companionInventory.Clear();
      int index = 0;
      for (int count = companions.Count; index < count; ++index)
        this.companionInventory.Add(companions[index]);
      Action companionsUpdated = this.OnCompanionsUpdated;
      if (companionsUpdated == null)
        return;
      companionsUpdated();
    }

    public int? GetCurrentWeaponLevel()
    {
      int level;
      return this.weaponInventory.TryGetLevel(this.GetCurrentWeapon(), out level) ? new int?(level) : new int?();
    }

    public void RequestWeaponChange(int weaponId)
    {
      Action<int> visualWeaponUpdated = this.OnRequestVisualWeaponUpdated;
      if (visualWeaponUpdated == null)
        return;
      visualWeaponUpdated(weaponId);
    }

    public delegate void PlayerDataInitialized(bool pendingFightFound);
  }
}
