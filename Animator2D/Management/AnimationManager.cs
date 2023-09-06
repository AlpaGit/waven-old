// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Management.AnimationManager
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;
using System.Collections.Generic;

namespace Ankama.Animations.Management
{
  internal static class AnimationManager
  {
    private static readonly Dictionary<string, AnimationInstance> s_references;
    private static AnimationInstance[] s_buffer;
    private static int s_bufferCapacity = 32;
    private static int s_bufferSize;
    private static int s_bufferStartIndex;
    private static int s_bufferNextIndex;

    static AnimationManager()
    {
      AnimationManager.s_references = new Dictionary<string, AnimationInstance>((IEqualityComparer<string>) StringComparer.Ordinal);
      AnimationManager.s_buffer = new AnimationInstance[AnimationManager.s_bufferCapacity];
    }

    public static void SetBufferCapacity(int size)
    {
      if (size == AnimationManager.s_bufferCapacity)
        return;
      if (AnimationManager.s_buffer != null)
      {
        if (size <= 0)
        {
          Array.Resize<AnimationInstance>(ref AnimationManager.s_buffer, 0);
          size = 0;
          AnimationManager.s_bufferStartIndex = 0;
          AnimationManager.s_bufferNextIndex = 0;
        }
        else
        {
          AnimationInstance[] animationInstanceArray = new AnimationInstance[size];
          int num = size <= AnimationManager.s_bufferSize ? size : AnimationManager.s_bufferSize;
          for (int index1 = 1; index1 <= num; ++index1)
          {
            int index2 = (AnimationManager.s_bufferCapacity + AnimationManager.s_bufferNextIndex - num) % AnimationManager.s_bufferCapacity;
            int index3 = num - index1;
            animationInstanceArray[index3] = AnimationManager.s_buffer[index2];
          }
          AnimationManager.s_buffer = animationInstanceArray;
          AnimationManager.s_bufferStartIndex = 0;
          AnimationManager.s_bufferNextIndex = num % size;
        }
      }
      AnimationManager.s_bufferCapacity = size;
    }

    public static bool TryGetAnimationInstance(string guid, out AnimationInstance instance)
    {
      if (AnimationManager.s_references.TryGetValue(guid, out instance))
      {
        ++instance.referenceCount;
        return true;
      }
      if (AnimationManager.s_bufferSize > 0)
      {
        for (int index1 = AnimationManager.s_bufferSize - 1; index1 > 0; --index1)
        {
          int index2 = (AnimationManager.s_bufferStartIndex + index1) % AnimationManager.s_bufferCapacity;
          instance = AnimationManager.s_buffer[index2];
          if (instance.guid.Equals(guid))
          {
            for (int index3 = AnimationManager.s_bufferSize - index1 - 1; index3 > 0; --index3)
            {
              int index4 = (index2 + 1) % AnimationManager.s_bufferCapacity;
              AnimationManager.s_buffer[index2] = AnimationManager.s_buffer[index4];
              index2 = index4;
            }
            --AnimationManager.s_bufferSize;
            AnimationManager.s_buffer[index2] = (AnimationInstance) null;
            AnimationManager.s_bufferNextIndex = index2;
            instance.referenceCount = 1;
            AnimationManager.s_references.Add(guid, instance);
            return true;
          }
        }
        instance = AnimationManager.s_buffer[AnimationManager.s_bufferStartIndex];
        if (instance.guid.Equals(guid))
        {
          --AnimationManager.s_bufferSize;
          AnimationManager.s_buffer[AnimationManager.s_bufferStartIndex] = (AnimationInstance) null;
          AnimationManager.s_bufferStartIndex = (AnimationManager.s_bufferNextIndex - AnimationManager.s_bufferSize + AnimationManager.s_bufferCapacity) % AnimationManager.s_bufferCapacity;
          AnimationManager.s_references.Add(guid, instance);
          instance.referenceCount = 1;
          return true;
        }
      }
      instance = (AnimationInstance) null;
      return false;
    }

    public static AnimationInstance CreateAnimationInstance(string guid, byte[] data)
    {
      AnimationInstance animationInstance = new AnimationInstance(guid, data);
      AnimationManager.s_references.Add(guid, animationInstance);
      return animationInstance;
    }

    public static bool ReleaseAnimationInstance(string guid)
    {
      AnimationInstance reference = AnimationManager.s_references[guid];
      --reference.referenceCount;
      if (reference.referenceCount != 0)
        return false;
      AnimationManager.s_references.Remove(guid);
      if (AnimationManager.s_bufferCapacity > 0)
      {
        if (AnimationManager.s_bufferSize == AnimationManager.s_bufferCapacity)
        {
          AnimationManager.s_buffer[AnimationManager.s_bufferNextIndex] = reference;
          AnimationManager.s_bufferNextIndex = (AnimationManager.s_bufferNextIndex + 1) % AnimationManager.s_bufferCapacity;
          AnimationManager.s_bufferStartIndex = AnimationManager.s_bufferNextIndex;
        }
        else
        {
          AnimationManager.s_buffer[AnimationManager.s_bufferNextIndex] = reference;
          AnimationManager.s_bufferNextIndex = (AnimationManager.s_bufferNextIndex + 1) % AnimationManager.s_bufferCapacity;
          ++AnimationManager.s_bufferSize;
        }
      }
      return true;
    }
  }
}
