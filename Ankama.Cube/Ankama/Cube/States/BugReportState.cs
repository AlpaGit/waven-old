// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.States.BugReportState
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.TEMPFastEnterMatch.Player;
using Ankama.Cube.UI;
using Ankama.Utilities;
using System;
using System.Collections;
using Unity.Cloud.UserReporting;
using Unity.Cloud.UserReporting.Client;
using Unity.Cloud.UserReporting.Plugin;
using UnityEngine;

namespace Ankama.Cube.States
{
  public class BugReportState : LoadSceneStateContext
  {
    private UserReport m_currentBugReport;
    private BugReportUI m_ui;
    private GameObject m_updater;
    private bool m_isCreatingUserReport;
    private bool m_isSubmittingUserReport;
    private bool m_uploadFinished;

    public static bool isReady { get; private set; } = true;

    public void Initialize()
    {
      BugReportState.isReady = false;
      this.m_isCreatingUserReport = true;
      this.m_isSubmittingUserReport = false;
      UnityUserReporting.Configure();
      UserReportingClient currentClient = UnityUserReporting.CurrentClient;
      currentClient.TakeScreenshot(1024, 1024, (Action<UserReportScreenshot>) (s => { }));
      currentClient.CreateUserReport(new Action<UserReport>(this.CreateUserReportCallback));
      this.m_updater = new GameObject("BugReportUpdater", new System.Type[1]
      {
        typeof (BugReportUpdater)
      });
    }

    protected override IEnumerator Load()
    {
      BugReportState uiloader = this;
      LoadSceneStateContext.UILoader<BugReportUI> loader = new LoadSceneStateContext.UILoader<BugReportUI>((LoadSceneStateContext) uiloader, "BugReportUI", "core/scenes/ui/option", true);
      yield return (object) loader.Load();
      uiloader.m_ui = loader.ui;
      while (uiloader.m_isCreatingUserReport)
        yield return (object) null;
      uiloader.SetThumbnail();
      uiloader.m_ui.gameObject.SetActive(true);
    }

    protected override void Enable()
    {
      this.m_ui.onSubmitClick = new Action<string, string, string>(this.OnSubmitClick);
      this.m_ui.onCloseClick = new Action(this.OnCloseClick);
    }

    protected override bool UseInput(InputState inputState)
    {
      if (inputState.id != 1 || inputState.state != InputState.State.Activated || !((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
        return true;
      this.OnCloseClick();
      return true;
    }

    protected override IEnumerator Unload()
    {
      BugReportState.isReady = true;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_updater)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_updater);
        this.m_updater = (GameObject) null;
      }
      return base.Unload();
    }

    protected override void Disable()
    {
      this.m_ui.onSubmitClick = (Action<string, string, string>) null;
      this.m_ui.onCloseClick = (Action) null;
    }

    private void SetThumbnail()
    {
      if (this.m_currentBugReport == null || (UnityEngine.Object) null == (UnityEngine.Object) this.m_ui)
        return;
      byte[] data = Convert.FromBase64String(this.m_currentBugReport.Thumbnail.DataBase64);
      Texture2D texture2D = new Texture2D(1, 1);
      texture2D.LoadImage(data);
      this.m_ui.SetThumbnail(Sprite.Create(texture2D, new Rect(0.0f, 0.0f, (float) texture2D.width, (float) texture2D.height), new Vector2(0.5f, 0.5f)));
    }

    private void OnSubmitClick(string summary, string description, string category)
    {
      if (this.m_currentBugReport == null || this.m_isSubmittingUserReport)
        return;
      this.m_isSubmittingUserReport = true;
      this.m_uploadFinished = false;
      this.m_currentBugReport.Summary = summary;
      UserReportNamedValue reportNamedValue = new UserReportNamedValue("Category", category);
      this.m_currentBugReport.Dimensions.Add(reportNamedValue);
      this.m_currentBugReport.Fields.Add(reportNamedValue);
      this.m_currentBugReport.Fields.Add(new UserReportNamedValue("Description", description));
      PlayerData instance = PlayerData.instance;
      if (instance != null)
        this.m_currentBugReport.Fields.Add(new UserReportNamedValue("User Nickname", instance.nickName));
      UnityUserReporting.CurrentClient.SendUserReport(this.m_currentBugReport, new Action<float, float>(this.UserReportSubmissionProgress), new Action<bool, UserReport>(this.UserReportSubmissionCallback));
    }

    private void OnCloseClick()
    {
      StateLayer layer = this.GetLayer();
      if (layer != null)
        StateManager.DiscardInputLayer(layer);
      this.parent.ClearChildState();
    }

    private void CreateUserReportCallback(UserReport bugReport)
    {
      if (string.IsNullOrEmpty(bugReport.ProjectIdentifier))
        Log.Warning("The user report's project identifier is not set. Please setup cloud services using the Services tab or manually specify a project identifier when calling UnityUserReporting.Configure().", 178, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\States\\OptionLayer\\BugReportState.cs");
      string str1 = "Unknown";
      string str2 = "0.0";
      foreach (UserReportNamedValue reportNamedValue in bugReport.DeviceMetadata)
      {
        switch (reportNamedValue.Name)
        {
          case "Platform":
            str1 = reportNamedValue.Value;
            continue;
          case "Version":
            str2 = reportNamedValue.Value;
            continue;
          default:
            continue;
        }
      }
      bugReport.Dimensions.Add(new UserReportNamedValue("Platform.Version", str1 + "." + str2));
      this.m_currentBugReport = bugReport;
      this.m_isCreatingUserReport = false;
    }

    private void UserReportSubmissionProgress(float uploadProgress, float downloadProgress)
    {
      this.m_uploadFinished = this.m_uploadFinished || (double) uploadProgress >= 1.0;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
        return;
      if ((double) uploadProgress < 1.0)
        this.m_ui.SetProgress(uploadProgress, 1);
      else
        this.m_ui.SetProgress(downloadProgress, 2);
    }

    private void UserReportSubmissionCallback(bool success, UserReport userReport)
    {
      this.m_isSubmittingUserReport = false;
      if (success || this.m_uploadFinished)
      {
        this.parent.ClearChildState();
        this.m_currentBugReport = (UserReport) null;
      }
      else
      {
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_ui))
          return;
        this.m_ui.ResetForm();
        this.m_ui.SetError();
      }
    }
  }
}
