// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.PointLoadingAnim
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using UnityEngine;

namespace Ankama.Cube.UI
{
  public class PointLoadingAnim : MonoBehaviour
  {
    [SerializeField]
    private PointLoadingAnimData m_datas;
    [SerializeField]
    private CanvasGroup[] m_points;
    private Sequence m_loopSequence;

    private void OnEnable()
    {
      if ((Object) this.m_datas == (Object) null)
        return;
      this.m_loopSequence = DOTween.Sequence();
      this.m_loopSequence.SetLoops<Sequence>(-1);
      for (int index = 0; index < this.m_points.Length; ++index)
      {
        CanvasGroup point = this.m_points[index];
        this.m_loopSequence.Insert((float) index * this.m_datas.delayBetweenPoints, (Tween) point.transform.DOPunchScale(Vector3.one * this.m_datas.scale, this.m_datas.duration, this.m_datas.vibrato, this.m_datas.elasticity));
      }
      this.m_loopSequence.AppendInterval(this.m_datas.delayBetweenLoops);
    }

    private void OnDisable()
    {
      this.m_loopSequence.Kill();
      this.m_loopSequence = (Sequence) null;
    }
  }
}
