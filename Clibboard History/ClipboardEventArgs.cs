using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clibboard_History {
    public class ClipboardEventArgs : EventArgs{
        public string Text { get; private set; }

        public ClipboardEventArgs(string text) {
            Text = text;
        }
    }
}
