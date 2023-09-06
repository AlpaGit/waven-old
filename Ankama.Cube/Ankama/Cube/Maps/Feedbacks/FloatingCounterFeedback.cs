// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Feedbacks.FloatingCounterFeedback
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using Ankama.Cube.Maps.Objects;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.Feedbacks
{
  public class FloatingCounterFeedback : MonoBehaviour
  {
    private readonly List<FloatingCounterFloatingObject> m_floatingObjects = new List<FloatingCounterFloatingObject>(3);
    private IObjectWithCounterEffects m_parent;
    private FloatingCounterEffect m_effectDefinition;
    private int m_objectsCount;

    public int objectsCount => this.m_objectsCount;

    public FloatingCounterEffect effect => this.m_effectDefinition;

    public IEnumerator Launch(
      IObjectWithCounterEffects parent,
      FloatingCounterEffect effect,
      int count)
    {
      this.m_parent = parent;
      this.m_effectDefinition = effect;
      this.m_objectsCount = count;
      return this.SpawnCoroutine();
    }

    public IEnumerator SetCount(int count)
    {
      this.m_objectsCount = Math.Max(0, count);
      int count1 = this.m_floatingObjects.Count;
      if (count1 != this.m_objectsCount)
      {
        if (this.m_objectsCount < count1)
          yield return (object) this.RemoveCoroutine();
        else
          yield return (object) this.SpawnCoroutine();
        if (count == 0)
          this.m_parent.ClearFloatingCounterEffect();
      }
    }

    public IEnumerator FadeOut()
    {
      List<FloatingCounterFloatingObject> floatingObjects = this.m_floatingObjects;
      int count = floatingObjects.Count;
      if (count != 0)
      {
        float animationDuration = this.m_effectDefinition.clearAnimationDuration;
        for (int index = 0; index < count; ++index)
          floatingObjects[index].FadeOut(animationDuration);
        yield return (object) new WaitForTime(animationDuration);
      }
    }

    public void SetColorModifier(Color color)
    {
      List<FloatingCounterFloatingObject> floatingObjects = this.m_floatingObjects;
      int count = floatingObjects.Count;
      for (int index = 0; index < count; ++index)
        floatingObjects[index].SetColorModifier(color);
    }

    public void Clear()
    {
      this.DestroyAll();
      this.m_objectsCount = 0;
      this.m_parent = (IObjectWithCounterEffects) null;
      this.m_effectDefinition = (FloatingCounterEffect) null;
    }

    private void DestroyAll()
    {
      List<FloatingCounterFloatingObject> floatingObjects = this.m_floatingObjects;
      int count = floatingObjects.Count;
      for (int index = 0; index < count; ++index)
        UnityEngine.Object.Destroy((UnityEngine.Object) floatingObjects[index].gameObject);
      floatingObjects.Clear();
    }

    private void Update() => this.transform.Rotate(this.m_effectDefinition.rotation * Time.deltaTime, Space.World);

    private IEnumerator SpawnCoroutine()
    {
      FloatingCounterFeedback floatingCounterFeedback = this;
      FloatingCounterEffect effectDefinition = floatingCounterFeedback.m_effectDefinition;
      float radius = effectDefinition.radius;
      float height = effectDefinition.height;
      FloatingCounterFloatingObject floatingObject = effectDefinition.floatingObject;
      float animationDuration = effectDefinition.startingAnimationDuration;
      float num1 = 6.28318548f / (float) floatingCounterFeedback.m_objectsCount;
      Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
      floatingCounterFeedback.Reposition();
      for (int count = floatingCounterFeedback.m_floatingObjects.Count; count < floatingCounterFeedback.m_objectsCount; ++count)
      {
        float num2 = num1 * (float) count;
        position.x = Mathf.Cos(num2) * radius;
        position.z = Mathf.Sin(num2) * radius;
        position.y = height;
        FloatingCounterFloatingObject counterFloatingObject = UnityEngine.Object.Instantiate<FloatingCounterFloatingObject>(floatingObject, floatingCounterFeedback.transform);
        floatingCounterFeedback.m_floatingObjects.Add(counterFloatingObject);
        counterFloatingObject.Spawn(position, animationDuration, num2, radius);
      }
      UnityEngine.Object.Instantiate<VisualEffect>(effectDefinition.spawnFX, floatingCounterFeedback.transform.position + effectDefinition.spawnFXOffset, Quaternion.identity);
      effectDefinition.PlaySound(floatingCounterFeedback.transform);
      yield return (object) new WaitForTime(animationDuration);
    }

    private IEnumerator RemoveCoroutine()
    {
      WaitForTime wait = new WaitForTime(this.m_effectDefinition.endAnimationDuration);
      List<FloatingCounterFloatingObject> floatingObjects = this.m_floatingObjects;
      for (int i = floatingObjects.Count - 1; i >= this.m_objectsCount; --i)
      {
        floatingObjects[i].Clear();
        floatingObjects.RemoveAt(i);
        yield return (object) wait;
        wait.Reset();
      }
      yield return (object) null;
      this.Reposition();
    }

    private void Reposition()
    {
      List<FloatingCounterFloatingObject> floatingObjects = this.m_floatingObjects;
      int count = floatingObjects.Count;
      if (count <= 1)
        return;
      float repositionDuration = this.m_effectDefinition.repositionDuration;
      Ease repositionEase = this.m_effectDefinition.repositionEase;
      float num = 6.28318548f / (float) this.m_objectsCount;
      for (int index = 0; index < count; ++index)
        floatingObjects[index].Reposition(num * (float) index, repositionDuration, repositionEase);
    }

    public void ChangeVisual(FloatingCounterEffect effect)
    {
      if ((UnityEngine.Object) effect == (UnityEngine.Object) this.m_effectDefinition)
        return;
      this.m_effectDefinition = effect;
      if (this.m_objectsCount <= 0)
        return;
      this.DestroyAll();
      this.StartCoroutine(this.SpawnCoroutine());
    }
  }
}
