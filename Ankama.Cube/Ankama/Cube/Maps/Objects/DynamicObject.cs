// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.DynamicObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  [UsedImplicitly]
  public sealed class DynamicObject : MonoBehaviour
  {
    private const float MaxFreeFallVelocity = 4.905f;
    private const float BounceVelocityCutoff = -1.22625f;
    [UsedImplicitly]
    [Range(0.0f, 1f)]
    [SerializeField]
    private float m_bounceFactor = 0.5f;
    private DynamicObject m_parent;
    private readonly List<DynamicObject> m_children = new List<DynamicObject>();
    private Vector3 m_localUpVector;
    private float m_inheritedVelocity;
    private float m_localHeight;

    [UsedImplicitly]
    private void Awake() => this.m_localUpVector = this.transform.parent.InverseTransformDirection(Vector3.up);

    public void BuildHierarchy()
    {
      Transform transform = this.transform;
      int childCount = transform.childCount;
      for (int index = 0; index < childCount; ++index)
      {
        DynamicObject component = transform.GetChild(index).GetComponent<DynamicObject>();
        if ((Object) null != (Object) component)
        {
          component.m_parent = this;
          component.BuildHierarchy();
          this.m_children.Add(component);
        }
      }
    }

    public void SetCellObject([CanBeNull] CellObject previousCell, [NotNull] CellObject containerCell)
    {
      DynamicObject parent = this.m_parent;
      if ((Object) null == (Object) parent)
      {
        if ((Object) null == (Object) previousCell)
          containerCell.AddRootDynamicObject(this);
        else
          this.m_localHeight += containerCell.AcquireRootDynamicObject(this, previousCell);
      }
      else
      {
        parent.m_children.Remove(this);
        if ((Object) null == (Object) previousCell)
          containerCell.AddRootDynamicObject(this);
        else
          this.m_localHeight = containerCell.AcquireDynamicObject(this, previousCell) + (this.transform.position.y - (previousCell.transform.position.y + 0.5f));
      }
      this.m_localUpVector = this.transform.parent.InverseTransformDirection(Vector3.up);
      if ((double) this.m_localHeight > 0.0)
        return;
      Vector3 localPosition = this.transform.localPosition;
      this.transform.localPosition = localPosition + this.m_localUpVector * (0.5f - Vector3.Dot(localPosition, this.m_localUpVector));
      this.m_localHeight = 0.0f;
      this.m_inheritedVelocity = 0.0f;
    }

    public void ResolvePhysics(float deltaTime, float gravityVelocity)
    {
      if ((double) this.m_localHeight > 0.0 || (double) this.m_inheritedVelocity > 0.0)
      {
        float parentVelocity = this.m_inheritedVelocity + gravityVelocity;
        float parentDeltaHeight = parentVelocity * deltaTime;
        float num1 = this.m_localHeight + parentDeltaHeight;
        if ((double) num1 <= 0.0)
        {
          if ((double) parentVelocity > -1.2262500524520874 || (double) this.m_bounceFactor <= 0.0)
          {
            this.transform.localPosition = this.transform.localPosition - this.m_localUpVector * this.m_localHeight;
            this.m_localHeight = 0.0f;
            this.m_inheritedVelocity = 0.0f;
          }
          else
          {
            float num2 = -num1 * this.m_bounceFactor;
            parentVelocity = -parentVelocity * this.m_bounceFactor;
            parentDeltaHeight = num2 - this.m_localHeight;
            this.transform.localPosition = this.transform.localPosition + this.m_localUpVector * parentDeltaHeight;
            this.m_localHeight = num2;
            this.m_inheritedVelocity = parentVelocity;
          }
        }
        else
        {
          this.transform.localPosition = this.transform.localPosition + this.m_localUpVector * parentDeltaHeight;
          this.m_localHeight = num1;
          this.m_inheritedVelocity = parentVelocity;
        }
        int index = 0;
        for (int count = this.m_children.Count; index < count; ++index)
          this.m_children[index].ResolvePhysics(deltaTime, parentDeltaHeight, parentVelocity, gravityVelocity);
      }
      else
      {
        int index = 0;
        for (int count = this.m_children.Count; index < count; ++index)
          this.m_children[index].ResolvePhysics(deltaTime, gravityVelocity);
      }
    }

    public void ResolvePhysics(
      float deltaTime,
      float parentDeltaHeight,
      float parentVelocity,
      float gravityVelocity)
    {
      float num1;
      float parentDeltaHeight1;
      if ((double) this.m_localHeight <= 0.0)
      {
        if ((double) this.m_inheritedVelocity > (double) parentVelocity)
        {
          num1 = this.m_inheritedVelocity;
          parentDeltaHeight1 = num1 * deltaTime;
          float num2 = parentDeltaHeight1 - parentDeltaHeight;
          this.transform.localPosition = this.transform.localPosition + this.m_localUpVector * (num2 - this.m_localHeight);
          this.m_localHeight = num2;
        }
        else
        {
          num1 = parentVelocity;
          parentDeltaHeight1 = parentDeltaHeight;
          this.m_inheritedVelocity = Mathf.Max(gravityVelocity, Mathf.Min(4.905f, num1));
        }
      }
      else
      {
        num1 = this.m_inheritedVelocity + gravityVelocity;
        parentDeltaHeight1 = num1 * deltaTime;
        float num3 = this.m_localHeight + (parentDeltaHeight1 - parentDeltaHeight);
        if ((double) num3 <= 0.0)
        {
          if ((double) num1 > -1.2262500524520874 || (double) this.m_bounceFactor <= 0.0)
          {
            this.transform.localPosition = this.transform.localPosition - this.m_localUpVector * this.m_localHeight;
            this.m_localHeight = 0.0f;
            this.m_inheritedVelocity = Mathf.Max(gravityVelocity, Mathf.Min(4.905f, parentVelocity));
          }
          else
          {
            float num4 = -num3 * this.m_bounceFactor;
            num1 = -num1 * this.m_bounceFactor;
            parentDeltaHeight1 = num4 - this.m_localHeight;
            this.transform.localPosition = this.transform.localPosition + this.m_localUpVector * parentDeltaHeight1;
            this.m_localHeight = num4;
            this.m_inheritedVelocity = num1;
          }
        }
        else
        {
          this.transform.localPosition = this.transform.localPosition + this.m_localUpVector * (num3 - this.m_localHeight);
          this.m_localHeight = num3;
          this.m_inheritedVelocity = num1;
        }
      }
      int index = 0;
      for (int count = this.m_children.Count; index < count; ++index)
        this.m_children[index].ResolvePhysics(deltaTime, parentDeltaHeight1, num1, gravityVelocity);
    }
  }
}
