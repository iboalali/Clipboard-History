using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_History.ClipboardToolStripItems {
    class NoOpToolStripItem : ClipboardToolStripItem {

        public override string GetTextToShow() {
            return "";
        }

        public override bool ShowInMenu() {
            return false;
        }
    }
}
