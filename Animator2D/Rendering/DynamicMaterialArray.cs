// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.DynamicMaterialArray
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Animations.Rendering
{
  internal sealed class DynamicMaterialArray
  {
    public const string UseColorEffectsKeyword = "USE_COLOR_EFFECTS";
    public const string SkipColorEffectsKeyword = "SKIP_COLOR_EFFECTS";
    private readonly Dictionary<int, Material> m_materials;
    private readonly Material m_sourceMaterial;
    private bool m_useColorEffects;
    private Material[] m_array = new Material[0];

    public DynamicMaterialArray(Material sourceMaterial)
    {
      this.m_sourceMaterial = sourceMaterial;
      this.m_materials = new Dictionary<int, Material>();
    }

    public void AddTexture(int id, Texture2D texture)
    {
      if (this.m_materials.ContainsKey(id))
        return;
      Material material = new Material(this.m_sourceMaterial)
      {
        mainTexture = (Texture) texture
      };
      if (this.m_useColorEffects)
      {
        material.DisableKeyword("USE_COLOR_EFFECTS");
        material.EnableKeyword("SKIP_COLOR_EFFECTS");
      }
      else
      {
        material.EnableKeyword("USE_COLOR_EFFECTS");
        material.DisableKeyword("SKIP_COLOR_EFFECTS");
      }
      this.m_materials.Add(id, material);
    }

    public void RemoveTexture(int id) => this.m_materials.Remove(id);

    public Material[] Get(List<int> textureIndexes)
    {
      int count = textureIndexes.Count;
      if (count != this.m_array.Length)
      {
        Array.Resize<Material>(ref this.m_array, count);
        for (int index = 0; index < count; ++index)
        {
          int textureIndex = textureIndexes[index];
          this.m_array[index] = this.m_materials[textureIndex];
        }
      }
      else
      {
        for (int index = 0; index < count; ++index)
        {
          int textureIndex = textureIndexes[index];
          this.m_array[index] = this.m_materials[textureIndex];
        }
      }
      return this.m_array;
    }

    public void UseColorEffects()
    {
      this.m_useColorEffects = true;
      foreach (Material material in this.m_materials.Values)
      {
        material.DisableKeyword("USE_COLOR_EFFECTS");
        material.EnableKeyword("SKIP_COLOR_EFFECTS");
      }
    }

    public void SkipColorEffects()
    {
      this.m_useColorEffects = false;
      foreach (Material material in this.m_materials.Values)
      {
        material.EnableKeyword("USE_COLOR_EFFECTS");
        material.DisableKeyword("SKIP_COLOR_EFFECTS");
      }
    }

    public void SetMaterial(Material material, Color color)
    {
      if (this.m_useColorEffects)
      {
        material.DisableKeyword("USE_COLOR_EFFECTS");
        material.EnableKeyword("SKIP_COLOR_EFFECTS");
      }
      else
      {
        material.EnableKeyword("USE_COLOR_EFFECTS");
        material.DisableKeyword("SKIP_COLOR_EFFECTS");
      }
      List<int> intList = new List<int>((IEnumerable<int>) this.m_materials.Keys);
      int count = intList.Count;
      for (int index = 0; index < count; ++index)
      {
        int key = intList[index];
        Material material1 = new Material(material)
        {
          color = color,
          mainTexture = this.m_materials[key].mainTexture
        };
        this.m_materials[key] = material1;
      }
    }

    public void SetColor(Color value)
    {
      foreach (Material material in this.m_materials.Values)
        material.color = value;
    }

    public void EnableKeyword(string keyword)
    {
      foreach (Material material in this.m_materials.Values)
        material.EnableKeyword(keyword);
    }

    public void DisableKeyword(string keyword)
    {
      foreach (Material material in this.m_materials.Values)
        material.DisableKeyword(keyword);
    }

    public bool IsKeywordEnabled(string keyword)
    {
      using (Dictionary<int, Material>.ValueCollection.Enumerator enumerator = this.m_materials.Values.GetEnumerator())
      {
        if (enumerator.MoveNext())
          return enumerator.Current.IsKeywordEnabled(keyword);
      }
      return false;
    }
  }
}
