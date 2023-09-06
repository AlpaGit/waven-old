// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindowManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.Fight.NotificationWindow
{
  public class NotificationWindowManager : MonoBehaviour
  {
    [SerializeField]
    private RectTransform m_windowsContainerTransform;
    [SerializeField]
    private Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow m_prefab;
    private static NotificationWindowManager s_manager;
    private List<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow> m_windows = new List<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow>();

    private void Awake() => NotificationWindowManager.s_manager = this;

    private void OnDestroy() => NotificationWindowManager.s_manager = (NotificationWindowManager) null;

    private void _AddNotification(string notification)
    {
      Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow notificationWindow = UnityEngine.Object.Instantiate<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow>(this.m_prefab, (Transform) this.m_windowsContainerTransform);
      notificationWindow.Open(notification);
      notificationWindow.OnClosed += new Action<Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow>(this.OnClosed);
    }

    private void OnClosed(Ankama.Cube.UI.Fight.NotificationWindow.NotificationWindow window) => UnityEngine.Object.Destroy((UnityEngine.Object) window.gameObject);

    public static void DisplayNotification(string notification)
    {
      if ((UnityEngine.Object) NotificationWindowManager.s_manager == (UnityEngine.Object) null)
        Log.Error("Impossible to add a notification: s_manager == null", 47, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\NotificationWindow\\NotificationWindowManager.cs");
      else
        NotificationWindowManager.s_manager._AddNotification(notification);
    }
  }
}
