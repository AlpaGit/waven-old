// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.AnimatedObjectDefinition
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Animations
{
  [PublicAPI]
  public sealed class AnimatedObjectDefinition : ScriptableObject
  {
    [PublicAPI]
    [SerializeField]
    public Material material;
    [PublicAPI]
    [SerializeField]
    public string assetBundleName;
    [PublicAPI]
    [SerializeField]
    public string defaultAnimationName;
    [PublicAPI]
    [SerializeField]
    public bool defaultAnimationLoops;
    [UsedImplicitly]
    [SerializeField]
    internal int defaultFrameRate;
    [UsedImplicitly]
    [SerializeField]
    internal short maxNodeCount;
    [UsedImplicitly]
    [SerializeField]
    internal string[] exposedNodeNames;
    [UsedImplicitly]
    [SerializeField]
    internal Graphic[] graphics;
    [UsedImplicitly]
    [SerializeField]
    internal Animation[] animations;

    [PublicAPI]
    public int animationCount => this.animations.Length;

    [PublicAPI]
    [NotNull]
    public string GetAnimationName(int index) => this.animations[index].name;

    internal bool GetUniqueMaterialInstance(
      Material overrideMaterial,
      out Material rendererMaterial)
    {
      Texture texture = (Texture) null;
      int length = this.graphics.Length;
      int index;
      for (index = 0; index < length; ++index)
      {
        Graphic graphic = this.graphics[index];
        if (graphic != null)
        {
          texture = (Texture) graphic.atlas;
          if ((Object) null != (Object) texture)
            break;
        }
      }
      for (; index < length; ++index)
      {
        Graphic graphic = this.graphics[index];
        if (graphic != null && (Object) texture != (Object) graphic.atlas)
        {
          if ((Object) null == (Object) overrideMaterial)
          {
            if ((Object) null == (Object) this.material)
            {
              Debug.LogWarning((object) ("[Animator2D] No suitable material is available for definition named '" + this.name + "'."));
              rendererMaterial = new Material(Shader.Find("Unlit/Transparent"));
            }
            else
              rendererMaterial = this.material;
          }
          else
            rendererMaterial = overrideMaterial;
          return false;
        }
      }
      if ((Object) null == (Object) overrideMaterial)
      {
        if ((Object) null == (Object) this.material)
        {
          Debug.LogWarning((object) ("[Animator2D] No suitable material is available for definition named '" + this.name + "'."));
          rendererMaterial = new Material(Shader.Find("Unlit/Transparent"))
          {
            mainTexture = (Object) null != (Object) texture ? texture : (Texture) Texture2D.whiteTexture
          };
        }
        else
          rendererMaterial = new Material(this.material)
          {
            mainTexture = (Object) null != (Object) texture ? texture : (Texture) Texture2D.whiteTexture
          };
      }
      else
        rendererMaterial = new Material(overrideMaterial)
        {
          mainTexture = (Object) null != (Object) texture ? texture : (Texture) Texture2D.whiteTexture
        };
      return true;
    }

    internal Animation GetDefaultAnimation()
    {
      int length = this.animations.Length;
      if (length == 0)
        return (Animation) null;
      if (string.IsNullOrEmpty(this.defaultAnimationName))
        return this.animations[0];
      for (int index = 0; index < length; ++index)
      {
        Animation animation = this.animations[index];
        if (animation.name.Equals(this.defaultAnimationName))
          return animation;
      }
      return (Animation) null;
    }

    internal Animation GetAnimation(string animationName)
    {
      int length = this.animations.Length;
      for (int index = 0; index < length; ++index)
      {
        Animation animation = this.animations[index];
        if (animation.name.Equals(animationName))
          return animation;
      }
      return (Animation) null;
    }
  }
}
