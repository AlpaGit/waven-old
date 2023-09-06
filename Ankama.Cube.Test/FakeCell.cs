// Decompiled with JetBrains decompiler
// Type: FakeCell
// Assembly: Ankama.Cube.Test, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CE3EDB01-1806-4494-9CF6-596877D58588
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.Test.dll

using UnityEngine;

public class FakeCell : MonoBehaviour
{
  [SerializeField]
  private Color highlightedColor;
  [SerializeField]
  private Color activeColor;
  [SerializeField]
  private Color highlightedActiveColor;
  public bool active;
  private Color inactiveColor = new Color(1f, 1f, 1f, 0.0f);
  private Color currentColor;
  private SpriteRenderer spriteRenderer;
  public bool highlighted;
  public Vector3 position;
  private float timer;

  private void Awake()
  {
    this.position = this.transform.position;
    this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    this.currentColor = !this.active ? this.inactiveColor : this.activeColor;
    this.spriteRenderer.color = this.currentColor;
    this.highlighted = false;
    this.timer = 0.0f;
  }

  private void Update()
  {
    if (this.highlighted)
    {
      if ((double) this.timer < 1.0)
      {
        this.timer += Time.deltaTime * 4f;
        this.UpdateColor();
      }
      else
        this.timer = 1f;
    }
    else if ((double) this.timer > 0.0)
    {
      this.timer -= Time.deltaTime * 4f;
      this.UpdateColor();
    }
    else
      this.timer = 0.0f;
  }

  private void UpdateColor()
  {
    this.currentColor = !this.active ? Color.Lerp(this.inactiveColor, this.highlightedColor, this.timer) : Color.Lerp(this.activeColor, this.highlightedActiveColor, this.timer);
    this.spriteRenderer.color = this.currentColor;
  }
}
