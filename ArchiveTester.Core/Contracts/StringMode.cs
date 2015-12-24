namespace ArchiveTester.Core.Contracts
{
  public enum StringMode
  {
    /// <summary>
    /// Lower case letters
    /// </summary>
    Lower = 1,

    /// <summary>
    /// Upper case letters
    /// </summary>
    Upper = 2,

    /// <summary>
    /// Digits 0-9
    /// </summary>
    Numbers = 4,

    /// <summary>
    /// Special Signs
    /// </summary>
    Signs = 8
  }
}
