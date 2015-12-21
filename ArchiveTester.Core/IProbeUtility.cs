using System;

namespace ArchiveTester.Core
{
  public interface IProbeUtility : IDisposable
  {
    event EventHandler<string> TestingPassword;

    IArchiveHandler ArchiveHandler { get; set; }

    string BruteForceArchive();
  }
}
