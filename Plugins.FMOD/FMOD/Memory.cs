﻿// Decompiled with JetBrains decompiler
// Type: FMOD.Memory
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using System;
using System.Runtime.InteropServices;

namespace FMOD
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct Memory
  {
    public static RESULT Initialize(
      IntPtr poolmem,
      int poollen,
      MEMORY_ALLOC_CALLBACK useralloc,
      MEMORY_REALLOC_CALLBACK userrealloc,
      MEMORY_FREE_CALLBACK userfree,
      MEMORY_TYPE memtypeflags)
    {
      return Memory.FMOD5_Memory_Initialize(poolmem, poollen, useralloc, userrealloc, userfree, memtypeflags);
    }

    public static RESULT GetStats(out int currentalloced, out int maxalloced) => Memory.GetStats(out currentalloced, out maxalloced, false);

    public static RESULT GetStats(out int currentalloced, out int maxalloced, bool blocking) => Memory.FMOD5_Memory_GetStats(out currentalloced, out maxalloced, blocking);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD5_Memory_Initialize(
      IntPtr poolmem,
      int poollen,
      MEMORY_ALLOC_CALLBACK useralloc,
      MEMORY_REALLOC_CALLBACK userrealloc,
      MEMORY_FREE_CALLBACK userfree,
      MEMORY_TYPE memtypeflags);

    [DllImport("fmodstudio")]
    private static extern RESULT FMOD5_Memory_GetStats(
      out int currentalloced,
      out int maxalloced,
      bool blocking);
  }
}
