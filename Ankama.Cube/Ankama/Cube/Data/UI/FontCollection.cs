// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.FontCollection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using Ankama.Cube.UI.Components;
using Ankama.Utilities;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ankama.Cube.Data.UI
{
  [CreateAssetMenu(fileName = "New Font Collection", menuName = "Waven/Data/UI/Font Collection")]
  public class FontCollection : ScriptableObject
  {
    public const string ResourceFontCollectionFolder = "GameData/UI/Fonts";
    public const string DefaultFontCollectionName = "Default";
    [UsedImplicitly]
    [HideInInspector]
    [SerializeField]
    private string m_guid;
    [UsedImplicitly]
    [SerializeField]
    private FontCollection.FontDataDictionary m_data = new FontCollection.FontDataDictionary();
    private readonly List<AbstractTextField> m_registeredTextFields = new List<AbstractTextField>();
    [NonSerialized]
    private int m_referenceCount;
    [NonSerialized]
    private FontData m_fontData;
    [NonSerialized]
    private TMP_FontAsset m_fontAsset;
    [NonSerialized]
    private Material m_styleMaterial;

    [NotNull]
    public string identifier => this.m_guid;

    [NotNull]
    public TMP_FontAsset fontAsset
    {
      get
      {
        if (this.m_referenceCount != 0)
          return this.m_fontAsset;
        Log.Error("Font collection named '" + this.name + "' has not been loaded before use.", 157, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        return TMP_FontAsset.defaultFontAsset;
      }
    }

    [CanBeNull]
    public Material styleMaterial
    {
      get
      {
        if (this.m_referenceCount != 0)
          return this.m_styleMaterial;
        Log.Error("Font collection named '" + this.name + "' has not been loaded before use.", 204, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        return (Material) null;
      }
    }

    [CanBeNull]
    public FontData fontData
    {
      get
      {
        if (this.m_referenceCount != 0)
          return this.m_fontData;
        Log.Error("Font collection named '" + this.name + "' has not been loaded before use.", 233, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        return (FontData) null;
      }
    }

    public void Load()
    {
      if (this.m_referenceCount > 0)
      {
        ++this.m_referenceCount;
      }
      else
      {
        FontLanguage currentFontLanguage = RuntimeData.currentFontLanguage;
        FontData fontData;
        if (!this.m_data.TryGetValue(currentFontLanguage, out fontData))
        {
          Log.Error(string.Format("Font collection named '{0}' doesn't have any font data for font language '{1}'.", (object) this.name, (object) currentFontLanguage), 256, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        }
        else
        {
          AssetReference fontAssetReference = fontData.fontAssetReference;
          if (!fontAssetReference.hasValue)
          {
            Log.Error(string.Format("Font collection named '{0}' doesn't have a font setup for font language '{1}'.", (object) this.name, (object) currentFontLanguage), 263, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
          }
          else
          {
            TMP_FontAsset tmpFontAsset = fontAssetReference.LoadFromResources<TMP_FontAsset>();
            if ((UnityEngine.Object) null == (UnityEngine.Object) tmpFontAsset)
            {
              Log.Error(string.Format("Font collection named '{0}' failed to load font for font language '{1}'.", (object) this.name, (object) currentFontLanguage), 270, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
            }
            else
            {
              AssetReference materialReference = fontData.styleMaterialReference;
              Material material = materialReference.hasValue ? materialReference.LoadFromResources<Material>() : (Material) null;
              RuntimeData.CultureCodeChanged += new RuntimeData.CultureCodeChangedEventHandler(this.CultureCodeChanged);
              this.m_fontData = fontData;
              this.m_fontAsset = tmpFontAsset;
              this.m_styleMaterial = material;
              this.m_referenceCount = 1;
            }
          }
        }
      }
    }

    public IEnumerator LoadAsync()
    {
      FontCollection fontCollection = this;
      if (fontCollection.m_referenceCount > 0)
      {
        ++fontCollection.m_referenceCount;
      }
      else
      {
        FontLanguage fontLanguage = RuntimeData.currentFontLanguage;
        FontData currentFontData;
        if (!fontCollection.m_data.TryGetValue(fontLanguage, out currentFontData))
        {
          Log.Error(string.Format("Font collection named '{0}' doesn't have any font data for font language '{1}'.", (object) fontCollection.name, (object) fontLanguage), 300, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        }
        else
        {
          AssetReference fontAssetReference = currentFontData.fontAssetReference;
          if (!fontAssetReference.hasValue)
          {
            Log.Error(string.Format("Font collection named '{0}' doesn't have a font setup for font language '{1}'.", (object) fontCollection.name, (object) fontLanguage), 307, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
          }
          else
          {
            AssetReferenceRequest<TMP_FontAsset> loadRequest = fontAssetReference.LoadFromResourcesAsync<TMP_FontAsset>();
            while (!loadRequest.isDone)
              yield return (object) null;
            TMP_FontAsset asset = loadRequest.asset;
            if ((UnityEngine.Object) null == (UnityEngine.Object) asset)
            {
              Log.Error(string.Format("Font collection named '{0}' failed to load font for font language '{1}'.", (object) fontCollection.name, (object) fontLanguage), 320, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
            }
            else
            {
              AssetReference materialReference = currentFontData.styleMaterialReference;
              Material material;
              if (materialReference.hasValue)
              {
                AssetReferenceRequest<Material> styleLoadRequest = materialReference.LoadFromResourcesAsync<Material>();
                while (!styleLoadRequest.isDone)
                  yield return (object) null;
                material = styleLoadRequest.asset;
                if ((UnityEngine.Object) null == (UnityEngine.Object) material)
                  Log.Warning(string.Format("Font collection named '{0}' failed to load style material for font language '{1}'.", (object) fontCollection.name, (object) fontLanguage), 337, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
                styleLoadRequest = (AssetReferenceRequest<Material>) null;
              }
              else
                material = (Material) null;
              RuntimeData.CultureCodeChanged += new RuntimeData.CultureCodeChangedEventHandler(fontCollection.CultureCodeChanged);
              fontCollection.m_fontData = currentFontData;
              fontCollection.m_fontAsset = asset;
              fontCollection.m_styleMaterial = material;
              fontCollection.m_referenceCount = 1;
            }
          }
        }
      }
    }

    public void Unload()
    {
      if (this.m_referenceCount == 0)
      {
        Log.Error("Tried to unload font collection named '" + this.name + "' but it is not loaded.", 359, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
      }
      else
      {
        --this.m_referenceCount;
        if (this.m_referenceCount > 0)
          return;
        RuntimeData.CultureCodeChanged -= new RuntimeData.CultureCodeChangedEventHandler(this.CultureCodeChanged);
        this.m_styleMaterial = (Material) null;
        this.m_fontAsset = (TMP_FontAsset) null;
        this.m_fontData = (FontData) null;
      }
    }

    public void RegisterTextField([NotNull] AbstractTextField textField)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) textField)
        throw new ArgumentNullException(nameof (textField));
      this.m_registeredTextFields.Add(textField);
    }

    public void UnregisterTextField(AbstractTextField textField)
    {
      if ((UnityEngine.Object) null == (UnityEngine.Object) textField)
        throw new ArgumentNullException(nameof (textField));
      this.m_registeredTextFields.Remove(textField);
    }

    private void CultureCodeChanged(CultureCode cultureCode, FontLanguage fontLanguage)
    {
      if (this.m_referenceCount == 0)
        return;
      this.ChangeFontLanguage(fontLanguage);
      foreach (AbstractTextField registeredTextField in this.m_registeredTextFields)
      {
        if ((UnityEngine.Object) null == (UnityEngine.Object) registeredTextField)
          Log.Warning("Found a registered null TextField instance.", 429, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        else
          registeredTextField.RefreshText();
      }
    }

    private void ChangeFontLanguage(FontLanguage fontLanguage)
    {
      FontData fontData;
      if (!this.m_data.TryGetValue(fontLanguage, out fontData))
      {
        Log.Error(string.Format("Font collection named '{0}' doesn't have any font data for font language '{1}'.", (object) this.name, (object) fontLanguage), 444, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
      }
      else
      {
        AssetReference fontAssetReference = fontData.fontAssetReference;
        if (!fontAssetReference.hasValue)
        {
          Log.Error(string.Format("Font collection named '{0}' doesn't have a font setup for font language '{1}'.", (object) this.name, (object) fontLanguage), 451, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
        }
        else
        {
          TMP_FontAsset tmpFontAsset = fontAssetReference.LoadFromResources<TMP_FontAsset>();
          if ((UnityEngine.Object) null == (UnityEngine.Object) tmpFontAsset)
          {
            Log.Error(string.Format("Font collection named '{0}' failed to load font for font language '{1}'.", (object) this.name, (object) fontLanguage), 458, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\UI\\FontCollection.cs");
          }
          else
          {
            AssetReference materialReference = fontData.styleMaterialReference;
            Material material = materialReference.hasValue ? materialReference.LoadFromResources<Material>() : (Material) null;
            if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_styleMaterial)
              UnityEngine.Resources.UnloadAsset((UnityEngine.Object) this.m_styleMaterial);
            if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_fontAsset)
              UnityEngine.Resources.UnloadAsset((UnityEngine.Object) this.m_fontAsset);
            this.m_fontData = fontData;
            this.m_fontAsset = tmpFontAsset;
            this.m_styleMaterial = material;
          }
        }
      }
    }

    [Serializable]
    private class FontDataDictionary : SerializableDictionary<FontLanguage, FontData>
    {
      public FontDataDictionary()
        : base((IEqualityComparer<FontLanguage>) FontLanguageComparer.instance)
      {
      }
    }
  }
}
