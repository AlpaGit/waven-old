// Decompiled with JetBrains decompiler
// Type: Ankama.Launcher.Zaap.ZaapWorker
// Assembly: Plugins.Launcher, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF8BAEB0-9B2E-4104-A005-EC2914290F3D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.Launcher.dll

using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using Zaap_CSharp_Client;

namespace Ankama.Launcher.Zaap
{
  internal class ZaapWorker
  {
    private volatile ZaapWorker.WorkerStatus m_status;
    private Thread m_workerThread;
    private BlockingQueue<ZaapTask> m_tasksQueue = new BlockingQueue<ZaapTask>();
    private ConcurrentQueue<Action> m_tasksDone = new ConcurrentQueue<Action>();
    private ZaapClient m_zaapClient;

    public bool Stop()
    {
      if (this.IsNot(ZaapWorker.WorkerStatus.STARTED) || this.IsNot(ZaapWorker.WorkerStatus.STARTING) || this.m_workerThread == null)
        return false;
      this.m_status = ZaapWorker.WorkerStatus.STOPPING;
      this.m_zaapClient = (ZaapClient) null;
      this.m_tasksQueue.Close();
      return true;
    }

    public bool Start()
    {
      Debug.Log((object) "Trying connection to Zaap");
      if (this.Is(ZaapWorker.WorkerStatus.STARTED) || this.Is(ZaapWorker.WorkerStatus.STARTING))
        return false;
      this.m_status = ZaapWorker.WorkerStatus.STARTING;
      this.m_zaapClient = ZaapClient.Connect();
      if (this.m_zaapClient == null)
      {
        Debug.LogWarning((object) "ZaapClient not created. Unable to start ZaapWorker");
        this.m_status = ZaapWorker.WorkerStatus.STOPPED;
        return false;
      }
      this.m_workerThread = new Thread(new ThreadStart(this.DoStart));
      this.m_workerThread.Start();
      return true;
    }

    public void AddTask(ZaapTask task) => this.m_tasksQueue.Enqueue(task);

    public bool TryGetResult(out Action result) => this.m_tasksDone.TryDequeue(out result);

    public ZaapWorker.WorkerStatus Status => this.m_status;

    public bool Is(ZaapWorker.WorkerStatus status) => this.m_status == status;

    public bool IsNot(ZaapWorker.WorkerStatus status) => this.m_status != status;

    private void DoStart()
    {
      ZaapClient zaapClient = this.m_zaapClient;
      this.m_status = ZaapWorker.WorkerStatus.STARTED;
      try
      {
        while (this.Is(ZaapWorker.WorkerStatus.STARTED))
        {
          ZaapTask zaapTask;
          if (this.m_tasksQueue.TryDequeue(out zaapTask))
          {
            Action action = zaapTask.Execute(zaapClient);
            if (action != null)
              this.m_tasksDone.Enqueue(action);
          }
        }
      }
      finally
      {
        this.m_status = ZaapWorker.WorkerStatus.STOPPED;
        zaapClient?.Disconnect();
      }
    }

    internal enum WorkerStatus
    {
      STOPPED,
      STARTING,
      STOPPING,
      STARTED,
    }
  }
}
