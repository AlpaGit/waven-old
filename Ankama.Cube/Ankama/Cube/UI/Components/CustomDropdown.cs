// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.Components.CustomDropdown
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ankama.Cube.UI.Components
{
  [AddComponentMenu("UI/Custom Dropdown", 35)]
  [RequireComponent(typeof (RectTransform))]
  public class CustomDropdown : 
    Selectable,
    IPointerClickHandler,
    IEventSystemHandler,
    ISubmitHandler,
    ICancelHandler
  {
    [SerializeField]
    private RectTransform m_Template;
    [SerializeField]
    private Text m_CaptionText;
    [SerializeField]
    private Image m_CaptionImage;
    [Space]
    [SerializeField]
    private Text m_ItemText;
    [SerializeField]
    private Image m_ItemImage;
    [Space]
    [SerializeField]
    private int m_Value;
    [Space]
    [SerializeField]
    private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();
    [Space]
    [SerializeField]
    private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();
    private GameObject m_Dropdown;
    private GameObject m_Blocker;
    private List<CustomDropdown.DropdownItem> m_Items = new List<CustomDropdown.DropdownItem>();
    private bool validTemplate;
    private static Dropdown.OptionData s_NoOptionData = new Dropdown.OptionData();

    public RectTransform template
    {
      get => this.m_Template;
      set
      {
        this.m_Template = value;
        this.RefreshShownValue();
      }
    }

    public Text captionText
    {
      get => this.m_CaptionText;
      set
      {
        this.m_CaptionText = value;
        this.RefreshShownValue();
      }
    }

    public Image captionImage
    {
      get => this.m_CaptionImage;
      set
      {
        this.m_CaptionImage = value;
        this.RefreshShownValue();
      }
    }

    public Text itemText
    {
      get => this.m_ItemText;
      set
      {
        this.m_ItemText = value;
        this.RefreshShownValue();
      }
    }

    public Image itemImage
    {
      get => this.m_ItemImage;
      set
      {
        this.m_ItemImage = value;
        this.RefreshShownValue();
      }
    }

    public List<Dropdown.OptionData> options
    {
      get => this.m_Options.options;
      set
      {
        this.m_Options.options = value;
        this.RefreshShownValue();
      }
    }

    public Dropdown.DropdownEvent onValueChanged
    {
      get => this.m_OnValueChanged;
      set => this.m_OnValueChanged = value;
    }

    public int value
    {
      get => this.m_Value;
      set
      {
        if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
          return;
        this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
        this.RefreshShownValue();
        UISystemProfilerApi.AddMarker("Dropdown.value", (Object) this);
        this.m_OnValueChanged.Invoke(this.m_Value);
      }
    }

    protected override void Awake()
    {
      if ((bool) (Object) this.m_CaptionImage)
        this.m_CaptionImage.enabled = (Object) this.m_CaptionImage.sprite != (Object) null;
      if (!(bool) (Object) this.m_Template)
        return;
      this.m_Template.gameObject.SetActive(false);
    }

    public void RefreshShownValue()
    {
      Dropdown.OptionData optionData = CustomDropdown.s_NoOptionData;
      if (this.options.Count > 0)
        optionData = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
      if ((bool) (Object) this.m_CaptionText)
        this.m_CaptionText.text = optionData == null || optionData.text == null ? "" : optionData.text;
      if (!(bool) (Object) this.m_CaptionImage)
        return;
      this.m_CaptionImage.sprite = optionData == null ? (Sprite) null : optionData.image;
      this.m_CaptionImage.enabled = (Object) this.m_CaptionImage.sprite != (Object) null;
    }

    public void AddOptions(List<Dropdown.OptionData> options)
    {
      this.options.AddRange((IEnumerable<Dropdown.OptionData>) options);
      this.RefreshShownValue();
    }

    public void AddOptions(List<string> options)
    {
      for (int index = 0; index < options.Count; ++index)
        this.options.Add(new Dropdown.OptionData(options[index]));
      this.RefreshShownValue();
    }

    public void AddOptions(List<Sprite> options)
    {
      for (int index = 0; index < options.Count; ++index)
        this.options.Add(new Dropdown.OptionData(options[index]));
      this.RefreshShownValue();
    }

    public void ClearOptions()
    {
      this.options.Clear();
      this.RefreshShownValue();
    }

    private void SetupTemplate()
    {
      this.validTemplate = false;
      if (!(bool) (Object) this.m_Template)
      {
        Debug.LogError((object) "The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", (Object) this);
      }
      else
      {
        GameObject gameObject = this.m_Template.gameObject;
        gameObject.SetActive(true);
        Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
        this.validTemplate = true;
        if (!(bool) (Object) componentInChildren || (Object) componentInChildren.transform == (Object) this.template)
        {
          this.validTemplate = false;
          Debug.LogError((object) "The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", (Object) this.template);
        }
        else if (!(componentInChildren.transform.parent is RectTransform))
        {
          this.validTemplate = false;
          Debug.LogError((object) "The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", (Object) this.template);
        }
        else if ((Object) this.itemText != (Object) null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
        {
          this.validTemplate = false;
          Debug.LogError((object) "The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", (Object) this.template);
        }
        else if ((Object) this.itemImage != (Object) null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
        {
          this.validTemplate = false;
          Debug.LogError((object) "The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", (Object) this.template);
        }
        if (!this.validTemplate)
        {
          gameObject.SetActive(false);
        }
        else
        {
          CustomDropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<CustomDropdown.DropdownItem>();
          dropdownItem.text = this.m_ItemText;
          dropdownItem.image = this.m_ItemImage;
          dropdownItem.toggle = componentInChildren;
          dropdownItem.rectTransform = (RectTransform) componentInChildren.transform;
          Canvas orAddComponent = CustomDropdown.GetOrAddComponent<Canvas>(gameObject);
          orAddComponent.overrideSorting = true;
          orAddComponent.sortingOrder = 30000;
          CustomDropdown.GetOrAddComponent<CustomGraphicRaycaster>(gameObject);
          CustomDropdown.GetOrAddComponent<CanvasGroup>(gameObject);
          gameObject.SetActive(false);
          this.validTemplate = true;
        }
      }
    }

    private static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
      T orAddComponent = go.GetComponent<T>();
      if (!(bool) (Object) orAddComponent)
        orAddComponent = go.AddComponent<T>();
      return orAddComponent;
    }

    public virtual void OnPointerClick(PointerEventData eventData) => this.Show();

    public virtual void OnSubmit(BaseEventData eventData) => this.Show();

    public virtual void OnCancel(BaseEventData eventData) => this.Hide();

    public void Show()
    {
      if (!this.IsActive() || !this.IsInteractable() || (Object) this.m_Dropdown != (Object) null)
        return;
      if (!this.validTemplate)
      {
        this.SetupTemplate();
        if (!this.validTemplate)
          return;
      }
      Canvas rootCanvas = this.gameObject.GetRootCanvas();
      if ((Object) rootCanvas == (Object) null)
        return;
      this.m_Template.gameObject.SetActive(true);
      this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
      this.m_Dropdown.name = "Dropdown List";
      this.m_Dropdown.SetActive(true);
      RectTransform transform1 = this.m_Dropdown.transform as RectTransform;
      transform1.SetParent(this.m_Template.transform.parent, false);
      CustomDropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<CustomDropdown.DropdownItem>();
      RectTransform transform2 = componentInChildren.rectTransform.parent.gameObject.transform as RectTransform;
      componentInChildren.rectTransform.gameObject.SetActive(true);
      Rect rect1 = transform2.rect;
      Rect rect2 = componentInChildren.rectTransform.rect;
      Vector2 vector2_1 = rect2.min - rect1.min + (Vector2) componentInChildren.rectTransform.localPosition;
      Vector2 vector2_2 = rect2.max - rect1.max + (Vector2) componentInChildren.rectTransform.localPosition;
      Vector2 size = rect2.size;
      this.m_Items.Clear();
      Toggle toggle = (Toggle) null;
      for (int index = 0; index < this.options.Count; ++index)
      {
        CustomDropdown.DropdownItem item = this.AddItem(this.options[index], this.value == index, componentInChildren, this.m_Items);
        if (!((Object) item == (Object) null))
        {
          item.toggle.isOn = this.value == index;
          item.toggle.onValueChanged.AddListener((UnityAction<bool>) (x => this.OnSelectItem(item.toggle)));
          if (item.toggle.isOn)
            item.toggle.Select();
          if ((Object) toggle != (Object) null)
          {
            Navigation navigation1 = toggle.navigation;
            Navigation navigation2 = item.toggle.navigation;
            navigation1.mode = Navigation.Mode.Explicit;
            navigation2.mode = Navigation.Mode.Explicit;
            navigation1.selectOnDown = (Selectable) item.toggle;
            navigation1.selectOnRight = (Selectable) item.toggle;
            navigation2.selectOnLeft = (Selectable) toggle;
            navigation2.selectOnUp = (Selectable) toggle;
            toggle.navigation = navigation1;
            item.toggle.navigation = navigation2;
          }
          toggle = item.toggle;
        }
      }
      Vector2 sizeDelta = transform2.sizeDelta with
      {
        y = size.y * (float) this.m_Items.Count + vector2_1.y - vector2_2.y
      };
      transform2.sizeDelta = sizeDelta;
      Rect rect3 = transform1.rect;
      double height1 = (double) rect3.height;
      rect3 = transform2.rect;
      double height2 = (double) rect3.height;
      float num1 = (float) (height1 - height2);
      if ((double) num1 > 0.0)
        transform1.sizeDelta = new Vector2(transform1.sizeDelta.x, transform1.sizeDelta.y - num1);
      Vector3[] fourCornersArray = new Vector3[4];
      transform1.GetWorldCorners(fourCornersArray);
      RectTransform transform3 = rootCanvas.transform as RectTransform;
      Rect rect4 = transform3.rect;
      for (int index1 = 0; index1 < 2; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Vector3 vector3 = transform3.InverseTransformPoint(fourCornersArray[index2]);
          double num2 = (double) vector3[index1];
          Vector2 vector2_3 = rect4.min;
          double num3 = (double) vector2_3[index1];
          if (num2 >= num3)
          {
            double num4 = (double) vector3[index1];
            vector2_3 = rect4.max;
            double num5 = (double) vector2_3[index1];
            if (num4 <= num5)
              continue;
          }
          flag = true;
          break;
        }
        if (flag)
          RectTransformUtility.FlipLayoutOnAxis(transform1, index1, false, false);
      }
      for (int index = 0; index < this.m_Items.Count; ++index)
      {
        RectTransform rectTransform = this.m_Items[index].rectTransform;
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, 0.0f);
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, 0.0f);
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, (float) ((double) vector2_1.y + (double) size.y * (double) (this.m_Items.Count - 1 - index) + (double) size.y * (double) rectTransform.pivot.y));
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, size.y);
      }
      this.m_Template.gameObject.SetActive(false);
      componentInChildren.gameObject.SetActive(false);
      this.m_Blocker = this.CreateBlocker(rootCanvas);
    }

    protected virtual GameObject CreateBlocker(Canvas rootCanvas)
    {
      GameObject blocker = new GameObject("Blocker");
      RectTransform rectTransform = blocker.AddComponent<RectTransform>();
      rectTransform.SetParent(rootCanvas.transform, false);
      rectTransform.anchorMin = (Vector2) Vector3.zero;
      rectTransform.anchorMax = (Vector2) Vector3.one;
      rectTransform.sizeDelta = Vector2.zero;
      Canvas canvas = blocker.AddComponent<Canvas>();
      canvas.overrideSorting = true;
      Canvas component = this.m_Dropdown.GetComponent<Canvas>();
      canvas.sortingLayerID = component.sortingLayerID;
      canvas.sortingOrder = component.sortingOrder - 1;
      blocker.AddComponent<CustomGraphicRaycaster>();
      blocker.AddComponent<Image>().color = Color.clear;
      blocker.AddComponent<Button>().onClick.AddListener(new UnityAction(this.Hide));
      return blocker;
    }

    protected virtual void DestroyBlocker(GameObject blocker) => Object.Destroy((Object) blocker);

    protected virtual GameObject CreateDropdownList(GameObject template) => Object.Instantiate<GameObject>(template);

    protected virtual void DestroyDropdownList(GameObject dropdownList) => Object.Destroy((Object) dropdownList);

    protected virtual CustomDropdown.DropdownItem CreateItem(
      CustomDropdown.DropdownItem itemTemplate)
    {
      return Object.Instantiate<CustomDropdown.DropdownItem>(itemTemplate);
    }

    private CustomDropdown.DropdownItem AddItem(
      Dropdown.OptionData data,
      bool selected,
      CustomDropdown.DropdownItem itemTemplate,
      List<CustomDropdown.DropdownItem> items)
    {
      CustomDropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
      dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
      dropdownItem.gameObject.SetActive(true);
      dropdownItem.gameObject.name = "Item " + (object) items.Count + (data.text != null ? (object) (": " + data.text) : (object) "");
      if ((Object) dropdownItem.toggle != (Object) null)
        dropdownItem.toggle.isOn = false;
      if ((bool) (Object) dropdownItem.text)
        dropdownItem.text.text = data.text;
      if ((bool) (Object) dropdownItem.image)
      {
        dropdownItem.image.sprite = data.image;
        dropdownItem.image.enabled = (Object) dropdownItem.image.sprite != (Object) null;
      }
      items.Add(dropdownItem);
      return dropdownItem;
    }

    public void Hide()
    {
      if ((Object) this.m_Dropdown != (Object) null)
        this.DestroyDropdownList(this.m_Dropdown);
      this.m_Dropdown = (GameObject) null;
      if ((Object) this.m_Blocker != (Object) null)
        this.DestroyBlocker(this.m_Blocker);
      this.m_Blocker = (GameObject) null;
      this.Select();
    }

    private void OnSelectItem(Toggle toggle)
    {
      if (!toggle.isOn)
        toggle.isOn = true;
      int num = -1;
      Transform transform = toggle.transform;
      Transform parent = transform.parent;
      for (int index = 0; index < parent.childCount; ++index)
      {
        if ((Object) parent.GetChild(index) == (Object) transform)
        {
          num = index - 1;
          break;
        }
      }
      if (num < 0)
        return;
      this.value = num;
      this.Hide();
    }

    protected internal class DropdownItem : 
      MonoBehaviour,
      IPointerEnterHandler,
      IEventSystemHandler,
      ICancelHandler
    {
      [SerializeField]
      private Text m_Text;
      [SerializeField]
      private Image m_Image;
      [SerializeField]
      private RectTransform m_RectTransform;
      [SerializeField]
      private Toggle m_Toggle;

      public Text text
      {
        get => this.m_Text;
        set => this.m_Text = value;
      }

      public Image image
      {
        get => this.m_Image;
        set => this.m_Image = value;
      }

      public RectTransform rectTransform
      {
        get => this.m_RectTransform;
        set => this.m_RectTransform = value;
      }

      public Toggle toggle
      {
        get => this.m_Toggle;
        set => this.m_Toggle = value;
      }

      public virtual void OnPointerEnter(PointerEventData eventData) => EventSystem.current.SetSelectedGameObject(this.gameObject);

      public virtual void OnCancel(BaseEventData eventData)
      {
        Dropdown componentInParent = this.GetComponentInParent<Dropdown>();
        if (!(bool) (Object) componentInParent)
          return;
        componentInParent.Hide();
      }
    }
  }
}
