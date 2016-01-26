using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.MemoryReading {
    class Initializer {
        public static GameObject FirstObject;

        public static bool ConnectToGame(out BlackMagic w, string title) {
            w = new BlackMagic();
            try {
                if (!w.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(title))) {
                    return false;
                }
                Console.WriteLine("Process found...");
                uint ObjMgrAddr = w.ReadUInt(w.ReadUInt((uint)w.MainModule.BaseAddress + (uint)ConstOffsets.ObjectManager.CurMgrPointer) + (uint)ConstOffsets.ObjectManager.CurMgrOffset);
                Console.WriteLine("Object Manager found...");
                FirstObject = new GameObject(w,(UIntPtr)w.ReadUInt(ObjMgrAddr + (uint)ConstOffsets.ObjectManager.FirstObject));
                Console.WriteLine("First Object found...");
                return true;
                
            }
            catch {
                return false;
            }
        }

    }
}
