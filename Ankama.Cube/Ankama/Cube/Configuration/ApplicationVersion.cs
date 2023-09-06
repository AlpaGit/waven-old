// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Configuration.ApplicationVersion
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;

namespace Ankama.Cube.Configuration
{
  public struct ApplicationVersion
  {
    public readonly int major;
    public readonly int minor;
    public readonly int patch;
    public readonly int build;
    public readonly string label;

    public string majorMinorPatch => string.Format("{0}.{1}.{2}", (object) this.major, (object) this.minor, (object) this.patch);

    public ApplicationVersion(string version)
    {
      string[] strArray1 = version.Split('-');
      if (strArray1.Length == 0 || strArray1.Length > 2)
        throw new ArgumentException("'" + version + "' is not a valid version number.");
      this.label = strArray1.Length < 2 ? string.Empty : strArray1[1];
      string[] strArray2 = strArray1[0].Split('.');
      if (strArray2.Length > 4 || strArray2.Length == 0)
        throw new ArgumentException("'" + version + "' is not a valid version number.");
      if (!int.TryParse(strArray2[0], out this.major))
        throw new ArgumentException("'" + version + "' is not a valid version number.");
      if (strArray2.Length > 1)
      {
        if (!int.TryParse(strArray2[1], out this.minor))
          throw new ArgumentException("'" + version + "' is not a valid version number.");
      }
      else
        this.minor = 0;
      if (strArray2.Length > 2)
      {
        if (!int.TryParse(strArray2[2], out this.patch))
          throw new ArgumentException("'" + version + "' is not a valid version number.");
      }
      else
        this.patch = 0;
      if (strArray2.Length > 3)
      {
        if (!int.TryParse(strArray2[3], out this.build))
          throw new ArgumentException("'" + version + "' is not a valid version number.");
      }
      else
        this.build = 0;
    }

    public bool Matches(ApplicationVersion other, ApplicationVersion.MatchMask mask)
    {
      if (mask != ApplicationVersion.MatchMask.None)
      {
        if (mask != ApplicationVersion.MatchMask.Patch)
          throw new ArgumentOutOfRangeException(nameof (mask), (object) mask, (string) null);
        return this.major == other.major && this.minor == other.minor;
      }
      return this.major == other.major && this.minor == other.minor && this.patch == other.patch;
    }

    public enum MatchMask
    {
      None,
      Patch,
    }
  }
}
