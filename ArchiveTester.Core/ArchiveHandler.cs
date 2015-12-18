using System.Collections.Generic;
using System.IO;
using SharpCompress.Archive;
using SharpCompress.Archive.Rar;
using SharpCompress.Common;

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
      IList<string> files = new List<string>();

      var archive = ArchiveFactory.Open(ArchiveFile);
      foreach (var entry in archive.Entries)
        files.Add(entry.Key);

      return files;
    }

    public bool TestPassword(IEnumerable<Stream> parts, string password)
    {
      RarArchive archive = RarArchive.Open(parts, Options.KeepStreamsOpen, password);
      foreach (var entry in archive.Entries)
      {
        if (entry.IsDirectory)
          continue;

        entry.WriteTo(stream);
      }
      return false;
    }

  }
}
