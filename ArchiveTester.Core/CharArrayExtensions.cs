namespace ArchiveTester.Core
{
  public static class CharArrayExtensions
  {
    /// <summary>
    /// Returns the least significant index where a value has been set.
    /// </summary>
    /// <param name="chars"></param>
    /// <returns></returns>
    public static int GetTrimmedLength(this char[] chars)
    {
      for (int i = chars.Length - 1; i >= 0; i++)
      {
        if (chars[i] == '\0')
          continue;

        return i;
      }

      return 0;
    }
  }
}
