// Decompiled with JetBrains decompiler
// Type: FMODUnity.FMODUtility
// Assembly: Plugins.FMOD, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F69CB912-BE3C-4720-BEEF-CCB5E09BA41B
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.FMOD.dll

using FMOD;
using FMOD.Studio;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace FMODUnity
{
  public static class FMODUtility
  {
    private static FMODPlatform s_currentPlatform;

    [PublicAPI]
    public static FMODPlatform currentPlatform
    {
      get
      {
        if (FMODUtility.s_currentPlatform == FMODPlatform.None)
          FMODUtility.s_currentPlatform = FMODUtility.GetCurrentPlatform();
        return FMODUtility.s_currentPlatform;
      }
    }

    [PublicAPI]
    public static VECTOR ToFMODVector(Vector3 vec)
    {
      VECTOR fmodVector;
      fmodVector.x = vec.x;
      fmodVector.y = vec.y;
      fmodVector.z = vec.z;
      return fmodVector;
    }

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes(Vector3 pos) => new ATTRIBUTES_3D()
    {
      forward = FMODUtility.ToFMODVector(Vector3.forward),
      up = FMODUtility.ToFMODVector(Vector3.up),
      position = FMODUtility.ToFMODVector(pos)
    };

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] Transform transform) => new ATTRIBUTES_3D()
    {
      forward = FMODUtility.ToFMODVector(transform.forward),
      up = FMODUtility.ToFMODVector(transform.up),
      position = FMODUtility.ToFMODVector(transform.position)
    };

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] Transform transform, [NotNull] Rigidbody rigidbody) => new ATTRIBUTES_3D()
    {
      forward = FMODUtility.ToFMODVector(transform.forward),
      up = FMODUtility.ToFMODVector(transform.up),
      position = FMODUtility.ToFMODVector(transform.position),
      velocity = FMODUtility.ToFMODVector(rigidbody.velocity)
    };

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] Transform transform, [NotNull] Rigidbody2D rigidbody)
    {
      Vector2 velocity = rigidbody.velocity;
      VECTOR vector;
      vector.x = velocity.x;
      vector.y = velocity.y;
      vector.z = 0.0f;
      return new ATTRIBUTES_3D()
      {
        forward = FMODUtility.ToFMODVector(transform.forward),
        up = FMODUtility.ToFMODVector(transform.up),
        position = FMODUtility.ToFMODVector(transform.position),
        velocity = vector
      };
    }

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] GameObject go)
    {
      Transform transform = go.transform;
      return new ATTRIBUTES_3D()
      {
        forward = FMODUtility.ToFMODVector(transform.forward),
        up = FMODUtility.ToFMODVector(transform.up),
        position = FMODUtility.ToFMODVector(transform.position)
      };
    }

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] GameObject go, [NotNull] Rigidbody rigidbody)
    {
      Transform transform = go.transform;
      return new ATTRIBUTES_3D()
      {
        forward = FMODUtility.ToFMODVector(transform.forward),
        up = FMODUtility.ToFMODVector(transform.up),
        position = FMODUtility.ToFMODVector(transform.position),
        velocity = FMODUtility.ToFMODVector(rigidbody.velocity)
      };
    }

    [PublicAPI]
    public static ATTRIBUTES_3D To3DAttributes([NotNull] GameObject go, [NotNull] Rigidbody2D rigidbody)
    {
      Vector2 velocity = rigidbody.velocity;
      VECTOR vector;
      vector.x = velocity.x;
      vector.y = velocity.y;
      vector.z = 0.0f;
      Transform transform = go.transform;
      return new ATTRIBUTES_3D()
      {
        forward = FMODUtility.ToFMODVector(transform.forward),
        up = FMODUtility.ToFMODVector(transform.up),
        position = FMODUtility.ToFMODVector(transform.position),
        velocity = vector
      };
    }

    private static FMODPlatform GetCurrentPlatform() => FMODPlatform.Windows;

    public static FMODPlatform GetParent(this FMODPlatform platform)
    {
      switch (platform)
      {
        case FMODPlatform.None:
        case FMODPlatform.PlayInEditor:
        case FMODPlatform.Default:
          return FMODPlatform.None;
        case FMODPlatform.Desktop:
        case FMODPlatform.Mobile:
          return FMODPlatform.Default;
        case FMODPlatform.MobileHigh:
          return FMODPlatform.Mobile;
        case FMODPlatform.MobileLow:
          return FMODPlatform.Mobile;
        case FMODPlatform.Console:
          return FMODPlatform.Default;
        case FMODPlatform.Windows:
        case FMODPlatform.Mac:
        case FMODPlatform.Linux:
          return FMODPlatform.Desktop;
        case FMODPlatform.iOS:
        case FMODPlatform.Android:
        case FMODPlatform.WindowsPhone:
          return FMODPlatform.Mobile;
        case FMODPlatform.XboxOne:
        case FMODPlatform.PS4:
        case FMODPlatform.WiiU:
          return FMODPlatform.Console;
        case FMODPlatform.PSVita:
        case FMODPlatform.AppleTV:
          return FMODPlatform.Mobile;
        case FMODPlatform.UWP:
          return FMODPlatform.Desktop;
        case FMODPlatform.Switch:
          return FMODPlatform.Mobile;
        case FMODPlatform.Count:
          throw new ArgumentException(nameof (platform));
        default:
          throw new ArgumentOutOfRangeException(nameof (platform), (object) platform, (string) null);
      }
    }

    public static void EnforceLibraryOrder()
    {
      int stats = (int) Memory.GetStats(out int _, out int _);
      int id = (int) Util.ParseID("", out Guid _);
    }
  }
}
