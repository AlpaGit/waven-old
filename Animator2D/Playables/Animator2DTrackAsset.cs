// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Playables.Animator2DTrackAsset
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Animations.Playables
{
  [TrackClipType(typeof (Animator2DPlayableAsset))]
  [TrackBindingType(typeof (Animator2D))]
  [TrackColor(0.9254902f, 0.05882353f, 0.08235294f)]
  [UsedImplicitly]
  [Serializable]
  internal sealed class Animator2DTrackAsset : TrackAsset
  {
    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver) => base.GatherProperties(director, driver);
  }
}
