// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Management.Animator2DManager
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using Ankama.Animations.Rendering;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ankama.Animations.Management
{
  internal static class Animator2DManager
  {
    private static Thread s_thread;
    private static List<Animator2D> s_objects;
    private static Queue<BufferRequest> s_bufferRequests;
    private static SpinLock s_bufferRequestsLock = new SpinLock(false);
    private static readonly AutoResetEvent s_autoResetEvent = new AutoResetEvent(false);

    static Animator2DManager()
    {
      GameObject gameObject = new GameObject("Animator2DManagerCallbackSource", new System.Type[1]
      {
        typeof (Animator2DManagerCallbackSource)
      });
      Animator2DManager.s_thread = new Thread(new ThreadStart(Animator2DManager.Buffer));
      Animator2DManager.s_objects = new List<Animator2D>(32);
      Animator2DManager.s_bufferRequests = new Queue<BufferRequest>(32);
      Animator2DManager.s_thread.Start();
    }

    internal static void Release()
    {
      if (Animator2DManager.s_thread != null)
      {
        if (Animator2DManager.s_thread.IsAlive)
          Animator2DManager.s_thread.Abort();
        Animator2DManager.s_thread = (Thread) null;
      }
      if (Animator2DManager.s_objects != null)
      {
        Animator2DManager.s_objects.Clear();
        Animator2DManager.s_objects = (List<Animator2D>) null;
      }
      if (Animator2DManager.s_bufferRequests == null)
        return;
      bool lockTaken = false;
      try
      {
        Animator2DManager.s_bufferRequestsLock.Enter(ref lockTaken);
        Animator2DManager.s_bufferRequests.Clear();
        Animator2DManager.s_bufferRequests = (Queue<BufferRequest>) null;
      }
      finally
      {
        if (lockTaken)
          Animator2DManager.s_bufferRequestsLock.Exit(false);
      }
    }

    internal static void Update()
    {
      float time = Time.time;
      int count = Animator2DManager.s_objects.Count;
      for (int index = 0; index < count; ++index)
        Animator2DManager.s_objects[index].Run(time);
    }

    private static void Buffer()
    {
      BufferRequest bufferRequest = new BufferRequest((Animator2D) null, (AnimationInstance) null, 0);
      while (true)
      {
        bool flag = false;
        bool lockTaken = false;
        try
        {
          Animator2DManager.s_bufferRequestsLock.Enter(ref lockTaken);
          if (Animator2DManager.s_bufferRequests.Count > 0)
          {
            bufferRequest = Animator2DManager.s_bufferRequests.Dequeue();
            flag = true;
          }
        }
        finally
        {
          if (lockTaken)
            Animator2DManager.s_bufferRequestsLock.Exit(false);
        }
        if (flag)
          bufferRequest.target.Buffer(bufferRequest.id, bufferRequest.animation);
        else
          Animator2DManager.s_autoResetEvent.WaitOne();
      }
    }

    internal static void Register(Animator2D obj)
    {
      if (Animator2DManager.s_objects == null)
        return;
      Animator2DManager.s_objects.Add(obj);
    }

    internal static void Unregister(Animator2D obj)
    {
      if (Animator2DManager.s_objects == null)
        return;
      int count = Animator2DManager.s_objects.Count;
      for (int index = 0; index < count; ++index)
      {
        if ((UnityEngine.Object) Animator2DManager.s_objects[index] == (UnityEngine.Object) obj)
        {
          Animator2DManager.s_objects.RemoveAt(index);
          return;
        }
      }
      Debug.LogWarning((object) ("[Animator2D] Tried to unregister an unregistered Animator2D named '" + obj.name + "'."));
    }

    internal static void AddBufferRequest(BufferRequest request)
    {
      bool lockTaken = false;
      try
      {
        Animator2DManager.s_bufferRequestsLock.Enter(ref lockTaken);
        Animator2DManager.s_bufferRequests.Enqueue(request);
      }
      finally
      {
        if (lockTaken)
          Animator2DManager.s_bufferRequestsLock.Exit(false);
      }
      Animator2DManager.s_autoResetEvent.Set();
    }
  }
}
