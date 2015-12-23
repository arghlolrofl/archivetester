using System;
using System.IO;
using System.Text;
using System.Threading;

namespace ArchiveTester.Core {
    public class ProbeUtility : IProbeUtility, IDisposable {
        FileInfo f;
        string lastPass;
        TestString testString;
        public event EventHandler<string> TestingPassword;
        public IArchiveHandler ArchiveHandler { get; set; }

        public ProbeUtility(IArchiveHandler handler) {
            ArchiveHandler = handler;
            testString = new TestString(6);

            f = new FileInfo("..\\..\\..\\config.cfg");
            if (!f.Exists)
                f.Create();
            else if (f.Length > 0) {
                using (StreamReader sr = f.OpenText())
                    testString = new TestString(6, sr.ReadToEnd());
            }
        }

        public string BruteForceArchive() {
            do {
                lastPass = testString.Value;
                TestingPassword(this, lastPass);

                if (ArchiveHandler.TestPassword(lastPass))
                    break;
            } while (testString.Increment());

            return testString.Value;
        }

        public void Dispose() {
            ArchiveHandler.Dispose();
            using (StreamWriter sw = new StreamWriter(f.FullName)) {
                sw.Write(lastPass);
            }
        }
    }
}
