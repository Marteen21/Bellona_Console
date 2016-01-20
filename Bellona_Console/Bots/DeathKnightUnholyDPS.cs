using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class DeathKnightUnholyDPS : DPSBot {
        private DoT frostFever = new DoT(55095, Controller.ConstController.WindowsVirtualKey.K_8);
        private DoT bloodFever = new DoT(55078, Controller.ConstController.WindowsVirtualKey.K_9);
        private Spell hornofWinter = new Spell(57330, Controller.ConstController.WindowsVirtualKey.K_Ú);
        private Spell unholyPresence = new Spell(48265, Controller.ConstController.WindowsVirtualKey.K_Á);
        private DKRuneSpell scourgeStrike = new DKRuneSpell(123, Controller.ConstController.WindowsVirtualKey.K_7, new DKSpellRuneCost(new Rune(RuneType.Unholy, 1)));
        private DKRuneSpell necroticStrike = new DKRuneSpell(73975, Controller.ConstController.WindowsVirtualKey.K_É, new DKSpellRuneCost(new Rune(RuneType.Unholy, 1)));
        private Spell festeringStrike = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_6);
        private Spell DeathCoil = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_F);
        private Spell suddenDoom = new Spell(81340, Controller.ConstController.WindowsVirtualKey.K_F);
        public DeathKnightUnholyDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
        }
        public override void Rota() {
            if (!Player.Unit.HasBuff(hornofWinter.ID)) {
                hornofWinter.SendCast();
            }
            else if (!Player.Unit.HasBuff(unholyPresence.ID)) {
                unholyPresence.SendCast();
            }
            if (!frostFever.ReCast(this.wowinfo, this.Target.Unit) && !bloodFever.ReCast(this.wowinfo, this.Target.Unit)) {
                festeringStrike.SendCast();
            }
            suddenDoom.CastIfHasBuff(this.wowinfo, this.Player.Unit);
            necroticStrike.ReCast(this.wowinfo, this.Target.Unit);
            scourgeStrike.CastIfHasRunesFor(this.wowinfo);
            if (Player.Unit.GetManaPercent() > 70) {
                DeathCoil.SendCast();
            }
        }

    }
}
