using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_History.ClipboardToolStripItems {
    class ExitTextToolStripItem : TextToolStripItem {
        private readonly string exitText = "Exit";

        public ExitTextToolStripItem() : base() {
            Data = exitText + "_" + Utils.RandomString(200);
        }

        public override string GetTextToShow() {
            return exitText;
        }
    }
}
