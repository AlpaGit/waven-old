// Decompiled with JetBrains decompiler
// Type: SafeAreaScaler
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

public class SafeAreaScaler : MonoBehaviour
{
  public RectTransform canvas;
  public RectTransform panel;
  [Header("Anchors")]
  [Range(0.0f, 1f)]
  public float TopPercent = 1f;
  [Range(0.0f, 1f)]
  public float BottomPercent = 1f;
  [Range(0.0f, 1f)]
  public float LeftPercent = 1f;
  [Range(0.0f, 1f)]
  public float RightPercent = 1f;
  private Rect lastSafeArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);

  private Rect GetSafeArea()
  {
    Rect safeArea = Screen.safeArea;
    double x = (double) safeArea.x * (double) this.LeftPercent;
    float num1 = (float) ((double) Screen.width - ((double) Screen.width - ((double) safeArea.width + (double) safeArea.x)) * (double) this.RightPercent - (double) safeArea.x * (double) this.LeftPercent);
    float num2 = safeArea.y * this.BottomPercent;
    float num3 = (float) ((double) Screen.height - ((double) Screen.height - ((double) safeArea.height + (double) safeArea.y)) * (double) this.TopPercent - (double) safeArea.y * (double) this.BottomPercent);
    double y = (double) num2;
    double width = (double) num1;
    double height = (double) num3;
    return new Rect((float) x, (float) y, (float) width, (float) height);
  }

  private void ApplySafeArea(Rect area)
  {
    Vector2 position = area.position;
    Vector2 vector2 = area.position + area.size;
    position.x /= (float) Screen.width;
    position.y /= (float) Screen.height;
    vector2.x /= (float) Screen.width;
    vector2.y /= (float) Screen.height;
    this.panel.anchorMin = position;
    this.panel.anchorMax = vector2;
    this.lastSafeArea = area;
  }

  private void Update()
  {
    Rect safeArea = this.GetSafeArea();
    if (!(safeArea != this.lastSafeArea))
      return;
    this.ApplySafeArea(safeArea);
  }
}
