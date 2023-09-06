// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.MapRotationHandler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public class MapRotationHandler : MonoBehaviour
  {
    private void Start() => CameraHandler.AddMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));

    private void OnDestroy() => CameraHandler.RemoveMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));

    private void OnMapRotationChanged(DirectionAngle previousRotation, DirectionAngle newRotation) => this.transform.rotation *= previousRotation.GetRotation() * newRotation.GetInverseRotation();
  }
}
