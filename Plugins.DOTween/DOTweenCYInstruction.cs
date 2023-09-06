// Decompiled with JetBrains decompiler
// Type: DG.Tweening.DOTweenCYInstruction
// Assembly: Plugins.DOTween, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9FF25450-B39C-42C8-B3DB-BB3A40E2DA5A
// Assembly location: E:\WAVEN\Waven_Data\Managed\Plugins.DOTween.dll

using UnityEngine;

namespace DG.Tweening
{
  public static class DOTweenCYInstruction
  {
    public class WaitForCompletion : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => this.t.active && !this.t.IsComplete();

      public WaitForCompletion(Tween tween) => this.t = tween;
    }

    public class WaitForRewind : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting
      {
        get
        {
          if (!this.t.active)
            return false;
          return !this.t.playedOnce || (double) this.t.position * (double) (this.t.CompletedLoops() + 1) > 0.0;
        }
      }

      public WaitForRewind(Tween tween) => this.t = tween;
    }

    public class WaitForKill : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => this.t.active;

      public WaitForKill(Tween tween) => this.t = tween;
    }

    public class WaitForElapsedLoops : CustomYieldInstruction
    {
      private readonly Tween t;
      private readonly int elapsedLoops;

      public override bool keepWaiting => this.t.active && this.t.CompletedLoops() < this.elapsedLoops;

      public WaitForElapsedLoops(Tween tween, int elapsedLoops)
      {
        this.t = tween;
        this.elapsedLoops = elapsedLoops;
      }
    }

    public class WaitForPosition : CustomYieldInstruction
    {
      private readonly Tween t;
      private readonly float position;

      public override bool keepWaiting => this.t.active && (double) this.t.position * (double) (this.t.CompletedLoops() + 1) < (double) this.position;

      public WaitForPosition(Tween tween, float position)
      {
        this.t = tween;
        this.position = position;
      }
    }

    public class WaitForStart : CustomYieldInstruction
    {
      private readonly Tween t;

      public override bool keepWaiting => this.t.active && !this.t.playedOnce;

      public WaitForStart(Tween tween) => this.t = tween;
    }
  }
}
