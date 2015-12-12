using ArchiveTester.Core;
using System;
using System.IO;
using System.Threading;

namespace ArchiveTester {
    class Program {
        static void Main(string[] args) {
            Console.WindowHeight = 64;
            Console.WindowWidth = 168;

            Program p = new Program();
            p.Run();
        }

        public IProbeUtility Probe { get; set; }

        public void Run() {
            Console.CancelKeyPress += Console_CancelKeyPress;

            string rarFilePath = @"e:\- incoming -\temp.och\WET.part01.rar";
            FileInfo rarFile = new FileInfo(rarFilePath);

            IArchiveHandler archive = new ArchiveHandler(rarFile);

            Probe = new ProbeUtility(archive);
            Probe.TestingPassword += Probe_TestingPassword;
            Console.Clear();
            Console.SetCursorPosition(1, 1);
            Console.WriteLine("###########################################################################");
            Console.WriteLine("#        Starting test ...                                                 #");
            Console.WriteLine("############################################################################");
            Console.WriteLine();
            

            string pass = Probe.BruteForceArchive();

            Console.WriteLine("Possible password: '{0}'", pass);
        }

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) {
            Probe?.Dispose();
        }

        private static void Probe_TestingPassword(object sender, string e) {
            Console.SetCursorPosition(2, 6);
            Console.WriteLine("Testing password: {0}\r\n", e);
        }
    }
}
