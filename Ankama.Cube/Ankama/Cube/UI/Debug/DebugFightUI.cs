// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Debug.DebugFightUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.Debug.FightAdminCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Debug
{
  public class DebugFightUI : MonoBehaviour
  {
    [SerializeField]
    private GameObject m_textPanel;
    [SerializeField]
    private Text m_text;
    [Header("Selectors")]
    [SerializeField]
    private DebugDropperSpell m_debugSpellDropper;
    [SerializeField]
    private DebugDropperCreature m_debugCreatureDropper;
    [SerializeField]
    private DebugSelectorProperty m_debugPropertySelector;
    [SerializeField]
    private DebugSelectorElementaryState m_debugElementaryStateSelector;
    [Header("Display info")]
    [SerializeField]
    private RawTextField m_turnCounterText;
    [SerializeField]
    private RawTextField m_timerText;
    private readonly List<AbstractFightAdminCommand> m_adminCommands = new List<AbstractFightAdminCommand>();
    private AbstractFightAdminCommand m_lastRunningCommand;
    private readonly Stopwatch m_stopwatch = new Stopwatch();
    private long m_lastTime;
    private bool m_commandsActivated;

    private void SetCommandsActivated(bool activated)
    {
      this.m_commandsActivated = activated;
      this.m_textPanel.SetActive(activated);
    }

    private void Awake()
    {
      this.SetCommandsActivated(false);
      this.InitializeAllCommands();
      string str1 = "";
      foreach (AbstractFightAdminCommand adminCommand in this.m_adminCommands)
      {
        string str2 = "<b><color=\"green\">" + (object) adminCommand.key + "</color></b>: " + adminCommand.name;
        str1 = !string.IsNullOrEmpty(str1) ? str1 + "\n" + str2 : str2;
      }
      this.m_text.text = str1;
      this.m_stopwatch.Start();
      RectTransform component = this.GetComponent<RectTransform>();
      Vector2 zero;
      Vector2 vector2 = zero = Vector2.zero;
      component.offsetMax = zero;
      component.offsetMin = vector2;
    }

    private void InitializeAllCommands()
    {
      this.m_adminCommands.Add((AbstractFightAdminCommand) new DrawSpellsCommand(KeyCode.S));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new DiscardSpellsCommand(KeyCode.Q));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new GiveActionPointsCommand(KeyCode.A));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new GiveReservePointsCommand(KeyCode.R));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new GiveElementPointsCommand(KeyCode.E));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new KillEntityCommand(KeyCode.K));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new DealDamageCommand(KeyCode.D));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new HealCommand(KeyCode.H));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new TeleportCommand(KeyCode.T));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new PickSpellCommand(KeyCode.F11, this.m_debugSpellDropper));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new InvokeCreatureCommand(KeyCode.I, this.m_debugCreatureDropper));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new SetPropertyCommand(KeyCode.P, this.m_debugPropertySelector));
      this.m_adminCommands.Add((AbstractFightAdminCommand) new ApplyElementaryStateCommand(KeyCode.M, this.m_debugElementaryStateSelector));
      this.m_adminCommands.Sort((Comparison<AbstractFightAdminCommand>) ((a, b) => string.CompareOrdinal(a.name, b.name)));
    }

    private void Update()
    {
      this.RefreshTimer();
      if (Input.GetKeyDown(KeyCode.F12))
        this.SetCommandsActivated(!this.m_commandsActivated);
      if (!this.m_commandsActivated || this.m_lastRunningCommand != null && this.m_lastRunningCommand.Handle())
        return;
      this.m_lastRunningCommand = (AbstractFightAdminCommand) null;
      foreach (AbstractFightAdminCommand adminCommand in this.m_adminCommands)
      {
        if (adminCommand.Handle())
        {
          this.m_lastRunningCommand = adminCommand;
          break;
        }
      }
    }

    private void RefreshTimer()
    {
      long elapsedMilliseconds = this.m_stopwatch.ElapsedMilliseconds;
      if (elapsedMilliseconds <= this.m_lastTime + 1000L)
        return;
      this.m_lastTime = elapsedMilliseconds;
      this.m_timerText.SetText(this.m_stopwatch.Elapsed.ToString("mm\\:ss"));
    }

    public void SetTurnIdex(int turnIndex) => this.m_turnCounterText.SetText(string.Format("Turn: {0}", (object) ((turnIndex - 1) / 2 + 1)));
  }
}
