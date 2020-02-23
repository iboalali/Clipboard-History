using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_History.ClipboardToolStripItems {
    class TextToolStripItem : ClipboardToolStripItem {

        protected TextToolStripItem() : base() {
        }

        public TextToolStripItem(string text) : base() {
            Data = text;
        }

        public string Data { get; protected set; }

        public override string GetTextToShow() {
            if (Data.Length > 100) {
                return Data.Substring(0, 100);
            }

            return Data;
        }

        public override bool Equals(object obj) {
            if (!(obj is TextToolStripItem)) {
                return false;
            }

            return (obj as TextToolStripItem).Data == Data;
        }

        public override int GetHashCode() {
            return Data.GetHashCode();
        }

        public override bool ShowInMenu() {
            return true;
        }
    }
}
