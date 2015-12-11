using System;

namespace ArchiveTester.Core
{
  public class TestString : ITestString
  {
    char[] _current;

    public string Value
    {
      get
      {
        return new string(_current);
      }
    }

    #region Initialization

    public TestString(int maxStringLength)
    {
      _current = new char[maxStringLength];
      _current[0] = (char)Ascii.Chars[0];
    }

    public TestString(int maxStringLength, string startString) : this(maxStringLength)
    {
      if (startString.Length > maxStringLength)
        throw new ArgumentException("Given string exceeds maximum string length!");

      for (int i = 0; i < startString.Length; i++)
        _current[i] = startString[i];
    }

    /// <summary>
    /// Increments the test string by 1 on the last set position. Handles overflows too.
    /// </summary>
    /// <returns>False, when no more increments are allowed (e.g.: last possible string permutation)</returns>
    public bool Increment()
    {
      char newChar;
      bool hasOverflow = true;
      int index = _current.GetTrimmedLength();

      while (hasOverflow)
      {
        hasOverflow = Ascii.GetSubsequentChar(_current[index], out newChar);

        if (hasOverflow && index == 0)
          return false;

        _current[index--] = newChar;
      }

      return true;
    }

    public bool Increment(int distance)
    {
      for (int i = 0; i < distance; i++)
      {
        if (!Increment())
          return false;
      }

      return true;
    }

    #endregion
  }
}
