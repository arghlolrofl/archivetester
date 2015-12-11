namespace ArchiveTester.Core
{
  public interface IProbeUtility
  {
    IArchiveHandler ArchiveHandler { get; set; }

    string BruteForceArchive();
  }
}
