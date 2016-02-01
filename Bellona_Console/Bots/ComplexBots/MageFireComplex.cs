using Bellona_Console.Controller;
using Bellona_Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellona_Console.Bots.ComplexBots {
    class MageFireComplex : ComplexBot {
        private DoT livingBomb = new DoT(44457, ConstController.WindowsVirtualKey.K_6);
        private DoT scorch = new DoT(22959, ConstController.WindowsVirtualKey.K_7);
        private Spell flameorb = new Spell(0, ConstController.WindowsVirtualKey.K_8);
        private Spell fireball = new Spell(0, ConstController.WindowsVirtualKey.K_9);
        private Spell combustion = new Spell(12654, ConstController.WindowsVirtualKey.VK_F12); //debuff ignite after critt  
        private Spell fireblast = new Spell(64343, ConstController.WindowsVirtualKey.VK_F11); // buff impact
        private Spell pyroblast = new Spell(48108, ConstController.WindowsVirtualKey.VK_F10); // buff hot streak
        private Spell arcaneexplosion = new Spell(0, ConstController.WindowsVirtualKey.VK_NUMPAD9);

        public MageFireComplex(uint rotaInterval, uint movementInterval) : base(rotaInterval, movementInterval, ComplexBotStance.DpsTargetRanged) {
            Console.WriteLine("Super Duper New Complex Bot Tester");
        }
        protected override void MovementEvent(object source) {
            base.MovementEvent(source);
            this.stance = base.SetStance();
            base.MoveBasedOnStance(this.stance);
        }
        protected override void RotaEvent(object source) {
            switch (stance) {
                case ComplexBotStance.AoEAtFocus:
                    arcaneexplosion.SendCast();
                    break;
                case ComplexBotStance.DpsTargetRanged:
                    livingBomb.ReCast(Program.ClientInfo, this.target.Unit);
                    pyroblast.CastIfHasBuff(Program.ClientInfo, this.player.Unit);
                    fireblast.CastIfHasBuff(Program.ClientInfo, this.player.Unit);
                    combustion.CastIfHasBuff(Program.ClientInfo, this.target.Unit);
                    if ((!this.target.Unit.HasBuff(scorch.ID) && !Program.ClientInfo.SpellIsPending) || (this.player.Unit.GetManaPercent() < 50)) {
                        scorch.SendCast();
                    }
                    if (this.player.Unit.MovingInfo.IsMoving) {
                        if (!Program.ClientInfo.SpellIsPending) {
                            scorch.SendCast();
                        }
                    }
                    else {
                        if (!Program.ClientInfo.SpellIsPending) {
                            fireball.SendCast();
                        }
                    }
                    break;
            }

        }
    }
}
