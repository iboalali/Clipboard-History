using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_History {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Mutex mutex = new Mutex(true, "com.iboalali.windowsclipboardhistory", out bool onlyInstance);

            if (!onlyInstance) {
                // I'm not the first instance, I'm going to die now.
                return;
            }

            // I'm the first instance
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayAppContext());

            GC.KeepAlive(mutex);
        }
    }
}
