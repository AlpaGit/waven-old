// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.SpinLoadingAnim
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.UI
{
  public class SpinLoadingAnim : MonoBehaviour
  {
    [SerializeField]
    private SpinLoadingAnimData m_datas;
    private float m_time;

    private void Update()
    {
      if ((Object) this.m_datas == (Object) null)
        return;
      this.m_time += Time.deltaTime * this.m_datas.speed;
      this.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, (float) ((this.m_datas.offsetType == SpinLoadingAnimData.OffsetType.Value ? (double) this.m_datas.offset : (double) this.transform.position.y + (double) this.transform.position.x) + (double) Mathf.FloorToInt(this.m_time / this.m_datas.step) * (double) this.m_datas.step));
    }
  }
}
