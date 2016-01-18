using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class DPSBot : Bot {
        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject Target;

        public DPSBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
        }
        public override void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            this.Player.RefreshUnit(wow);
            if (wowinfo.TargetGUID == 0) {
                return; //No target
            }
            if (Target.GUID != wowinfo.TargetGUID) {
                Target = new GameObject(wow, this.wowinfo.TargetGUID);
            }
            else {
                Target.Unit.Refresh(wow, Target);
            }
            Rota();

        }
        public virtual void Rota() {

        }
    }
}
