// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LayerMaskNames
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public static class LayerMaskNames
  {
    public const string Default = "Default";
    public const string Clouds = "CloudsShadow";
    public const string Water = "Water";
    public const string CharacterFocus = "CharacterFocus";
    public static int waterMask = LayerMask.GetMask(nameof (Water));
    public static int cloudsMask = LayerMask.GetMask("CloudsShadow");
    public static int reflectionMask = LayerMask.GetMask(nameof (Default));
    public static int characterFocusMask = LayerMask.GetMask(nameof (CharacterFocus));
    public static int defaultLayer = LayerMask.NameToLayer(nameof (Default));
    public static int waterLayer = LayerMask.NameToLayer(nameof (Water));
    public static int characterFocusLayer = LayerMask.NameToLayer(nameof (CharacterFocus));
    public static uint reflectionRenderMask = 1;
    public static uint doNotRenderInReflectionRenderMask = 2;
    public static uint everyThingRenderMask = uint.MaxValue;
  }
}
