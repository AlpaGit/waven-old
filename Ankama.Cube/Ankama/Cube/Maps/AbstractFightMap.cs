// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.AbstractFightMap
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Fight.Movement;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.SRP;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public abstract class AbstractFightMap : MonoBehaviour, IMap
  {
    [SerializeField]
    [HideInInspector]
    protected CellObject[] m_cellObjects = new CellObject[0];
    [SerializeField]
    protected FightMapFeedbackResources m_feedbackResources;
    [SerializeField]
    protected float m_gravity = -9.81f;
    [NonSerialized]
    public Action<Target?> onTargetSelected;
    [NonSerialized]
    public Action<Target?, CellObject> onTargetChanged;
    protected IMapDefinition m_mapDefinition;
    protected AbstractFightMap.InteractiveMode m_interactiveMode;
    protected FightMapInputHandler m_inputHandler;
    private AbstractFightMap.TargetInputMode m_targetInputMode = AbstractFightMap.TargetInputMode.Click;
    protected readonly FightPathFinder m_pathFinder = new FightPathFinder();
    protected readonly FightPathFinderFeedbackManager m_pathFinderFeedbackManager = new FightPathFinderFeedbackManager();
    protected readonly FightMapCellPointerManager m_cellPointerManager = new FightMapCellPointerManager();
    protected bool m_feedbackNeedsUpdate;
    protected FightMapMovementContext[] m_movementContexts;
    protected FightMapMovementContext m_activeMovementContext;
    protected FightMapMovementContext m_localMovementContext;
    protected FightMapTargetContext m_targetContext;
    protected CellObject[] m_cellObjectsByIndex;
    protected MapVirtualGrid m_virtualGrid;
    private readonly List<CellObject> m_referenceCells = new List<CellObject>(64);
    private readonly List<CellObject> m_linkedCells = new List<CellObject>(1);
    private readonly List<AbstractFightMap.CellObjectAnimationInstance> m_cellObjectAnimationInstances = new List<AbstractFightMap.CellObjectAnimationInstance>(2);
    private bool m_virtualGridIsDirty = true;
    private bool m_cellObjectNeedsCleanup;
    private IObjectWithFocus m_objectFocusedByCursor;

    [PublicAPI]
    public void SetNoInteractionPhase()
    {
      this.EndCurrentPhase();
      this.m_interactiveMode = AbstractFightMap.InteractiveMode.None;
    }

    [PublicAPI]
    public bool IsInMovementPhase() => this.m_interactiveMode == AbstractFightMap.InteractiveMode.Movement;

    [PublicAPI]
    public void SetMovementPhase()
    {
      this.EndCurrentPhase();
      this.m_interactiveMode = AbstractFightMap.InteractiveMode.Movement;
      FightMapMovementContext localMovementContext = this.m_localMovementContext;
      if (localMovementContext == null)
        return;
      this.m_cellPointerManager.BeginHighlightingPlayableCharacters((IMap) this, localMovementContext.entityProvider);
    }

    [PublicAPI]
    public bool IsInTargetingPhase() => this.m_interactiveMode == AbstractFightMap.InteractiveMode.Target;

    [PublicAPI]
    public void SetTargetingPhase([NotNull] IEnumerable<Target> targets)
    {
      if (this.m_targetContext == null)
      {
        Log.Error("Targeting phase requested but no target context exists.", 130, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\AbstractFightMap.cs");
      }
      else
      {
        this.EndCurrentPhase();
        this.m_interactiveMode = AbstractFightMap.InteractiveMode.Target;
        this.m_targetContext.Begin(targets);
        this.m_cellPointerManager.SetCharacterFocusLayer();
        this.m_feedbackNeedsUpdate = true;
      }
    }

    [PublicAPI]
    public void EndCurrentPhase()
    {
      switch (this.m_interactiveMode)
      {
        case AbstractFightMap.InteractiveMode.None:
          return;
        case AbstractFightMap.InteractiveMode.Movement:
          if (this.m_pathFinder.tracking)
          {
            this.m_pathFinder.End();
            this.m_activeMovementContext.UpdateTarget((IEntityWithBoardPresence) null);
            this.m_cellPointerManager.SetAnimatedCursor(false);
            this.m_feedbackNeedsUpdate = true;
          }
          this.m_cellPointerManager.EndHighlightingPlayableCharacters();
          break;
        case AbstractFightMap.InteractiveMode.Target:
          if (this.m_targetContext != null && this.m_targetContext.End())
          {
            this.m_cellPointerManager.SetDefaultLayer();
            this.m_feedbackNeedsUpdate = true;
            break;
          }
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      this.m_inputHandler.SetDirty();
      this.m_interactiveMode = AbstractFightMap.InteractiveMode.None;
    }

    [PublicAPI]
    public bool HasAvailableTarget() => this.m_targetContext != null && this.m_targetContext.isActive;

    [PublicAPI]
    public void SetTargetInputMode(AbstractFightMap.TargetInputMode targetMode) => this.m_targetInputMode = targetMode;

    protected void Create()
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2Int vector2Int = mapDefinition.sizeMax - mapDefinition.sizeMin;
      this.m_cellObjectsByIndex = new CellObject[vector2Int.x * vector2Int.y];
      int length = this.m_cellObjects.Length;
      for (int index = 0; index < length; ++index)
      {
        CellObject cellObject = this.m_cellObjects[index];
        Vector2Int coords = cellObject.coords;
        this.m_cellObjectsByIndex[mapDefinition.GetCellIndex(coords.x, coords.y)] = cellObject;
        cellObject.Initialize((IMap) this);
      }
      this.m_virtualGrid = new MapVirtualGrid(mapDefinition, this.m_cellObjects);
      this.CreateCellHighlights();
    }

    protected BoxCollider CreateCollider()
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2Int sizeMin = mapDefinition.sizeMin;
      Vector2 vector2 = (Vector2) (mapDefinition.sizeMax - sizeMin);
      Vector3 vector3_1 = new Vector3(-0.5f, 0.0f, -0.5f);
      Vector3 vector3_2 = new Vector3((float) sizeMin.x, 0.0f, (float) sizeMin.y);
      Vector3 vector3_3 = new Vector3(vector2.x, 0.0f, vector2.y);
      BoxCollider collider = this.gameObject.AddComponent<BoxCollider>();
      collider.center = (Vector3) mapDefinition.origin + vector3_2 + 0.5f * vector3_3 + vector3_1;
      collider.size = new Vector3(vector2.x, 0.0f, vector2.y);
      collider.isTrigger = true;
      return collider;
    }

    protected void InitializeHandlers(BoxCollider mapCollider, bool giveUserControl)
    {
      CameraHandler current = CameraHandler.current;
      current.Initialize(this.m_mapDefinition, mapCollider.bounds, giveUserControl);
      this.m_inputHandler = new FightMapInputHandler((Collider) mapCollider, current);
    }

    private void CreateCellHighlights()
    {
      uint reflectionRenderMask = LayerMaskNames.doNotRenderInReflectionRenderMask;
      FightMapFeedbackResources feedbackResources = this.m_feedbackResources;
      Material material1;
      Material material2;
      if ((UnityEngine.Object) null == (UnityEngine.Object) feedbackResources)
      {
        material1 = (Material) null;
        material2 = (Material) null;
        Log.Error("Fight map has no feedback resources.", 271, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\AbstractFightMap.cs");
      }
      else
      {
        material1 = feedbackResources.areaFeedbackMaterial;
        if ((UnityEngine.Object) null == (UnityEngine.Object) material1)
          Log.Error("Feedback resources named '" + feedbackResources.name + "' has no highlight material.", 278, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\AbstractFightMap.cs");
        material2 = feedbackResources.movementFeedbackMaterial;
        if ((UnityEngine.Object) null == (UnityEngine.Object) material2)
          Log.Error("Feedback resources named '" + feedbackResources.name + "' has no movement highlight material.", 284, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\AbstractFightMap.cs");
      }
      GameObject gameObject = new GameObject("Highlight");
      CellHighlight prefab = gameObject.AddComponent<CellHighlight>();
      CellObject[] cellObjects = this.m_cellObjects;
      int length = cellObjects.Length;
      for (int index = 0; index < length; ++index)
        cellObjects[index].CreateHighlight(prefab, material1, reflectionRenderMask);
      UnityEngine.Object.Destroy((UnityEngine.Object) gameObject);
      this.m_pathFinderFeedbackManager.Initialize(this, material2, reflectionRenderMask);
      this.m_cellPointerManager.Initialize(feedbackResources.movementFeedbackResources, material2, reflectionRenderMask);
    }

    private void OnDestroy()
    {
      this.m_pathFinderFeedbackManager.Release();
      this.m_cellPointerManager.Release();
    }

    protected abstract void ApplyMovement(
      Vector2Int[] path,
      [NotNull] ICharacterEntity trackedCharacter,
      [CanBeNull] IEntityWithBoardPresence targetedEntity);

    [UsedImplicitly]
    protected virtual void Update()
    {
      this.UpdateSystems();
      switch (this.m_interactiveMode)
      {
        case AbstractFightMap.InteractiveMode.None:
          this.UpdatePreviewMode();
          break;
        case AbstractFightMap.InteractiveMode.Movement:
          this.UpdateMovementMode();
          break;
        case AbstractFightMap.InteractiveMode.Target:
          this.UpdateTargetMode();
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void LateUpdate()
    {
      if (this.m_feedbackNeedsUpdate)
      {
        this.UpdateFeedbacks();
        this.m_feedbackNeedsUpdate = false;
      }
      MapVirtualGrid virtualGrid = this.m_virtualGrid;
      List<CellObject> referenceCells = this.m_referenceCells;
      List<CellObject> linkedCells = this.m_linkedCells;
      List<AbstractFightMap.CellObjectAnimationInstance> animationInstances = this.m_cellObjectAnimationInstances;
      float deltaTime = Time.deltaTime;
      float gravityVelocity = deltaTime * this.m_gravity;
      if (this.m_virtualGridIsDirty)
      {
        referenceCells.Clear();
        virtualGrid.GetReferenceCellsNoAlloc(referenceCells);
        this.m_virtualGridIsDirty = false;
      }
      int count1 = referenceCells.Count;
      int count2 = animationInstances.Count;
      if (count2 > 0)
      {
        for (int index1 = 0; index1 < count2; ++index1)
        {
          AbstractFightMap.CellObjectAnimationInstance animationInstance = animationInstances[index1];
          for (int index2 = 0; index2 < count1; ++index2)
          {
            CellObject cellObject = referenceCells[index2];
            float heightDelta = animationInstance.Resolve(cellObject.coords);
            cellObject.ApplyAnimation(heightDelta);
          }
        }
        animationInstances.Clear();
        this.m_cellObjectNeedsCleanup = true;
      }
      else if (this.m_cellObjectNeedsCleanup)
      {
        bool flag = false;
        for (int index = 0; index < count1; ++index)
          flag = referenceCells[index].CleanupAnimation(deltaTime, gravityVelocity) | flag;
        this.m_cellObjectNeedsCleanup = flag;
      }
      for (int index3 = 0; index3 < count1; ++index3)
      {
        CellObject referenceCell = referenceCells[index3];
        bool isSleeping = referenceCell.ResolvePhysics(deltaTime, gravityVelocity);
        linkedCells.Clear();
        virtualGrid.GetLinkedCellsNoAlloc(referenceCell.coords, linkedCells);
        int count3 = linkedCells.Count;
        for (int index4 = 0; index4 < count3; ++index4)
          linkedCells[index4].CopyPhysics(referenceCell, isSleeping, deltaTime, gravityVelocity);
      }
    }

    private void UpdateSystems()
    {
      FightMapInputHandler inputHandler = this.m_inputHandler;
      if (!inputHandler.Update(this.m_mapDefinition))
        return;
      if (this.m_objectFocusedByCursor != null)
      {
        this.m_objectFocusedByCursor.SetFocus(false);
        this.m_objectFocusedByCursor = (IObjectWithFocus) null;
      }
      FightMapCellPointerManager cellPointerManager = this.m_cellPointerManager;
      Vector2Int? targetCell = inputHandler.targetCell;
      if (targetCell.HasValue)
      {
        Vector2Int vector2Int = targetCell.Value;
        int regionIndex;
        if (this.TryGetRegionIndex(vector2Int, out regionIndex))
        {
          FightMapMovementContext movementContext = this.m_movementContexts[regionIndex];
          IEntityWithBoardPresence character;
          movementContext.entityProvider.TryGetEntityAt<IEntityWithBoardPresence>(vector2Int, out character);
          CellObject referenceCell = this.m_virtualGrid.GetReferenceCell(vector2Int);
          if ((UnityEngine.Object) null != (UnityEngine.Object) referenceCell)
          {
            cellPointerManager.SetCursorPosition(referenceCell);
            cellPointerManager.ShowCursor();
          }
          else
            cellPointerManager.HideCursor();
          if (character != null && character.view is IObjectWithFocus view)
          {
            view.SetFocus(true);
            this.m_objectFocusedByCursor = view;
          }
          if (this.m_pathFinder.tracking)
          {
            if (movementContext == this.m_activeMovementContext)
            {
              ICharacterEntity trackedCharacter = movementContext.trackedCharacter;
              bool flag;
              if (movementContext.canDoActionOnTarget)
              {
                IEntityWithBoardPresence targeted = (IEntityWithBoardPresence) null;
                if (character != null && character != trackedCharacter && (movementContext.GetCell(vector2Int).state & FightMapMovementContext.CellState.Targetable) != FightMapMovementContext.CellState.None)
                  targeted = character;
                movementContext.UpdateTarget(targeted);
                flag = targeted != null;
              }
              else
                flag = false;
              if (movementContext.canMove)
              {
                if (flag)
                {
                  if (trackedCharacter.hasRange)
                    this.m_pathFinder.Reset();
                  else
                    this.m_pathFinder.Move(movementContext.stateProvider, movementContext.grid, vector2Int, true);
                }
                else if ((movementContext.GetCell(vector2Int).state & (FightMapMovementContext.CellState.Reachable | FightMapMovementContext.CellState.Occupied)) == FightMapMovementContext.CellState.Reachable)
                  this.m_pathFinder.Move(movementContext.stateProvider, movementContext.grid, vector2Int, false);
                else
                  this.m_pathFinder.Reset();
              }
              cellPointerManager.SetAnimatedCursor(flag);
            }
            else
            {
              this.m_pathFinder.Reset();
              cellPointerManager.SetAnimatedCursor(false);
            }
            this.m_feedbackNeedsUpdate = true;
            return;
          }
          if (this.m_interactiveMode != AbstractFightMap.InteractiveMode.Target || this.onTargetChanged == null)
            return;
          Target target;
          if (this.m_targetContext.TryGetTargetAt(vector2Int, out target))
          {
            if (target.type == Target.Type.Entity && target.entity == character)
              this.m_targetContext.UpdateTarget(vector2Int, character);
            else
              this.m_targetContext.UpdateTarget(vector2Int, (IEntityWithBoardPresence) null);
            this.onTargetChanged(new Target?(target), referenceCell);
            return;
          }
          this.m_targetContext.UpdateTarget(vector2Int, (IEntityWithBoardPresence) null);
          this.onTargetChanged(new Target?(), (CellObject) null);
          return;
        }
      }
      if (this.m_pathFinder.tracking)
      {
        this.m_pathFinder.Reset();
        this.m_feedbackNeedsUpdate = true;
      }
      this.m_cellPointerManager.HideCursor();
      if (this.m_interactiveMode != AbstractFightMap.InteractiveMode.Target)
        return;
      Action<Target?, CellObject> onTargetChanged = this.onTargetChanged;
      if (onTargetChanged == null)
        return;
      onTargetChanged(new Target?(), (CellObject) null);
    }

    private void UpdatePreviewMode()
    {
      FightMapInputHandler inputHandler = this.m_inputHandler;
      if (inputHandler.pressedMouseButton)
      {
        if (!inputHandler.mouseButtonPressLocation.HasValue)
          return;
        Vector2Int vector2Int = inputHandler.mouseButtonPressLocation.Value;
        int regionIndex;
        if (!this.TryGetRegionIndex(vector2Int, out regionIndex))
          return;
        FightMapMovementContext movementContext = this.m_movementContexts[regionIndex];
        ICharacterEntity character;
        if (!movementContext.entityProvider.TryGetEntityAt<ICharacterEntity>(vector2Int, out character))
          return;
        if (this.m_activeMovementContext != null)
          this.m_activeMovementContext.End();
        movementContext.Begin(character, this.m_pathFinder);
        this.m_activeMovementContext = movementContext;
        this.m_feedbackNeedsUpdate = true;
      }
      else
      {
        if (!inputHandler.releasedMouseButton)
          return;
        FightMapMovementContext activeMovementContext = this.m_activeMovementContext;
        if (activeMovementContext == null)
          return;
        activeMovementContext.End();
        this.m_activeMovementContext = (FightMapMovementContext) null;
        this.m_feedbackNeedsUpdate = true;
      }
    }

    private void UpdateMovementMode()
    {
      FightMapInputHandler inputHandler = this.m_inputHandler;
      if (inputHandler.pressedMouseButton)
      {
        if (this.m_pathFinder.tracking || !inputHandler.mouseButtonPressLocation.HasValue)
          return;
        Vector2Int vector2Int = inputHandler.mouseButtonPressLocation.Value;
        int regionIndex;
        if (!this.TryGetRegionIndex(vector2Int, out regionIndex))
          return;
        FightMapMovementContext movementContext = this.m_movementContexts[regionIndex];
        IMapEntityProvider entityProvider = movementContext.entityProvider;
        ICharacterEntity character;
        if (!entityProvider.TryGetEntityAt<ICharacterEntity>(vector2Int, out character))
          return;
        if (this.m_activeMovementContext != null)
          this.m_activeMovementContext.End();
        movementContext.Begin(character, this.m_pathFinder);
        this.m_activeMovementContext = movementContext;
        this.m_feedbackNeedsUpdate = true;
        if (entityProvider.IsCharacterPlayable(character))
        {
          this.m_pathFinder.Begin(vector2Int, character.movementPoints, movementContext.canPassThrough);
          this.m_cellPointerManager.EndHighlightingPlayableCharacters();
        }
        this.m_cellPointerManager.ShowCursor();
        this.m_cellPointerManager.SetAnimatedCursor(false);
      }
      else
      {
        if (!inputHandler.releasedMouseButton)
          return;
        FightMapMovementContext activeMovementContext = this.m_activeMovementContext;
        if (activeMovementContext == null)
          return;
        if (this.m_pathFinder.tracking)
        {
          this.m_cellPointerManager.BeginHighlightingPlayableCharacters((IMap) this, activeMovementContext.entityProvider);
          this.ApplyMovement(this.m_pathFinder.currentPath.ToArray(), activeMovementContext.trackedCharacter, activeMovementContext.targetedEntity);
          this.m_pathFinder.End();
          this.m_cellPointerManager.SetAnimatedCursor(false);
        }
        activeMovementContext.End();
        this.m_activeMovementContext = (FightMapMovementContext) null;
        this.m_feedbackNeedsUpdate = true;
      }
    }

    private void UpdateTargetMode()
    {
      Vector2Int? coords;
      if (!this.TargetInputEvent(out coords))
        return;
      if (this.onTargetSelected == null)
      {
        Log.Warning("A target was selected but nothing was listening.", 757, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\AbstractFightMap.cs");
      }
      else
      {
        Target target;
        if (coords.HasValue && this.m_targetContext.TryGetTargetAt(coords.Value, out target))
          this.onTargetSelected(new Target?(target));
        else
          this.onTargetSelected(new Target?());
      }
    }

    private void UpdateFeedbacks()
    {
      FightMapFeedbackResources feedbackResources = this.m_feedbackResources;
      if ((UnityEngine.Object) null == (UnityEngine.Object) feedbackResources)
        return;
      FightMapMovementContext[] movementContexts = this.m_movementContexts;
      int length = movementContexts.Length;
      for (int index = 0; index < length; ++index)
      {
        FightMapMovementContext mapMovementContext = movementContexts[index];
        if (mapMovementContext.hasEnded)
        {
          IMapDefinition mapDefinition = this.m_mapDefinition;
          IMapStateProvider stateProvider = mapMovementContext.stateProvider;
          Vector2Int sizeMin = stateProvider.sizeMin;
          Vector2Int sizeMax = stateProvider.sizeMax;
          for (int y = sizeMin.y; y < sizeMax.y; ++y)
          {
            for (int x = sizeMin.x; x < sizeMax.x; ++x)
            {
              CellObject cellObject = this.m_cellObjectsByIndex[mapDefinition.GetCellIndex(x, y)];
              if (!((UnityEngine.Object) null == (UnityEngine.Object) cellObject))
                cellObject.highlight.ClearSprite();
            }
          }
        }
      }
      FightMapTargetContext targetContext = this.m_targetContext;
      if (targetContext != null)
      {
        if (targetContext.isActive)
        {
          IMapDefinition mapDefinition = this.m_mapDefinition;
          IMapStateProvider stateProvider = targetContext.stateProvider;
          Vector2Int sizeMin = stateProvider.sizeMin;
          Vector2Int sizeMax = stateProvider.sizeMax;
          Color targetableAreaColor = feedbackResources.feedbackColors.targetableAreaColor;
          for (int y = sizeMin.y; y < sizeMax.y; ++y)
          {
            for (int x = sizeMin.x; x < sizeMax.x; ++x)
            {
              CellObject cellObject = this.m_cellObjectsByIndex[mapDefinition.GetCellIndex(x, y)];
              if (!((UnityEngine.Object) null == (UnityEngine.Object) cellObject))
                FightMapFeedbackHelper.SetupSpellTargetHighlight(feedbackResources, targetContext, cellObject.coords, cellObject.highlight, targetableAreaColor);
            }
          }
          this.m_pathFinderFeedbackManager.Clear();
          return;
        }
        if (targetContext.hasEnded)
        {
          IMapDefinition mapDefinition = this.m_mapDefinition;
          IMapStateProvider stateProvider = targetContext.stateProvider;
          Vector2Int sizeMin = stateProvider.sizeMin;
          Vector2Int sizeMax = stateProvider.sizeMax;
          for (int y = sizeMin.y; y < sizeMax.y; ++y)
          {
            for (int x = sizeMin.x; x < sizeMax.x; ++x)
            {
              CellObject cellObject = this.m_cellObjectsByIndex[mapDefinition.GetCellIndex(x, y)];
              if (!((UnityEngine.Object) null == (UnityEngine.Object) cellObject))
                cellObject.highlight.ClearSprite();
            }
          }
        }
      }
      FightMapMovementContext activeMovementContext = this.m_activeMovementContext;
      if (activeMovementContext == null)
      {
        this.m_pathFinderFeedbackManager.Clear();
      }
      else
      {
        ICharacterEntity trackedCharacter = activeMovementContext.trackedCharacter;
        if (trackedCharacter != null)
        {
          Color highlightColor = this.GetHighlightColor(feedbackResources.feedbackColors, activeMovementContext.entityProvider, trackedCharacter);
          IMapDefinition mapDefinition = this.m_mapDefinition;
          IMapStateProvider stateProvider = activeMovementContext.stateProvider;
          Vector2Int sizeMin = stateProvider.sizeMin;
          Vector2Int sizeMax = stateProvider.sizeMax;
          for (int y = sizeMin.y; y < sizeMax.y; ++y)
          {
            for (int x = sizeMin.x; x < sizeMax.x; ++x)
            {
              CellObject cellObject = this.m_cellObjectsByIndex[mapDefinition.GetCellIndex(x, y)];
              if (!((UnityEngine.Object) null == (UnityEngine.Object) cellObject))
                FightMapFeedbackHelper.SetupMovementAreaHighlight(feedbackResources, activeMovementContext, cellObject.coords, cellObject.highlight, highlightColor);
            }
          }
        }
        if (this.m_pathFinder.tracking)
        {
          Vector2Int? refCoord = activeMovementContext.targetedEntity?.area.refCoord;
          this.m_pathFinderFeedbackManager.Setup(feedbackResources.movementFeedbackResources, this.m_pathFinder.currentPath, refCoord);
        }
        else
          this.m_pathFinderFeedbackManager.Clear();
      }
    }

    private bool TargetInputEvent(out Vector2Int? coords)
    {
      FightMapInputHandler inputHandler = this.m_inputHandler;
      switch (this.m_targetInputMode)
      {
        case AbstractFightMap.TargetInputMode.Drag:
          coords = inputHandler.mouseButtonReleaseLocation;
          return inputHandler.releasedMouseButton;
        case AbstractFightMap.TargetInputMode.Click:
          coords = inputHandler.mouseButtonReleaseLocation;
          return inputHandler.clickedMouseButton;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private bool TryGetRegionIndex(Vector2Int coords, out int regionIndex)
    {
      FightMapMovementContext[] movementContexts = this.m_movementContexts;
      int length = movementContexts.Length;
      if (length == 1)
      {
        regionIndex = 0;
        return true;
      }
      for (int index = 0; index < length; ++index)
      {
        if (movementContexts[index].Contains(coords))
        {
          regionIndex = index;
          return true;
        }
      }
      regionIndex = -1;
      return false;
    }

    protected virtual Color GetHighlightColor(
      [NotNull] FightMapFeedbackColors feedbackColors,
      [NotNull] IMapEntityProvider mapEntityProvider,
      [NotNull] ICharacterEntity trackedCharacter)
    {
      return feedbackColors.targetableAreaColor;
    }

    public virtual CellObject GetCellObject(int x, int y) => this.m_cellObjectsByIndex[this.m_mapDefinition.GetCellIndex(x, y)];

    public virtual bool TryGetCellObject(int x, int y, out CellObject cellObject)
    {
      IMapDefinition mapDefinition = this.m_mapDefinition;
      Vector2 sizeMin = (Vector2) mapDefinition.sizeMin;
      if ((double) x < (double) sizeMin.x || (double) y < (double) sizeMin.y)
      {
        cellObject = (CellObject) null;
        return false;
      }
      Vector2 sizeMax = (Vector2) mapDefinition.sizeMax;
      if ((double) x >= (double) sizeMax.x || (double) y >= (double) sizeMax.y)
      {
        cellObject = (CellObject) null;
        return false;
      }
      int cellIndex = mapDefinition.GetCellIndex(x, y);
      cellObject = this.m_cellObjectsByIndex[cellIndex];
      return (UnityEngine.Object) null != (UnityEngine.Object) cellObject;
    }

    public virtual Vector2Int GetCellCoords(Vector3 worldPosition)
    {
      Vector3Int origin = this.m_mapDefinition.origin;
      Vector3Int vector3Int = new Vector3Int(Mathf.FloorToInt(worldPosition.x), origin.y, Mathf.FloorToInt(worldPosition.z)) - origin;
      return new Vector2Int(vector3Int.x, vector3Int.z);
    }

    public void AddArea(Area area)
    {
      this.m_virtualGrid.AddArea(area);
      this.m_virtualGridIsDirty = true;
    }

    public void MoveArea(Area from, Area to)
    {
      this.m_virtualGrid.MoveArea(from, to);
      this.m_virtualGridIsDirty = true;
    }

    public void RemoveArea(Area area)
    {
      this.m_virtualGrid.RemoveArea(area);
      this.m_virtualGridIsDirty = true;
    }

    public virtual MapCellIndicator GetCellIndicator(int x, int y) => MapCellIndicator.None;

    public void ApplyCellObjectAnimation(
      [NotNull] CellObjectAnimationParameters parameters,
      Vector2Int origin,
      Quaternion rotation,
      float strength,
      float time)
    {
      if (!parameters.isValid)
        return;
      this.m_cellObjectAnimationInstances.Add(new AbstractFightMap.CellObjectAnimationInstance(parameters, origin, rotation, strength, time));
    }

    protected enum InteractiveMode
    {
      None,
      Movement,
      Target,
    }

    public enum TargetInputMode
    {
      Drag,
      Click,
    }

    private struct CellObjectAnimationInstance
    {
      private readonly CellObjectAnimationParameters m_parameters;
      private readonly Vector2Int m_origin;
      private readonly Quaternion m_rotation;
      private readonly float m_strength;
      private readonly float m_time;
      private readonly Vector2Int m_minBounds;
      private readonly Vector2Int m_maxBounds;

      public CellObjectAnimationInstance(
        CellObjectAnimationParameters parameters,
        Vector2Int origin,
        Quaternion rotation,
        float strength,
        float time)
      {
        this.m_parameters = parameters;
        this.m_origin = origin;
        this.m_rotation = rotation;
        this.m_strength = strength;
        this.m_time = time;
        parameters.GetBounds(origin, rotation, time, out this.m_minBounds, out this.m_maxBounds);
      }

      public float Resolve(Vector2Int coords)
      {
        int x = coords.x;
        int y = coords.y;
        return x < this.m_minBounds.x || x > this.m_maxBounds.x || y < this.m_minBounds.y || y > this.m_maxBounds.y ? 0.0f : this.m_strength * this.m_parameters.Compute(coords, this.m_origin, this.m_rotation, this.m_time);
      }
    }
  }
}
