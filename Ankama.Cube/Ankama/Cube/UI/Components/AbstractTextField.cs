// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.AbstractTextField
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ankama.Cube.UI.Components
{
  public abstract class AbstractTextField : UIBehaviour
  {
    [UsedImplicitly]
    [SerializeField]
    private string m_fontCollectionIdentifier = string.Empty;
    [UsedImplicitly]
    [SerializeField]
    private AbstractTextField.FontStyle m_fontStyle;
    [UsedImplicitly]
    [SerializeField]
    private AbstractTextField.TextStyle m_textStyle;
    [UsedImplicitly]
    [SerializeField]
    private Color m_color = Color.white;
    [UsedImplicitly]
    [SerializeField]
    private TextAlignmentOptions m_textAlignment = TextAlignmentOptions.TopLeft;
    [UsedImplicitly]
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_textAlignmentWrapMix = 0.4f;
    [UsedImplicitly]
    [SerializeField]
    private bool m_enableWordWrapping = true;
    [UsedImplicitly]
    [SerializeField]
    private AbstractTextField.OverflowMode m_overflowMode;
    [UsedImplicitly]
    [SerializeField]
    private bool m_richText;
    private bool m_textDirty = true;
    [NonSerialized]
    private FontCollection m_fontCollection;
    [NonSerialized]
    private TextMeshProUGUICustom m_textMeshProComponent;

    [PublicAPI]
    public FontCollection fontCollection
    {
      get => this.m_fontCollection;
      set
      {
        if ((UnityEngine.Object) value == (UnityEngine.Object) this.m_fontCollection)
          return;
        if (this.IsActive())
        {
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontCollection)
            this.m_fontCollection.UnregisterTextField(this);
          if ((UnityEngine.Object) null != (UnityEngine.Object) value)
            value.RegisterTextField(this);
        }
        this.m_fontCollection = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.ApplyFontCollection();
      }
    }

    [PublicAPI]
    public AbstractTextField.FontStyle fontStyle
    {
      get => this.m_fontStyle;
      set
      {
        if (value == this.m_fontStyle)
          return;
        this.m_fontStyle = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.fontStyle = (FontStyles) (this.m_fontStyle | (AbstractTextField.FontStyle) this.m_textStyle);
      }
    }

    [PublicAPI]
    public AbstractTextField.TextStyle textStyle
    {
      get => this.m_textStyle;
      set
      {
        if (value == this.m_textStyle)
          return;
        this.m_textStyle = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.fontStyle = (FontStyles) (this.m_fontStyle | (AbstractTextField.FontStyle) this.m_textStyle);
      }
    }

    [PublicAPI]
    public Color color
    {
      get => this.m_color;
      set
      {
        if (value == this.m_color)
          return;
        this.m_color = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.color = value;
      }
    }

    [PublicAPI]
    public Material material => (UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent ? this.m_textMeshProComponent.fontMaterial : (Material) null;

    [PublicAPI]
    public TextAlignmentOptions alignment
    {
      get => this.m_textAlignment;
      set
      {
        if (value == this.m_textAlignment)
          return;
        this.m_textAlignment = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.alignment = value;
      }
    }

    [PublicAPI]
    public float alignmentWrapMix
    {
      get => this.m_textAlignmentWrapMix;
      set
      {
        value = Mathf.Clamp01(value);
        this.m_textAlignmentWrapMix = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.wordWrappingRatios = value;
      }
    }

    [PublicAPI]
    public bool enableWordWrapping
    {
      get => this.m_enableWordWrapping;
      set
      {
        if (value == this.m_enableWordWrapping)
          return;
        this.m_enableWordWrapping = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.enableWordWrapping = value;
      }
    }

    [PublicAPI]
    public AbstractTextField.OverflowMode overflowMode
    {
      get => this.m_overflowMode;
      set
      {
        if (value == this.m_overflowMode)
          return;
        this.m_overflowMode = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.overflowMode = (TextOverflowModes) this.m_overflowMode;
      }
    }

    [PublicAPI]
    public bool richText
    {
      get => this.m_richText;
      set
      {
        if (value == this.m_richText)
          return;
        this.m_richText = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent))
          return;
        this.m_textMeshProComponent.richText = value;
      }
    }

    [PublicAPI]
    public event AbstractTextField.TextComponentCreatedEventHandler TextComponentCreated;

    [PublicAPI]
    [NotNull]
    public string GetText() => this.m_textDirty || !((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent) ? this.GetFormattedText() : this.m_textMeshProComponent.text;

    [CanBeNull]
    public TMP_Text GetTextComponent() => (TMP_Text) this.m_textMeshProComponent;

    protected abstract string GetFormattedText();

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_fontCollection)
        this.GetFontCollection();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_textMeshProComponent)
        this.CreateTextMeshProComponent();
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontCollection))
        return;
      this.m_fontCollection.RegisterTextField(this);
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_textMeshProComponent)
        return;
      this.m_textMeshProComponent.enabled = true;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontCollection)
        this.m_fontCollection.RegisterTextField(this);
      if (!this.m_textDirty)
        return;
      this.RefreshText();
    }

    protected override void OnDisable()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontCollection)
        this.m_fontCollection.UnregisterTextField(this);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent)
        this.m_textMeshProComponent.enabled = false;
      base.OnDisable();
    }

    protected override void OnDestroy()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_textMeshProComponent)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_textMeshProComponent);
        this.m_textMeshProComponent = (TextMeshProUGUICustom) null;
      }
      if ((UnityEngine.Object) this.m_fontCollection != (UnityEngine.Object) null)
        this.m_fontCollection.Unload();
      base.OnDestroy();
    }

    private void GetFontCollection()
    {
      if (this.m_fontCollectionIdentifier.Length == 0)
        this.m_fontCollection = (FontCollection) null;
      else if (!RuntimeData.TryGetFontCollection(this.m_fontCollectionIdentifier, out this.m_fontCollection))
        Log.Warning("Could not find font collection for TextField named '" + this.name + "'.", 483, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Components\\AbstractTextField.cs");
      else
        this.m_fontCollection.Load();
    }

    private void CreateTextMeshProComponent()
    {
      TextMeshProUGUICustom textComponent = this.GetComponent<TextMeshProUGUICustom>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) textComponent)
        textComponent = this.gameObject.AddComponent<TextMeshProUGUICustom>();
      else
        textComponent.enabled = true;
      this.m_textMeshProComponent = textComponent;
      this.ApplyFontCollection();
      textComponent.fontStyle = (FontStyles) (this.m_fontStyle | (AbstractTextField.FontStyle) this.m_textStyle);
      textComponent.alignment = this.m_textAlignment;
      textComponent.color = this.m_color;
      textComponent.wordWrappingRatios = this.m_textAlignmentWrapMix;
      textComponent.enableWordWrapping = this.m_enableWordWrapping;
      textComponent.overflowMode = (TextOverflowModes) this.m_overflowMode;
      textComponent.richText = this.m_richText;
      textComponent.enableKerning = true;
      textComponent.extraPadding = true;
      textComponent.raycastTarget = false;
      if (this.m_textDirty)
      {
        textComponent.text = this.GetFormattedText();
        this.m_textDirty = false;
      }
      AbstractTextField.TextComponentCreatedEventHandler componentCreated = this.TextComponentCreated;
      if (componentCreated == null)
        return;
      componentCreated((TMP_Text) textComponent);
    }

    private void ApplyFontCollection()
    {
      TextMeshProUGUICustom meshProComponent = this.m_textMeshProComponent;
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_fontCollection)
      {
        meshProComponent.font = TMP_FontAsset.defaultFontAsset;
      }
      else
      {
        meshProComponent.font = this.m_fontCollection.fontAsset;
        if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontCollection.styleMaterial)
          meshProComponent.fontSharedMaterial = this.m_fontCollection.styleMaterial;
        else
          meshProComponent.fontSharedMaterial = meshProComponent.font.material;
        FontData fontData = this.m_fontCollection.fontData;
        if (fontData == null)
          return;
        meshProComponent.fontSize = fontData.fontSize;
        meshProComponent.characterSpacing = fontData.characterSpacing;
        meshProComponent.wordSpacing = fontData.wordSpacing;
        meshProComponent.lineSpacing = fontData.lineSpacing;
        meshProComponent.paragraphSpacing = fontData.paragraphSpacing;
      }
    }

    public void RefreshText()
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_textMeshProComponent)
      {
        this.m_textDirty = true;
      }
      else
      {
        this.m_textMeshProComponent.text = this.GetFormattedText();
        this.m_textDirty = false;
      }
    }

    [Flags]
    public enum FontStyle
    {
      [UsedImplicitly] Normal = 0,
      [UsedImplicitly] Bold = 1,
      [UsedImplicitly] Italic = 2,
      [UsedImplicitly] Underline = 4,
      [UsedImplicitly] Strikethrough = 64, // 0x00000040
    }

    public enum TextStyle
    {
      [UsedImplicitly] Normal = 0,
      [UsedImplicitly] LowerCase = 8,
      [UsedImplicitly] UpperCase = 16, // 0x00000010
      [UsedImplicitly] SmallCaps = 32, // 0x00000020
    }

    public enum OverflowMode
    {
      [UsedImplicitly] Overflow,
      [UsedImplicitly] Ellipsis,
      [UsedImplicitly] Masking,
      [UsedImplicitly] Truncate,
    }

    public delegate void TextComponentCreatedEventHandler(TMP_Text textComponent);
  }
}
