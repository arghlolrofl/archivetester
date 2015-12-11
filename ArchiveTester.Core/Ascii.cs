using System;
using System.Collections;
using System.Collections.Generic;

namespace ArchiveTester.Core
{
  public class Ascii
  {
    class AsciiRange { public int StartIndex; public int EndIndex; }

    static Dictionary<string, AsciiRange> Ranges = new Dictionary<string, AsciiRange>() {
      { "lower", new AsciiRange() { StartIndex = 97, EndIndex = 122 } },
      { "upper", new AsciiRange() { StartIndex = 65, EndIndex = 90 } },
      { "numbers", new AsciiRange() { StartIndex = 48, EndIndex = 57 } }
    };

    public static ArrayList Chars = new ArrayList();

    static StringMode _mode;
    public static StringMode Mode
    {
      get { return _mode; }
      private set { _mode = value; }
    }

    static Ascii()
    {
      Mode = StringMode.Lower | StringMode.Numbers;
      setupTestPool();
    }

    public static void SetStringMode(StringMode mode)
    {
      Mode = mode;
      setupTestPool();
    }

    private static void setupTestPool()
    {
      BitArray bitArray = new BitArray(new int[] { (int)Mode });
      bool[] flags = new bool[bitArray.Count];
      bitArray.CopyTo(flags, 0);

      Chars.Clear();
      int position = -1;
      while (++position <= 3)
      {
        switch (position)
        {
          case 0:
            if (flags[position])
              addRangeToChars("lower");
            break;
          case 1:
            if (flags[position])
              addRangeToChars("upper");
            break;
          case 2:
            if (flags[position])
              addRangeToChars("numbers");
            break;
          case 3:
            if (flags[position])
              addSpecialSigns();
            break;
          default:
            break;
        }
      }
    }

    private static void addRangeToChars(string key)
    {
      AsciiRange range;
      Ranges.TryGetValue(key, out range);
      if (range == null)
        throw new Exception("Undefined range: " + key);

      for (int i = range.StartIndex; i <= range.EndIndex; i++)
        Chars.Add((char)i);
    }

    private static void addSpecialSigns()
    {
      for (int i = 32; i <= 126; i++)
      {
        foreach (var range in Ranges)
        {
          if (i >= range.Value.StartIndex && i <= range.Value.EndIndex)
            continue;

          Chars.Add((char)i);
        }
      }
    }

    /// <summary>
    /// Searches for a subsequent char for a given one.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="newChar">The character following the given character.</param>
    /// <returns>True, if overflowed. Else false</returns>
    public static bool GetSubsequentChar(char c, out char newChar)
    {
      int index = Chars.IndexOf(c);
      if (index == Chars.Count - 1)
      {
        newChar = (char)Chars[0];
        return true;
      }
      else {
        newChar = (char)Chars[index + 1];
        return false;
      }
    }
  }
}
