using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clipboard_History {
    public class ClipboardQueue {

        private static ClipboardQueue instance;

        public static ClipboardQueue GetInstance() {
            if (instance == null) {
                instance = new ClipboardQueue();
            }

            return instance;
        }

        /// <summary>
        /// The list with the history of the clipboard.
        /// It is ordered from top to bottom from least recentrly used to last recently used.
        /// </summary>
        public List<String> History {
            get;
            private set;
        }

        private ClipboardQueue() {
            History = new List<string>();
        }

        public void Add(string text) {
            if (History.Contains(text)) {
                // We remove the text if it's already in the history, to move it to the bottom of 
                // the list (last recently used)
                History.Remove(text);
            }


            // This keeps the history count at 15 entries.
            if (History.Count > 15) {
                History.Insert(History.Count - 1, text);
                History.RemoveAt(0);
            } else {
                History.Add(text);
            }
        }
    }
}
