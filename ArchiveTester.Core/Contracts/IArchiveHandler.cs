using System.Collections.Generic;
using System.IO;

namespace ArchiveTester.Core.Contracts
{
  public interface IArchiveHandler
  {
    FileInfo ArchiveFile { get; set; }

    IEnumerable<string> GetContents();

    bool TestPassword(string password);
  }
}
