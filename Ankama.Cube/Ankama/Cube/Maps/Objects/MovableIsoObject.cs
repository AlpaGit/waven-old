// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.MovableIsoObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class MovableIsoObject : IsoObject
  {
    public Vector2 innerCellPosition { get; private set; }

    public override void AttachToCell(CellObject containerCell)
    {
      base.AttachToCell(containerCell);
      Vector3 localPosition = this.transform.localPosition;
      this.innerCellPosition = new Vector2(Vector3.Dot(this.m_localRightVector, localPosition), Vector3.Dot(this.m_localForwardVector, localPosition));
    }

    public virtual void SetCellObject([NotNull] CellObject containerCell)
    {
      if ((Object) containerCell == (Object) this.m_cellObject)
        return;
      float a = Vector3.Dot(this.m_localUpVector, this.transform.localPosition);
      Transform transform = containerCell.transform;
      this.m_localRightVector = transform.InverseTransformDirection(Vector3.right);
      this.m_localForwardVector = transform.InverseTransformDirection(Vector3.forward);
      this.m_localUpVector = transform.InverseTransformDirection(Vector3.up);
      this.transform.SetParent(transform, true);
      Vector3 localPosition = this.transform.localPosition;
      this.innerCellPosition = new Vector2(Vector3.Dot(this.m_localRightVector, localPosition), Vector3.Dot(this.m_localForwardVector, localPosition));
      if ((Object) null == (Object) this.m_cellObject)
      {
        containerCell.AcquireIsoObject((IsoObject) this);
      }
      else
      {
        float b = Vector3.Dot(this.m_localUpVector, localPosition);
        if (!Mathf.Approximately(a, b))
          this.transform.localPosition = localPosition + this.m_localUpVector * (a - b);
        containerCell.AcquireIsoObject((IsoObject) this, this.m_cellObject);
      }
      this.m_cellObject = containerCell;
    }

    public void SetCellObjectInnerPosition(Vector2 innerPosition)
    {
      if (!(this.innerCellPosition != innerPosition))
        return;
      this.transform.localPosition = this.transform.localPosition + this.m_localRightVector * (innerPosition.x - this.innerCellPosition.x) + this.m_localForwardVector * (innerPosition.y - this.innerCellPosition.y);
      this.innerCellPosition = innerPosition;
    }
  }
}
