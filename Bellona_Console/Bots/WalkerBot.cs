using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {

    public class WalkerBot : Bot {
        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject Target;

        public WalkerBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
        }
        public override void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            Target = new GameObject(wow, this.wowinfo.TargetGUID);
            float vx = Target.Unit.Position.X - Player.Unit.Position.X;
            float vy = Target.Unit.Position.Y - Player.Unit.Position.Y;
            double szeg=Math.Atan(-vy / -vx);
            Console.WriteLine(180*szeg/3.14 + "°");
        }
    }
}

