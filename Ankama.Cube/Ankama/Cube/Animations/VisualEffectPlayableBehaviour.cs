// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.VisualEffectPlayableBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Animations
{
  public sealed class VisualEffectPlayableBehaviour : PlayableBehaviour
  {
    private readonly Transform m_ownerTransform;
    private readonly VisualEffectContext m_visualEffectContext;
    private readonly VisualEffect m_prefab;
    private readonly VisualEffectPlayableAsset.StopMode m_stopMode;
    private readonly VisualEffectPlayableAsset.ParentingMode m_parentingMode;
    private readonly VisualEffectPlayableAsset.OrientationMethod m_orientationMethod;
    private readonly Vector3 m_offset;
    private GameObject m_instance;
    private VisualEffect m_visualEffect;
    private PlayableGraph m_playableGraph;

    [UsedImplicitly]
    public VisualEffectPlayableBehaviour() => throw new NotImplementedException();

    public VisualEffectPlayableBehaviour(
      GameObject owner,
      VisualEffectContext context,
      VisualEffect prefab,
      VisualEffectPlayableAsset.StopMode stopMode,
      VisualEffectPlayableAsset.ParentingMode parentingMode,
      VisualEffectPlayableAsset.OrientationMethod orientationMethod,
      Vector3 offset)
    {
      this.m_ownerTransform = owner.transform;
      this.m_visualEffectContext = context;
      this.m_prefab = prefab;
      this.m_stopMode = stopMode;
      this.m_parentingMode = parentingMode;
      this.m_orientationMethod = orientationMethod;
      this.m_offset = offset;
    }

    public override void OnPlayableDestroy(Playable playable) => this.Stop();

    public override void OnBehaviourPause(Playable playable, FrameData info) => this.Stop();

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_prefab || (UnityEngine.Object) null != (UnityEngine.Object) this.m_visualEffect || this.Start())
        return;
      Log.Error("Failed to start a VisualEffect, this will leak GameObject every frame.", 141, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\VisualEffectPlayableBehaviour.cs");
    }

    private bool Start()
    {
      Quaternion rotation = Quaternion.identity;
      Vector3 scale = Vector3.one;
      Transform parent;
      switch (this.m_parentingMode)
      {
        case VisualEffectPlayableAsset.ParentingMode.Owner:
          parent = this.m_ownerTransform;
          break;
        case VisualEffectPlayableAsset.ParentingMode.Parent:
          parent = this.m_ownerTransform.parent;
          break;
        case VisualEffectPlayableAsset.ParentingMode.ContextOwner:
          parent = this.m_visualEffectContext != null ? this.m_visualEffectContext.transform : this.m_ownerTransform;
          break;
        case VisualEffectPlayableAsset.ParentingMode.ContextParent:
          parent = this.m_visualEffectContext != null ? this.m_visualEffectContext.transform.parent : this.m_ownerTransform.parent;
          break;
        case VisualEffectPlayableAsset.ParentingMode.World:
          parent = (Transform) null;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Vector3 position = !((UnityEngine.Object) null != (UnityEngine.Object) parent) ? this.m_ownerTransform.position + this.m_offset : parent.position + this.m_offset;
      switch (this.m_orientationMethod)
      {
        case VisualEffectPlayableAsset.OrientationMethod.None:
          CameraHandler current = CameraHandler.current;
          if ((UnityEngine.Object) null != (UnityEngine.Object) current)
          {
            rotation = current.mapRotation.GetInverseRotation();
            break;
          }
          break;
        case VisualEffectPlayableAsset.OrientationMethod.Context:
          if (this.m_visualEffectContext != null)
          {
            this.m_visualEffectContext.GetVisualEffectTransformation(out rotation, out scale);
            break;
          }
          break;
        case VisualEffectPlayableAsset.OrientationMethod.Director:
          rotation = this.m_ownerTransform.rotation;
          break;
        case VisualEffectPlayableAsset.OrientationMethod.Transform:
          rotation = (UnityEngine.Object) null != (UnityEngine.Object) parent ? parent.rotation : this.m_ownerTransform.rotation;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      VisualEffect visualEffect = VisualEffectFactory.Instantiate(this.m_prefab, position, rotation, scale, parent);
      visualEffect.destructionOverride = new Action<VisualEffect>(this.OnVisualEffectInstanceDestructionRequest);
      if (this.m_visualEffectContext != null)
      {
        switch (this.m_parentingMode)
        {
          case VisualEffectPlayableAsset.ParentingMode.ContextOwner:
          case VisualEffectPlayableAsset.ParentingMode.ContextParent:
            this.m_visualEffectContext.AddVisualEffect(visualEffect);
            break;
        }
      }
      visualEffect.Play();
      this.m_instance = visualEffect.gameObject;
      this.m_visualEffect = visualEffect;
      return true;
    }

    private void Stop()
    {
      switch (this.m_stopMode)
      {
        case VisualEffectPlayableAsset.StopMode.None:
          VisualEffect visualEffect1 = this.m_visualEffect;
          if (!((UnityEngine.Object) null != (UnityEngine.Object) visualEffect1))
            break;
          if (this.m_visualEffectContext != null)
          {
            switch (this.m_parentingMode)
            {
              case VisualEffectPlayableAsset.ParentingMode.ContextOwner:
              case VisualEffectPlayableAsset.ParentingMode.ContextParent:
                this.m_visualEffectContext.RemoveVisualEffect(visualEffect1);
                break;
            }
          }
          this.m_visualEffect = (VisualEffect) null;
          break;
        case VisualEffectPlayableAsset.StopMode.Stop:
          VisualEffect visualEffect2 = this.m_visualEffect;
          if (!((UnityEngine.Object) null != (UnityEngine.Object) visualEffect2))
            break;
          visualEffect2.Stop();
          if (this.m_visualEffectContext != null)
          {
            switch (this.m_parentingMode)
            {
              case VisualEffectPlayableAsset.ParentingMode.ContextOwner:
              case VisualEffectPlayableAsset.ParentingMode.ContextParent:
                this.m_visualEffectContext.RemoveVisualEffect(visualEffect2);
                break;
            }
          }
          this.m_visualEffect = (VisualEffect) null;
          break;
        case VisualEffectPlayableAsset.StopMode.Destroy:
          if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_instance))
            break;
          UnityEngine.Object.Destroy((UnityEngine.Object) this.m_instance);
          if (this.m_visualEffectContext != null)
          {
            switch (this.m_parentingMode)
            {
              case VisualEffectPlayableAsset.ParentingMode.ContextOwner:
              case VisualEffectPlayableAsset.ParentingMode.ContextParent:
                this.m_visualEffectContext.RemoveVisualEffect(this.m_visualEffect);
                break;
            }
          }
          this.m_visualEffect = (VisualEffect) null;
          this.m_instance = (GameObject) null;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void OnVisualEffectInstanceDestructionRequest(VisualEffect instance) => VisualEffectFactory.Release(this.m_prefab, instance);
  }
}
