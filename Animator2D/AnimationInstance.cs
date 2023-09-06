// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.AnimationInstance
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;
using System.Text;

namespace Ankama.Animations
{
  internal sealed class AnimationInstance
  {
    public readonly string guid;
    public readonly ushort frameCount;
    public readonly ushort nodeCount;
    public readonly ushort labelCount;
    public readonly Animation.NodeState combinedNodeState;
    public readonly int[] frameDataPositions;
    public readonly byte[] bytes;
    public readonly AnimationLabel[] labels;
    public int referenceCount = 1;

    public AnimationInstance(string guid, byte[] bytes)
    {
      int num1 = 0;
      this.frameCount = BitConverter.ToUInt16(bytes, 0);
      this.nodeCount = BitConverter.ToUInt16(bytes, 2);
      this.labelCount = BitConverter.ToUInt16(bytes, 4);
      this.combinedNodeState = (Animation.NodeState) bytes[6];
      int startIndex = num1 + 8;
      this.labels = new AnimationLabel[(int) this.labelCount];
      if (this.labelCount > (ushort) 0)
      {
        for (int index1 = 0; index1 < (int) this.labelCount; ++index1)
        {
          int uint16 = (int) BitConverter.ToUInt16(bytes, startIndex);
          byte count = bytes[startIndex + 2];
          int index2 = startIndex + 3;
          string label = Encoding.UTF8.GetString(bytes, index2, (int) count);
          int num2 = index2 + (int) count;
          startIndex = num2 + num2 % 2;
          this.labels[index1] = new AnimationLabel(uint16, label);
        }
        startIndex += startIndex % 4;
      }
      this.frameDataPositions = new int[(int) this.frameCount];
      for (int index = 0; index < (int) this.frameCount; ++index)
      {
        this.frameDataPositions[index] = BitConverter.ToInt32(bytes, startIndex);
        startIndex += 4;
      }
      this.guid = guid;
      this.bytes = bytes;
    }
  }
}
