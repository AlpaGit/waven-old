// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.InputTextField
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data.UI.Localization.TextFormatting;
using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [ExecuteInEditMode]
  public sealed class InputTextField : UIBehaviour
  {
    [UsedImplicitly]
    [SerializeField]
    private bool m_interactable = true;
    [UsedImplicitly]
    [SerializeField]
    private RawTextField m_text;
    [UsedImplicitly]
    [SerializeField]
    private TextField m_placeholderText;
    [UsedImplicitly]
    [SerializeField]
    private RectTransform m_viewport;
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.ContentType m_contentType;
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.LineType m_lineType;
    [UsedImplicitly]
    [SerializeField]
    private int m_characterLimit;
    [UsedImplicitly]
    [SerializeField]
    private Color m_selectionColor = new Color(0.65882355f, 0.807843149f, 1f, 0.7529412f);
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.OnChangeEvent m_onValueChanged = new TMP_InputField.OnChangeEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.SubmitEvent m_onEndEdit = new TMP_InputField.SubmitEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.SubmitEvent m_onSubmit = new TMP_InputField.SubmitEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.SelectionEvent m_onSelect = new TMP_InputField.SelectionEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.SelectionEvent m_onDeselect = new TMP_InputField.SelectionEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.TextSelectionEvent m_onTextSelection = new TMP_InputField.TextSelectionEvent();
    [UsedImplicitly]
    [SerializeField]
    private TMP_InputField.TextSelectionEvent m_onEndTextSelection = new TMP_InputField.TextSelectionEvent();
    [NonSerialized]
    private TMP_InputField m_inputFieldComponent;

    public Selectable selectable => (Selectable) this.m_inputFieldComponent;

    [PublicAPI]
    public bool interactable
    {
      get => this.m_interactable;
      set
      {
        if (value == this.m_interactable)
          return;
        this.m_interactable = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.interactable = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.ContentType contentType
    {
      get => this.m_contentType;
      set
      {
        if (value == this.m_contentType)
          return;
        this.m_contentType = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.contentType = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.LineType lineType
    {
      get => this.m_lineType;
      set
      {
        if (value == this.m_lineType)
          return;
        this.m_lineType = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.lineType = value;
      }
    }

    [PublicAPI]
    public int characterLimit
    {
      get => this.m_characterLimit;
      set
      {
        if (value == this.m_characterLimit)
          return;
        this.m_characterLimit = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.characterLimit = value;
      }
    }

    [PublicAPI]
    public Color selectionColor
    {
      get => this.m_selectionColor;
      set
      {
        if (value == this.m_selectionColor)
          return;
        this.m_selectionColor = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.selectionColor = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.OnChangeEvent onValueChanged
    {
      get => this.m_onValueChanged;
      set
      {
        this.m_onValueChanged = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onValueChanged = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.SubmitEvent onEndEdit
    {
      get => this.m_onEndEdit;
      set
      {
        this.m_onEndEdit = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onEndEdit = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.SubmitEvent onSubmit
    {
      get => this.m_onSubmit;
      set
      {
        this.m_onSubmit = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onSubmit = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.SelectionEvent onSelect
    {
      get => this.m_onSelect;
      set
      {
        this.m_onSelect = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onSelect = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.SelectionEvent onDeselect
    {
      get => this.m_onDeselect;
      set
      {
        this.m_onDeselect = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onDeselect = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.TextSelectionEvent onTextSelection
    {
      get => this.m_onTextSelection;
      set
      {
        this.m_onTextSelection = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onTextSelection = value;
      }
    }

    [PublicAPI]
    public TMP_InputField.TextSelectionEvent onEndTextSelection
    {
      get => this.m_onEndTextSelection;
      set
      {
        this.m_onEndTextSelection = value;
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent))
          return;
        this.m_inputFieldComponent.onEndTextSelection = value;
      }
    }

    [NotNull]
    [PublicAPI]
    public string GetText()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent)
        return this.m_inputFieldComponent.text;
      return !((UnityEngine.Object) null != (UnityEngine.Object) this.m_text) ? string.Empty : this.m_text.GetText();
    }

    [PublicAPI]
    public void SetText([NotNull] string value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent)
      {
        this.m_inputFieldComponent.text = value;
      }
      else
      {
        if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_text))
          return;
        this.m_text.SetText(value);
      }
    }

    [PublicAPI]
    public string GetPlaceholderText() => !((UnityEngine.Object) null != (UnityEngine.Object) this.m_placeholderText) ? string.Empty : this.m_placeholderText.GetText();

    [PublicAPI]
    public void SetPlaceholderText(int textKeyId, IValueProvider valueProvider = null)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_placeholderText)
        return;
      this.m_placeholderText.SetText(textKeyId, valueProvider);
    }

    [PublicAPI]
    public void SetPlaceholderText(string textKeyName, IValueProvider valueProvider = null)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_placeholderText)
        return;
      this.m_placeholderText.SetText(textKeyName, valueProvider);
    }

    protected override void Start()
    {
      base.Start();
      if (!((UnityEngine.Object) null == (UnityEngine.Object) this.m_inputFieldComponent))
        return;
      this.CreateInputTextMeshProComponent();
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      if ((UnityEngine.Object) null == (UnityEngine.Object) this.m_inputFieldComponent)
        return;
      this.m_inputFieldComponent.enabled = true;
    }

    protected override void OnDisable()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent)
        this.m_inputFieldComponent.enabled = false;
      base.OnDisable();
    }

    protected override void OnDestroy()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_text)
        this.m_text.TextComponentCreated -= new AbstractTextField.TextComponentCreatedEventHandler(this.OnTextComponentCreated);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_placeholderText)
        this.m_placeholderText.TextComponentCreated -= new AbstractTextField.TextComponentCreatedEventHandler(this.OnPlaceholderTextComponentCreated);
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_inputFieldComponent)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.m_inputFieldComponent);
        this.m_inputFieldComponent = (TMP_InputField) null;
      }
      base.OnDestroy();
    }

    private void CreateInputTextMeshProComponent()
    {
      TMP_InputField tmpInputField = this.GetComponent<TMP_InputField>();
      if ((UnityEngine.Object) null == (UnityEngine.Object) tmpInputField)
        tmpInputField = this.gameObject.AddComponent<TMP_InputField>();
      this.m_inputFieldComponent = tmpInputField;
      tmpInputField.enabled = false;
      tmpInputField.interactable = this.m_interactable;
      tmpInputField.textViewport = this.m_viewport;
      tmpInputField.transition = Selectable.Transition.None;
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_text)
      {
        TMP_Text textComponent = this.m_text.GetTextComponent();
        if ((UnityEngine.Object) null == (UnityEngine.Object) textComponent)
        {
          this.m_text.TextComponentCreated += new AbstractTextField.TextComponentCreatedEventHandler(this.OnTextComponentCreated);
          tmpInputField.enabled = false;
        }
        else
          this.OnTextComponentCreated(textComponent);
      }
      if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_placeholderText)
      {
        TMP_Text textComponent = this.m_placeholderText.GetTextComponent();
        if ((UnityEngine.Object) null == (UnityEngine.Object) textComponent)
          this.m_placeholderText.TextComponentCreated += new AbstractTextField.TextComponentCreatedEventHandler(this.OnPlaceholderTextComponentCreated);
        else
          this.OnPlaceholderTextComponentCreated(textComponent);
      }
      tmpInputField.onValueChanged = this.m_onValueChanged;
      tmpInputField.onEndEdit = this.m_onEndEdit;
      tmpInputField.onSubmit = this.m_onSubmit;
      tmpInputField.onSelect = this.m_onSelect;
      tmpInputField.onDeselect = this.m_onDeselect;
      tmpInputField.onTextSelection = this.m_onTextSelection;
      tmpInputField.onEndTextSelection = this.m_onEndTextSelection;
    }

    private void OnTextComponentCreated(TMP_Text textComponent)
    {
      TMP_InputField inputFieldComponent = this.m_inputFieldComponent;
      if ((UnityEngine.Object) null == (UnityEngine.Object) inputFieldComponent)
        return;
      inputFieldComponent.textComponent = textComponent;
      inputFieldComponent.text = (UnityEngine.Object) null != (UnityEngine.Object) this.m_text ? this.m_text.GetText() : string.Empty;
      inputFieldComponent.contentType = this.m_contentType;
      inputFieldComponent.lineType = this.m_lineType;
      inputFieldComponent.characterLimit = this.m_characterLimit;
      inputFieldComponent.selectionColor = this.m_selectionColor;
      inputFieldComponent.richText = false;
      inputFieldComponent.isRichTextEditingAllowed = false;
      inputFieldComponent.enabled = (UnityEngine.Object) null != (UnityEngine.Object) textComponent;
    }

    private void OnPlaceholderTextComponentCreated(TMP_Text textComponent)
    {
      TMP_InputField inputFieldComponent = this.m_inputFieldComponent;
      if ((UnityEngine.Object) null == (UnityEngine.Object) inputFieldComponent)
        return;
      inputFieldComponent.placeholder = (Graphic) textComponent;
      textComponent.enabled = string.IsNullOrEmpty(inputFieldComponent.text);
    }
  }
}
