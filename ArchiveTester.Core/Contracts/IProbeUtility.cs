using System;

namespace ArchiveTester.Core.Contracts
{
  public interface IProbeUtility : IDisposable
  {
    event EventHandler<string> TestingPassword;

    bool Run();
  }
}
