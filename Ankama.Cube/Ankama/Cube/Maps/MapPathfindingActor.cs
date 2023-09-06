// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.MapPathfindingActor
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Animations;
using Ankama.Animations.Events;
using Ankama.Cube.Animations;
using Ankama.Cube.Data;
using Ankama.Cube.Maps.Objects;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Maps
{
  public class MapPathfindingActor : MonoBehaviour
  {
    [SerializeField]
    private PlayableDirector m_playableDirector;
    [SerializeField]
    private Animator2D m_animator2D;
    [SerializeField]
    private float m_speedFactor = 1f;
    [SerializeField]
    private float m_heightOffest = 0.07f;
    private Coroutine m_movementCoroutine;
    private const float MovementCellTraversalTime = 5f;
    private bool m_hasTimeline;
    private DirectionAngle m_mapRotation;
    private Ankama.Cube.Data.Direction m_direction = Ankama.Cube.Data.Direction.SouthEast;
    private CharacterAnimationParameters m_animationParameters;
    private CharacterAnimationCallback m_animationCallback;
    private AnimatedFightCharacterData m_characterData;
    private MapCharacterObjectContext m_context;
    private bool m_isRunning;

    public Ankama.Cube.Data.Direction direction
    {
      get => this.m_direction;
      set
      {
        if (value == this.m_direction)
          return;
        if (this.m_context != null)
          this.m_context.UpdateDirection(this.m_direction, value);
        this.m_direction = value;
      }
    }

    public Vector2 coords => new Vector2(this.transform.position.x, this.transform.position.z);

    private void Awake()
    {
    }

    private IEnumerator Start()
    {
      yield break;
    }

    protected void OnEnable()
    {
      this.m_animator2D.AnimationLooped += new AnimationLoopedEventHandler(this.OnAnimationLooped);
      this.m_animationCallback = new CharacterAnimationCallback((IAnimator2D) this.m_animator2D);
      this.m_context = new MapCharacterObjectContext(this);
      this.m_context.Initialize();
      this.m_playableDirector.extrapolationMode = DirectorWrapMode.Hold;
    }

    protected void OnDisable()
    {
      if (this.m_movementCoroutine != null)
      {
        this.StopCoroutine(this.m_movementCoroutine);
        this.m_movementCoroutine = (Coroutine) null;
      }
      if (this.m_animationCallback != null)
      {
        this.m_animationCallback.Release();
        this.m_animationCallback = (CharacterAnimationCallback) null;
      }
      if (this.m_context != null)
      {
        this.m_context.Release();
        this.m_context = (MapCharacterObjectContext) null;
      }
      this.m_animator2D.AnimationLooped -= new AnimationLoopedEventHandler(this.OnAnimationLooped);
    }

    public void SetCharacterData(AnimatedFightCharacterData data, AnimatedObjectDefinition def)
    {
      this.m_characterData = data;
      this.m_animator2D.Initialised += new Animator2DInitialisedEventHandler(this.InitAnimatorCallback);
      this.m_animator2D.SetDefinition(def);
    }

    private void InitAnimatorCallback(object sender, Animator2DInitialisedEventArgs e)
    {
      this.m_animator2D.Initialised -= new Animator2DInitialisedEventHandler(this.InitAnimatorCallback);
      if (this.m_movementCoroutine != null)
        return;
      this.PlayIdleAnimation();
    }

    public void FollowPath(List<Vector3> path) => this.FollowPath(path, Vector3.zero);

    public void FollowPath(List<Vector3> path, Vector3 endLookAt)
    {
      if ((UnityEngine.Object) this.m_animator2D.GetDefinition() == (UnityEngine.Object) null)
        return;
      if (this.m_movementCoroutine != null)
      {
        this.StopCoroutine(this.m_movementCoroutine);
        this.m_movementCoroutine = (Coroutine) null;
      }
      if (path == null)
        this.PlayIdleAnimation();
      else
        this.m_movementCoroutine = this.StartCoroutine(this.MoveToRoutine(path, endLookAt));
    }

    private Ankama.Cube.Data.Direction GetDirection(Vector3 start, Vector3 end) => this.GetDirection(end - start);

    private Ankama.Cube.Data.Direction GetDirection(Vector3 direction) => (double) Mathf.Abs(direction.x) > (double) Mathf.Abs(direction.z) ? ((double) direction.x < 0.0 ? Ankama.Cube.Data.Direction.SouthWest : Ankama.Cube.Data.Direction.NorthEast) : ((double) direction.z < 0.0 ? Ankama.Cube.Data.Direction.SouthEast : Ankama.Cube.Data.Direction.NorthWest);

    private IEnumerator MoveToRoutine(List<Vector3> path, Vector3 endLookAt)
    {
      MapPathfindingActor pathfindingActor = this;
      int movementCellsCount = path.Count;
      if (movementCellsCount <= 1)
      {
        pathfindingActor.PlayIdleAnimation();
      }
      else
      {
        Animator2D animator = pathfindingActor.m_animator2D;
        Vector3 startCell = path[0];
        CharacterAnimationInfo transitionAnimationInfo;
        if (!pathfindingActor.m_isRunning && pathfindingActor.m_characterData.idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.IdleToRun))
        {
          transitionAnimationInfo = new CharacterAnimationInfo(new Vector2(startCell.x, startCell.z), "idle_run", "idle-to-run", false, movementCellsCount >= 2 ? pathfindingActor.GetDirection(startCell, path[1]) : pathfindingActor.direction, pathfindingActor.m_mapRotation);
          if (!transitionAnimationInfo.animationName.Equals(pathfindingActor.m_animator2D.animationName))
            pathfindingActor.StartAnimation(transitionAnimationInfo);
          while (!CharacterObjectUtility.HasAnimationEnded(animator, transitionAnimationInfo) && pathfindingActor.m_characterData.idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.IdleToRun))
            yield return (object) null;
          transitionAnimationInfo = new CharacterAnimationInfo();
        }
        pathfindingActor.m_isRunning = true;
        Vector3 previousCoords = startCell;
        for (int i = 1; i < movementCellsCount; ++i)
        {
          Vector3 coords = path[i];
          Vector3 direction = coords - previousCoords;
          float magnitude = direction.magnitude;
          CharacterAnimationInfo animationInfo = new CharacterAnimationInfo(new Vector2(coords.x, coords.z), "run", "run", true, pathfindingActor.GetDirection(direction), pathfindingActor.m_mapRotation);
          pathfindingActor.StartAnimation(animationInfo, restart: false);
          float cellTraversalDuration = (float) ((double) magnitude * 5.0 * (1.0 / (double) pathfindingActor.m_speedFactor)) / (float) animator.frameRate;
          float animationTime = 0.0f;
          do
          {
            Vector3 vector3 = Vector3.Lerp(previousCoords, coords, animationTime / cellTraversalDuration);
            pathfindingActor.transform.position = vector3 + Vector3.up * pathfindingActor.m_heightOffest;
            yield return (object) null;
            animationTime += Time.deltaTime;
          }
          while ((double) animationTime < (double) cellTraversalDuration);
          pathfindingActor.transform.position = coords + Vector3.up * pathfindingActor.m_heightOffest;
          previousCoords = coords;
          coords = new Vector3();
        }
        pathfindingActor.m_isRunning = false;
        if (pathfindingActor.m_characterData.idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.RunToIdle))
        {
          transitionAnimationInfo = new CharacterAnimationInfo(new Vector2(previousCoords.x, previousCoords.z), "run_idle", "run-to-idle", false, pathfindingActor.direction, pathfindingActor.m_mapRotation);
          pathfindingActor.StartAnimation(transitionAnimationInfo);
          while (!CharacterObjectUtility.HasAnimationEnded(animator, transitionAnimationInfo) && pathfindingActor.m_characterData.idleToRunTransitionMode.HasFlag((Enum) AnimatedFightCharacterData.IdleToRunTransitionMode.RunToIdle))
            yield return (object) null;
          transitionAnimationInfo = new CharacterAnimationInfo();
        }
        if (endLookAt != Vector3.zero)
          pathfindingActor.m_direction = pathfindingActor.GetDirection(endLookAt);
        pathfindingActor.PlayIdleAnimation();
        pathfindingActor.m_movementCoroutine = (Coroutine) null;
      }
    }

    private void PlayIdleAnimation() => this.StartAnimation(new CharacterAnimationInfo(this.coords, "idle", "idle", true, this.m_direction, this.m_mapRotation), restart: false);

    private void StartAnimation(
      CharacterAnimationInfo animationInfo,
      Action onComplete = null,
      Action onCancel = null,
      bool restart = true,
      bool async = false)
    {
      string animationName = animationInfo.animationName;
      string timelineKey = animationInfo.timelineKey;
      this.m_animator2D.transform.localRotation = animationInfo.flipX ? Quaternion.Euler(0.0f, -135f, 0.0f) : Quaternion.Euler(0.0f, 45f, 0.0f);
      this.direction = animationInfo.direction;
      ITimelineAssetProvider characterData = (ITimelineAssetProvider) this.m_characterData;
      if (characterData != null)
      {
        TimelineAsset timelineAsset1;
        bool timelineAsset2 = characterData.TryGetTimelineAsset(timelineKey, out timelineAsset1);
        if (timelineAsset2 && (UnityEngine.Object) null != (UnityEngine.Object) timelineAsset1)
        {
          if ((UnityEngine.Object) timelineAsset1 != (UnityEngine.Object) this.m_playableDirector.playableAsset)
          {
            this.m_playableDirector.Play((PlayableAsset) timelineAsset1);
          }
          else
          {
            if (restart || !this.m_animator2D.animationName.Equals(animationName))
              this.m_playableDirector.time = 0.0;
            this.m_playableDirector.Resume();
          }
          this.m_hasTimeline = true;
        }
        else
        {
          if (timelineAsset2)
            Log.Warning("Character named '" + this.m_characterData.name + "' has a timeline setup for key '" + timelineKey + "' but the actual asset is null.", 335, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\HavreMap\\MapPathfindingActor.cs");
          this.m_playableDirector.time = 0.0;
          this.m_playableDirector.Pause();
          this.m_hasTimeline = false;
        }
      }
      this.m_animationCallback.Setup(animationName, restart, onComplete, onCancel);
      this.m_animator2D.SetAnimation(animationName, animationInfo.loops, async, restart);
      this.m_animationParameters = animationInfo.parameters;
    }

    private void OnAnimationLooped(object sender, AnimationLoopedEventArgs e)
    {
      if (!this.m_hasTimeline)
        return;
      this.m_playableDirector.time = 0.0;
      this.m_playableDirector.Resume();
    }
  }
}
