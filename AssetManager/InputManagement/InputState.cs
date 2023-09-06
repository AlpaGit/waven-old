// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.InputManagement.InputState
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;

namespace Ankama.AssetManagement.InputManagement
{
  [PublicAPI]
  public struct InputState
  {
    [PublicAPI]
    public readonly int id;
    [PublicAPI]
    public readonly InputState.State state;

    internal InputState(int id, InputState.State state)
    {
      this.id = id;
      this.state = state;
    }

    [PublicAPI]
    public enum State
    {
      [PublicAPI] None,
      [PublicAPI] Activated,
      [PublicAPI] Active,
      [PublicAPI] Repeated,
      [PublicAPI] Deactivated,
    }
  }
}
