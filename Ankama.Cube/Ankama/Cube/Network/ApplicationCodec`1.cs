// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Network.ApplicationCodec`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Network
{
  public interface ApplicationCodec<T>
  {
    bool TrySerialize(T t, out byte[] result);

    bool TryDeserialize(byte[] data, out T result);
  }
}
