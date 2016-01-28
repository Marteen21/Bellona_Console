using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.HealBots {
    class PriestDiscHeal : HealPartyBot {
        private Spell heal = new Spell(123, Controller.ConstController.WindowsVirtualKey.K_5);
        private DoT renew = new DoT(139, Controller.ConstController.WindowsVirtualKey.K_4);
        private Spell PowerWordShield = new Spell(17, Controller.ConstController.WindowsVirtualKey.K_6);
        private Spell penance = new Spell(17, Controller.ConstController.WindowsVirtualKey.K_7);
        private Spell flashheal = new Spell(17, Controller.ConstController.WindowsVirtualKey.K_8);
        private Spell fade = new Spell(17, Controller.ConstController.WindowsVirtualKey.K_9);

        private WalkerBot followFocus;
        public PriestDiscHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint healTimerInterval, uint walkerTimerInterval) : base(wowProcess, globalinfo, healTimerInterval) {
            Console.WriteLine("Priest Beta Healing");
            followFocus = new WalkerBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentFocus);
        }
        public PriestDiscHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint healTimerInterval) : base(wowProcess, globalinfo, healTimerInterval) {
            Console.WriteLine("Priest Beta Healing");
        }
        public override void Rota() {
            base.Rota();
            uint targethealthpercent = Target.Unit.GetHealthPercent();
            if (targethealthpercent > 95) {

            }
            else if (targethealthpercent > 75) {
                if (Player.GUID == Target.GUID) {
                    fade.SendCast();
                }
                if (Player.Unit.IsMoving) {
                    renew.ReCast(wowinfo, Target.Unit);
                    if (Target.Unit.HasBuff(PowerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                        PowerWordShield.SendCast();
                    }
                }
                else {
                    heal.SendCast();
                }
            }
            else if (targethealthpercent > 60) {
                renew.ReCast(wowinfo, Target.Unit);
                if (Target.Unit.HasBuff(PowerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                    PowerWordShield.SendCast();
                }
                if (!Player.Unit.IsMoving) {
                    penance.SendCast();
                    heal.SendCast();
                }
            }
            else {
                renew.ReCast(wowinfo, Target.Unit);
                if (Target.Unit.HasBuff(PowerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                    PowerWordShield.SendCast();
                }
                if (!Player.Unit.IsMoving) {
                    penance.SendCast();
                    flashheal.SendCast();
                }

            }
        }

    }
}


