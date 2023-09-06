// Decompiled with JetBrains decompiler
// Type: Ankama.AssetManagement.StateManager
// Assembly: AssetManager, Version=3.4.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FBDDDF1F-801F-467D-A5C9-95D72C757F25
// Assembly location: E:\WAVEN\Waven_Data\Managed\AssetManager.dll

using Ankama.AssetManagement.InputManagement;
using Ankama.AssetManagement.StateManagement;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Ankama.AssetManagement
{
  [PublicAPI]
  public static class StateManager
  {
    [PublicAPI]
    public const string DefaultLayerName = "default";
    internal static StateManagerCallbackSource callbackSource;
    private static readonly Dictionary<string, StateLayer> s_layers = new Dictionary<string, StateLayer>((IEqualityComparer<string>) StringComparer.Ordinal);
    private static readonly List<StateLayer> s_layersBuffer = new List<StateLayer>();
    private static readonly List<StateLayer> s_activeInputLayerStack = new List<StateLayer>();
    private static readonly List<StateLayer> s_activeInputLayerStackBuffer = new List<StateLayer>();
    private static readonly List<InputDefinition> s_inputDefinitions = new List<InputDefinition>();
    private static readonly List<InputState> s_inputStateBuffer = new List<InputState>();
    private static bool s_updatingInputStates;

    internal static void Initialize()
    {
      if ((UnityEngine.Object) null != (UnityEngine.Object) StateManager.callbackSource)
        return;
      StateManager.callbackSource = new GameObject("StateManagerCallbackSource", new System.Type[1]
      {
        typeof (StateManagerCallbackSource)
      }).GetComponent<StateManagerCallbackSource>();
      StateLayer stateLayer = new StateLayer("default", StateManager.s_activeInputLayerStack.Count);
      StateManager.s_layers.Add("default", stateLayer);
      StateManager.s_activeInputLayerStack.Add(stateLayer);
    }

    internal static IEnumerator Update()
    {
      List<InputState> inputStateBuffer = StateManager.s_inputStateBuffer;
      List<StateLayer> activeInputLayerStackBuffer = StateManager.s_activeInputLayerStackBuffer;
      List<StateLayer> layersBuffer = StateManager.s_layersBuffer;
      do
      {
        StateManager.s_updatingInputStates = true;
        int count1 = StateManager.s_inputDefinitions.Count;
        for (int index = 0; index < count1; ++index)
        {
          InputDefinition inputDefinition = StateManager.s_inputDefinitions[index];
          try
          {
            InputState.State inputState = inputDefinition.GetInputState();
            if (inputState != InputState.State.None)
              inputStateBuffer.Add(new InputState(inputDefinition.id, inputState));
          }
          catch (Exception ex)
          {
            UnityEngine.Debug.LogException(ex);
          }
        }
        StateManager.s_updatingInputStates = false;
        int count2 = StateManager.s_activeInputLayerStack.Count;
        for (int index = 0; index < count2; ++index)
          activeInputLayerStackBuffer.Add(StateManager.s_activeInputLayerStack[index]);
        int count3 = inputStateBuffer.Count;
        for (int index1 = 0; index1 < count3; ++index1)
        {
          InputState inputState = inputStateBuffer[index1];
          int index2 = activeInputLayerStackBuffer.Count - 1;
          while (index2 >= 0 && !activeInputLayerStackBuffer[index2].ProcessInput(inputState))
            --index2;
        }
        activeInputLayerStackBuffer.Clear();
        inputStateBuffer.Clear();
        foreach (StateLayer stateLayer in StateManager.s_layers.Values)
          layersBuffer.Add(stateLayer);
        int count4 = layersBuffer.Count;
        for (int index = 0; index < count4; ++index)
          layersBuffer[index].Execute();
        layersBuffer.Clear();
        yield return (object) null;
      }
      while ((UnityEngine.Object) null != (UnityEngine.Object) StateManager.callbackSource);
    }

    [PublicAPI]
    [NotNull]
    public static StateLayer AddLayer([NotNull] string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (StateManager.s_layers.ContainsKey(name))
        throw new ArgumentException("[StateManager] A layer named '" + name + "' already exists.");
      StateLayer stateLayer = new StateLayer(name, StateManager.s_activeInputLayerStack.Count);
      StateManager.s_layers.Add(name, stateLayer);
      StateManager.s_activeInputLayerStack.Add(stateLayer);
      return stateLayer;
    }

    [PublicAPI]
    public static StateLayer GetDefaultLayer() => StateManager.s_layers["default"];

    [PublicAPI]
    public static bool TryGetLayer([NotNull] string name, out StateLayer stateLayer) => name != null ? StateManager.s_layers.TryGetValue(name, out stateLayer) : throw new ArgumentNullException(nameof (name));

    [PublicAPI]
    public static void RemoveLayer([NotNull] string name)
    {
      if (name.Equals("default"))
        throw new ArgumentException("[StateManager] The default layer cannot be removed.");
      StateLayer removedLayer;
      if (!StateManager.s_layers.TryGetValue(name, out removedLayer))
        throw new ArgumentException("[StateManager] The layer named '" + name + "' does not exist.");
      StateManager.s_layers.Remove(name);
      for (int index = StateManager.s_activeInputLayerStack.Count - 1; index >= 0; --index)
      {
        StateLayer activeInputLayer = StateManager.s_activeInputLayerStack[index];
        if (activeInputLayer == removedLayer)
        {
          StateManager.s_activeInputLayerStack.RemoveAt(index);
          break;
        }
        --activeInputLayer.index;
      }
      removedLayer.ClearChildState();
      StateManager.callbackSource.StartCoroutine(StateManager.RemoveLayerRoutine(removedLayer));
    }

    private static IEnumerator RemoveLayerRoutine(StateLayer removedLayer)
    {
      while (removedLayer.Execute())
        yield return (object) null;
    }

    public static void RegisterInputDefinition([NotNull] InputDefinition inputDefinition)
    {
      if (inputDefinition == null)
        throw new ArgumentNullException(nameof (inputDefinition));
      if (StateManager.s_updatingInputStates)
        throw new Exception("StateManager.RegisterInputDefinition cannot be called during the execution of InputDefinition.GetInputState.");
      int priority = inputDefinition.priority;
      List<InputDefinition> inputDefinitions = StateManager.s_inputDefinitions;
      for (int index = inputDefinitions.Count - 1; index >= 0; --index)
      {
        if (inputDefinitions[index].id == inputDefinition.id)
        {
          inputDefinitions.RemoveAt(index);
          break;
        }
      }
      for (int index = inputDefinitions.Count - 1; index >= 0; --index)
      {
        if (inputDefinitions[index].priority >= priority)
        {
          inputDefinitions.Insert(index + 1, inputDefinition);
          return;
        }
      }
      inputDefinitions.Insert(0, inputDefinition);
    }

    public static void UnregisterInputDefinition([NotNull] InputDefinition inputDefinition)
    {
      if (inputDefinition == null)
        throw new ArgumentNullException(nameof (inputDefinition));
      if (StateManager.s_updatingInputStates)
        throw new Exception("StateManager.UnregisterInputDefinition cannot be called during the execution of InputDefinition.GetInputState.");
      List<InputDefinition> inputDefinitions = StateManager.s_inputDefinitions;
      for (int index = inputDefinitions.Count - 1; index >= 0; --index)
      {
        if (inputDefinitions[index].id == inputDefinition.id)
        {
          inputDefinitions.RemoveAt(index);
          break;
        }
      }
    }

    public static void UnregisterInputDefinition(int id)
    {
      if (StateManager.s_updatingInputStates)
        throw new Exception("StateManager.UnregisterInputDefinition cannot be called during the execution of InputDefinition.GetInputState.");
      List<InputDefinition> inputDefinitions = StateManager.s_inputDefinitions;
      for (int index = inputDefinitions.Count - 1; index >= 0; --index)
      {
        if (inputDefinitions[index].id == id)
        {
          inputDefinitions.RemoveAt(index);
          break;
        }
      }
    }

    [PublicAPI]
    public static void SetActiveInputLayer([NotNull] StateLayer stateLayer)
    {
      if (stateLayer == null)
        throw new ArgumentNullException(nameof (stateLayer));
      List<StateLayer> activeInputLayerStack = StateManager.s_activeInputLayerStack;
      int num = activeInputLayerStack.Count - 1;
      if (stateLayer.index == num)
        return;
      for (int index = num; index >= 0; --index)
      {
        StateLayer stateLayer1 = activeInputLayerStack[index];
        if (stateLayer1 == stateLayer)
        {
          activeInputLayerStack.RemoveAt(index);
          break;
        }
        --stateLayer1.index;
      }
      stateLayer.index = num;
      activeInputLayerStack.Add(stateLayer);
    }

    [PublicAPI]
    public static StateLayer GetActiveInputLayer()
    {
      List<StateLayer> activeInputLayerStack = StateManager.s_activeInputLayerStack;
      int count = activeInputLayerStack.Count;
      return count == 0 ? (StateLayer) null : activeInputLayerStack[count - 1];
    }

    [PublicAPI]
    public static void DiscardInputLayer([NotNull] StateLayer stateLayer)
    {
      if (stateLayer == null)
        throw new ArgumentNullException(nameof (stateLayer));
      if (stateLayer.index == 0)
        return;
      List<StateLayer> activeInputLayerStack = StateManager.s_activeInputLayerStack;
      int count = activeInputLayerStack.Count;
      for (int index = 0; index < count; ++index)
      {
        StateLayer stateLayer1 = activeInputLayerStack[index];
        if (stateLayer1 == stateLayer)
        {
          activeInputLayerStack.RemoveAt(index);
          break;
        }
        ++stateLayer1.index;
      }
      stateLayer.index = 0;
      activeInputLayerStack.Insert(0, stateLayer);
    }

    [PublicAPI]
    public static void SimulateInput(InputDefinition inputDefinition, InputState.State state) => StateManager.SimulateInput(inputDefinition.id, state);

    [PublicAPI]
    public static void SimulateInput(int id, InputState.State state)
    {
      InputState inputState = new InputState(id, state);
      List<StateLayer> layerStackBuffer = StateManager.s_activeInputLayerStackBuffer;
      if (layerStackBuffer.Count == 0)
      {
        int count = StateManager.s_activeInputLayerStack.Count;
        for (int index = 0; index < count; ++index)
          layerStackBuffer.Add(StateManager.s_activeInputLayerStack[index]);
        int index1 = layerStackBuffer.Count - 1;
        while (index1 >= 0 && !layerStackBuffer[index1].ProcessInput(inputState))
          --index1;
        layerStackBuffer.Clear();
      }
      else
      {
        int index = layerStackBuffer.Count - 1;
        while (index >= 0 && !layerStackBuffer[index].ProcessInput(inputState))
          --index;
      }
    }

    [Conditional("UNITY_EDITOR")]
    private static void NotifyStateLayerChanged()
    {
    }
  }
}
