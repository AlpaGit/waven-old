// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.DeckSlot
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Data.Levelable;
using Ankama.Cube.Protocols.PlayerProtocol;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.UI.DeckMaker
{
  public class DeckSlot
  {
    private DeckInfo m_deckInfo;
    private readonly int m_slotId;
    private bool m_preconstructed;
    private static int s_slotId;

    public event Action<DeckSlot> OnModification;

    public DeckInfo DeckInfo => this.m_deckInfo;

    public bool HasDeckInfo => this.m_deckInfo != null;

    public bool Preconstructed => this.m_preconstructed;

    public int SlotId => this.m_slotId;

    public int? Id => this.m_deckInfo?.Id;

    public string Name => this.m_deckInfo?.Name;

    public int? Weapon => this.m_deckInfo?.Weapon;

    public int? God => this.m_deckInfo?.God;

    public IList<int> Spells
    {
      get
      {
        DeckInfo deckInfo = this.m_deckInfo;
        return deckInfo == null ? (IList<int>) null : (IList<int>) deckInfo.Spells;
      }
    }

    public IList<int> Companions
    {
      get
      {
        DeckInfo deckInfo = this.m_deckInfo;
        return deckInfo == null ? (IList<int>) null : (IList<int>) deckInfo.Companions;
      }
    }

    public bool isAvailableEmptyDeckSlot => (!this.HasDeckInfo || !this.Id.HasValue) && !this.m_preconstructed;

    public int Level => this.m_deckInfo == null ? 0 : this.m_deckInfo.GetLevel((ILevelProvider) PlayerData.instance.weaponInventory);

    public DeckSlot(DeckInfo deckInfo, bool preconstructed = false)
    {
      this.m_deckInfo = deckInfo;
      this.m_slotId = ++DeckSlot.s_slotId;
      this.m_preconstructed = preconstructed;
    }

    public void SetDeckInfo(DeckInfo info)
    {
      this.m_deckInfo = info;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public void SetName(string name)
    {
      if (this.m_deckInfo == null || string.Equals(name, this.m_deckInfo.Name))
        return;
      this.m_deckInfo.Name = name;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public void SetId(int? id)
    {
      if (this.m_deckInfo == null)
        return;
      int? id1 = this.m_deckInfo.Id;
      int? nullable = id;
      if (id1.GetValueOrDefault() == nullable.GetValueOrDefault() & id1.HasValue == nullable.HasValue)
        return;
      this.m_deckInfo.Id = id;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public void SetWeapon(int? weapon)
    {
      if (this.m_deckInfo == null)
        return;
      int weapon1 = this.m_deckInfo.Weapon;
      int? nullable = weapon;
      int valueOrDefault = nullable.GetValueOrDefault();
      if (weapon1 == valueOrDefault & nullable.HasValue)
        return;
      DeckInfo deckInfo = this.m_deckInfo;
      nullable = weapon;
      int num = nullable ?? 0;
      deckInfo.Weapon = num;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public void SetCompanionAt(int companion, int index)
    {
      if (this.m_deckInfo == null || this.m_deckInfo.Companions[index] == companion)
        return;
      this.m_deckInfo.Companions[index] = companion;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public void SetSpellAt(int spell, int index)
    {
      if (this.m_deckInfo == null || this.m_deckInfo.Spells[index] == spell)
        return;
      this.m_deckInfo.Spells[index] = spell;
      Action<DeckSlot> onModification = this.OnModification;
      if (onModification == null)
        return;
      onModification(this);
    }

    public DeckSlot Clone(bool keepPreconstructed = true)
    {
      DeckSlot deckSlot = new DeckSlot(this.m_deckInfo?.Clone());
      if (keepPreconstructed)
        deckSlot.m_preconstructed = this.m_preconstructed;
      return deckSlot;
    }
  }
}
