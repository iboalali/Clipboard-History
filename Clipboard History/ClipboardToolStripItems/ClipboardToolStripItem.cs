using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_History {
    public abstract class ClipboardToolStripItem : ToolStripItem {
        // TODO: make an Exit ToolsStripItem
        // TODO: make a no-data ToolsStripItem
        // TODO: make a text ToolsStripItem (this class)

        public ClipboardToolStripItem() : base() {

        }

        public abstract string GetTextToShow();

        public abstract bool ShowInMenu();
    }
}
