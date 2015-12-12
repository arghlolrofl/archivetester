using System.Collections.Generic;
using System.IO;
using System;
using SevenZip;
using System.Diagnostics;
using System.Threading;

/// <summary>
/// How to test a file in an archive with 7zip
///    1. path to 7z.exe
///	   2. test archive
///	   3. archive path
///	   4. file in archive
///	   5. extract with given password
///& 'C:\Program Files\7-Zip\7z.exe' t .\temp.zip testfile2.txt -p123
/// </summary>


namespace ArchiveTester.Core {
    public class ArchiveHandler : IArchiveHandler, IDisposable {
        ProcessStartInfo _psi = new ProcessStartInfo();
        List<FileStream> _parts = new List<FileStream>();
        FileInfo _archiveFile;


        public FileInfo ArchiveFile {
            get { return _archiveFile; }
            set { _archiveFile = value; }
        }


        public ArchiveHandler(FileInfo file) {
            if (!file.Exists)
                throw new FileNotFoundException("File not found: " + file.FullName);

            _archiveFile = file;

            //for (int i = 1; i < 10; i++) {
            //    FileInfo partFile = new FileInfo(file.FullName.Replace("1", i.ToString()));
            //    if (!partFile.Exists)
            //        throw new FileNotFoundException("File not found: " + partFile.FullName);

            //    _parts.Add(partFile.OpenRead());
            //}

            _psi = new ProcessStartInfo();
            _psi.CreateNoWindow = true;
            _psi.FileName = @"e:\tools\7zip\7z.exe";
            _psi.WorkingDirectory = @"e:\- incoming -\temp.och\";
            _psi.RedirectStandardError =
                _psi.RedirectStandardInput =
                _psi.RedirectStandardOutput = true;
            _psi.UseShellExecute = false;
        }

        public IEnumerable<string> GetArchiveContent() {
            throw new NotImplementedException();
        }

        public bool TestPassword(string password) {
            throw new NotImplementedException();
        }

        public bool Extract(string path, string password = null) {
            try {
                //_psi.Arguments = "/c " + @"e:\tools\7zip\7z.exe t .\WET.part01.rar """ + path + @""" -p" + password;
                string tpl = "t \"{0}\" \"{1}\" -p{2}";
                _psi.Arguments = String.Format(tpl, @"e:\- incoming -\temp.och\WET.part01.rar", path, password);

                using (Process p = Process.Start(_psi))
                using (StreamReader sr = p.StandardOutput)
                using (StreamWriter sw = p.StandardInput)
                using (StreamReader se = p.StandardError) {
                    Console.WriteLine(_psi.Arguments.Substring(2));

                    string output = sr.ReadToEnd().Replace("\r\n", "\r\n\t\t");

                    Console.WriteLine(Environment.NewLine + "-> " + Environment.NewLine + output);
                    Console.WriteLine(se.ReadToEnd());

                    if (output.Contains("Wrong password?"))
                        return false;
                }

                return true;
            } catch (Exception ex) {
                Console.WriteLine("EXCEPTION");
                Console.WriteLine(ex.Message);
                return false;
            } finally {

            }
        }

        public void Dispose() {
            foreach (var stream in _parts) {
                if (stream != null) {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }
    }
}
