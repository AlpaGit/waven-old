// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Tools.DisplayMeshFilterNormals
// Assembly: Ankama.Cube.Tools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E5F9BDA-0991-43A6-B1CC-DE1630412C37
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Ankama.Cube.Tools.dll

using UnityEngine;

namespace Ankama.Cube.Tools
{
  [ExecuteInEditMode]
  public class DisplayMeshFilterNormals : MonoBehaviour
  {
    [SerializeField]
    private float m_size = 0.1f;
    [SerializeField]
    private Color m_color = Color.red;
    private MeshFilter m_meshFilter;

    private void OnDrawGizmos()
    {
      if ((Object) this.m_meshFilter == (Object) null)
        this.m_meshFilter = this.GetComponent<MeshFilter>();
      if ((Object) this.m_meshFilter == (Object) null)
        return;
      Mesh sharedMesh = this.m_meshFilter.sharedMesh;
      Vector3[] vertices = sharedMesh.vertices;
      Vector3[] normals = sharedMesh.normals;
      Gizmos.color = this.m_color;
      for (int index = 0; index < vertices.Length; ++index)
      {
        Vector3 from = this.transform.TransformPoint(vertices[index]);
        Gizmos.DrawLine(from, from + this.transform.TransformDirection(normals[index]) * this.m_size);
      }
    }
  }
}
