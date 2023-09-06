// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapTargetContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.SRP;
using Ankama.Utilities;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class FightMapTargetContext
  {
    private const float TargetCharacterFocusFactor = 0.6f;
    private const float TweenInDelay = 0.4f;
    private const float TweenInDuration = 0.433333337f;
    private const float TweenOutDelay = 0.0833333358f;
    private const float TweenOutDuration = 0.333333343f;
    public readonly IMapStateProvider stateProvider;
    private readonly List<Target> m_data = new List<Target>();
    private readonly Dictionary<int, Target> m_targets = new Dictionary<int, Target>();
    private TweenerCore<float, float, FloatOptions> m_characterFocusTween;
    private FightMapTargetContext.CharacterFocusTweenState m_characterFocusTweenState;
    private float m_characterFocusFactor;
    public IEntityWithBoardPresence targetedEntity;
    private bool m_hasEnded;

    public bool isActive { get; private set; }

    public bool hasEnded
    {
      get
      {
        int num = this.m_hasEnded ? 1 : 0;
        this.m_hasEnded = false;
        return num != 0;
      }
    }

    public FightMapTargetContext([NotNull] IMapStateProvider stateProvider) => this.stateProvider = stateProvider;

    public void Begin([NotNull] IEnumerable<Target> targetEnumerable)
    {
      IMapStateProvider stateProvider = this.stateProvider;
      List<Target> data = this.m_data;
      Dictionary<int, Target> targets = this.m_targets;
      bool flag = false;
      foreach (Target target in targetEnumerable)
      {
        data.Add(target);
        switch (target.type)
        {
          case Target.Type.Coord:
            Coord coord = target.coord;
            int cellIndex1 = stateProvider.GetCellIndex(coord.x, coord.y);
            targets.Add(cellIndex1, target);
            continue;
          case Target.Type.Entity:
            if (target.entity is IEntityWithBoardPresence entity)
            {
              if (entity.view is ICharacterObject view)
                view.ShowSpellTargetFeedback(false);
              Vector2Int[] occupiedCoords = entity.area.occupiedCoords;
              int length = occupiedCoords.Length;
              for (int index = 0; index < length; ++index)
              {
                Vector2Int vector2Int = occupiedCoords[index];
                int cellIndex2 = stateProvider.GetCellIndex(vector2Int.x, vector2Int.y);
                targets.Add(cellIndex2, target);
              }
              flag = true;
              continue;
            }
            continue;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      if (flag)
        this.StartCharacterFocus();
      this.isActive = targets.Count > 0;
    }

    public void UpdateTarget(Vector2Int targetCellCoords, IEntityWithBoardPresence targetEntity)
    {
      if (this.targetedEntity != null && this.targetedEntity.view is ICharacterObject view1)
        view1.ShowSpellTargetFeedback(false);
      if (targetEntity != null && targetEntity.view is ICharacterObject view2)
        view2.ShowSpellTargetFeedback(true);
      this.targetedEntity = targetEntity;
    }

    public bool End()
    {
      this.StopCharacterFocus();
      if (!this.isActive)
        return false;
      Dictionary<int, Target> targets = this.m_targets;
      foreach (Target target in targets.Values)
      {
        if (target.type == Target.Type.Entity && target.entity is IEntityWithBoardPresence entity && entity.view is ICharacterObject view)
          view.HideSpellTargetFeedback();
      }
      targets.Clear();
      this.m_data.Clear();
      this.m_hasEnded = true;
      this.targetedEntity = (IEntityWithBoardPresence) null;
      this.isActive = false;
      return true;
    }

    public void Refresh()
    {
      if (!this.isActive)
        return;
      IMapStateProvider stateProvider = this.stateProvider;
      List<Target> data = this.m_data;
      Dictionary<int, Target> targets = this.m_targets;
      targets.Clear();
      int count = data.Count;
      for (int index1 = 0; index1 < count; ++index1)
      {
        Target target = data[index1];
        switch (target.type)
        {
          case Target.Type.Coord:
            Coord coord = target.coord;
            int cellIndex1 = stateProvider.GetCellIndex(coord.x, coord.y);
            targets.Add(cellIndex1, target);
            break;
          case Target.Type.Entity:
            if (target.entity is IEntityWithBoardPresence entity)
            {
              Vector2Int[] occupiedCoords = entity.area.occupiedCoords;
              int length = occupiedCoords.Length;
              for (int index2 = 0; index2 < length; ++index2)
              {
                Vector2Int vector2Int = occupiedCoords[index2];
                int cellIndex2 = stateProvider.GetCellIndex(vector2Int.x, vector2Int.y);
                targets.Add(cellIndex2, target);
              }
              break;
            }
            Log.Error(string.Format("Entity target with id {0} does not implement {1}.", (object) target.entity.id, (object) "IEntityWithBoardPresence"), 228, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\FightMapTargetContext.cs");
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    public bool HasNonEntityTargetAt(Vector2Int coords)
    {
      Target target;
      return this.m_targets.TryGetValue(this.stateProvider.GetCellIndex(coords.x, coords.y), out target) && target.type != Target.Type.Entity;
    }

    public bool TryGetTargetAt(Vector2Int coords, out Target target) => this.m_targets.TryGetValue(this.stateProvider.GetCellIndex(coords.x, coords.y), out target);

    private void StartCharacterFocus()
    {
      switch (this.m_characterFocusTweenState)
      {
        case FightMapTargetContext.CharacterFocusTweenState.None:
          float num = (float) (0.43333333730697632 * (0.60000002384185791 - (double) this.m_characterFocusFactor) / 0.60000002384185791);
          if (Mathf.Approximately(0.0f, num))
          {
            this.m_characterFocusTweenState = FightMapTargetContext.CharacterFocusTweenState.None;
            break;
          }
          this.m_characterFocusTween = DOTween.To(new DOGetter<float>(this.CharacterFocusTweenGetter), new DOSetter<float>(this.CharacterFocusTweenSetter), 0.6f, num).SetEase<TweenerCore<float, float, FloatOptions>>(Ease.InOutQuad).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnCharacterFocusTweenComplete));
          if (this.m_characterFocusTweenState == FightMapTargetContext.CharacterFocusTweenState.None)
            this.m_characterFocusTween.SetDelay<TweenerCore<float, float, FloatOptions>>(0.4f);
          this.m_characterFocusTweenState = FightMapTargetContext.CharacterFocusTweenState.In;
          break;
        case FightMapTargetContext.CharacterFocusTweenState.In:
          break;
        case FightMapTargetContext.CharacterFocusTweenState.Out:
          this.m_characterFocusTween.Kill();
          this.m_characterFocusTween = (TweenerCore<float, float, FloatOptions>) null;
          goto case FightMapTargetContext.CharacterFocusTweenState.None;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void StopCharacterFocus()
    {
      switch (this.m_characterFocusTweenState)
      {
        case FightMapTargetContext.CharacterFocusTweenState.None:
          float num = (float) (0.3333333432674408 * (double) this.m_characterFocusFactor / 0.60000002384185791);
          if (Mathf.Approximately(0.0f, num))
          {
            this.m_characterFocusTweenState = FightMapTargetContext.CharacterFocusTweenState.None;
            break;
          }
          this.m_characterFocusTween = DOTween.To(new DOGetter<float>(this.CharacterFocusTweenGetter), new DOSetter<float>(this.CharacterFocusTweenSetter), 0.0f, num).SetEase<TweenerCore<float, float, FloatOptions>>(Ease.InOutQuad).OnComplete<TweenerCore<float, float, FloatOptions>>(new TweenCallback(this.OnCharacterFocusTweenComplete));
          if (this.m_characterFocusTweenState == FightMapTargetContext.CharacterFocusTweenState.None)
            this.m_characterFocusTween.SetDelay<TweenerCore<float, float, FloatOptions>>(0.0833333358f);
          this.m_characterFocusTweenState = FightMapTargetContext.CharacterFocusTweenState.Out;
          break;
        case FightMapTargetContext.CharacterFocusTweenState.In:
          this.m_characterFocusTween.Kill();
          this.m_characterFocusTween = (TweenerCore<float, float, FloatOptions>) null;
          goto case FightMapTargetContext.CharacterFocusTweenState.None;
        case FightMapTargetContext.CharacterFocusTweenState.Out:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private float CharacterFocusTweenGetter() => this.m_characterFocusFactor;

    private void CharacterFocusTweenSetter(float value)
    {
      CubeSRP.characterFocusFactor = value;
      this.m_characterFocusFactor = value;
    }

    private void OnCharacterFocusTweenComplete()
    {
      this.m_characterFocusTweenState = FightMapTargetContext.CharacterFocusTweenState.None;
      this.m_characterFocusTween = (TweenerCore<float, float, FloatOptions>) null;
    }

    private enum CharacterFocusTweenState
    {
      None,
      In,
      Out,
    }
  }
}
