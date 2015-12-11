namespace ArchiveTester.Core
{
  public interface ITestString
  {
    string Value { get; }

    bool Increment();
    bool Increment(int distance);
  }
}