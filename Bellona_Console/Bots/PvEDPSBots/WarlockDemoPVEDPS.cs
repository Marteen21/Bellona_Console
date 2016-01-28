using Bellona_Console.Controller;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.PvEDPSBots {
    class WarlockDemoPVEDPS : PvEDPSBot {
        private DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.K_4);
        private DoT immolate = new DoT(348, ConstController.WindowsVirtualKey.K_3);
        private DoT baneofdoom = new DoT(603, ConstController.WindowsVirtualKey.K_5);
        private DoT handofguldan = new DoT(86000, ConstController.WindowsVirtualKey.K_6);
        private Spell moltenCore = new Spell(71165, ConstController.WindowsVirtualKey.K_2);
        private Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.K_1);
        private Spell soulFire = new Spell(63167, ConstController.WindowsVirtualKey.K_7);
        private WalkerBot followFocus;

        public WarlockDemoPVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint dpsTimerInterval, uint walkerTimerInterval) : base(wowProcess, globalinfo, dpsTimerInterval) {
            Console.WriteLine("Initialize Warlock Demonology Complex PvE (Beta) bot");
            followFocus = new WalkerBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentFocus);
        }
        public override void Rota() {
            corruption.ReCast(this.wowinfo, this.Target.Unit);
            immolate.ReCast(this.wowinfo, this.Target.Unit);
            baneofdoom.ReCast(this.wowinfo, this.Target.Unit);
            if (this.Target.Unit.HasBuff(immolate.ID) && this.Target.Unit.HasBuff(corruption.ID) && this.Target.Unit.HasBuff(baneofdoom.ID)) {
                shadowTrance.CastIfHasBuff(this.wowinfo, this.Player.Unit);
                handofguldan.SendCast();
                if (!moltenCore.CastIfHasBuff(this.wowinfo, this.Player.Unit) && !soulFire.CastIfHasBuff(this.wowinfo, this.Player.Unit)) {
                    shadowTrance.SendCast();
                }
            }
        }
    }
}
