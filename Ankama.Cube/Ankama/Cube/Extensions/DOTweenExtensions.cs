// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.DOTweenExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;

namespace Ankama.Cube.Extensions
{
  public static class DOTweenExtensions
  {
    public static bool HasOvershootParam(this Ease ease) => ease == Ease.OutBack || ease == Ease.InBack || ease == Ease.InOutBack;

    public static bool HasAmplitudeParam(this Ease ease) => ease == Ease.InElastic || ease == Ease.OutElastic || ease == Ease.InOutElastic;
  }
}
