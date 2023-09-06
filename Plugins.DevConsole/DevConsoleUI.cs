// Decompiled with JetBrains decompiler
// Type: DevConsole.DevConsoleUI
// Assembly: Plugins.DevConsole, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4D25270E-9F85-416E-93E9-F4DD7A02C55D
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.DevConsole.dll

using Ankama.Cube.TEMPFastEnterMatch.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DevConsole
{
  public class DevConsoleUI : MonoBehaviour
  {
    [Header("Console Behaviour")]
    [Tooltip("Autofill with the first autocomplete option if there is more than one option?")]
    public bool fillFirstAutocompleteIfMultiple;
    [Tooltip("Use tab as well as arrows to cycle autocomplete options")]
    public bool tabToCycleAutocompleteOptions = true;
    [Tooltip("Is the developer console listening for a key press?")]
    public bool listeningForKey = true;
    [Tooltip("Key to press to activate or deactivate the console")]
    public KeyCode activationKey = KeyCode.BackQuote;
    [Tooltip("Should Shift key be down to activate or deactivate the console")]
    public bool activationKeyNeedsShift;
    [Tooltip("Should Alt key be down to activate or deactivate the console")]
    public bool activationKeyNeedsAlt;
    [Tooltip("Should Ctrl key be down to activate or deactivate the console")]
    public bool activationKeyNeedsCtrl;
    [Tooltip("Key to press to attempt to autocomplete current command")]
    public KeyCode autocompleteKey = KeyCode.Tab;
    [Tooltip("Key to press in combintion with autocomplete key to move backwards")]
    public KeyCode autocompleteReverseModifier = KeyCode.LeftShift;
    [Tooltip("Should spaces be automatically added at the end of autocompleted commands")]
    public bool addSpaceAtEndOfAutocomplete;
    [Header("UI Links")]
    public InputField commandInput;
    public Canvas consoleCanvas;
    public GameObject commandPanel;
    public Text consoleOutput;
    public ScrollRect consoleOutputScrollRect;
    protected DevConsoleUI.State m_currentState;
    protected int m_maximumCharacters = 10000;
    protected bool m_isOpen;
    protected List<string> m_autocompleteList = new List<string>();
    protected int m_autocompleteIndex;
    protected bool m_isFirstOpen = true;
    protected bool m_canCycleAutocomplete;

    private void Start()
    {
      this.consoleCanvas.enabled = this.m_isOpen;
      this.commandPanel.SetActive(this.m_isOpen);
      ConsoleDaemon.Instance.OnClearConsole.AddListener(new UnityAction(this.ClearConsole));
      ConsoleDaemon.Instance.OnAddTextToConsole.AddListener(new UnityAction<string>(this.AddTextToConsole));
    }

    private void Update()
    {
      if (!this.listeningForKey)
        return;
      if (Input.anyKeyDown && !Input.GetKeyDown(this.autocompleteKey) && !Input.GetKeyDown(this.autocompleteReverseModifier))
        this.m_canCycleAutocomplete = false;
      if (Input.GetKeyDown(this.activationKey) && PlayerData.instance.admin)
      {
        int num1 = !this.activationKeyNeedsAlt || Input.GetKey(KeyCode.LeftAlt) ? 1 : (Input.GetKey(KeyCode.RightAlt) ? 1 : 0);
        bool flag1 = !this.activationKeyNeedsCtrl || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        bool flag2 = !this.activationKeyNeedsShift || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        int num2 = flag1 ? 1 : 0;
        if ((num1 & num2 & (flag2 ? 1 : 0)) != 0)
        {
          this.m_isOpen = !this.m_isOpen;
          if (this.commandInput.text.Length > 0 && this.commandInput.text[this.commandInput.text.Length - 1] == '`')
            this.commandInput.text = this.commandInput.text.Substring(0, this.commandInput.text.Length - 1);
          if (!this.m_isOpen)
          {
            this.commandInput.DeactivateInputField();
          }
          else
          {
            this.commandInput.Select();
            this.commandInput.ActivateInputField();
          }
          this.consoleCanvas.enabled = this.m_isOpen;
          this.commandPanel.SetActive(this.m_isOpen);
        }
      }
      if (!this.m_isOpen)
        return;
      if (this.m_isFirstOpen)
      {
        this.m_isFirstOpen = false;
        this.consoleOutput.text += ConsoleDaemon.Instance.GetWelcomeMessage();
        this.ScrollToBottom();
      }
      if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
      {
        this.ExecuteCommand();
        this.m_currentState = DevConsoleUI.State.Default;
      }
      else
      {
        bool flag3 = Input.GetKeyDown(this.autocompleteKey) && this.m_canCycleAutocomplete && this.m_autocompleteList != null && this.m_autocompleteList.Count > 0 && this.m_currentState == DevConsoleUI.State.AutocompleteSelection;
        if (Input.GetKeyDown(KeyCode.Tab) && !flag3)
        {
          this.m_autocompleteList = ConsoleDaemon.Instance.FetchAutocompleteOptions(this.commandInput.text);
          this.m_autocompleteIndex = 0;
          if (this.tabToCycleAutocompleteOptions && !this.m_canCycleAutocomplete && this.m_autocompleteList != null)
            this.m_autocompleteIndex = this.m_autocompleteList.Count - 1;
          this.m_canCycleAutocomplete = this.tabToCycleAutocompleteOptions;
          if (this.m_autocompleteList == null || this.m_autocompleteList.Count == 0)
            this.m_currentState = DevConsoleUI.State.Default;
          else if (this.m_autocompleteList.Count == 1)
          {
            this.commandInput.text = this.m_autocompleteList[0] + (this.addSpaceAtEndOfAutocomplete ? " " : "");
            this.commandInput.selectionFocusPosition = this.commandInput.selectionAnchorPosition = this.commandInput.text.Length;
            this.m_currentState = DevConsoleUI.State.Default;
          }
          else
          {
            this.consoleOutput.text += "Autocomplete options:";
            foreach (string autocomplete in this.m_autocompleteList)
            {
              Text consoleOutput = this.consoleOutput;
              consoleOutput.text = consoleOutput.text + Environment.NewLine + "    " + autocomplete;
            }
            this.consoleOutput.text += Environment.NewLine;
            if (this.fillFirstAutocompleteIfMultiple)
            {
              this.commandInput.text = this.m_autocompleteList[0] + (this.addSpaceAtEndOfAutocomplete ? " " : "");
              this.commandInput.selectionFocusPosition = this.commandInput.selectionAnchorPosition = this.m_autocompleteList[0].Length;
            }
            else
            {
              string text = this.commandInput.text;
              int val1 = int.MaxValue;
              foreach (string autocomplete in this.m_autocompleteList)
                val1 = Math.Min(val1, autocomplete.Length);
              string autocomplete1 = this.m_autocompleteList[0];
              this.m_autocompleteList.RemoveAt(0);
              string str = (string) null;
              for (int length = text.Length; length < val1 && str == null; ++length)
              {
                char ch = autocomplete1[length];
                foreach (string autocomplete2 in this.m_autocompleteList)
                {
                  if ((int) autocomplete2[length] != (int) ch)
                  {
                    str = autocomplete1.Substring(0, length);
                    break;
                  }
                }
              }
              if (str != null)
              {
                this.commandInput.text = str;
                this.commandInput.selectionFocusPosition = this.commandInput.selectionAnchorPosition = this.commandInput.text.Length;
              }
            }
            this.m_currentState = DevConsoleUI.State.AutocompleteSelection;
          }
          this.ScrollToBottom();
        }
        else
        {
          bool flag4 = Input.GetKeyDown(KeyCode.UpArrow) || flag3 && Input.GetKeyDown(this.autocompleteReverseModifier);
          bool flag5 = Input.GetKeyDown(KeyCode.DownArrow) || flag3 && !Input.GetKeyDown(this.autocompleteReverseModifier);
          if (!(flag4 | flag5))
            return;
          string str;
          if (this.m_currentState == DevConsoleUI.State.AutocompleteSelection && this.m_autocompleteList != null && this.m_autocompleteList.Count > 1)
          {
            this.m_autocompleteIndex = (this.m_autocompleteIndex + (flag4 ? -1 : 1) + this.m_autocompleteList.Count) % this.m_autocompleteList.Count;
            str = this.m_autocompleteList[this.m_autocompleteIndex] + (this.addSpaceAtEndOfAutocomplete ? " " : "");
          }
          else
            str = flag4 ? ConsoleDaemon.Instance.GetHistory_Previous() : ConsoleDaemon.Instance.GetHistory_Next();
          if (string.IsNullOrEmpty(str))
            return;
          this.commandInput.text = str;
          this.commandInput.selectionFocusPosition = this.commandInput.selectionAnchorPosition = str.Length;
        }
      }
    }

    private string FormatOutput(string rawOutput)
    {
      string[] strArray = rawOutput.Split(new string[1]
      {
        Environment.NewLine
      }, StringSplitOptions.None);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].StartsWith("[Error]"))
          strArray[index] = "<color=#ff0000ff>" + strArray[index] + "</color>";
      }
      return string.Join(Environment.NewLine, strArray);
    }

    public void ExecuteCommand()
    {
      Text consoleOutput1 = this.consoleOutput;
      consoleOutput1.text = consoleOutput1.text + Environment.NewLine + "$ " + this.commandInput.text;
      string rawOutput = ConsoleDaemon.Instance.ExecuteCommand(this.commandInput.text);
      Text consoleOutput2 = this.consoleOutput;
      consoleOutput2.text = consoleOutput2.text + Environment.NewLine + this.FormatOutput(rawOutput);
      this.consoleOutput.text += Environment.NewLine;
      this.ScrollToBottom();
      this.commandInput.text = "";
      this.commandInput.ActivateInputField();
    }

    protected void ScrollToBottom()
    {
      if (this.consoleOutput.text.Length > this.m_maximumCharacters)
        this.consoleOutput.text = this.consoleOutput.text.Remove(0, this.consoleOutput.text.Length - this.m_maximumCharacters);
      Canvas.ForceUpdateCanvases();
      this.consoleOutputScrollRect.verticalNormalizedPosition = 0.0f;
      Canvas.ForceUpdateCanvases();
    }

    public void AddTextToConsole(string rawText)
    {
      Text consoleOutput = this.consoleOutput;
      consoleOutput.text = consoleOutput.text + Environment.NewLine + this.FormatOutput(rawText);
      this.ScrollToBottom();
    }

    public void ClearConsole()
    {
      this.consoleOutput.text = "";
      this.ScrollToBottom();
    }

    public enum State
    {
      Default,
      AutocompleteSelection,
    }
  }
}
