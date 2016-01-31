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
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern bool PostMessage(IntPtr hWnd, uint Msg, string wParam, string lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static readonly uint WM_KEYDOWN = 0x0100;
        private static readonly uint WM_KEYUP = 0x0101;
        private static readonly uint WM_COPYDATA = 0x004A;
        public static void Send(ConstController.WindowsVirtualKey Key) {
            IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
            PostMessage(Handle, WM_KEYDOWN, (int)Key, 0);
            PostMessage(Handle, WM_KEYUP, (int)Key, 0);
        }

        //public static void SendString(string str) {
        //    IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
        //    PostMessage(Handle, WM_COPYDATA, str, str);

        //}
        public static void KeyDown(ConstController.WindowsVirtualKey Key, ref bool state) {
            //if (!state) {
            IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
            PostMessage(Handle, WM_KEYDOWN, (int)Key, 0);
            //}
            state = true;

        }
        public static void KeyUp(ConstController.WindowsVirtualKey Key, ref bool state) {
            //if (state) {
            IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
            PostMessage(Handle, WM_KEYUP, (int)Key, 0);
            //}
            state = false;
        }
        public static void KeyDown(ConstController.WindowsVirtualKey Key, bool state, ref bool mystate) {
            if (state || mystate) {
                IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
                PostMessage(Handle, WM_KEYDOWN, (int)Key, 0);
            }
            mystate = true;

        }
        public static void KeyUp(ConstController.WindowsVirtualKey Key, bool state, ref bool mystate) {
            if (state || mystate) {
                IntPtr Handle = FindWindow(null, Program.PROCESS_WINDOW_TITLE);
                PostMessage(Handle, WM_KEYUP, (int)Key, 0);
            }
            mystate = false;
        }
    }
}
