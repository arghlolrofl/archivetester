namespace ArchiveTester.Core
{
  public class ProbeUtility : IProbeUtility
  {
    public IArchiveHandler ArchiveHandler { get; set; }

    public ProbeUtility(IArchiveHandler handler)
    {
      ArchiveHandler = handler;
    }

    public string BruteForceArchive()
    {
      return null;
    }
  }
}
