// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.CompanionBarRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Protocols.FightCommonProtocol;
using Ankama.Cube.UI.Components;
using Ankama.Cube.UI.DeckMaker;
using Ankama.Cube.UI.Fight.NotificationWindow;
using Ankama.Cube.UI.Fight.Windows;
using Ankama.Cube.Utility;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.UI.Fight
{
  public sealed class CompanionBarRework : 
    MonoBehaviour,
    ICompanionStatusCellRendererConfigurator,
    ICompanionCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator,
    IDragNDropValidator
  {
    [SerializeField]
    private DynamicList m_companionList;
    [SerializeField]
    private DynamicList m_additionalCompanionList;
    [SerializeField]
    private FightTooltip m_tooltip;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private FightUIFactory m_factory;
    private readonly ObjectPool<Queue<CompanionStatusData>> m_queuePool = new ObjectPool<Queue<CompanionStatusData>>();
    private readonly Dictionary<int, Queue<CompanionStatusData>> m_companionStatusQueue = new Dictionary<int, Queue<CompanionStatusData>>();
    private readonly Dictionary<int, CompanionStatusData> m_companionStatusInView = new Dictionary<int, CompanionStatusData>();
    private readonly List<ReserveCompanionStatus> m_companionStatusList = new List<ReserveCompanionStatus>();
    private readonly List<ReserveCompanionStatus> m_additionalCompanionStatusList = new List<ReserveCompanionStatus>();
    private readonly Dictionary<EventCategory, List<int>> m_companionsPerCategoryInvalidatingStatus = new Dictionary<EventCategory, List<int>>();
    private readonly Dictionary<EventCategory, List<int>> m_companionsPerCategoryInvalidatingView = new Dictionary<EventCategory, List<int>>();
    private CastEventListener m_castEventListener;
    private PlayerStatus m_playerStatus;
    private CompanionStatusCellRenderer m_companionBeingCast;
    private bool m_interactable;
    private CastHighlight m_castHighlight;

    public void SetPlayerStatus(PlayerStatus playerStatus) => this.m_playerStatus = playerStatus;

    public void SetInteractable(bool interactable)
    {
      this.m_interactable = interactable;
      this.m_companionList.UpdateAllConfigurators();
      this.m_additionalCompanionList.UpdateAllConfigurators();
    }

    private void Awake()
    {
      this.m_companionList.SetCellRendererConfigurator((ICellRendererConfigurator) this);
      this.m_additionalCompanionList.SetCellRendererConfigurator((ICellRendererConfigurator) this);
      this.m_castEventListener = new CastEventListener();
      this.m_castEventListener.OnCastCompanionDragBegin += new CastEventListener.CompanionCastBeginDelegate(this.OnCastCompanionDragBegin);
      this.m_castEventListener.OnCastCompanionDragEnd += new Action<CompanionStatusCellRenderer, bool>(this.OnCastCompanionDragEnd);
      FightCastManager.OnTargetChange += new FightCastManager.OnTargetChangeDelegate(this.OnFightMapTargetChanged);
      FightCastManager.OnUserActionEnd += new FightCastManager.OnUserActionEndDelegate(this.OnFightMapUserActionEnd);
    }

    private void OnDestroy()
    {
      FightCastManager.OnTargetChange -= new FightCastManager.OnTargetChangeDelegate(this.OnFightMapTargetChanged);
      FightCastManager.OnUserActionEnd -= new FightCastManager.OnUserActionEndDelegate(this.OnFightMapUserActionEnd);
    }

    private void OnFightMapTargetChanged(bool hasTarget, CellObject cellObject)
    {
      if ((UnityEngine.Object) this.m_companionBeingCast == (UnityEngine.Object) null)
        return;
      this.CleanCastHighlight();
      if (hasTarget && (UnityEngine.Object) cellObject != (UnityEngine.Object) null)
      {
        DragNDropListener.instance.SnapDragToWorldPosition(CameraHandler.current.camera, cellObject.transform.position);
        this.m_companionBeingCast.OnEnterTarget();
        this.m_castHighlight = this.m_factory.CreateCastHighlight<ReserveCompanionStatus>((ReserveCompanionStatus) this.m_companionBeingCast.value, cellObject.highlight.transform);
      }
      else
      {
        DragNDropListener.instance.CancelSnapDrag();
        this.m_companionBeingCast.OnExitTarget();
      }
    }

    private void OnFightMapUserActionEnd(FightCastState state)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_companionBeingCast)
        return;
      switch (state)
      {
        case FightCastState.Targeting:
          this.m_companionBeingCast.StartCast();
          break;
        case FightCastState.Cancelled:
          this.m_companionBeingCast.CancelCast();
          DragNDropListener.instance.CancelSnapDrag();
          this.m_companionBeingCast = (CompanionStatusCellRenderer) null;
          this.CleanCastHighlight();
          break;
        case FightCastState.Casting:
          this.m_companionBeingCast.StartCast();
          DragNDropListener.instance.CancelSnapDrag();
          break;
        case FightCastState.DoneCasting:
          this.m_companionBeingCast.DoneCasting();
          this.m_companionBeingCast = (CompanionStatusCellRenderer) null;
          this.CleanCastHighlight();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (state), (object) state, (string) null);
      }
    }

    private void OnCastCompanionDragBegin(CompanionStatusCellRenderer cellRenderer, bool click)
    {
      this.m_companionBeingCast = cellRenderer;
      FightMap.current.SetTargetInputMode(click ? AbstractFightMap.TargetInputMode.Click : AbstractFightMap.TargetInputMode.Drag);
      FightCastManager.StartInvokingCompanion(this.m_playerStatus, (ReserveCompanionStatus) cellRenderer.value);
    }

    private void OnCastCompanionDragEnd(CompanionStatusCellRenderer cellRenderer, bool onTarget)
    {
      if ((UnityEngine.Object) this.m_companionBeingCast == (UnityEngine.Object) null)
        return;
      this.m_companionBeingCast = (CompanionStatusCellRenderer) null;
      if (onTarget)
        return;
      FightCastManager.StopInvokingCompanion(true);
    }

    private void CleanCastHighlight()
    {
      if (!((UnityEngine.Object) this.m_castHighlight != (UnityEngine.Object) null))
        return;
      this.m_factory.DestroyCellHighlight(this.m_castHighlight);
      this.m_castHighlight = (CastHighlight) null;
    }

    public void RefreshUsability(PlayerStatus playerStatus)
    {
      foreach (ReserveCompanionStatus companion in playerStatus.GetAvailableCompanionStatusEnumerator())
        this.EnqueueCompanionStatusData(playerStatus, companion);
      foreach (ReserveCompanionStatus companion in playerStatus.GetAdditionalCompanionStatusEnumerator())
        this.EnqueueCompanionStatusData(playerStatus, companion);
    }

    public void AddCompanionStatus(PlayerStatus playerStatus, ReserveCompanionStatus companion)
    {
      this.EnqueueCompanionStatusData(playerStatus, companion);
      HashSet<EventCategory> eventsToAdd = new HashSet<EventCategory>();
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) companion.definition.eventsInvalidatingCost)
        eventsToAdd.Add(eventCategory);
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) companion.definition.eventsInvalidatingCasting)
        eventsToAdd.Add(eventCategory);
      this.AddFightEventListeners(companion, (IEnumerable<EventCategory>) eventsToAdd, true);
    }

    public void ChangeCompanionStateStatus(
      PlayerStatus playerStatus,
      int companionDefinitionId,
      CompanionReserveState state)
    {
      ReserveCompanionStatus companionStatus;
      if (!playerStatus.TryGetCompanion(companionDefinitionId, out companionStatus))
        return;
      this.EnqueueCompanionStatusData(playerStatus, companionStatus);
    }

    public void RemoveCompanionStatus(int companionDefinitionId) => this.RemoveFightEventListeners(companionDefinitionId, this.m_playerStatus.fightId, true);

    public IEnumerator UpdateAvailableCompanions()
    {
      this.m_companionStatusInView.Clear();
      foreach (KeyValuePair<int, Queue<CompanionStatusData>> companionStatus in this.m_companionStatusQueue)
      {
        Queue<CompanionStatusData> companionStatusDataQueue = companionStatus.Value;
        if (companionStatusDataQueue.Count != 0)
          this.m_companionStatusInView.Add(companionStatus.Key, companionStatusDataQueue.Dequeue());
      }
      this.m_companionList.UpdateAllConfigurators();
      this.m_additionalCompanionList.UpdateAllConfigurators();
      yield break;
    }

    public IEnumerator AddCompanion(ReserveCompanionStatus companion)
    {
      this.DequeueCompanionStatusData(companion, false);
      HashSet<EventCategory> eventsToAdd = new HashSet<EventCategory>();
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) companion.definition.eventsInvalidatingCost)
        eventsToAdd.Add(eventCategory);
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) companion.definition.eventsInvalidatingCasting)
        eventsToAdd.Add(eventCategory);
      this.AddFightEventListeners(companion, (IEnumerable<EventCategory>) eventsToAdd, false);
      if (companion.isGiven)
      {
        this.m_additionalCompanionStatusList.Add(companion);
        this.m_additionalCompanionList.Insert<ReserveCompanionStatus>(this.m_additionalCompanionStatusList.Count - 1, companion);
      }
      else
      {
        this.m_companionStatusList.Add(companion);
        this.m_companionList.Insert<ReserveCompanionStatus>(this.m_companionStatusList.Count - 1, companion);
        yield break;
      }
    }

    public IEnumerator ChangeCompanionState(PlayerStatus playerStatus, int companionDefinitionId)
    {
      ReserveCompanionStatus companionStatus;
      if (playerStatus.TryGetCompanion(companionDefinitionId, out companionStatus))
      {
        this.DequeueCompanionStatusData(companionStatus, true);
        yield break;
      }
    }

    public IEnumerator RemoveCompanion(int companionDefinitionId)
    {
      int index = 0;
      int count = this.m_additionalCompanionStatusList.Count;
      while (index < count && this.m_additionalCompanionStatusList[index].definition.id != companionDefinitionId)
        ++index;
      if (index < this.m_additionalCompanionStatusList.Count)
      {
        this.RemoveFightEventListeners(this.m_additionalCompanionStatusList[index].definition.id, this.m_playerStatus.fightId, false);
        this.m_additionalCompanionStatusList.RemoveAt(index);
        this.m_additionalCompanionList.RemoveAt(index);
        this.m_companionStatusQueue.Remove(companionDefinitionId);
        this.m_companionStatusInView.Remove(companionDefinitionId);
        yield break;
      }
    }

    private void OnCastingValidityUpdated(EventCategory category)
    {
      List<int> intList;
      if (!this.m_companionsPerCategoryInvalidatingStatus.TryGetValue(category, out intList))
        return;
      foreach (int definitionId in intList)
      {
        ReserveCompanionStatus companionStatus;
        if (this.m_playerStatus.TryGetCompanion(definitionId, out companionStatus))
          this.EnqueueCompanionStatusData(this.m_playerStatus, companionStatus);
      }
    }

    private void OnCastingValidityViewUpdated(EventCategory category)
    {
      List<int> intList;
      if (!this.m_companionsPerCategoryInvalidatingView.TryGetValue(category, out intList))
        return;
      foreach (int definitionId in intList)
      {
        ReserveCompanionStatus companionStatus;
        if (this.m_playerStatus.TryGetCompanion(definitionId, out companionStatus))
          this.DequeueCompanionStatusData(companionStatus, true);
      }
    }

    private void EnqueueCompanionStatusData(
      PlayerStatus playerStatus,
      ReserveCompanionStatus companion)
    {
      CompanionStatusData data = new CompanionStatusData()
      {
        state = companion.state,
        isGiven = companion.isGiven
      };
      CastValidityHelper.RecomputeCompanionCastValidity(playerStatus, companion, ref data);
      CastValidityHelper.RecomputeCompanionCost(companion, ref data);
      int id = companion.definition.id;
      Queue<CompanionStatusData> companionStatusDataQueue;
      if (!this.m_companionStatusQueue.TryGetValue(id, out companionStatusDataQueue))
      {
        companionStatusDataQueue = this.m_queuePool.Get();
        this.m_companionStatusQueue.Add(id, companionStatusDataQueue);
      }
      companionStatusDataQueue.Enqueue(data);
    }

    private void DequeueCompanionStatusData(ReserveCompanionStatus companionStatus, bool andUpdate)
    {
      Queue<CompanionStatusData> companionStatusDataQueue;
      if (!this.m_companionStatusQueue.TryGetValue(companionStatus.definition.id, out companionStatusDataQueue) || companionStatusDataQueue.Count == 0)
        return;
      if (this.m_companionStatusInView.ContainsKey(companionStatus.definition.id))
        this.m_companionStatusInView[companionStatus.definition.id] = companionStatusDataQueue.Dequeue();
      else
        this.m_companionStatusInView.Add(companionStatus.definition.id, companionStatusDataQueue.Dequeue());
      if (!andUpdate)
        return;
      if (companionStatus.isGiven)
        this.m_additionalCompanionList.UpdateAllConfigurators();
      else
        this.m_companionList.UpdateAllConfigurators();
    }

    private void AddFightEventListeners(
      ReserveCompanionStatus companionStatus,
      IEnumerable<EventCategory> eventsToAdd,
      bool status)
    {
      foreach (EventCategory eventCategory in eventsToAdd)
        this.AddFightEventListener(companionStatus, eventCategory, status);
      this.AddFightEventListener(companionStatus, EventCategory.ElementPointsChanged, status);
    }

    private void AddFightEventListener(
      ReserveCompanionStatus companionStatus,
      EventCategory eventCategory,
      bool status)
    {
      Dictionary<EventCategory, List<int>> dictionary = status ? this.m_companionsPerCategoryInvalidatingStatus : this.m_companionsPerCategoryInvalidatingView;
      List<int> intList;
      if (!dictionary.TryGetValue(eventCategory, out intList))
      {
        intList = ListPool<int>.Get();
        dictionary.Add(eventCategory, intList);
        if (status)
          FightLogicExecutor.AddListenerUpdateStatus(companionStatus.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityUpdated), eventCategory);
        else
          FightLogicExecutor.AddListenerUpdateView(companionStatus.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityViewUpdated), eventCategory);
      }
      intList.Add(companionStatus.definition.id);
    }

    private void RemoveFightEventListeners(int companionDefinitionId, int fightId, bool status)
    {
      List<EventCategory> eventCategoryList = new List<EventCategory>();
      Dictionary<EventCategory, List<int>> dictionary = status ? this.m_companionsPerCategoryInvalidatingStatus : this.m_companionsPerCategoryInvalidatingView;
      foreach (KeyValuePair<EventCategory, List<int>> keyValuePair in dictionary)
      {
        List<int> list = keyValuePair.Value;
        for (int index = list.Count - 1; index >= 0; --index)
        {
          if (list[index] == companionDefinitionId)
          {
            list.RemoveAt(index);
            break;
          }
        }
        if (list.Count == 0)
        {
          ListPool<int>.Release(list);
          eventCategoryList.Add(keyValuePair.Key);
        }
      }
      foreach (EventCategory eventCategory in eventCategoryList)
      {
        if (status)
          FightLogicExecutor.RemoveListenerUpdateStatus(fightId, new Action<EventCategory>(this.OnCastingValidityUpdated), eventCategory);
        else
          FightLogicExecutor.RemoveListenerUpdateView(fightId, new Action<EventCategory>(this.OnCastingValidityViewUpdated), eventCategory);
        dictionary.Remove(eventCategory);
      }
    }

    public FightTooltip tooltip => this.m_tooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    public CompanionStatusData? GetCompanionStatusData(ReserveCompanionStatus companion)
    {
      if (companion == null)
        return new CompanionStatusData?();
      CompanionStatusData companionStatusData;
      return this.m_companionStatusInView.TryGetValue(companion.definition.id, out companionStatusData) ? new CompanionStatusData?(companionStatusData) : new CompanionStatusData?();
    }

    public CastEventListener GetEventListener() => this.m_castEventListener;

    public bool IsParentInteractable() => this.m_interactable;

    public IDragNDropValidator GetDragNDropValidator() => (IDragNDropValidator) this;

    public bool IsValidDrag(object value)
    {
      if (FightCastManager.currentCastType != FightCastManager.CurrentCastType.None)
        return false;
      ReserveCompanionStatus companionStatus = (ReserveCompanionStatus) value;
      if (CastValidityHelper.ComputeCompanionCostCastValidity(this.m_playerStatus, companionStatus) != CastValidity.SUCCESS)
        return false;
      CastValidity companionCastValidity = CastValidityHelper.ComputeCompanionCastValidity(this.m_playerStatus, companionStatus);
      if (companionCastValidity != CastValidity.SUCCESS)
        NotificationWindowManager.DisplayNotification(TextCollectionUtility.GetFormattedText(companionCastValidity));
      return companionCastValidity == CastValidity.SUCCESS;
    }

    public bool IsValidDrop(object value) => true;

    private void Update()
    {
      if (!InputUtility.GetSecondaryUp() || FightCastManager.currentCastType != FightCastManager.CurrentCastType.Companion || FightCastManager.currentCastState >= FightCastState.Casting)
        return;
      FightCastManager.StopInvokingCompanion(true);
    }
  }
}
