using System.Collections.Generic;
using System.IO;

namespace ArchiveTester.Core
{
  public class ArchiveHandler : IArchiveHandler
  {


    private FileInfo _archiveFile;
    public FileInfo ArchiveFile
    {
      get { return _archiveFile; }
      set { _archiveFile = value; }
    }

    public IEnumerable<string> GetArchiveContent()
    {

    }

    public bool TestPassword(string password)
    {

    }

  }
}
