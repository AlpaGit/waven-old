// Decompiled with JetBrains decompiler
// Type: UnityEngine.SpriteTextRenderer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace UnityEngine
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (MeshRenderer), typeof (MeshFilter))]
  public sealed class SpriteTextRenderer : MonoBehaviour
  {
    private static readonly int s_mainTextPropertyID = Shader.PropertyToID("_MainTex");
    [SerializeField]
    [HideInInspector]
    private MeshRenderer m_meshRenderer;
    [SerializeField]
    [HideInInspector]
    private MeshFilter m_meshFilter;
    [SerializeField]
    [HideInInspector]
    private Material m_material;
    [SerializeField]
    private string m_text;
    [SerializeField]
    private SpriteFont m_font;
    [SerializeField]
    private SpriteFontAlignment m_alignment = SpriteFontAlignment.Center;
    [SerializeField]
    private Color m_color = Color.white;
    [SerializeField]
    private Color m_tint = new Color(1f, 1f, 1f, 0.0f);
    private MaterialPropertyBlock m_matPropBlock;

    public string text
    {
      get => this.m_text;
      set
      {
        if (!(value != this.m_text))
          return;
        this.m_text = value;
        if (!string.IsNullOrEmpty(value))
        {
          if (!((Object) null != (Object) this.m_font) || !this.m_font.isValid || !this.m_font.isLoaded)
            return;
          this.Rebuild();
        }
        else
          this.Clear();
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
          this.SetTexture();
          if (string.IsNullOrEmpty(this.m_text))
            return;
          this.Rebuild();
        }
        else
        {
          this.Clear();
          this.ClearTexture();
        }
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
        if (string.IsNullOrEmpty(this.m_text) || !((Object) null != (Object) this.m_font) || !this.m_font.isValid || !this.m_font.isLoaded)
          return;
        this.Rebuild();
      }
    }

    public Color color
    {
      get => this.m_color;
      set
      {
        if (!(value != this.m_color))
          return;
        this.m_color = value;
        if (string.IsNullOrEmpty(this.m_text) || !((Object) null != (Object) this.m_font) || !this.m_font.isValid)
          return;
        this.ApplyColorAndTint();
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
        this.ApplyColorAndTint();
      }
    }

    public int sortingLayerID
    {
      get => this.m_meshRenderer.sortingLayerID;
      set => this.m_meshRenderer.sortingLayerID = value;
    }

    public string sortingLayerName
    {
      get => this.m_meshRenderer.sortingLayerName;
      set => this.m_meshRenderer.sortingLayerName = value;
    }

    public int sortingOrder
    {
      get => this.m_meshRenderer.sortingOrder;
      set => this.m_meshRenderer.sortingOrder = value;
    }

    public Bounds bounds => this.m_meshRenderer.bounds;

    public Bounds textBounds
    {
      get
      {
        Mesh sharedMesh = this.m_meshFilter.sharedMesh;
        return !((Object) null == (Object) sharedMesh) ? sharedMesh.bounds : new Bounds();
      }
    }

    public void Set(string textValue, Color colorValue)
    {
      if (textValue != this.m_text)
      {
        this.m_text = textValue;
        this.m_color = colorValue;
        if (!string.IsNullOrEmpty(textValue))
        {
          if (!((Object) null != (Object) this.m_font) || !this.m_font.isValid || !this.m_font.isLoaded)
            return;
          this.Rebuild();
        }
        else
          this.Clear();
      }
      else
        this.color = colorValue;
    }

    private void Rebuild() => this.m_meshFilter.sharedMesh = this.m_font.BuildMesh(this.m_text, (Color32) this.m_color, (Vector4) this.m_tint, this.m_alignment);

    private void Clear() => this.m_meshFilter.sharedMesh = (Mesh) null;

    private void ApplyColorAndTint() => this.m_font.ChangeMeshColor(this.m_meshFilter.sharedMesh, this.m_color, (Vector4) this.m_tint);

    private void SetTexture()
    {
      this.m_meshRenderer.GetPropertyBlock(this.m_matPropBlock);
      this.m_matPropBlock.SetTexture(SpriteTextRenderer.s_mainTextPropertyID, (Texture) this.m_font.texture);
      this.m_meshRenderer.SetPropertyBlock(this.m_matPropBlock);
    }

    private void ClearTexture()
    {
      this.m_meshRenderer.GetPropertyBlock(this.m_matPropBlock);
      this.m_matPropBlock.SetTexture(SpriteTextRenderer.s_mainTextPropertyID, (Texture) Texture2D.whiteTexture);
      this.m_meshRenderer.SetPropertyBlock(this.m_matPropBlock);
    }

    private void Awake()
    {
      this.m_matPropBlock = new MaterialPropertyBlock();
      if ((Object) null != (Object) this.m_font && this.m_font.Load())
      {
        this.SetTexture();
        if (!string.IsNullOrEmpty(this.m_text))
          this.Rebuild();
        else
          this.Clear();
      }
      this.m_meshRenderer.enabled = this.enabled;
    }

    private void OnEnable() => this.m_meshRenderer.enabled = true;

    private void OnDisable() => this.m_meshRenderer.enabled = false;

    private void OnDestroy()
    {
      if ((Object) null != (Object) this.m_font)
      {
        this.ClearTexture();
        this.m_font.Unload();
      }
      this.m_matPropBlock = (MaterialPropertyBlock) null;
    }

    private void OnDidApplyAnimationProperties()
    {
      if (!((Object) null != (Object) this.m_font) || !this.m_font.isValid || string.IsNullOrEmpty(this.m_text))
        return;
      this.ApplyColorAndTint();
    }
  }
}
