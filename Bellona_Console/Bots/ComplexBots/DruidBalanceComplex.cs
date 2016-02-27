using Bellona_Console.Controller;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {
    class DruidBalanceComplex : ComplexBot {

        Spell moonkin = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        DoT moonfire = new DoT(8921, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        DoT insectswarm = new DoT(5570, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        Spell starsurge = new Spell(93400, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        Spell starfire = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        Spell wrath = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        Spell starfall = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        Spell innervate = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD7);
        Spell barkskin = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD8);
        Spell solarbeam = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD9);
        Spell solarfire = new Spell(93402);
        Spell lunareclipse = new Spell(48518);
        Spell solareclipse = new Spell(48517);
        private bool balancepowerRight = true;

        public DruidBalanceComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetRanged) {
            Console.WriteLine("Druid Balance Complex Bot");
        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance = base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }

        protected override void RotaEvent(Object source) {
            switch (stance) {
                case ComplexBotStance.AoEAtFocus:

                    break;
                case ComplexBotStance.DpsTargetRanged:

                    if (this.player.Unit.GetHealthPercent() < 30) {
                        barkskin.SendCast();
                    }
                    if (this.player.Unit.GetManaPercent() < 60) {
                        innervate.SendCast();
                    }
                    if (this.player.Unit.Shapeshift != ShapeshiftForm.Moonkin && this.player.Unit.IsInCombat) {
                        moonkin.SendCast();
                    }
                    //if (this.target.Unit.CastingSpellID != 0) {
                    //    solarbeam.SendCast();
                    //}
                    if (this.player.Unit.HasBuff(lunareclipse.ID)) {
                        starfall.SendCast();
                    }
                    insectswarm.ReCast(Program.ClientInfo, this.target.Unit);
                    if (!this.target.Unit.HasBuff(moonfire.ID) && !this.target.Unit.HasBuff(solarfire.ID)) {
                        moonfire.SendCast();
                    }
                    starsurge.SendCast();
                    if (!Program.ClientInfo.SpellIsPending || this.player.Unit.CastingSpellID==0) {
                        if (this.player.Unit.HasBuff(solareclipse.ID)) {
                            wrath.SendCast();
                        }
                        else if(this.player.Unit.HasBuff(lunareclipse.ID)){
                            starfire.SendCast();
                        }
                        else if (player.Unit.BalancePower > 0) {
                            starfire.SendCast();
                        }
                        else {
                            wrath.SendCast();
                        }
                    }
                    break;
            }
        }
    }
}
