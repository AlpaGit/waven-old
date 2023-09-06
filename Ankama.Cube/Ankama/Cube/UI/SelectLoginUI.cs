// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.SelectLoginUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Player;
using Ankama.Cube.TEMPFastEnterMatch.Auth.ConnectionMethod;
using Ankama.Cube.UI.Components;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ankama.Cube.UI
{
  public class SelectLoginUI : AbstractUI
  {
    [SerializeField]
    protected AnimatedTextButton m_createGuestButton;
    [SerializeField]
    protected AnimatedTextButton m_regularAccountButton;
    [SerializeField]
    protected TextField m_titleText;

    public event Action OnCreateGuest;

    public event Action OnConnectGuest;

    public event Action OnRegularAccount;

    private void Start()
    {
      PlayerPreferences.useGuest = true;
      PlayerPreferences.Save();
      this.m_createGuestButton.onClick.AddListener((UnityAction) (() =>
      {
        if (CredentialProvider.gameCredentialProvider.HasGuestAccount())
        {
          Action onConnectGuest = this.OnConnectGuest;
          if (onConnectGuest == null)
            return;
          onConnectGuest();
        }
        else
        {
          Action onCreateGuest = this.OnCreateGuest;
          if (onCreateGuest == null)
            return;
          onCreateGuest();
        }
      }));
      this.m_regularAccountButton.onClick.AddListener((UnityAction) (() =>
      {
        Action onRegularAccount = this.OnRegularAccount;
        if (onRegularAccount == null)
          return;
        onRegularAccount();
      }));
      this.SetTexts();
    }

    private void SetTexts()
    {
      if (CredentialProvider.gameCredentialProvider.HasGuestAccount())
      {
        this.m_titleText.SetText(45852);
        this.m_createGuestButton.textField.SetText(19445);
        this.m_regularAccountButton.textField.SetText(84332);
      }
      else
      {
        this.m_titleText.SetText(34597);
        this.m_createGuestButton.textField.SetText(51147);
        this.m_regularAccountButton.textField.SetText(74237);
      }
    }

    public void HideGuestSelection()
    {
      PlayerPreferences.useGuest = false;
      this.m_createGuestButton.gameObject.SetActive(false);
    }
  }
}
