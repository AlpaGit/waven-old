// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.CellObjectAnimationPlayableBehaviour
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Data;
using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using FMOD.Studio;
using FMODUnity;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Animations
{
  public sealed class CellObjectAnimationPlayableBehaviour : PlayableBehaviour
  {
    private readonly AbstractFightMap m_fightMap;
    private readonly CellObjectAnimationParameters m_parameters;
    private readonly Vector2Int m_origin;
    private readonly Quaternion m_rotation;
    private readonly float m_strength;
    private EventInstance m_eventInstance;

    public CellObjectAnimationPlayableBehaviour(
      [NotNull] AbstractFightMap fightMap,
      [NotNull] CellObjectAnimationParameters parameters,
      Vector2Int origin,
      Quaternion rotation,
      float strength)
    {
      this.m_fightMap = fightMap;
      this.m_parameters = parameters;
      this.m_origin = origin;
      this.m_rotation = rotation;
      this.m_strength = strength;
    }

    [UsedImplicitly]
    public CellObjectAnimationPlayableBehaviour()
    {
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
      if ((Object) null == (Object) this.m_fightMap)
        return;
      AudioReferenceWithParameters sound = this.m_parameters.sound;
      if (!sound.isValid)
        return;
      AbstractFightMap fightMap = this.m_fightMap;
      Vector2Int origin = this.m_origin;
      int x = origin.x;
      origin = this.m_origin;
      int y = origin.y;
      CellObject cellObject;
      ref CellObject local = ref cellObject;
      Transform transform = !fightMap.TryGetCellObject(x, y, out local) ? (Transform) null : cellObject.transform;
      if (AudioManager.TryCreateInstance(sound, transform, out this.m_eventInstance))
      {
        int num1 = (int) this.m_eventInstance.setParameterValue("Strength", this.m_strength);
        int num2 = (int) this.m_eventInstance.start();
      }
      else
        Log.Warning("Failed to create event instance for cell object animation parameters named '" + this.m_parameters.name + "'.", 109, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Animations\\CellObjectAnimationPlayableBehaviour.cs");
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
      if (!this.m_eventInstance.isValid())
        return;
      int num1 = (int) this.m_eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
      int num2 = (int) this.m_eventInstance.release();
      this.m_eventInstance.clearHandle();
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
      if ((Object) null == (Object) this.m_fightMap)
        return;
      this.m_fightMap.ApplyCellObjectAnimation(this.m_parameters, this.m_origin, this.m_rotation, this.m_strength, (float) playable.GetTime<Playable>());
    }
  }
}
