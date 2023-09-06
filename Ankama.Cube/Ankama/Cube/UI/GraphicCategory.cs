// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.UI.GraphicCategory
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Code.UI;
using Ankama.Cube.Data.UI.Localization.TextFormatting;
using Ankama.Cube.Player;
using Ankama.Cube.SRP;
using Ankama.Cube.UI.Components;
using Ankama.ScreenManagement;
using Ankama.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ankama.Cube.UI
{
  public class GraphicCategory : OptionCategory
  {
    [SerializeField]
    protected Button m_defaultResolutionButton;
    [SerializeField]
    protected TextFieldDropdown m_fullScreenDropdown;
    [SerializeField]
    protected TextFieldDropdown m_displayDropdown;
    [SerializeField]
    protected TextFieldDropdown m_ratioDropdown;
    [SerializeField]
    protected TextFieldDropdown m_resolutionDropdown;
    [SerializeField]
    protected TextFieldDropdown m_graphicPresetDropdown;
    [SerializeField]
    protected Button m_applyButton;
    private int m_currentFullScreenDropDownIndex = -1;
    private int m_currentDisplayDropDownIndex = -1;
    private int m_currentResolutionDropDownIndex = -1;
    private int m_currentRatioDropDownIndex = -1;
    private int m_currentGraphicPresetDropDownIndex = -1;
    private const float MinimumSupportedRatio = 1.25f;
    private const float MaximumSupportedRatio = 2.33333325f;
    private const float SupportedRatioThreshold = 0.005f;
    private static readonly float[] SupportedRatios = new float[9]
    {
      2.33333325f,
      2f,
      1.77777779f,
      1.6f,
      1.33333337f,
      1.25f,
      1.66666663f,
      1.5f,
      1.4f
    };
    private static readonly string[] SupportedRatioNames = new string[9]
    {
      "21:9",
      "18:9",
      "16:9",
      "16:10",
      "4:3",
      "5:4",
      "5:3",
      "3:2",
      "7:5"
    };
    private List<GraphicCategory.ResolutionData> m_fullScreenResolutions = new List<GraphicCategory.ResolutionData>();
    private GraphicCategory.ResolutionData m_windowedResolutions;
    private bool m_lastFullScreen;

    protected void Awake()
    {
      List<string> options1 = new List<string>()
      {
        RuntimeData.FormattedText(9912, (IValueProvider) null),
        RuntimeData.FormattedText(68421, (IValueProvider) null)
      };
      this.m_fullScreenDropdown.ClearOptions();
      this.m_fullScreenDropdown.AddOptions(options1);
      this.m_fullScreenDropdown.RefreshShownValue();
      List<string> options2 = new List<string>();
      List<QualityAsset> qualityPresets = QualityManager.GetQualityPresets();
      for (int index = 0; index < qualityPresets.Count; ++index)
      {
        QualityAsset qualityAsset = qualityPresets[index];
        string[] strArray = qualityAsset.name.Split('_');
        string str = strArray.Length != 0 ? strArray[strArray.Length - 1] : qualityAsset.name;
        options2.Add(str);
      }
      this.m_graphicPresetDropdown.ClearOptions();
      this.m_graphicPresetDropdown.AddOptions(options2);
      this.m_graphicPresetDropdown.RefreshShownValue();
    }

    protected override void OnBecameEnable()
    {
      ScreenManager.UpdateDisplayInformation();
      this.UpdateResolutionData();
      this.InitializeScreenUI();
      this.m_lastFullScreen = Device.fullScreen;
      Device.ScreenStateChanged += new Device.ScreenSateChangedDelegate(this.OnScreenStateChanged);
      this.m_graphicPresetDropdown.value = QualityManager.GetQualityPresetIndex();
      this.m_graphicPresetDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnQualityDropdownChanged));
      this.m_currentGraphicPresetDropDownIndex = this.m_graphicPresetDropdown.value;
      this.m_applyButton.onClick.AddListener(new UnityAction(this.OnApplyButtonClicked));
      this.UpdateApplyButton();
    }

    protected override void OnBecameDisable()
    {
      this.DisposeScreenUIEvent();
      Device.ScreenStateChanged -= new Device.ScreenSateChangedDelegate(this.OnScreenStateChanged);
      this.m_graphicPresetDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnQualityDropdownChanged));
      this.m_applyButton.onClick.RemoveListener(new UnityAction(this.OnApplyButtonClicked));
    }

    protected void UpdateApplyButton() => this.m_applyButton.interactable = this.m_fullScreenDropdown.value != this.m_currentFullScreenDropDownIndex || this.m_displayDropdown.value != this.m_currentDisplayDropDownIndex || this.m_ratioDropdown.value != this.m_currentRatioDropDownIndex || this.m_resolutionDropdown.value != this.m_currentResolutionDropDownIndex || this.m_graphicPresetDropdown.value != this.m_currentGraphicPresetDropDownIndex;

    protected void OnApplyButtonClicked()
    {
      this.ApplyScreenResolution();
      this.ApplyGraphicQuality();
      this.UpdateApplyButton();
    }

    protected void ApplyGraphicQuality()
    {
      if (this.m_currentGraphicPresetDropDownIndex == this.m_graphicPresetDropdown.value)
        return;
      this.m_currentGraphicPresetDropDownIndex = this.m_graphicPresetDropdown.value;
      QualityManager.SetQualityPresetIndex(this.m_currentGraphicPresetDropDownIndex);
      PlayerPreferences.graphicPresetIndex = this.m_currentGraphicPresetDropDownIndex;
    }

    protected void OnQualityDropdownChanged(int value) => this.UpdateApplyButton();

    private void InitializeScreenUI()
    {
      this.DisposeScreenUIEvent();
      bool fullScreen = Device.fullScreen;
      this.m_fullScreenDropdown.value = fullScreen ? 0 : 1;
      this.m_fullScreenDropdown.RefreshShownValue();
      this.m_displayDropdown.transform.parent.parent.gameObject.SetActive(fullScreen);
      this.m_defaultResolutionButton.transform.parent.gameObject.SetActive(fullScreen);
      if (!ScreenManager.TryGetCurrentDisplayIndex(out int _))
      {
        Log.Error("Cannot get current Display", 179, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
        this.m_displayDropdown.ClearOptions();
        this.m_displayDropdown.AddOptions(new List<string>()
        {
          RuntimeData.FormattedText(28972, (IValueProvider) null)
        });
        this.m_displayDropdown.value = 0;
        this.m_displayDropdown.RefreshShownValue();
        this.m_ratioDropdown.ClearOptions();
        this.m_ratioDropdown.AddOptions(new List<string>()
        {
          RuntimeData.FormattedText(48663, (IValueProvider) null)
        });
        this.m_ratioDropdown.value = 0;
        this.m_ratioDropdown.RefreshShownValue();
        this.m_resolutionDropdown.ClearOptions();
        this.m_resolutionDropdown.AddOptions(new List<string>()
        {
          RuntimeData.FormattedText(33868, (IValueProvider) null)
        });
        this.m_resolutionDropdown.value = 0;
        this.m_resolutionDropdown.RefreshShownValue();
        this.m_fullScreenDropdown.interactable = false;
        this.m_displayDropdown.interactable = false;
        this.m_ratioDropdown.interactable = false;
        this.m_resolutionDropdown.interactable = false;
        this.m_defaultResolutionButton.interactable = false;
        Action<PopupInfo> onErrorPopupInfo = this.OnErrorPopupInfo;
        if (onErrorPopupInfo != null)
          onErrorPopupInfo(new PopupInfo()
          {
            title = (RawTextData) 72461,
            message = (RawTextData) 10321,
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            selectedButton = 1,
            style = PopupStyle.Error
          });
      }
      else
      {
        this.InitializeDisplayList();
        this.UpdateRatioList();
        this.UpdateResolutionList();
        this.m_fullScreenDropdown.interactable = true;
        this.m_displayDropdown.interactable = true;
        this.m_ratioDropdown.interactable = true;
        this.m_resolutionDropdown.interactable = true;
        this.UpdateDefaultResolutionButton();
        this.m_fullScreenDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnFullscreenDropdownChanged));
        this.m_displayDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnDisplayDropdownChanged));
        this.m_ratioDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnRatioDropdownChanged));
        this.m_resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(this.OnResolutionDropdownChanged));
        this.m_defaultResolutionButton.onClick.AddListener(new UnityAction(this.OnDefaultResolutionButtonClicked));
      }
      this.m_currentFullScreenDropDownIndex = this.m_fullScreenDropdown.value;
      this.m_currentDisplayDropDownIndex = this.m_displayDropdown.value;
      this.m_currentRatioDropDownIndex = this.m_ratioDropdown.value;
      this.m_currentResolutionDropDownIndex = this.m_resolutionDropdown.value;
    }

    private void DisposeScreenUIEvent()
    {
      this.m_fullScreenDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnFullscreenDropdownChanged));
      this.m_displayDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnDisplayDropdownChanged));
      this.m_ratioDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnRatioDropdownChanged));
      this.m_resolutionDropdown.onValueChanged.RemoveListener(new UnityAction<int>(this.OnResolutionDropdownChanged));
      this.m_defaultResolutionButton.onClick.RemoveListener(new UnityAction(this.OnDefaultResolutionButtonClicked));
    }

    private void OnScreenStateChanged()
    {
      bool fullScreen = Device.fullScreen;
      bool flag = this.m_currentFullScreenDropDownIndex == 0;
      if (fullScreen == this.m_lastFullScreen || fullScreen == flag)
        return;
      this.m_lastFullScreen = fullScreen;
      this.InitializeScreenUI();
      this.UpdateApplyButton();
    }

    private void UpdateResolutionData()
    {
      this.m_fullScreenResolutions.Clear();
      int displayCount = ScreenManager.GetDisplayCount();
      if (displayCount == 0)
        Log.Error("No display detected", 280, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      for (int index1 = 0; index1 < displayCount; ++index1)
      {
        DisplayInfo displayInfo = ScreenManager.GetDisplayInfo(index1);
        Resolution systemResolution = displayInfo.systemResolution;
        int ratioIndex1 = GraphicCategory.GetRatioIndex(systemResolution.width, systemResolution.height);
        GraphicCategory.ResolutionData resolutionData = new GraphicCategory.ResolutionData();
        resolutionData.ratioIndex = new List<int>();
        resolutionData.resolutionsByRatio = new List<List<Resolution>>();
        resolutionData.defaultResolution = systemResolution;
        resolutionData.defaultRatioIndex = ratioIndex1;
        resolutionData.displayInfo = displayInfo;
        int length = displayInfo.resolutions.Length;
        for (int index2 = 0; index2 < length; ++index2)
        {
          Resolution resolution = displayInfo.resolutions[index2];
          int ratioIndex2 = GraphicCategory.GetRatioIndex(resolution.width, resolution.height);
          if (ratioIndex2 >= 0)
          {
            if (!resolutionData.ratioIndex.Contains(ratioIndex2))
            {
              resolutionData.ratioIndex.Add(ratioIndex2);
              resolutionData.resolutionsByRatio.Add(new List<Resolution>()
              {
                resolution
              });
            }
            else
            {
              int index3 = resolutionData.ratioIndex.IndexOf(ratioIndex2);
              resolutionData.resolutionsByRatio[index3].Add(resolution);
            }
            if (systemResolution.width == resolution.width && systemResolution.height == resolution.height)
            {
              int index4 = resolutionData.ratioIndex.IndexOf(ratioIndex2);
              resolutionData.defaultRatioDropDownIndex = index4;
              resolutionData.defaultResolutionDropDownIndex = resolutionData.resolutionsByRatio[index4].Count - 1;
            }
          }
        }
        this.m_fullScreenResolutions.Add(resolutionData);
      }
      this.m_windowedResolutions = new GraphicCategory.ResolutionData();
      this.m_windowedResolutions.ratioIndex = new List<int>();
      this.m_windowedResolutions.resolutionsByRatio = new List<List<Resolution>>();
      int windowedResolutionCount = ScreenManager.GetWindowedResolutionCount();
      if (windowedResolutionCount == 0)
        Log.Error("No windowed resolution detected", 334, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      for (int index = 0; index < windowedResolutionCount; ++index)
      {
        Resolution windowedResolution = ScreenManager.GetWindowedResolution(index);
        int ratioIndex = GraphicCategory.GetRatioIndex(windowedResolution.width, windowedResolution.height);
        if (ratioIndex >= 0)
        {
          if (!this.m_windowedResolutions.ratioIndex.Contains(ratioIndex))
          {
            this.m_windowedResolutions.ratioIndex.Add(ratioIndex);
            this.m_windowedResolutions.resolutionsByRatio.Add(new List<Resolution>()
            {
              windowedResolution
            });
          }
          else
            this.m_windowedResolutions.resolutionsByRatio[this.m_windowedResolutions.ratioIndex.IndexOf(ratioIndex)].Add(windowedResolution);
        }
      }
    }

    private bool CheckIfDataValid(bool fullscreen, int displayIndex, Resolution resolution)
    {
      if (!this.CheckIfResolutionDataAreSynchronized())
        return false;
      return fullscreen ? GraphicCategory.CheckIfDisplayResolutionIsValid(displayIndex, resolution) : GraphicCategory.CheckIfWindowedResolutionIsValid(resolution);
    }

    private bool CheckIfResolutionDataAreSynchronized()
    {
      int displayCount = ScreenManager.GetDisplayCount();
      if (displayCount != this.m_fullScreenResolutions.Count)
        return false;
      for (int index = 0; index < displayCount; ++index)
      {
        if (!this.m_fullScreenResolutions[index].displayInfo.name.Equals(ScreenManager.GetDisplayInfo(index).name))
          return false;
      }
      return true;
    }

    private static bool CheckIfWindowedResolutionIsValid(Resolution resolution)
    {
      bool flag = false;
      int windowedResolutionCount = ScreenManager.GetWindowedResolutionCount();
      for (int index = 0; index < windowedResolutionCount; ++index)
      {
        Resolution windowedResolution = ScreenManager.GetWindowedResolution(index);
        if (resolution.width == windowedResolution.width && resolution.height == windowedResolution.height)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private static bool CheckIfDisplayResolutionIsValid(int displayIndex, Resolution resolution) => ScreenManager.ContainsResolutionSize(ScreenManager.GetDisplayInfo(displayIndex).resolutions, resolution);

    private void InitializeDisplayList()
    {
      int displayIndex;
      if (!ScreenManager.TryGetCurrentDisplayIndex(out displayIndex))
      {
        Log.Error("Cannot get current Display", 422, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      }
      else
      {
        int displayCount = ScreenManager.GetDisplayCount();
        if (displayCount == 0)
          Log.Error("No display detected", 429, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
        List<string> options = new List<string>();
        for (int index = 0; index < displayCount; ++index)
        {
          DisplayInfo displayInfo = ScreenManager.GetDisplayInfo(index);
          options.Add(string.Format("{0} {1}", (object) displayInfo.name, (object) (index + 1)));
        }
        this.m_displayDropdown.ClearOptions();
        this.m_displayDropdown.AddOptions(options);
        this.m_displayDropdown.value = displayIndex;
        this.m_displayDropdown.RefreshShownValue();
      }
    }

    private void UpdateRatioList()
    {
      int ratioIndex = GraphicCategory.GetRatioIndex(Screen.width, Screen.height);
      int num = -1;
      List<string> options = new List<string>();
      if (this.m_displayDropdown.value >= 0)
      {
        GraphicCategory.ResolutionData resolutionData = this.m_fullScreenDropdown.value != 0 ? this.m_windowedResolutions : this.m_fullScreenResolutions[this.m_displayDropdown.value];
        for (int index1 = 0; index1 < resolutionData.ratioIndex.Count; ++index1)
        {
          int index2 = resolutionData.ratioIndex[index1];
          options.Add(GraphicCategory.SupportedRatioNames[index2]);
          if (ratioIndex == index2)
            num = options.Count - 1;
        }
      }
      if (num == -1)
        num = options.Count - 1;
      this.m_ratioDropdown.ClearOptions();
      this.m_ratioDropdown.AddOptions(options);
      this.m_ratioDropdown.value = num;
      this.m_ratioDropdown.RefreshShownValue();
    }

    private void UpdateResolutionList()
    {
      int num = -1;
      List<string> options = new List<string>();
      if (this.m_displayDropdown.value >= 0 && this.m_ratioDropdown.value >= 0)
      {
        List<Resolution> resolutionList = (this.m_fullScreenDropdown.value != 0 ? this.m_windowedResolutions : this.m_fullScreenResolutions[this.m_displayDropdown.value]).resolutionsByRatio[this.m_ratioDropdown.value];
        for (int index = 0; index < resolutionList.Count; ++index)
        {
          Resolution resolution = resolutionList[index];
          if (resolution.refreshRate == 0)
            options.Add(string.Format("{0}x{1}", (object) resolution.width, (object) resolution.height));
          else
            options.Add(string.Format("{0}x{1} {2}Hz", (object) resolution.width, (object) resolution.height, (object) resolution.refreshRate));
          if (resolution.width == Screen.width && resolution.height == Screen.height)
            num = index;
        }
      }
      if (num == -1)
        num = options.Count - 1;
      this.m_resolutionDropdown.ClearOptions();
      this.m_resolutionDropdown.AddOptions(options);
      this.m_resolutionDropdown.value = num;
      this.m_resolutionDropdown.RefreshShownValue();
    }

    protected void UpdateDefaultResolutionButton()
    {
      if (this.m_fullScreenDropdown.value != 0)
      {
        this.m_defaultResolutionButton.interactable = false;
      }
      else
      {
        GraphicCategory.ResolutionData screenResolution = this.m_fullScreenResolutions[this.m_displayDropdown.value];
        this.m_defaultResolutionButton.interactable = this.m_ratioDropdown.value != screenResolution.defaultRatioDropDownIndex || this.m_resolutionDropdown.value != screenResolution.defaultResolutionDropDownIndex;
      }
    }

    protected void OnFullscreenDropdownChanged(int value)
    {
      bool flag = value == 0;
      this.m_displayDropdown.transform.parent.parent.gameObject.SetActive(flag);
      this.m_defaultResolutionButton.transform.parent.gameObject.SetActive(flag);
      this.UpdateRatioList();
      this.UpdateResolutionList();
      this.UpdateDefaultResolutionButton();
      this.UpdateApplyButton();
    }

    private void OnDisplayDropdownChanged(int value)
    {
      this.UpdateRatioList();
      this.UpdateResolutionList();
      this.UpdateDefaultResolutionButton();
      this.UpdateApplyButton();
    }

    protected void OnRatioDropdownChanged(int value)
    {
      this.UpdateResolutionList();
      this.UpdateDefaultResolutionButton();
      this.UpdateApplyButton();
    }

    protected void OnResolutionDropdownChanged(int value)
    {
      this.UpdateDefaultResolutionButton();
      this.UpdateApplyButton();
    }

    protected void OnDefaultResolutionButtonClicked()
    {
      if (this.m_fullScreenDropdown.value != 0)
        return;
      GraphicCategory.ResolutionData screenResolution = this.m_fullScreenResolutions[this.m_displayDropdown.value];
      this.m_ratioDropdown.value = screenResolution.defaultRatioDropDownIndex;
      this.m_resolutionDropdown.value = screenResolution.defaultResolutionDropDownIndex;
    }

    protected void ApplyScreenResolution()
    {
      if (this.m_displayDropdown.value == -1)
        Log.Error("No display index selected", 593, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      else if (this.m_ratioDropdown.value == -1)
        Log.Error("No ratio index selected", 599, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      else if (this.m_resolutionDropdown.value == -1)
      {
        Log.Error("No resolution index selected", 605, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\UI\\Option\\Configuration\\GraphicCategory.cs");
      }
      else
      {
        this.m_currentFullScreenDropDownIndex = this.m_fullScreenDropdown.value;
        this.m_currentDisplayDropDownIndex = this.m_displayDropdown.value;
        this.m_currentRatioDropDownIndex = this.m_ratioDropdown.value;
        this.m_currentResolutionDropDownIndex = this.m_resolutionDropdown.value;
        bool fullscreen = this.m_currentFullScreenDropDownIndex == 0;
        Resolution resolution = (!fullscreen ? this.m_windowedResolutions : this.m_fullScreenResolutions[this.m_displayDropdown.value]).resolutionsByRatio[this.m_currentRatioDropDownIndex][this.m_currentResolutionDropDownIndex];
        ScreenManager.UpdateDisplayInformation();
        if (!this.CheckIfDataValid(fullscreen, this.m_displayDropdown.value, resolution))
        {
          this.UpdateResolutionData();
          this.InitializeScreenUI();
          this.UpdateApplyButton();
          Action<PopupInfo> onErrorPopupInfo = this.OnErrorPopupInfo;
          if (onErrorPopupInfo == null)
            return;
          onErrorPopupInfo(new PopupInfo()
          {
            title = (RawTextData) 72461,
            message = (RawTextData) 97607,
            buttons = new ButtonData[1]
            {
              new ButtonData((TextData) 27169)
            },
            selectedButton = 1,
            style = PopupStyle.Error
          });
        }
        else
        {
          if (fullscreen)
            this.StartCoroutine(ScreenManager.SetFullScreenMode(this.m_currentDisplayDropDownIndex, resolution));
          else
            this.StartCoroutine(ScreenManager.SetWindowedMode(resolution));
          this.UpdateDefaultResolutionButton();
        }
      }
    }

    private static int GetRatioIndex(int width, int height)
    {
      float num1 = (float) width / (float) height;
      if ((double) num1 - 1.25 < -0.004999999888241291 || 2.3333332538604736 - (double) num1 < -0.004999999888241291)
        return -1;
      int length = GraphicCategory.SupportedRatios.Length;
      float num2 = float.MaxValue;
      int ratioIndex1 = 0;
      for (int ratioIndex2 = 0; ratioIndex2 < length; ++ratioIndex2)
      {
        float num3 = Mathf.Abs(num1 - GraphicCategory.SupportedRatios[ratioIndex2]);
        if ((double) num3 < 1.4012984643248171E-45)
          return ratioIndex2;
        if ((double) num3 < (double) num2)
        {
          num2 = num3;
          ratioIndex1 = ratioIndex2;
        }
      }
      return ratioIndex1;
    }

    private struct ResolutionData
    {
      public DisplayInfo displayInfo;
      public int defaultRatioDropDownIndex;
      public int defaultResolutionDropDownIndex;
      public int defaultRatioIndex;
      public Resolution defaultResolution;
      public List<int> ratioIndex;
      public List<List<Resolution>> resolutionsByRatio;
    }
  }
}
