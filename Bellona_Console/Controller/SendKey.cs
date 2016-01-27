using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Controller {
    class SendKey {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const Int32 WM_KEYDOWN = 0x0100;
        private const Int32 WM_KEYUP = 0x0101;

        public static void Send(ConstController.WindowsVirtualKey Key) {
            IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
            PostMessage(Handle, WM_KEYDOWN, (int)Key, 0);
            PostMessage(Handle, WM_KEYUP, (int)Key, 0);
        }
        public static void Send(ConstController.WindowsVirtualKey Key, int time) {
            IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
            PostMessage(Handle, WM_KEYDOWN, (int)Key, 0);
            System.Threading.Thread.Sleep(time);
            PostMessage(Handle, WM_KEYUP, (int)Key, 0);
        }
    }
}
