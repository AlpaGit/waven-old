// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightPathFinderFeedbackManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class FightPathFinderFeedbackManager
  {
    private const float Height = 0.51f;
    private const int InitialCapacity = 4;
    private readonly List<SpriteRenderer> m_active = new List<SpriteRenderer>(4);
    private AbstractFightMap m_fightMap;
    private DirectionAngle m_mapRotation;
    private GameObject m_prefab;
    private GameObjectPool m_pool;

    public void Initialize(AbstractFightMap fightMap, Material material, uint renderLayerMask)
    {
      this.m_fightMap = fightMap;
      GameObject gameObject = new GameObject("", new System.Type[1]
      {
        typeof (SpriteRenderer)
      });
      SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
      component.sharedMaterial = material;
      component.renderingLayerMask = renderLayerMask;
      gameObject.SetActive(false);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject);
      this.m_pool = new GameObjectPool(gameObject, 4);
      this.m_prefab = gameObject;
    }

    public void SetMapRotation(DirectionAngle mapRotation) => this.m_mapRotation = mapRotation;

    public void Release()
    {
      List<SpriteRenderer> active = this.m_active;
      int count = active.Count;
      for (int index = 0; index < count; ++index)
      {
        SpriteRenderer spriteRenderer = active[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) spriteRenderer)
          UnityEngine.Object.Destroy((UnityEngine.Object) spriteRenderer.gameObject);
      }
      active.Clear();
      if (this.m_pool != null)
      {
        this.m_pool.Dispose();
        this.m_pool = (GameObjectPool) null;
      }
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_prefab))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.m_prefab);
      this.m_prefab = (GameObject) null;
    }

    public void Setup(
      MovementFeedbackResources resources,
      List<Vector2Int> path,
      Vector2Int? target)
    {
      int count1 = path.Count;
      if (count1 < 2)
      {
        this.Clear();
      }
      else
      {
        Sprite[] sprites = resources.sprites;
        DirectionAngle mapRotation = this.m_mapRotation;
        List<SpriteRenderer> active = this.m_active;
        int count2 = active.Count;
        Vector2Int vector2Int1 = path[0];
        Vector2Int vector2Int2 = path[1];
        Ankama.Cube.Data.Direction previousDirection = vector2Int1.GetDirectionTo(vector2Int2).Rotate(mapRotation);
        Ankama.Cube.Data.Direction direction1 = previousDirection;
        if (count2 >= count1)
        {
          for (int index = count2 - 1; index >= count1; --index)
          {
            this.m_pool.Release(active[index].gameObject);
            this.m_active.RemoveAt(index);
          }
          this.SetStartSprite(sprites, active[0], vector2Int1, direction1);
          for (int index = 1; index < count1 - 1; ++index)
          {
            Vector2Int vector2Int3 = vector2Int2;
            vector2Int2 = path[index + 1];
            direction1 = vector2Int3.GetDirectionTo(vector2Int2).Rotate(mapRotation);
            this.SetSprite(sprites, active[index], vector2Int3, direction1, previousDirection);
            previousDirection = direction1;
          }
          if (target.HasValue)
          {
            Ankama.Cube.Data.Direction direction2 = vector2Int2.GetDirectionTo(target.Value).Rotate(mapRotation);
            this.SetSprite(sprites, active[count1 - 1], vector2Int2, direction2, previousDirection);
          }
          else
            this.SetEndSprite(sprites, active[count1 - 1], vector2Int2, direction1);
        }
        else if (count2 > 0)
        {
          this.SetStartSprite(sprites, active[0], vector2Int1, direction1);
          for (int index = 1; index < count2; ++index)
          {
            Vector2Int from = vector2Int2;
            vector2Int2 = path[index + 1];
            direction1 = from.GetDirectionTo(vector2Int2).Rotate(mapRotation);
            this.SetSprite(sprites, active[index], path[index], direction1, previousDirection);
            previousDirection = direction1;
          }
          for (int index = count2; index < count1 - 1; ++index)
          {
            Vector2Int from = vector2Int2;
            vector2Int2 = path[index + 1];
            direction1 = from.GetDirectionTo(vector2Int2).Rotate(mapRotation);
            this.SetSprite(sprites, (SpriteRenderer) null, path[index], direction1, previousDirection);
            previousDirection = direction1;
          }
          if (target.HasValue)
          {
            Ankama.Cube.Data.Direction direction3 = vector2Int2.GetDirectionTo(target.Value).Rotate(mapRotation);
            this.SetSprite(sprites, (SpriteRenderer) null, vector2Int2, direction3, previousDirection);
          }
          else
            this.SetEndSprite(sprites, (SpriteRenderer) null, vector2Int2, direction1);
        }
        else
        {
          this.SetStartSprite(sprites, (SpriteRenderer) null, vector2Int1, direction1);
          for (int index = 1; index < count1 - 1; ++index)
          {
            Vector2Int from = vector2Int2;
            vector2Int2 = path[index + 1];
            direction1 = from.GetDirectionTo(vector2Int2).Rotate(mapRotation);
            this.SetSprite(sprites, (SpriteRenderer) null, path[index], direction1, previousDirection);
            previousDirection = direction1;
          }
          if (target.HasValue)
          {
            Ankama.Cube.Data.Direction direction4 = vector2Int2.GetDirectionTo(target.Value).Rotate(mapRotation);
            this.SetSprite(sprites, (SpriteRenderer) null, vector2Int2, direction4, previousDirection);
          }
          else
            this.SetEndSprite(sprites, (SpriteRenderer) null, vector2Int2, direction1);
        }
      }
    }

    public void Clear()
    {
      List<SpriteRenderer> active = this.m_active;
      int count = active.Count;
      if (count == 0)
        return;
      for (int index = 0; index < count; ++index)
        this.m_pool.Release(active[index].gameObject);
      active.Clear();
    }

    private void SetStartSprite(
      Sprite[] sprites,
      SpriteRenderer instance,
      Vector2Int coord,
      Ankama.Cube.Data.Direction direction)
    {
      Transform transform1 = this.m_fightMap.GetCellObject(coord.x, coord.y).transform;
      Transform transform2;
      if ((UnityEngine.Object) null == (UnityEngine.Object) instance)
      {
        GameObject gameObject = this.m_pool.Instantiate(transform1);
        instance = gameObject.GetComponent<SpriteRenderer>();
        transform2 = gameObject.transform;
        this.m_active.Add(instance);
      }
      else
      {
        transform2 = instance.transform;
        transform2.SetParent(transform1, false);
      }
      Sprite sprite;
      bool flag;
      switch (direction)
      {
        case Ankama.Cube.Data.Direction.SouthEast:
          sprite = sprites[6];
          flag = true;
          break;
        case Ankama.Cube.Data.Direction.SouthWest:
          sprite = sprites[6];
          flag = false;
          break;
        case Ankama.Cube.Data.Direction.NorthWest:
          sprite = sprites[7];
          flag = false;
          break;
        case Ankama.Cube.Data.Direction.NorthEast:
          sprite = sprites[7];
          flag = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Quaternion rotation = this.m_mapRotation.GetInverseRotation() * Quaternion.Euler(90f, flag ? 90f : 0.0f, 0.0f);
      instance.sprite = sprite;
      instance.flipX = flag;
      transform2.SetPositionAndRotation(transform1.position + new Vector3(0.0f, 0.51f, 0.0f), rotation);
      instance.gameObject.SetActive(true);
    }

    private void SetSprite(
      Sprite[] sprites,
      SpriteRenderer instance,
      Vector2Int coord,
      Ankama.Cube.Data.Direction direction,
      Ankama.Cube.Data.Direction previousDirection)
    {
      Transform transform1 = this.m_fightMap.GetCellObject(coord.x, coord.y).transform;
      Transform transform2;
      if ((UnityEngine.Object) null == (UnityEngine.Object) instance)
      {
        GameObject gameObject = this.m_pool.Instantiate(transform1);
        instance = gameObject.GetComponent<SpriteRenderer>();
        transform2 = gameObject.transform;
        this.m_active.Add(instance);
      }
      else
      {
        transform2 = instance.transform;
        transform2.SetParent(transform1, false);
      }
      Sprite sprite;
      bool flag;
      if (direction == previousDirection)
      {
        switch (direction)
        {
          case Ankama.Cube.Data.Direction.SouthEast:
            sprite = sprites[8];
            flag = false;
            break;
          case Ankama.Cube.Data.Direction.SouthWest:
            sprite = sprites[8];
            flag = true;
            break;
          case Ankama.Cube.Data.Direction.NorthWest:
            sprite = sprites[9];
            flag = false;
            break;
          case Ankama.Cube.Data.Direction.NorthEast:
            sprite = sprites[9];
            flag = true;
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      else
      {
        switch (previousDirection)
        {
          case Ankama.Cube.Data.Direction.SouthEast:
            switch (direction)
            {
              case Ankama.Cube.Data.Direction.SouthWest:
                sprite = sprites[0];
                flag = true;
                break;
              case Ankama.Cube.Data.Direction.NorthWest:
                sprite = sprites[9];
                flag = false;
                break;
              case Ankama.Cube.Data.Direction.NorthEast:
                sprite = sprites[1];
                flag = true;
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            break;
          case Ankama.Cube.Data.Direction.SouthWest:
            switch (direction)
            {
              case Ankama.Cube.Data.Direction.SouthEast:
                sprite = sprites[0];
                flag = false;
                break;
              case Ankama.Cube.Data.Direction.NorthWest:
                sprite = sprites[1];
                flag = false;
                break;
              case Ankama.Cube.Data.Direction.NorthEast:
                sprite = sprites[9];
                flag = true;
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            break;
          case Ankama.Cube.Data.Direction.NorthWest:
            switch (direction)
            {
              case Ankama.Cube.Data.Direction.SouthEast:
                sprite = sprites[8];
                flag = false;
                break;
              case Ankama.Cube.Data.Direction.SouthWest:
                sprite = sprites[2];
                flag = true;
                break;
              case Ankama.Cube.Data.Direction.NorthEast:
                sprite = sprites[3];
                flag = true;
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            break;
          case Ankama.Cube.Data.Direction.NorthEast:
            switch (direction)
            {
              case Ankama.Cube.Data.Direction.SouthEast:
                sprite = sprites[2];
                flag = false;
                break;
              case Ankama.Cube.Data.Direction.SouthWest:
                sprite = sprites[8];
                flag = true;
                break;
              case Ankama.Cube.Data.Direction.NorthWest:
                sprite = sprites[3];
                flag = false;
                break;
              default:
                throw new ArgumentOutOfRangeException();
            }
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
      Quaternion rotation = this.m_mapRotation.GetInverseRotation() * Quaternion.Euler(90f, flag ? 90f : 0.0f, 0.0f);
      instance.sprite = sprite;
      instance.flipX = flag;
      transform2.SetPositionAndRotation(transform1.position + new Vector3(0.0f, 0.51f, 0.0f), rotation);
      instance.gameObject.SetActive(true);
    }

    private void SetEndSprite(
      Sprite[] sprites,
      SpriteRenderer instance,
      Vector2Int coord,
      Ankama.Cube.Data.Direction direction)
    {
      Transform transform1 = this.m_fightMap.GetCellObject(coord.x, coord.y).transform;
      Transform transform2;
      if ((UnityEngine.Object) null == (UnityEngine.Object) instance)
      {
        GameObject gameObject = this.m_pool.Instantiate(transform1);
        instance = gameObject.GetComponent<SpriteRenderer>();
        transform2 = gameObject.transform;
        this.m_active.Add(instance);
      }
      else
      {
        transform2 = instance.transform;
        transform2.SetParent(transform1, false);
      }
      Sprite sprite;
      bool flag;
      switch (direction)
      {
        case Ankama.Cube.Data.Direction.SouthEast:
          sprite = sprites[4];
          flag = true;
          break;
        case Ankama.Cube.Data.Direction.SouthWest:
          sprite = sprites[4];
          flag = false;
          break;
        case Ankama.Cube.Data.Direction.NorthWest:
          sprite = sprites[5];
          flag = false;
          break;
        case Ankama.Cube.Data.Direction.NorthEast:
          sprite = sprites[5];
          flag = true;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      Quaternion rotation = this.m_mapRotation.GetInverseRotation() * Quaternion.Euler(90f, flag ? 90f : 0.0f, 0.0f);
      instance.sprite = sprite;
      instance.flipX = flag;
      transform2.SetPositionAndRotation(transform1.position + new Vector3(0.0f, 0.51f, 0.0f), rotation);
      instance.gameObject.SetActive(true);
    }
  }
}
