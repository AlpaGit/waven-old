// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Demo.UI.LoginUIDemo
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.Demo.UI
{
  public class LoginUIDemo : BaseOpenCloseUI
  {
    [SerializeField]
    private InputField m_pseudo;
    [SerializeField]
    private Button m_loginButton;
    [SerializeField]
    private Image m_blackBackground;
    [SerializeField]
    private Selectable[] m_selectables;
    private int m_selectedIndex;
    public Action<bool, string> onConnect;

    private void Start()
    {
      this.m_blackBackground.gameObject.SetActive(true);
      this.m_blackBackground.WithAlpha<Image>(1f);
      this.m_pseudo.text = string.Empty;
      this.m_pseudo.Select();
      this.m_pseudo.onValueChanged.AddListener(new UnityAction<string>(this.OnPseudoChanged));
      this.m_loginButton.interactable = false;
      this.m_loginButton.onClick.AddListener(new UnityAction(this.OnLoginButtonClicked));
    }

    private void OnPseudoChanged(string pseudo) => this.m_loginButton.interactable = !string.IsNullOrWhiteSpace(pseudo);

    public void DoClickSelected()
    {
      Selectable[] selectables = this.m_selectables;
      int length = selectables.Length;
      if (length == 0)
        return;
      while (this.m_selectedIndex < length)
      {
        Selectable button = selectables[this.m_selectedIndex];
        if ((UnityEngine.Object) button != (UnityEngine.Object) this.m_pseudo)
        {
          InputUtility.SimulateClickOn(button);
          break;
        }
        this.SelectNext();
      }
    }

    public void SelectNext()
    {
      if (this.m_selectables.Length == 0)
        return;
      this.m_selectedIndex = (this.m_selectedIndex + 1) % this.m_selectables.Length;
      this.m_selectables[this.m_selectedIndex].Select();
    }

    private void OnLoginButtonClicked()
    {
      Action<bool, string> onConnect = this.onConnect;
      if (onConnect == null)
        return;
      onConnect(false, this.m_pseudo.text.Trim());
    }
  }
}
