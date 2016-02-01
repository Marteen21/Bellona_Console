using Bellona_Console.Bots.Other;
using Bellona_Console.Controller;
using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.HealBots {
    class PriestDiscHeal : HealPartyBot {
        private Spell heal = new Spell(123, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private Spell flashHeal = new Spell(17, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private Spell greaterHeal = new Spell(17, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private Spell penance = new Spell(17, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private Spell prayerofHealing = new Spell(2, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell fade = new Spell(17, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD6);
        private Spell powerWordShield = new Spell(17, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD7);
        private DoT renew = new DoT(139, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD8);
        private Spell prayerOfMending = new Spell(16, Controller.ConstController.WindowsVirtualKey.VK_NUMPAD9);
        private DoT drinking = new DoT(80167,ConstController.WindowsVirtualKey.VK_NUMPAD4);
        private WalkerBot followFocus;
        public PriestDiscHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint healTimerInterval, uint walkerTimerInterval) : base(wowProcess, globalinfo, healTimerInterval) {
            Console.WriteLine("Priest Beta Healing");
            followFocus = new WalkerBot(this.wow, this.wowinfo, walkerTimerInterval, WalkTargetType.CurrentFocus, 10);
        }
        public PriestDiscHeal(BlackMagic wowProcess, WoWGlobal globalinfo, uint healTimerInterval) : base(wowProcess, globalinfo, healTimerInterval) {
            Console.WriteLine("Priest Beta Healing");
        }
        public override void Rota() {
            base.Rota();
            uint targethealthpercent = Target.Unit.GetHealthPercent();
            if (NumberofLowHPPartyMembers > 3) {
                prayerofHealing.SendCast();
            }
            if (targethealthpercent > 93) {
                if(!Player.Unit.IsInCombat && Player.Unit.GetManaPercent()<80) {
                    drinking.ReCast(wowinfo, Player.Unit);
                }
            }
            else if (targethealthpercent > 75) {
                if (Target.Unit.HasBuff(powerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                    powerWordShield.SendCast();
                }
                if (Player.Unit.MovingInfo.IsMoving && Target.Unit.HasBuff(6788)) {
                    renew.ReCast(wowinfo, Target.Unit);
                }
                if (!Player.Unit.MovingInfo.IsMoving) {
                    penance.SendCast();
                    heal.SendCast();
                }
            }
            else if (targethealthpercent > 35) {
                if (Target.Unit.HasBuff(powerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                    powerWordShield.SendCast();
                }
                if (Player.Unit.MovingInfo.IsMoving && Target.Unit.HasBuff(6788)) {
                    renew.ReCast(wowinfo, Target.Unit);
                }
                if (!Player.Unit.MovingInfo.IsMoving) {
                    penance.SendCast();
                    greaterHeal.SendCast();
                }
            }
            else {
                if (Player.GUID == Target.GUID) {
                    fade.SendCast();
                }
                if (Target.Unit.HasBuff(powerWordShield.ID) && !Target.Unit.HasBuff(6788)) {
                    powerWordShield.SendCast();
                }
                if (Player.Unit.MovingInfo.IsMoving && Target.Unit.HasBuff(6788)) {
                    renew.ReCast(wowinfo, Target.Unit);
                }
                if (!Player.Unit.MovingInfo.IsMoving) {
                    penance.SendCast();
                    flashHeal.SendCast();
                }

            }
        }

    }
}


