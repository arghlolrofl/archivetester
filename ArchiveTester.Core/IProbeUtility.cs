using System;

namespace ArchiveTester.Core {
    public interface IProbeUtility : IDisposable {
        IArchiveHandler ArchiveHandler { get; set; }

        string BruteForceArchive();

        event EventHandler<string> TestingPassword;
    }
}
