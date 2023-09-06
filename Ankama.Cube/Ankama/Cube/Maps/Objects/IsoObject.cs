// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.IsoObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public abstract class IsoObject : MonoBehaviour
  {
    protected CellObject m_cellObject;
    protected Area m_area;
    protected Vector3 m_localRightVector;
    protected Vector3 m_localForwardVector;
    protected Vector3 m_localUpVector;

    public abstract IsoObjectDefinition definition { get; protected set; }

    public CellObject cellObject => this.m_cellObject;

    public Area area => this.m_area;

    protected virtual void OnEnable()
    {
      Transform parent = this.transform.parent;
      if ((Object) null == (Object) parent)
      {
        this.m_localRightVector = Vector3.right;
        this.m_localForwardVector = Vector3.forward;
        this.m_localUpVector = Vector3.up;
      }
      else
      {
        this.m_localRightVector = parent.InverseTransformDirection(Vector3.right);
        this.m_localForwardVector = parent.InverseTransformDirection(Vector3.forward);
        this.m_localUpVector = parent.InverseTransformDirection(Vector3.up);
      }
    }

    public virtual void InitializeDefinitionAndArea(
      [NotNull] IsoObjectDefinition isoObjectDefinition,
      int x,
      int y)
    {
      this.definition = isoObjectDefinition;
      if (isoObjectDefinition.areaDefinition == null)
        return;
      Vector2Int position = new Vector2Int(x, y);
      this.m_area = isoObjectDefinition.areaDefinition.ToArea(position);
    }

    public virtual void AttachToCell([NotNull] CellObject containerCell) => this.m_cellObject = containerCell;

    public void DetachFromCell()
    {
      if (!((Object) null != (Object) this.m_cellObject))
        return;
      this.m_cellObject.RemoveIsoObject(this);
      this.m_cellObject = (CellObject) null;
    }

    public virtual void Destroy() => Object.Destroy((Object) this.gameObject);
  }
}
