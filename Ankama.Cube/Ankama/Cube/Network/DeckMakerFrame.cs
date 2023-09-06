// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.DeckMakerFrame
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Protocols.PlayerProtocol;
using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Network
{
  public class DeckMakerFrame : CubeMessageFrame
  {
    public Action<RemoveDeckResultEvent> onRemoveConfigResult;
    public Action<SaveDeckResultEvent> onSaveConfigResult;
    public Action<SelectDeckAndWeaponResultEvent> onSelectDeckAndWeaponResult;

    public DeckMakerFrame()
    {
      this.WhenReceiveEnqueue<SaveDeckResultEvent>(new Action<SaveDeckResultEvent>(this.OnSaveConfigResult));
      this.WhenReceiveEnqueue<RemoveDeckResultEvent>(new Action<RemoveDeckResultEvent>(this.OnRemoveConfigResult));
      this.WhenReceiveEnqueue<SelectDeckAndWeaponResultEvent>(new Action<SelectDeckAndWeaponResultEvent>(this.OnDeckAndWeaponChangeResult));
    }

    private void OnRemoveConfigResult(RemoveDeckResultEvent evt)
    {
      Action<RemoveDeckResultEvent> removeConfigResult = this.onRemoveConfigResult;
      if (removeConfigResult == null)
        return;
      removeConfigResult(evt);
    }

    private void OnSaveConfigResult(SaveDeckResultEvent evt)
    {
      Action<SaveDeckResultEvent> saveConfigResult = this.onSaveConfigResult;
      if (saveConfigResult == null)
        return;
      saveConfigResult(evt);
    }

    private void OnDeckAndWeaponChangeResult(SelectDeckAndWeaponResultEvent evt)
    {
      Action<SelectDeckAndWeaponResultEvent> deckAndWeaponResult = this.onSelectDeckAndWeaponResult;
      if (deckAndWeaponResult == null)
        return;
      deckAndWeaponResult(evt);
    }

    public void SendSaveSquadRequest(
      int? id,
      string name,
      Family god,
      int weapon,
      IReadOnlyList<int> companions,
      IReadOnlyList<int> spells)
    {
      DeckInfo deckInfo = new DeckInfo()
      {
        Id = id,
        Name = name,
        Weapon = weapon,
        God = (int) god
      };
      deckInfo.Companions.AddRange((IEnumerable<int>) companions);
      deckInfo.Spells.AddRange((IEnumerable<int>) spells);
      this.m_connection.Write((IMessage) new SaveDeckCmd()
      {
        Info = deckInfo
      });
    }

    public void SendRemoveSquadRequest(int id) => this.m_connection.Write((IMessage) new RemoveDeckCmd()
    {
      Id = id
    });

    public void SendSelectDecksAndWeapon(int? weaponId, Dictionary<int, int> selectedDecksPerWeapon)
    {
      SelectDeckAndWeaponCmd deckAndWeaponCmd = new SelectDeckAndWeaponCmd()
      {
        SelectedWeapon = weaponId
      };
      foreach (KeyValuePair<int, int> keyValuePair in selectedDecksPerWeapon)
      {
        int? nullable = new int?();
        if (keyValuePair.Value >= 0)
          nullable = new int?(keyValuePair.Value);
        deckAndWeaponCmd.SelectedDecks.Add(new SelectDeckInfo()
        {
          WeaponId = keyValuePair.Key,
          DeckId = nullable
        });
      }
      this.m_connection.Write((IMessage) deckAndWeaponCmd);
    }
  }
}
