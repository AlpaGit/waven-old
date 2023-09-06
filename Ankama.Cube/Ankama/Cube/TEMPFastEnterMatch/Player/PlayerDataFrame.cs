// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.TEMPFastEnterMatch.Player.PlayerDataFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Network;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Utilities;
using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.TEMPFastEnterMatch.Player
{
  public class PlayerDataFrame : CubeMessageFrame
  {
    public event PlayerAccountLoaded OnPlayerAccountLoaded;

    public PlayerDataFrame() => this.WhenReceiveEnqueue<PlayerDataEvent>(new Action<PlayerDataEvent>(this.OnPlayerDataEvent));

    private void OnPlayerDataEvent(PlayerDataEvent msg)
    {
      if (msg.Account != null)
      {
        Log.Info("Player account loaded : " + msg.Account.NickName, 26, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\TEMPFastEnterMatch\\Player\\PlayerDataFrame.cs");
        PlayerDataEvent.Types.OccupationData occupation = msg.Occupation;
        bool pendingFightFound = occupation != null && occupation.InFight;
        PlayerData.Init((long) msg.Account.Hash, msg.Account.NickName, msg.Account.AccountType, msg.Account.Admin, pendingFightFound);
        PlayerAccountLoaded playerAccountLoaded = this.OnPlayerAccountLoaded;
        if (playerAccountLoaded != null)
          playerAccountLoaded(pendingFightFound);
      }
      if (msg.Decks != null)
      {
        PlayerData instance = PlayerData.instance;
        instance.SetAllDecks((IEnumerable<DeckInfo>) msg.Decks.CustomDecks);
        foreach (KeyValuePair<int, int> selectedDeck in msg.Decks.SelectedDecks)
          instance.AddOrUpdateSelectedDeck(selectedDeck.Key, selectedDeck.Value);
      }
      else if (msg.DecksUpdates != null)
      {
        PlayerData instance = PlayerData.instance;
        foreach (DeckInfo info in msg.DecksUpdates.DeckUpdated)
          instance.AddOrUpdateDeck(info);
        foreach (int id in msg.DecksUpdates.DeckRemovedId)
          instance.RemoveDeck(id);
        foreach (PlayerDataEvent.Types.DeckIncrementalUpdateData.Types.SelectedDeckPerWeapon selectedDeckPerWeapon in msg.DecksUpdates.DeckSelectionsUpdated)
        {
          if (selectedDeckPerWeapon.DeckId.HasValue)
            PlayerData.instance.AddOrUpdateSelectedDeck(selectedDeckPerWeapon.WeaponId, selectedDeckPerWeapon.DeckId.Value);
          else
            PlayerData.instance.RemoveSelectedDeck(selectedDeckPerWeapon.WeaponId);
        }
      }
      if (msg.CompanionData != null)
        PlayerData.instance.UpdateCompanionData(msg.CompanionData.Companions);
      if (msg.WeaponLevelsData != null)
        PlayerData.instance.UpdateWeaponsLevelsData(msg.WeaponLevelsData.WeaponLevels);
      if (msg.SelectedWeaponsData != null)
        PlayerData.instance.UpdateSelectedWeaponsData(msg.SelectedWeaponsData.SelectedWeapons);
      if (msg.Hero == null)
        return;
      PlayerData.instance.UpdatePlayerHeroInfo(msg.Hero.Info);
    }

    public void GetPlayerInitialData() => this.m_connection.Write((IMessage) new GetPlayerDataCmd()
    {
      Occupation = true,
      AccountData = true,
      Decks = true,
      HeroData = true,
      Companions = true,
      Weapons = true
    });

    public override void Dispose()
    {
      PlayerData.Clean();
      base.Dispose();
    }
  }
}
