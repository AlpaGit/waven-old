// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.BugReportUI
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class BugReportUI : AbstractUI
  {
    [SerializeField]
    private Button m_closeButton;
    [SerializeField]
    private Button m_forumButton;
    [SerializeField]
    private Button m_knownBugsButton;
    [SerializeField]
    private Image m_thumbnailImage;
    [SerializeField]
    private Dropdown m_categoryDropdown;
    [SerializeField]
    private InputField m_summaryInput;
    [SerializeField]
    private InputField m_descriptionInput;
    [SerializeField]
    private GameObject m_progress;
    [SerializeField]
    private Text m_progressText;
    [SerializeField]
    private GameObject m_error;
    [SerializeField]
    private Button m_submitButton;
    public Action<string, string, string> onSubmitClick;
    public Action onCloseClick;
    private bool m_submitting;

    protected override void Awake()
    {
      base.Awake();
      this.ResetForm();
      this.m_forumButton.onClick.AddListener(new UnityAction(this.OnForumClick));
      this.m_knownBugsButton.onClick.AddListener(new UnityAction(this.OnKnownBugsClick));
      this.m_submitButton.onClick.AddListener(new UnityAction(this.OnSubmitClick));
      this.m_closeButton.onClick.AddListener(new UnityAction(this.OnCloseClick));
    }

    protected override void OnDestroy()
    {
      this.m_forumButton.onClick.RemoveListener(new UnityAction(this.OnForumClick));
      this.m_knownBugsButton.onClick.RemoveListener(new UnityAction(this.OnKnownBugsClick));
      this.m_submitButton.onClick.RemoveListener(new UnityAction(this.OnSubmitClick));
      this.m_closeButton.onClick.RemoveListener(new UnityAction(this.OnCloseClick));
      base.OnDestroy();
    }

    protected override void Update()
    {
      base.Update();
      if (this.m_submitting)
        return;
      this.m_submitButton.interactable = !string.IsNullOrWhiteSpace(this.m_summaryInput.text) && !string.IsNullOrWhiteSpace(this.m_descriptionInput.text);
    }

    public void SetThumbnail(Sprite sprite)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_thumbnailImage))
        return;
      this.m_thumbnailImage.sprite = sprite;
      this.m_thumbnailImage.preserveAspect = true;
    }

    public void SetProgress(float progress, int phaseIndex)
    {
      this.m_progressText.text = string.Format("Envoi du rapport en cours : {0:P} ({1} / 2)...", (object) progress, (object) phaseIndex);
      this.m_progress.gameObject.SetActive(true);
    }

    public void SetError() => this.m_error.gameObject.SetActive(true);

    public void ResetForm()
    {
      this.m_submitting = false;
      this.m_summaryInput.interactable = true;
      this.m_descriptionInput.interactable = true;
      this.m_progress.gameObject.SetActive(false);
      this.m_error.gameObject.SetActive(false);
    }

    private void OnKnownBugsClick() => Application.OpenURL("https://forum.waven-game.com/fr/17-bugs/269-liste-bugs-connus");

    private void OnForumClick() => Application.OpenURL("https://forum.waven-game.com/");

    private void OnSubmitClick()
    {
      this.m_submitting = true;
      this.m_submitButton.interactable = false;
      this.m_summaryInput.interactable = false;
      this.m_descriptionInput.interactable = false;
      this.m_error.gameObject.SetActive(false);
      string text1 = this.m_summaryInput.text;
      string text2 = this.m_descriptionInput.text;
      string str = this.m_categoryDropdown.value == 0 ? "Technical" : "Fight";
      Action<string, string, string> onSubmitClick = this.onSubmitClick;
      if (onSubmitClick == null)
        return;
      onSubmitClick(text1, text2, str);
    }

    private void OnCloseClick()
    {
      Action onCloseClick = this.onCloseClick;
      if (onCloseClick == null)
        return;
      onCloseClick();
    }
  }
}
