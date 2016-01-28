using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class HealBot : Bot {

        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject Target;
        protected GameObject Focus;
        protected GameObject Party1;
        protected GameObject Party2;
        protected GameObject Party3;
        protected GameObject Party4;
        protected GameObject Party5;

        public HealBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            Target = new GameObject(wowProcess, this.wowinfo.TargetGUID);
            Focus = new GameObject(wowProcess, this.wowinfo.FocusGUID);
            //Party1 = new GameObject(wowProcess, this.wowinfo.Party1GUID);
            //Party2 = new GameObject(wowProcess, this.wowinfo.Party2GUID);
            //Party3 = new GameObject(wowProcess, this.wowinfo.Party3GUID);
            //Party4 = new GameObject(wowProcess, this.wowinfo.Party4GUID);
            //Party5 = new GameObject(wowProcess, this.wowinfo.Party5GUID);

        }
        public override void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            Target = new GameObject(wow, this.wowinfo.TargetGUID);
            Focus = new GameObject(wow, this.wowinfo.FocusGUID);
            //Party1 = new GameObject(wow, this.wowinfo.Party1GUID);
            //Party2 = new GameObject(wow, this.wowinfo.Party2GUID);
            //Party3 = new GameObject(wow, this.wowinfo.Party3GUID);
            //Party4 = new GameObject(wow, this.wowinfo.Party4GUID);
            //Party5 = new GameObject(wow, this.wowinfo.Party5GUID);

            Rota();

        }
        public virtual void Rota() {

        }
    }
}
