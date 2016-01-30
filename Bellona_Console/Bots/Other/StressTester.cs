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
            this.ticks++;
            PrintToConsoleAt(("Refreshing Global info " + this.ticks), 1);
            wowinfo.Refresh(wow);
            PrintToConsoleAt(("Refreshing Player Unit info " + this.ticks), 2);
            Player.RefreshUnit(wow);
            if (Target.GUID != wowinfo.TargetGUID) {
                PrintToConsoleAt(("Target changed " + this.ticks), 3);
                Target = new GameObject(wow, this.wowinfo.TargetGUID);
            }
            if (Target.GUID == 0) {
                PrintToConsoleAt(("No target " + this.ticks), 4);
            }
            else {
                PrintToConsoleAt(("Refreshing Target Unit info " + this.ticks), 5);
                Target.RefreshUnit(wow);
            }
        }
        private void PrintToConsoleAt(string str, int ctop) {
            Console.WriteLine(str);
        }

    }
}
