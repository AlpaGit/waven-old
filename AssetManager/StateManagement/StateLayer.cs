// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StateManagement.StateLayer
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;

namespace Ankama.AssetManagement.StateManagement
{
  [PublicAPI]
  public sealed class StateLayer : StateContext
  {
    [PublicAPI]
    public readonly string name;

    [PublicAPI]
    public int index { get; internal set; }

    internal StateLayer(string name, int index)
    {
      this.name = name;
      this.index = index;
      this.loadState = StateLoadState.Updating;
    }
  }
}
