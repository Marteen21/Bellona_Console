using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;

namespace Bellona_Console.Bots.HealBots {
    class ShamanHeal : HealPartyBot {

        Spell watershild = new Spell(52127, ConstController.WindowsVirtualKey.K_6);
        Spell manatide = new Spell(0, ConstController.WindowsVirtualKey.K_7);
        Spell earthshild = new Spell(974, ConstController.WindowsVirtualKey.K_8);
        Spell riptide = new Spell(0, ConstController.WindowsVirtualKey.K_9);
        Spell tidalwaves = new Spell(53390);
        Spell greaterhw = new Spell(0, ConstController.WindowsVirtualKey.VK_F12);
        Spell hw = new Spell(0, ConstController.WindowsVirtualKey.VK_F11);
        Spell natureswiftness = new Spell(0, ConstController.WindowsVirtualKey.VK_F9);
        Spell chainheal = new Spell(0, ConstController.WindowsVirtualKey.VK_F10);
        private WalkerBot focusfollow;

        public ShamanHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Shaman Resto Heal bot");
        }
        public ShamanHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint healinterval, uint walkerinterval) : base(wowProcess, globalinfo, healinterval) {
            Console.WriteLine("Initialize Shaman Resto Heal bot");
            focusfollow = new WalkerBot(wow, wowinfo, walkerinterval, WalkTargetType.CurrentFocus,10);
        }

        public override void Rota() {
            base.Rota();
            if (!this.Player.Unit.HasBuff(watershild.ID)) {
                watershild.SendCast();
            }
            if (this.Player.Unit.GetManaPercent() < 50) {
                manatide.SendCast();
            }
            if (!this.Focus.Unit.HasBuff(earthshild.ID) && this.Focus.GUID !=0) {
                earthshild.SendCast();
            }
            if (this.Target.Unit.GetHealthPercent() < 95) {
                riptide.SendCast();
            }
            if (this.Player.Unit.HasBuff(tidalwaves.ID) && this.Target.Unit.GetHealthPercent()<90 && !this.wowinfo.SpellIsPending ) {
                if (this.Target.Unit.GetHealthPercent() < 30) {
                    greaterhw.SendCast();
                }
                else {
                    hw.SendCast();
                }
            }
            if (this.Target.Unit.GetHealthPercent() < 10) {
                natureswiftness.SendCast();
            }
            if (this.NumberofLowHPPartyMembers >= 3) {
                chainheal.SendCast();
            }


        }
    }
}
