using Bellona_Console.Controller;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {
    class DruidFeralComplex : ComplexBot {
        private DoT rake = new DoT(1822, ConstController.WindowsVirtualKey.K_2);
        private DoT rip = new DoT(1079, ConstController.WindowsVirtualKey.K_R);
        private DoT mangle = new DoT(33876, ConstController.WindowsVirtualKey.K_5);
        private DoT FF = new DoT(91565, ConstController.WindowsVirtualKey.K_4);
        private DoT pounce = new DoT(9005, ConstController.WindowsVirtualKey.K_1);
        private DoT shred = new DoT(58180, ConstController.WindowsVirtualKey.K_1);
        private Spell prowl = new Spell(5215);
        private Spell mark = new Spell(79061, ConstController.WindowsVirtualKey.K_B);
        private Spell cyclon = new Spell(69369, ConstController.WindowsVirtualKey.K_G);
        private bool needcat = false;

        public DruidFeralComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetBackMelee) {
            Console.WriteLine("Super Duper New Complex Bot Tester");
        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance = base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }
        protected override void RotaEvent(object source) {
            if (!this.player.Unit.HasBuff(mark.ID) && this.player.Unit.Shapeshift == ShapeshiftForm.Normal) {
                mark.SendCast();
            }
            if (this.player.Unit.Shapeshift == ShapeshiftForm.Cat) {
                needcat = false;
                if (!this.player.Unit.HasBuff(prowl.ID)) {
                    rake.ReCast(Program.ClientInfo, this.target.Unit);
                    mangle.ReCast(Program.ClientInfo, this.target.Unit);
                    FF.ReCast(Program.ClientInfo, this.target.Unit);
                    if (Program.ClientInfo.ComboPoints == 5) {
                        rip.ReCast(Program.ClientInfo, this.target.Unit);
                    }
                    if (this.player.Unit.SecondaryPower > 60) {
                        shred.SendCast();
                    }
                }
                else {
                    pounce.SendCast();
                }

            }
        }

    }
}
