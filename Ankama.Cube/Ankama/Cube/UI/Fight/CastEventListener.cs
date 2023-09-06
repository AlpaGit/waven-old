// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.CastEventListener
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.DeckMaker;
using System;

namespace Ankama.Cube.UI.Fight
{
  public class CastEventListener
  {
    public event CastEventListener.SpellCastBeginDelegate OnCastSpellDragBegin;

    public event Action<SpellStatusCellRenderer, bool> OnCastSpellDragEnd;

    public event CastEventListener.CompanionCastBeginDelegate OnCastCompanionDragBegin;

    public event Action<CompanionStatusCellRenderer, bool> OnCastCompanionDragEnd;

    public void CastSpellDragBegin(SpellStatusCellRenderer spellStatusData, bool click)
    {
      CastEventListener.SpellCastBeginDelegate castSpellDragBegin = this.OnCastSpellDragBegin;
      if (castSpellDragBegin == null)
        return;
      castSpellDragBegin(spellStatusData, click);
    }

    public void CastSpellDragEnd(SpellStatusCellRenderer spellStatusData, bool onTarget)
    {
      Action<SpellStatusCellRenderer, bool> castSpellDragEnd = this.OnCastSpellDragEnd;
      if (castSpellDragEnd == null)
        return;
      castSpellDragEnd(spellStatusData, onTarget);
    }

    public void CastCompanionDragBegin(CompanionStatusCellRenderer statusData, bool click)
    {
      CastEventListener.CompanionCastBeginDelegate companionDragBegin = this.OnCastCompanionDragBegin;
      if (companionDragBegin == null)
        return;
      companionDragBegin(statusData, click);
    }

    public void CastCompanionDragEnd(CompanionStatusCellRenderer statusData, bool onTarget)
    {
      Action<CompanionStatusCellRenderer, bool> companionDragEnd = this.OnCastCompanionDragEnd;
      if (companionDragEnd == null)
        return;
      companionDragEnd(statusData, onTarget);
    }

    public delegate void SpellCastBeginDelegate(SpellStatusCellRenderer spellStatusData, bool click);

    public delegate void CompanionCastBeginDelegate(
      CompanionStatusCellRenderer spellStatusData,
      bool click);
  }
}
