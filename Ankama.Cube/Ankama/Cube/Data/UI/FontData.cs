// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.UI.FontData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.AssetManagement.AssetReferences;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Data.UI
{
  [Serializable]
  public class FontData
  {
    [UsedImplicitly]
    [SerializeField]
    private FontLanguage m_fontLanguage;
    [UsedImplicitly]
    [SerializeField]
    private AssetReference m_fontAsset;
    [UsedImplicitly]
    [SerializeField]
    private AssetReference m_styleMaterial;
    [UsedImplicitly]
    [SerializeField]
    [Range(8f, 300f)]
    private float m_defaultFontSize = 32f;
    [UsedImplicitly]
    [SerializeField]
    [Range(8f, 300f)]
    private float m_mobileFontSize = 32f;
    [UsedImplicitly]
    [SerializeField]
    private float m_characterSpacing;
    [UsedImplicitly]
    [SerializeField]
    private float m_wordSpacing;
    [UsedImplicitly]
    [SerializeField]
    private float m_lineSpacing;
    [UsedImplicitly]
    [SerializeField]
    private float m_paragraphSpacing;

    public AssetReference fontAssetReference => this.m_fontAsset;

    public AssetReference styleMaterialReference => this.m_styleMaterial;

    public float fontSize => this.m_defaultFontSize;

    public float characterSpacing => this.m_characterSpacing;

    public float wordSpacing => this.m_wordSpacing;

    public float lineSpacing => this.m_lineSpacing;

    public float paragraphSpacing => this.m_paragraphSpacing;
  }
}
