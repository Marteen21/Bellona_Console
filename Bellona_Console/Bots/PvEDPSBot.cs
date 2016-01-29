using Bellona_Console.Controller;
using Bellona_Console.MemoryReading;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    public class PvEDPSBot : Bot {
        protected BlackMagic wow;
        protected WoWGlobal wowinfo;
        protected GameObject Player;
        protected GameObject CurTarget;
        protected GameObject Target;
        protected GameObject Focus;

        public PvEDPSBot(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(tt) {
            this.wow = wowProcess;
            this.wowinfo = globalinfo;
            this.wowinfo.Refresh(wowProcess);
            Player = new GameObject(wowProcess, this.wowinfo.PlayerGUID);
            CurTarget = new GameObject(wowProcess, this.wowinfo.TargetGUID);
            Focus = new GameObject(wowProcess, this.wowinfo.FocusGUID);
            SetTarget();
        }
        public override void BotEvent(Object source, System.Timers.ElapsedEventArgs e) {
            this.ticks++;
            this.wowinfo.Refresh(wow);
            Player = new GameObject(wow, this.wowinfo.PlayerGUID);
            CurTarget = new GameObject(wow, this.wowinfo.TargetGUID);
            Focus = new GameObject(wow, this.wowinfo.FocusGUID);
            SetTarget();
            if (Target.GUID != 0) {
                Rota();
            }


        }
        public virtual void Rota() {
            if (wowinfo.TargetGUID != Target.GUID) {
                SendKey.Send(ConstController.WindowsVirtualKey.VK_F6);
            }
        }
        private void SetTarget() {
            if (Focus.Unit.TargetGUID != 0) {
                Target = new GameObject(wow,(UInt64) this.Focus.Unit.TargetGUID);
            }
            else {
                Target = new GameObject(wow,(UInt64) this.wowinfo.TargetGUID);
            }
        }
    }
}
