using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots {
    class MageFirePVEDPS : PvEDPSBot {

        private DoT livingBomb = new DoT(44457, ConstController.WindowsVirtualKey.K_6);
        private DoT scorch = new DoT(22959, ConstController.WindowsVirtualKey.K_7);
        private Spell flameorb = new Spell(0, ConstController.WindowsVirtualKey.K_8);
        private Spell fireball = new Spell(0, ConstController.WindowsVirtualKey.K_9);
        private Spell combustion = new Spell(12654, ConstController.WindowsVirtualKey.VK_F12); //debuff ignite after critt  
        private Spell fireblast = new Spell(64343, ConstController.WindowsVirtualKey.VK_F11); // buff impact
        private Spell pyroblast = new Spell(48108, ConstController.WindowsVirtualKey.VK_F10); // buff hot streak
        private WalkerBot followFocus;

        public MageFirePVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt, uint walkerTimerInterval) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize FireMage PVE DPS bot");
            followFocus = new WalkerBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentFocus, 10);
        }
        public MageFirePVEDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize FireMage DPS bot");
        }

        public override void Rota() {
            base.Rota();
            livingBomb.ReCast(this.wowinfo, this.Target.Unit);
            pyroblast.CastIfHasBuff(this.wowinfo, this.Player.Unit);
            fireblast.CastIfHasBuff(this.wowinfo, this.Player.Unit);
            combustion.CastIfHasBuff(this.wowinfo, this.Target.Unit);
            if ((!this.Target.Unit.HasBuff(scorch.ID) && !this.wowinfo.SpellIsPending) || (this.Player.Unit.GetManaPercent() < 50 && !this.wowinfo.SpellIsPending)) {
                scorch.SendCast();
            }
            if (this.Player.Unit.IsMoving) {
                if (!this.wowinfo.SpellIsPending) {
                    scorch.SendCast();
                }
            }
            else {
                if (!this.wowinfo.SpellIsPending) {
                    fireball.SendCast();
                }
            }


        }


    }
}
