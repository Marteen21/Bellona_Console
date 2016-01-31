using Bellona_Console.Controller;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {
    class WarlockDemoComplex : ComplexBot {
        private Spell shadowTrance = new Spell(17941, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private Spell moltenCore = new Spell(71165, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private DoT immolate = new DoT(348, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private DoT corruption = new DoT(172, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private DoT baneofdoom = new DoT(603, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        private DoT handofguldan = new DoT(86000, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell soulFire = new Spell(63167, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        private Spell lifeTap = new Spell(1454, ConstController.WindowsVirtualKey.VK_NUMPAD7);
        private Spell hellfire = new Spell(1949, ConstController.WindowsVirtualKey.VK_NUMPAD8);

        public WarlockDemoComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetRanged) {
            Console.WriteLine("Super Duper New Complex Bot Tester");
        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance = base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }

        protected override void RotaEvent(Object source) {
            switch (stance) {
                case ComplexBotStance.AoEAtFocus:
                    if (!player.Unit.HasBuff(hellfire.ID)) {
                        hellfire.SendCast();
                    }
                    break;
                case ComplexBotStance.DpsTargetRanged:
                    if (this.player.Unit.GetHealthPercent() > 40 && this.player.Unit.GetManaPercent() < 50) {
                        lifeTap.SendCast();
                    }
                    corruption.ReCast(Program.ClientInfo, this.target.Unit);
                    immolate.ReCast(Program.ClientInfo, this.target.Unit);
                    baneofdoom.ReCast(Program.ClientInfo, this.target.Unit);
                    if (this.target.Unit.HasBuff(immolate.ID) && this.target.Unit.HasBuff(corruption.ID) && this.target.Unit.HasBuff(baneofdoom.ID)) {
                        shadowTrance.CastIfHasBuff(Program.ClientInfo, this.player.Unit);
                        handofguldan.SendCast();
                        if (!moltenCore.CastIfHasBuff(Program.ClientInfo, this.player.Unit) && !soulFire.CastIfHasBuff(Program.ClientInfo, this.player.Unit)) {
                            shadowTrance.SendCast();
                        }
                    }
                    break;
            }
        }
    }
}
