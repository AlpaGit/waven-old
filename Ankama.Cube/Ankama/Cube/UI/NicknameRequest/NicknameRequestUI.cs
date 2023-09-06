// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.NicknameRequest.NicknameRequestUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI.NicknameRequest
{
  public class NicknameRequestUI : AbstractUI
  {
    [SerializeField]
    private InputField m_nicknameInputField;
    [SerializeField]
    private GameObject m_errorContainer;
    [SerializeField]
    private RawTextField m_errorMessage;
    [SerializeField]
    private RawTextField m_suggestions;
    [SerializeField]
    private Button m_okButton;

    public event Action<string> OnNicknameRequest;

    protected override void Awake()
    {
      this.m_errorContainer.SetActive(false);
      this.m_okButton.onClick.AddListener(new UnityAction(this.OnNickname));
      this.m_okButton.interactable = false;
      this.m_nicknameInputField.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChanged));
    }

    private void OnValueChanged(string value) => this.m_okButton.interactable = ((true ? 1 : 0) & (value.Length < 3 ? 0 : (value.Length <= 20 ? 1 : 0))) != 0;

    private void OnNickname()
    {
      this.interactable = true;
      Action<string> onNicknameRequest = this.OnNicknameRequest;
      if (onNicknameRequest == null)
        return;
      onNicknameRequest(this.m_nicknameInputField.text);
    }

    public void OnNicknameError(IList<string> suggestList, string errorKey, string errorTranslated)
    {
      this.m_errorContainer.SetActive(true);
      this.m_errorMessage.SetText(errorTranslated);
      this.m_suggestions.SetText(string.Join(", ", (IEnumerable<string>) suggestList));
      this.interactable = true;
      this.m_nicknameInputField.ActivateInputField();
    }
  }
}
