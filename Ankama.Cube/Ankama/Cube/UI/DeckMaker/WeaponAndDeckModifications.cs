// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.DeckMaker.WeaponAndDeckModifications
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.UI.DeckMaker
{
  public class WeaponAndDeckModifications
  {
    private int? m_selectedWeapon;
    private readonly Dictionary<int, int> m_selectedDecksPerWeapon = new Dictionary<int, int>();

    public event Action OnSelectedWeaponUpdated;

    public event Action OnSelectedDecksUpdated;

    public bool hasModifications => this.m_selectedWeapon.HasValue || this.m_selectedDecksPerWeapon.Count != 0;

    public int? selectedWeapon => this.m_selectedWeapon;

    public Dictionary<int, int> selectedDecksPerWeapon => this.m_selectedDecksPerWeapon;

    public void Setup()
    {
      PlayerData.instance.OnSelectedWeaponUpdated += new Action(this.OnSelectedWeaponUpdate);
      PlayerData.instance.OnSelectedDecksUpdated += new Action(this.OnSelectedDecksUpdate);
    }

    public void Unregister()
    {
      PlayerData.instance.OnSelectedWeaponUpdated -= new Action(this.OnSelectedWeaponUpdate);
      PlayerData.instance.OnSelectedDecksUpdated -= new Action(this.OnSelectedDecksUpdate);
    }

    private void OnSelectedWeaponUpdate()
    {
      this.m_selectedWeapon = new int?();
      Action selectedWeaponUpdated = this.OnSelectedWeaponUpdated;
      if (selectedWeaponUpdated == null)
        return;
      selectedWeaponUpdated();
    }

    public void SetSelectedWeapon(int weapon)
    {
      this.m_selectedWeapon = PlayerData.instance.GetCurrentWeapon() != weapon ? new int?(weapon) : new int?();
      Action selectedWeaponUpdated = this.OnSelectedWeaponUpdated;
      if (selectedWeaponUpdated == null)
        return;
      selectedWeaponUpdated();
    }

    public int GetSelectedWeapon() => this.m_selectedWeapon ?? PlayerData.instance.GetCurrentWeapon();

    private void OnSelectedDecksUpdate()
    {
      this.m_selectedDecksPerWeapon.Remove(this.GetSelectedWeapon());
      Action selectedDecksUpdated = this.OnSelectedDecksUpdated;
      if (selectedDecksUpdated == null)
        return;
      selectedDecksUpdated();
    }

    public void SetSelectedDeckForWeapon(int weapon, int deck)
    {
      if (PlayerData.instance.GetSelectedDeckByWeapon(weapon) == deck)
        this.m_selectedDecksPerWeapon.Remove(weapon);
      else
        this.m_selectedDecksPerWeapon[weapon] = deck;
      Action selectedDecksUpdated = this.OnSelectedDecksUpdated;
      if (selectedDecksUpdated == null)
        return;
      selectedDecksUpdated();
    }

    public int GetSelectedDeckForWeapon(int weapon)
    {
      int num;
      return this.m_selectedDecksPerWeapon.TryGetValue(weapon, out num) ? num : PlayerData.instance.GetSelectedDeckByWeapon(weapon);
    }
  }
}
