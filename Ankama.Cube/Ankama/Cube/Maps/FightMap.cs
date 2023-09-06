// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMap
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Data;
using Ankama.Cube.Data.Maps;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Cube.States;
using Ankama.Cube.UI.Fight;
using Ankama.Utilities;
using FMODUnity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  [UsedImplicitly]
  public sealed class FightMap : AbstractFightMap
  {
    private Dictionary<int, GameObject> m_monsterSpawnCellDictionary;
    public static FightMap current;
    [SerializeField]
    [HideInInspector]
    private FightMapDefinition m_definition;
    [SerializeField]
    private MapRenderSettings m_renderSettings;
    [SerializeField]
    private BossFightMapResources m_bossFightMapResources;
    [SerializeField]
    private BossObject m_bossObject;
    [SerializeField]
    private AudioEventGroup m_musicGroup;
    [SerializeField]
    private AudioEventGroup m_ambianceGroup;
    private readonly FightMapAudioContext m_audioContext = new FightMapAudioContext();
    private AudioWorldMusicRequest m_worldMusicRequest;

    public BossObject bossObject => this.m_bossObject;

    public IEnumerator AddMonsterSpawnCell(int x, int y, Ankama.Cube.Data.Direction direction)
    {
      FightMap fightMap = this;
      MonsterSpawnCellDefinition monsterSpawnCellDefinition = fightMap.m_bossFightMapResources.monsterSpawnCellDefinition;
      if (!((Object) null == (Object) monsterSpawnCellDefinition))
      {
        IMapDefinition mapDefinition = fightMap.m_mapDefinition;
        Vector2 sizeMin = (Vector2) mapDefinition.sizeMin;
        if ((double) x >= (double) sizeMin.x && (double) y >= (double) sizeMin.y)
        {
          Vector2 sizeMax = (Vector2) mapDefinition.sizeMax;
          if ((double) x < (double) sizeMax.x && (double) y < (double) sizeMax.y)
          {
            int index = mapDefinition.GetCellIndex(x, y);
            CellObject cellObject = fightMap.m_cellObjectsByIndex[index];
            Transform transform = cellObject.transform;
            Vector3 position = transform.position + 0.5f * Vector3.up;
            Quaternion rotation = Quaternion.identity;
            if (AudioManager.isReady)
            {
              AudioReference appearanceSound = monsterSpawnCellDefinition.appearanceSound;
              if (appearanceSound.isValid)
                AudioManager.PlayOneShot(appearanceSound, transform);
            }
            VisualEffect appearanceEffect = monsterSpawnCellDefinition.appearanceEffect;
            if ((Object) null != (Object) appearanceEffect)
            {
              Object.Instantiate<VisualEffect>(appearanceEffect, position, rotation, transform);
              float appearanceDelay = monsterSpawnCellDefinition.appearanceDelay;
              if ((double) appearanceDelay > 0.0)
                yield return (object) new WaitForTime(appearanceDelay);
            }
            GameObject gameObject = monsterSpawnCellDefinition.Instantiate(position, rotation, cellObject.transform);
            if (!((Object) null == (Object) gameObject))
            {
              gameObject.GetComponent<SpawnCellObject>().SetDirection(direction);
              fightMap.m_monsterSpawnCellDictionary.Add(index, gameObject);
            }
          }
        }
      }
    }

    public IEnumerator RemoveMonsterSpawnCell(int x, int y)
    {
      FightMap fightMap = this;
      MonsterSpawnCellDefinition monsterSpawnCellDefinition = fightMap.m_bossFightMapResources.monsterSpawnCellDefinition;
      if (!((Object) null == (Object) monsterSpawnCellDefinition))
      {
        int cellIndex = fightMap.m_mapDefinition.GetCellIndex(x, y);
        GameObject instance;
        if (fightMap.m_monsterSpawnCellDictionary.TryGetValue(cellIndex, out instance))
        {
          fightMap.m_monsterSpawnCellDictionary.Remove(cellIndex);
          Transform transform = instance.transform;
          if (AudioManager.isReady)
          {
            AudioReference disappearanceSound = monsterSpawnCellDefinition.disappearanceSound;
            if (disappearanceSound.isValid)
              AudioManager.PlayOneShot(disappearanceSound, transform);
          }
          VisualEffect disappearanceEffect = monsterSpawnCellDefinition.disappearanceEffect;
          if ((Object) null != (Object) disappearanceEffect)
          {
            Object.Instantiate<VisualEffect>(disappearanceEffect, transform.position, transform.rotation, transform.parent);
            float disappearanceDelay = monsterSpawnCellDefinition.disappearanceDelay;
            if ((double) disappearanceDelay > 0.0)
              yield return (object) new WaitForTime(disappearanceDelay);
          }
          monsterSpawnCellDefinition.DestroyInstance(instance);
        }
      }
    }

    public IEnumerator ClearMonsterSpawnCells(int fightId)
    {
      MonsterSpawnCellDefinition monsterSpawnCellDefinition = this.m_bossFightMapResources.monsterSpawnCellDefinition;
      if (!((Object) null == (Object) monsterSpawnCellDefinition))
      {
        FightMapDefinition definition = this.m_definition;
        FightMapRegionDefinition region = definition.regions[fightId];
        Vector2Int sizeMin = region.sizeMin;
        Vector2Int sizeMax = region.sizeMax;
        List<int> indicesToRemove = ListPool<int>.Get();
        foreach (KeyValuePair<int, GameObject> monsterSpawnCell in this.m_monsterSpawnCellDictionary)
        {
          int key = monsterSpawnCell.Key;
          Vector2Int cellCoords = definition.GetCellCoords(key);
          if (cellCoords.x >= sizeMin.x && cellCoords.y >= sizeMin.y && cellCoords.x < sizeMax.x && cellCoords.y < sizeMax.y)
          {
            Transform transform = monsterSpawnCell.Value.transform;
            if (AudioManager.isReady)
            {
              AudioReference disappearanceSound = monsterSpawnCellDefinition.disappearanceSound;
              if (disappearanceSound.isValid)
                AudioManager.PlayOneShot(disappearanceSound, transform);
            }
            VisualEffect disappearanceEffect = monsterSpawnCellDefinition.disappearanceEffect;
            if ((Object) null != (Object) disappearanceEffect)
              Object.Instantiate<VisualEffect>(disappearanceEffect, transform.position, transform.rotation, transform.parent);
            indicesToRemove.Add(key);
          }
        }
        int indicesToRemoveCount = indicesToRemove.Count;
        if (indicesToRemoveCount > 0)
        {
          float disappearanceDelay = monsterSpawnCellDefinition.disappearanceDelay;
          if ((double) disappearanceDelay > 0.0)
            yield return (object) new WaitForTime(disappearanceDelay);
          for (int index = 0; index < indicesToRemoveCount; ++index)
          {
            GameObject instance;
            if (this.m_monsterSpawnCellDictionary.TryGetValue(indicesToRemove[index], out instance))
              monsterSpawnCellDefinition.DestroyInstance(instance);
          }
        }
        ListPool<int>.Release(indicesToRemove);
      }
    }

    public void AddHeroLostFeedback(Vector2Int position)
    {
      GameObject[] heroLostFeedbacks = this.m_bossFightMapResources.heroLostFeedbacks;
      int length = heroLostFeedbacks.Length;
      if (length == 0)
        return;
      int index = Random.Range(0, length);
      GameObject original = heroLostFeedbacks[index];
      if ((Object) null == (Object) original)
      {
        Log.Error(string.Format("HeroLostFeedback at index {0} is null for map named {1}.", (object) index, (object) this.m_definition.displayName), 213, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\FightMap.BossFight.cs");
      }
      else
      {
        CellObject cellObject;
        if (!this.TryGetCellObject(position.x, position.y, out cellObject))
        {
          Log.Error(string.Format("Tried to add an HeroLostFeedback instance at position {0} but there is no cell there.", (object) position), 220, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\FightMap.BossFight.cs");
        }
        else
        {
          Transform transform = cellObject.transform;
          Vector3 position1 = transform.position + 0.5f * Vector3.up;
          Object.Instantiate<GameObject>(original, position1, Quaternion.identity, transform);
        }
      }
    }

    public FightMapDefinition definition => this.m_definition;

    public MapRenderSettings ambience => this.m_renderSettings;

    protected override void ApplyMovement(
      Vector2Int[] path,
      ICharacterEntity trackedCharacter,
      IEntityWithBoardPresence targetedEntity)
    {
      FightState instance = FightState.instance;
      if (instance == null)
        return;
      if (targetedEntity != null)
      {
        instance.frame.SendEntityAttack(trackedCharacter.id, path, targetedEntity.id);
      }
      else
      {
        if (path.Length <= 1)
          return;
        instance.frame.SendEntityMovement(trackedCharacter.id, path);
      }
    }

    protected override Color GetHighlightColor(
      FightMapFeedbackColors feedbackColors,
      IMapEntityProvider mapEntityProvider,
      ICharacterEntity trackedCharacter)
    {
      FightStatus local = FightStatus.local;
      PlayerType playerType = local != mapEntityProvider ? (trackedCharacter.teamIndex == GameStatus.localPlayerTeamIndex ? PlayerType.Ally : PlayerType.Opponent) : (local.localPlayerId != trackedCharacter.ownerId ? (trackedCharacter.teamIndex == GameStatus.localPlayerTeamIndex ? PlayerType.Ally | PlayerType.Local : PlayerType.Opponent | PlayerType.Local) : PlayerType.Player);
      return feedbackColors.GetPlayerColor(playerType);
    }

    public override MapCellIndicator GetCellIndicator(int x, int y)
    {
      Dictionary<int, GameObject> spawnCellDictionary = this.m_monsterSpawnCellDictionary;
      if (spawnCellDictionary != null)
      {
        int cellIndex = this.m_mapDefinition.GetCellIndex(x, y);
        if (spawnCellDictionary.ContainsKey(cellIndex))
          return MapCellIndicator.Death;
      }
      return MapCellIndicator.None;
    }

    private void Awake()
    {
      this.m_mapDefinition = (IMapDefinition) this.m_definition;
      this.m_interactiveMode = AbstractFightMap.InteractiveMode.None;
      this.Create();
      this.enabled = false;
    }

    protected override void Update()
    {
      base.Update();
      FightUIRework.tooltipsEnabled = !this.m_pathFinder.tracking && FightCastManager.currentCastType == FightCastManager.CurrentCastType.None;
    }

    public IEnumerator Initialize()
    {
      FightMap fightMap = this;
      CameraHandler.AddMapRotationListener(new CameraHandler.MapRotationChangedDelegate(fightMap.OnMapRotationChanged));
      FightStatus local = FightStatus.local;
      int regionCount = fightMap.m_mapDefinition.regionCount;
      fightMap.m_movementContexts = new FightMapMovementContext[regionCount];
      for (int fightId = 0; fightId < regionCount; ++fightId)
      {
        FightStatus fightStatus = FightLogicExecutor.GetFightStatus(fightId);
        fightStatus.EntitiesChanged += new EntitiesChangedDelegate(fightMap.OnEntitiesChanged);
        FightMapMovementContext mapMovementContext = new FightMapMovementContext((IMapStateProvider) fightStatus.mapStatus, (IMapEntityProvider) fightStatus);
        if (fightStatus == local)
          fightMap.m_localMovementContext = mapMovementContext;
        fightMap.m_movementContexts[fightId] = mapMovementContext;
      }
      if (fightMap.m_localMovementContext != null)
      {
        IMapStateProvider stateProvider = fightMap.m_localMovementContext.stateProvider;
        fightMap.m_targetContext = new FightMapTargetContext(stateProvider);
      }
      BoxCollider collider = fightMap.CreateCollider();
      fightMap.InitializeHandlers(collider, false);
      MonsterSpawnCellDefinition spawnCellDefinition = fightMap.m_bossFightMapResources.monsterSpawnCellDefinition;
      if ((Object) null != (Object) spawnCellDefinition)
      {
        yield return (object) spawnCellDefinition.Initialize();
        fightMap.m_monsterSpawnCellDictionary = new Dictionary<int, GameObject>();
      }
      if (AudioManager.isReady)
      {
        fightMap.m_audioContext.Initialize();
        fightMap.m_worldMusicRequest = AudioManager.LoadWorldMusic(fightMap.m_musicGroup, fightMap.m_ambianceGroup, (AudioContext) fightMap.m_audioContext);
        while (fightMap.m_worldMusicRequest.state == AudioWorldMusicRequest.State.Loading)
          yield return (object) null;
      }
    }

    public void Begin()
    {
      if (this.m_worldMusicRequest != null)
        AudioManager.StartWorldMusic(this.m_worldMusicRequest);
      this.enabled = true;
    }

    public void Stop()
    {
      this.SetNoInteractionPhase();
      if (this.m_activeMovementContext == null)
        return;
      this.m_activeMovementContext.End();
      this.m_activeMovementContext = (FightMapMovementContext) null;
      this.m_feedbackNeedsUpdate = true;
    }

    public void End()
    {
      if (this.m_worldMusicRequest == null)
        return;
      AudioManager.StopWorldMusic(this.m_worldMusicRequest);
    }

    public void Release()
    {
      CameraHandler.RemoveMapRotationListener(new CameraHandler.MapRotationChangedDelegate(this.OnMapRotationChanged));
      if (FightLogicExecutor.isValid)
      {
        int regionCount = this.m_mapDefinition.regionCount;
        for (int fightId = 0; fightId < regionCount; ++fightId)
        {
          FightStatus fightStatus = FightLogicExecutor.GetFightStatus(fightId);
          if (fightStatus != null)
            fightStatus.EntitiesChanged -= new EntitiesChangedDelegate(this.OnEntitiesChanged);
        }
      }
      MonsterSpawnCellDefinition spawnCellDefinition = this.m_bossFightMapResources.monsterSpawnCellDefinition;
      if ((Object) null != (Object) spawnCellDefinition)
      {
        foreach (GameObject instance in this.m_monsterSpawnCellDictionary.Values)
          spawnCellDefinition.DestroyInstance(instance);
        this.m_monsterSpawnCellDictionary.Clear();
        this.m_monsterSpawnCellDictionary = (Dictionary<int, GameObject>) null;
        spawnCellDefinition.Release();
      }
      if (AudioManager.isReady)
        this.m_audioContext.Release();
      this.m_worldMusicRequest = (AudioWorldMusicRequest) null;
      this.m_movementContexts = (FightMapMovementContext[]) null;
      this.m_localMovementContext = (FightMapMovementContext) null;
      this.m_activeMovementContext = (FightMapMovementContext) null;
      this.m_targetContext = (FightMapTargetContext) null;
    }

    public void SetTurnIndex(int turnIndex)
    {
      if (this.m_audioContext == null)
        return;
      this.m_audioContext.turnIndex = turnIndex;
    }

    public void SetLocalPlayerHeroLife(int life, int baseLife)
    {
      if (this.m_audioContext == null)
        return;
      this.m_audioContext.localPlayerHeroLife = Mathf.Clamp01((float) life / (float) baseLife);
    }

    public void SetBossEvolutionStep(int value)
    {
      if (this.m_audioContext == null)
        return;
      this.m_audioContext.bossEvolutionStep = value;
    }

    private void OnMapRotationChanged(
      DirectionAngle previousMapRotation,
      DirectionAngle newMapRotation)
    {
      this.m_pathFinderFeedbackManager.SetMapRotation(newMapRotation);
      this.m_cellPointerManager.SetMapRotation(newMapRotation);
    }

    private void OnEntitiesChanged(FightStatus fightStatus, EntitiesChangedFlags flags)
    {
      FightMapMovementContext movementContext = this.m_movementContexts[fightStatus.fightId];
      if ((flags & (EntitiesChangedFlags.Added | EntitiesChangedFlags.Removed | EntitiesChangedFlags.AreaMoved)) != EntitiesChangedFlags.None && this.m_activeMovementContext == movementContext)
      {
        ICharacterEntity trackedCharacter = movementContext.trackedCharacter;
        if (trackedCharacter != null)
        {
          movementContext.End();
          if (!trackedCharacter.isDirty)
            movementContext.Begin(trackedCharacter, this.m_pathFinder);
          else
            this.m_activeMovementContext = (FightMapMovementContext) null;
          this.m_feedbackNeedsUpdate = true;
        }
      }
      if (this.m_targetContext != null && (flags & (EntitiesChangedFlags.Removed | EntitiesChangedFlags.AreaMoved)) != EntitiesChangedFlags.None)
      {
        this.m_targetContext.Refresh();
        this.m_feedbackNeedsUpdate = true;
      }
      if (this.m_interactiveMode == AbstractFightMap.InteractiveMode.Movement && this.m_localMovementContext == movementContext)
        this.m_cellPointerManager.RefreshPlayableCharactersHighlights((IMap) this, (IMapEntityProvider) fightStatus);
      this.m_inputHandler.SetDirty();
    }
  }
}
