using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ArchiveTester.Core;
using ArchiveTester.Core.Contracts;

namespace ArchiveTester
{
  class Program
  {
    const string CONFIG_PATH = "..\\..\\..\\config.cfg";

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

    public IList<IProbeUtility> Probes { get; set; }
    public ITestString TestString { get; set; }


    private Program Initialize()
    {
      Console.Clear();
      Console.SetCursorPosition(0, 0);
      Console.WriteLine("############################################################################");
      Console.WriteLine("# Password Test");
      Console.WriteLine("############################################################################");
      Console.WriteLine("#");
      Console.WriteLine("#     Password: ");
      Console.WriteLine("#");
      Console.WriteLine("############################################################################");
      Console.CancelKeyPress += Console_CancelKeyPress;

      prepareTestString();
      setupProbes();

      return this;
    }

    public void Run()
    {
      Task<bool>[] tasks = new Task<bool>[Probes.Count];
      for (int i = 0; i < Probes.Count; i++)
        tasks[i] = Task.Factory.StartNew(() => Probes[i].Run());

      Task.WaitAll(tasks);
    }

    private void prepareTestString()
    {
      FileInfo configFile = new FileInfo(CONFIG_PATH);

      if (!configFile.Exists)
      {
        configFile.Create();
        TestString = new TestString(6);
      }
      else if (configFile.Length > 0)
      {
        using (StreamReader sr = configFile.OpenText())
          TestString = new TestString(6, sr.ReadToEnd());
      }
    }

    private void setupProbes()
    {
      Probes = new List<IProbeUtility>();

      string[] commandLineArgs = Environment.GetCommandLineArgs();

      if (commandLineArgs.Length <= 1 || string.IsNullOrEmpty(commandLineArgs[1]))
        throw new ArgumentNullException("No path parameter supplied!");

      IList<string> archivePathes = new List<string>(commandLineArgs.Length - 1);
      for (int i = 0; i < commandLineArgs.Length - 1; i++)
        archivePathes.Add(commandLineArgs[i + 1]);

      foreach (string path in archivePathes)
      {
        FileInfo targetFile = new FileInfo(path);
        if (!targetFile.Exists)
          throw new FileNotFoundException("File not found: " + targetFile.FullName);

        IArchiveHandler archiveHandler = new ArchiveHandler(targetFile);
        IProbeUtility probe = new ProbeUtility(archiveHandler, TestString.GetNext);
        probe.TestingPassword += Probe_TestingPassword;

        Probes.Add(probe);
      }
    }

    private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
      foreach (var probe in Probes)
        probe?.Dispose();
    }

    private void Probe_TestingPassword(object sender, string e)
    {
      Console.SetCursorPosition(16, 4);
      Console.Write(e);
    }
  }
}
