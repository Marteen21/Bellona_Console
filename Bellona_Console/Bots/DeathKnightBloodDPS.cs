using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots {
    class DeathKnightBloodDPS : DPSBot{
        private DoT frostFever = new DoT(55095, Controller.ConstController.WindowsVirtualKey.K_8);
        private DoT scarletFever = new DoT(81130, Controller.ConstController.WindowsVirtualKey.K_9);
        private DKRuneSpell hearthStrike = new DKRuneSpell(123, Controller.ConstController.WindowsVirtualKey.K_7, new DKSpellRuneCost(new Rune(RuneType.Blood, 1)));
        private Spell DeathStrike = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_6);
        private Spell HornofWinter = new Spell(57330, Controller.ConstController.WindowsVirtualKey.K_Ú);
        private Spell BloodPresence = new Spell(48263,Controller.ConstController.WindowsVirtualKey.K_Á);
        private Spell DeathCoil = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_F);
        private Spell RuneStrike = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_4);

        public DeathKnightBloodDPS(BlackMagic wowProcess, WoWGlobal globalinfo, uint tt) : base(wowProcess, globalinfo, tt) {
        }
        public override void Rota() {
            if (!Player.Unit.HasBuff(HornofWinter.ID)) {
                HornofWinter.SendCast();
            }
            else if(!Player.Unit.HasBuff(BloodPresence.ID)){
                BloodPresence.SendCast();
            }
            if(!frostFever.ReCast(this.wowinfo,this.Target.Unit)&& !scarletFever.ReCast(this.wowinfo, this.Target.Unit)) {
                DeathStrike.SendCast();
            }
            if (Player.Unit.GetManaPercent() > 30) {
                RuneStrike.SendCast();
                DeathCoil.SendCast();
            }
            hearthStrike.CastIfHasRunesFor(this.wowinfo, this.Player.Unit);
        }

    }
}
