using Clipboard_History.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_History {
    class TrayAppContext : ApplicationContext {

        private System.ComponentModel.Container components;
        private NotifyIcon notifyIcon;
        private ClipboardNotification.NotificationForm form;

        public TrayAppContext() {
            InitializeContext();
            form = new ClipboardNotification.NotificationForm();
            ClipboardNotification.ClipboardUpdate += ClipboardNotification_ClipboardUpdate;

        }

        private void ClipboardNotification_ClipboardUpdate(object sender, EventArgs e) {
            if (e is ClipboardEventArgs) {
                ClipboardQueue.GetInstance().Add((e as ClipboardEventArgs).Text);
            }
        }

        private void InitializeContext() {

            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components) {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Resources.AppIcon,
                Text = "Clipboard History",
                Visible = true
            };


            //notifyIcon.MouseClick += NotifyIcon_MouseClick;
            notifyIcon.ContextMenuStrip.LostFocus += ContextMenuStrip_LostFocus;
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.ContextMenuStrip.Click += ContextMenuStrip_Click;
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e) {
            notifyIcon.ContextMenuStrip.Show(Cursor.Position);
        }

        private void ContextMenuStrip_LostFocus(object sender, EventArgs e) {
            notifyIcon.ContextMenuStrip.Hide();
        }

        private void ContextMenuStrip_Click(object sender, EventArgs e) {
            var mouseEventArgs = (e as MouseEventArgs);
            var item = (sender as ContextMenuStrip).GetItemAt(mouseEventArgs.X, mouseEventArgs.Y);

            if (item.Text == "Exit") {
                // Check if the exit button was clicked
                Exit();
            } else {
                // If not copy the clicked text
                Clipboard.SetText(item.Text);
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = false;

            notifyIcon.ContextMenuStrip.Items.Clear();

            foreach (var item in ClipboardQueue.GetInstance().History) {
                notifyIcon.ContextMenuStrip.Items.Add(item);
            }

            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());

            notifyIcon.ContextMenuStrip.Items.Add("Exit");
        }

        protected override void Dispose(bool disposing) {
            if (disposing && components != null) { components.Dispose(); }
        }

        //protected override void ExitThreadCore() {
        //    notifyIcon.Visible = false; // should remove lingering tray icon!
        //    base.ExitThreadCore();
        //}

        void Exit() {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;
            form.Dispose();

            Application.Exit();
        }
    }
}
