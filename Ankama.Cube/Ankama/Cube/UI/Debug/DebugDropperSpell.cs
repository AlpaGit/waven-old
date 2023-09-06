// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugDropperSpell
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using System.Linq;

namespace Ankama.Cube.UI.Debug
{
  public class DebugDropperSpell : DebugDropperDataDefinition<SpellDefinition>
  {
    private const string DisplayName = "Spell";
    private const string SearchPrefKey = "DebugSpellDropperSearch";

    protected override SpellDefinition[] dataValues => RuntimeData.spellDefinitions.Values.ToArray<SpellDefinition>();

    protected override void Awake()
    {
      base.Awake();
      this.Initialize("Spell", "DebugSpellDropperSearch");
    }
  }
}
