using Bellona_Console.Controller;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {
    class RogueComplex : ComplexBot{

        private DoT hemo = new DoT(89775, ConstController.WindowsVirtualKey.VK_NUMPAD0);
        private DoT slicendice = new DoT(5171, ConstController.WindowsVirtualKey.VK_NUMPAD1);
        private DoT recup = new DoT(73651, ConstController.WindowsVirtualKey.VK_NUMPAD2);
        private DoT repture = new DoT(1943, ConstController.WindowsVirtualKey.VK_NUMPAD3);
        private Spell evis = new Spell(123, ConstController.WindowsVirtualKey.VK_NUMPAD4);
        private Spell backstab = new Spell(1454, ConstController.WindowsVirtualKey.VK_NUMPAD5);
        private Spell fanofknives = new Spell(1949, ConstController.WindowsVirtualKey.VK_NUMPAD6);
        public RogueComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetBackMelee) {
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
                    fanofknives.SendCast();
                    break;
                case ComplexBotStance.DpsTargetBackMelee:
                    if (Program.ClientInfo.ComboPoints == 5) {
                        if (!recup.ReCast(Program.ClientInfo, player.Unit) && !repture.ReCast(Program.ClientInfo, target.Unit) && !slicendice.ReCast(Program.ClientInfo, player.Unit)) {
                            evis.SendCast();
                        }
                    }
                    else {
                        hemo.ReCast(Program.ClientInfo, target.Unit);
                        backstab.SendCast();
                    }
                    break;
            }
        }
    }
}
