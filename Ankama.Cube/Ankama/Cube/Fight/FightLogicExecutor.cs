// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.FightLogicExecutor
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Fight.Events;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Fight
{
  public static class FightLogicExecutor
  {
    private static bool s_running;
    private static FightLogicExecutor.Instance[] s_instances;
    private static readonly Dictionary<long, FightEvent> s_eventHierarchyBuffer = new Dictionary<long, FightEvent>();
    private static readonly List<FightEvent> s_eventGroupBuffer = new List<FightEvent>(8);
    public static bool fightInitialized;
    private static long s_synchronizationBarrierId;
    private static FightLogicExecutor.SynchronizationBarrier s_synchronizationBarrier;

    public static bool isValid => FightLogicExecutor.s_instances != null;

    public static int fightCount => FightLogicExecutor.s_instances.Length;

    public static void Initialize(int count)
    {
      FightLogicExecutor.fightInitialized = false;
      FightLogicExecutor.s_instances = new FightLogicExecutor.Instance[count];
    }

    public static void AddFightStatus(FightStatus fightStatus) => FightLogicExecutor.s_instances[fightStatus.fightId] = new FightLogicExecutor.Instance(fightStatus);

    public static void Start()
    {
      if (FightLogicExecutor.s_running)
        return;
      FightLogicExecutor.s_running = true;
      Main.monoBehaviour.StartCoroutineImmediateSafe(FightLogicExecutor.Run());
    }

    public static void Stop()
    {
      if (!FightLogicExecutor.s_running)
        return;
      FightLogicExecutor.s_running = false;
    }

    public static FightStatus GetFightStatus(int fightId) => FightLogicExecutor.s_instances[fightId].fightStatus;

    public static void ProcessFightEvents(int fightId, List<FightEvent> fightEvents) => FightLogicExecutor.s_instances[fightId].ProcessFightEvents(fightEvents);

    public static void NotifyEntityRemoved(int fightId) => FightLogicExecutor.s_instances[fightId].NotifyEntityRemoved();

    public static void NotifySpellRemovedForPlayer(int fightId, PlayerStatus playerStatus) => FightLogicExecutor.s_instances[fightId].NotifySpellRemovedForPlayer(playerStatus);

    private static IEnumerator Run()
    {
      FightLogicExecutor.Instance[] instances = FightLogicExecutor.s_instances;
      int instanceCount = instances.Length;
      IEnumerator[] routines = new IEnumerator[instanceCount];
      for (int index = 0; index < instanceCount; ++index)
        routines[index] = instances[index].Tick();
      while (FightLogicExecutor.s_running)
      {
        for (int index = 0; index < instanceCount; ++index)
          routines[index].MoveNextRecursiveImmediateSafe();
        yield return (object) null;
      }
      for (int index = 0; index < instanceCount; ++index)
        FightLogicExecutor.s_instances[index].Clear();
      FightLogicExecutor.s_instances = (FightLogicExecutor.Instance[]) null;
      FightLogicExecutor.s_synchronizationBarrierId = 0L;
      FightLogicExecutor.s_synchronizationBarrier = new FightLogicExecutor.SynchronizationBarrier();
    }

    public static void AddListenerUpdateStatus(
      int fightId,
      Action<EventCategory> listener,
      EventCategory eventCategoriesToListen)
    {
      FightLogicExecutor.s_instances[fightId].AddListenerUpdateStatus(listener, eventCategoriesToListen);
    }

    public static void AddListenerUpdateView(
      int fightId,
      Action<EventCategory> listener,
      EventCategory eventCategoriesToListen)
    {
      FightLogicExecutor.s_instances[fightId].AddListenerUpdateView(listener, eventCategoriesToListen);
    }

    public static void RemoveListenerUpdateStatus(
      int fightId,
      Action<EventCategory> listener,
      EventCategory category)
    {
      if (FightLogicExecutor.s_instances == null)
        return;
      FightLogicExecutor.s_instances[fightId].RemoveListenerUpdateStatus(listener, category);
    }

    public static void RemoveListenerUpdateView(
      int fightId,
      Action<EventCategory> listener,
      EventCategory category)
    {
      if (FightLogicExecutor.s_instances == null)
        return;
      FightLogicExecutor.s_instances[fightId].RemoveListenerUpdateView(listener, category);
    }

    public static void FireUpdateStatus(int fightId, EventCategory category) => FightLogicExecutor.s_instances[fightId].FireUpdateStatus(category);

    public static void FireUpdateView(int fightId, EventCategory category) => FightLogicExecutor.s_instances[fightId].FireUpdateView(category);

    private static IEnumerator SetupSynchronizationBarrier()
    {
      FightLogicExecutor.SynchronizationBarrier synchronizationBarrier = FightLogicExecutor.s_synchronizationBarrier;
      if (synchronizationBarrier.counter == 0)
        FightLogicExecutor.s_synchronizationBarrier = new FightLogicExecutor.SynchronizationBarrier(FightLogicExecutor.s_synchronizationBarrierId++);
      else if (synchronizationBarrier.maxCounter == FightLogicExecutor.s_instances.Length)
      {
        Log.Error("Synchronization error: tried to setup a barrier but the current barrier has already reached all instances.", 55, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Synchronization.cs");
      }
      else
      {
        FightLogicExecutor.s_synchronizationBarrier = FightLogicExecutor.SynchronizationBarrier.Upgrade(synchronizationBarrier);
        yield break;
      }
    }

    private static IEnumerator ReleaseSynchronizationBarrier()
    {
      FightLogicExecutor.SynchronizationBarrier synchronizationBarrier1 = FightLogicExecutor.s_synchronizationBarrier;
      long barrierId = synchronizationBarrier1.id;
      if (synchronizationBarrier1.counter == 0)
      {
        Log.Error("Synchronization error: tried to release a barrier but no barrier is currently setup.", 69, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Synchronization.cs");
      }
      else
      {
        int instanceCount = FightLogicExecutor.s_instances.Length;
        while (FightLogicExecutor.s_synchronizationBarrier.maxCounter != instanceCount)
        {
          yield return (object) null;
          if (!FightLogicExecutor.s_running)
            break;
        }
        FightLogicExecutor.s_synchronizationBarrier = FightLogicExecutor.SynchronizationBarrier.Downgrade(FightLogicExecutor.s_synchronizationBarrier);
        do
        {
          FightLogicExecutor.SynchronizationBarrier synchronizationBarrier2 = FightLogicExecutor.s_synchronizationBarrier;
          if (synchronizationBarrier2.id == barrierId && synchronizationBarrier2.counter != 0)
            yield return (object) null;
          else
            goto label_9;
        }
        while (FightLogicExecutor.s_running);
        goto label_7;
label_9:
        yield break;
label_7:;
      }
    }

    private class DirtySpellsCounter
    {
      private readonly List<PlayerStatus> m_keys;
      private readonly List<int> m_values;

      public DirtySpellsCounter(int capacity)
      {
        this.m_keys = new List<PlayerStatus>(capacity);
        this.m_values = new List<int>(capacity);
      }

      public int count => this.m_keys.Count;

      public bool GetAt(int index, out PlayerStatus key, out int value)
      {
        if (index >= 0 && index < this.m_values.Count)
        {
          key = this.m_keys[index];
          value = this.m_values[index];
          return key != null && value > 0;
        }
        key = (PlayerStatus) null;
        value = 0;
        return false;
      }

      public void ResetAll()
      {
        int count = this.m_values.Count;
        for (int index = 0; index < count; ++index)
          this.m_values[index] = 0;
      }

      public void Increment(PlayerStatus key, int value)
      {
        int index = this.m_keys.IndexOf(key);
        if (index >= 0 && index < this.m_values.Count)
        {
          this.m_values[index] += value;
        }
        else
        {
          this.m_keys.Add(key);
          this.m_values.Add(value);
        }
      }
    }

    private class Instance
    {
      public readonly FightStatus fightStatus;
      private int m_dirtyEntitiesCounter;
      private readonly FightLogicExecutor.DirtySpellsCounter m_dirtySpellsCounters = new FightLogicExecutor.DirtySpellsCounter(2);
      private IEnumerator m_current;
      private bool m_awaitsSynchronization;
      private readonly Queue<IEnumerator> m_executionQueue = new Queue<IEnumerator>();
      private readonly Dictionary<EventCategory, List<Action<EventCategory>>> m_listenerUpdateStatus = new Dictionary<EventCategory, List<Action<EventCategory>>>();
      private readonly Dictionary<EventCategory, List<Action<EventCategory>>> m_listenerUpdateView = new Dictionary<EventCategory, List<Action<EventCategory>>>();

      public Instance(FightStatus fightStatus) => this.fightStatus = fightStatus;

      public IEnumerator Tick()
      {
        while (true)
        {
          while (this.m_current == null)
            yield return (object) null;
          yield return (object) this.m_current;
          this.m_current = this.m_executionQueue.Count <= 0 ? (IEnumerator) null : this.m_executionQueue.Dequeue();
        }
      }

      public void Clear()
      {
        this.fightStatus.Dispose();
        this.m_executionQueue.Clear();
        this.m_current = (IEnumerator) null;
      }

      public void ProcessFightEvents(List<FightEvent> fightEvents)
      {
        this.ProcessFightEventsUpdateStatus(fightEvents, this.fightStatus);
        this.ProcessFightEventsUpdateViews(fightEvents, this.fightStatus);
        this.CleanUpFightStatus();
      }

      public void NotifyEntityRemoved() => ++this.m_dirtyEntitiesCounter;

      public void NotifySpellRemovedForPlayer(PlayerStatus playerStatus) => this.m_dirtySpellsCounters.Increment(playerStatus, 1);

      private void ProcessFightEventsUpdateStatus(
        List<FightEvent> fightEvents,
        FightStatus activeFightStatus)
      {
        this.m_dirtyEntitiesCounter = 0;
        this.m_dirtySpellsCounters.ResetAll();
        int count = fightEvents.Count;
        for (int index = 0; index < count; ++index)
        {
          FightEvent fightEvent = fightEvents[index];
          int? parentEventId = fightEvent.parentEventId;
          long? nullable = parentEventId.HasValue ? new long?((long) parentEventId.GetValueOrDefault()) : new long?();
          if (nullable.HasValue)
            FightLogicExecutor.s_eventHierarchyBuffer[nullable.Value].AddChildEvent(fightEvent);
          try
          {
            fightEvent.UpdateStatus(activeFightStatus);
          }
          catch (Exception ex)
          {
            Log.Error(string.Format("Exception occured while event {0} #{1} updated fight status.", (object) fightEvent.eventType, (object) fightEvent.eventId), 139, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
            Debug.LogException(ex);
          }
          FightLogicExecutor.s_eventHierarchyBuffer.Add((long) fightEvent.eventId, fightEvent);
        }
        try
        {
          activeFightStatus.TriggerUpdateEvents();
        }
        catch (Exception ex)
        {
          Log.Error(string.Format("Exception occured while triggering update events of fight status #{0}.", (object) activeFightStatus.fightId), 158, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
          Debug.LogException(ex);
        }
        FightLogicExecutor.s_eventHierarchyBuffer.Clear();
      }

      private void ProcessFightEventsUpdateViews(
        List<FightEvent> fightEvents,
        FightStatus activeFightStatus)
      {
        int index = 1;
        int count = fightEvents.Count;
        FightEvent other = fightEvents[0];
        FightLogicExecutor.s_eventGroupBuffer.Add(other);
        FightEvent fightEvent1;
        for (; index < count && other.IsInvisible(); other = fightEvent1)
        {
          fightEvent1 = fightEvents[index];
          ++index;
          FightLogicExecutor.s_eventGroupBuffer.Add(fightEvent1);
        }
        while (index < count)
        {
          FightEvent fightEvent2 = fightEvents[index];
          ++index;
          if (!fightEvent2.IsInvisible() && !fightEvent2.CanBeGroupedWith(other))
            this.SendFightEventGroupToExecution(activeFightStatus);
          FightLogicExecutor.s_eventGroupBuffer.Add(fightEvent2);
          other = fightEvent2;
        }
        this.SendFightEventGroupToExecution(activeFightStatus);
      }

      private void SendFightEventGroupToExecution(FightStatus activeFightStatus)
      {
        int count = FightLogicExecutor.s_eventGroupBuffer.Count;
        if (count == 1)
        {
          FightEvent fightEvent = FightLogicExecutor.s_eventGroupBuffer[0];
          try
          {
            if (fightEvent.SynchronizeExecution())
            {
              this.Execute(FightLogicExecutor.SetupSynchronizationBarrier());
              IEnumerator action = fightEvent.UpdateView(activeFightStatus);
              if (action != null)
                this.Execute(action);
              this.Execute(FightLogicExecutor.ReleaseSynchronizationBarrier());
            }
            else
            {
              IEnumerator action = fightEvent.UpdateView(activeFightStatus);
              if (action != null)
                this.Execute(action);
            }
          }
          catch (Exception ex)
          {
            Log.Error(string.Format("Exception occured while event {0} #{1} updated fight view.", (object) fightEvent.eventType, (object) fightEvent.eventId), 250, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
            Debug.LogException(ex);
          }
        }
        else
        {
          IEnumerator[] enumeratorArray = new IEnumerator[count];
          for (int index = 0; index < count; ++index)
          {
            FightEvent fightEvent = FightLogicExecutor.s_eventGroupBuffer[index];
            try
            {
              enumeratorArray[index] = fightEvent.UpdateView(activeFightStatus);
            }
            catch (Exception ex)
            {
              Log.Error(string.Format("Exception occured while event {0} #{1} updated fight view.", (object) fightEvent.eventType, (object) fightEvent.eventId), 268, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
              Debug.LogException(ex);
            }
          }
          this.Execute(EnumeratorUtility.ParallelRecursiveImmediateExecution(enumeratorArray));
        }
        FightLogicExecutor.s_eventGroupBuffer.Clear();
      }

      private void Execute([NotNull] IEnumerator action)
      {
        if (this.m_current == null)
          this.m_current = action;
        else
          this.m_executionQueue.Enqueue(action);
      }

      private void CleanUpFightStatus()
      {
        if (this.m_dirtyEntitiesCounter > 0)
          this.Execute(this.Cleanup(this.m_dirtyEntitiesCounter));
        int count = this.m_dirtySpellsCounters.count;
        for (int index = 0; index < count; ++index)
        {
          PlayerStatus key;
          int counter;
          if (this.m_dirtySpellsCounters.GetAt(index, out key, out counter))
            this.Execute(key.CleanupDirtySpells(counter));
        }
        this.Execute(this.ClearSpellEffectOverrides());
      }

      private IEnumerator Cleanup(int dirtyEntitiesCounter)
      {
        this.fightStatus.Cleanup(dirtyEntitiesCounter);
        yield break;
      }

      private IEnumerator ClearSpellEffectOverrides()
      {
        FightSpellEffectFactory.ClearSpellEffectOverrides(this.fightStatus.fightId);
        yield break;
      }

      public void AddListenerUpdateStatus(
        Action<EventCategory> listener,
        EventCategory eventCategoryToListen)
      {
        List<Action<EventCategory>> actionList;
        if (!this.m_listenerUpdateStatus.TryGetValue(eventCategoryToListen, out actionList))
        {
          actionList = new List<Action<EventCategory>>();
          this.m_listenerUpdateStatus.Add(eventCategoryToListen, actionList);
        }
        actionList.Add(listener);
      }

      public void AddListenerUpdateView(Action<EventCategory> listener, EventCategory category)
      {
        List<Action<EventCategory>> actionList;
        if (!this.m_listenerUpdateView.TryGetValue(category, out actionList))
        {
          actionList = new List<Action<EventCategory>>();
          this.m_listenerUpdateView.Add(category, actionList);
        }
        actionList.Add(listener);
      }

      public void RemoveListenerUpdateStatus(Action<EventCategory> listener, EventCategory category)
      {
        List<Action<EventCategory>> actionList;
        if (this.m_listenerUpdateStatus.TryGetValue(category, out actionList) && actionList.Remove(listener))
          return;
        Log.Error(string.Format("Try to remove an unknown status listener for event category {0}.", (object) category), 365, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
      }

      public void RemoveListenerUpdateView(Action<EventCategory> listener, EventCategory category)
      {
        List<Action<EventCategory>> actionList;
        if (this.m_listenerUpdateView.TryGetValue(category, out actionList) && actionList.Remove(listener))
          return;
        Log.Error(string.Format("Try to remove an unknown view listener for event category {0}.", (object) category), 378, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Fight\\FightLogicExecutor.Instance.cs");
      }

      public void FireUpdateStatus(EventCategory category)
      {
        List<Action<EventCategory>> actionList;
        if (!this.m_listenerUpdateStatus.TryGetValue(category, out actionList))
          return;
        int index = 0;
        for (int count = actionList.Count; index < count; ++index)
          actionList[index](category);
      }

      public void FireUpdateView(EventCategory category)
      {
        List<Action<EventCategory>> actionList;
        if (!this.m_listenerUpdateView.TryGetValue(category, out actionList))
          return;
        int index = 0;
        for (int count = actionList.Count; index < count; ++index)
          actionList[index](category);
      }
    }

    private struct SynchronizationBarrier
    {
      public readonly long id;
      public readonly int counter;
      public readonly int maxCounter;

      public SynchronizationBarrier(long id)
      {
        this.id = id;
        this.counter = 1;
        this.maxCounter = 1;
      }

      private SynchronizationBarrier(long id, int counter, int maxCounter)
      {
        this.id = id;
        this.counter = counter;
        this.maxCounter = maxCounter;
      }

      public static FightLogicExecutor.SynchronizationBarrier Upgrade(
        FightLogicExecutor.SynchronizationBarrier barrier)
      {
        return new FightLogicExecutor.SynchronizationBarrier(barrier.id, barrier.counter + 1, barrier.maxCounter + 1);
      }

      public static FightLogicExecutor.SynchronizationBarrier Downgrade(
        FightLogicExecutor.SynchronizationBarrier barrier)
      {
        return new FightLogicExecutor.SynchronizationBarrier(barrier.id, barrier.counter - 1, barrier.maxCounter);
      }
    }
  }
}
