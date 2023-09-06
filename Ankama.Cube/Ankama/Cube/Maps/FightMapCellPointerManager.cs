// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapCellPointerManager
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Extensions;
using Ankama.Cube.Fight.Entities;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.SRP;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class FightMapCellPointerManager
  {
    private const float Shift = -0.01f;
    private const float Height = 0.51f;
    private const int InitialCapacity = 4;
    private readonly List<CellPointer> m_active = new List<CellPointer>(4);
    private DirectionAngle m_mapRotation;
    private GameObject m_prefab;
    private GameObjectPool m_pool;
    private bool m_displayPlayableCharactersHighlights;
    private CellPointer m_cursor;

    public void Initialize(
      MovementFeedbackResources resources,
      Material material,
      uint renderLayerMask)
    {
      Sprite allyCursorSprite = resources.allyCursorSprite;
      GameObject gameObject1 = new GameObject("", new System.Type[1]
      {
        typeof (CellPointer)
      });
      CellPointer component1 = gameObject1.GetComponent<CellPointer>();
      component1.Initialize(material, renderLayerMask);
      component1.SetSprite(allyCursorSprite);
      gameObject1.SetActive(false);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) gameObject1);
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      CellPointer component2 = gameObject2.GetComponent<CellPointer>();
      component2.Initialize(material, renderLayerMask);
      gameObject2.SetActive(true);
      this.m_pool = new GameObjectPool(gameObject1, 4);
      this.m_prefab = gameObject1;
      this.m_cursor = component2;
      CellPointer.Initialize();
    }

    public void SetMapRotation(DirectionAngle mapRotation)
    {
      this.m_mapRotation = mapRotation;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor))
        return;
      this.m_cursor.transform.rotation = mapRotation.GetInverseRotation();
    }

    public void Release()
    {
      CellPointer.Release();
      List<CellPointer> active = this.m_active;
      int count = active.Count;
      for (int index = 0; index < count; ++index)
      {
        CellPointer cellPointer = active[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) cellPointer)
          UnityEngine.Object.Destroy((UnityEngine.Object) cellPointer.gameObject);
      }
      active.Clear();
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_cursor.gameObject);
        this.m_cursor = (CellPointer) null;
      }
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

    public void SetCursorPosition([NotNull] CellObject parent)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_cursor)
        return;
      Transform transform1 = parent.transform;
      Transform transform2 = this.m_cursor.transform;
      transform2.SetParent(transform1, true);
      Quaternion inverseRotation = this.m_mapRotation.GetInverseRotation();
      transform2.SetPositionAndRotation(transform1.position + inverseRotation * new Vector3(-0.01f, 0.51f, -0.01f), this.m_mapRotation.GetInverseRotation() * Quaternion.Euler(90f, 0.0f, 0.0f));
      if (!this.m_displayPlayableCharactersHighlights)
        return;
      List<CellPointer> active = this.m_active;
      int count = active.Count;
      for (int index = 0; index < count; ++index)
      {
        CellPointer cellPointer = active[index];
        if (!((UnityEngine.Object) null == (UnityEngine.Object) cellPointer) && (UnityEngine.Object) cellPointer.transform.parent == (UnityEngine.Object) transform1)
        {
          this.m_cursor.gameObject.SetActive(false);
          return;
        }
      }
      this.m_cursor.gameObject.SetActive(true);
    }

    public void ShowCursor()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_cursor)
        return;
      this.m_cursor.Show();
    }

    public void HideCursor()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_cursor)
        return;
      this.m_cursor.Hide();
    }

    public void SetAnimatedCursor(bool value)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_cursor)
        return;
      this.m_cursor.SetAnimated(value);
    }

    public void BeginHighlightingPlayableCharacters(IMap map, IMapEntityProvider entityProvider)
    {
      if (this.m_displayPlayableCharactersHighlights)
        return;
      GameObjectPool pool = this.m_pool;
      if (pool == null)
        return;
      List<CellPointer> active = this.m_active;
      Quaternion inverseRotation = this.m_mapRotation.GetInverseRotation();
      Vector3 vector3 = inverseRotation * new Vector3(-0.01f, 0.51f, -0.01f);
      Quaternion rotation = inverseRotation * Quaternion.Euler(90f, 0.0f, 0.0f);
      Transform parent = (UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor ? this.m_cursor.transform.parent : (Transform) null;
      foreach (IEntityWithBoardPresence playableCharacter in entityProvider.EnumeratePlayableCharacters())
      {
        Vector2Int refCoord = playableCharacter.area.refCoord;
        Transform transform = map.GetCellObject(refCoord.x, refCoord.y).transform;
        Vector3 position = transform.position + vector3;
        GameObject gameObject = pool.Instantiate(position, rotation, transform);
        CellPointer component = gameObject.GetComponent<CellPointer>();
        component.SetAnimated(true);
        component.Show();
        gameObject.SetActive(true);
        active.Add(component);
        if ((UnityEngine.Object) transform == (UnityEngine.Object) parent)
          this.m_cursor.gameObject.SetActive(false);
      }
      this.m_displayPlayableCharactersHighlights = true;
    }

    public void RefreshPlayableCharactersHighlights(IMap map, IMapEntityProvider entityProvider)
    {
      this.EndHighlightingPlayableCharacters();
      this.BeginHighlightingPlayableCharacters(map, entityProvider);
    }

    public void EndHighlightingPlayableCharacters()
    {
      if (!this.m_displayPlayableCharactersHighlights)
        return;
      GameObjectPool pool = this.m_pool;
      if (pool == null)
        return;
      List<CellPointer> active = this.m_active;
      int count = active.Count;
      for (int index = 0; index < count; ++index)
      {
        CellPointer cellPointer = active[index];
        if (!((UnityEngine.Object) null == (UnityEngine.Object) cellPointer))
          pool.Release(cellPointer.gameObject);
      }
      active.Clear();
      this.m_displayPlayableCharactersHighlights = false;
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor))
        return;
      this.m_cursor.gameObject.SetActive(true);
    }

    public void SetCharacterFocusLayer()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor))
        return;
      this.m_cursor.gameObject.layer = LayerMaskNames.characterFocusLayer;
    }

    public void SetDefaultLayer()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_cursor))
        return;
      this.m_cursor.gameObject.layer = LayerMaskNames.defaultLayer;
    }
  }
}
