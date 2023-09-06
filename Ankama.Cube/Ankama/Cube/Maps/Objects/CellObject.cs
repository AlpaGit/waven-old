// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.CellObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps.Feedbacks;
using Ankama.Utilities;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [UsedImplicitly]
  public sealed class CellObject : MonoBehaviour
  {
    [UsedImplicitly]
    [SerializeField]
    private Vector2Int m_coords;
    private readonly List<IsoObject> m_childrenIsoObjects = new List<IsoObject>();
    private readonly List<DynamicObject> m_rootDynamicChildren = new List<DynamicObject>();
    private Vector3 m_originalLocalPosition;
    private Vector3 m_localUpVector;
    private bool m_isSleeping = true;
    private float m_controlledHeightBuffer;
    private float m_controlledLocalHeight;
    private float m_controlledVelocity;

    public IMap parentMap { get; private set; }

    public Vector2Int coords => this.m_coords;

    public CellHighlight highlight { get; private set; }

    public void Initialize([NotNull] IMap map)
    {
      this.parentMap = map;
      Transform transform = this.transform;
      Transform parent = transform.parent;
      this.m_originalLocalPosition = transform.localPosition;
      this.m_localUpVector = (Object) null == (Object) parent ? Vector3.up : parent.InverseTransformDirection(Vector3.up);
      this.PrepareIsoObjects();
      this.BuildDynamicObjectsHierarchy(transform);
    }

    public void CreateHighlight(CellHighlight prefab, Material material, uint renderLayerMask)
    {
      CellHighlight cellHighlight = Object.Instantiate<CellHighlight>(prefab, this.transform, false);
      Transform transform = cellHighlight.transform;
      transform.localPosition = this.transform.InverseTransformDirection(Vector3.up) * 0.501f;
      transform.forward = -Vector3.up;
      cellHighlight.Initialize(material, renderLayerMask);
      this.highlight = cellHighlight;
    }

    public void AcquireIsoObject([NotNull] IsoObject isoObject)
    {
      this.m_childrenIsoObjects.Add(isoObject);
      this.parentMap.AddArea(isoObject.area);
    }

    public void AcquireIsoObject([NotNull] IsoObject isoObject, [NotNull] CellObject other)
    {
      this.m_childrenIsoObjects.Add(isoObject);
      other.m_childrenIsoObjects.Remove(isoObject);
      Area area = isoObject.area;
      Area copy = area.GetCopy();
      area.MoveTo(this.coords);
      this.parentMap.MoveArea(copy, area);
    }

    public void RemoveIsoObject([NotNull] IsoObject isoObject)
    {
      List<IsoObject> childrenIsoObjects = this.m_childrenIsoObjects;
      int count = childrenIsoObjects.Count;
      for (int index = 0; index < count; ++index)
      {
        if ((Object) childrenIsoObjects[index] == (Object) isoObject)
        {
          this.parentMap.RemoveArea(isoObject.area);
          childrenIsoObjects.RemoveAt(index);
          return;
        }
      }
      Log.Warning(string.Format("Could not find IsoObject '{0}' to remove in cell object named '{1}'.", (object) isoObject, (object) this.name), 113, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\CellObject.cs");
    }

    public bool TryGetIsoObject<T>(out T isoObject) where T : class, IIsoObject
    {
      List<IsoObject> childrenIsoObjects = this.m_childrenIsoObjects;
      int count = childrenIsoObjects.Count;
      for (int index = 0; index < count; ++index)
      {
        if (childrenIsoObjects[index] is T obj)
        {
          isoObject = obj;
          return true;
        }
      }
      isoObject = default (T);
      return false;
    }

    private void PrepareIsoObjects()
    {
      List<IsoObject> childrenIsoObjects = this.m_childrenIsoObjects;
      this.GetComponentsInChildren<IsoObject>(childrenIsoObjects);
      int count = childrenIsoObjects.Count;
      for (int index = 0; index < count; ++index)
        childrenIsoObjects[index].AttachToCell(this);
    }

    [PublicAPI]
    public void ApplyAnimation(float heightDelta)
    {
      this.m_controlledHeightBuffer += heightDelta;
      this.m_isSleeping = false;
    }

    [PublicAPI]
    public bool CleanupAnimation(float deltaTime, float gravityVelocity)
    {
      float controlledLocalHeight = this.m_controlledLocalHeight;
      if (Mathf.Approximately(controlledLocalHeight, 0.0f))
        return false;
      float num = (this.m_controlledVelocity + controlledLocalHeight * gravityVelocity) * deltaTime;
      this.m_controlledHeightBuffer = (double) controlledLocalHeight > 0.0 ? Mathf.Max(0.0f, controlledLocalHeight + num) : Mathf.Min(0.0f, controlledLocalHeight + num);
      this.m_isSleeping = false;
      return true;
    }

    [PublicAPI]
    public void ResetAnimation()
    {
      this.m_controlledHeightBuffer = 0.0f;
      this.m_controlledVelocity = 0.0f;
      this.m_isSleeping = false;
    }

    public bool ResolvePhysics(float deltaTime, float gravityVelocity)
    {
      List<DynamicObject> rootDynamicChildren = this.m_rootDynamicChildren;
      int count = rootDynamicChildren.Count;
      if (this.m_isSleeping)
      {
        for (int index = 0; index < count; ++index)
          rootDynamicChildren[index].ResolvePhysics(deltaTime, gravityVelocity);
        return true;
      }
      float controlledHeightBuffer = this.m_controlledHeightBuffer;
      float parentDeltaHeight = controlledHeightBuffer - this.m_controlledLocalHeight;
      float parentVelocity = parentDeltaHeight / deltaTime;
      this.transform.localPosition = this.m_originalLocalPosition + this.m_localUpVector * controlledHeightBuffer;
      for (int index = 0; index < count; ++index)
        rootDynamicChildren[index].ResolvePhysics(deltaTime, parentDeltaHeight, parentVelocity, gravityVelocity);
      this.m_controlledVelocity = parentVelocity;
      this.m_controlledLocalHeight = controlledHeightBuffer;
      this.m_controlledHeightBuffer = 0.0f;
      this.m_isSleeping = true;
      return false;
    }

    public void CopyPhysics(
      CellObject referenceCell,
      bool isSleeping,
      float deltaTime,
      float gravityVelocity)
    {
      List<DynamicObject> rootDynamicChildren = this.m_rootDynamicChildren;
      int count = rootDynamicChildren.Count;
      float controlledLocalHeight = referenceCell.m_controlledLocalHeight;
      if (isSleeping && this.m_isSleeping)
      {
        for (int index = 0; index < count; ++index)
          rootDynamicChildren[index].ResolvePhysics(deltaTime, gravityVelocity);
        this.m_controlledVelocity = 0.0f;
      }
      else
      {
        float parentDeltaHeight = controlledLocalHeight - this.m_controlledLocalHeight;
        float parentVelocity = parentDeltaHeight / deltaTime;
        this.transform.localPosition = this.m_originalLocalPosition + this.m_localUpVector * controlledLocalHeight;
        for (int index = 0; index < count; ++index)
          rootDynamicChildren[index].ResolvePhysics(deltaTime, parentDeltaHeight, parentVelocity, gravityVelocity);
        this.m_controlledVelocity = parentVelocity;
      }
      this.m_controlledLocalHeight = controlledLocalHeight;
      this.m_controlledHeightBuffer = 0.0f;
      this.m_isSleeping = true;
    }

    public void AddRootDynamicObject([NotNull] DynamicObject dynamicObject) => this.m_rootDynamicChildren.Add(dynamicObject);

    public float AcquireDynamicObject([NotNull] DynamicObject dynamicObject, [NotNull] CellObject other)
    {
      this.m_rootDynamicChildren.Add(dynamicObject);
      return other.m_controlledLocalHeight - this.m_controlledLocalHeight;
    }

    public float AcquireRootDynamicObject([NotNull] DynamicObject dynamicObject, [NotNull] CellObject other)
    {
      this.m_rootDynamicChildren.Add(dynamicObject);
      other.m_rootDynamicChildren.Remove(dynamicObject);
      return other.m_controlledLocalHeight - this.m_controlledLocalHeight;
    }

    private void BuildDynamicObjectsHierarchy(Transform t)
    {
      int childCount = t.childCount;
      for (int index = 0; index < childCount; ++index)
      {
        Transform child = t.GetChild(index);
        DynamicObject component = child.GetComponent<DynamicObject>();
        if ((Object) null != (Object) component)
        {
          component.BuildHierarchy();
          this.m_rootDynamicChildren.Add(component);
        }
        else
          this.BuildDynamicObjectsHierarchy(child);
      }
    }
  }
}
