// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Rendering.CustomisableRenderState
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System.Runtime.InteropServices;

namespace Ankama.Animations.Rendering
{
  [StructLayout(LayoutKind.Sequential, Size = 40)]
  internal struct CustomisableRenderState
  {
    public float m00;
    public float m01;
    public float m03;
    public float m10;
    public float m11;
    public float m13;
    public float multiplicativeColor;
    public float additiveColor;
    public short spriteIndex;
    public short customisationIndex;
    public byte alpha;

    public unsafe int Compute(byte[] animationData, int dataPosition)
    {
      int num = (int) animationData[dataPosition];
      if ((num & 2) != 0)
        this.alpha = animationData[dataPosition + 1];
      dataPosition += 2;
      if ((num & 33) != 0)
      {
        fixed (byte* numPtr = &animationData[dataPosition])
        {
          this.spriteIndex = *(short*) numPtr;
          this.customisationIndex = *(short*) (numPtr + 2);
        }
        dataPosition += 4;
      }
      if ((num & 4) != 0)
      {
        fixed (byte* numPtr = &animationData[dataPosition])
          this.multiplicativeColor = *(float*) numPtr;
        dataPosition += 4;
      }
      if ((num & 8) != 0)
      {
        fixed (byte* numPtr = &animationData[dataPosition])
          this.additiveColor = *(float*) numPtr;
        dataPosition += 4;
      }
      if ((num & 16) != 0)
      {
        fixed (byte* numPtr = &animationData[dataPosition])
        {
          this.m00 = *(float*) numPtr;
          this.m01 = *(float*) (numPtr + 4);
          this.m03 = *(float*) (numPtr + 8);
          this.m10 = *(float*) (numPtr + 12);
          this.m11 = *(float*) (numPtr + 16);
          this.m13 = *(float*) (numPtr + 20);
        }
        dataPosition += 24;
      }
      return dataPosition;
    }

    public static int Advance(byte[] animationData, int dataPosition)
    {
      int num = (int) animationData[dataPosition];
      dataPosition += 2;
      if ((num & 33) != 0)
        dataPosition += 4;
      if ((num & 4) != 0)
        dataPosition += 4;
      if ((num & 8) != 0)
        dataPosition += 4;
      if ((num & 16) != 0)
        dataPosition += 24;
      return dataPosition;
    }

    public void Reset()
    {
      this.m00 = 1f;
      this.m01 = 0.0f;
      this.m03 = 0.0f;
      this.m10 = 0.0f;
      this.m11 = 1f;
      this.m13 = 0.0f;
      this.spriteIndex = (short) -1;
      this.alpha = byte.MaxValue;
      this.multiplicativeColor = 16711422f;
      this.additiveColor = 8355711f;
    }
  }
}
