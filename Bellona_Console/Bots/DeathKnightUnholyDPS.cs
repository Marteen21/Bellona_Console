using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class DeathKnightUnholyDPS : DPSBot{
        private DoT frostFever = new DoT(55095, Controller.ConstController.WindowsVirtualKey.K_8);
        private DoT scarletFever = new DoT(81130, Controller.ConstController.WindowsVirtualKey.K_9);
        private Spell HornofWinter = new Spell(57330, Controller.ConstController.WindowsVirtualKey.K_Ú);
        private Spell UnholyPresence = new Spell(48265, Controller.ConstController.WindowsVirtualKey.K_Á);

        public DeathKnightUnholyDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
        }
        public override void Rota() {
            if (!Player.Unit.HasBuff(HornofWinter.ID)) {
                HornofWinter.SendCast();
            }
            else if (!Player.Unit.HasBuff(UnholyPresence.ID)) {
                UnholyPresence.SendCast();
            }
            if (!frostFever.ReCast(this.wowinfo, this.Target.Unit) && !scarletFever.ReCast(this.wowinfo, this.Target.Unit)) {
                
            }
        }

    }
}
