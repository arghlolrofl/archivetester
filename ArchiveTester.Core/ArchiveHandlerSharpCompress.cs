using SharpCompress.Archive.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTester.Core {
    public class ArchiveHandlerSharpCompress : IArchiveHandler {
        const int targetFileSize = 21;
        List<Stream> _parts = new List<Stream>(9);
        FileInfo _archiveFile;

        public FileInfo ArchiveFile {
            get { return _archiveFile; }
            set { _archiveFile = value; }
        }

        bool areStreamsOpened = false;
        bool areStreamsReset = true;

        public ArchiveHandlerSharpCompress(FileInfo file) {
            if (!file.Exists)
                throw new FileNotFoundException("File not found: " + file.FullName);

            _archiveFile = file;

            openStreams();
        }

        public bool TestPassword(string password) {
            resetStreams();

            using (RarArchive rar = RarArchive.Open(_parts, Options.KeepStreamsOpen, password)) {
                //using (RarArchive rar = RarArchive.Open(ArchiveFile, SharpCompress.Common.Options.None, password)) {
                try {
                    using (Stream srcStream = rar.Entries.Single((r) => r.Size == targetFileSize).OpenEntryStream())
                        if (srcStream.ReadByte() == 68 && srcStream.Position == 21)
                            return true;
                } catch {
                    return false;
                } finally {
                    areStreamsReset = false;
                }

                return false;
            }
        }

        void openStreams() {
            IList<Stream> tempParts = new List<Stream>(9);

            for (int i = 1; i < 10; i++) {
                FileInfo partFile = new FileInfo(ArchiveFile.FullName.Replace("1", i.ToString()));
                if (!partFile.Exists)
                    break;
                    //throw new FileNotFoundException("File not found: " + partFile.FullName);

                tempParts.Add(partFile.OpenRead());
            }

            foreach (var tp in tempParts) {
                MemoryStream memStream = new MemoryStream();
                tp.CopyTo(memStream);
                _parts.Add(memStream);
            }

            foreach (var tp in tempParts)
                tp.Close();

            areStreamsOpened = true;
            areStreamsReset = true;
        }

        void resetStreams() {
            if (areStreamsReset)
                return;

            foreach (var part in _parts)
                (part as MemoryStream).Seek(0, SeekOrigin.Begin);

            areStreamsReset = true;
        }

        void closeStreams() {
            foreach (var part in _parts)
                part?.Close();

            areStreamsOpened = false;
        }

        public void Dispose() {
            closeStreams();
        }
    }
}
