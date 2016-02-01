using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.Other {
    class StressTester : Bot{
        private BlackMagic wow;
        private WoWGlobal wowinfo;
        private GameObject Player;
        private GameObject Target;

        public StressTester(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
        }

        public override void BotEvent(Object source) {
            SendKey.Send(ConstController.WindowsVirtualKey.VK_MBUTTON);
            SendKey.Send(ConstController.WindowsVirtualKey.VK_NUMPAD6);
        }
        private void PrintToConsoleAt(string str, int ctop) {
            Console.WriteLine(str);
        }

    }
}
