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




        public ShamanHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Shaman Resto Heal bot");
        }

        public override void Rota() {
            if (!this.Player.Unit.HasBuff(watershild.ID)) {
                watershild.SendCast();
            }
            if (this.Player.Unit.GetManaPercent() < 50) {
                manatide.SendCast();
            }
            if (!this.Focus.Unit.HasBuff(earthshild.ID)) {
                earthshild.SendCast();
            }
            if (this.Target.Unit.GetHealthPercent() < 95) {
                riptide.SendCast();
            }

            
        }
    }
}
