using System.Collections.Generic;
using System.IO;

namespace ArchiveTester.Core {
    public interface IArchiveHandler {
        FileInfo ArchiveFile { get; set; }

        IEnumerable<string> GetArchiveContent();

        bool TestPassword(string password);
        bool Extract(string path, string password = null);
    }
}
