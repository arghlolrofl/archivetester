using System.Collections.Generic;
using System.IO;
using SevenZip;

namespace ArchiveTester.Core
{
  public class ArchiveHandler : IArchiveHandler
  {
    SevenZipExtractor _extractor;

    private FileInfo _archiveFile;
    public FileInfo ArchiveFile
    {
      get { return _archiveFile; }
      set { _archiveFile = value; }
    }

    public IEnumerable<string> GetArchiveContent()
    {
      SevenZipExtractor extractor = new SevenZipExtractor(ArchiveFile.FullName);
      return extractor.ArchiveFileNames;
    }

    public bool TestPassword(string password)
    {
      _extractor = new SevenZipExtractor(ArchiveFile.FullName, password);
      return _extractor.Check();
    }

  }
}
