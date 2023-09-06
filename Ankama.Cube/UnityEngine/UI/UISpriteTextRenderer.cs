// Decompiled with JetBrains decompiler
// Type: UnityEngine.UI.UISpriteTextRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace UnityEngine.UI
{
  [AddComponentMenu("UI/UI Sprite Text Renderer", 12)]
  public sealed class UISpriteTextRenderer : MaskableGraphic, ILayoutElement, ICanvasRaycastFilter
  {
    private static Material s_etc1DefaultUIMaterial;
    [SerializeField]
    private string m_text;
    [SerializeField]
    private SpriteFont m_font;
    [SerializeField]
    private SpriteFontAlignment m_alignment = SpriteFontAlignment.Center;
    [SerializeField]
    private Color m_color = Color.white;
    [SerializeField]
    private Color m_tint = new Color(1f, 1f, 1f, 1f);
    private float m_computedWidth;

    public string text
    {
      get => this.m_text;
      set
      {
        if (value.Equals(this.m_text))
          return;
        this.m_text = value;
        if (!((Object) null != (Object) this.m_font) || !this.m_font.isValid)
          return;
        this.SetVerticesDirty();
      }
    }

    public SpriteFont font
    {
      get => this.m_font;
      set
      {
        if (!((Object) value != (Object) this.m_font))
          return;
        if ((Object) null != (Object) this.m_font)
          this.m_font.Unload();
        this.m_font = value;
        if ((Object) null != (Object) value)
        {
          if (!value.Load())
            return;
          if (!string.IsNullOrEmpty(this.m_text))
            this.SetAllDirty();
          else
            this.SetMaterialDirty();
        }
        else if (!string.IsNullOrEmpty(this.m_text))
          this.SetAllDirty();
        else
          this.SetMaterialDirty();
      }
    }

    public SpriteFontAlignment alignment
    {
      get => this.m_alignment;
      set
      {
        if (value == this.m_alignment)
          return;
        this.m_alignment = value;
        if (string.IsNullOrEmpty(this.m_text) || !((Object) null != (Object) this.m_font) || !this.m_font.isValid)
          return;
        this.SetVerticesDirty();
        this.SetLayoutDirty();
      }
    }

    public override Color color
    {
      get => this.m_color;
      set
      {
        if (!(value != this.m_color))
          return;
        this.m_color = value;
        if (string.IsNullOrEmpty(this.m_text) || !((Object) null != (Object) this.m_font) || !this.m_font.isValid)
          return;
        this.SetVerticesDirty();
      }
    }

    public Color tint
    {
      get => this.m_tint;
      set
      {
        if (!(value != this.m_tint))
          return;
        this.m_tint = value;
        if (string.IsNullOrEmpty(this.m_text) || !((Object) null != (Object) this.m_font) || !this.m_font.isValid)
          return;
        this.SetVerticesDirty();
      }
    }

    public static Material defaultETC1GraphicMaterial
    {
      get
      {
        if ((Object) UISpriteTextRenderer.s_etc1DefaultUIMaterial == (Object) null)
          UISpriteTextRenderer.s_etc1DefaultUIMaterial = Canvas.GetETC1SupportedCanvasMaterial();
        return UISpriteTextRenderer.s_etc1DefaultUIMaterial;
      }
    }

    public override Texture mainTexture
    {
      get
      {
        if (!((Object) null == (Object) this.m_font))
          return (Texture) this.m_font.texture;
        return (Object) this.material != (Object) null && (Object) this.material.mainTexture != (Object) null ? this.material.mainTexture : (Texture) Graphic.s_WhiteTexture;
      }
    }

    public bool hasBorder => false;

    public float pixelsPerUnit
    {
      get
      {
        float num1 = 100f;
        if ((Object) null != (Object) this.m_font)
          num1 = this.m_font.pixelsPerUnit;
        float num2 = 100f;
        if ((Object) null != (Object) this.canvas)
          num2 = this.canvas.referencePixelsPerUnit;
        return num1 / num2;
      }
    }

    public override Material material
    {
      get
      {
        if ((Object) this.m_Material != (Object) null)
          return this.m_Material;
        return (Object) null != (Object) this.m_font && (Object) null != (Object) this.m_font.associatedAlphaSplitTexture ? UISpriteTextRenderer.defaultETC1GraphicMaterial : this.defaultMaterial;
      }
      set => base.material = value;
    }

    protected override void OnEnable()
    {
      if ((Object) null != (Object) this.m_font)
        this.m_font.Load();
      base.OnEnable();
    }

    protected override void OnDisable()
    {
      if ((Object) null != (Object) this.m_font)
        this.m_font.Unload();
      base.OnDisable();
    }

    protected override void UpdateGeometry()
    {
      CanvasRenderer canvasRenderer = this.canvasRenderer;
      Mesh workerMesh = Graphic.workerMesh;
      if ((Object) null == (Object) this.m_font || !this.m_font.isValid || string.IsNullOrEmpty(this.m_text))
      {
        workerMesh.Clear();
        this.m_computedWidth = 0.0f;
      }
      else
      {
        if ((Object) this.m_Material == (Object) null && this.m_tint != Color.white)
        {
          Color color = this.m_color * this.m_tint;
          this.m_font.BuildUIMesh(workerMesh, this.m_text, (Color32) color, this.m_alignment);
          this.m_font.ChangeMeshColor(workerMesh, color, (Vector4) Color.white);
        }
        else
        {
          this.m_font.BuildUIMesh(workerMesh, this.m_text, (Color32) this.m_color, this.m_alignment);
          this.m_font.ChangeMeshColor(workerMesh, this.m_color, (Vector4) this.m_tint);
        }
        this.m_computedWidth = this.m_font.lastComputedWidth;
      }
      Mesh mesh = workerMesh;
      canvasRenderer.SetMesh(mesh);
    }

    protected override void UpdateMaterial()
    {
      base.UpdateMaterial();
      if ((Object) null == (Object) this.m_font)
      {
        this.canvasRenderer.SetAlphaTexture((Texture) null);
      }
      else
      {
        Texture2D alphaSplitTexture = this.m_font.associatedAlphaSplitTexture;
        if (!((Object) null != (Object) alphaSplitTexture))
          return;
        this.canvasRenderer.SetAlphaTexture((Texture) alphaSplitTexture);
      }
    }

    public void CalculateLayoutInputHorizontal()
    {
    }

    public void CalculateLayoutInputVertical()
    {
    }

    public float minWidth => 0.0f;

    public float preferredWidth => (Object) null == (Object) this.m_font ? 0.0f : this.m_computedWidth;

    public float flexibleWidth => -1f;

    public float minHeight => 0.0f;

    public float preferredHeight => (Object) null == (Object) this.m_font ? 0.0f : this.m_font.height / this.pixelsPerUnit;

    public float flexibleHeight => -1f;

    public int layoutPriority => 0;

    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera) => true;
  }
}
