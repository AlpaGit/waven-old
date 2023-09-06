// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.ZaapGodObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class ZaapGodObject : ZaapObject
  {
    [SerializeField]
    private Transform m_statuePosition;
    private GameObject m_statue;

    public void SetStatue(GameObject prefab)
    {
      if ((Object) this.m_statue != (Object) null)
      {
        Object.Destroy((Object) this.m_statue);
        this.m_statue = (GameObject) null;
      }
      if ((Object) prefab == (Object) null)
        return;
      this.m_statue = Object.Instantiate<GameObject>(prefab, this.m_statuePosition, false);
    }
  }
}
