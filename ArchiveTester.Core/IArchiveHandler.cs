using System;
using System.Collections.Generic;
using System.IO;

namespace ArchiveTester.Core {
    public interface IArchiveHandler : IDisposable {
        FileInfo ArchiveFile { get; set; }

        bool TestPassword(string password);
    }
}
