// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Fight.SpellBarRework
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
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
  public sealed class SpellBarRework : 
    MonoBehaviour,
    ISpellStatusCellRendererConfigurator,
    ISpellCellRendererConfigurator,
    IWithTooltipCellRendererConfigurator,
    ICellRendererConfigurator,
    IDragNDropValidator
  {
    [SerializeField]
    private DynamicList m_spellList;
    [SerializeField]
    private FightTooltip m_tooltip;
    [SerializeField]
    private TooltipPosition m_tooltipPosition;
    [SerializeField]
    private FightUIFactory m_factory;
    private CastEventListener m_castEventListener;
    private readonly ObjectPool<Queue<SpellStatusData>> m_queuePool = new ObjectPool<Queue<SpellStatusData>>();
    private readonly Dictionary<int, Queue<SpellStatusData>> m_spellUsabilityQueue = new Dictionary<int, Queue<SpellStatusData>>();
    private readonly Dictionary<int, SpellStatusData> m_spellUsabilityInView = new Dictionary<int, SpellStatusData>();
    private readonly List<SpellStatus> m_spellStatusList = new List<SpellStatus>();
    private readonly Dictionary<EventCategory, List<int>> m_spellsPerCategoryInvalidatingStatus = new Dictionary<EventCategory, List<int>>();
    private readonly Dictionary<EventCategory, List<int>> m_spellsPerCategoryInvalidatingView = new Dictionary<EventCategory, List<int>>();
    private PlayerStatus m_playerStatus;
    private SpellStatusCellRenderer m_spellBeingCast;
    private readonly List<SpellStatusCellRenderer> m_spellsInDoneCasting = new List<SpellStatusCellRenderer>();
    private bool m_interactable;
    private CastHighlight m_castHighlight;
    private bool m_doneCasting;
    private readonly Queue<List<int>> m_refreshUsabilityQueue = new Queue<List<int>>();

    private void Awake()
    {
      this.m_castEventListener = new CastEventListener();
      this.m_castEventListener.OnCastSpellDragBegin += new CastEventListener.SpellCastBeginDelegate(this.OnCastSpellDragBegin);
      this.m_castEventListener.OnCastSpellDragEnd += new Action<SpellStatusCellRenderer, bool>(this.OnCastSpellDragEnd);
      FightCastManager.OnTargetChange += new FightCastManager.OnTargetChangeDelegate(this.OnFightMapTargetChanged);
      FightCastManager.OnUserActionEnd += new FightCastManager.OnUserActionEndDelegate(this.OnFightMapUserActionEnd);
      this.m_spellList.SetCellRendererConfigurator((ICellRendererConfigurator) this);
    }

    private void OnDestroy()
    {
      FightCastManager.OnTargetChange -= new FightCastManager.OnTargetChangeDelegate(this.OnFightMapTargetChanged);
      FightCastManager.OnUserActionEnd -= new FightCastManager.OnUserActionEndDelegate(this.OnFightMapUserActionEnd);
    }

    private void OnFightMapTargetChanged(bool hasTarget, CellObject cellObject)
    {
      if ((UnityEngine.Object) this.m_spellBeingCast == (UnityEngine.Object) null)
        return;
      this.CleanCastHighlight();
      if (hasTarget && (UnityEngine.Object) cellObject != (UnityEngine.Object) null)
      {
        DragNDropListener.instance.SnapDragToWorldPosition(CameraHandler.current.camera, cellObject.transform.position);
        this.m_spellBeingCast.OnEnterTarget();
        this.m_castHighlight = this.m_factory.CreateCastHighlight<SpellStatus>((SpellStatus) this.m_spellBeingCast.value, cellObject.highlight.transform);
      }
      else
      {
        DragNDropListener.instance.CancelSnapDrag();
        this.m_spellBeingCast.OnExitTarget();
      }
    }

    private void OnFightMapUserActionEnd(FightCastState state)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_spellBeingCast)
        return;
      this.m_doneCasting = state == FightCastState.DoneCasting;
      switch (state)
      {
        case FightCastState.Selecting:
          this.m_spellBeingCast.StartCast();
          DragNDropListener.instance.CancelSnapDrag();
          break;
        case FightCastState.Cancelled:
          this.m_spellBeingCast.CancelCast();
          DragNDropListener.instance.CancelSnapDrag();
          this.m_spellBeingCast = (SpellStatusCellRenderer) null;
          this.CleanCastHighlight();
          break;
        case FightCastState.Casting:
          this.m_spellBeingCast.StartCast();
          DragNDropListener.instance.CancelSnapDrag();
          break;
        case FightCastState.DoneCasting:
          this.m_spellBeingCast.DoneCasting();
          this.CleanCastHighlight();
          this.m_spellsInDoneCasting.Add(this.m_spellBeingCast);
          this.m_spellBeingCast = (SpellStatusCellRenderer) null;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (state), (object) state, (string) null);
      }
    }

    private void OnCastSpellDragBegin(SpellStatusCellRenderer spellStatus, bool click)
    {
      this.m_spellBeingCast = spellStatus;
      FightMap.current.SetTargetInputMode(click ? AbstractFightMap.TargetInputMode.Click : AbstractFightMap.TargetInputMode.Drag);
      FightCastManager.StartCastingSpell(this.m_playerStatus, (SpellStatus) spellStatus.value);
    }

    private void OnCastSpellDragEnd(SpellStatusCellRenderer cellRenderer, bool onTarget)
    {
      if (!this.m_spellsInDoneCasting.Contains(cellRenderer))
        return;
      if (!this.m_doneCasting && !onTarget)
        FightCastManager.StopCastingSpell(true);
      this.m_spellsInDoneCasting.Remove(cellRenderer);
    }

    private void CleanCastHighlight()
    {
      if (!((UnityEngine.Object) this.m_castHighlight != (UnityEngine.Object) null))
        return;
      this.m_factory.DestroyCellHighlight(this.m_castHighlight);
      this.m_castHighlight = (CastHighlight) null;
    }

    public void SetPlayerStatus(PlayerStatus playerStatus) => this.m_playerStatus = playerStatus;

    public void SetInteractable(bool interactable)
    {
      this.m_interactable = interactable;
      this.m_spellList.UpdateAllConfigurators();
    }

    public void AddSpellStatus(SpellStatus spellStatus)
    {
      SpellStatusData data = new SpellStatusData();
      CastValidityHelper.RecomputeSpellCastValidity(spellStatus.ownerPlayer, spellStatus, ref data);
      CastValidityHelper.RecomputeSpellCost(spellStatus, ref data);
      this.EnqueueSpellStatusData(spellStatus.instanceId, data);
      HashSet<EventCategory> eventsToAdd = new HashSet<EventCategory>();
      SpellDefinition definition = spellStatus.definition;
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) definition.eventsInvalidatingCost)
        eventsToAdd.Add(eventCategory);
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) definition.eventsInvalidatingCasting)
        eventsToAdd.Add(eventCategory);
      this.AddFightEventListeners(spellStatus, (IEnumerable<EventCategory>) eventsToAdd, true);
    }

    public IEnumerator AddSpell(SpellStatus spellStatus)
    {
      this.DequeueSpellStatusEvent(spellStatus.instanceId, false);
      this.m_spellStatusList.Add(spellStatus);
      this.m_spellList.Insert<SpellStatus>(this.m_spellStatusList.Count - 1, spellStatus);
      HashSet<EventCategory> eventsToAdd = new HashSet<EventCategory>();
      SpellDefinition definition = spellStatus.definition;
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) definition.eventsInvalidatingCost)
        eventsToAdd.Add(eventCategory);
      foreach (EventCategory eventCategory in (IEnumerable<EventCategory>) definition.eventsInvalidatingCasting)
        eventsToAdd.Add(eventCategory);
      this.AddFightEventListeners(spellStatus, (IEnumerable<EventCategory>) eventsToAdd, false);
      yield break;
    }

    public void RemoveSpellStatus(int spellInstanceId)
    {
      int index = 0;
      int count = this.m_spellStatusList.Count;
      while (index < count && this.m_spellStatusList[index].instanceId != spellInstanceId)
        ++index;
      if (index >= this.m_spellStatusList.Count)
        return;
      this.RemoveFightEventListeners(this.m_spellStatusList[index], true);
    }

    public IEnumerator RemoveSpell(int spellInstanceId)
    {
      while (this.m_spellsInDoneCasting.Count != 0)
        yield return (object) null;
      int index = 0;
      int count = this.m_spellStatusList.Count;
      while (index < count && this.m_spellStatusList[index].instanceId != spellInstanceId)
        ++index;
      if (index < this.m_spellStatusList.Count)
      {
        this.RemoveFightEventListeners(this.m_spellStatusList[index], false);
        this.m_spellStatusList.RemoveAt(index);
        this.m_spellList.RemoveAt(index);
        this.m_spellUsabilityInView.Remove(spellInstanceId);
      }
    }

    public void RefreshUsability(PlayerStatus status, bool recomputeCosts)
    {
      List<int> intList = ListPool<int>.Get();
      foreach (SpellStatus enumerateSpellStatu in status.EnumerateSpellStatus())
      {
        if (enumerateSpellStatu != null)
        {
          SpellStatusData data = new SpellStatusData();
          CastValidityHelper.RecomputeSpellCastValidity(enumerateSpellStatu.ownerPlayer, enumerateSpellStatu, ref data);
          CastValidityHelper.RecomputeSpellCost(enumerateSpellStatu, ref data);
          int instanceId = enumerateSpellStatu.instanceId;
          this.EnqueueSpellStatusData(instanceId, data);
          intList.Add(instanceId);
        }
      }
      this.m_refreshUsabilityQueue.Enqueue(intList);
    }

    public void UpdateUsability(bool recomputeCosts)
    {
      this.m_spellUsabilityInView.Clear();
      List<int> list = this.m_refreshUsabilityQueue.Dequeue();
      int index = 0;
      for (int count = list.Count; index < count; ++index)
      {
        int key = list[index];
        Queue<SpellStatusData> spellStatusDataQueue;
        if (this.m_spellUsabilityQueue.TryGetValue(key, out spellStatusDataQueue) && spellStatusDataQueue.Count != 0)
          this.m_spellUsabilityInView.Add(key, spellStatusDataQueue.Dequeue());
        else
          Log.Error(string.Format("No SpellStatusData found in queue for spellInstanceId {0}", (object) key), 326, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\FightRework\\SpellBar\\SpellBarRework.cs");
      }
      ListPool<int>.Release(list);
      this.m_spellList.UpdateAllConfigurators();
    }

    private void OnCastingValidityUpdated(EventCategory category)
    {
      List<int> intList;
      if (!this.m_spellsPerCategoryInvalidatingStatus.TryGetValue(category, out intList))
        return;
      int count = intList.Count;
      for (int index = 0; index < count; ++index)
      {
        SpellStatus spellStatus;
        if (this.m_playerStatus.TryGetSpell(intList[index], out spellStatus))
        {
          SpellStatusData data = new SpellStatusData();
          CastValidityHelper.RecomputeSpellCost(spellStatus, ref data);
          CastValidityHelper.RecomputeSpellCastValidity(spellStatus.ownerPlayer, spellStatus, ref data);
          this.EnqueueSpellStatusData(spellStatus.instanceId, data);
        }
      }
    }

    private void OnCastingValidityViewUpdated(EventCategory category)
    {
      List<int> intList;
      if (!this.m_spellsPerCategoryInvalidatingView.TryGetValue(category, out intList))
        return;
      foreach (int spellStatusId in intList)
        this.DequeueSpellStatusEvent(spellStatusId, true);
    }

    private void EnqueueSpellStatusData(int statusId, SpellStatusData spellStatusData)
    {
      Queue<SpellStatusData> spellStatusDataQueue;
      if (!this.m_spellUsabilityQueue.TryGetValue(statusId, out spellStatusDataQueue))
      {
        spellStatusDataQueue = this.m_queuePool.Get();
        this.m_spellUsabilityQueue.Add(statusId, spellStatusDataQueue);
      }
      spellStatusDataQueue.Enqueue(spellStatusData);
    }

    private void DequeueSpellStatusEvent(int spellStatusId, bool andUpdate)
    {
      Queue<SpellStatusData> spellStatusDataQueue;
      if (!this.m_spellUsabilityQueue.TryGetValue(spellStatusId, out spellStatusDataQueue))
        return;
      if (this.m_spellUsabilityInView.ContainsKey(spellStatusId))
        this.m_spellUsabilityInView[spellStatusId] = spellStatusDataQueue.Dequeue();
      else
        this.m_spellUsabilityInView.Add(spellStatusId, spellStatusDataQueue.Dequeue());
      SpellStatus spellStatus;
      if (!(this.TryGetSpellStatusByInstanceId(spellStatusId, out spellStatus) & andUpdate))
        return;
      this.m_spellList.UpdateConfiguratorWithValue((object) spellStatus);
    }

    private bool TryGetSpellStatusByInstanceId(int instanceId, out SpellStatus spellStatus)
    {
      int index = 0;
      for (int count = this.m_spellStatusList.Count; index < count; ++index)
      {
        SpellStatus spellStatus1 = this.m_spellStatusList[index];
        if (spellStatus1.instanceId == instanceId)
        {
          spellStatus = spellStatus1;
          return true;
        }
      }
      spellStatus = (SpellStatus) null;
      return false;
    }

    private void AddFightEventListeners(
      SpellStatus spellStatus,
      IEnumerable<EventCategory> eventsToAdd,
      bool status)
    {
      Dictionary<EventCategory, List<int>> dictionary = status ? this.m_spellsPerCategoryInvalidatingStatus : this.m_spellsPerCategoryInvalidatingView;
      foreach (EventCategory eventCategory in eventsToAdd)
      {
        List<int> intList;
        if (!dictionary.TryGetValue(eventCategory, out intList))
        {
          intList = ListPool<int>.Get();
          dictionary.Add(eventCategory, intList);
          if (status)
            FightLogicExecutor.AddListenerUpdateStatus(spellStatus.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityUpdated), eventCategory);
          else
            FightLogicExecutor.AddListenerUpdateView(spellStatus.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityViewUpdated), eventCategory);
        }
        intList.Add(spellStatus.instanceId);
      }
    }

    private void RemoveFightEventListeners(SpellStatus spellStatusToRemove, bool status)
    {
      List<EventCategory> list1 = ListPool<EventCategory>.Get();
      int instanceId = spellStatusToRemove.instanceId;
      Dictionary<EventCategory, List<int>> dictionary = status ? this.m_spellsPerCategoryInvalidatingStatus : this.m_spellsPerCategoryInvalidatingView;
      foreach (KeyValuePair<EventCategory, List<int>> keyValuePair in dictionary)
      {
        List<int> list2 = keyValuePair.Value;
        int index = 0;
        for (int count = list2.Count; index < count; ++index)
        {
          if (list2[index] == instanceId)
          {
            list2.RemoveAt(index);
            break;
          }
        }
        if (list2.Count == 0)
        {
          list1.Add(keyValuePair.Key);
          ListPool<int>.Release(list2);
        }
      }
      foreach (EventCategory eventCategory in list1)
      {
        if (status)
          FightLogicExecutor.RemoveListenerUpdateStatus(spellStatusToRemove.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityUpdated), eventCategory);
        else
          FightLogicExecutor.RemoveListenerUpdateView(spellStatusToRemove.ownerPlayer.fightId, new Action<EventCategory>(this.OnCastingValidityViewUpdated), eventCategory);
        dictionary.Remove(eventCategory);
      }
      ListPool<EventCategory>.Release(list1);
    }

    public FightTooltip tooltip => this.m_tooltip;

    public TooltipPosition tooltipPosition => this.m_tooltipPosition;

    public IDragNDropValidator GetDragNDropValidator() => (IDragNDropValidator) this;

    public bool IsParentInteractable() => this.m_interactable;

    public SpellStatusData? GetSpellStatusData(SpellStatus spellStatus)
    {
      if (spellStatus == null)
        return new SpellStatusData?();
      SpellStatusData spellStatusData;
      return this.m_spellUsabilityInView.TryGetValue(spellStatus.instanceId, out spellStatusData) ? new SpellStatusData?(spellStatusData) : new SpellStatusData?();
    }

    public CastEventListener GetEventListener() => this.m_castEventListener;

    public bool IsValidDrag(object value)
    {
      if (FightCastManager.currentCastType != FightCastManager.CurrentCastType.None)
        return false;
      SpellStatus spellStatus = (SpellStatus) value;
      if (CastValidityHelper.ComputeSpellCostCastValidity(this.m_playerStatus, spellStatus) != CastValidity.SUCCESS)
        return false;
      CastValidity spellCastValidity = CastValidityHelper.ComputeSpellCastValidity(this.m_playerStatus, spellStatus);
      if (spellCastValidity != CastValidity.SUCCESS)
        NotificationWindowManager.DisplayNotification(TextCollectionUtility.GetFormattedText(spellCastValidity));
      return spellCastValidity == CastValidity.SUCCESS;
    }

    public bool IsValidDrop(object value) => true;

    private void Update()
    {
      if (!InputUtility.GetSecondaryUp() || FightCastManager.currentCastType != FightCastManager.CurrentCastType.Spell || FightCastManager.currentCastState >= FightCastState.Casting)
        return;
      FightCastManager.StopCastingSpell(true);
    }
  }
}
