// from: http://stackoverflow.com/questions/2226920/how-to-monitor-clipboard-content-changes-in-c
// https://gist.github.com/glombard/7986317

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Clipboard_History {
    /// <summary>
    /// Provides notifications when the contents of the clipboard is updated.
    /// </summary>
    public sealed class ClipboardNotification {
        /// <summary>
        /// Occurs when the contents of the clipboard is updated.
        /// </summary>
        public static event EventHandler ClipboardUpdate;

        private static NotificationForm _form = new NotificationForm();

        /// <summary>
        /// Raises the <see cref="ClipboardUpdate"/> event.
        /// </summary>
        /// <param name="e">Event arguments for the event.</param>
        private static void OnClipboardUpdate(ClipboardEventArgs e) {
            ClipboardUpdate?.Invoke(null, e);
        }

        /// <summary>
        /// Hidden form to recieve the WM_CLIPBOARDUPDATE message.
        /// </summary>
        public class NotificationForm : Form {
            public NotificationForm() {
                // Turn the child window into a message-only window (refer to Microsoft docs)
                NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);

                // Place window in the system-maintained clipboard format listener list
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            protected override void WndProc(ref Message m) {
                // Listen for operating system message
                if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE) {
                    try {
                        if (Clipboard.ContainsText()) {
                            var args = new ClipboardEventArgs(Clipboard.GetText());
                            OnClipboardUpdate(args);
                        }
                    } catch (ExternalException) {
                        // ignored
                    }
                }

                // Called for any unhandled messages
                base.WndProc(ref m);
            }
        }
    }

    internal static class NativeMethods {
        // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        // See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
        // See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}