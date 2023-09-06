// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.InputManagement.InputDefinition
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using JetBrains.Annotations;

namespace Ankama.AssetManagement.InputManagement
{
  [PublicAPI]
  public abstract class InputDefinition
  {
    public readonly int id;
    public readonly int priority;

    protected InputDefinition(int id, int priority = 0)
    {
      this.id = id;
      this.priority = priority;
    }

    public abstract InputState.State GetInputState();
  }
}
