// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.Extensions.NonDrawingGraphic
// Assembly: Plugins.UnityUIExtensions, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4A06936C-BE86-4636-9C8E-0D1E3F2B036B
// Assembly location: F:\WAVEN-old\Waven_Data\Managed\Plugins.UnityUIExtensions.dll

namespace UnityEngine.UI.Extensions
{
  [AddComponentMenu("Layout/Extensions/NonDrawingGraphic")]
  public class NonDrawingGraphic : MaskableGraphic
  {
    public override void SetMaterialDirty()
    {
    }

    public override void SetVerticesDirty()
    {
    }

    protected override void OnPopulateMesh(VertexHelper vh) => vh.Clear();
  }
}
