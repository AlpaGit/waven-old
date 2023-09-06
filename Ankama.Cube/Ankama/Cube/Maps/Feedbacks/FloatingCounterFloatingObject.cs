// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Feedbacks.FloatingCounterFloatingObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Maps.Feedbacks
{
  [ExecuteInEditMode]
  public class FloatingCounterFloatingObject : MonoBehaviour
  {
    private static readonly int s_colorPropertyId = Shader.PropertyToID("_Color");
    [SerializeField]
    private GameObject m_castFX;
    [SerializeField]
    private AnimationCurve m_spawnAnimationCurve;
    [SerializeField]
    private float m_oscillationSpeed;
    [SerializeField]
    private float m_oscillationAmplitude;
    [HideInInspector]
    [SerializeField]
    private Renderer[] m_renderers;
    private float m_oscillationPhase;
    private float m_radius;
    private float m_positionY;
    private MaterialPropertyBlock m_colorModifierPropertyBlock;

    public void Spawn(Vector3 position, float duration, float phase, float radius)
    {
      this.m_oscillationPhase = phase;
      this.m_radius = radius;
      this.m_positionY = position.y;
      this.transform.localRotation = Quaternion.LookRotation(position.normalized with
      {
        y = 0.0f
      }, new Vector3(0.0f, 1f, 0.0f));
      this.StartCoroutine(this.SpawnCoroutine(position, duration));
    }

    public void Reposition(float angle, float duration, Ease ease) => DOVirtual.Float(this.m_oscillationPhase, angle, duration, new TweenCallback<float>(this.RepositionTweenCallback)).SetEase<Tweener>(ease);

    public void FadeOut(float duration) => DOVirtual.Float(1f, 0.0f, duration, new TweenCallback<float>(this.FadeOutTweenCallback));

    public void SetColorModifier(Color color)
    {
      MaterialPropertyBlock properties = this.m_colorModifierPropertyBlock;
      if (properties == null)
      {
        properties = new MaterialPropertyBlock();
        this.m_colorModifierPropertyBlock = properties;
      }
      properties.SetColor(FloatingCounterFloatingObject.s_colorPropertyId, color);
      Renderer[] renderers = this.m_renderers;
      int length = renderers.Length;
      for (int index = 0; index < length; ++index)
      {
        Renderer renderer = renderers[index];
        if ((Object) null != (Object) renderer)
        {
          SpriteRenderer spriteRenderer = renderer as SpriteRenderer;
          if ((Object) null != (Object) spriteRenderer)
            spriteRenderer.color = color;
          else
            renderer.SetPropertyBlock(properties);
        }
      }
    }

    public void Clear()
    {
      Object.Instantiate<GameObject>(this.m_castFX, this.transform.position, Quaternion.identity);
      Object.Destroy((Object) this.gameObject);
    }

    private IEnumerator SpawnCoroutine(Vector3 position, float duration)
    {
      FloatingCounterFloatingObject counterFloatingObject = this;
      Transform selfTransform = counterFloatingObject.transform;
      WaitForEndOfFrame wait = new WaitForEndOfFrame();
      Vector3 startPosition = position + position.normalized * 3f;
      Vector3 startScale = new Vector3(0.0f, 0.0f, 0.0f);
      Vector3 endScale = new Vector3(1f, 1f, 1f);
      for (float f = 0.0f; (double) f < 1.0; f += Time.deltaTime / duration)
      {
        selfTransform.localPosition = Vector3.LerpUnclamped(startPosition, position, counterFloatingObject.m_spawnAnimationCurve.Evaluate(f));
        selfTransform.localScale = Vector3.LerpUnclamped(startScale, endScale, f);
        yield return (object) wait;
      }
      selfTransform.localPosition = position;
      selfTransform.localScale = endScale;
      if (!Mathf.Approximately(counterFloatingObject.m_oscillationAmplitude, 0.0f))
      {
        while (true)
        {
          Vector3 localPosition = selfTransform.localPosition with
          {
            y = counterFloatingObject.m_positionY + Mathf.Sin(counterFloatingObject.m_oscillationSpeed * Time.time + counterFloatingObject.m_oscillationPhase) * counterFloatingObject.m_oscillationAmplitude
          };
          selfTransform.localPosition = localPosition;
          yield return (object) null;
        }
      }
    }

    private void RepositionTweenCallback(float phase)
    {
      this.m_oscillationPhase = phase;
      this.transform.localPosition = new Vector3(Mathf.Cos(phase) * this.m_radius, this.m_positionY, Mathf.Sin(phase) * this.m_radius);
      this.transform.localRotation = Quaternion.LookRotation(this.transform.localPosition.normalized with
      {
        y = 0.0f
      }, new Vector3(0.0f, 1f, 0.0f));
    }

    private void FadeOutTweenCallback(float alpha)
    {
      Color color = new Color(1f, 1f, 1f, alpha);
      Renderer[] renderers = this.m_renderers;
      int length = renderers.Length;
      for (int index = 0; index < length; ++index)
        renderers[index].material.color = color;
    }
  }
}
