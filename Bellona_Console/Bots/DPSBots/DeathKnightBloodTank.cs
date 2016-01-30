using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.DPSBots {
    class DeathKnightBloodTank : DPSBot {
        private DoT frostFever = new DoT(55095, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private DoT scarletFever = new DoT(55078, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private Spell DeathStrike = new Spell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private DKRuneSpell hearthStrike = new DKRuneSpell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD3, new DKSpellRuneCost(new Rune(RuneType.Blood, 1)));
        private DKRuneSpell RuneTap = new DKRuneSpell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD4, new DKSpellRuneCost(new Rune(RuneType.Blood, 1)));
        private Spell RuneStrike = new Spell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private DKRuneSpell BoneShield = new DKRuneSpell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD6, new DKSpellRuneCost(new Rune(RuneType.Unholy, 1)));


        private Spell DeathCoil = new Spell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD9);

        private Spell HornofWinter = new Spell(57330, Controller.ConstController.WindowsVirtualKey.VK_PRIOR);
        private Spell BloodPresence = new Spell(48263, Controller.ConstController.WindowsVirtualKey.VK_NEXT);
        public DeathKnightBloodTank(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
            Console.WriteLine("Initialize Death Knight Unholy DPS bot");
        }
        public override void Rota() {
            if (!Player.Unit.HasBuff(HornofWinter.ID)) {
                HornofWinter.SendCast();
            }
            else if (!Player.Unit.HasBuff(BloodPresence.ID)) {
                BloodPresence.SendCast();
            }
            if (Player.Unit.GetHealthPercent() < 70) {
                DeathStrike.SendCast();
            }
            else if (!frostFever.ReCast(this.wowinfo, this.Target.Unit) && !scarletFever.ReCast(this.wowinfo, this.Target.Unit)) {
                DeathStrike.SendCast();
            }
            if (Player.Unit.GetHealthPercent() < 70) {
                RuneTap.CastIfHasRunesFor(this.wowinfo);
                BoneShield.SendCast();
            }
            else {
                hearthStrike.CastIfHasRunesFor(this.wowinfo);
            }
        }

    }
}
