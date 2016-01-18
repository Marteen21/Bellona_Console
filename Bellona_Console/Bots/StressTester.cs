using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class StressTester {
        private BlackMagic wow;
        private WoWGlobal wowinfo;
        private GameObject Player;
        private GameObject Target;
        private System.Timers.Timer aTimer;
        private long ticks = 0;
        private static int cursorTopPos = 1;

        public StressTester(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) {
            Console.Clear();
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
            SetupTimer(tt);
            this.aTimer.Elapsed += TestEvent;
        }
        private void SetupTimer(uint tt) {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = tt;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        private void RefreshTick() {
            wowinfo.Refresh(wow);
            Player.RefreshUnit(wow);
            if (Target.GUID != wowinfo.TargetGUID) {
                Target = new GameObject(wow, this.wowinfo.TargetGUID);
            }
            Target.RefreshUnit(wow);
            PrintTicks();

        }
        public void TestEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.RefreshTick();
        }
        private void PrintTicks() {
            this.ticks++;
            int temp = Console.CursorTop;
            Console.CursorTop = cursorTopPos;
            Console.WriteLine("Stress test is running " + ticks);
            Console.CursorTop = temp;
        }
    }
}
