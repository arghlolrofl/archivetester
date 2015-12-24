using System;
using ArchiveTester.Core.Contracts;

namespace ArchiveTester.Core {
    public class ProbeUtility : IProbeUtility {
        Func<string> _nextStringAction;

        public IArchiveHandler ArchiveHandler { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handler"></param>
        public ProbeUtility(IArchiveHandler handler, Func<string> action = null) {
            ArchiveHandler = handler;
            _nextStringAction = action;
        }

        public bool Run() {
            string nextPass = null;
            do {
                nextPass = _nextStringAction.Invoke();
                if (String.IsNullOrEmpty(nextPass))
                    return false;

                RaiseTestingPassword(nextPass);
            } while (!ArchiveHandler.TestPassword(nextPass));

            return true;
        }

        #region EventHandlers

        public event EventHandler<string> TestingPassword;
        private void RaiseTestingPassword(string pw) {
            EventHandler<string> handler = TestingPassword;
            if (handler != null)
                handler(this, pw);
        }

        #endregion

        public void Dispose() {

        }
    }
}
