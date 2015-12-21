using System;
using System.IO;
using ArchiveTester.Core;

namespace ArchiveTester
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WindowHeight = 48;
      Console.WindowWidth = 128;

      try
      {
        Program p = new Program().Initialize();
        p.Run();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
      }
    }

    public IProbeUtility Probe { get; set; }

    public void Run()
    {
      string pass = Probe.BruteForceArchive();

      Console.WriteLine("Possible password: '{0}'", pass);
    }

    private Program Initialize()
    {
      string[] commandLineArgs = Environment.GetCommandLineArgs();

      if (commandLineArgs.Length == 1 || string.IsNullOrEmpty(commandLineArgs[1]))
        throw new ArgumentNullException("No path parameter supplied!");

      Console.Clear();
      Console.SetCursorPosition(0, 0);
      Console.WriteLine("############################################################################");
      Console.WriteLine("# Testing '" + commandLineArgs[0] + "'");
      Console.WriteLine("############################################################################");
      Console.WriteLine("#");
      Console.WriteLine("#     Password: ");
      Console.WriteLine("#");
      Console.WriteLine("############################################################################");
      Console.CancelKeyPress += Console_CancelKeyPress;

      FileInfo archiveFile = new FileInfo(commandLineArgs[0]);
      IArchiveHandler archive = new ArchiveHandler(archiveFile);

      Probe = new ProbeUtility(archive);
      Probe.TestingPassword += Probe_TestingPassword;

      return this;
    }

    private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
      Probe?.Dispose();
    }

    private static void Probe_TestingPassword(object sender, string e)
    {
      Console.SetCursorPosition(16, 3);
      Console.Write(e);
    }
  }
}
