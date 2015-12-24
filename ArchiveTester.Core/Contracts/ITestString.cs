namespace ArchiveTester.Core.Contracts
{
  public interface ITestString
  {
    string Value { get; }

    string GetNext();
  }
}
