using Bellona_Console.Models;
using Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellona_Console.Controller;
using Bellona_Console.Bots.Other;

namespace Bellona_Console.Bots.ComplexBots {
    class PaladinRetriComplex : ComplexBot {

        //private Spell might = new Spell(79102, ConstController.WindowsVirtualKey.K_6);
        private Spell plea = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private Spell exorcism = new Spell(59578, ConstController.WindowsVirtualKey.VK_NUMPAD4); //art of war
        private Spell inquisition = new Spell(84963, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        //private Spell truth = new Spell(31801, ConstController.WindowsVirtualKey.K_9);
        private Spell judgement = new Spell(89906, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private Spell templar = new Spell(90174, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private Spell crusader = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell hammer = new Spell(31884, ConstController.WindowsVirtualKey.VK_NUMPAD6);//avenging wrath buff alat ingyen lehet tolni
        //private Spell repetance = new Spell(0, ConstController.WindowsVirtualKey.VK_F7);

        public PaladinRetriComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetBackMelee) {
            Console.WriteLine("paladin complex bot");
        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance = base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }
        protected override void RotaEvent(object source) {
            if (stance==ComplexBotStance.DpsTargetBackMelee || stance==ComplexBotStance.AoEAtFocus) {

                //repetance.SendCast();
                if (this.player.Unit.GetManaPercent() < 50 && this.player.Unit.GetHealthPercent() > 70) {
                    plea.SendCast();
                }
                if (this.player.Unit.HasBuff(exorcism.ID)) {
                    exorcism.SendCast();
                }
                if (!this.player.Unit.HasBuff(inquisition.ID) && (this.player.Unit.HolyPower == 3 || this.player.Unit.HasBuff(templar.ID))) {
                    inquisition.SendCast();
                }
                judgement.SendCast();
                crusader.SendCast();
                if (this.player.Unit.HolyPower == 3 || this.player.Unit.HasBuff(templar.ID)) {
                    templar.SendCast();
                }
                if (this.target.Unit.GetHealthPercent() > 20 || this.player.Unit.HasBuff(hammer.ID)) {
                    hammer.SendCast();
                } 
            }
        }
    }
}
