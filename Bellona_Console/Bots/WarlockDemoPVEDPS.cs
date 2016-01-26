using Bellona_Console.Controller;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class WarlockDemoPVEDPS : DPSBot {
        private DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.K_4);
        private DoT immolate = new DoT(348, ConstController.WindowsVirtualKey.K_3);
        private DoT baneofdoom = new DoT(603, ConstController.WindowsVirtualKey.K_5);
        private DoT handofguldan = new DoT(86000, ConstController.WindowsVirtualKey.K_6);
        private Spell moltenCore = new Spell(71165, ConstController.WindowsVirtualKey.K_2);
        private Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.K_1);
        private Spell soulFire = new Spell(63167, ConstController.WindowsVirtualKey.K_7);

        public WarlockDemoPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Warlock Demonology DPS bot");
        }
        public override void Rota() {
            corruption.ReCast(this.wowinfo, this.Target.Unit);
            immolate.ReCast(this.wowinfo, this.Target.Unit);
            baneofdoom.ReCast(this.wowinfo, this.Target.Unit);
            if (this.Target.Unit.HasBuff(immolate.ID) && this.Target.Unit.HasBuff(corruption.ID) && this.Target.Unit.HasBuff(baneofdoom.ID)) {
                if (!handofguldan.ReCast(this.wowinfo, this.Target.Unit)) {
                    shadowTrance.CastIfHasBuff(this.wowinfo, this.Player.Unit);
                    if (!moltenCore.CastIfHasBuff(this.wowinfo, this.Player.Unit) && !soulFire.CastIfHasBuff(this.wowinfo, this.Player.Unit)) {
                        shadowTrance.SendCast();
                    }
                }
            }
        }
    }
}
