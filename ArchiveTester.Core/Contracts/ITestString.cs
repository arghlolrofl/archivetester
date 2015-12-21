namespace ArchiveTester.Core.Contracts
{
  public interface ITestString
  {
    string Value { get; set; }

    void Initialize();

    string GetNext();
  }
}
