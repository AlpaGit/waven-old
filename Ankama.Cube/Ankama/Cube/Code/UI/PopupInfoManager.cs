// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Code.UI.PopupInfoManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement;
using Ankama.AssetManagement.StateManagement;
using Ankama.Cube.UI;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections.Generic;

namespace Ankama.Cube.Code.UI
{
  public class PopupInfoManager : Singleton<PopupInfoManager>
  {
    private List<PopupInfoManager.StackedMessage> m_messages = new List<PopupInfoManager.StackedMessage>();
    private PopupInfoManager.StackedMessage m_displayedMessage;
    private PopupInfoState m_popupState;
    private int m_stackedId;
    public Action<int> onPopupDisplayed;
    public Action<int> onPopupBeginClosing;

    public bool isPopupDisplaying => this.m_popupState != null && this.m_popupState.loadState != StateLoadState.Unloaded;

    public static int Show(StateContext parentState, PopupInfo info) => Singleton<PopupInfoManager>.instance.Add(parentState, info);

    public static void RemoveById(int id) => Singleton<PopupInfoManager>.instance.Remove(id);

    public static void ClearAllMessages() => Singleton<PopupInfoManager>.instance.ClearAll();

    private int Add(StateContext parentState, PopupInfo info)
    {
      if (parentState is PopupInfoState)
        parentState = parentState.parent;
      ++this.m_stackedId;
      this.m_messages.Add(new PopupInfoManager.StackedMessage(this.m_stackedId, info, parentState));
      if (this.m_messages.Count == 1)
        Singleton<SceneEventListener>.instance.AddUpdateListener(new Action(this.Update));
      return this.m_stackedId;
    }

    private void Remove(int id)
    {
      Log.Info(string.Format("Remove {0}", (object) id), 72, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\PopupInfo\\PopupInfoManager.cs");
      for (int index = this.m_messages.Count - 1; index >= 0; --index)
      {
        if (this.m_messages[index].id == id)
        {
          this.m_messages.RemoveAt(index);
          break;
        }
      }
      if (!this.isPopupDisplaying || this.m_displayedMessage.id != id)
        return;
      this.m_popupState.Close();
    }

    private void ClearAll()
    {
      this.m_messages.Clear();
      if (!this.isPopupDisplaying)
        return;
      this.m_popupState.Close();
    }

    private void Update()
    {
      if (this.m_messages.Count == 0)
        Singleton<SceneEventListener>.instance.RemoveUpdateListener(new Action(this.Update));
      if (this.m_messages.Count <= 0 || this.isPopupDisplaying)
        return;
      this.DisplayStackedMessage();
    }

    private void DisplayStackedMessage()
    {
      if (this.m_messages.Count == 0 || this.isPopupDisplaying)
        return;
      this.m_displayedMessage = this.m_messages[0];
      this.m_messages.RemoveAt(0);
      StateContext parentState = this.m_displayedMessage.parentState;
      if (parentState == null || parentState.loadState > StateLoadState.Updating)
        return;
      this.m_popupState = new PopupInfoState(this.m_displayedMessage.info);
      parentState.SetChildState((StateContext) this.m_popupState);
      this.m_popupState.onClose += (Action) (() =>
      {
        Action<int> popupBeginClosing = this.onPopupBeginClosing;
        if (popupBeginClosing == null)
          return;
        popupBeginClosing(this.m_displayedMessage.id);
      });
      Action<int> onPopupDisplayed = this.onPopupDisplayed;
      if (onPopupDisplayed == null)
        return;
      onPopupDisplayed(this.m_displayedMessage.id);
    }

    public static void ShowApplicationError(PopupInfo popupInfo)
    {
      StateLayer stateLayer;
      if (!StateManager.TryGetLayer("application", out stateLayer))
      {
        stateLayer = StateManager.AddLayer("application");
        StateManager.SetActiveInputLayer(stateLayer);
        UIManager.instance.NotifyLayerIndexChange();
      }
      popupInfo.style = PopupStyle.Error;
      PopupInfoManager.Show((StateContext) stateLayer, popupInfo);
    }

    public struct StackedMessage
    {
      public int id;
      public PopupInfo info;
      public StateContext parentState;

      public StackedMessage(int id, PopupInfo info, StateContext parentState)
      {
        this.id = id;
        this.info = info;
        this.parentState = parentState;
      }
    }
  }
}
